using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis.Serializers;
using StackExchange.Redis.KeyspaceIsolation;
using TinyFx.Serialization;
using TinyFx.Data;
using static System.Collections.Specialized.BitVector32;
using Newtonsoft.Json;
using TinyFx.Collections;
using System.Net;
using static StackExchange.Redis.RedisChannel;
using TinyFx.Reflection;
using System.Reflection;
using Grpc.Core;
using BloomFilter;
using BloomFilter.Redis;

namespace TinyFx.Extensions.StackExchangeRedis
{
    /// <summary>
    /// Redis辅助类，获取：
    ///     IDatabase:      操作Redis底层类，包含全部操作
    ///     RedisClient:    操作Redis高层类，针对Redis类型进行了封装
    /// </summary>
    public static class RedisUtil
    {
        #region Constructors
        private static ConcurrentDictionary<string, ConnectionStringElement> _elementDic = new();
        // key: ConfigString
        private static readonly ConcurrentDictionary<string, ConnectionMultiplexer> _redisDict = new();

        private static ISerializer _jsonSerializer { get; }
        public static JsonSerializerSettings JsonOptions { get; }
        private static readonly ISerializer _bytesSerializer = new RedisBytesSerializer();
        //private static readonly ISerializer _memoryPackSerializer = new RedisMemoryPackSerializer();

        static RedisUtil()
        {
            var serializer = new TinyJsonSerializer();
            _jsonSerializer = serializer;
            JsonOptions = serializer.JsonOptions;
        }
        #endregion

        #region Redis & Database
        public static ConnectionMultiplexer GetRedis(string connectionStringName = null)
        {
            var element = GetConfigElement(connectionStringName);
            return GetRedisByConnectionString(element.ConnectionString);
        }
        public static ConnectionMultiplexer GetRedisByConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");
            return _redisDict.GetOrAdd(connectionString, (key) =>
            {
                return ConnectionMultiplexer.ConnectAsync(connectionString).GetTaskResult(true);
            });
        }
        public static async Task<ConnectionMultiplexer> GetRedisByConnectionStringAsync(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            if (!_redisDict.TryGetValue(connectionString, out var ret))
            {
                ret = await ConnectionMultiplexer.ConnectAsync(connectionString);
                _redisDict.TryAdd(connectionString, ret);
            }
            return ret;
        }
        /// <summary>
        /// 默认Database
        /// </summary>
        public static IDatabase DefaultDatabase
            => GetDatabase();

        /// <summary>
        /// 获得基础Redis操作类IDatabase
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <param name="databaseIndex"></param>
        /// <returns></returns>
        public static IDatabase GetDatabase(string connectionStringName = null, int databaseIndex = -1)
        {
            var element = GetConfigElement(connectionStringName);
            return GetRedisByConnectionString(element.ConnectionString).GetDatabase(databaseIndex);
        }
        internal static ConnectionStringElement GetConfigElement(string connectionStringName = null, Type type = null)
        {
            var key = $"{type?.FullName}|{connectionStringName}";
            if (!_elementDic.TryGetValue(key, out var ret))
            {
                var section = ConfigUtil.GetSection<RedisSection>();
                if (section == null)
                    throw new Exception($"Redis配置不存在");
                if (string.IsNullOrEmpty(connectionStringName))
                {
                    connectionStringName = (type == null)
                        || !section.ConnectionStringNamespaces.TryGetValue(type.Namespace, out string name)
                        ? section.DefaultConnectionStringName
                        : name;
                }
                if (string.IsNullOrEmpty(connectionStringName))
                    throw new ArgumentNullException("connectionStringName");
                if (!section.ConnectionStrings.TryGetValue(connectionStringName, out ret))
                    throw new Exception($"Redis配置Redis:ConnectionStrings:Name不存在。Name:{connectionStringName}");
                if (string.IsNullOrEmpty(ret.ConnectionString))
                    throw new Exception($"Redis配置Redis:ConnectionStrings:ConnectionString不能为空。Name:{ret.Name}");
                _elementDic.TryAdd(key, ret);
            }
            return ret;
        }
        #endregion

