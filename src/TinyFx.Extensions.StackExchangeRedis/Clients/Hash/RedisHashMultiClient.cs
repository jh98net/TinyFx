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
    ///     可存入null值，不存在抛出异常CacheNotFound
    /// </summary>
    public class RedisHashMultiClient : RedisHashBase<object>
    {
        public RedisHashMultiClient(RedisClientOptions options = null) : base(options) { }

        #region Set & SetItem
        /// <summary>
        /// 【创建或更新】设置hash结构中的field对应的缓存值
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="always">true:无论是否存在总是添加，false：不存在时才添加</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool Set<T>(string field, T value, bool always = true, CommandFlags flags = CommandFlags.None)
            => Database.HashSet(RedisKey, field, Serialize(value), always ? When.Always : When.NotExists, flags);

        /// <summary>
        /// 【创建或更新】设置hash结构中的field
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="always">true:无论是否存在总是添加，false：不存在时才添加</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string field, T value, bool always = true, CommandFlags flags = CommandFlags.None)
            => await Database.HashSetAsync(RedisKey, field, Serialize(value), always ? When.Always : When.NotExists, flags);

        /// <summary>
        /// 【创建或更新】根据item对象的属性名和值设置hash结构中的field
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="flags"></param>
        public void SetItem<T>(T item, CommandFlags flags = CommandFlags.None)
              where T : new()
        {
            var entries = GetEntries(item);
            Database.HashSet(RedisKey, entries, flags);
        }
        public async Task SetItemAsync<T>(T item, CommandFlags flags = CommandFlags.None)
              where T : new()
        {
            var entries = GetEntries(item);
            await Database.HashSetAsync(RedisKey, entries, flags);
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

        #region TryGet & GetItem
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
            return TryDeserialize(redisValue, out value);
        }

        public T GetItem<T>(CommandFlags commandFlags = CommandFlags.None)
            where T : new()
        {
            var entries = Database.HashGetAll(RedisKey, commandFlags);
            return GetItemByHashEntry<T>(entries);
        }
        public async Task<T> GetItemAsync<T>(CommandFlags commandFlags = CommandFlags.None)
            where T : new()
        {
            var entries = await Database.HashGetAllAsync(RedisKey, commandFlags);
            return GetItemByHashEntry<T>(entries);
        }

        private T GetItemByHashEntry<T>(HashEntry[] entries)
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
        /// <param name="flags"></param>
        /// <returns></returns>
        public CacheValue<T> GetOrLoad<T>(string field, CommandFlags flags = CommandFlags.None)
        {
            CacheValue<T> ret = null;
            var redisValue = Database.HashGet(RedisKey, field, flags);
            if (!TryDeserialize(redisValue, out T value))
            {
                if (LoadValueWhenRedisNotExists(field, out object objRet))
                {
                    ret = new CacheValue<T>((T)objRet);
                    Database.HashSet(RedisKey, field, Serialize(ret), When.Always, flags);
                }
                else
                    ret = new CacheValue<T>(false);
            }
            else
                ret = new CacheValue<T>(value);
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
        /// <param name="flags"></param>
        /// <returns></returns>
        public T GetOrException<T>(string field, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.HashGet(RedisKey, field, flags);
            if (!TryDeserialize(redisValue, out T ret))
                throw new CacheNotFound($"[Redis Hash]field不存在。RedisKey: {RedisKey} field: {field} type:{GetType().FullName}");
            return ret;
        }
        public async Task<T> GetOrExceptionAsync<T>(string field, CommandFlags flags = CommandFlags.None)
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
        public T GetOrDefault<T>(string field, T defaultValue, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.HashGet(RedisKey, field, flags);
            return TryDeserialize(redisValue, out T ret) ? ret : defaultValue;
        }
        public async Task<T> GetOrDefaultAsync<T>(string field, T defaultValue, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = await Database.HashGetAsync(RedisKey, field, flags);
            return TryDeserialize(redisValue, out T ret) ? ret : defaultValue;
        }
        #endregion
    }
}
