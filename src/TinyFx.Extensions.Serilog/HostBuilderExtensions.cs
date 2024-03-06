using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
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
        public static IHostBuilder AddSerilogEx(this IHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            var section = ConfigUtil.Configuration.GetSection("Serilog");
            if (section == null)
                return builder;

            // 配置Log.Logger
            SetELKSinkIndexFormat(ConfigUtil.Configuration);
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(ConfigUtil.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty(SerilogUtil.EnvironmentNamePropertyName, ConfigUtil.EnvironmentString)
                .Enrich.WithProperty(SerilogUtil.ProjectIdPropertyName, ConfigUtil.Project?.ProjectId ?? "未知程序")
                .Enrich.WithProperty(SerilogUtil.ServiceIdPropertyName, ConfigUtil.ServiceInfo.ServiceId ?? "未知服务")
                .Enrich.WithProperty(SerilogUtil.MachineIPPropertyName, NetUtil.GetLocalIP())
                //.Enrich.WithProperty(SerilogUtil.IndexNamePropertyName, ConfigUtil.Project?.ProjectId.Replace('.', '_').ToLowerInvariant())
                .Enrich.WithTemplateHash()
                .CreateLogger();
            builder.UseSerilog(Log.Logger);
            LogUtil.Init();

            // 启动Serilog内部调试
            //Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));
            //Serilog.Debugging.SelfLog.Enable(Console.Error);
            LogUtil.Info("配置 => [Serilog]");
            return builder;
        }
        private static bool SetELKSinkIndexFormat(IConfiguration config)
        {
            var elk = config["Serilog:WriteTo:ELKSink:Name"];
            if (!string.IsNullOrEmpty(elk))
            {
                var idx = config["Serilog:WriteTo:ELKSink:Args:indexFormat"];
                if (string.IsNullOrEmpty(idx))
                {
                    var projectId = ConfigUtil.Project.ProjectId.Replace('.', '_');
                    var env = !string.IsNullOrEmpty(ConfigUtil.EnvironmentString)
                        ? $"-{ConfigUtil.EnvironmentString.ToLower().Replace('.', '_')}"
                        : null;
                    config["Serilog:WriteTo:ELKSink:Args:indexFormat"]
                        = $"idx-{projectId}{env}-{{0:yyyyMMdd}}";
                }
                return true;
            }
            return false;
        }
    }
}
