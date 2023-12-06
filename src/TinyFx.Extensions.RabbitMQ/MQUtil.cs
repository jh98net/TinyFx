using EasyNetQ;
using EasyNetQ.Topology;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text;
using TinyFx.Logging;
using TinyFx.Security;
using TinyFx.Text;

namespace TinyFx.Extensions.RabbitMQ
{
    /// <summary>
    /// MQ辅助类，需要UseRabbitMQEx()注册
    ///     MQUtil发布 => IConsumer子类消费
    /// </summary>
    public static class MQUtil
    {
        internal static MQContainer _container => DIUtil.GetRequiredService<MQContainer>();
        private static ConcurrentDictionary<Type, Attribute> _messageAttrDict = new();
        internal static ConcurrentDictionary<Type, SubscribeQueueService> _subQueueSvcDict = new();

        #region Methods
        public static IBus GetBus(string connectionStringName = null)
            => GetBusData(connectionStringName).Bus;
        public static MQBusData GetBusData(string connectionStringName = null)
            => _container.GetBusData(connectionStringName);
        internal static T GetMessageAttribute<T>(object message)
            where T : Attribute
            => GetMessageAttribute<T>(message.GetType());
        internal static T GetMessageAttribute<T>(Type messageType)
            where T : Attribute
        {
            return (T)_messageAttrDict.GetOrAdd(messageType, messageType.GetCustomAttribute<T>());
        }
        #endregion

        #region Publish => Subscribe （消费类继承SubscribeConsumer）
        /// <summary>
        /// 向MQ发布Publish命令，消费类需要继承SubscribeConsumer进行消费
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="routingKey"></param>
        /// <param name="configAction"></param>
        /// <param name="connectionStringName"></param>
        public static void Publish<TMessage>(TMessage message, string routingKey = null, Action<IPublishConfiguration> configAction = null, string connectionStringName = null)
            where TMessage : new()
        {
            configAction = GetPublishAction<TMessage>(message, routingKey, configAction, connectionStringName, false)
                .GetTaskResult();
            var data = GetPubSubData(message, configAction, connectionStringName);
            GetBus(data.ConnStrName)
                .PubSub.Publish(data.Message, data.Action);
        }

        /// <summary>
        /// 向MQ发布Publish命令，消费类需要继承SubscribeConsumer进行消费
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="routingKey"></param>
        /// <param name="configAction"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static async Task PublishAsync<TMessage>(TMessage message, string routingKey = null, Action<IPublishConfiguration> configAction = null, string connectionStringName = null)
            where TMessage : new()
        {
            configAction = await GetPublishAction<TMessage>(message, routingKey, configAction, connectionStringName, false);
            var data = GetPubSubData(message, configAction, connectionStringName);
            await GetBus(data.ConnStrName)
                .PubSub.PublishAsync(data.Message, data.Action);

        }
        private static async Task<Action<IPublishConfiguration>> GetPublishAction<TMessage>(TMessage message, string routingKey, Action<IPublishConfiguration> configAction, string connectionStringName, bool isRepublish)
        {
            if (message is IMQMessage msg)
            {
                if (!isRepublish && msg.MQMeta != null)
                    throw new Exception("MQUtil.Publish时MQMeta必须为null");
                msg.MQMeta ??= new()
                {
                    MessageId = ObjectId.NewId(),
                    Timestamp = DateTime.UtcNow.UtcDateTimeToTimestamp(false)
                };
                if (!string.IsNullOrEmpty(routingKey))
                    msg.MQMeta.RoutingKey = routingKey;
                else
                    routingKey = msg.MQMeta.RoutingKey;
            }
            if (string.IsNullOrEmpty(routingKey))
            {
                return configAction;
            }
            var msgType = typeof(TMessage);
            var queueCount = await _subQueueSvcDict
                .GetOrAdd(msgType, new SubscribeQueueService(msgType))
                .GetQueueCount();
            if (queueCount > 1)
            {
                var topic = $"hash.{MurmurHash3.Hash32(routingKey) % queueCount + 1}";
                configAction = configAction == null
                    ? (c) => { c.WithTopic(topic); }
                : (c) =>
                {
                    configAction(c);
                    c.WithTopic(topic);
                };
            }
            //else
            //    LogUtil.Debug("MQUtil.Publish时，传入routingKey时不存在多个queue。{MQMessageType}", typeof(TMessage).FullName);
            return configAction;
        }
        private static (TMessage Message, Action<IPublishConfiguration> Action, string ConnStrName) GetPubSubData<TMessage>(TMessage message, Action<IPublishConfiguration> configAction, string connectionStringName = null)
        {
            var attr = GetMessageAttribute<MQPublishMessageAttribute>(message);
            var action = GetPublishConfiguration(attr, configAction);
            var connStrName = connectionStringName ?? attr?.ConnectionStringName;
            return (message, action, connStrName);
        }
        private static Action<IPublishConfiguration> GetPublishConfiguration(MQPublishMessageAttribute attr, Action<IPublishConfiguration> configAction)
        {
            return (c) =>
            {
                if (attr != null)
                {
                    if (attr.ExpireSeconds != 0)
                        c.WithExpires(TimeSpan.FromMilliseconds(attr.ExpireSeconds));
                    if (attr.Priority != 0)
                        c.WithPriority(attr.Priority);
                    if (!string.IsNullOrEmpty(attr.Topic))
                        c.WithTopic(attr.Topic);
                }
                configAction?.Invoke(c);
            };
        }
        #endregion

