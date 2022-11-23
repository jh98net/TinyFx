using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;

namespace TinyFx.Extensions.StackExchangeRedis
{
    public class RedisHashBase<TField> : RedisClientBase
    {
        public override RedisType RedisType => RedisType.Hash;
        public RedisHashBase(RedisClientOptions options = null) : base(options) { }

        #region LoadValueWhenRedisNotExists
        public delegate bool LoadValueDelegate(string field, out TField value);
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
        protected virtual bool LoadValueWhenRedisNotExists(string field, out TField value)
        {
            if (LoadValueHandler != null)
            {
                return LoadValueHandler(field, out value);
            }
            throw new NotImplementedException();
        }
        public delegate Dictionary<string, TField> LoadAllValuesDelegate();
        public LoadAllValuesDelegate LoadAllValuesHandler;
        /// <summary>
        /// 调用GetAllOrLoad()时，当RedisKey不存在，则调用此方法返回并存储全部hash值到redis中，需要时子类实现override
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<string, TField> LoadAllValuesWhenRedisNotExists()
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
        public void Set(Dictionary<string, TField> values, CommandFlags flags = CommandFlags.None)
        {
            var entries = values.Select(kv => new HashEntry(kv.Key, Serialize(kv.Value)));
            Database.HashSet(RedisKey, entries.ToArray(), flags);
        }

        /// <summary>
        /// 【创建或更新】设置hash结构中的field。如果key不存在创建，如果field存在则覆盖，不存在则添加
        /// </summary>
        /// <param name="values"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task SetAsync(Dictionary<string, TField> values, CommandFlags flags = CommandFlags.None)
        {
            var entries = values.Select(kv => new HashEntry(kv.Key, Serialize(kv.Value)));
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
                if (TryDeserialize(value, out TField v))
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
            return Database.HashGetAll(RedisKey, flags).ToDictionary(
                        x => x.Name.ToString(),
                        x => Deserialize<TField>(x.Value),
                        StringComparer.Ordinal);
        }

        /// <summary>
        /// 从Hash结构Get所有缓存项
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, TField>> GetAllAsync(CommandFlags flags = CommandFlags.None)
        {
            var ret = await Database.HashGetAllAsync(RedisKey, flags);
            return ret.ToDictionary(
                        x => x.Name.ToString(),
                        x => Deserialize<TField>(x.Value),
                        StringComparer.Ordinal);
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
            var ret = LoadAllValuesWhenRedisNotExists();
            Set(ret, flags);
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
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool Exists(string field, CommandFlags commandFlags = CommandFlags.None)
            => Database.HashExists(RedisKey, field, commandFlags);

        /// <summary>
        /// Hash是否存在指定field
        /// </summary>
        /// <param name="field"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public Task<bool> ExistsAsync(string field, CommandFlags commandFlags = CommandFlags.None)
            => Database.HashExistsAsync(RedisKey, field, commandFlags);

        /// <summary>
        /// 获取hash中所有的fields
        /// </summary>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public IEnumerable<string> GetFields(CommandFlags commandFlags = CommandFlags.None)
            => Database.HashKeys(RedisKey, commandFlags).Select(x => x.ToString());

        /// <summary>
        /// 获取hash中所有的fields
        /// </summary>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetFieldsAsync(CommandFlags commandFlags = CommandFlags.None)
            => (await Database.HashKeysAsync(RedisKey, commandFlags)).Select(x => x.ToString());

        /// <summary>
        /// 返回hash中所有的values
        /// </summary>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public IEnumerable<TField> GetValues(CommandFlags commandFlags = CommandFlags.None)
            => Database.HashValues(RedisKey, commandFlags).Select(x => Deserialize<TField>(x));

        /// <summary>
        /// 返回hash中所有的values
        /// </summary>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TField>> GetValuesAsync(CommandFlags commandFlags = CommandFlags.None)
            => (await Database.HashValuesAsync(RedisKey, commandFlags)).Select(x => Deserialize<TField>(x));

        /// <summary>
        /// 获取hash内缓存项数量
        /// </summary>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public long GetLength(CommandFlags commandFlags = CommandFlags.None)
            => Database.HashLength(RedisKey, commandFlags);

        /// <summary>
        /// 获取hash内缓存项数量
        /// </summary>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public async Task<long> GetLengthAsync(CommandFlags commandFlags = CommandFlags.None)
            => await Database.HashLengthAsync(RedisKey, commandFlags);

        /// <summary>
        /// HSCAN游标查询命令。时间复杂度O(N).N为hash中的field数量
        /// </summary>
        /// <param name="pattern">要获取条目的键的模式</param>
        /// <param name="pageSize">要迭代的页面大小</param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public Dictionary<string, TField> Scan(string pattern, int pageSize, CommandFlags commandFlags = CommandFlags.None)
        {
            return Database.HashScan(RedisKey, pattern, pageSize, commandFlags).ToDictionary(
                x => x.Name.ToString(),
                x => Deserialize<TField>(x.Value),
                StringComparer.Ordinal);
        }

        /// <summary>
        /// HSCAN游标查询命令
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, TField>> ScanAsync(string pattern, int pageSize, CommandFlags commandFlags = CommandFlags.None)
        {
            return (await Task.Run(() => Database.HashScan(RedisKey, pattern, pageSize, commandFlags)))
                .ToDictionary(x => x.Name.ToString(), x => Deserialize<TField>(x.Value), StringComparer.Ordinal);
        }
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
            return Database.HashScan(RedisKey, pattern, pageSize, cursor, pageOffset, flags).ToDictionary(
               x => x.Name.ToString(),
               x => Deserialize<TField>(x.Value),
               StringComparer.Ordinal);
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
        {
            return (await Task.Run(() => Database.HashScan(RedisKey, pattern, pageSize, cursor, pageOffset, flags)))
                .ToDictionary(x => x.Name.ToString(), x => Deserialize<TField>(x.Value), StringComparer.Ordinal);
        }
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
    }
}