        #region CreateClient
        public static RedisListClient<T> CreateListClient<T>(string redisKey, string connectionStringName = null)
        {
            var options = GetOptions(connectionStringName, null);
            var ret = new RedisListClient<T>(options);
            ret.RedisKey = redisKey;
            return ret;
        }
        public static RedisSetClient<T> CreateSetClient<T>(string redisKey, string connectionStringName = null)
        {
            var options = GetOptions(connectionStringName, null);
            var ret = new RedisSetClient<T>(options);
            ret.RedisKey = redisKey;
            return ret;
        }
        public static RedisSortedSetClient<T> CreateSortedSetClient<T>(string redisKey, string connectionStringName = null)
        {
            var options = GetOptions(connectionStringName, null);
            var ret = new RedisSortedSetClient<T>(options);
            ret.RedisKey = redisKey;
            return ret;
        }
        public static RedisStringClient<T> CreateStringClient<T>(string redisKey, string connectionStringName = null)
        {
            var options = GetOptions(connectionStringName, null);
            var ret = new RedisStringClient<T>(options);
            ret.RedisKey = redisKey;
            return ret;
        }
        public static RedisHashClient<T> CreateHashClient<T>(string redisKey, string connectionStringName = null)
        {
            var options = GetOptions(connectionStringName, null);
            var ret = new RedisHashClient<T>(options);
            ret.RedisKey = redisKey;
            return ret;
        }
        public static RedisHashMultiClient CreateHashMultiClient(string redisKey, string connectionStringName = null)
        {
            var options = GetOptions(connectionStringName, null);
            var ret = new RedisHashMultiClient(options);
            ret.RedisKey = redisKey;
            return ret;
        }
        public static RedisHashExpireClient<T> CreateHashExpireClient<T>(string redisKey, string connectionStringName = null)
        {
            var options = GetOptions(connectionStringName, null);
            var ret = new RedisHashExpireClient<T>(options);
            ret.RedisKey = redisKey;
            return ret;
        }
        public static RedisHashExpireMutilClient CreateHashExpireMutilClient(string redisKey, string connectionStringName = null)
        {
            var options = GetOptions(connectionStringName, null);
            var ret = new RedisHashExpireMutilClient(options);
            ret.RedisKey = redisKey;
            return ret;
        }
        #endregion

        #region GetRedisKey
        public static string GetGlobalGroupRedisKey(string group, Type type, object id = null)
        {
            return id == null
            ? $"Global:{group}:{GetTypeName(type)}"
            : $"Global:{group}:{GetTypeName(type)}:{Convert.ToString(id)}";
        }
        /// <summary>
        /// 全局默认RedisKey格式:Global:TypeName:Id
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetGlobalRedisKey(Type type, object id = null)
        {
            return id == null
            ? $"Global:{GetTypeName(type)}"
            : $"Global:{GetTypeName(type)}:{Convert.ToString(id)}";
        }
        /// <summary>
        /// 全局默认RedisKey格式:Global:TypeName:Id
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetGlobalRedisKey(string type, object id = null)
        {
            return id == null
                ? $"Global:{type}"
                : $"Global:{type}:{Convert.ToString(id)}";
        }
        public static string GetProjectGroupRedisKey(string group, Type type, object id = null)
        {
            return id == null
                ? $"{ConfigUtil.Project.ProjectId}:{group}:{GetTypeName(type)}"
                : $"{ConfigUtil.Project.ProjectId}:{group}:{GetTypeName(type)}:{Convert.ToString(id)}";
        }
        /// <summary>
        /// 项目默认RedisKey格式:ProjectId:TypeName:Id
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetProjectRedisKey(Type type, object id = null)
        {
            return id == null
                ? $"{ConfigUtil.Project.ProjectId}:{GetTypeName(type)}"
                : $"{ConfigUtil.Project.ProjectId}:{GetTypeName(type)}:{Convert.ToString(id)}";
        }
        /// <summary>
        /// 项目默认RedisKey格式:ProjectId:TypeName:Id
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetProjectRedisKey(string type, object id = null)
        {
            return id == null
                ? $"{ConfigUtil.Project.ProjectId}:{type}"
                : $"{ConfigUtil.Project.ProjectId}:{type}:{Convert.ToString(id)}";
        }
        private static ConcurrentDictionary<Type, string> _typeNameCache = new ConcurrentDictionary<Type, string>();
        private static string GetTypeName(Type type)
        {
            if (_typeNameCache.TryGetValue(type, out string typeName))
                return typeName;
            typeName = type.Name;
            var idx = typeName.IndexOf('`');
            if (idx > 0)
                typeName = typeName.Substring(0, idx);
            typeName = typeName.TrimEnd("DCache", false);
            _typeNameCache.TryAdd(type, typeName);
            return typeName;
        }
        #endregion

