using EasyNetQ;
using MQDemoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Extensions.RabbitMQ;

namespace MQDemo3
{
    public class DemoMQSub : MQSubscribeConsumer<SubMsg>
    {
        protected override void Configuration(ISubscriptionConfiguration x)
        {
        }

        protected override async Task OnMessage(SubMsg message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[负载]: {message.Message}");
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
    */
}
