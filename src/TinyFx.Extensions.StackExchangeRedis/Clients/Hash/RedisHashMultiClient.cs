using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Reflection;

namespace TinyFx.Extensions.StackExchangeRedis
{
    /// <summary>
    /// Redis Hash表（key-value结构） value值的类型可以不相同
    ///     可以被继承，也可以直接构建
    ///     RedisKey => Field => RedisValue
    ///     可存入null值
    /// </summary>
    public class RedisHashMultiClient : RedisHashBase<object>
    {
        public RedisHashMultiClient(object key = null, RedisClientOptions options = null) : base(key, options) { }

        #region Set & SetEntity
        /// <summary>
        /// 【创建或更新】设置hash结构中的field对应的缓存值
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="always">true:无论是否存在总是添加，false：不存在时才添加</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string field, T value, bool always = true, CommandFlags flags = CommandFlags.None)
        {
            var ret = await Database.HashSetAsync(RedisKey, field, Serialize(value), always ? When.Always : When.NotExists, flags);
            await SetSlidingExpirationAsync();
            return ret;
        }

        /// <summary>
        /// 【创建或更新】根据item对象的属性名和值设置hash结构中的field
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="flags"></param>
        public async Task SetEntityAsync<T>(T item, CommandFlags flags = CommandFlags.None)
              where T : new()
        {
            var entries = GetEntries(item);
            await Database.HashSetAsync(RedisKey, entries, flags);
            await SetSlidingExpirationAsync();
        }

        private HashEntry[] GetEntries<T>(T item)
              where T : new()
        {
            var entries = new List<HashEntry>();
            var props = typeof(T).GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                if (prop.IsDefined(typeof(JsonIgnoreAttribute), true))
                    continue;
                var propValue = ReflectionUtil.GetPropertyValue<T>(item, prop.Name);
                var redisValue = Serialize(propValue);
                entries.Add(new HashEntry(prop.Name, redisValue));
            }
            return entries.ToArray();
        }
        #endregion

        #region Get & GetEntity
        public async Task<CacheValue<T>> GetAsync<T>(string field, CommandFlags flags = CommandFlags.None)
        {
            var value = await Database.HashGetAsync(RedisKey, field, flags);
            var ret = TryDeserialize(value, out T v)
                ? new CacheValue<T>(true, v)
                : new CacheValue<T>(false);
            await SetSlidingExpirationAsync();
            return ret;
        }

        public async Task<T> GetEntityAsync<T>(CommandFlags flags = CommandFlags.None)
            where T : new()
        {
            var entries = await Database.HashGetAllAsync(RedisKey, flags);
            var ret = GetEntityByHashEntry<T>(entries);
            await SetSlidingExpirationAsync();
            return ret;
        }

        private T GetEntityByHashEntry<T>(HashEntry[] entries)
            where T : new()
        {
            T ret = new T();
            var props = ReflectionUtil.GetPropertyDic<T>();
            foreach (var entry in entries)
            {
                if (!props.ContainsKey(entry.Name))
                    continue;
                if (!TryDeserialize(entry.Value, props[entry.Name].PropertyType, out object propValue))
                    continue;
                ReflectionUtil.SetPropertyValue(ret, entry.Name, propValue);
            }
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
        public async Task<CacheValue<T>> GetOrLoadAsync<T>(string field, bool enforce = false, CommandFlags flags = CommandFlags.None)
        {
            CacheValue<T> ret;
            if (enforce || !TryDeserialize(await Database.HashGetAsync(RedisKey, field, flags), out T value))
            {
                var loadValue = await LoadValueWhenRedisNotExistsAsync(field);
                if (loadValue.HasValue)
                {
                    await Database.HashSetAsync(RedisKey, field, Serialize(loadValue.Value), When.Always, flags);
                    ret = new CacheValue<T>((T)loadValue.Value);
                }
                else
                {
                    if (IsLoadValueNotExistsToRedis)
                        await Database.HashSetAsync(RedisKey, field, Serialize(null), When.Always, flags);
                    ret = new CacheValue<T>(false);
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
        public async Task<T> GetOrExceptionAsync<T>(string field, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = await Database.HashGetAsync(RedisKey, field, flags);
            if (!TryDeserialize(redisValue, out T ret))
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
        public async Task<T> GetOrDefaultAsync<T>(string field, T defaultValue, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = await Database.HashGetAsync(RedisKey, field, flags);
            if (!TryDeserialize(redisValue, out T ret))
                ret = defaultValue;
            await SetSlidingExpirationAsync();
            return ret;
        }
        #endregion
    }
}