        #region Lock
        /// <summary>
        /// 分布式事务锁(锁自动延期)
        /// using(var redLock = await LockAsync())
        /// {
        ///     if(redLock.IsLocked) //成功上锁
        ///     { }
        ///     else
        ///     { }
        /// }
        /// </summary>
        /// <param name="lockKey">要锁定资源的键值（一般指业务范围）</param>
        /// <param name="seconds">锁定资源后，如不手动释放，则在过期时间后自动释放锁（注意需确保锁定后的执行完成），单位秒</param>
        /// <param name="retryCount">重试次数，默认6次</param>
        /// <param name="retryInterval">重试间隔，默认500</param>
        /// <returns></returns>
        public static async Task<RedLock> LockAsync(string lockKey, int seconds, int retryCount = 6, int retryInterval = 500)
            => await LockAsync(lockKey, TimeSpan.FromSeconds(seconds), retryCount, TimeSpan.FromMilliseconds(retryInterval));
        /// <summary>
        /// 申请锁，直到等待时间到期。申请到后锁自动延期
        /// </summary>
        /// <param name="lockKey"></param>
        /// <param name="waitSeconds"></param>
        /// <returns></returns>
        public static async Task<RedLock> LockWaitAsync(string lockKey, int waitSeconds)
        {
            var retryCount = waitSeconds * 1000 / 500;
            var retryInterval = TimeSpan.FromMilliseconds(500);
            return await LockAsync(lockKey, null, retryCount, retryInterval);
        }
        public static async Task<RedLock> LockAsync(string lockKey, TimeSpan? expiryTime = null, int retryCount = 0, TimeSpan? retryInterval = null)
        {
            var ret = new RedLock(DefaultDatabase, lockKey, expiryTime, retryCount, retryInterval);
            ret.ClientType = typeof(RedisUtil);
            await ret.StartAsync();
            return ret;
        }
        #endregion

        #region Publish
        /// <summary>
        /// 发布广播消息（RedisSubscribeConsumer子类消费）
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task PublishAsync<TMessage>(TMessage message)
        {
            var attr = typeof(TMessage).GetCustomAttribute<RedisPublishMessageAttribute>();
            var channel = GetChannel(message, attr?.PatternMode ?? PatternMode.Auto);
            var msg = await GetSerializer(RedisSerializeMode.Json).SerializeAsync(message);
            await GetRedis(attr?.ConnectionStringName)
                .GetSubscriber()
                .PublishAsync(channel, msg);
        }
        /// <summary>
        /// 发布队列消息,队列消息将被阻塞且单一执行（RedisQueueConsumer子类消费）
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task PublishQueueAsync<TMessage>(TMessage message)
        {
            var attr = typeof(TMessage).GetCustomAttribute<RedisPublishMessageAttribute>();
            var channel = GetChannel(message, attr?.PatternMode ?? PatternMode.Auto);
            var msg = await GetSerializer(RedisSerializeMode.Json).SerializeAsync(message);
            var redis = GetRedis(attr?.ConnectionStringName);
            var key = GetQueueKey<TMessage>();
            await redis.GetDatabase().ListLeftPushAsync(key, msg, flags: CommandFlags.FireAndForget);
            await redis.GetSubscriber().PublishAsync(channel, string.Empty);
        }
        internal static string GetBaseChannelName<TMessage>()
            => $"_PubSub:{typeof(TMessage).FullName}";
        private static RedisChannel GetChannel<TMessage>(TMessage msg, PatternMode mode = PatternMode.Auto)
        {
            var key = (msg is IRedisPublishMessage)
                ? ((IRedisPublishMessage)msg).PatternKey
                : null;
            return GetChannel(key, mode);
        }
        internal static RedisChannel GetChannel<TMessage>(string key, PatternMode mode = PatternMode.Auto)
        {
            var name = GetBaseChannelName<TMessage>();
            if (!string.IsNullOrEmpty(key))
                name = $"{name}:{key}";
            return new RedisChannel(name, mode);
        }
        internal static string GetQueueKey<TMessage>()
        {
            return $"_Queue:{typeof(TMessage).FullName}";
        }
        #endregion

