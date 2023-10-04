﻿using Microsoft.Extensions.Configuration;
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
        internal static List<IHostLifetimeEvent> LifetimeEvents = new();
        /// <summary>
        /// 添加应用程序生命周期事件类
        /// </summary>
        /// <param name="lifetimeEvent"></param>
        public static void RegisterLifetimeEvent(IHostLifetimeEvent lifetimeEvent)
        {
            LifetimeEvents.Add(lifetimeEvent);
        }
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

        public static void Run(string[] args = null)
            => CreateBuilder(args).Build().Run();

        /// <summary>
        /// 运行配置了TinyFx的Host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Task RunAsync(string[] args = null)
        {
            return CreateBuilder(args).Build().RunAsync();
        }

        /// <summary>
        /// 应用程序生命周期事件的通知
        /// </summary>
        public static IHostApplicationLifetime ApplicationLifetime
            => DIUtil.GetRequiredService<IHostApplicationLifetime>();
    }
}
