using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinyFx;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx
{
    public static class TinyFxHost
    {
        /// <summary>
        /// 创建默认Host并UseTinyFx
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .UseTinyFx();
        }
        /// <summary>
        /// 阻塞运行
        /// </summary>
        /// <param name="args"></param>
        public static void Run(string[] args = null)
            => CreateBuilder(args).Build().Run();

        /// <summary>
        /// 阻塞运行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Task RunAsync(string[] args = null)
        {
            return CreateBuilder(args).Build().RunAsync();
        }

        #region IHostApplicationLifetime
        /// <summary>
        /// 应用程序生命周期事件的通知
        /// </summary>
        public static IHostApplicationLifetime ApplicationLifetime
            => DIUtil.GetRequiredService<IHostApplicationLifetime>();
        internal static List<Func<Task>> OnStartedEvents = new();
        internal static List<Func<Task>> OnStoppingEvents = new();
        internal static List<Func<Task>> OnStoppedEvents = new();

        public static void Register(ITinyFxHostEvent @event)
        {
            OnStartedEvents.Add(@event.OnStarted);
            OnStoppingEvents.Add(@event.OnStopping);
            OnStoppedEvents.Add(@event.OnStopped);
        }

        public static void RegisterOnStarted(Func<Task> func)
        {
            OnStartedEvents.Add(func);
        }
        public static void RegisterOnStopping(Func<Task> func)
        {
            OnStoppingEvents.Add(func);
        }
        public static void RegisterOnStopped(Func<Task> func)
        {
            OnStoppedEvents.Add(func);
        }
        #endregion
    }
    public interface ITinyFxHostEvent
    {
        Task OnStarted();
        Task OnStopping();
        Task OnStopped();
    }
}
