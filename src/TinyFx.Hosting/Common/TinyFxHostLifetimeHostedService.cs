using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Collections;
using TinyFx.Configuration;

namespace TinyFx
{
    internal class TinyFxHostLifetimeHostedService : IHostedService
    {
        #region Properties
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;

        public TinyFxHostLifetimeHostedService(IHostApplicationLifetime appLifetime, ILogger<TinyFxHostLifetimeHostedService> logger)
        {
            _logger = logger;
            _appLifetime = appLifetime;

            _appLifetime.ApplicationStarted.Register(async () => await OnStarted());
            _appLifetime.ApplicationStopping.Register(async () => await OnStopping());
            _appLifetime.ApplicationStopped.Register(async () => await OnStopped());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        #endregion

        private async Task OnStarted()
        {
            _logger.LogWarning($"[IHostApplicationLifetime.ApplicationStarted] 服务已启动");
            // 此处代码在Host启动后执行
            await TinyFxHost.OnStartedEvents.ForEachAsync(async x =>
            {
                await x();
            });
        }

        private async Task OnStopping()
        {
            _logger.LogWarning($"[IHostApplicationLifetime.ApplicationStopping] 服务将停止");
            // 此处代码在Host停止动作开始时执行
            await TinyFxHost.OnStoppingEvents.ForEachAsync(async x =>
            {
                await x();
            });
        }

        private async Task OnStopped()
        {
            _logger.LogWarning($"[IHostApplicationLifetime.ApplicationStopped] 服务已停止");
            // 此处代码在Host停止后执行
            await TinyFxHost.OnStoppedEvents.ForEachAsync(async x =>
            {
                await x();
            });
        }
    }
}
