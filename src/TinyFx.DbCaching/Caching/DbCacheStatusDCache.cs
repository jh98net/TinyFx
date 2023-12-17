using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.DbCaching
{
    internal class DbCacheStatusDCache : RedisHashClient<DbCacheStatusDO>
    {
        private static DbCacheStatusDCache _instance;
        public static DbCacheStatusDCache Create(string connectionStringName = null)
        {
            var useConfig = string.IsNullOrEmpty(connectionStringName);
            if (useConfig && _instance != null) return _instance;
            var ret = new DbCacheStatusDCache(connectionStringName);
            if(useConfig)
                _instance = ret;
            return ret;
        }
        private DbCacheStatusDCache(string connectionStringName = null)
        {
            Options.ConnectionStringName = connectionStringName;
            RedisKey = RedisPrefixConst.DB_CACHING_STATUS;
        }
    }
    internal class DbCacheStatusDO
    {
        public string NacosInstanceId { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }

    }
}
