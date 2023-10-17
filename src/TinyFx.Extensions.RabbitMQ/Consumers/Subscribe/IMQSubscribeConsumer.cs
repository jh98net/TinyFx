using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.RabbitMQ
{
    public interface IMQSubscribeConsumer : IMQConsumer
    {
        MQSubscribeMode SubscribeMode { get; }
        Type MQMessageType { get; }
        int QueueCount { get; }
        int QueueIndex { get; }
        bool IsRegisted { get; }
    }
    internal interface IMQSubscribeSetQueueIndex
    {
        void SetQueueIndex(int value);
    }
}
