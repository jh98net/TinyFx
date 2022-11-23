using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;

namespace TinyFx.Extensions.StackExchangeRedis
{
    /// <summary>
    /// Redis string结构,可以被继承，也可以直接构建
    /// 可存入null值，不存在抛出异常CacheNotFound
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RedisStringClient<T> : RedisClientBase
    {
        public override RedisType RedisType => RedisType.String;

        #region Constructors
        public RedisStringClient(RedisClientOptions options = null) : base(options) { }
        #endregion

        #region LoadValueWhenRedisNotExists
        /// <summary>
        /// 在调用GetOrLoad时，当RedisKey不存时，将调用此方法获取缓存值，返回并存储到Redis中，需要时子类实现override。
        /// 子类实现时注意：
        /// 1)可返回null，存入Redis的值为RedisValue.EmptyString
        /// 2)可抛出异常CacheNotFound：表示缓存值不存在
        /// </summary>
        /// <returns></returns>
        protected virtual T LoadValueWhenRedisNotExists()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Set
        /// <summary>
        /// 设置值并设置指定秒数过期
        /// </summary>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool SetAndExpireSeconds(T value, int seconds, When when = When.Always, CommandFlags flags = CommandFlags.None)
            => Database.StringSet(RedisKey, Serialize(value), new TimeSpan(0, 0, seconds), when, flags);

        /// <summary>
        /// 设置值并设置指定分钟数过期
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minutes"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool SetAndExpireMinutes(T value, int minutes, When when = When.Always, CommandFlags flags = CommandFlags.None)
            => Database.StringSet(RedisKey, Serialize(value), new TimeSpan(0, minutes, 0), when, flags);

        /// <summary>
        /// 设置值并设置指定小时数过期
        /// </summary>
        /// <param name="value"></param>
        /// <param name="hours"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool SetAndExpireHours(T value, int hours, When when = When.Always, CommandFlags flags = CommandFlags.None)
            => Database.StringSet(RedisKey, Serialize(value), new TimeSpan(hours, 0, 0), when, flags);

        /// <summary>
        /// 设置值并设置指定天数过期
        /// </summary>
        /// <param name="value"></param>
        /// <param name="days"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool SetAndExpireDays(T value, int days, When when = When.Always, CommandFlags flags = CommandFlags.None)
            => Database.StringSet(RedisKey, Serialize(value), new TimeSpan(days,0, 0, 0), when, flags);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool Set(T value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
            => Database.StringSet(RedisKey, Serialize(value), expiry, when, flags);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public Task<bool> SetAsync(T value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
            => Database.StringSetAsync(RedisKey, Serialize(value), expiry, when, flags);
        #endregion

        #region Get & GetOrLoad & GetOrException & GetOrDefault
        /// <summary>
        /// 获取此Hash中field对应的缓存值，不存在返回default(T)
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T Get(CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.StringGet(RedisKey, flags);
            return Deserialize<T>(redisValue);
        }
        public async Task<T> GetAsync(CommandFlags flags = CommandFlags.None)
            => await Task.Factory.StartNew(() => Get(flags));

        public T GetOrLoad(CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.StringGet(RedisKey, flags);
            if (!TryDeserialize(redisValue, out T ret))
            {
                ret = LoadValueWhenRedisNotExists();
                Database.StringSet(RedisKey, Serialize(ret), null, When.Always, flags);
            }
            return ret;
        }
        public async Task<T> GetOrLoadAsync(CommandFlags flags = CommandFlags.None)
            => await Task.Factory.StartNew(() => GetOrLoad(flags));

        /// <summary>
        /// 获取缓存，如果不存在则抛出异常CacheNotFound
        /// </summary>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public T GetOrException(CommandFlags commandFlags = CommandFlags.None)
        {
            var redisValue = Database.StringGet(RedisKey, commandFlags);
            if(!TryDeserialize(redisValue, out T ret))
                throw new CacheNotFound($"[Redis String]缓存项不存在。key:{RedisKey} type:{GetType().FullName}");
            return ret;
        }
        public async Task<T> GetOrExceptionAsync(CommandFlags flags = CommandFlags.None)
            => await Task.Factory.StartNew(() => GetOrException(flags));

        /// <summary>
        /// 获取缓存,如果不存在，则返回默认值
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public T GetOrDefault(T defaultValue, CommandFlags commandFlags = CommandFlags.None)
        {
            var redisValue = Database.StringGet(RedisKey, commandFlags);
            return TryDeserialize(redisValue, out T ret) ? ret : defaultValue;
        }
        public async Task<T> GetOrDefaultAsync(T defaultValue, CommandFlags flags = CommandFlags.None)
            => await Task.Factory.StartNew(() => GetOrDefault(defaultValue, flags));
        #endregion

        #region Increment
        /// <summary>
        /// Hash结构存储增量数字。如果field不存在则设置为0。支持long
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long Increment(long value = 1, CommandFlags flags = CommandFlags.None)
            => Database.StringIncrement(RedisKey, value, flags);

        /// <summary>
        /// Hash结构存储增量数字。如果field不存在则设置为0。支持long
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public double Increment(double value, CommandFlags flags = CommandFlags.None)
            => Database.StringIncrement(RedisKey, value, flags);
        public async Task<long> IncerementAsync(long value = 1, CommandFlags flags = CommandFlags.None)
            => await Database.StringIncrementAsync(RedisKey, value, flags);

        public async Task<double> IncerementAsync(double value, CommandFlags flags = CommandFlags.None)
            => await Database.StringIncrementAsync(RedisKey, value, flags);

        /// <summary>
        /// 减量数字-value,如不存在key则创建，返回减量后值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long Decrement(long value = 1, CommandFlags flags = CommandFlags.None)
            => Database.StringDecrement(RedisKey, value, flags);

        /// <summary>
        /// 减量数字-value,如不存在key则创建，返回减量后值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public double Decrement(double value, CommandFlags flags = CommandFlags.None)
            => Database.StringDecrement(RedisKey, value, flags);
        public async Task<long> DecrementAsync(long value = 1, CommandFlags flags = CommandFlags.None)
            => await Database.StringDecrementAsync(RedisKey, value, flags);

        public async Task<double> DecrementAsync(double value, CommandFlags flags = CommandFlags.None)
            => await Database.StringDecrementAsync(RedisKey, value, flags);

        #endregion

        #region GetLength & Append & BitCount & GetRange &SetRange
        /// <summary>
        /// 返回存储在键处的字符串值的长度
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long GetLength(CommandFlags flags = CommandFlags.None)
            => Database.StringLength(RedisKey, flags);
        /// <summary>
        /// 如果key已经存在并且是字符串，则此命令将值附加在字符串的末尾。 
        /// 如果key不存在，则会创建key并将其设置为空字符串，因此APPEND在这种特殊情况下将类似于SET
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long Append(string value, CommandFlags flags = CommandFlags.None)
            => Database.StringAppend(RedisKey, value, flags);
        /// <summary>
        /// 计算字符串中的设置位数（填充计数）。 
        /// 默认情况下，将检查字符串中包含的所有字节，也可以仅在传递附加参数start和end的间隔中指定计数操作。 
        /// 像GETRANGE命令一样，开始和结束可以包含负值，以便从字符串的末尾开始索引字节，其中-1是最后一个字节，-2是倒数第二个，依此类推
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long BitCount(long start = 0, long end = -1, CommandFlags flags = CommandFlags.None)
            => Database.StringBitCount(RedisKey, start, end, flags);

        /// <summary>
        /// 返回存储在key处的字符串值的子字符串，该字符串由偏移量start和end（包括两端）确定。 
        /// 可以使用负偏移量来提供从字符串末尾开始的偏移量。 因此-1表示最后一个字符，-2表示倒数第二个，依此类推。
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public string GetRange(long start, long end, CommandFlags flags = CommandFlags.None)
            => Database.StringGetRange(RedisKey, start, end, flags);
        /// <summary>
        /// 从指定的偏移量开始，覆盖整个值范围内从key存储的字符串的一部分。 
        /// 如果偏移量大于key处字符串的当前长度，则该字符串将填充零字节以使偏移量适合。 
        /// 不存在的键被视为空字符串，因此此命令将确保它包含足够大的字符串以能够将值设置为offset
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public string SetRange(long offset, string value, CommandFlags flags = CommandFlags.None)
            => Database.StringSetRange(RedisKey, offset, value, flags);
        #endregion
    }
}
