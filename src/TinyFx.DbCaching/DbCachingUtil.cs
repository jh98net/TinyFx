using Dm.parser;
using EasyNetQ;
using SqlSugar;
using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using TinyFx.Collections;
using TinyFx.Configuration;
using TinyFx.Data.SqlSugar;
using TinyFx.DbCaching.Caching;
using TinyFx.Extensions.RabbitMQ;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Hosting;
using TinyFx.Hosting.Services;
using TinyFx.Logging;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TinyFx.DbCaching
{
    /// <summary>
    /// 支持通知更新的内存缓存辅助类
    /// </summary>
    public static class DbCachingUtil
    {
        // key: typename|splitDbKeys value: configId|tablename
        internal static ConcurrentDictionary<string, string> CachKeyDict = new();
        // key: configId|tableName ===> [eoTypeName, memory]
        internal static ConcurrentDictionary<string, ConcurrentDictionary<string, object>> CacheDict = new();

        #region GetSingle
        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <typeparam name="TEntity">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="id">主键值</param>
        /// <param name="splitDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static TEntity GetSingle<TEntity>(object id, params object[] splitDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(splitDbKeys).GetSingle(id);

        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <typeparam name="TEntity">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="fieldsExpr">主键或者唯一索引值的列定义</param>
        /// <param name="valuesEntity">主键或者唯一索引值的值定义</param>
        /// <param name="splitDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static TEntity GetSingle<TEntity>(Expression<Func<TEntity, object>> fieldsExpr, object valuesEntity, params object[] splitDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(splitDbKeys).GetSingle(fieldsExpr, valuesEntity);
        public static TEntity GetSingle<TEntity>(Expression<Func<TEntity>> expr, params object[] splitDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(splitDbKeys).GetSingle(expr);
        public static TEntity GetSingleByKey<TEntity>(string fieldsKey, string valuesKey, params object[] splitDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(splitDbKeys).GetSingleByKey(fieldsKey, valuesKey);
        #endregion

        #region GetList
        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <typeparam name="TEntity">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="fieldsExpr">索引值的列定义</param>
        /// <param name="valuesEntity">索引值的值定义</param>
        /// <param name="splitDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static List<TEntity> GetList<TEntity>(Expression<Func<TEntity, object>> fieldsExpr, object valuesEntity, params object[] splitDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(splitDbKeys).GetList(fieldsExpr, valuesEntity);
        public static List<TEntity> GetList<TEntity>(Expression<Func<TEntity>> expr, params object[] splitDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(splitDbKeys).GetList(expr);
        public static List<TEntity> GetListByKey<TEntity>(string fieldsKey, string valuesKey, params object[] splitDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(splitDbKeys).GetListByKey(fieldsKey, valuesKey);

        /// <summary>
        /// 获取所有缓存项
        /// </summary>
        /// <typeparam name="TEntity">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="splitDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static List<TEntity> GetAllList<TEntity>(params object[] splitDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(splitDbKeys).GetAllList();
        #endregion

        #region GetOrAddCustom
        /// <summary>
        /// 自定义单字典缓存，name唯一
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <param name="splitDbKeys"></param>
        /// <returns></returns>
        public static Dictionary<string, TEntity> GetOrAddCustom<TEntity>(string name, Func<List<TEntity>, Dictionary<string, TEntity>> func, params object[] splitDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(splitDbKeys).GetOrAddCustom(name, func);
        /// <summary>
        /// 自定义列表字典缓存，name唯一
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <param name="splitDbKeys"></param>
        /// <returns></returns>
        public static Dictionary<string, List<TEntity>> GetOrAddCustom<TEntity>(string name, Func<List<TEntity>, Dictionary<string, List<TEntity>>> func, params object[] splitDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(splitDbKeys).GetOrAddCustom(name, func);
        /// <summary>
        /// 自定义对象缓存，name唯一
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <param name="splitDbKeys"></param>
        /// <returns></returns>
        public static TCache GetOrAddCustom<TEntity, TCache>(string name, Func<List<TEntity>, TCache> func, params object[] splitDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(splitDbKeys).GetOrAddCustom(name, func);

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="TEntity">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="splitDbKeys">分库路由数据</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DbCacheMemory<TEntity> GetCache<TEntity>(params object[] splitDbKeys)
          where TEntity : class, new()
        {
            return GetNamedCache<TEntity>(null, splitDbKeys);
        }
        public static DbCacheMemory<TEntity> GetNamedCache<TEntity>(string cacheName, params object[] splitDbKeys)
          where TEntity : class, new()
        {
            var key = splitDbKeys == null || splitDbKeys.Length == 0
                ? typeof(TEntity).FullName
                : $"{typeof(TEntity).FullName}|{string.Join('|', splitDbKeys)}";

            // configId|tableName
            var cacheKey = CachKeyDict.GetOrAdd(key, k =>
            {
                var attr = typeof(TEntity).GetCustomAttribute<SugarTable>();
                if (attr == null)
                    throw new Exception($"内存缓存类型仅支持有SugarTableAttribute的类。type: {typeof(TEntity).FullName}");
                var routingProvider = DIUtil.GetRequiredService<IDbSplitProvider>();
                var configId = routingProvider.SplitDb<TEntity>(splitDbKeys);
                return GetCacheKey(configId, attr.TableName);
            });
            // configId|tableName => dict
            var dict = CacheDict.GetOrAdd(cacheKey, (k) => new ConcurrentDictionary<string, object>());
            // eoTypeName => memory
            cacheName ??= typeof(TEntity).FullName;
            var ret = dict.GetOrAdd(cacheName, (k) => new DbCacheMemory<TEntity>(splitDbKeys));
            return (DbCacheMemory<TEntity>)ret;
        }
        #endregion

        #region 缓存更新--后台管理
        /// <summary>
        /// 数据表是否存在内存缓存对象
        /// </summary>
        /// <param name="configId"></param>
        /// <param name="tableName"></param>
        /// <param name="redisConnectionStringName"></param>
        /// <returns></returns>
        public static async Task<bool> ContainsCacheItem(string configId, string tableName, string redisConnectionStringName = null)
        {
            var listDCache = new DbCacheListDCache(redisConnectionStringName);
            var dataDCache = new DbCacheDataDCache(configId, tableName, redisConnectionStringName);
            var key = GetCacheKey(configId, tableName);
            return await listDCache.FieldExistsAsync(key) && await dataDCache.KeyExistsAsync();
        }
        /// <summary>
        /// 获取所有缓存项
        /// </summary>
        /// <param name="redisConnectionStringName"></param>
        /// <returns></returns>
        public static async Task<List<DbCacheItem>> GetAllCacheItem(string redisConnectionStringName = null)
        {
            var ret = new List<DbCacheItem>();
            var listDCache = new DbCacheListDCache(redisConnectionStringName);
            var fields = await listDCache.GetFieldsAsync();
            foreach (var field in fields)
            {
                var keys = ParseCacheKey(field);
                var dataDCache = new DbCacheDataDCache(keys.configId, keys.tableName, redisConnectionStringName);
                if (!await dataDCache.KeyExistsAsync())
                    continue;
                ret.Add(new DbCacheItem
                {
                    ConfigId = keys.configId,
                    TableName = keys.tableName,
                });
            }
            return ret;
        }

        /// <summary>
        /// 发布更新通知
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task PublishUpdate(DbCacheChangeMessage message)
        {
            foreach (var item in message.Changed)
            {
                if (!await ContainsCacheItem(item.ConfigId, item.TableName, message.RedisConnectionStringName))
                    continue;
                var dataProvider = new PageDataProvider(item.ConfigId, item.TableName, message.RedisConnectionStringName);
                await dataProvider.SetRedisValues();
            }
            switch (message.PublishMode)
            {
                case DbCachingPublishMode.Redis:
                    await RedisUtil.PublishAsync(message, message.RedisConnectionStringName);
                    break;
                case DbCachingPublishMode.MQ:
                    await MQUtil.PublishAsync(message, null, null, message.MQConnectionStringName);
                    break;
                case DbCachingPublishMode.All:
                    await RedisUtil.PublishAsync(message, message.RedisConnectionStringName);
                    await MQUtil.PublishAsync(message, null, null, message.MQConnectionStringName);
                    break;
            }
        }

        internal const string DB_CACHING_CHECK_KEY = "DB_CACHING_CHECK_KEY";
        internal const string DB_CACHING_CHECK_DATA = "DB_CACHING_CHECK_DATA";
        /// <summary>
        /// 发送验证消息
        /// </summary>
        /// <returns></returns>
        public static async Task<List<DbCachingCheckResult>> PublishCheck(string redisConnectionStringName = null, int timeoutSeconds = 20)
        {
            var ret = new List<DbCachingCheckResult>();
            var msg = new DbCacheCheckMessage
            {
                TraceId = StringUtil.GetGuidString(),
                RedisConnectionStringName = redisConnectionStringName,
                CheckDate = DateTime.Now.ToFormatString()
            };
            await RedisUtil.PublishAsync(msg, redisConnectionStringName);
            var dataService = DIUtil.GetService<ITinyFxHostDataService>();
            if (dataService == null)
                throw new Exception("获取所有host的DbCaching缓存检查数据异常，ITinyFxHostDataService不存在");
            var serviceIds = await dataService.GetHosts(redisConnectionStringName);
            var idQueue = new Queue<string>();
            serviceIds.ForEach(x => idQueue.Enqueue(x));
            var currTime = 0;
            while (idQueue.Count > 0)
            {
                var serviceId = idQueue.Dequeue();
                await Task.Delay(200);
                currTime += 200;
                if (currTime > timeoutSeconds * 1000)
                    throw new Exception($"DbCachingUtil.PublishCheck操作超时，serviceId:{serviceId}");
                var traceId = await dataService.GetHostData<string>(serviceId, DB_CACHING_CHECK_KEY, redisConnectionStringName);
                if (!traceId.HasValue || traceId.Value != msg.TraceId)
                {
                    idQueue.Enqueue(serviceId);
                    continue;
                }
                var items = await dataService.GetHostData<List<DbCachingCheckItem>>(serviceId
                    , DB_CACHING_CHECK_DATA, redisConnectionStringName);
                if (!items.HasValue)
                    continue;
                var result = new DbCachingCheckResult
                {
                    ServiceId = serviceId,
                    Items = items.Value
                };
                ret.Add(result);
            }
            return ret;
        }

        #endregion

        #region Utils
        internal static string GetCacheKey(string configId, string tableName)
            => $"{configId ?? DbUtil.DefaultConfigId}|{tableName}";
        internal static (string configId, string tableName) ParseCacheKey(string key)
        {
            var keys = key.Split('|');
            return (keys[0], keys[1]);
        }

        private static ConcurrentDictionary<string, string> _redisConnDict = new();
        private const int REDIS_ASYNC_TIMEOUT = 20000;
        internal static string GetRedisConnectionString(string connectionStringName = null)
        {
            connectionStringName ??= string.Empty;
            if (!_redisConnDict.TryGetValue(connectionStringName, out var ret))
            {
                var redisSection = ConfigUtil.GetSection<RedisSection>();
                ret = string.IsNullOrEmpty(connectionStringName)
                   ? redisSection.GetConnectionStringElement(ConfigUtil.GetSection<DbCachingSection>().RedisConnectionStringName).ConnectionString
                   : redisSection.GetConnectionStringElement(connectionStringName).ConnectionString;
                var conn = ConfigurationOptions.Parse(ret);
                conn.ClientName = "DbCacheDataDCache";
                conn.AsyncTimeout = REDIS_ASYNC_TIMEOUT;
                conn.SyncTimeout = REDIS_ASYNC_TIMEOUT;
                ret = conn.ToString();

                _redisConnDict.TryAdd(connectionStringName, ret);
            }
            return ret;
        }
        #endregion
    }
}
