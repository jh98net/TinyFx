using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;

namespace TinyFx.Extensions.StackExchangeRedis
{
    /// <summary>
    /// Redis Hash表（key-value结构）value值的类型必须是T
    ///     可以被继承，也可以直接构建
    ///     RedisKey => Field => RedisValue
    ///     可存入null值，不存在抛出异常CacheNotFound
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RedisHashClient<T> : RedisHashBase<T>
    {
        public RedisHashClient(RedisClientOptions options = null) : base(options) { }

        #region Set
        /// <summary>
        /// 【创建或更新】设置hash结构中的field对应的缓存值
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="always">true:无论是否存在总是添加，false：不存在时才添加</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool Set(string field, T value, bool always = true, CommandFlags flags = CommandFlags.None)
            => Database.HashSet(RedisKey, field, Serialize(value), always ? When.Always : When.NotExists, flags);

        /// <summary>
        /// 【创建或更新】设置hash结构中的field
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="always">true:无论是否存在总是添加，false：不存在时才添加</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string field, T value, bool always = true, CommandFlags flags = CommandFlags.None)
            => await Database.HashSetAsync(RedisKey, field, Serialize(value), always ? When.Always : When.NotExists, flags);
        #endregion

        #region TryGet
        /// <summary>
        /// 获取此Hash中field对应的缓存值
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns>true: 缓存项存在</returns>
        public bool TryGet(string field, out T value, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.HashGet(RedisKey, field, flags);
            return TryDeserialize(redisValue, out value);
        }
        #endregion

        #region GetOrLoad
        /// <summary>
        /// 从Hash结构根据field获取缓存项，如果不存在则调用LoadValueWhenRedisNotExists()放入redis并返回
        /// </summary>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public CacheValue<T> GetOrLoad(string field, CommandFlags flags = CommandFlags.None)
        {
            CacheValue<T> ret = null;
            var redisValue = Database.HashGet(RedisKey, field, flags);
            if (!TryDeserialize(redisValue, out T value))
            {
                if (LoadValueWhenRedisNotExists(field, out T objRet))
                {
                    ret = new CacheValue<T>(objRet);
                    Database.HashSet(RedisKey, field, Serialize(ret), When.Always, flags);
                }
                else
                    ret = new CacheValue<T>(false);
            }
            else
                ret = new CacheValue<T>(value);
            return ret;
        }
        public async Task<CacheValue<T>> GetOrLoadAsync(string field, CommandFlags flags = CommandFlags.None)
            => await Task.Factory.StartNew(() => GetOrLoad(field, flags));

        #endregion

        #region GetOrException
        /// <summary>
        /// 从Hash结构根据field获取缓存项，如果不存在则抛出异常CacheNotFound
        /// </summary>
        /// <param name="field"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public T GetOrException(string field, CommandFlags commandFlags = CommandFlags.None)
        {
            var redisValue = Database.HashGet(RedisKey, field, commandFlags);
            if(!TryDeserialize(redisValue, out T ret))
                throw new CacheNotFound($"[Redis Hash]field不存在。RedisKey: {RedisKey} field: {field} type:{GetType().FullName}");
            return ret;
        }
        public async Task<T> GetOrExceptionAsync(string field, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = await Database.HashGetAsync(RedisKey, field, flags);
            if (!TryDeserialize(redisValue, out T ret))
                throw new CacheNotFound($"[Redis Hash]field不存在。RedisKey: {RedisKey} field: {field} type:{GetType().FullName}");
            return ret;
        }
        #endregion

        #region GetOrDefault
        /// <summary>
        /// 从Hash结构根据field获取缓存项，如果不存在，则返回默认值。
        /// </summary>
        /// <param name="field"></param>
        /// <param name="defaultValue"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T GetOrDefault(string field, T defaultValue, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.HashGet(RedisKey, field, flags);
            return TryDeserialize(redisValue, out T ret) ? ret : defaultValue;
        }
        public async Task<T> GetOrDefaultAsync(string field, T defaultValue, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = await Database.HashGetAsync(RedisKey, field, flags);
            return TryDeserialize(redisValue, out T ret) ? ret : defaultValue;
        }
        #endregion
    }
}
