using Google.Protobuf.WellKnownTypes;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;

namespace TinyFx.Extensions.StackExchangeRedis
{
    public class RedisHashExpireBase<TField> : RedisClientBase
    {
        public override RedisType RedisType => RedisType.Hash;
        /// <summary>
        /// 保存field时，如果没有设置过期时间时使用的默认过期时间
        /// </summary>
        public RedisHashExpireBase(object key = null, RedisClientOptions options = null) : base(key, options) { }

        #region LoadValueWhenRedisNotExists
        public delegate Task<CacheValue<CacheItem<TField>>> LoadValueDelegate(string field);
        public LoadValueDelegate LoadValueHandler;
        /// <summary>
        /// 在调用GetOrLoad时，当Redis不存在此field时，将调用此方法获取field对应的缓存值，返回并存储到Redis中，需要时子类实现override。
        ///     1) 返回true表示保存缓存项，false表示不保存缓存项
        ///     2) value可返回null，存入Redis的值为RedisValue.EmptyString
        ///     3) 可抛出异常CacheNotFound：表示field对应的值不存在
        /// </summary>
        /// <param name="field">key</param>
        /// <returns></returns>
        protected virtual async Task<CacheValue<CacheItem<TField>>> LoadValueWhenRedisNotExistsAsync(string field)
        {
            if (LoadValueHandler != null)
                return await LoadValueHandler(field);
            throw new NotImplementedException();
        }

        public delegate Task<CacheValue<Dictionary<string, CacheItem<TField>>>> LoadAllValuesDelegate();
        public LoadAllValuesDelegate LoadAllValuesHandler;
        /// <summary>
        /// 调用GetAllOrLoad()时，当RedisKey不存在，则调用此方法返回并存储全部hash值到redis中，需要时子类实现override
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<CacheValue<Dictionary<string, CacheItem<TField>>>> LoadAllValuesWhenRedisNotExistsAsync()
        {
            if (LoadAllValuesHandler != null)
                return await LoadAllValuesHandler();
            throw new NotImplementedException();
        }
        #endregion

        #region Set
        /// <summary>
        /// 【创建或更新】设置hash结构中的field
        /// </summary>
        /// <param name="values"></param>
        /// <param name="flags"></param>
        public async Task SetAsync(Dictionary<string, CacheItem<TField>> values, CommandFlags flags = CommandFlags.None)
        {
            var entries = values.Select(kv => new HashEntry(kv.Key, SerializeExpire(kv.Value)));
            await Database.HashSetAsync(RedisKey, entries.ToArray(), flags);
            await SetSlidingExpirationAsync();
        }

        #endregion

        #region Get & GetAll
        public async Task<CacheValue<TField>> GetAsync(string field, CommandFlags flags = CommandFlags.None)
        {
            var value = await Database.HashGetAsync(RedisKey, field, flags);
            var ret = GetCacheValue(field, value);
            await SetSlidingExpirationAsync();
            return ret;
        }

        /// <summary>
        /// 获取一组field对应的缓存值
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, CacheValue<TField>>> GetAsync(IEnumerable<string> fields, CommandFlags flags = CommandFlags.None)
        {
            var arrFields = fields.Select(key => (RedisValue)key).ToArray();
            var values = await Database.HashGetAsync(RedisKey, arrFields, flags);
            var ret = GetCacheValues(arrFields, values);
            await SetSlidingExpirationAsync();
            return ret;
        }
        private Dictionary<string, CacheValue<TField>> GetCacheValues(RedisValue[] fields, RedisValue[] values)
        {
            var ret = new Dictionary<string, CacheValue<TField>>();
            for (int i = 0; i < fields.Length; i++)
            {
                var key = fields[i];
                var value = values[i];
                if (TryDeserializeExpire(key, value, out TField v))
                    ret.Add(key, new CacheValue<TField>(true, v));
                else
                    ret.Add(key, new CacheValue<TField>(false));
            }
            return ret;
        }
        private CacheValue<TField> GetCacheValue(string field, RedisValue value)
        {
            return TryDeserializeExpire(field, value, out TField v)
                ? new CacheValue<TField>(true, v)
                : new CacheValue<TField>(false);
        }

