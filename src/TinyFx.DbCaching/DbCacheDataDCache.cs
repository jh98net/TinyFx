using SqlSugar;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Configuration;
using TinyFx.Data.SqlSugar;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.DbCaching
{
    internal class DbCacheDataDCache : RedisHashClient<string>
    {
        private static readonly DbCacheDataDCache _instance = new DbCacheDataDCache();
        public static DbCacheDataDCache Create() => _instance;

        private const int ASYNC_TIMEOUT = 60000;
        private DbCacheDataDCache()
        {
            var section = ConfigUtil.GetSection<RedisSection>();
            var element = section.GetConnectionStringElement();
            var conn = ConfigurationOptions.Parse(element.ConnectionString);
            conn.ClientName = "DbCacheDataDCache";
            conn.AsyncTimeout = ASYNC_TIMEOUT;
            conn.SyncTimeout = ASYNC_TIMEOUT;
            Options.ConnectionString = conn.ToString();
            RedisKey = RedisPrefixConst.DB_CACHING_DATA;
        }
        protected override async Task<CacheValue<string>> LoadValueWhenRedisNotExistsAsync(string field)
        {
            var ret = new CacheValue<string>();
            var keys = DbCachingUtil.ParseCacheKey(field);
            var list = await DbUtil.GetDb(keys.ConfigId).Queryable<object>()
                .AS(keys.TableName).ToListAsync() ?? new List<object>();
            ret.Value = SerializerUtil.SerializeJson(list);
            ret.HasValue = true;
            return ret;
        }

        public async Task<List<DbCacheItem>> GetAllCacheItem()
        {
            return (await GetFieldsAsync()).Select(x =>
            {
                var keys = DbCachingUtil.ParseCacheKey(x);
                return new DbCacheItem
                {
                    ConfigId = keys.ConfigId,
                    TableName = keys.TableName,
                };
            }).ToList();
        }

        /// <summary>
        /// 数据表是否存在内存缓存对象
        /// </summary>
        /// <param name="configId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task<bool> ContainsCacheItem(string configId, string tableName)
        {
            return await ExistsAsync(DbCachingUtil.GetCacheKey(configId, tableName));
        }
    }
}
