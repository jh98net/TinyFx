using SqlSugar;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TinyFx.Data.SqlSugar;
using TinyFx.Reflection;

namespace TinyFx.DbCaching
{
    internal class DbCacheMemory<TEntity> : IDbCacheMemory<TEntity>, IDbCacheMemoryUpdate
        where TEntity : class, new()
    {
        #region Properties
        public SugarTable TableAttribute { get; }
        public string ConfigId { get; }
        public string CachKey { get; }
        public List<string> PrimaryKeys { get; }
        public List<TEntity> DbData { get; private set; }

        // key: 一对一类型(主键或唯一索引)
        public ConcurrentDictionary<string, Dictionary<string, TEntity>> SingleDict { get; } = new();
        // key: 一对多类型
        public ConcurrentDictionary<string, Dictionary<string, List<TEntity>>> ListDict { get; } = new();
        public ConcurrentDictionary<string, object> CustomDict { get; } = new();
        public DbCacheMemory(params object[] routingDbKeys)
        {
            TableAttribute = typeof(TEntity).GetCustomAttribute<SugarTable>();
            if (TableAttribute == null)
                throw new Exception($"内存缓存类型仅支持有SugarTableAttribute的类。type: {typeof(TEntity).FullName}");
            var routingProvider = DIUtil.GetRequiredService<IDbRoutingProvider>();
            ConfigId = routingProvider.RouteDb<TEntity>(routingDbKeys);
            CachKey = DbCachingUtil.GetCacheKey(ConfigId, TableAttribute.TableName);
            PrimaryKeys = DbUtil.GetDb(ConfigId).DbMaintenance.GetPrimaries(TableAttribute.TableName);
            DbData = GetInitData().GetTaskResult();
        }
        #endregion

        public TEntity GetSingle(object id)
        {
            if (PrimaryKeys?.Count != 1)
                throw new Exception($"多主键不支持DbCacheMemory<T>.GetById。type:{typeof(TEntity).FullName}");
            return GetSingleValue(PrimaryKeys[0], Convert.ToString(id));
        }
        public TEntity GetSingle(Expression<Func<TEntity>> expr)
        {
            var keys = GetKeys(expr);
            return GetSingleValue(keys.DictKey, keys.ValueKey);
        }
        public List<TEntity> GetList(Expression<Func<TEntity>> expr)
        {
            var keys = GetKeys(expr);
            return GetListValue(keys.DictKey, keys.ValueKey);
        }
        public List<TEntity> GetAllList() => DbData;

        /// <summary>
        /// 自定义单字典缓存，name唯一
        /// </summary>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public Dictionary<string, TEntity> GetOrAddCustom(string name, Func<List<TEntity>, Dictionary<string, TEntity>> func)
        {
            WaitForUpdate();
            var key = $"_CUSTOM_SINGLE_{name}";
            return SingleDict.GetOrAdd(key, (k) =>
            {
                return func(DbData);
            });
        }
        /// <summary>
        /// 自定义列表字典缓存，name唯一
        /// </summary>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public Dictionary<string, List<TEntity>> GetOrAddCustom(string name, Func<List<TEntity>, Dictionary<string, List<TEntity>>> func)
        {
            WaitForUpdate();
            var key = $"_CUSTOM_LIST_{name}";
            return ListDict.GetOrAdd(key, (k) =>
            {
                return func(DbData);
            });
        }
        public TCache GetOrAddCustom<TCache>(string name, Func<List<TEntity>, TCache> func)
        {
            WaitForUpdate();
            var key = $"_CUSTOM_OBJECT_{name}";
            return (TCache)CustomDict.GetOrAdd(key, (k) =>
            {
                return func(DbData);
            });
        }

        #region Utils
        private async Task<List<TEntity>> GetInitData()
        {
            var result = await new DbCacheDataDCache().GetOrLoadAsync(CachKey);
            if (!result.HasValue)
                throw new Exception($"DbCacheMemory没有获取缓存之.type:{this.GetType().FullName}");
            return SerializerUtil.DeserializeJson<List<TEntity>>(result.Value);
        }
        private (string DictKey, string ValueKey) GetKeys(Expression<Func<TEntity>> expr)
        {
            //var visitor = new DbCacheMemoryExpressionVisitor();
            //visitor.Visit(expr);
            //return visitor.GetKeys();
            var entityType = typeof(TEntity);          
            var fieldBuilder = new StringBuilder();
            var valueBuilder = new StringBuilder();
            switch (expr.Body.NodeType)
            {
                case ExpressionType.MemberInit:
                    var memberInitExpr = expr.Body as MemberInitExpression;
                    foreach (var elementExpr in memberInitExpr.Bindings)
                    {
                        if (elementExpr.BindingType != MemberBindingType.Assignment)
                            throw new Exception("暂时不支持除MemberBindingType.Assignment类型外的成员绑定表达式");

                        if (elementExpr is MemberAssignment memberAssignment)
                        {
                            if (fieldBuilder.Length > 0)
                            {
                                fieldBuilder.Append('|');
                                valueBuilder.Append('|');
                            }
                            var memberValue = this.Evaluate(memberAssignment.Expression);
                            fieldBuilder.Append(memberAssignment.Member.Name);
                            valueBuilder.Append(Convert.ToString(memberValue));
                        }
                    } 
                    break;
                default: throw new NotSupportedException("不支持的表达式");
            }
            return (fieldBuilder.ToString(), valueBuilder.ToString());
        }

        private TEntity GetSingleValue(string dictKey, string valueKey)
        {
            WaitForUpdate();
            var dict = SingleDict.GetOrAdd(dictKey, x =>
            {
                var value = new Dictionary<string, TEntity>();
                DbData.ForEach(entry =>
                {
                    var names = dictKey.Split('|');
                    var vvs = names.Select(n => ReflectionUtil.GetPropertyValue(entry, n));
                    var vkey = string.Join('|', vvs);
                    if (value.ContainsKey(vkey))
                        throw new Exception($"IDbCacheMemory获取单个缓存时不唯一。type:{typeof(TEntity).FullName} dictKey:{dictKey} valueKey:{valueKey}");
                    value.Add(vkey, entry);
                });
                return value;
            });
            return dict.TryGetValue(valueKey, out TEntity ret) ? ret : null;
        }
        private List<TEntity> GetListValue(string dictKey, string valueKey)
        {
            WaitForUpdate();
            var dict = ListDict.GetOrAdd(dictKey, x =>
            {
                var value = new Dictionary<string, List<TEntity>>();
                DbData.ForEach(entry =>
                {
                    var names = dictKey.Split('|');
                    var vvs = names.Select(n => ReflectionUtil.GetPropertyValue(entry, n));
                    var vkey = string.Join('|', vvs);
                    if (value.ContainsKey(vkey))
                        value[vkey].Add(entry);
                    else
                        value.Add(vkey, new List<TEntity> { entry });
                });
                return value;
            });
            return dict.TryGetValue(valueKey, out List<TEntity> ret) ? ret : null;
        }
        #endregion

        #region Update
        private bool _isUpdating = false;
        private void WaitForUpdate()
        {
            if (!_isUpdating) return;
            while (_isUpdating)
            {
                Thread.Sleep(100);
            }
        }
        private List<TEntity> _updateList;
        public void BeginUpdate(string data)
        {
            _updateList = SerializerUtil.DeserializeJson<List<TEntity>>(data);
        }
        public void EndUpdate()
        {
            _isUpdating = true;
            try
            {
                DbData = _updateList;
                SingleDict.Clear();
                ListDict.Clear();
                CustomDict.Clear();
            }
            finally
            {
                _isUpdating = false;
            }
        }
        #endregion

        private object Evaluate(Expression expr)
        {
            var lambdaExpr = Expression.Lambda(expr);
            return lambdaExpr.Compile().DynamicInvoke();
        }
    }
}
