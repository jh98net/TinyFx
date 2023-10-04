using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.Extensions.RabbitMQ
{
    internal class SubscribeOrderService
    {
        private string _connectionStringName { get; }
        private Type _messageType { get; }
        public SubscribeOrderService(Type messageType)
        {
            var section = ConfigUtil.GetSection<RabbitMQSection>();
            _connectionStringName = section!.RedisConnectionStringName;
            _messageType = messageType;
        }

        #region QueueCount
        public async Task<int> GetQueueCount()
        {
            var redisKey = GetQueueCountRedisKey();
            var client = RedisUtil.CreateStringClient<int>(redisKey, _connectionStringName);
            return await client.GetOrDefaultAsync(0);
        }
        public async Task SetQueueCount(int count)
        {
            var redisKey = GetQueueCountRedisKey();
            var client = RedisUtil.CreateStringClient<int>(redisKey, _connectionStringName);
            var curr = await client.GetOrDefaultAsync(0);
            if (curr > 0)
            {
                // 取小的
                count = Math.Min(count, curr);
            }
            await client.SetAsync(count);
        }
        private string _queueCountRedisKey;
        private string GetQueueCountRedisKey()
            => _queueCountRedisKey ??= GetRedisKey("QueueCount");
        #endregion

        #region QueueIndex
        public async Task<long> GetQueueIndex()
        {
            var redisKey = GetQueueIndexRedisKey();
            var client = RedisUtil.CreateStringClient<long>(redisKey, _connectionStringName);
            var ret = await client.IncrementAsync(1);
            if (ret > long.MaxValue - 1000)
            {
                await client.SetAsync(0);
                ret = 0;
            }
            return ret;
        }
        private string _queueIndexRedisKey;
        private string GetQueueIndexRedisKey()
            => _queueIndexRedisKey ??= GetRedisKey("QueueIndex");
        private string GetRedisKey(string flag)
        {
            var ret = _messageType.FullName;
            var idx = ret.LastIndexOf('.');
            if (idx >= 0)
                ret = ret.Substring(idx + 1);
            return $"_MQSubOrders:{ret}:{flag}";
        }
        #endregion

    }

}
