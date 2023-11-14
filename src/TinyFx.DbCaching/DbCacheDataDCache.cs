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
            // 分页数量
            var keys = DbCachingUtil.ParseCacheKey(field);
            var dataProvider = new PageDataProvider(keys.ConfigId, keys.TableName);
            if (keys.pageIndex == 0)
            {
                ret.Value = Convert.ToString(await dataProvider.GetPageCount()); //分页数
                ret.HasValue = true;
                return ret;
            }
            ret.Value = await dataProvider.GetPageData(keys.pageIndex);
            ret.HasValue = true;
            return ret;
        }

        public async Task<List<DbCacheItem>> GetAllCacheItem()
        {
            var ret = new List<DbCacheItem>();
            foreach (var field in await GetFieldsAsync())
            {
                var keys = DbCachingUtil.ParseCacheKey(field);
                if (keys.pageIndex == 0) // 此字段保存的是分页数
                {
                    ret.Add(new DbCacheItem()
                    {
                        ConfigId = keys.ConfigId,
                        TableName = keys.TableName,
                    });
                }
            }
            return ret;
        }

        /// <summary>
        /// 数据表是否存在内存缓存对象
        /// </summary>
        /// <param name="configId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task<bool> ContainsCacheItem(string configId, string tableName)
        {
            var key = DbCachingUtil.GetCacheKey(configId, tableName);
            return await ExistsAsync($"{key}|0");
        }
    }
}
