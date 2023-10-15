using Microsoft.AspNetCore.DataProtection.KeyManagement;
using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Data.SqlSugarEx;
using TinyFx.Extensions.StackExchangeRedis;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TinyFx.DbCaching
{
    /// <summary>
    /// 支持通知更新的内存缓存辅助类
    /// </summary>
    public static class DbCacheUtil
    {
        // key: typename|routingDbKeys value: cacheKey
        private static ConcurrentDictionary<string, string> _cachKeyDict = new();
        internal static ConcurrentDictionary<string, object> CacheDict = new();

        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <typeparam name="T">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="id">主键值</param>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static T GetSingle<T>(object id, params object[] routingDbKeys)
          where T : class, new()
            => GetCache<T>(routingDbKeys).GetSingle(id);

        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <typeparam name="T">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="whereExpr">主键或者唯一索引值的表达式</param>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static T GetSingle<T>(Expression<Func<T>> whereExpr, params object[] routingDbKeys)
          where T : class, new()
            => GetCache<T>(routingDbKeys).GetSingle(whereExpr);

        /// <summary>
        /// 获取一组缓存项
        /// </summary>
        /// <typeparam name="T">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="whereExpr">过滤条件值的表达式</param>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static List<T> GetList<T>(Expression<Func<T>> whereExpr, params object[] routingDbKeys)
          where T : class, new()
            => GetCache<T>(routingDbKeys).GetList(whereExpr);

        /// <summary>
        /// 获取所有缓存项
        /// </summary>
        /// <typeparam name="T">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        public static List<T> GetAllList<T>(params object[] routingDbKeys)
          where T : class, new()
            => GetCache<T>(routingDbKeys).GetAllList();

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">有SugarTableAttribute的数据库实体类</typeparam>
        /// <param name="routingDbKeys">分库路由数据</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IDbCacheMemory<T> GetCache<T>(params object[] routingDbKeys)
          where T : class, new()
        {
            var key = routingDbKeys.Length == 0
                ? typeof(T).FullName
                : $"{typeof(T).FullName}|{string.Join('|', routingDbKeys)}";
            var cacheKey = _cachKeyDict.GetOrAdd(key, x =>
            {
                var attr = typeof(T).GetCustomAttribute<SugarTable>();
                if (attr == null)
                    throw new Exception($"内存缓存类型仅支持有SugarTableAttribute的类。type: {typeof(T).FullName}");
                var routingProvider = DIUtil.GetRequiredService<IDbRoutingProvider>();
                var configId = routingProvider.RouteDb<T>(routingDbKeys);
                return GetCacheKey(configId, attr.TableName);
            });
            var ret = CacheDict.GetOrAdd(cacheKey, new DbCacheMemory<T>(routingDbKeys));
            return (IDbCacheMemory<T>)ret;
        }

        /// <summary>
        /// 发布更新通知
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static async Task PublishUpdate<T>(List<DbCacheChangeItem> items)
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
