using EasyNetQ.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;

namespace TinyFx.Extensions.RabbitMQ
{
    internal class MQHostedService : BackgroundService
    {
        private MQContainer _container;
        public MQHostedService()
        {
            var section = ConfigUtil.GetSection<RabbitMQSection>();
            // init
            _container = DIUtil.GetRequiredService<MQContainer>();
            _container.Init();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _container.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