        /// <summary>
        /// 从Hash结构Get所有缓存项
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, TField>> GetAllAsync(CommandFlags flags = CommandFlags.None)
        {
            var ret = new Dictionary<string, TField>();
            var entries = await Database.HashGetAllAsync(RedisKey, flags);
            foreach (var entry in entries)
            {
                if (TryDeserializeExpire(entry.Name, entry.Value, out TField v))
                    ret.Add(entry.Name, v);
            }
            await SetSlidingExpirationAsync();
            return ret;
        }

        /// <summary>
        /// 获取所有hash缓存值，如果RedisKey不存在，则调用LoadAllValuesWhenRedisNotExists()返回并保存到redis中
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<CacheValue<Dictionary<string, TField>>> GetAllOrLoadAsync(CommandFlags flags = CommandFlags.None)
        {
            CacheValue<Dictionary<string, TField>> ret = null;
            if (!await KeyExistsAsync(flags))
            {
                var loadValues = await LoadAllValuesWhenRedisNotExistsAsync();
                if (loadValues.HasValue)
                {
                    await SetAsync(loadValues.Value, flags);
                    ret = new CacheValue<Dictionary<string, TField>>(true, new Dictionary<string, TField>());
                    foreach (var item in loadValues.Value)
                    {
                        if(!item.Value.IsExpired)
                            ret.Value.Add(item.Key, item.Value.Value);
                    }
                    await SetSlidingExpirationAsync();
                }
                else
                    ret = new CacheValue<Dictionary<string, TField>>(false);
            }
            else
            {
                ret = new CacheValue<Dictionary<string, TField>>(await GetAllAsync(flags));
            }
            return ret;
        }
        #endregion

        #region Delete
        /// <summary>
        /// 从Hash结构移除kefield。时间复杂度：O(1)
        /// </summary>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string field, CommandFlags flags = CommandFlags.None)
        {
            var ret = await Database.HashDeleteAsync(RedisKey, field, flags);
            await SetSlidingExpirationAsync();
            return ret;
        }

        /// <summary>
        /// 从Hash结构移除fields。时间复杂度：O(1)
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(IEnumerable<string> fields, CommandFlags flags = CommandFlags.None)
        {
            var ret = await Database.HashDeleteAsync(RedisKey, fields.Select(x => (RedisValue)x).ToArray(), flags);
            await SetSlidingExpirationAsync();
            return ret;
        }
        #endregion

        #region Exists/GetFields/GetValues/GetLength/Scan
        /// <summary>
        /// Hash是否存在指定field
        /// </summary>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string field, CommandFlags flags = CommandFlags.None)
        {
            var ret = await Database.HashExistsAsync(RedisKey, field, flags);
            if (ret)
                await SetSlidingExpirationAsync();
            return ret;
        }

        /// <summary>
        /// 获取hash中所有的fields
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetFieldsAsync(CommandFlags flags = CommandFlags.None)
        {
            var ret = (await Database.HashKeysAsync(RedisKey, flags)).Select(x => x.ToString());
            await SetSlidingExpirationAsync();
            return ret;
        }

        /// <summary>
        /// 返回hash中所有的values
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<List<TField>> GetValuesAsync(CommandFlags flags = CommandFlags.None)
        {
            var ret = new List<TField>();
            var values = await Database.HashValuesAsync(RedisKey, flags);
            foreach (var value in values)
            {
                if (TryDeserializeExpire(value, out CacheItem<TField> v))
                {
                    if (!v.IsExpired)
                        ret.Add(v.Value);
                }
            }
            await SetSlidingExpirationAsync();
            return ret;
        }

        /// <summary>
        /// 获取hash内缓存项数量
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<long> GetLengthAsync(CommandFlags flags = CommandFlags.None)
        {
            var ret = await Database.HashLengthAsync(RedisKey, flags);
            await SetSlidingExpirationAsync();
            return ret;
        }

