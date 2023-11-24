using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
        public string TableName { get; }
        public string CachKey { get; }
        public List<string> PrimaryKeys { get; }
        public List<TEntity> DbData { get; private set; }

        // key: 一对一类型(主键或唯一索引)
        public ConcurrentDictionary<string, Dictionary<string, TEntity>> SingleDict { get; } = new();
        // key: 一对多类型
        public ConcurrentDictionary<string, Dictionary<string, List<TEntity>>> ListDict { get; } = new();
        public ConcurrentDictionary<string, object> CustomDict { get; } = new();
        public DbCacheMemory(params object[] splitDbKeys)
        {
            TableAttribute = typeof(TEntity).GetCustomAttribute<SugarTable>();
            if (TableAttribute == null)
                throw new Exception($"DbCacheMemory内存缓存类型仅支持有SugarTableAttribute的类。type: {typeof(TEntity).FullName}");
            var spliteProvider = DIUtil.GetRequiredService<IDbSplitProvider>();
            ConfigId = spliteProvider.SplitDb<TEntity>(splitDbKeys);
            TableName = TableAttribute.TableName;
            CachKey = DbCachingUtil.GetCacheKey(ConfigId, TableName);
            PrimaryKeys = DbUtil.GetDb(ConfigId).DbMaintenance.GetPrimaries(TableName);
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
            return await new PageDataProvider(ConfigId, TableName).GetRedisValues<TEntity>();
        }
        private (string FieldsKey, string ValuesKey) GetKeys(Expression<Func<TEntity>> expr)
        {
            //var visitor = new DbCacheMemoryExpressionVisitor();
            //visitor.Visit(expr);
            //return visitor.GetKeys();

            if (expr.Body.NodeType != ExpressionType.MemberInit)
                throw new Exception("DbCacheMemory暂时不支持除ExpressionType.MemberInit类型外的成员绑定表达式");
            var memberInitExpr = expr.Body as MemberInitExpression;
            var items = new List<(string field, string value)>(memberInitExpr.Bindings.Count);
            for (int i = 0; i < memberInitExpr.Bindings.Count; i++)
            {
                var elementExpr = memberInitExpr.Bindings[i];
                if (elementExpr.BindingType != MemberBindingType.Assignment)
                    throw new Exception("DbCacheMemory暂时不支持除MemberBindingType.Assignment类型外的成员绑定表达式");
                if (elementExpr is MemberAssignment memberAssignment)
                {
                    var memberValue = this.Evaluate(memberAssignment.Expression);
                    var field = memberAssignment.Member.Name;
                    var value = Convert.ToString(memberValue);
                    items.Add((field, value));
                }
            }
            return GetKeys(items);
        }
        private (string FieldsKey, string ValuesKey) GetKeys(Expression<Func<TEntity, object>> fieldsExpr, object valuesEntity)
        {
            switch (fieldsExpr.Body)
            {
                case NewExpression newExpr:
                    var items = new List<(string field, string value)>(newExpr.Arguments.Count);
                    for (var i = 0; i < newExpr.Arguments.Count; i++)
                    {
                        var item = newExpr.Arguments[i];
                        if (item is MemberExpression newMemExpr)
                        {
                            var field = newMemExpr.Member.Name;
                            var value = Convert.ToString(ReflectionUtil.GetPropertyValue(valuesEntity, newMemExpr.Member.Name));
                            items.Add((field, value));
                        }
                    }
                    return GetKeys(items);
                case MemberExpression memExpr:
                    return (memExpr.Member.Name, Convert.ToString(valuesEntity));
                case UnaryExpression oneExpr:
                    if (!(oneExpr.Operand is MemberExpression memExpr2))
                        throw new Exception("DbCacheMemory使用单列单值获取keys时仅支持UnaryExpression和MemberExpression");
                    return (memExpr2.Member.Name, Convert.ToString(valuesEntity));
            }
            throw new Exception("DbCacheMemory仅支持NewExpression和MemberExpression表达式");
        }
        private (string FieldsKey, string ValuesKey) GetKeys(List<(string field, string value)> items)
        {
            items.Sort();
            return (string.Join('|', items.Select(x => x.field)), string.Join('|', items.Select(x => x.value)));
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
        public void BeginUpdate(List<string> datas)
        {
            var ret = new List<TEntity>();
            foreach (var data in datas)
            {
                var items = SerializerUtil.DeserializeJson<List<TEntity>>(data);
                ret.AddRange(items);
            }
            _updateList = ret;
        }
        public void EndUpdate()
        {
            try
            {
                var oldList = DbData;
                DbData = _updateList;
                SingleDict.Clear();
                ListDict.Clear();
                CustomDict.Clear();
                _isUpdating = true;
                UpdateCallback?.Invoke(oldList, DbData);
            }
            finally
            {
                _isUpdating = false;
            }
        }
        public Action<List<TEntity>, List<TEntity>> UpdateCallback;
        #endregion

        private object Evaluate(Expression expr)
        {
            var lambdaExpr = Expression.Lambda(expr);
            return lambdaExpr.Compile().DynamicInvoke();
        }
    }
}
