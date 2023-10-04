using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Reflection;

namespace TinyFx.Extensions.StackExchangeRedis.Clients
{
    /*
    public class RedisHashEntryClient<T> : RedisClientBase
            where T : class, new()
    {
        public override RedisType RedisType => RedisType.Hash;
        #region SetEntry
        public async Task SetEntry(T entry)
        {
            var values = new Dictionary<string, object>();
            ReflectionUtil.GetPropertyDic<T>().ForEach(x =>
            {
                values.Add(x.Key, ReflectionUtil.GetPropertyValue<object>(entry, x.Key));
            });
        }
        #endregion
    }
    */
}
