using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis.Serializers;
using StackExchange.Redis.KeyspaceIsolation;

namespace TinyFx.Extensions.StackExchangeRedis
{
    /// <summary>
    /// Redis辅助类，获取：
    ///     IDatabase:      操作Redis底层类，包含全部操作
    ///     RedisClient:    操作Redis高层类，针对Redis类型进行了封装
    /// </summary>
    public static class RedisUtil
    {
        // key: ConfigString
        private static readonly ConcurrentDictionary<string, ConnectionMultiplexer> _multiplexers = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        private static readonly ISerializer _jsonSerializer = new RedisJsonSerializer();
        //private static readonly ISerializer _bytesSerializer = new RedisDefaultBytesSerializer();

        #region Create
        /// <summary>
        /// 获得基础Redis操作类IDatabase
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <param name="databaseIndex"></param>
        /// <returns></returns>
        public static IDatabase GetDatabase(string connectionStringName = null, int databaseIndex = -1)
        {
            var element = GetConfigElement(connectionStringName);
            return GetMultiplexer(element.ConnectionString).GetDatabase(databaseIndex);
        }
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
            _typeNameCache.TryAdd(type, typeName);
            return typeName;
        }
        #endregion

        #region Utils
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
        internal static ConnectionStringElement GetConfigElement(string connectionStringName = null, Type type = null)
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
            if (!section.ConnectionStrings.TryGetValue(connectionStringName, out ConnectionStringElement config))
                throw new Exception($"Redis配置Redis:ConnectionStrings:Name不存在。Name:{connectionStringName}");
            if (string.IsNullOrEmpty(config.ConnectionString))
                throw new Exception($"Redis配置Redis:ConnectionStrings:ConnectionString不能为空。Name:{config.Name}");
            return config;
        }
        internal static ConnectionMultiplexer GetMultiplexer(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");
            return _multiplexers.GetOrAdd(connectionString, (key) =>
            {
                return ConnectionMultiplexer.Connect(connectionString);
            });
        }
        internal static ISerializer GetSerializer(RedisSerializeMode serializer)
        {
            ISerializer ret = null;
            switch (serializer)
            {
                case RedisSerializeMode.Json:
                    ret = _jsonSerializer;
                    break;
                //case RedisSerializeMode.DefaultBytes:
                //    ret = _bytesSerializer;
                //    break;
                default:
                    throw new Exception("仅支持json序列化");
            }
            return ret;
        }
        #endregion 
    }
}
