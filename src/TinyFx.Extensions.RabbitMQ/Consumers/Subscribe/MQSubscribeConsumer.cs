using EasyNetQ;
using EasyNetQ.Logging;
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
    public abstract class MQSubscribeConsumer<TMessage> : BaseMQConsumer, IMQSubscribeConsumer, IMQSubscribeSetQueueIndex
        where TMessage : class, new()
    {
        #region Properties
        /// <summary>
        /// 订阅模式
        /// </summary>
        public abstract MQSubscribeMode SubscribeMode { get; }
        public Type MQMessageType => typeof(TMessage);
        /// <summary>
        /// 仅对MultiQueue和SAC有效
        /// </summary>
        public virtual int QueueCount { get; } = 2;
        /// <summary>
        /// 仅对MultiQueue和SAC有效
        /// </summary>
        public int QueueIndex { get; private set; }
        public void SetQueueIndex(int value)
        {
            QueueIndex = value;
        }

        /// <summary>
        /// 是否启用高可用(仅MQ群集使用)
        /// </summary>
        protected virtual bool UseQuorum { get; set; }

        public bool IsRegisted { get; private set; }
        private SubscriptionResult? _subResult;
        public MQSubscribeConsumer()
        {
        }
        protected override string GetConnectionStringName()
        {
            return MQUtil.GetMessageAttribute<MQPublishMessageAttribute>(typeof(TMessage))
                    ?.ConnectionStringName;
        }
        #endregion

        public override async Task Register()
        {
            var subId = GetSubscriptionId();
            var configAction = GetConfigAction();
            var onMessage = GetOnMessageFunc();

            if (SubscribeMode == MQSubscribeMode.Multicast)
            {
                _subResult = await Bus.PubSub.SubscribeAsync(subId, onMessage, configAction);
                Bus.Advanced.Connected += (_, e) =>
                {
                    _subResult = Bus.PubSub.Subscribe(subId, onMessage, configAction);
                };
            }
            else
            {
                _subResult = await Bus.PubSub.SubscribeAsync(subId, onMessage, configAction);
            }
            IsRegisted = true;
        }

        protected string GetSubscriptionId()
        {
            switch (SubscribeMode)
            {
                case MQSubscribeMode.OneQueue:
                    return $"{GetType().FullName}";
                case MQSubscribeMode.Multicast:
                    return $"{GetType().FullName}-MC-{StringUtil.GetGuidString()}";
                case MQSubscribeMode.MultiQueue:
                case MQSubscribeMode.SAC:
                    return $"{GetType().FullName}-Q{QueueIndex}";
            }
            throw new NotImplementedException();
        }
        private Action<ISubscriptionConfiguration> GetConfigAction()
        {
            return (x) =>
            {
                if (SubscribeMode == MQSubscribeMode.Multicast)
                {
                    x.WithAutoDelete(true);
                }
                if (UseQuorum && ConnectionStringElement.UseQuorum)
                {
                    if (SubscribeMode == MQSubscribeMode.Multicast)
                        throw new Exception($"SubscribeConsumer的IsBroadcast=true时无法启用UseQuorum.type:{GetType().FullName}");
                    x.WithQueueType(QueueType.Quorum);
                }
                Configuration(x);
                if (SubscribeMode == MQSubscribeMode.MultiQueue || SubscribeMode == MQSubscribeMode.SAC)
                    x.WithTopic($"hash.{QueueIndex}");
                if (SubscribeMode == MQSubscribeMode.SAC)
                    x.WithSingleActiveConsumer();//单一消费者
            };
        }
        protected virtual Func<TMessage, CancellationToken, Task> GetOnMessageFunc()
        {
            return async (msg, cancellationToken) =>
            {
                var tmpMsg = msg as IMQMessage;
                try
                {
                    await OnMessage(msg, cancellationToken);
                    if (DIUtil.GetService<RabbitMQSection>()?.LogEnabled ?? false)
                    {
                        GetLogger().AddMessage("[MQ] SubscribeConsumer消费成功。")
                            .AddField("MQMessageId", tmpMsg?.MQMeta?.MessageId)
                            .AddField("MQMessageBody", SerializerUtil.SerializeJson(msg))
                            .AddField("MQElaspedTime", GetElaspedTime(tmpMsg?.MQMeta?.Timestamp))
                            .Save();
                    }
                }
                catch (Exception ex)
                {
                    GetLogger().AddMessage("[MQ] SubscribeConsumer消费异常。")
                        .AddField("MQMessageId", tmpMsg?.MQMeta?.MessageId)
                        .AddField("MQMessageBody", SerializerUtil.SerializeJson(msg))
                        .AddField("MQElaspedTime", GetElaspedTime(tmpMsg?.MQMeta?.Timestamp))
                        .AddException(ex)
                        .Save();

                    // 不要catch，此异常将导致被发送到默认错误代理队列 error queue (broker)
                    throw new EasyNetQException($"SubscribeConsumer消费异常。ConsumerType:{GetType().FullName} MessageId:{tmpMsg?.MQMeta?.MessageId}");
                }
            };
        }
        protected ILogBuilder GetLogger()
        {
            var logger = new LogBuilder()
              .AddField("MQConsumerType", GetType().FullName)
              .AddField("MQMessageType", MQMessageType.FullName)
              .AddField("MQSubscribeMode", SubscribeMode)
              .AddField("MQQueueCount", QueueCount)
              .AddField("MQSubId", GetSubscriptionId());
            return logger;
        }
        /// <summary>
        /// 收到消息处理函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task OnMessage(TMessage message, CancellationToken cancellationToken);
        /// <summary>
        /// 配置设置（topic等）
        /// </summary>
        /// <param name="x"></param>
        protected abstract void Configuration(ISubscriptionConfiguration x);

        public override void Dispose()
        {
            _subResult?.Dispose();
            base.Dispose();
        }
    }
}
