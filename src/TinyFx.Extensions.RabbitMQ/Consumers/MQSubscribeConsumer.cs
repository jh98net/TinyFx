using EasyNetQ;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Text;

namespace TinyFx.Extensions.RabbitMQ
{
    /// <summary>
    /// MQ消息订阅(Publish => Subscribe)模式的Subscribe基类
    /// 用于接收使用MQUtil.Publish方法发布的MQ消息
    /// 继承的子类名建议使用MQSub结尾
    /// </summary>
    /// <typeparam name="TMessage">接收的消息类型</typeparam>
    public abstract class MQSubscribeConsumer<TMessage> : MQConsumerBase
        where TMessage : class, new()
    {
        /// <summary>
        /// true: 广播模式(断联删除queue) false: 负载模式
        /// </summary>
        protected virtual bool IsBroadcast { get; } // false轮播
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
                    _subscriptionId = IsBroadcast
                        ? $"{GetType().FullName}_{ObjectId.NewId()}"
                        : $"{GetType().FullName}";
                }
                return _subscriptionId;
            }
        }
        private SubscriptionResult? _subResult;
        public MQSubscribeConsumer()
        {
        }
        protected override string GetConnectionStringName()
        {
            return MQUtil.GetMessageAttribute<MQPublishMessageAttribute>(typeof(TMessage))
                    ?.ConnectionStringName;
        }
        public override async Task Register()
        {
            Func<TMessage, CancellationToken, Task> onMessage = (msg, cancellationToken) =>
            {
                return OnMessage(msg, cancellationToken).ContinueWith(task =>
                {
                    var tmpMsg = msg as IMQMessage;
                    if (task.IsCompleted && !task.IsFaulted)
                    {
                        LogUtil.Debug("[MQ] SubscribeConsumer消费成功。{MQConsumerType}{MQMessageType}{MQMessageId}{MQElaspedTime}"
                            , GetType().FullName, msg.GetType().FullName, tmpMsg?.MessageId, GetElaspedTime(tmpMsg?.Timestamp));
                    }
                    else
                    {
                        LogUtil.Error(task.Exception, "[MQ] SubscribeConsumer消费异常。{MQConsumerType}{MQMessageBody}{MQSubId}{MQMessageId}{MQElaspedTime}"
                            , GetType().FullName, SerializerUtil.SerializeJson(msg), SubscriptionId, tmpMsg?.MessageId, GetElaspedTime(tmpMsg?.Timestamp));
                        // 不要catch，此异常将导致被发送到默认错误代理队列 error queue (broker)
                        throw new EasyNetQException($"SubscribeConsumer消费异常。ConsumerType:{GetType().FullName} MessageId:{tmpMsg?.MessageId}");
                    }
                });
            };
            Action<ISubscriptionConfiguration> configureAction = (x) =>
            {
                if (IsBroadcast)
                {
                    x.WithAutoDelete(true);
                }
                if (UseQuorum && ConnectionStringElement.UseQuorum)
                {
                    if (IsBroadcast)
                        throw new Exception($"SubscribeConsumer的IsBroadcast=true时无法启用UseQuorum.type:{GetType().FullName}");
                    x.WithQueueType(QueueType.Quorum);
                }
                Configuration(x);
            };
            if (IsBroadcast)
            {
                _subResult = await Bus.PubSub.SubscribeAsync(SubscriptionId, onMessage, configureAction);
                Bus.Advanced.Connected += (_, e) =>
                {
                    _subResult = Bus.PubSub.Subscribe(SubscriptionId, onMessage, configureAction);
                };
            }
            else
            {
                _subResult = await Bus.PubSub.SubscribeAsync(SubscriptionId, onMessage, configureAction);
            }
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
}
