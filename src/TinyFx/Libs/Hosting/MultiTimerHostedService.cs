using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Common;
using TinyFx.Logging;

namespace TinyFx.Hosting
{
    /// <summary>
    /// 多定时任务后台服务
    /// var svc = new MultiTimerHostedService();
    /// svc.RegisterWork(...)
    /// services.AddHostedService(svc);
    /// </summary>
    public class MultiTimerHostedService : BackgroundService
    {
        protected MultiTimerWorks TimerWorks { get; } = new MultiTimerWorks();
        private Task _timerTask;
        public MultiTimerHostedService()
        {
        }

        /// <summary>
        /// 注册定时任务
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public void RegisterWork(TimerWork work)
            => TimerWorks.RegisterWork(work);
        public bool RegisterAndUpdateWork(TimerWork work)
            => TimerWorks.RegisterAndUpdateWork(work);
        /// <summary>
        /// 取消定时任务
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        public bool UnregisterWork(string workId)
            => TimerWorks.UnregisterWork(workId);


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
            {
                LogUtil.Debug($"MultiTimerHostedService 服务正在停止..");
            });
            try
            {

                //执行TimerWorks任务
                _timerTask = TimerWorks.StartAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, $"未处理异常：MultiTimerHostedService 执行时！");
            }
            return Task.CompletedTask;
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await TimerWorks.StopAsync(cancellationToken);
            if (_timerTask != null)
                await _timerTask;
            await base.StopAsync(cancellationToken);
            LogUtil.Info($"MultiTimerHostedService 服务已经停止...");
        }
    }
}
