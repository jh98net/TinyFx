using SqlSugar;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using TinyFx.Data.SqlSugar;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.DbCaching
{
    /// <summary>
    /// 支持通知更新的内存缓存辅助类
    /// </summary>
    public static class DbCachingUtil
    {
        // key: typename|routingDbKeys value: cacheKey
        private static ConcurrentDictionary<string, string> _cachKeyDict = new();
        internal static ConcurrentDictionary<string, object> CacheDict = new();

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
        /// <param name="whereExpr">主键或者唯一索引值的表达式</param>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static TEntity GetSingle<TEntity>(Expression<Func<TEntity>> whereExpr, params object[] routingDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(routingDbKeys).GetSingle(whereExpr);

        /// <summary>
        /// 获取一组缓存项
        /// </summary>
        /// <typeparam name="TEntity">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="whereExpr">过滤条件值的表达式</param>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static List<TEntity> GetList<TEntity>(Expression<Func<TEntity>> whereExpr, params object[] routingDbKeys)
          where TEntity : class, new()
            => GetCache<TEntity>(routingDbKeys).GetList(whereExpr);

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
        public static IDbCacheMemory<TEntity> GetCache<TEntity>(params object[] routingDbKeys)
          where TEntity : class, new()
        {
            var key = routingDbKeys.Length == 0
                ? typeof(TEntity).FullName
                : $"{typeof(TEntity).FullName}|{string.Join('|', routingDbKeys)}";
            var cacheKey = _cachKeyDict.GetOrAdd(key, x =>
            {
                var attr = typeof(TEntity).GetCustomAttribute<SugarTable>();
                if (attr == null)
                    throw new Exception($"内存缓存类型仅支持有SugarTableAttribute的类。type: {typeof(TEntity).FullName}");
                var routingProvider = DIUtil.GetRequiredService<IDbRoutingProvider>();
                var configId = routingProvider.RouteDb<TEntity>(routingDbKeys);
                return GetCacheKey(configId, attr.TableName);
            });
            var ret = CacheDict.GetOrAdd(cacheKey, new DbCacheMemory<TEntity>(routingDbKeys));
            return (IDbCacheMemory<TEntity>)ret;
        }

        /// <summary>
        /// 发布更新通知
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static async Task PublishUpdate<TEntity>(List<DbCacheChangeItem> items)
        {
            var dataDCache = new DbCacheDataDCache();
            foreach (var item in items)
            {
                var list = await DbUtil.GetDb(item.ConfigId).Queryable<object>()
                    .AS(item.TableName).ToListAsync() ?? new List<object>();
                var data = SerializerUtil.SerializeJson(list);
                var key = GetCacheKey(item.ConfigId, item.TableName);
                await dataDCache.SetAsync(key, data);
            }
            var msg = new DbCacheChangeMessage
            {
                Changed = items
            };
            await RedisUtil.PublishAsync(msg);
        }

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
