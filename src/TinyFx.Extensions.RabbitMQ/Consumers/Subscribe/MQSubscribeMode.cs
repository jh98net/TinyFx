using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.RabbitMQ
{
    public enum MQSubscribeMode
    {
        /// <summary>
        /// 单Queue模式: 
        ///     服务为消息创建一个Queue，多个消费者负载均衡消费
        /// </summary>
        OneQueue,
        /// <summary>
        /// 多Queue模式:
        ///     服务为消息创建多个Queue，多个消费者负载均衡消费
        ///     Queue数量建议等于服务器cpu核数
        /// </summary>
        MultiQueue,
        /// <summary>
        /// SAC模式: 
        ///     服务为消息创建多个Queue，且每个Queue同时有且只有1个消费者，其他为备用消费者
        /// </summary>
        SAC,
        /// <summary>
        /// 多播模式: 
        ///     服务中的每一个实例都会为消息创建一个Queue，消息会被发送到每一个实例
        ///     建议使用redis实现
        /// </summary>
        Multicast
    }
}