        /// <summary>
        /// HSCAN游标查询命令
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <param name="cursor"></param>
        /// <param name="pageOffset"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, TField>> ScanAsync(RedisValue pattern, int pageSize, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            var ret = new Dictionary<string, TField>();
            var entries = Database.HashScanAsync(RedisKey, pattern, pageSize, cursor, pageOffset, flags);
            await foreach (var entry in entries)
            {
                if (TryDeserializeExpire(entry.Name, entry.Value, out TField v))
                    ret.Add(entry.Name, v);
            }
            await SetSlidingExpirationAsync();
            return ret;
        }
        #endregion

        #region FieldExpire
        public Task<bool> FieldExpireAtAsync(string field, DateTime expire, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(expire), flags);

        public Task<bool> FieldExpireAsync(string field, TimeSpan expire, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(expire), flags);

        public Task<bool> FieldExpireSecondsAsync(string field, int seconds, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(new TimeSpan(0, 0, seconds)), flags);

        public Task<bool> FieldExpireMinutesAsync(string field, int minutes, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(new TimeSpan(0, minutes, 0)), flags);

        public Task<bool> FieldExpireHoursAsync(string field, int hours, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(new TimeSpan(hours, 0, 0)), flags);

        public Task<bool> FieldExpireDaysAsync(string field, int days, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(new TimeSpan(days, 0, 0, 0)), flags);

        private async Task<bool> FieldExpireBase(string field, Action<CacheItem<TField>> setExpire, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = await Database.HashGetAsync(RedisKey, field, flags);
            if (!TryDeserializeExpire(redisValue, out CacheItem<TField> cacheItem))
                return false;
            setExpire(cacheItem);
            await Database.HashSetAsync(RedisKey, field, SerializeExpire(cacheItem), When.Always, flags);
            return true;
        }
        #endregion

        #region Serializer
        protected RedisValue SerializeExpire(TField value, DateTime? expireAt)
        {
            var cacheItem = new CacheItem<TField>(value, expireAt);
            return Serializer.Serialize(cacheItem);
        }
        protected RedisValue SerializeExpire(TField value, TimeSpan? expire)
        {
            var cacheItem = new CacheItem<TField>(value, expire);
            return Serializer.Serialize(cacheItem);
        }
        protected RedisValue SerializeExpire(CacheItem<TField> cacheItem)
        {
            return Serializer.Serialize(cacheItem);
        }
        protected TField DeserializeExpire(string field, RedisValue redisValue)
        {
            if (!TryDeserializeExpire(field, redisValue, out TField ret))
                throw new CacheNotFound($"redis缓存项不存在,此调用非法请检查代码! type:{GetType().FullName}");
            return ret;
        }
        protected bool TryDeserializeExpire(RedisValue redisValue, out CacheItem<TField> cacheItem)
        {
            cacheItem = default;
            if (redisValue.IsNull)// redis不存在此key
            {
                return false;
            }
            else // 正常有值
            {
                cacheItem = (CacheItem<TField>)Serializer.Deserialize(redisValue, typeof(CacheItem<TField>));
                return !cacheItem.IsExpired;
            }
        }
        protected bool TryDeserializeExpire(string field, RedisValue redisValue, out TField value)
        {
            return TryDeserializeExpire<TField>(field, redisValue, out value);
        }
        protected bool TryDeserializeExpire<T>(string field, RedisValue redisValue, out T value)
        {
            value = default;
            if (redisValue.IsNull)// redis不存在此key
            {
                return false;
            }
            else // 正常有值
            {
                var cacheItem = (CacheItem<T>)Serializer.Deserialize(redisValue, typeof(CacheItem<T>));
                if (cacheItem.IsExpired)
                {
                    var _ = DeleteAsync(field);
                    return false;
                }
                else
                {
                    value = cacheItem.Value;
                }
            }
            return true;
        }
        #endregion
    }
}
