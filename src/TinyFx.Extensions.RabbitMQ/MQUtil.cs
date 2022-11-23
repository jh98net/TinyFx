using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using EasyNetQ.ConnectionString;
using EasyNetQ.DI;
using IConnectionFactory = RabbitMQ.Client.IConnectionFactory;
using TinyFx.Configuration;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Concurrent;
using EasyNetQ.Logging;
using Microsoft.Extensions.Logging;
using TinyFx.Net;

namespace TinyFx.Extensions.RabbitMQ
{
    /// <summary>
    /// MQ辅助类，需要UseRabbitMQEx()注册
    ///     MQUtil发布 => IConsumer子类消费
    /// </summary>
    public static class MQUtil
    {
        internal static MQContainer _container => DIUtil.GetRequiredService<MQContainer>();

        #region Methods
        public static IBus CreateBus(string connectionName = null)
            => _container.GetBus(connectionName);

        public static MQMessageAttribute GetMessageAttribute<T>()
            => _container.GetMessageAttribute<T>();
        /// <summary>
        /// 取消释放MQ消费者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool DisposeConsumer<T>()
            where T : IConsumer, new()
            => _container.DisposeConsumer<T>();
        #endregion

        #region Publish => Subscribe （消费类继承SubscribeConsumer）
        public static void Publish<TMessage>(TMessage message, Action<IPublishConfiguration> configAction = null)
            where TMessage : class
        {
            var attr = _container.GetMessageAttribute<TMessage>();
            if (configAction == null)
                configAction = GetPublishConfiguration(attr);
            _container.GetBus(attr.ConnectionStringName)
                .PubSub.Publish(message, configAction);
        }
        public static Task PublishAsync<TMessage>(TMessage message, Action<IPublishConfiguration> configAction=null) 
            where TMessage : class
        {
            var attr = _container.GetMessageAttribute<TMessage>();
            if (configAction == null)
                configAction = GetPublishConfiguration(attr);
            return _container.GetBus(attr.ConnectionStringName)
                .PubSub.PublishAsync(message, configAction);
        }
        private static Action<IPublishConfiguration> GetPublishConfiguration(MQMessageAttribute attr)
        {
            return (c) => {
                if (attr.Expires != 0)
                    c.WithExpires(TimeSpan.FromMilliseconds(attr.Expires));
                if (attr.Priority != 0)
                    c.WithPriority(attr.Priority);
                if (!string.IsNullOrEmpty(attr.Topic))
                    c.WithTopic(attr.Topic);
            };
        }
        #endregion

        #region Request => Respond（消费类继承RespondConsumer）
        public static ResponseResult<TResponse> Request<TMessage, TResponse>(TMessage message, Action<IRequestConfiguration> configAction = null)
        {
            var attr = _container.GetMessageAttribute<TMessage>();
            if (configAction == null)
                configAction = GetRequestConfiguration(attr);
            return _container.GetBus(attr.ConnectionStringName)
                .Rpc.Request<TMessage, ResponseResult<TResponse>>(message, configAction);
        }
        public static Task<ResponseResult<TResponse>> RequestAsync<TMessage, TResponse>(TMessage message, Action<IRequestConfiguration> configAction = null)
        {
            var attr = _container.GetMessageAttribute<TMessage>();
            if (configAction == null)
                configAction = GetRequestConfiguration(attr);
            return _container.GetBus(attr.ConnectionStringName)
                .Rpc.RequestAsync<TMessage, ResponseResult<TResponse>>(message, configAction);
        }
        private static Action<IRequestConfiguration> GetRequestConfiguration(MQMessageAttribute attr)
        {
            return (c) => {
                if (attr.Expires != 0)
                    c.WithExpiration(TimeSpan.FromMilliseconds(attr.Expires));
                if (attr.Priority != 0)
                    c.WithPriority(attr.Priority);
                if (!string.IsNullOrEmpty(attr.QueueName))
                    c.WithQueueName(attr.QueueName);
            };
        }
        #endregion

        #region Send => Receive（消费类继承ReceiveConsumer）
        
        public static void Send<T>(T message, string queue=null)
            where T : class
        {
            queue = GetSendQueueName<T>(queue, out var attr);
            _container.GetBus(attr.ConnectionStringName)
                .SendReceive.Send(queue, message);
        }
        public static Task SendAsync<T>(T message, string queue = null)
             where T : class
        {
            queue = GetSendQueueName<T>(queue, out var attr);
            return _container.GetBus(attr.ConnectionStringName)
                .SendReceive.SendAsync(queue, message);
        }
        internal static string GetSendQueueName<T>(string queue, out MQMessageAttribute attr)
        {
            attr = _container.GetMessageAttribute<T>();
            queue ??= attr.QueueName;
            if (string.IsNullOrEmpty(queue))
                throw new Exception($"Send => Receive模式，消息类必须定义MQMessageAttribute属性QueueName");
            return queue;
        }
        #endregion
    }
}