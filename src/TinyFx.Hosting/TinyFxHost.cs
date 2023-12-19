using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinyFx;
using TinyFx.Configuration;
using TinyFx.Extensions.Serilog;
using TinyFx.Hosting;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx
{
    public static class TinyFxHost
    {
        /// <summary>
        /// 创建默认Host并UseTinyFx
        /// </summary>
        /// <param name="envString"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateBuilder(string envString = null, string[] args = null)
        {
            SerilogUtil.CreateBootstrapLogger();
            var builder = Host.CreateDefaultBuilder(args)
                .AddTinyFx(envString)
                .AddSerilogEx()
                .AddAutoMapperEx()
                .AddRedisEx()
                .AddSqlSugarEx()
                .AddRabbitMQEx()
                .AddIDGenerator()
                .AddDbCachingEx();
            return builder;
        }

        public static IHost CreateHost(string envString = null, string[] args = null)
        {
            return CreateBuilder(envString, args)
                .Build()
                .UseTinyFx();
        }

        /// <summary>
        /// 非阻塞运行
        /// </summary>
        /// <param name="envString"></param>
        /// <param name="args"></param>
        public static void Start(string envString = null, string[] args = null)
            => CreateHost(envString, args).Start();

        /// <summary>
        /// 非阻塞运行
        /// </summary>
        /// <param name="envString"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Task StartAsync(string envString = null, string[] args = null)
            => CreateHost(envString, args).StartAsync();

        /// <summary>
        /// 阻塞运行
        /// </summary>
        /// <param name="envString"></param>
        /// <param name="args"></param>
        public static void Run(string envString = null, string[] args = null)
            => CreateHost(envString, args).Run();

        /// <summary>
        /// 阻塞运行
        /// </summary>
        /// <param name="envString"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Task RunAsync(string envString = null, string[] args = null)
            => CreateHost(envString, args).RunAsync();

        #region IHostApplicationLifetime
        /// <summary>
        /// 应用程序生命周期事件的通知
        /// </summary>
        public static IHostApplicationLifetime Lifetime
            => DIUtil.GetRequiredService<IHostApplicationLifetime>();

        internal static List<Func<Task>> OnStartedEvents = new();
        internal static List<Func<Task>> OnStoppingEvents = new();
        internal static List<Func<Task>> OnStoppedEvents = new();

        public static void RegisterLifetimeEvent(ITinyFxHostLifetimeEvent @event)
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
}
