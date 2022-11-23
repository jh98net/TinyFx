using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Logging;

namespace TinyFx.Hosting
{
    /// <summary>
    /// 定时任务后台服务基类
    /// 注册：services.AddHostedService();
    /// </summary>
    public abstract class TimerHostedService : IHostedService, IDisposable
    {
        private readonly Timer _timer;
        public abstract TimeSpan Period { get; }
        public bool IsExecuting { get; private set; }

        protected TimerHostedService()
        {
            _timer = new Timer(Execute, null, Timeout.Infinite, 0);
        }

        public void Execute(object state = null)
        {
            try
            {
                IsExecuting = true;
                LogUtil.Debug($"开始执行服务: {this.GetType().FullName}");
                ExecuteAsync().Wait();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, $"执行异常: {this.GetType().FullName}");
            }
            finally
            {
                IsExecuting = false;
                LogUtil.Debug($"执行结束: {this.GetType().FullName}");
            }
        }

        protected abstract Task ExecuteAsync();

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            LogUtil.Debug($"启动服务: {this.GetType().FullName}");
            _timer.Change(TimeSpan.FromSeconds(new Random().Next(10)), Period);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            LogUtil.Debug($"停止服务: {this.GetType().FullName}");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
