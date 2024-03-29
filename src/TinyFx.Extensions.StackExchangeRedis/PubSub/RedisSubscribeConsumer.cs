﻿using Google.Protobuf;
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
    /// redis发布订阅的消费基类(队列消息将多播发送)
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public abstract class RedisSubscribeConsumer<TMessage> : IRedisConsumer, IDisposable
         where TMessage : class
    {
        private ISubscriber _sub;
        private RedisChannel _channel;
        private ChannelMessageQueue _queue;

        public virtual string ConnectionStringName { get; set; }
        /// <summary>
        /// 模式匹配，如: *
        /// </summary>
        public virtual string PatternKey { get; set; }
        public virtual PatternMode? PatternMode { get; set; }
        /// <summary>
        /// 消息是否并发处理
        /// </summary>
        public virtual bool IsConcurrentProcess { get; set; } = true;

        public RedisSubscribeConsumer()
        {
            var attr = typeof(TMessage).GetCustomAttribute<RedisPublishMessageAttribute>();
            if (string.IsNullOrEmpty(ConnectionStringName))
                ConnectionStringName = attr?.ConnectionStringName;
            if (!PatternMode.HasValue)
                PatternMode = attr?.PatternMode ?? RedisChannel.PatternMode.Auto;

        }
        public void Register()
        {
            _sub = RedisUtil.GetRedis(ConnectionStringName).GetSubscriber();
            _channel = RedisUtil.GetChannel<TMessage>(PatternKey, PatternMode.Value);
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
                LogUtil.Error(ex, $"RedisSubscribeConsumer.OnMessage异常。type:{this.GetType().FullName}");
                await OnError(msg, ex);
            }
        }

        protected abstract Task OnMessage(TMessage message);
        protected abstract Task OnError(TMessage message, Exception ex);

        public void Dispose()
        {
            
        }
    }
}
