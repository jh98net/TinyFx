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
        private static DbCacheDataDCache _instance;
        public static DbCacheDataDCache Create(string connectionStringName = null)
        {
            var useConfig = string.IsNullOrEmpty(connectionStringName);
            if (useConfig && _instance != null) return _instance;

            var redisSection = ConfigUtil.GetSection<RedisSection>();
            string connString = useConfig
                ? redisSection.GetConnectionStringElement(ConfigUtil.GetSection<DbCachingSection>()?.RedisConnectionStringName).ConnectionString
                : redisSection.GetConnectionStringElement(connectionStringName).ConnectionString;

            var conn = ConfigurationOptions.Parse(connString);
            conn.ClientName = "DbCacheDataDCache";
            conn.AsyncTimeout = ASYNC_TIMEOUT;
            conn.SyncTimeout = ASYNC_TIMEOUT;
            var ret = new DbCacheDataDCache(conn.ToString());
            if(useConfig)
                _instance = ret;
            return ret;
        }

        private const int ASYNC_TIMEOUT = 20000;
        private DbCacheDataDCache(string connectionString)
        {
            Options.ConnectionString = connectionString;
            RedisKey = RedisPrefixConst.DB_CACHING_DATA;
        }
        protected override async Task<CacheValue<string>> LoadValueWhenRedisNotExistsAsync(string field)
        {
            var ret = new CacheValue<string>();
            // 分页数量
            var keys = ParseCacheKey(field);
            var dataProvider = new PageDataProvider(keys.ConfigId, keys.TableName);
            if (keys.pageIndex == -1)
                throw new Exception($"DbCacheDataDCache的field异常,pageIndex不能为-1: {field}");
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
                var keys = ParseCacheKey(field);
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
        private (string ConfigId, string TableName, int pageIndex) ParseCacheKey(string value)
        {
            var keys = value?.Split('|');
            if (keys == null || keys.Any(k => string.IsNullOrEmpty(k)) || keys.Length < 2)
                throw new Exception($"DbCacheUtil.ParseCacheKey异常. value: {value}");
            var configId = keys[0];
            var table = keys[1];
            var pageIndex = keys.Length == 3 ? Convert.ToInt32(keys[2]) : -1;
            return (configId, table, pageIndex);
        }
    }
}
