using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;
using TinyFx.Extensions.Serilog;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx
{
    public static class SerilogHostBuilderExtensions
    {
        /// <summary>
        /// 设置Serilog为logging provider,
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostBuilder UseSerilogEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.Configuration.GetSection("Serilog");
            if (section == null)
                return builder;
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty(SerilogUtil.EnvironmentNamePropertyName, ConfigUtil.EnvironmentString)
                .Enrich.WithProperty(SerilogUtil.MachineIPPropertyName, NetUtil.GetLocalIP())
                .Enrich.WithProperty(SerilogUtil.MachineNamePropertyName, Environment.MachineName)
                .Enrich.WithProperty(SerilogUtil.ProjectIdPropertyName, ConfigUtil.Project?.ProjectId ?? "未知程序")
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .ReadFrom.Configuration(ConfigUtil.Configuration)
                .CreateLogger(); 
            builder.ConfigureLogging(logger =>
            {
                logger.ClearProviders();
            });
            builder.UseSerilog(logger: Log.Logger, dispose: true);
            LogUtil.Rebuild();
            /*
            builder.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(ConfigUtil.Configuration, "Serilog")
                    .Enrich.FromLogContext();
            });
            builder.ConfigureServices((services) => {
                services.AddLogging((loggingBuilder) => {
                    loggingBuilder.AddSerilog(dispose: true);
                });
            });
            */

            // 启动Serilog内部调试
            //SL.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));
            //SL.Debugging.SelfLog.Enable(Console.Error);
            LogUtil.Trace("Serilog 配置启动");
            return builder;
        }
    }
}
