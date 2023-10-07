using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Logging;
using TinyFx.Text;

namespace TinyFx.Extensions.RabbitMQ.Consumers
{
    /// <summary>
    /// MQ消息订阅(Publish => Subscribe)模式的顺序执行的Subscribe基类，可支持负载和高可用
    /// 注意服务实例数量不能小于Queue数量
    /// 用于接收使用MQUtil.Publish方法发布的MQ消息
    /// 继承的子类名建议使用MQSub结尾
    /// </summary>
    /// <typeparam name="TMessage">接收的消息类型</typeparam>
    public abstract class MQSubscribeOrderConsumer<TMessage> : MQConsumerBase, IMQSubscribeOrder
        where TMessage : IMQMessage, new()
    {
        /// <summary>
        /// 队列数量
        /// </summary>
        public abstract int QueueCount { get; }
        /// <summary>
        /// 是否注册成为单一消费者Single Active Consumer
        /// true:  同时生成多个consumer并注册到多个queue，同一时间只有一个consumer有效,可以保证消息被顺序执行
        /// false: 同时生成多个consumer并注册到多个queue，同一时间多个consumer同时有效，可以使消息并发执行
        /// </summary>
        protected virtual bool IsRegisterSAC { get; set; } = true;
        /// <summary>
        /// 是否启用高可用(仅MQ群集使用)
        /// </summary>
        protected virtual bool UseQuorum { get; set; }

        private string _subscriptionId;
        public string SubscriptionId
        {
            get
            {
                if (string.IsNullOrEmpty(_subscriptionId))
                {
                    _subscriptionId = $"{GetType().FullName}_{QueueIndex}";
                }
                return _subscriptionId;
            }
        }
        public Type GetMessageType()
            => typeof(TMessage);
        public int QueueIndex;

        public void SetQueueIndex(int value)
        {
            QueueIndex = value;
        }

        private SubscriptionResult? _subResult;
        public MQSubscribeOrderConsumer()
        {
        }
        protected override string GetConnectionStringName()
        {
            return MQUtil.GetMessageAttribute<MQPublishMessageAttribute>(typeof(TMessage))
                    ?.ConnectionStringName;
        }
        public bool IsRegisted { get; private set; }
        public override async Task Register()
        {
            Func<TMessage, CancellationToken, Task> onMessage = (msg, cancellationToken) =>
            {
                return OnMessage(msg, cancellationToken).ContinueWith(task =>
                {
                    if (task.IsCompleted && !task.IsFaulted)
                    {
                        LogUtil.Debug("[MQ] MQSubscribeOrderConsumer消费成功。{MQConsumerType}{MQMessageType}{MQMessageId}{MQElaspedTime}"
                            , GetType().FullName, msg.GetType().FullName, msg.MessageId, GetElaspedTime(msg.Timestamp));
                    }
                    else
                    {
                        LogUtil.Error(task.Exception, "[MQ] MQSubscribeOrderConsumer消费异常。{MQConsumerType}{MQMessageBody}{MQSubId}{MQMessageId}{MQElaspedTime}"
                            , GetType().FullName, SerializerUtil.SerializeJson(msg), SubscriptionId, msg.MessageId, GetElaspedTime(msg.Timestamp));
                        // 不要catch，此异常将导致被发送到默认错误代理队列 error queue (broker)
                        throw new EasyNetQException($"MQSubscribeOrderConsumer消费异常。ConsumerType:{GetType().FullName} MessageId:{msg.MessageId}");
                    }
                });
            };
            Action<ISubscriptionConfiguration> configureAction = (x) =>
            {
                if (UseQuorum && ConnectionStringElement.UseQuorum)
                {
                    x.WithQueueType(QueueType.Quorum);
                }
                Configuration(x);
                x.WithTopic($"hash.{QueueIndex}");
                if(IsRegisterSAC)
                    x.WithSingleActiveConsumer();//单一消费者
            };
            _subResult = await Bus.PubSub.SubscribeAsync(SubscriptionId, onMessage, configureAction);
            IsRegisted = true;
        }

        protected abstract Task OnMessage(TMessage message, CancellationToken cancellationToken);

        /// <summary>
        /// 配置设置（主要考虑设置topic）
        /// </summary>
        /// <param name="x"></param>
        protected abstract void Configuration(ISubscriptionConfiguration x);
        public override void Dispose()
        {
            base.Dispose();
            _subResult?.Dispose();
        }
    }
    public interface IMQSubscribeOrder
    {
        Type GetMessageType();
        int QueueCount { get; }
        void SetQueueIndex(int value);
        bool IsRegisted { get; }
        Task Register();
    }
}
