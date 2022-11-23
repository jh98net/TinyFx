using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis.Serializers;

namespace TinyFx.Extensions.StackExchangeRedis
{
    /// <summary>
    /// Redis对象基类：hash, list, set, zset(sortedset),string
    ///     RedisKey = GetGlobalRedisKey() 或者 GetGlobalRedisKey()
    /// </summary>
    public abstract class RedisClientBase
    {
        #region Properties
        /// <summary>
        /// Redis类型
        /// </summary>
        public abstract RedisType RedisType { get; }

        /// <summary>
        /// 当前Redis缓存key
        ///     GetProjectRedisKey(id) --默认
        ///     GetGlobalRedisKey(id)
        /// </summary>
        public string RedisKey { get; set; }

        /// <summary>
        /// Redis设置
        /// </summary>
        public RedisClientOptions Options { get; private set; }
        /// <summary>
        /// 设置DatabaseIndex
        /// </summary>
        /// <param name="databaseIndex">等于-1,使用connectionString定义</param>
        public void SetDatabaseIndex(int databaseIndex = -1)
        {
            Options.DatabaseIndex = databaseIndex;
        }

        private ISerializer _serializer;
        /// <summary>
        /// 缓存对象序列化器
        /// </summary>
        protected ISerializer Serializer
        {
            get
            {
                if (_serializer == null)
                    _serializer = RedisUtil.GetSerializer(Options.SerializeMode);
                return _serializer;
            }
        }

        private static ConcurrentDictionary<Type, string> _connectionStringCache = new ConcurrentDictionary<Type, string>();