        #region BloomFilter
        /// <summary>
        /// 布隆过滤器
        /// </summary>
        /// <param name="redisKey">业务标识</param>
        /// <param name="expectedElements">预期总元素数</param>
        /// <param name="method">哈希算法</param>
        /// <returns></returns>
        public static IBloomFilter CreateBloomFilter(string redisKey, long expectedElements, HashMethod method = HashMethod.Murmur3)
        {
            var key = $"_BloomFilter:{redisKey}";
            var conn = GetRedis();
            return FilterRedisBuilder.Build(conn, key, expectedElements, method);
        }
        #endregion

        #region Utils
        /// <summary>
        /// 查询指定redis指定database指定pattern的keys
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="connectionStringName"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public static async Task<List<string>> ScanKeysAsync(RedisValue pattern = default, string connectionStringName = null, int database = -1)
        {
            var ret = new List<string>();
            foreach (var endPoint in GetEndPoints(connectionStringName))
            {
                var server = GetRedis(connectionStringName).GetServer(endPoint);
                var keys = server.KeysAsync(database, pattern);
                await foreach (var key in keys)
                    ret.Add(key.ToString());
            }
            return ret;
        }

        /// <summary>
        /// 获得服务器节点信息
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static EndPoint[] GetEndPoints(string connectionStringName = null)
            => GetRedis(connectionStringName).GetEndPoints();
        public static EndPoint[] GetEndPointsByConnectionString(string connectionString)
            => GetRedisByConnectionString(connectionString).GetEndPoints();
        /// <summary>
        /// 获取redis server，以便于使用服务器命令
        /// </summary>
        /// <param name="hostAndPort"></param>
        /// <param name="asyncState"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static IServer GetServer(string hostAndPort, object? asyncState = null, string connectionStringName = null)
            => GetRedis(connectionStringName).GetServer(hostAndPort, asyncState);
        public static IServer GetServerByConnectionString(string connectionString, string hostAndPort, object? asyncState = null)
            => GetRedisByConnectionString(connectionString).GetServer(hostAndPort, asyncState);

        internal static RedisClientOptions GetOptions(string connectionStringName = null, Type type = null)
        {
            var element = GetConfigElement(connectionStringName, type);
            return new RedisClientOptions
            {
                ConnectionStringName = element.Name,
                ConnectionString = element.ConnectionString,
                SerializeMode = element.SerializeMode
            };
        }

        internal static ISerializer GetSerializer(RedisSerializeMode serializer)
        {
            ISerializer ret = null;
            switch (serializer)
            {
                case RedisSerializeMode.Json:
                    ret = _jsonSerializer;
                    break;
                case RedisSerializeMode.Bytes:
                    ret = _bytesSerializer;
                    break;
                //case RedisSerializeMode.MemoryPack:
                //    ret = _memoryPackSerializer;
                //    break;
                default:
                    throw new Exception("仅支持json序列化");
            }
            return ret;
        }

        internal static void ReleaseAllRedis()
        {
            _redisDict.ForEach(x => x.Value.Dispose());
        }
        #endregion 
    }
}
