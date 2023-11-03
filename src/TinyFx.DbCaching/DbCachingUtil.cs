using SqlSugar;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using TinyFx.Data.SqlSugar;
using TinyFx.Extensions.StackExchangeRedis;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TinyFx.DbCaching
{
    /// <summary>
    /// 支持通知更新的内存缓存辅助类
    /// </summary>
    public static class DbCachingUtil
    {
        // key: typename|routingDbKeys value: configId|tablename
        private static ConcurrentDictionary<string, string> _cachKeyDict = new();
        // key: configId|tableName ===> [ eoTypeName, memory]
        internal static ConcurrentDictionary<string, ConcurrentDictionary<string, object>> CacheDict = new();
        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <typeparam name="TEntity">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="id">主键值</param>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static TEntity GetSingle<TEntity>(object id, params object[] routingDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(routingDbKeys).GetSingle(id);

        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <typeparam name="TEntity">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="fieldsExpr">主键或者唯一索引值的列定义</param>
        /// <param name="valuesEntity">主键或者唯一索引值的值定义</param>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static TEntity GetSingle<TEntity>(Expression<Func<TEntity, object>> fieldsExpr, object valuesEntity, params object[] routingDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(routingDbKeys).GetSingle(fieldsExpr, valuesEntity);
        public static TEntity GetSingleByKey<TEntity>(string fieldsKey, string valuesKey, params object[] routingDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(routingDbKeys).GetSingleByKey(fieldsKey, valuesKey);


        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <typeparam name="TEntity">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="fieldsExpr">索引值的列定义</param>
        /// <param name="valuesEntity">索引值的值定义</param>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static List<TEntity> GetList<TEntity>(Expression<Func<TEntity, object>> fieldsExpr, object valuesEntity, params object[] routingDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(routingDbKeys).GetList(fieldsExpr, valuesEntity);
        public static List<TEntity> GetListByKey<TEntity>(string fieldsKey, string valuesKey, params object[] routingDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(routingDbKeys).GetListByKey(fieldsKey, valuesKey);

        /// <summary>
        /// 获取所有缓存项
        /// </summary>
        /// <typeparam name="TEntity">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static List<TEntity> GetAllList<TEntity>(params object[] routingDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(routingDbKeys).GetAllList();

        /// <summary>
        /// 自定义单字典缓存，name唯一
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <param name="routingDbKeys"></param>
        /// <returns></returns>
        public static Dictionary<string, TEntity> GetOrAddCustom<TEntity>(string name, Func<List<TEntity>, Dictionary<string, TEntity>> func, params object[] routingDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(routingDbKeys).GetOrAddCustom(name, func);
        /// <summary>
        /// 自定义列表字典缓存，name唯一
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <param name="routingDbKeys"></param>
        /// <returns></returns>
        public static Dictionary<string, List<TEntity>> GetOrAddCustom<TEntity>(string name, Func<List<TEntity>, Dictionary<string, List<TEntity>>> func, params object[] routingDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(routingDbKeys).GetOrAddCustom(name, func);
        /// <summary>
        /// 自定义对象缓存，name唯一
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <param name="routingDbKeys"></param>
        /// <returns></returns>
        public static TCache GetOrAddCustom<TEntity, TCache>(string name, Func<List<TEntity>, TCache> func, params object[] routingDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(routingDbKeys).GetOrAddCustom(name, func);

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="TEntity">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DbCacheMemory<TEntity> GetCache<TEntity>(params object[] routingDbKeys)
          where TEntity : class, new()
        {
            return GetNamedCache<TEntity>(null, routingDbKeys);
        }
        public static DbCacheMemory<TEntity> GetNamedCache<TEntity>(string cacheName, params object[] routingDbKeys)
          where TEntity : class, new()
        {
            var key = routingDbKeys == null || routingDbKeys.Length == 0
                ? typeof(TEntity).FullName
                : $"{typeof(TEntity).FullName}|{string.Join('|', routingDbKeys)}";

            // configId|tableName
            var cacheKey = _cachKeyDict.GetOrAdd(key, k =>
            {
                var attr = typeof(TEntity).GetCustomAttribute<SugarTable>();
                if (attr == null)
                    throw new Exception($"内存缓存类型仅支持有SugarTableAttribute的类。type: {typeof(TEntity).FullName}");
                var routingProvider = DIUtil.GetRequiredService<IDbRoutingProvider>();
                var configId = routingProvider.RouteDb<TEntity>(routingDbKeys);
                return GetCacheKey(configId, attr.TableName);
            });
            // dict
            var dict = CacheDict.GetOrAdd(cacheKey, (k) => new ConcurrentDictionary<string, object>());
            // eoTypeName => memory
            cacheName ??= typeof(TEntity).FullName;
            var ret = dict.GetOrAdd(cacheName, (k) => new DbCacheMemory<TEntity>(routingDbKeys));
            return (DbCacheMemory<TEntity>)ret;
        }

        #region 缓存更新
        /// <summary>
        /// 数据表是否存在内存缓存对象
        /// </summary>
        /// <param name="configId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static async Task<bool> ContainsCacheItem(string configId, string tableName)
        {
            return await DbCacheDataDCache.Create().ContainsCacheItem(configId, tableName);
        }
        public static async Task<List<DbCacheItem>> GetAllCacheItem()
        {
            return await DbCacheDataDCache.Create().GetAllCacheItem();
        }
        /// <summary>
        /// 发布更新通知
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static async Task PublishUpdate(List<DbCacheChangeItem> items)
        {
            items ??= (await GetAllCacheItem()).Select(x => new DbCacheChangeItem
            {
                ConfigId = x.ConfigId,
                TableName = x.TableName,
            }).ToList();
            foreach (var item in items)
            {
                var list = await DbUtil.GetDb(item.ConfigId).Queryable<object>()
                    .AS(item.TableName).ToListAsync() ?? new List<object>();
                var data = SerializerUtil.SerializeJson(list);
                var key = GetCacheKey(item.ConfigId, item.TableName);
                await DbCacheDataDCache.Create().SetAsync(key, data);
            }
            var msg = new DbCacheChangeMessage
            {
                Changed = items
            };
            await RedisUtil.PublishAsync(msg);
        }
        #endregion

        internal static string GetCacheKey(string configId, string tableName)
            => $"{configId ?? DbUtil.DefaultConfigId}|{tableName}";

        internal static (string ConfigId, string TableName) ParseCacheKey(string value)
        {
            var keys = value?.Split('|');
            if (keys?.Any(x => string.IsNullOrEmpty(x)) ?? true || keys.Length != 2)
                throw new Exception($"DbCacheUtil.ParseCacheKey异常. value: {value}");
            var configId = keys[0];
            var table = keys[1];
            return (configId, table);
        }
    }
}
