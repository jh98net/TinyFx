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
    public interface IHostLifetimeEvent
    {
        Task OnStarted();
        Task OnStopping();
        Task OnStopped();
    }
    public class DefaultHostLifetimeEvent : IHostLifetimeEvent
    {
        public virtual Task OnStarted()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnStopped()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnStopping()
        {
            return Task.CompletedTask;
        }
    }
    internal class LifetimeEventsHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;

        public LifetimeEventsHostedService(IHostApplicationLifetime appLifetime, ILogger<LifetimeEventsHostedService> logger)
        {
            _logger = logger;
            _appLifetime = appLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(async () => await OnStarted());
            _appLifetime.ApplicationStopping.Register(async () => await OnStopping());
            _appLifetime.ApplicationStopped.Register(async () => await OnStopped());

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task OnStarted()
        {
            _logger.LogInformation($"[{ConfigUtil.Project.ProjectId}] LifetimeEventsHostedService.OnStarted被调用");
            // 此处代码在Host启动后执行
            await TinyFxHost.LifetimeEvents.ForEachAsync(async x =>
            {
                await x.OnStarted();
            });
        }

        private async Task OnStopping()
        {
            _logger.LogInformation($"[{ConfigUtil.Project.ProjectId}] LifetimeEventsHostedService.OnStopping被调用");
            // 此处代码在Host停止动作开始时执行
            await TinyFxHost.LifetimeEvents.ForEachAsync(async x =>
            {
                await x.OnStopping();
            });
        }

        private async Task OnStopped()
        {
            _logger.LogInformation($"[{ConfigUtil.Project.ProjectId}] LifetimeEventsHostedService.OnStopped被调用");
            // 此处代码在Host停止后执行
            await TinyFxHost.LifetimeEvents.ForEachAsync(async x =>
            {
                await x.OnStopped();
            });
        }
    }
}
