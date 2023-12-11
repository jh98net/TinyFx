using EasyNetQ;
using EasyNetQ.Logging;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Hosting;
using TinyFx.Logging;

namespace TinyFx.Extensions.RabbitMQ
{
    internal class MQHostedService : IHostedService
    {
        private MQContainer _container;
        private System.Timers.Timer _timer;
        public MQHostedService()
        {
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _container = DIUtil.GetRequiredService<MQContainer>();
            await _container.InitAsync();
            // SAC
            var section = ConfigUtil.GetSection<RabbitMQSection>();
            _timer = new System.Timers.Timer(section.SACBindDelay * 1000);
            _timer.AutoReset = false;
            _timer.Enabled = true;
            _timer.Elapsed += async (_, _) => 
            {
                await _container.BindSACConsumer();
            };
            _timer.Start();
            
            //LogUtil.Info("RabbitMQ资源已加载: MQContainer.InitAsync()");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Stop();
            _timer.Dispose();
            _container.Dispose();
            LogUtil.Info("RabbitMQ资源已释放: MQContainer.Dispose()");
            return Task.CompletedTask;
        }
    }
}
