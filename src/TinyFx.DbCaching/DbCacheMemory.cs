using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Data.SqlSugar;
using TinyFx.Reflection;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TinyFx.DbCaching
{
    internal class DbCacheMemory<T> : IDbCacheMemory<T>, IDbCacheMemoryUpdate
        where T : class, new()
    {
        #region Properties
        public SugarTable TableAttribute { get; }
        public string ConfigId { get; }
        public string CachKey { get; }
        public List<string> PrimaryKeys { get; }
        public List<T> DbData { get; private set; }

        // key: 一对一类型(主键或唯一索引)
        public ConcurrentDictionary<string, Dictionary<string, T>> SingleDict { get; } = new();
        // key: 一对多类型
        public ConcurrentDictionary<string, Dictionary<string, List<T>>> ListDict { get; } = new();
        public DbCacheMemory(params object[] routingDbKeys)
        {
            TableAttribute = typeof(T).GetCustomAttribute<SugarTable>();
            if (TableAttribute == null)
                throw new Exception($"内存缓存类型仅支持有SugarTableAttribute的类。type: {typeof(T).FullName}");
            var routingProvider = DIUtil.GetRequiredService<IDbRoutingProvider>();
            ConfigId = routingProvider.RouteDb<T>(routingDbKeys);
            CachKey = DbCacheUtil.GetCacheKey(ConfigId, TableAttribute.TableName);
            PrimaryKeys = DbUtil.GetDb(ConfigId).DbMaintenance.GetPrimaries(TableAttribute.TableName);
            DbData = GetInitData().GetTaskResult();
        }
        #endregion

        public T GetSingle(object id)
        {
            if (PrimaryKeys?.Count != 1)
                throw new Exception($"多主键不支持DbCacheMemory<T>.GetById。type:{typeof(T).FullName}");
            return GetSingleValue(PrimaryKeys[0], Convert.ToString(id));
        }
        public T GetSingle(Expression<Func<T>> expr)
        {
            var keys = GetKeys(expr);
            return GetSingleValue(keys.DictKey, keys.ValueKey);
        }
        public List<T> GetList(Expression<Func<T>> expr)
        {
            var keys = GetKeys(expr);
            return GetListValue(keys.DictKey, keys.ValueKey);
        }
        public List<T> GetAllList() => DbData;

        #region Utils
        private async Task<List<T>> GetInitData()
        {
            var result = await new DbCacheDataDCache().GetOrLoadAsync(CachKey);
            if (!result.HasValue)
                throw new Exception($"DbCacheMemory没有获取缓存之.type:{this.GetType().FullName}");
            return SerializerUtil.DeserializeJson<List<T>>(result.Value);
        }
        private (string DictKey, string ValueKey) GetKeys(Expression<Func<T>> expr)
        {
            var body = (MemberInitExpression)expr.Body;
            var dictKeys = new List<string>(body.Bindings.Count);
            var valueKeys = new List<string>(body.Bindings.Count);
            foreach (var binding in body.Bindings)
            {
                dictKeys.Add(binding.Member.Name);
                var constant = (ConstantExpression)((MemberAssignment)binding).Expression;
                valueKeys.Add(Convert.ToString(constant.Value));
            }
            return (string.Join('|', dictKeys), string.Join('|', valueKeys));
        }

        private T GetSingleValue(string dictKey, string valueKey)
        {
            WaitForUpdate();
            var dict = SingleDict.GetOrAdd(dictKey, x =>
            {
                var value = new Dictionary<string, T>();
                DbData.ForEach(entry =>
                {
                    var names = dictKey.Split('|');
                    var vvs = names.Select(n => ReflectionUtil.GetPropertyValue(entry, n));
                    var vkey = string.Join('|', vvs);
                    if (value.ContainsKey(vkey))
                        throw new Exception($"IDbCacheMemory获取单个缓存时不唯一。type:{typeof(T).FullName} dictKey:{dictKey} valueKey:{valueKey}");
                    value.Add(vkey, entry);
                });
                return value;
            });
            return dict.TryGetValue(valueKey, out T ret) ? ret : null;
        }
        private List<T> GetListValue(string dictKey, string valueKey)
        {
            WaitForUpdate();
            var dict = ListDict.GetOrAdd(dictKey, x =>
            {
                var value = new Dictionary<string, List<T>>();
                DbData.ForEach(entry =>
                {
                    var names = dictKey.Split('|');
                    var vvs = names.Select(n => ReflectionUtil.GetPropertyValue(entry, n));
                    var vkey = string.Join('|', vvs);
                    if (value.ContainsKey(vkey))
                        value[vkey].Add(entry);
                    else
                        value.Add(vkey, new List<T> { entry });
                });
                return value;
            });
            return dict.TryGetValue(valueKey, out List<T> ret) ? ret : null;
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
        private List<T> _updateList;
        public void BeginUpdate(string data)
        {
            _updateList = SerializerUtil.DeserializeJson<List<T>>(data);
        }
        public void EndUpdate()
        {
            _isUpdating = true;
            try
            {
                DbData = _updateList;
                SingleDict.Clear();
                ListDict.Clear();
            }
            finally
            {
                _isUpdating = false;
            }
        }
        #endregion
    }
}
