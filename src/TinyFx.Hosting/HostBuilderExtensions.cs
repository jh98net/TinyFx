using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using TinyFx.Configuration;
using TinyFx.Extensions.Serilog;
using TinyFx.Hosting.Common;
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
        public static IHostBuilder AddTinyFx(this IHostBuilder builder, string envString = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (Serilog.Log.Logger == null)
                SerilogUtil.CreateBootstrapLogger();
            
            //
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
                DIUtil.InitServices(services);
            });

            // InitConfiguration
            var configHelper = new ConfigInitHelper(builder, envString);
            var configuration = configHelper.GetConfiguration();
            ConfigUtil.InitConfiguration(configuration, configHelper.EnvString);
            builder.ConfigureHostOptions((context, opts) =>
            {
                context.HostingEnvironment.EnvironmentName = ConfigUtil.EnvironmentString;
                context.HostingEnvironment.ApplicationName = ConfigUtil.Project.ProjectId;
                context.Configuration = ConfigUtil.Configuration;
            });
            if (ConfigUtil.Project.MinThreads > 0)
                ThreadPool.SetMinThreads(ConfigUtil.Project.MinThreads, ConfigUtil.Project.MinThreads);

            LogUtil.Debug("TinyFx 配置完成");
            return builder;
        }
    }
}
