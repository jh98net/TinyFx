using EasyNetQ;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;

namespace TinyFx.Extensions.RabbitMQ
{
    public abstract class MQSubActionConsumer<TMessage> : MQSubscribeConsumer<TMessage>
         where TMessage : class, new()
    {
        public override MQSubscribeMode SubscribeMode => MQSubscribeMode.OneQueue;
        private List<MQSubAction> ActionList = new();
        protected void RegisterAction(Func<TMessage, CancellationToken, Task> action, string desc = null)
        {
            ActionList.Add(new MQSubAction
            {
                Action = action,
                MethodName = action.Method.Name,
                Description = desc
            });
        }
        protected override Func<TMessage, CancellationToken, Task> GetOnMessageFunc()
        {
            return (msg, cancellationToken) => OnMessage(msg, cancellationToken);
        }
        protected override async Task OnMessage(TMessage message, CancellationToken cancellationToken)
        {
            var msg = message as IMQMessage;
            var republish = !string.IsNullOrEmpty(msg?.MQMeta?.ErrorAction);
            // 重试
            if (republish)
            {
                msg.MQMeta.RepublishCount++;
                var item = ActionList.Find(x => x.MethodName == msg.MQMeta.ErrorAction);
                if (item == null)
                    return;
                try
                {
                    await item.Action(message, cancellationToken);
                    if (DIUtil.GetService<RabbitMQSection>()?.LogEnabled ?? false)
                    {
                        GetLogger().AddMessage("[MQ] SubscribeConsumer消费成功。")
                           .AddField("MQMessageId", msg?.MQMeta?.MessageId)
                           .AddField("MQMessageBody", SerializerUtil.SerializeJson(msg))
                           .AddField("MQElaspedTime", GetElaspedTime(msg?.MQMeta?.Timestamp))
                           .AddField("MQRepublish", republish)
                           .AddField("MQRepublishCount", msg.MQMeta.RepublishCount)
                           .AddField("MQActionName", item.MethodName)
                           .AddField("MQActionDesc", item.Description)
                           .Save();
                    }
                }
                catch (Exception ex)
                {
                    GetLogger().AddMessage("[MQ] SubscribeConsumer消费异常。")
                       .AddField("MQMessageId", msg?.MQMeta?.MessageId)
                       .AddField("MQMessageBody", SerializerUtil.SerializeJson(msg))
                       .AddField("MQElaspedTime", GetElaspedTime(msg?.MQMeta?.Timestamp))
                       .AddField("MQRepublish", republish)
                       .AddField("MQRepublishCount", msg.MQMeta.RepublishCount)
                       .AddField("MQActionName", item.MethodName)
                       .AddField("MQActionDesc", item.Description)
                       .AddException(ex)
                       .Save();
                    var err = new MQSubActionError
                    {
                        MessageType = MQMessageType.FullName,
                        ConsumerType = this.GetType().Name,
                        MessageData = SerializerUtil.SerializeJson(message),

                        ProjectId = ConfigUtil.Project.ProjectId,
                        MessageId = msg.MQMeta.MessageId,
                        ErrorAction = item.MethodName,
                        RepublishCount = msg.MQMeta.RepublishCount,
                        Description = item.Description,
                        Exception = ex
                    };
                    await OnError(err);
                }
            }
            else
            {
                foreach (var item in ActionList)
                {
                    try
                    {
                        await item.Action(message, cancellationToken);
                        if (DIUtil.GetService<RabbitMQSection>()?.LogEnabled ?? false)
                        {
                            GetLogger().AddMessage("[MQ] SubscribeConsumer消费成功。")
                               .AddField("MQMessageId", msg?.MQMeta?.MessageId)
                               .AddField("MQMessageBody", SerializerUtil.SerializeJson(msg))
                               .AddField("MQElaspedTime", GetElaspedTime(msg?.MQMeta?.Timestamp))
                               .AddField("MQRepublish", republish)
                               .AddField("MQRepublishCount", msg.MQMeta.RepublishCount)
                               .AddField("MQActionName", item.MethodName)
                               .AddField("MQActionDesc", item.Description)
                              .Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        GetLogger().AddMessage("[MQ] SubscribeConsumer消费异常。")
                           .AddField("MQMessageId", msg?.MQMeta?.MessageId)
                           .AddField("MQMessageBody", SerializerUtil.SerializeJson(msg))
                           .AddField("MQElaspedTime", GetElaspedTime(msg?.MQMeta?.Timestamp))
                           .AddField("MQRepublish", republish)
                           .AddField("MQRepublishCount", msg.MQMeta.RepublishCount)
                           .AddField("MQActionName", item.MethodName)
                           .AddField("MQActionDesc", item.Description)
                           .AddException(ex)
                           .Save();

                        if (msg != null)
                        {
                            msg.MQMeta.ErrorAction = item.MethodName;
                        }
                        var err = new MQSubActionError
                        {
                            MessageType = MQMessageType.FullName,
                            ConsumerType = this.GetType().Name,
                            MessageData = SerializerUtil.SerializeJson(message),

                            ProjectId = ConfigUtil.Project.ProjectId,
                            MessageId = msg?.MQMeta?.MessageId,
                            ErrorAction = item.MethodName,
                            Description = item.Description,
                            Exception = ex
                        };
                        await OnError(err);
                    }
                }
            }
        }
        protected abstract Task OnError(MQSubActionError error);
        public class MQSubAction
        {
            public string MethodName { get; set; }
            public string Description { get; set; }
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
        public int RepublishCount { get; set; }

        public string Description { get; set; }
        public Exception Exception { get; set; }
        public string ProjectId { get; set; }
    }

}
