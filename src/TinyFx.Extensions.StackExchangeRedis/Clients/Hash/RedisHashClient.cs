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
    ///     可存入null值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RedisHashClient<T> : RedisHashBase<T>
    {
        public RedisHashClient(object key = null, RedisClientOptions options = null) : base(key, options) { }

        #region Set
        /// <summary>
        /// 【创建或更新】设置hash结构中的field对应的缓存值
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="always">true:无论是否存在总是添加，false：不存在时才添加</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string field, T value, bool always = true, CommandFlags flags = CommandFlags.None)
        {
            var ret = await Database.HashSetAsync(RedisKey, field, Serialize(value), always ? When.Always : When.NotExists, flags);
            await SetSlidingExpirationAsync();
            return ret;
        }
        #endregion

        #region GetOrLoad
        /// <summary>
        /// 从Hash结构根据field获取缓存项，如果不存在则调用LoadValueWhenRedisNotExists()放入redis并返回
        /// </summary>
        /// <param name="field"></param>
        /// <param name="enforce">是否强制Load</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<CacheValue<T>> GetOrLoadAsync(string field, bool enforce = false, CommandFlags flags = CommandFlags.None)
        {
            CacheValue<T> ret;
            if (enforce || !TryDeserialize(await Database.HashGetAsync(RedisKey, field, flags), out T value))
            {
                ret = await LoadValueWhenRedisNotExistsAsync(field);
                if (ret.HasValue)
                {
                    await Database.HashSetAsync(RedisKey, field, Serialize(ret.Value), When.Always, flags);
                }
                else
                {
                    if (IsLoadValueNotExistsToRedis)
                    {
                        await Database.HashSetAsync(RedisKey, field, Serialize(null), When.Always, flags);
                    }
                }
            }
            else
            {
                ret = new CacheValue<T>(value);
            }
            await SetSlidingExpirationAsync();
            return ret;
        }
        #endregion

        #region GetOrException
        /// <summary>
        /// 从Hash结构根据field获取缓存项，如果不存在则抛出异常CacheNotFound
        /// </summary>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<T> GetOrExceptionAsync(string field, CommandFlags flags = CommandFlags.None)
        {
            return await GetOrExceptionAsync<T>(field, flags);
        }

        public async Task<TValue> GetOrExceptionAsync<TValue>(string field, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = await Database.HashGetAsync(RedisKey, field, flags);
            if (!TryDeserialize(redisValue, out TValue ret))
                throw new CacheNotFound($"[Redis Hash]field不存在。RedisKey: {RedisKey} field: {field} type:{GetType().FullName}");
            await SetSlidingExpirationAsync();
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
        public async Task<TValue> GetOrDefaultAsync<TValue>(string field, TValue defaultValue, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = await Database.HashGetAsync(RedisKey, field, flags);
            var ret = TryDeserialize(redisValue, out TValue value) ? value : defaultValue;
            await SetSlidingExpirationAsync();
            return ret;
        }
        public async Task<T> GetOrDefaultAsync(string field, T defaultValue, CommandFlags flags = CommandFlags.None)
        {
            return await GetOrDefaultAsync<T>(field, defaultValue, flags);
        }
        #endregion
    }
}
