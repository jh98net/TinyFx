using SqlSugar;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TinyFx.Data.SqlSugar;
using TinyFx.Reflection;
using static Grpc.Core.Metadata;

namespace TinyFx.DbCaching
{
    public class DbCacheMemory<TEntity> : IDbCacheMemoryUpdate
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
                throw new Exception($"DbCacheMemory内存缓存类型仅支持有SugarTableAttribute的类。type: {typeof(TEntity).FullName}");
            var routingProvider = DIUtil.GetRequiredService<IDbRoutingProvider>();
            ConfigId = routingProvider.RouteDb<TEntity>(routingDbKeys);
            CachKey = DbCachingUtil.GetCacheKey(ConfigId, TableAttribute.TableName);
            PrimaryKeys = DbUtil.GetDb(ConfigId).DbMaintenance.GetPrimaries(TableAttribute.TableName);
            DbData = GetInitData().GetTaskResult();
        }
        #endregion

        public List<TEntity> GetAllList() => DbData;

        #region GetSingle
        public TEntity GetSingle(object id)
        {
            if (PrimaryKeys?.Count != 1)
                throw new Exception($"多主键不支持DbCacheMemory<T>.GetById。type:{typeof(TEntity).FullName}");
            return GetSingleByKey(PrimaryKeys[0], Convert.ToString(id));
        }
        public TEntity GetSingle(Expression<Func<TEntity>> expr)
        {
            var keys = GetKeys(expr);
            return GetSingleByKey(keys.FieldsKey, keys.ValuesKey);
        }
        public TEntity GetSingle(Expression<Func<TEntity, object>> fieldsExpr, object valuesEntity)
        {
            var keys = GetKeys(fieldsExpr, valuesEntity);
            return GetSingleByKey(keys.FieldsKey, keys.ValuesKey);
        }
        public TEntity GetSingleByKey(string fieldsKey, string valuesKey)
        {
            WaitForUpdate();
            var dict = SingleDict.GetOrAdd(fieldsKey, x =>
            {
                var value = new Dictionary<string, TEntity>();
                DbData.ForEach(entry =>
                {
                    var names = fieldsKey.Split('|');
                    var vvs = names.Select(n => ReflectionUtil.GetPropertyValue(entry, n));
                    var vkey = string.Join('|', vvs);
                    if (value.ContainsKey(vkey))
                        throw new Exception($"IDbCacheMemory获取单个缓存时不唯一。type:{typeof(TEntity).FullName} dictKey:{fieldsKey} valueKey:{valuesKey}");
                    value.Add(vkey, entry);
                });
                return value;
            });
            return dict.TryGetValue(valuesKey, out TEntity ret) ? ret : null;
        }
        #endregion

        #region GetList
        public List<TEntity> GetList(Expression<Func<TEntity>> expr)
        {
            var keys = GetKeys(expr);
            return GetListByKey(keys.FieldsKey, keys.ValuesKey);
        }
        public List<TEntity> GetList(Expression<Func<TEntity, object>> fieldsExpr, object valuesEntity)
        {
            var keys = GetKeys(fieldsExpr, valuesEntity);
            return GetListByKey(keys.FieldsKey, keys.ValuesKey);
        }
        public List<TEntity> GetListByKey(string fieldsKey, string valuesKey)
        {
            WaitForUpdate();
            var dict = ListDict.GetOrAdd(fieldsKey, x =>
            {
                var value = new Dictionary<string, List<TEntity>>();
                DbData.ForEach(entry =>
                {
                    var names = fieldsKey.Split('|');
                    var vvs = names.Select(n => ReflectionUtil.GetPropertyValue(entry, n));
                    var vkey = string.Join('|', vvs);
                    if (value.ContainsKey(vkey))
                        value[vkey].Add(entry);
                    else
                        value.Add(vkey, new List<TEntity> { entry });
                });
                return value;
            });
            return dict.TryGetValue(valuesKey, out List<TEntity> ret) ? ret : null;
        }
        #endregion

        #region GetOrAddCustom
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
        #endregion

        #region Utils
        private async Task<List<TEntity>> GetInitData()
        {
            var result = await new DbCacheDataDCache().GetOrLoadAsync(CachKey);
            if (!result.HasValue)
                throw new Exception($"DbCacheMemory没有获取缓存之.type:{this.GetType().FullName}");
            return SerializerUtil.DeserializeJson<List<TEntity>>(result.Value);
        }
        private (string FieldsKey, string ValuesKey) GetKeys(Expression<Func<TEntity>> expr)
        {
            //var visitor = new DbCacheMemoryExpressionVisitor();
            //visitor.Visit(expr);
            //return visitor.GetKeys();

            if (expr.Body.NodeType != ExpressionType.MemberInit)
                throw new Exception("DbCacheMemory暂时不支持除ExpressionType.MemberInit类型外的成员绑定表达式");
            var memberInitExpr = expr.Body as MemberInitExpression;
            var fields = new string[memberInitExpr.Bindings.Count];
            var values = new string[memberInitExpr.Bindings.Count];
            for (int i = 0; i < fields.Length; i++)
            {
                var elementExpr = memberInitExpr.Bindings[i];
                if (elementExpr.BindingType != MemberBindingType.Assignment)
                    throw new Exception("DbCacheMemory暂时不支持除MemberBindingType.Assignment类型外的成员绑定表达式");
                if (elementExpr is MemberAssignment memberAssignment)
                {
                    fields[i] = memberAssignment.Member.Name;
                    var memberValue = this.Evaluate(memberAssignment.Expression);
                    values[i] = Convert.ToString(memberValue);
                }
            }
            return (string.Join('|', fields), string.Join('|', values));
        }
        private (string FieldsKey, string ValuesKey) GetKeys(Expression<Func<TEntity, object>> fieldsExpr, object valuesEntity)
        {
            switch (fieldsExpr.Body)
            {
                case NewExpression newExpr:
                    var fields = new string[newExpr.Arguments.Count];
                    var values = new string[newExpr.Arguments.Count];
                    for (var i = 0; i < newExpr.Arguments.Count; i++)
                    {
                        var item = newExpr.Arguments[i];
                        if (item is MemberExpression newMemExpr)
                        {
                            fields[i] += newMemExpr.Member.Name;
                            values[i] += Convert.ToString(ReflectionUtil.GetPropertyValue(valuesEntity, newMemExpr.Member.Name));
                        }
                    }
                    return (string.Join('|', fields), string.Join('|', values));
                case MemberExpression memExpr:
                    return (memExpr.Member.Name, Convert.ToString(valuesEntity));
                    //case UnaryExpression oneExpr:
                    //    if (!(oneExpr.Operand is MemberExpression memExpr2))
                    //        throw new Exception("DbCacheMemory使用单列单值获取keys时仅支持UnaryExpression和MemberExpression");
                    //    return (memExpr2.Member.Name, Convert.ToString(valuesEntity));
            }
            throw new Exception("DbCacheMemory仅支持NewExpression和MemberExpression表达式");
        }
        #endregion

        #region Update
        private volatile bool _isUpdating = false;
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
