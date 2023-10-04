using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.RabbitMQ
{
    public class MQBusData
    {
        public MQConnectionStringElement Element { get; set; }
        public ConnectionConfiguration Connection { get; set; }
        public IBus Bus { get; set; }

        public void Dispose()
        {
            Bus?.Dispose();
        }
    }
}
