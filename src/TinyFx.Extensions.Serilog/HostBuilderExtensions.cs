using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
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
            builder.ConfigureServices(services =>
            {
                services.AddSerilog(dispose: true);
            });
            builder.UseSerilog((context, services, configuration) =>
            {
                SetELKSinkIndexFormat(ConfigUtil.Configuration);
                configuration.ReadFrom.Configuration(ConfigUtil.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty(SerilogUtil.EnvironmentNamePropertyName, ConfigUtil.EnvironmentString)
                    .Enrich.WithProperty(SerilogUtil.ProjectIdPropertyName, ConfigUtil.Project?.ProjectId ?? "未知程序")
                    .Enrich.WithProperty(SerilogUtil.MachineIPPropertyName, NetUtil.GetLocalIP())
                    //.Enrich.WithProperty(SerilogUtil.IndexNamePropertyName, ConfigUtil.Project?.ProjectId.Replace('.', '_').ToLowerInvariant())
                    .Enrich.WithTemplateHash();
            }, true);

            // 启动Serilog内部调试
            //Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));
            //Serilog.Debugging.SelfLog.Enable(Console.Error);
            LogUtil.Debug("Serilog 配置启动");
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
                    config["Serilog:WriteTo:ELKSink:Args:indexFormat"]
                        = $"idx-{ConfigUtil.Project.ProjectId.Replace('.', '_')}-{{0:yyyy.MM.dd}}";
                }
                return true;
            }
            return false;
        }
    }
}