        private string GetConnectionString()
        {
            if (!string.IsNullOrEmpty(Options.ConnectionString))
                return Options.ConnectionString;
            var type = GetType();
            // 子类才缓存
            if (_connectionStringCache.TryGetValue(type, out string ret))
                return ret;
            ret = RedisUtil.GetConfigElement(Options.ConnectionStringName, type).ConnectionString;
            if (type.Namespace != "TinyFx.Extensions.StackExchangeRedis")
                _connectionStringCache.TryAdd(type, ret);
            return ret;
        }
        private IDatabase _database;
        /// <summary>
        /// 操作缓存数据的Database
        /// </summary>
        public IDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var connString = GetConnectionString();
                    _database = RedisUtil.GetMultiplexer(connString)
                        .GetDatabase(Options.DatabaseIndex);
                }
                return _database;
            }
        }

        /// <summary>
        /// 根据 Options 的设置 Redis Key 的 Expire
        /// </summary>
        public void KeyExpire()
        {
            if (Options.ExpirySpan.HasValue)
                _database.KeyExpire(RedisKey, Options.ExpirySpan.Value);
            else if (Options.ExpiryAt.HasValue)
                _database.KeyExpire(RedisKey, Options.ExpiryAt.Value);
        }
        #endregion

        #region Constructors

        public RedisClientBase(RedisClientOptions options = null)
        {
            Options = options ?? new RedisClientOptions();
            RedisKey = RedisUtil.GetProjectRedisKey(GetType());
        }
        #endregion    

        #region Methods
        public bool KeyDelete(CommandFlags flags = CommandFlags.None)
            => Database.KeyDelete(RedisKey, flags);
        public byte[] KeyDump(RedisKey key, CommandFlags flags = CommandFlags.None)
            => Database.KeyDump(RedisKey, flags);
        /// <summary>
        /// 当前RedisKey是否存在
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool KeyExists(CommandFlags flags = CommandFlags.None)
            => Database.KeyExists(RedisKey, flags);
        /// <summary>
        /// 设置当前缓存的确切到期时间
        /// </summary>
        /// <param name="expiryAt">要设置的确切到期日期</param>
        /// <param name="flags"></param>
        /// <returns>如果设置了超时，则为true。 如果密钥不存在或无法设置超时，则为false</returns>
        public bool KeyExpireAt(DateTime expiryAt, CommandFlags flags = CommandFlags.None)
            => Database.KeyExpire(RedisKey, expiryAt, flags);

        /// <summary>
        /// 设置当前缓存从添加时算起，缓存多长时间到期
        /// </summary>
        /// <param name="expirySpan">要设置的超时时间</param>
        /// <param name="flags"></param>
        /// <returns>如果设置了超时，则为true。 如果密钥不存在或无法设置超时，则为false</returns>
        public bool KeyExpire(TimeSpan expirySpan, CommandFlags flags = CommandFlags.None)
            => Database.KeyExpire(RedisKey, expirySpan, flags);
        public bool KeyExpireMilliseconds(int milliseconds, CommandFlags flags = CommandFlags.None)
             => KeyExpire(TimeSpan.FromMilliseconds(milliseconds), flags);
        /// <summary>
        /// 设置当前缓存到指定秒数后过期
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool KeyExpireSeconds(int seconds, CommandFlags flags = CommandFlags.None)
            => KeyExpire(TimeSpan.FromSeconds(seconds), flags);
        /// <summary>
        /// 设置当前缓存到指定分钟数后过期
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool KeyExpireMinutes(int minutes, CommandFlags flags = CommandFlags.None)
            => KeyExpire(TimeSpan.FromMinutes(minutes), flags);
        /// <summary>
        /// 设置当前缓存到指定小时数后过期
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool KeyExpireHours(int hours, CommandFlags flags = CommandFlags.None)
            => KeyExpire(TimeSpan.FromHours(hours), flags);
        public bool KeyExpireDays(int days, CommandFlags flags = CommandFlags.None)
            => KeyExpire(TimeSpan.FromDays(days), flags);
        /// <summary>
        /// 返回自存储在指定键处的对象处于空闲状态以来的时间（未请求读或写操作）
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public TimeSpan? KeyIdleTime(CommandFlags flags = CommandFlags.None)
            => Database.KeyIdleTime(RedisKey, flags);

        /// <summary>
        /// 将键重命名为newkey。 当源名称和目标名称相同或键不存在时，它将返回错误
        /// </summary>
        /// <param name="newKey"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns>如果密钥已重命名，则为true，否则为false</returns>
        public bool KeyRename(RedisKey newKey, When when = When.Always, CommandFlags flags = CommandFlags.None)
            => Database.KeyRename(RedisKey, newKey, when, flags);
        /// <summary>
        /// 返回具有超时的键的剩余生存时间
        /// </summary>
        /// <param name="flags"></param>
        /// <returns>TTL，如果键不存在或没有超时，则为null</returns>
        public TimeSpan? KeyTimeToLive(CommandFlags flags = CommandFlags.None)
            => Database.KeyTimeToLive(RedisKey, flags);
        /// <summary>
        /// 当前Redis缓存数据类型
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public RedisType KeyType(CommandFlags flags = CommandFlags.None)
            => Database.KeyType(RedisKey, flags);

        /// <summary>
        /// 如果尚未使用，则获取一个锁（指定令牌值）
        /// </summary>
        /// <param name="key">锁的key</param>
        /// <param name="token">要在key上设置的值</param>
        /// <param name="expiry">key到期</param>
        /// <param name="flags"></param>
        public bool LockTake(string key, string token, TimeSpan expiry, CommandFlags flags = CommandFlags.None)
            => Database.LockTake(key, token, expiry, flags);

        /// <summary>
        /// 如果令牌值正确，则释放一个锁
        /// </summary>
        /// <param name="key">锁的key</param>
        /// <param name="token">key上必须匹配的值</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool LockRelease(string key, string token, CommandFlags flags = CommandFlags.None)
            => Database.LockRelease(key, token, flags);

        /// <summary>
        /// 查询针对锁持有的令牌
        /// </summary>
        /// <param name="key">锁的key</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public string LockQuery(string key, CommandFlags flags = CommandFlags.None)
            => Database.LockQuery(key, flags);

        /// <summary>
        /// 分布式锁定
        /// </summary>
        /// <param name="lockKey">要锁定资源的键值，针对每一个需要锁定的资源，名称必须唯一，如需要锁定操作用户coin，LockKey="lock_key_user_coin"</param>
        /// <param name="expiryTime">锁定资源后，如不手动释放，则在过期时间后自动释放锁（注意需确保锁定后的执行完成），默认10秒</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="retryInterval">重试间隔</param>
        /// <returns></returns>
        public RedLock Lock(string lockKey, TimeSpan? expiryTime = null, int retryCount = 0, TimeSpan? retryInterval = null)
        {
            var ret = new RedLock(Database, lockKey, expiryTime, retryCount, retryInterval);
            ret.ClientType = GetType();
            ret.Start();
            return ret;
        }
        public RedLock Lock(string lockKey, int seconds, int retryCount = 0, TimeSpan? retryInterval = null)
            => Lock(lockKey, TimeSpan.FromSeconds(seconds), retryCount, retryInterval);
        
        /// <summary>
        /// 分布式锁定，样例
        /// using(var redLock = await LockAsync())
        /// {
        ///     if(redLock.IsLocked) //成功上锁
        ///     { }
        ///     else
        ///     { }
        /// }
        /// </summary>
        /// <param name="lockKey">要锁定资源的键值，针对每一个需要锁定的资源，名称必须唯一，如需要锁定操作用户coin，LockKey="lock_key_user_coin"</param>
        /// <param name="expiryTime">锁定资源后，如不手动释放，则在过期时间后自动释放锁（注意需确保锁定后的执行完成），默认10秒</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="retryInterval">重试间隔</param>
        /// <returns></returns>
        public async Task<RedLock> LockAsync(string lockKey, TimeSpan? expiryTime = null, int retryCount = 0, TimeSpan? retryInterval = null)
        {
            var ret = new RedLock(Database, lockKey, expiryTime, retryCount, retryInterval);
            ret.ClientType = GetType();
            await ret.StartAsync();
            return ret;
        }
        public async Task<RedLock> LockAsync(string lockKey, int seconds, int retryCount = 0, TimeSpan? retryInterval = null)
            => await LockAsync(lockKey, TimeSpan.FromSeconds(seconds), retryCount, retryInterval);
        #endregion

        #region Utils
        /// <summary>
        /// 存入Redis前将T类型转换为RedisValue，如果值为null则存入EmptyString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual RedisValue Serialize(object value)
            => (value == null) ? RedisValue.EmptyString
            : (RedisValue)Serializer.Serialize(value);

        /// <summary>
        /// 反序列化，缓存项必须存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        protected T Deserialize<T>(RedisValue redisValue)
        {
            if (!TryDeserialize(redisValue, out T ret))
                throw new CacheNotFound($"redis缓存项不存在,此调用非法请检查代码! type:{GetType().FullName}");
            return ret;
        }

        protected bool TryDeserialize<T>(RedisValue redisValue, out T value)
        {
            var ret = TryDeserialize(redisValue, typeof(T), out object objValue);
            value = ret ? (T)objValue : default;
            return ret;
        }
        protected virtual bool TryDeserialize(RedisValue redisValue, Type toType, out object value)
        {
            value = null;
            if (redisValue.IsNull)// redis不存在此key
            {
                return false;
            }
            else if (redisValue.IsNullOrEmpty) // redis值是EmptyString
            {
                if (!AllowNull(toType))
                    throw new Exception($"缓存存储的是RedisValue.EmptyString,需转换的类不支持null。type:{toType.FullName}");
                else
                    return true;
            }
            else // 正常有值
            {
                value = Serializer.Deserialize(redisValue, toType);
            }
            return true;
        }
        private static ConcurrentDictionary<Type, bool> _typeCache = new ConcurrentDictionary<Type, bool>();
        // 当前类型是否可存null值
        private bool AllowNull(Type toType)
        {
            if (!_typeCache.TryGetValue(toType, out bool ret))
            {
                if (toType.IsClass || toType.IsNullableType())
                    ret = true;
                _typeCache.TryAdd(toType, ret);
            }
            return ret;
        }
        #endregion

        #region GetRedisKey
        /// <summary>
        /// 全局默认RedisKey格式:Global:TypeName:Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetGlobalRedisKey(object id = null)
            => RedisUtil.GetGlobalRedisKey(GetType(), id);
        /// <summary>
        /// 全局默认RedisKey格式:Global:TypeName:Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetProjectRedisKey(object id = null)
            => RedisUtil.GetProjectRedisKey(GetType(), id);
        #endregion
    }
}
