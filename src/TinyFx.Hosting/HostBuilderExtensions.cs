using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading;
using TinyFx.Configuration;
using TinyFx.Extensions.Serilog;
using TinyFx.Hosting;
using TinyFx.Hosting.Common;
using TinyFx.Hosting.Services;
using TinyFx.Logging;

namespace TinyFx
{
    public static class TinyFxHostBuilderExtensions
    {
        /// <summary>
        /// 应用程序中配置TinyFx，优先使用应用程序的配置文件，其次使用tinyfx.json
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="envString"></param>
        /// <returns></returns>
        public static IHostBuilder AddTinyFxEx(this IHostBuilder builder, string envString = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (Serilog.Log.Logger == null)
                SerilogUtil.CreateBootstrapLogger();
            builder.ConfigureLogging(logger => logger.ClearProviders());
            //
            builder.ConfigureServices((context, services) =>
            {
                // DI
                DIUtil.InitServices(services);
                // ILoggerFactory
                services.AddSingleton(new LoggerFactory().AddSerilog(Log.Logger));
                services.AddScoped<ILogBuilder>((sp) =>
                {
                    var ret = new LogBuilder("TINYFX_CONTEXT");
                    ret.IsContext = true;
                    return ret;
                });

                // DistributedMemoryCache
                services.AddDistributedMemoryCache();
            });

            // InitConfiguration
            ConfigUtil.ServiceInfo.HostIp = new HostIpGetter().Get();
            ConfigUtil.ServiceInfo.HostPort = new HostPortGetter().Get();
            var configHelper = new ConfigSourceBuilder(builder, envString);
            var configuration = configHelper.Build();
            ConfigUtil.InitConfiguration(configuration, configHelper.EnvString);
            builder.ConfigureHostOptions((context, opts) =>
            {
                context.HostingEnvironment.EnvironmentName = ConfigUtil.EnvironmentString;
                context.HostingEnvironment.ApplicationName = ConfigUtil.Project.ProjectId;
                context.Configuration = ConfigUtil.Configuration;
            });
            if (ConfigUtil.Project.MinThreads > 0)
                ThreadPool.SetMinThreads(ConfigUtil.Project.MinThreads, ConfigUtil.Project.MinThreads);

            // Hosting
            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<ITinyFxHostLifetimeService>(HostingUtil.LifetimeService);
                services.AddSingleton<ITinyFxHostTimerService>(new DefaultTinyFxHostTimerService());
                services.AddSingleton<ITinyFxHostRegisterService>(new RedisTinyFxHostRegisterService());
            });
            LogUtil.Info("配置 => [TinyFx]");
            return builder;
        }

        public static IHostBuilder AddTinyFxHostEx(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddHostedService<TinyFxHostedService>();
            });
            return builder;
        }
    }
}
