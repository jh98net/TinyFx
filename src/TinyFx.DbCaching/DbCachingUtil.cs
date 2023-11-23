using Dm.parser;
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
        // key: typename|splitDbKeys value: configId|tablename
        private static ConcurrentDictionary<string, string> _cachKeyDict = new();
        // key: configId|tableName ===> [eoTypeName, memory]
        internal static ConcurrentDictionary<string, ConcurrentDictionary<string, object>> CacheDict = new();
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
            var cacheKey = _cachKeyDict.GetOrAdd(key, k =>
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

        #region 缓存更新--后台管理
        /// <summary>
        /// 数据表是否存在内存缓存对象
        /// </summary>
        /// <param name="configId"></param>
        /// <param name="tableName"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static async Task<bool> ContainsCacheItem(string configId, string tableName, string connectionStringName = null)
        {
            return await DbCacheDataDCache.Create(connectionStringName).ContainsCacheItem(configId, tableName);
        }
        public static async Task<List<DbCacheItem>> GetAllCacheItem(string connectionStringName = null)
        {
            return await DbCacheDataDCache.Create(connectionStringName).GetAllCacheItem();
        }
        /// <summary>
        /// 发布更新通知
        /// </summary>
        /// <param name="items"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static async Task PublishUpdate(List<DbCacheItem> items, string connectionStringName = null)
        {
            items ??= await GetAllCacheItem(connectionStringName);
            foreach (var item in items)
            {
                var dataProvider = new PageDataProvider(item.ConfigId, item.TableName, connectionStringName);
                await dataProvider.SetRedisValues();
            }
            var msg = new DbCacheChangeMessage
            {
                Changed = items
            };
            await RedisUtil.PublishAsync(msg, connectionStringName);
        }
        #endregion

        internal static string GetCacheKey(string configId, string tableName)
            => $"{configId ?? DbUtil.DefaultConfigId}|{tableName}";
    }
}
