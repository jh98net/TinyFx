using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.Extensions.RabbitMQ
{
    internal class SubscribeQueueService
    {
        private string _connectionStringName { get; }
        private Type _messageType { get; }
        /// <summary>
        /// QueueCount内存缓存过期时间毫秒。默认5秒
        /// </summary>
        public int QueueCountTimeout { get; set; } = 5000;

        public SubscribeQueueService(Type messageType)
        {
            var section = ConfigUtil.GetSection<RabbitMQSection>();
            _connectionStringName = section!.RedisConnectionStringName;
            _messageType = messageType;
        }

        #region QueueCount
        private int _queueCount = 1;
        private long _lastTimestamp = 0;
        public async Task<int> GetQueueCount(bool useCache = true)
        {
            // 缓存
            if (useCache && DateTime.UtcNow.UtcDateTimeToTimestamp(false) - _lastTimestamp < QueueCountTimeout)
                return _queueCount;

            var redisKey = GetQueueCountRedisKey();
            var client = RedisUtil.CreateStringClient<int>(redisKey, _connectionStringName);
            _queueCount = await client.GetOrDefaultAsync(1);
            _lastTimestamp = DateTime.UtcNow.UtcDateTimeToTimestamp(false);
            return _queueCount;
        }
        public async Task SetQueueCount(int count)
        {
            var redisKey = GetQueueCountRedisKey();
            var client = RedisUtil.CreateStringClient<int>(redisKey, _connectionStringName);
            await client.SetAsync(count);
        }
        private string _queueCountRedisKey;
        private string GetQueueCountRedisKey()
            => _queueCountRedisKey ??= GetRedisKey("QueueCount");
        #endregion

        #region QueueIndex
        public async Task<long> GetQueueIndex()
        {
            var redisKey = GetRedisKey("QueueIndex");
            var client = RedisUtil.CreateStringClient<long>(redisKey, _connectionStringName);
            var ret = await client.IncrementAsync(1);
            return ret;
        }
        private string GetRedisKey(string flag)
        {
            var ret = _messageType.FullName;
            var idx = ret.LastIndexOf('.');
            if (idx >= 0)
                ret = ret.Substring(idx + 1);
            return $"_MQSubQueue:{ret}:{flag}";
        }
        #endregion

    }

}
