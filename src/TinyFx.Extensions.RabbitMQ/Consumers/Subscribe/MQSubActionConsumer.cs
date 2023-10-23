using EasyNetQ;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;

namespace TinyFx.Extensions.RabbitMQ.Consumers.Subscribe
{
    public abstract class MQSubActionConsumer<TMessage> : MQSubscribeConsumer<TMessage>
         where TMessage :class, IMQMessage, new()
    {
        private List<MQSubAction> ActionList = new();
        protected void RegisterAction(Func<TMessage, CancellationToken, Task> action, string name = null)
        {
            ActionList.Add(new MQSubAction
            {
                Action = action,
                Name = name ?? action.Method.Name
            });
        }
        protected override Func<TMessage, CancellationToken, Task> GetOnMessageFunc()
        {
            return async (msg, cancellationToken) =>
            {
                var tmpMsg = msg as IMQMessage;
                try
                {
                    await TinyFxUtil.RetryExecuteAsync(async () =>
                    {
                        await OnMessage(msg, cancellationToken);
                    }, RetryCount, RetryInterval);
                    if (DIUtil.GetService<RabbitMQSection>()?.LogEnabled ?? false)
                    {
                        LogUtil.Debug("[MQ] SubscribeConsumer消费成功。{MQConsumerType}{MQMessageType}{MQMessageId}{MQElaspedTime}"
                            , GetType().FullName, msg.GetType().FullName, tmpMsg?.MessageId, GetElaspedTime(tmpMsg?.Timestamp));
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.Error(ex, "[MQ] SubscribeConsumer消费异常。{MQConsumerType}{MQMessageBody}{MQSubId}{MQMessageId}{MQElaspedTime}"
                        , GetType().FullName, SerializerUtil.SerializeJson(msg), GetSubscriptionId(), tmpMsg?.MessageId, GetElaspedTime(tmpMsg?.Timestamp));
                    // 不要catch，此异常将导致被发送到默认错误代理队列 error queue (broker)
                    throw new EasyNetQException($"SubscribeConsumer消费异常。ConsumerType:{GetType().FullName} MessageId:{tmpMsg?.MessageId}");
                }
            };
        }
        protected override Task OnMessage(TMessage message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        protected override void Configuration(ISubscriptionConfiguration x)
        {
        }
        public class MQSubAction
        {
            public string Name { get; set; }
            public Func<TMessage, CancellationToken, Task> Action { get; set; }
        }
    }

}
