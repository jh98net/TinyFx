using Google.Protobuf;
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
    public interface IRedisSubscribeConsumer
    { 
    }
    /// <summary>
    /// redis发布订阅的消费基类(队列消息将广播执行)
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public abstract class RedisSubscribeConsumer<TMessage>: IRedisSubscribeConsumer
         where TMessage : class
    {
        public virtual string ConnectionStringName { get; }
        public virtual PatternMode PatternMode { get; } = PatternMode.Auto;
        /// <summary>
        /// 消息是否并发处理
        /// </summary>
        public virtual bool IsConcurrentProcess { get; } = true;

        private ISubscriber _sub;
        private RedisChannel _channel;
        private ChannelMessageQueue _queue;

        public RedisSubscribeConsumer()
        {
            var attr = typeof(TMessage).GetCustomAttribute<RedisPublishMessageAttribute>();
            ConnectionStringName = attr?.ConnectionStringName;
            PatternMode = attr?.PatternMode ?? PatternMode.Auto;


            _sub = RedisUtil.GetRedis(ConnectionStringName).GetSubscriber();
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
        }
        private async Task ExecMessage(RedisValue channelMessage)
        {
            var msg = await RedisUtil.GetSerializer(RedisSerializeMode.Json)
               .DeserializeAsync<TMessage>(channelMessage);
            try
            {
                await OnMessage(msg);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, "RedisSubscribeConsumer.OnMessage异常。type:{0}", this.GetType().FullName);
                await OnError(msg, ex);
            }
        }

        protected abstract Task OnMessage(TMessage message);
        protected abstract Task OnError(TMessage message, Exception ex);
    }
}
