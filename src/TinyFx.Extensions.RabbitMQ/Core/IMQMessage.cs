using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.RabbitMQ
{
    public interface IMQMessage
    {
        /// <summary>
        /// 消息唯一ID
        /// </summary>
        string MessageId { get; set; }
        /// <summary>
        /// 消息发送时间
        /// </summary>
        long Timestamp { get; set; }
    }
    internal class MQMessageBase : IMQMessage
    {
        public string MessageId { get; set; }
        public long Timestamp { get; set; }
    }
}