        #region Republish
        /// <summary>
        /// 重新发布异常的Message
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="messageJson"></param>
        /// <param name="configAction"></param>
        /// <param name="connectionStringName"></param>
        public static void Republish(string messageType, string messageJson, Action<IPublishConfiguration> configAction = null, string connectionStringName = null)
        {
            var msgType = Type.GetType(messageType);
            var msg = SerializerUtil.DeserializeJson(messageJson, msgType);
            Republish(msg, configAction, connectionStringName);
        }
        /// <summary>
        /// 重新发布异常的Message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="connectionStringName"></param>
        /// <exception cref="Exception"></exception>
        public static void Republish(object message, Action<IPublishConfiguration> configAction = null, string connectionStringName = null)
        {
            var msgType = message.GetType();
            if (!(message is IMQMessage msg))
                throw new Exception("MQUtil.Republish的Message必须继承自IMQMessage");
            if (msg.MQMeta == null || string.IsNullOrEmpty(msg.MQMeta.MessageId) || string.IsNullOrEmpty(msg.MQMeta.ErrorAction))
                throw new Exception("MQUtil.Republish时message.MQMeta.MessageId和ErrorAction不能为空");
            var method = GetGenericMethod(typeof(MQUtil), "Republish", BindingFlags.Static | BindingFlags.NonPublic
                , new Type[] { msgType, typeof(string), typeof(Action<IPublishConfiguration>), typeof(string) }
                , msgType);
            method.Invoke(null, new object[] { message, null, configAction, connectionStringName });
        }
        private static MethodInfo GetGenericMethod(Type targetType, string name, BindingFlags flags, Type[] parameterTypes, params Type[] typeArguments)
        {
            var methods = targetType.GetMethods(flags).Where(m => m.Name == name && m.IsGenericMethod);
            foreach (MethodInfo method in methods)
            {
                var parameters = method.GetParameters();
                if (parameters.Length != parameterTypes.Length)
                    continue;

                for (var i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].ParameterType != parameterTypes[i])
                        break;
                }
                return method.MakeGenericMethod(typeArguments);
            }
            return null;
        }
        //private static void Republish<TMessage>(TMessage message, string routingKey = null, Action<IPublishConfiguration> configAction = null, string connectionStringName = null)
        //    where TMessage : new()
        //{
        //    configAction = GetPublishAction<TMessage>(message, routingKey, configAction, connectionStringName, true)
        //        .GetTaskResult();
        //    var data = GetPubSubData(message, configAction, connectionStringName);
        //    GetBus(data.ConnStrName)
        //        .PubSub.Publish(data.Message, data.Action);
        //}
        #endregion

        #region SchedulePublish
        public static void SchedulePublish<TMessage>(TMessage message, TimeSpan delay, Action<IFuturePublishConfiguration> configAction = null, string connectionStringName = null)
            where TMessage : new()
        {
            var data = GetSchedulerData(message, delay, configAction, connectionStringName);
            GetBus(data.ConnStrName)
                .Scheduler.FuturePublish(data.Message, message.GetType(), delay, data.Action);
        }
        /// <summary>
        /// 发布延迟执行任务
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        /// <param name="configAction"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static Task SchedulePublishAsync<TMessage>(TMessage message, TimeSpan delay, Action<IFuturePublishConfiguration> configAction = null, string connectionStringName = null)
            where TMessage : new()
        {
            var data = GetSchedulerData(message, delay, configAction, connectionStringName);
            return GetBus(data.ConnStrName)
                .Scheduler.FuturePublishAsync(data.Message, delay, data.Action);
        }
        private static (TMessage Message, Action<IFuturePublishConfiguration> Action, string ConnStrName) GetSchedulerData<TMessage>(TMessage message, TimeSpan delay, Action<IFuturePublishConfiguration> configAction, string connectionStringName = null)
            where TMessage : new()
        {
            if (message is IMQMessage msg)
            {
                msg.MQMeta = new()
                {
                    MessageId = ObjectId.NewId(),
                    Timestamp = DateTime.UtcNow.UtcDateTimeToTimestamp(false),
                    Delay = delay
                };
            }
            var attr = GetMessageAttribute<MQPublishMessageAttribute>(message);
            var action = GetFuturePublishConfiguration(attr, configAction);
            var connStrName = connectionStringName ?? attr?.ConnectionStringName;
            return (message, action, connStrName);
        }
        private static Action<IFuturePublishConfiguration> GetFuturePublishConfiguration(MQPublishMessageAttribute attr, Action<IFuturePublishConfiguration> configAction)
        {
            return (c) =>
            {
                if (attr != null)
                {
                    //if (attr.ExpireSeconds != 0)
                    //    throw new Exception($"FuturePublish不支持ExpireSeconds。");
                    if (attr.Priority != 0)
                        c.WithPriority(attr.Priority);
                    if (!string.IsNullOrEmpty(attr.Topic))
                        c.WithTopic(attr.Topic);
                }
                configAction?.Invoke(c);
            };
        }

        #endregion

        #region Request => Respond（响应类继承RespondConsumer）
        public static MQResponseResult<TResponse> Request<TMessage, TResponse>(TMessage message, Action<IRequestConfiguration> configAction = null, string connectionStringName = null)
            where TMessage : new()
        {
            var data = GetRpcData(message, configAction, connectionStringName);
            return GetBus(data.ConnStrName)
                .Rpc.Request<TMessage, MQResponseResult<TResponse>>(data.Message, data.Action);
        }
        /// <summary>
        /// 向MQ发布Request请求，响应类需要继承RespondConsumer进行响应
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="message"></param>
        /// <param name="configAction"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static Task<MQResponseResult<TResponse>> RequestAsync<TMessage, TResponse>(TMessage message, Action<IRequestConfiguration> configAction = null, string connectionStringName = null)
            where TMessage : new()
        {
            var data = GetRpcData(message, configAction, connectionStringName);
            return GetBus(data.ConnStrName)
                .Rpc.RequestAsync<TMessage, MQResponseResult<TResponse>>(data.Message, data.Action);
        }
        private static (TMessage Message, Action<IRequestConfiguration> Action, string ConnStrName) GetRpcData<TMessage>(TMessage message, Action<IRequestConfiguration> configAction, string connectionStringName = null)
              where TMessage : new()
        {
            if (message is IMQMessage msg)
            {
                msg.MQMeta = new()
                {
                    MessageId = ObjectId.NewId(),
                    Timestamp = DateTime.UtcNow.UtcDateTimeToTimestamp(false),
                };
            }
            var attr = GetMessageAttribute<MQRequestMessageAttribute>(message);
            var action = GetRequestConfiguration(attr, configAction);
            var connStrName = connectionStringName ?? attr?.ConnectionStringName;
            return (message, action, connStrName);
        }
        private static Action<IRequestConfiguration> GetRequestConfiguration(MQRequestMessageAttribute attr, Action<IRequestConfiguration> configAction)
        {
            return (c) =>
            {
                if (attr != null)
                {
                    if (attr.ExpireSeconds != 0)
                        c.WithExpiration(TimeSpan.FromMilliseconds(attr.ExpireSeconds));
                    if (attr.Priority != 0)
                        c.WithPriority(attr.Priority);
                    if (!string.IsNullOrEmpty(attr.QueueName))
                        c.WithQueueName(attr.QueueName);
                }
                configAction?.Invoke(c);
            };
        }
        #endregion

        #region Send => Receive（消费类继承ReceiveConsumer）
        /// <summary>
        /// 向MQ发送Send消息，响应类需要继承ReceiveConsumer进行响应
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="queueName"></param>
        /// <param name="configAction"></param>
        /// <param name="connectionStringName"></param>
        public static void Send<TMessage>(TMessage message, string queueName = null, Action<ISendConfiguration> configAction = null, string connectionStringName = null)
            where TMessage : new()
        {
            var data = GetSendReceiveData(message, queueName, configAction, connectionStringName);
            GetBus(data.ConnStrName)
                .SendReceive.Send(data.Queue, data.Message, data.Action);
        }

        /// <summary>
        /// 向MQ发送Send消息，响应类需要继承ReceiveConsumer进行响应
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="queueName"></param>
        /// <param name="configAction"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static Task SendAsync<TMessage>(TMessage message, string queueName = null, Action<ISendConfiguration> configAction = null, string connectionStringName = null)
            where TMessage : new()
        {
            var data = GetSendReceiveData(message, queueName, configAction, connectionStringName);
            return GetBus(data.ConnStrName)
                .SendReceive.SendAsync(data.Queue, data.Message, data.Action);
        }
        private static (TMessage Message, Action<ISendConfiguration> Action, string ConnStrName, string Queue) GetSendReceiveData<TMessage>(TMessage message, string queueName = null, Action<ISendConfiguration> configAction = null, string connectionStringName = null)
               where TMessage : new()
        {
            if (message is IMQMessage msg)
            {
                msg.MQMeta = new()
                {
                    MessageId = ObjectId.NewId(),
                    Timestamp = DateTime.UtcNow.UtcDateTimeToTimestamp(false),
                };
            }
            var attr = GetMessageAttribute<MQSendMessageAttribute>(message);
            var action = GetSendConfiguration(attr, configAction);
            var connStrName = connectionStringName ?? attr?.ConnectionStringName;
            var queue = queueName ?? attr.QueueName;
            if (string.IsNullOrEmpty(queue))
                throw new Exception($"MQUtil.Send时，queueName不能为空。messageType:{message.GetType().FullName}");
            return (message, action, connStrName, queue);
        }
        private static Action<ISendConfiguration> GetSendConfiguration(MQSendMessageAttribute attr, Action<ISendConfiguration> configAction)
        {
            return (c) =>
            {
                if (attr != null)
                {
                    if (attr.Priority != 0)
                        c.WithPriority(attr.Priority);
                }
                configAction?.Invoke(c);
            };
        }
        #endregion

        #region ErrorQueue
        /// <summary>
        /// 获取错误队列中的消息
        /// </summary>
        /// <param name="count"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static async Task<List<MQErrorQueueMessage>> GetErrorMessages(int count, string connectionStringName = null)
        {
            var ret = new List<MQErrorQueueMessage>();
            using var consumer = await GetErrorQueueConsumer(connectionStringName);
            var result = await consumer.PullBatchAsync(count);
            foreach (var msg in result.Messages)
            {
                if (msg.IsAvailable)
                {
                    var item = GetMQErrorQueueMessage(msg);
                    ret.Add(item);
                }
            }
            return ret;
        }
        /// <summary>
        /// 重新发送并移除错误队列中的消息
        /// </summary>
        /// <param name="messageId">如果为null，则会发送第一个没有MessageId的消息</param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static async Task<bool> RepublishErrorMessage(string messageId, string connectionStringName = null)
        {
            var bus = GetBus(connectionStringName).Advanced;
            using var consumer = await GetErrorQueueConsumer(connectionStringName);
            while (true)
            {
                var result = await consumer.PullAsync();
                if (!result.IsAvailable)
                    return false;
                var item = GetMQErrorQueueMessage(result);
                // 相同message
                if (item.MQMessage?.MQMeta?.MessageId == messageId)
                {
                    await bus.ExchangeDeclarePassiveAsync(item.Exchange);
                    var exchange = new Exchange(item.Exchange);
                    var msgBody = Encoding.UTF8.GetBytes(item.Message);
                    await bus.PublishAsync(exchange, item.RoutingKey, true, item.BasicProperties, msgBody);
                    await consumer.AckAsync(result.ReceivedInfo.DeliveryTag);
                    LogUtil.Info("MQUtil.RepublishErrorMessage()重新发送并移除错误队列中的消息。{MessageId}", messageId);
                    return true;
                }
            }
        }
        private static async Task<IPullingConsumer<PullResult>> GetErrorQueueConsumer(string connectionStringName = null)
        {
            var bus = GetBus(connectionStringName).Advanced;
            var errQueue = bus.Container.Resolve<IConventions>().ErrorQueueNamingConvention.Invoke(null);
            await bus.QueueDeclarePassiveAsync(errQueue);
            var queue = new EasyNetQ.Topology.Queue(errQueue);
            var consumer = bus.CreatePullingConsumer(queue, false);
            return consumer;
        }
        private static MQErrorQueueMessage GetMQErrorQueueMessage(PullResult result)
        {
            var body = Encoding.UTF8.GetString(result.Body.ToArray());
            var ret = SerializerUtil.DeserializeJson<MQErrorQueueMessage>(body);
            ret.MQMessage = SerializerUtil.DeserializeJson<MQMessageBase>(ret.Message.Replace("\\", ""));
            ret.ReceivedInfo = result.ReceivedInfo;
            return ret;
        }
        #endregion
    }
}