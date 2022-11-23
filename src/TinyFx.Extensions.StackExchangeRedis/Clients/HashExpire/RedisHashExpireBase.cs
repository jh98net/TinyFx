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
        public RedisHashExpireBase(RedisClientOptions options = null) : base(options) { }

        #region LoadValueWhenRedisNotExists
        public delegate bool LoadValueDelegate(string field, out CacheItem<TField> value);
        public LoadValueDelegate LoadValueHandler;
        /// <summary>
        /// 在调用GetOrLoad时，当Redis不存在此field时，将调用此方法获取field对应的缓存值，返回并存储到Redis中，需要时子类实现override。
        ///     1) 返回true表示保存缓存项，false表示不保存缓存项
        ///     2) value可返回null，存入Redis的值为RedisValue.EmptyString
        ///     3) 可抛出异常CacheNotFound：表示field对应的值不存在
        /// </summary>
        /// <param name="field">key</param>
        /// <param name="value">返回的缓存项</param>
        /// <returns></returns>
        protected virtual bool LoadValueWhenRedisNotExists(string field, out CacheItem<TField> value)
        {
            if (LoadValueHandler != null)
            {
                return LoadValueHandler(field, out value);
            }
            throw new NotImplementedException();
        }
        public delegate Dictionary<string, CacheItem<TField>> LoadAllValuesDelegate();
        public LoadAllValuesDelegate LoadAllValuesHandler;
        /// <summary>
        /// 调用GetAllOrLoad()时，当RedisKey不存在，则调用此方法返回并存储全部hash值到redis中，需要时子类实现override
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<string, CacheItem<TField>> LoadAllValuesWhenRedisNotExists()
        {
            if (LoadAllValuesHandler != null)
                return LoadAllValuesHandler();
            throw new NotImplementedException();
        }
        #endregion

        #region Set
        /// <summary>
        /// 【创建或更新】设置hash结构中的field
        /// </summary>
        /// <param name="values"></param>
        /// <param name="flags"></param>
        public void Set(Dictionary<string, CacheItem<TField>> values, CommandFlags flags = CommandFlags.None)
        {
            var entries = values.Select(kv => new HashEntry(kv.Key, SerializeExpire(kv.Value)));
            Database.HashSet(RedisKey, entries.ToArray(), flags);
        }

        /// <summary>
        /// 【创建或更新】设置hash结构中的field。如果key不存在创建，如果field存在则覆盖，不存在则添加
        /// </summary>
        /// <param name="values"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task SetAsync(Dictionary<string, CacheItem<TField>> values, CommandFlags flags = CommandFlags.None)
        {
            var entries = values.Select(kv => new HashEntry(kv.Key, SerializeExpire(kv.Value)));
            await Database.HashSetAsync(RedisKey, entries.ToArray(), flags);
        }
        #endregion

        #region Get & GetAll
        /// <summary>
        /// 获取一组field对应的缓存值
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public Dictionary<string, CacheValue<TField>> Get(IEnumerable<string> fields, CommandFlags flags = CommandFlags.None)
        {
            var arrFields = fields.Select(key => (RedisValue)key).ToArray();
            var values = Database.HashGet(RedisKey, arrFields, flags);
            return GetCacheValues(arrFields, values);
        }
        /// <summary>
        /// 从Hash结构Get缓存项
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, CacheValue<TField>>> GetAsync(IEnumerable<string> fields, CommandFlags flags = CommandFlags.None)
        {
            var arrFields = fields.Select(key => (RedisValue)key).ToArray();
            var values = await Database.HashGetAsync(RedisKey, arrFields, flags);
            return GetCacheValues(arrFields, values);
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

        /// <summary>
        /// 从Hash结构Get所有缓存项
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public Dictionary<string, TField> GetAll(CommandFlags flags = CommandFlags.None)
        {
            var ret = new Dictionary<string, TField>();
            var entries = Database.HashGetAll(RedisKey, flags);
            foreach (var entry in entries)
            {
                if (TryDeserializeExpire(entry.Name, entry.Value, out TField v))
                    ret.Add(entry.Name, v);
            }
            return ret;
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
            return ret;
        }

        /// <summary>
        /// 获取所有hash缓存值，如果RedisKey不存在，则调用LoadAllValuesWhenRedisNotExists()返回并保存到redis中
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public Dictionary<string, TField> GetAllOrLoad(CommandFlags flags = CommandFlags.None)
        {
            if (KeyExists(flags))
                return GetAll(flags);
            var values = LoadAllValuesWhenRedisNotExists();
            Set(values, flags);
            var ret = new Dictionary<string, TField>();
            foreach (var item in values)
                ret.Add(item.Key, item.Value.Value);
            return ret;
        }
        public async Task<Dictionary<string, TField>> GetAllOrLoadAsync(CommandFlags flags = CommandFlags.None)
            => await Task.Factory.StartNew(() => GetAllOrLoad(flags));
        #endregion

        #region Delete
        /// <summary>
        /// 从Hash结构移除kefield。时间复杂度：O(1)
        /// </summary>
        /// <param name="field"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool Delete(string field, CommandFlags commandFlags = CommandFlags.None)
            => Database.HashDelete(RedisKey, field, commandFlags);

        /// <summary>
        /// 从Hash结构移除fields。时间复杂度：O(1)
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public long Delete(IEnumerable<string> fields, CommandFlags commandFlags = CommandFlags.None)
            => Database.HashDelete(RedisKey, fields.Select(x => (RedisValue)x).ToArray(), commandFlags);

        /// <summary>
        /// 从Hash结构移除field。时间复杂度：O(1)
        /// </summary>
        /// <param name="field"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(string field, CommandFlags commandFlags = CommandFlags.None)
            => Database.HashDeleteAsync(RedisKey, field, commandFlags);

        /// <summary>
        /// 从Hash结构移除fields。时间复杂度：O(1)
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public Task<long> DeleteAsync(IEnumerable<string> fields, CommandFlags commandFlags = CommandFlags.None)
            => Database.HashDeleteAsync(RedisKey, fields.Select(x => (RedisValue)x).ToArray(), commandFlags);
        #endregion

        #region Exists/GetFields/GetValues/GetLength/Scan
        /// <summary>
        /// Hash是否存在指定field
        /// </summary>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool Exists(string field, CommandFlags flags = CommandFlags.None)
            => Database.HashExists(RedisKey, field, flags);

        /// <summary>
        /// Hash是否存在指定field
        /// </summary>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public Task<bool> ExistsAsync(string field, CommandFlags flags = CommandFlags.None)
            => Database.HashExistsAsync(RedisKey, field, flags);

        /// <summary>
        /// 获取hash中所有的fields
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IEnumerable<string> GetFields(CommandFlags flags = CommandFlags.None)
            => Database.HashKeys(RedisKey, flags).Select(x => x.ToString());

        /// <summary>
        /// 获取hash中所有的fields
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetFieldsAsync(CommandFlags flags = CommandFlags.None)
            => (await Database.HashKeysAsync(RedisKey, flags)).Select(x => x.ToString());

        /// <summary>
        /// 返回hash中所有的values
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public List<TField> GetValues(CommandFlags flags = CommandFlags.None)
        {
            var ret = new List<TField>();
            var values = Database.HashValues(RedisKey, flags);
            foreach (var value in values)
            {
                if (TryDeserializeExpire(value, out CacheItem<TField> v) && !v.IsExpired)
                {
                    ret.Add(v.Value);
                }
            }
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
                if (TryDeserializeExpire(value, out CacheItem<TField> v) && !v.IsExpired)
                {
                    ret.Add(v.Value);
                }
            }
            return ret;
        }

        /// <summary>
        /// 获取hash内缓存项数量
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long GetLength(CommandFlags flags = CommandFlags.None)
            => Database.HashLength(RedisKey, flags);

        /// <summary>
        /// 获取hash内缓存项数量
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<long> GetLengthAsync(CommandFlags flags = CommandFlags.None)
            => await Database.HashLengthAsync(RedisKey, flags);

        /// <summary>
        /// HSCAN游标查询命令。时间复杂度O(N).N为hash中的field数量
        /// </summary>
        /// <param name="pattern">要获取条目的键的模式</param>
        /// <param name="pageSize">要迭代的页面大小</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public Dictionary<string, TField> Scan(string pattern, int pageSize, CommandFlags flags = CommandFlags.None)
        {
            var ret = new Dictionary<string, TField>();
            var entries = Database.HashScan(RedisKey, pattern, pageSize, flags);
            foreach (var entry in entries)
            {
                if (TryDeserializeExpire(entry.Name, entry.Value, out TField v))
                    ret.Add(entry.Name, v);
            }
            return ret;
        }

        /// <summary>
        /// HSCAN游标查询命令
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, TField>> ScanAsync(string pattern, int pageSize, CommandFlags flags = CommandFlags.None)
            => await Task.Run(() => Scan(pattern, pageSize, flags));
        /// <summary>
        /// HSCAN游标查询命令
        /// </summary>
        /// <param name="pattern">查询表达式，如：t*</param>
        /// <param name="pageSize">要迭代的页面大小</param>
        /// <param name="cursor">HSCAN 命令每次被调用之后， 都会向用户返回一个新的游标， 用户在下次迭代时需要使用这个新游标作为 HSCAN 命令的游标参数， 以此来延续之前的迭代过程。返回0表示结束</param>
        /// <param name="pageOffset"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public Dictionary<string, TField> Scan(RedisValue pattern, int pageSize, long cursor, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            var ret = new Dictionary<string, TField>();
            var entries = Database.HashScan(RedisKey, pattern, pageSize, cursor, pageOffset, flags);
            foreach (var entry in entries)
            {
                if (TryDeserializeExpire(entry.Name, entry.Value, out TField v))
                    ret.Add(entry.Name, v);
            }
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
        public async Task<Dictionary<string, TField>> ScanAsync(RedisValue pattern, int pageSize, long cursor, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
            => await Task.Run(() => Scan(pattern, pageSize, cursor, pageOffset, flags));
        #endregion

        #region Increment
        /// <summary>
        /// Hash结构存储增量数字。如果field不存在则设置为0。支持long
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long Increment(string field, long value = 1, CommandFlags flags = CommandFlags.None)
            => Database.HashIncrement(RedisKey, field, value, flags);

        /// <summary>
        /// Hash结构存储增量数字。如果field不存在则设置为0。支持long
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public double Increment(string field, double value, CommandFlags flags = CommandFlags.None)
            => Database.HashIncrement(RedisKey, field, value, flags);
        public async Task<long> IncerementAsync(string field, long value = 1, CommandFlags flags = CommandFlags.None)
            => await Database.HashIncrementAsync(RedisKey, field, value, flags);

        public async Task<double> IncerementAsync(string field, double value, CommandFlags flags = CommandFlags.None)
            => await Database.HashIncrementAsync(RedisKey, field, value, flags);

        /// <summary>
        /// 减量数字-value,如不存在key则创建，返回减量后值
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long Decrement(string field, long value = 1, CommandFlags flags = CommandFlags.None)
            => Database.HashDecrement(RedisKey, field, value, flags);

        /// <summary>
        /// 减量数字-value,如不存在key则创建，返回减量后值
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public double Decrement(string field, double value, CommandFlags flags = CommandFlags.None)
            => Database.HashDecrement(RedisKey, field, value, flags);
        public async Task<long> DecrementAsync(string field, long value = 1, CommandFlags flags = CommandFlags.None)
            => await Database.HashDecrementAsync(RedisKey, field, value, flags);

        public async Task<double> DecrementAsync(string field, double value, CommandFlags flags = CommandFlags.None)
            => await Database.HashDecrementAsync(RedisKey, field, value, flags);

        #endregion

        #region FieldExpire
        public bool FieldExpire(string field, DateTime expire, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(expire), flags);
        public bool FieldExpire(string field, TimeSpan expire, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(expire), flags);
        public bool FieldExpireAtSeconds(string field, int seconds, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(new TimeSpan(0, 0, seconds)), flags);
        public bool FieldExpireAtMinutes(string field, int minutes, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(new TimeSpan(0, minutes, 0)), flags);
        public bool FieldExpireAtHours(string field, int hours, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(new TimeSpan(hours, 0, 0)), flags);
        public bool FieldExpireAtDays(string field, int days, CommandFlags flags = CommandFlags.None)
            => FieldExpireBase(field, item => item.SetExpire(new TimeSpan(days, 0, 0, 0)), flags);
        private bool FieldExpireBase(string field, Action<CacheItem<TField>> setExpire, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.HashGet(RedisKey, field, flags);
            if (!TryDeserializeExpire(redisValue, out CacheItem<TField> cacheItem))
                return false;
            setExpire(cacheItem);
            Database.HashSet(RedisKey, field, SerializeExpire(cacheItem), When.Always, flags);
            return true;
        }
        #endregion

        #region Serializer
        protected RedisValue SerializeExpire(TField value)
        {
            var cacheItem = new CacheItem<TField>(value);
            return Serializer.Serialize(cacheItem);
        }
        protected RedisValue SerializeExpire(TField value, DateTime expireAt)
        {
            var cacheItem = new CacheItem<TField>(value, expireAt);
            return Serializer.Serialize(cacheItem);
        }
        protected RedisValue SerializeExpire(TField value, TimeSpan expire)
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
            }
            return true;
        }
        protected bool TryDeserializeExpire(string field, RedisValue redisValue, out TField value)
        {
            value = default;
            if (redisValue.IsNull)// redis不存在此key
            {
                return false;
            }
            else // 正常有值
            {
                var cacheItem = (CacheItem<TField>)Serializer.Deserialize(redisValue, typeof(CacheItem<TField>));
                if (cacheItem.IsExpired)
                {
                    Database.HashDelete(RedisKey, field);
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
