using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Logging;
using static StackExchange.Redis.RedisChannel;

namespace TinyFx.Extensions.StackExchangeRedis
{
    /// <summary>
    /// redis队列消费基类(队列消息将被阻塞且单一执行)
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public abstract class RedisQueueConsumer<TMessage>
         where TMessage : class
    {
        public virtual string ConnectionStringName { get; }
        public virtual PatternMode PatternMode { get; } = PatternMode.Auto;
        /// <summary>
        /// 消息是否并发处理
        /// </summary>
        public virtual bool IsConcurrentProcess { get; } = true;

        private ConnectionMultiplexer _redis;
        private IDatabase _database;
        private ISubscriber _sub;
        private RedisChannel _channel;
        private ChannelMessageQueue _queue;
        private string _queueKey;

        public RedisQueueConsumer()
        {
            var attr = typeof(TMessage).GetCustomAttribute<RedisPublishMessageAttribute>();
            ConnectionStringName = attr?.ConnectionStringName;
            PatternMode = attr?.PatternMode ?? PatternMode.Auto;

            _redis = RedisUtil.GetRedis(ConnectionStringName);
            _database = _redis.GetDatabase();
            _sub = _redis.GetSubscriber();
            _channel = RedisUtil.GetRedisChannel<TMessage>(PatternMode);
            if (IsConcurrentProcess)
            {
                _sub.Subscribe(_channel, async (c, m) =>
                {
                    await ExecMessage(m);
                });
            }
            else
            {
                _queue = _sub.Subscribe(_channel);
                _queue.OnMessage((m) => ExecMessage(m.Message));
            }
            _queueKey = RedisUtil.GetQueueKey<TMessage>();
        }
        private async Task ExecMessage(RedisValue channelMessage)
        {
            var value = await _database.ListRightPopAsync(_queueKey);
            if (value.IsNullOrEmpty)
                return;
            var msg = await RedisUtil.GetSerializer(RedisSerializeMode.Json)
                .DeserializeAsync<TMessage>(value);
            try
            {
                await OnMessage(msg);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, "RedisQueueConsumer.OnMessage异常。type:{0}", this.GetType().FullName);
                await OnError(msg, ex);
            }
        }

        protected abstract Task OnMessage(TMessage message);
        protected abstract Task OnError(TMessage message, Exception ex);
    }
}
