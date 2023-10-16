using EasyNetQ;
using MQDemoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Extensions.RabbitMQ;

namespace MQDemo2
{
    public class DemoMQSub1 : MQSubscribeOrderConsumer<SubMsg>
    {
        public override int QueueCount => 4;

        protected override void Configuration(ISubscriptionConfiguration x)
        {
        }

        protected override async Task OnMessage(SubMsg message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[SAC]: OrderNum:{QueueIndex} 进入{message.Message}");
            await Task.Delay(10000);
            Console.WriteLine($"[SAC]: OrderNum:{QueueIndex} 执行{message.Message}");
            await Task.Delay(10000);
            Console.WriteLine($"[SAC]: OrderNum:{QueueIndex} 完成{message.Message}");
        }
    }
    /*
    public class DemoMQSub1 : MQSubscribeConsumer<SubMsg1>
    {

        protected override bool IsBroadcast => true;
        protected override void Configuration(ISubscriptionConfiguration x)
        {
        }

        protected override async Task OnMessage(SubMsg1 message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[广播]: {message.Message}");
        }
    }

    public class DemoMQSub2 : MQSubscribeConsumer<SubMsg2>
    {
        protected override void Configuration(ISubscriptionConfiguration x)
        {
            x.WithTopic("a.*");
        }

        protected override async Task OnMessage(SubMsg2 message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[收到广播消息]: topic: a.* => {message.Message}");
        }
    }
    public class DemoMQSub21 : MQSubscribeConsumer<SubMsg2>
    {
        protected override void Configuration(ISubscriptionConfiguration x)
        {
            x.WithTopic("xxyy.operators.*");
        }

        protected override async Task OnMessage(SubMsg2 message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[收到广播消息]: topic: xxyy.operators.* => {message.Message}");
        }
    }
    */
}
