using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;

namespace TinyFx.Extensions.StackExchangeRedis
{
    public class RedisHashExpireMutilClient: RedisHashExpireBase<object>
    {
        public RedisHashExpireMutilClient(RedisClientOptions options = null) : base(options) { }

        #region Set
        public bool Set<T>(string field, T value, bool always = true, CommandFlags flags = CommandFlags.None)
           => Database.HashSet(RedisKey, field, SerializeExpire(value), always ? When.Always : When.NotExists, flags);
        /// <summary>
        /// 【创建或更新】设置hash结构中的field对应的缓存值
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="expireAt">过期时间</param>
        /// <param name="always">true:无论是否存在总是添加，false：不存在时才添加</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool Set<T>(string field, T value, DateTime expireAt, bool always = true, CommandFlags flags = CommandFlags.None)
            => Database.HashSet(RedisKey, field, SerializeExpire(value, expireAt), always ? When.Always : When.NotExists, flags);
        public bool Set<T>(string field, T value, TimeSpan expire, bool always = true, CommandFlags flags = CommandFlags.None)
            => Database.HashSet(RedisKey, field, SerializeExpire(value, expire), always ? When.Always : When.NotExists, flags);

        /// <summary>
        /// 【创建或更新】设置hash结构中的field
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="expireAt">过期时间</param>
        /// <param name="always">true:无论是否存在总是添加，false：不存在时才添加</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string field, T value, DateTime expireAt, bool always = true, CommandFlags flags = CommandFlags.None)
            => await Database.HashSetAsync(RedisKey, field, SerializeExpire(value, expireAt), always ? When.Always : When.NotExists, flags);
        public async Task<bool> SetAsync<T>(string field, T value, TimeSpan expire, bool always = true, CommandFlags flags = CommandFlags.None)
            => await Database.HashSetAsync(RedisKey, field, SerializeExpire(value, expire), always ? When.Always : When.NotExists, flags);

        /// <summary>
        /// 设置值，并设置key在指定秒数后过期
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool SetAndExpireSeconds<T>(string field, T value, int seconds, CommandFlags flags = CommandFlags.None)
            => Set(field, value, new TimeSpan(0, 0, seconds), true, flags);
        /// <summary>
        /// 设置值，并设置key在指定分钟数后过期
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="minutes"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool SetAndExpireMinutes<T>(string field, T value, int minutes, CommandFlags flags = CommandFlags.None)
            => Set(field, value, new TimeSpan(0, minutes, 0), true, flags);
        /// <summary>
        /// 设置值，并设置key在指定小时数后过期
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="hours"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool SetAndExpireHours<T>(string field, T value, int hours, CommandFlags flags = CommandFlags.None)
            => Set(field, value, new TimeSpan(hours, 0, 0), true, flags);
        /// <summary>
        /// 设置值，并设置key在指定天数后过期
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="days"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool SetAndExpireDays<T>(string field, T value, int days, CommandFlags flags = CommandFlags.None)
            => Set(field, value, new TimeSpan(days, 0, 0, 0), true, flags);

        #endregion

        #region TryGet
        /// <summary>
        /// 获取此Hash中field对应的缓存值
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns>true: 缓存项存在</returns>
        public bool TryGet<T>(string field, out T value, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.HashGet(RedisKey, field, flags);
            var ret = TryDeserializeExpire(field, redisValue, out object objValue);
            value = (T)objValue;
            return ret;
        }
        #endregion

        #region GetOrLoad
        /// <summary>
        /// 从Hash结构根据field获取缓存项，如果不存在则调用LoadValueWhenRedisNotExists()放入redis并返回
        /// </summary>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public CacheValue<T> GetOrLoad<T>(string field, CommandFlags flags = CommandFlags.None)
        {
            CacheValue<T> ret = null;
            var redisValue = Database.HashGet(RedisKey, field, flags);
            if (!TryDeserializeExpire(redisValue, out CacheItem<object> value))
            {
                if (LoadValueWhenRedisNotExists(field, out CacheItem<object> objRet))
                {
                    ret = objRet.IsExpired ? new CacheValue<T>(false) : new CacheValue<T>((T)objRet.Value);
                    Database.HashSet(RedisKey, field, SerializeExpire(objRet), When.Always, flags);
                }
                else
                    throw new CacheNotFound($"GetOrLoad时LoadValueWhenRedisNotExists没有返回值。key:{RedisKey} field:{field}");
            }
            else
                ret = new CacheValue<T>((T)value.Value);
            return ret;
        }
        public async Task<CacheValue<T>> GetOrLoadAsync<T>(string field, CommandFlags flags = CommandFlags.None)
            => await Task.Factory.StartNew(() => GetOrLoad<T>(field, flags));

        #endregion

        #region GetOrException
        /// <summary>
        /// 从Hash结构根据field获取缓存项，如果不存在则抛出异常CacheNotFound
        /// </summary>
        /// <param name="field"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public T GetOrException<T>(string field, CommandFlags commandFlags = CommandFlags.None)
        {
            var redisValue = Database.HashGet(RedisKey, field, commandFlags);
            if (!TryDeserializeExpire(field, redisValue, out object ret))
                throw new CacheNotFound($"[Redis Hash]field不存在。RedisKey: {RedisKey} field: {field} type:{GetType().FullName}");
            return (T)ret;
        }
        public async Task<T> GetOrExceptionAsync<T>(string field, CommandFlags flags = CommandFlags.None)
            => await Task.Factory.StartNew(() => GetOrException<T>(field, flags));
        #endregion

        #region GetOrDefault
        /// <summary>
        /// 从Hash结构根据field获取缓存项，如果不存在，则返回默认值。
        /// </summary>
        /// <param name="field"></param>
        /// <param name="defaultValue"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public T GetOrDefault<T>(string field, T defaultValue, CommandFlags commandFlags = CommandFlags.None)
        {
            var redisValue = Database.HashGet(RedisKey, field, commandFlags);
            return TryDeserializeExpire(field, redisValue, out object ret) ? (T)ret : defaultValue;
        }
        public async Task<T> GetOrDefaultAsync<T>(string field, T defaultValue, CommandFlags flags = CommandFlags.None)
            => await Task.Factory.StartNew(() => GetOrDefault(field, defaultValue, flags));
        #endregion
    }
}
