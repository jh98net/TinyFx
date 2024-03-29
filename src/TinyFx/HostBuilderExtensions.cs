﻿using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Randoms;
using TinyFx.Reflection;

namespace TinyFx
{
    public static class TinyFxHostBuilderExtensions
    {
        private static bool _inited = false;
        /// <summary>
        /// 应用程序中配置TinyFx，优先使用应用程序的配置文件，其次使用tinyfx.json
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="envString"></param>
        /// <returns></returns>
        public static IHostBuilder UseTinyFx(this IHostBuilder builder, string envString = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (_inited)
                return builder;
            if (Serilog.Log.Logger == null)
                LogUtil.CreateBootstrapLogger();
            builder.ConfigureServices((context, services) =>
            {
                //Serilog
                services.AddLogging(builder => builder.AddSerilog(dispose: true));
                services.AddScoped<ILogBuilder>((sp) =>
                {
                    var ret = new LogBuilder("TINYFX_CONTEXT");
                    ret.IsContextLog = true;
                    return ret;
                });
                services.AddDistributedMemoryCache();
                services.AddHostedService<TinyFxHostLifetimeHostedService>();
                // DI
                DIUtil.SetServices(services);
            });

            ConfigUtil.Init(builder, envString);
            builder.ConfigureHostOptions((context, opts) =>
            {
                context.HostingEnvironment.EnvironmentName = ConfigUtil.EnvironmentString;
                context.HostingEnvironment.ApplicationName = ConfigUtil.Project.ProjectId;
                context.Configuration = ConfigUtil.Configuration;
            });
            if (ConfigUtil.Project.MinThreads > 0)
                ThreadPool.SetMinThreads(ConfigUtil.Project.MinThreads, ConfigUtil.Project.MinThreads);

            _inited = true;

            LogUtil.Trace("TinyFx 配置启动");
            return builder;
        }
    }
}
