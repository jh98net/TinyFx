using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFx.Extensions.RabbitMQ
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class MQMessageAttribute: Attribute
    {
        /// <summary>
        /// [ALL]MQ链接字符串
        /// </summary>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// [Publish,Request]为消息设置 TTL(milliseconds)
        /// </summary>
        public int Expires { get; set; }
        /// <summary>
        /// [Publish,Request,Send]设置消息的优先级
        /// </summary>
        public byte Priority { get; set; }
        /// <summary>
        /// [Publish]设置消息的主题，用于消息过滤分发，.符号分割如：xxyy.sub.game
        ///     * 匹配一个单词
        ///     # 匹配零个或多个单词
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// [Request,Send]设置要发布到的队列名称
        /// </summary>
        public string QueueName { get; set; }

        public MQMessageAttribute(string connectionStringName = null, string topic=null, string queueName=null, int expires=0, byte priority=0)
        {
            ConnectionStringName = connectionStringName;
            Topic = topic;
            QueueName = queueName;
            Expires = expires;
            Priority = priority;
        }
    }
}
