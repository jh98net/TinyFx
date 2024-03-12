﻿using Microsoft.Extensions.Hosting;
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
        private ITinyFxHostLifetimeService _lifetimeService;
        public TinyFxHostedService(ITinyFxHostRegisterService registerService, ITinyFxHostTimerService timerService, ITinyFxHostLifetimeService lifetimeService, IHostApplicationLifetime lifetime)
        {
            _registerService = registerService;
            _timerService = timerService;
            _lifetimeService = lifetimeService;
            // 1-StartAsync
            // 2-ApplicationStarted
            // 3-ApplicationStopping
            // 4-StopAsync
            // 5-ApplicationStopped
            lifetime.ApplicationStarted.Register(async () => 
            {
                foreach (var item in _lifetimeService?.StartedEvents)
                {
                    await item.Invoke();
                }
            });
            lifetime.ApplicationStopped.Register(async () =>
            {
                foreach (var item in _lifetimeService?.StoppedEvents)
                {
                    await item.Invoke();
                }
            });
            if (ConfigUtil.Host.RegisterEnabled)
            {
                lifetime.ApplicationStarted.Register(async () => await _registerService.Register());
                if (_timerService != null)
                {
                    if (ConfigUtil.Host.HeartbeatInterval > 0)
                    {
                        _timerService.Register(new TinyFxHostTimerItem
                        {
                            Id = "ITinyFxHostTimerService.Heartbeat",
                            Title = "Host心跳",
                            Interval = ConfigUtil.Host.HeartbeatInterval,
                            ExecuteCount = 0,
                            TryCount = -1,
                            Callback = (stoppingToken) => _registerService.Heartbeat()
                        });
                    }
                    if (ConfigUtil.Host.HeathInterval > 0)
                    {
                        _timerService.Register(new TinyFxHostTimerItem
                        {
                            Id = "ITinyFxHostTimerService.Health",
                            Title = "Host检查",
                            Interval = ConfigUtil.Host.HeathInterval,
                            ExecuteCount = 0,
                            TryCount = -1,
                            Callback = (stoppingToken) => _registerService.Health()
                        });
                    }
                }
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var item in _lifetimeService?.StartingEvents)
            {
                await item.Invoke();
            }
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _timerService?.StartAsync(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _timerService?.StopAsync();
            if (ConfigUtil.Host.RegisterEnabled)
                await _registerService?.Unregister();
            foreach (var item in _lifetimeService?.StoppingEvents)
            {
                await item.Invoke();
            }
            await base.StopAsync(cancellationToken);
        }
    }
}
