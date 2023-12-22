using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Common;
using TinyFx.Configuration;
using TinyFx.Hosting.Services;
using TinyFx.Logging;

namespace TinyFx.Hosting
{
    public class TinyFxHostedService : BackgroundService
    {
        private ITinyFxHostRegisterService _registerService;
        private ITinyFxHostTimerService _timerService;
        public TinyFxHostedService(ITinyFxHostRegisterService registerService, ITinyFxHostTimerService timerService)
        {
            _registerService = registerService;
            _timerService = timerService;
            if (_timerService != null && _registerService != null)
            {
                _timerService.Register(new TinyFxHostTimerItem
                {
                    Id = "ITinyFxHostTimerService.Heartbeat",
                    Title = "Host服务心跳",
                    Interval = ConfigUtil.Project.HostHeartbeatInterval,
                    ExecuteCount = 0,
                    TryCount = int.MaxValue,
                    Callback = (stoppingToken) => _registerService.Heartbeat()
                });
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _registerService?.Register();
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _timerService?.StartAsync(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _timerService?.StopAsync();
            await _registerService?.Unregister();
            await base.StopAsync(cancellationToken);
        }
    }
}
