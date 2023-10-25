using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Data.SqlSugar;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.DbCaching
{
    public class DbCacheDataDCache : RedisHashClient<string>
    {
        public DbCacheDataDCache()
        {
            RedisKey = GetGlobalRedisKey();
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
