﻿using EasyNetQ;
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
         where TMessage : class, IMQMessage, new()
    {
        public override MQSubscribeMode SubscribeMode => MQSubscribeMode.OneQueue;
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
            return (msg, cancellationToken) => OnMessage(msg, cancellationToken);
        }
        protected override async Task OnMessage(TMessage message, CancellationToken cancellationToken)
        {
            var republish = !string.IsNullOrEmpty(message.MQMeta.ErrorAction);
            // 重试
            if (republish) 
            {
                var item = ActionList.Find(x => x.Name == message.MQMeta.ErrorAction);
                if (item == null)
                    return;
                try
                {
                    await TinyFxUtil.RetryExecuteAsync(async () =>
                    {
                        await item.Action(message, cancellationToken);
                    }, RetryCount, RetryInterval);
                    if (DIUtil.GetService<RabbitMQSection>()?.LogEnabled ?? false)
                    {
                        LogUtil.Debug("[MQ] SubscribeConsumer重新消费成功。{MQConsumerType}{MQMessageType}{MQMessageId}"
                            , GetType().FullName, MQMessageType.FullName, message?.MQMeta?.MessageId);
                    }
                }
                catch (Exception ex)
                {
                    var err = new MQSubActionError
                    {
                        MessageType = MQMessageType.FullName,
                        ConsumerType = this.GetType().Name,
                        MessageData = SerializerUtil.SerializeJson(message),

                        ProjectId = ConfigUtil.Project.ProjectId,
                        MessageId = message.MQMeta.MessageId,
                        ErrorAction = item.Name,
                        Exception = ex
                    };
                    LogUtil.Error(ex, "[MQ] SubscribeConsumer重新消费异常。{MQConsumerType}{MQMessageBody}{MQSubId}{MQMessageId}"
                       , GetType().FullName, SerializerUtil.SerializeJson(message), GetSubscriptionId(), message?.MQMeta?.MessageId);
                    await OnError(err);
                }
            }
            else
            {
                foreach (var item in ActionList)
                {
                    try
                    {
                        await TinyFxUtil.RetryExecuteAsync(async () =>
                        {
                            await item.Action(message, cancellationToken);
                        }, RetryCount, RetryInterval);
                        if (DIUtil.GetService<RabbitMQSection>()?.LogEnabled ?? false)
                        {
                            LogUtil.Debug("[MQ] SubscribeConsumer消费成功。{MQConsumerType}{MQMessageType}{MQMessageId}{MQElaspedTime}"
                                , GetType().FullName, MQMessageType.FullName, message?.MQMeta?.MessageId, GetElaspedTime(message?.MQMeta?.Timestamp));
                        }
                    }
                    catch (Exception ex)
                    {
                        message.MQMeta.ErrorActionList.Add(item.Name);
                        message.MQMeta.ErrorAction = item.Name;
                        var err = new MQSubActionError
                        {
                            MessageType = MQMessageType.FullName,
                            ConsumerType = this.GetType().Name,
                            MessageData = SerializerUtil.SerializeJson(message),

                            ProjectId = ConfigUtil.Project.ProjectId,
                            MessageId = message.MQMeta.MessageId,
                            ErrorAction = item.Name,
                            Exception = ex
                        };
                        LogUtil.Error(ex, "[MQ] SubscribeConsumer消费异常。{MQConsumerType}{MQMessageBody}{MQSubId}{MQMessageId}{MQElaspedTime}"
                            , GetType().FullName, SerializerUtil.SerializeJson(message), GetSubscriptionId(), message?.MQMeta?.MessageId, GetElaspedTime(message?.MQMeta?.Timestamp));
                        await OnError(err);
                    }
                }
            }
        }
        protected abstract Task OnError(MQSubActionError error);
        public class MQSubAction
        {
            public string Name { get; set; }
            public Func<TMessage, CancellationToken, Task> Action { get; set; }
        }
    }

    public class MQSubActionError
    {
        public string ConsumerType { get; set; }
        public string MessageType { get; set; }
        public string MessageData { get; set; }

        public string MessageId { get; set; }
        public string ErrorAction { get; set; }
        public Exception Exception { get; set; }
        public string ProjectId { get; set; }
    }

}
