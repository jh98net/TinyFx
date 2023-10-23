using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.RabbitMQ
{
    public interface IMQMessage
    {
        MQMessageMeta MQMeta { get; set; }
    }
    public class MQMessageMeta
    {
        /// <summary>
        /// 消息路由关键值
        /// </summary>
        public string RoutingKey { get; set; }
        /// <summary>
        /// 延迟间隔
        /// </summary>
        public TimeSpan Delay { get; set; }

        /// <summary>
        /// 消息唯一ID（自动设置）
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// 消息发送时间（自动设置）
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// 异常Action列表
        /// </summary>
        public List<string> ErrorActionList { get; set; } = new();
        /// <summary>
        /// 当前异常的Action（自动设置）
        /// </summary>
        public string ErrorAction { get; set; }
    }
    internal class MQMessageBase : IMQMessage
    {
        public MQMessageMeta MQMeta { get; set; }
    }
}
