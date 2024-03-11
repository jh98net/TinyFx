using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using Refit;
using Serilog;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using TinyFx.Configuration;
using TinyFx.Extensions.Nacos;
using TinyFx.Extensions.Serilog;
using TinyFx.Hosting;
using TinyFx.Hosting.Common;
using TinyFx.Hosting.Services;
using TinyFx.Logging;
using TinyFx.Net;

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
            
            // Logger
            if (Serilog.Log.Logger == null)
                SerilogUtil.CreateBootstrapLogger();
            builder.ConfigureLogging(logger => logger.ClearProviders());
            builder.ConfigureServices((context, services) =>
            {
                // DI
                DIUtil.InitServices(services);
                services.AddOptions();
                services.AddSingleton(new LoggerFactory().AddSerilog(Log.Logger)); // ILoggerFactory
                services.AddScoped<ILogBuilder>((sp) => // ILogBuilder
                {
                    var ret = new LogBuilder("TINYFX_CONTEXT");
                    ret.IsContext = true;
                    return ret;
                });

                // DistributedMemoryCache
                services.AddDistributedMemoryCache();
            });

            // Configuration
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
                services.AddSingleton<ITinyFxHostRegisterService>(new RedisHostRegisterService());
                switch (configHelper.From)
                {
                    case ConfigSourceFrom.File:
                        services.AddSingleton<ITinyFxHostMicroService>(new RedisHostMicroService());
                        break;
                    case ConfigSourceFrom.Nacos:
                        services.AddSingleton<ITinyFxHostMicroService>(new NacosHostMicroService());
                        break;
                }
            });

            // HttpClient
            builder.ConfigureServices(services =>
            {
                var clientSection = ConfigUtil.GetSection<JsonHttpClientSection>();
                if (clientSection == null || clientSection.Clients.Count == 0)
                    return;
                foreach (var client in clientSection.Clients)
                {
                    var builder = services.AddHttpClient(client.Key, c =>
                    {
                        if (!string.IsNullOrEmpty(client.Value.BaseAddress))
                            c.BaseAddress = new Uri(client.Value.BaseAddress);
                        if (client.Value.RequestHeaders.Count > 0)
                        {
                            foreach (var header in client.Value.RequestHeaders)
                            {
                                c.DefaultRequestHeaders.Add(header.Key, header.Value);
                            }
                        }
                        if (client.Value.Timeout > 0)
                            c.Timeout = TimeSpan.FromSeconds(client.Value.Timeout);
                    }).SetHandlerLifetime(TimeSpan.FromMinutes(5));

                    if (client.Value.Retry > 0)
                    {
                        builder.AddPolicyHandler(HttpPolicyExtensions
                            .HandleTransientHttpError()
                            .Or<TimeoutRejectedException>() // 若超时则抛出此异常
                            .WaitAndRetryAsync(client.Value.Retry, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
                        builder.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(10));

                    }
                }
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
