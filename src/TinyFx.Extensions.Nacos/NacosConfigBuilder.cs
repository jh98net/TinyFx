using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nacos.V2.Naming.Utils;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using Polly;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static System.Collections.Specialized.BitVector32;
using Nacos.V2;

namespace TinyFx.Extensions.Nacos
{
    public class NacosConfigBuilder : IExternalConfigBuilder
    {
        public IConfiguration Build(IConfiguration sourceConfig, IHostBuilder hostBuilder)
        {
            var nacosConfig = sourceConfig.GetSection("Nacos");
            if (nacosConfig == null)
                return null;
            var section = new NacosSection();
            section.Bind(nacosConfig);
            if (!section.Enabled)
                return null;

            SetNacosConfig(section, nacosConfig);
            NacosUtil.Section.Bind(nacosConfig);
            section = NacosUtil.Section;

            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            builder.AddNacosV2Configuration(nacosConfig);
            builder.AddConfiguration(sourceConfig, false);

            var ret = builder.Build();
            SetReturnConfig(section, ret);

            hostBuilder.ConfigureServices(services =>
            {
                services.AddHttpClient(NacosOpenApiService.HTTP_CLIENT_NAME)
                    .ConfigurePrimaryHttpMessageHandler(() =>
                        new HttpClientHandler()
                        {
                            UseProxy = false,
                            AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
                        }
                    );
            });

            LogUtil.Info($"配置管理 [加载nacos配置源] ServerAddresses: {string.Join('|', section.ServerAddresses)} Namespace: {section.Namespace} ServiceName:{section.ServiceName}");
            return ret;
        }

        private void SetNacosConfig(NacosSection section, IConfigurationSection nacosConfig)
        {
            // HostIp
            var hostIp = ConfigUtil.ServiceInfo.HostIp;
            if (string.IsNullOrEmpty(section.Ip))
            {
                if (!string.IsNullOrEmpty(hostIp))
                {
                    nacosConfig["Ip"] = hostIp;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(hostIp))
                {
                    ConfigUtil.ServiceInfo.HostIp = section.Ip;
                }
                else
                {
                    if (hostIp != section.Ip)
                        throw new Exception($"Nacos:Ip配置[{section.Ip}]与ConfigUtil.ServiceInfo.HostIp[{hostIp}]不一致。");
                }
            }

            // HostPort
            var hostPort = ConfigUtil.ServiceInfo.HostPort;
            if (section.Port <= 0)
            {
                if (hostPort > 0)
                {
                    nacosConfig["Port"] = hostPort.ToString();
                }
            }
            else
            {
                if (hostPort <= 0)
                {
                    ConfigUtil.ServiceInfo.HostPort = section.Port;
                }
                else
                {
                    if (hostPort != section.Port)
                        throw new Exception($"Nacos:Port配置[{section.Port}]与ConfigUtil.ServiceInfo.HostPort[{hostPort}]不一致。");
                }
            }

            // FailoverDir
            if (!string.IsNullOrEmpty(section.FailoverDir))
            {
                var file = Path.Combine(section.FailoverDir, "nacos", "naming", section.Namespace, "failover", UtilAndComs.FAILOVER_SWITCH);
                var path = Path.GetDirectoryName(file);
                try
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    if (!File.Exists(file))
                        File.WriteAllText(file, "0");
                    System.Environment.SetEnvironmentVariable("JM.SNAPSHOT.PATH", section.FailoverDir);
                }
                catch { }
            }
        }

        private void SetReturnConfig(NacosSection section, IConfigurationRoot config)
        {
            if (!string.IsNullOrEmpty(section.ServiceName))
            {
                var pid = config["Project:ProjectId"];
                if (string.IsNullOrEmpty(pid))
                    config["Project:ProjectId"] = section.ServiceName;
                else
                {
                    if (pid != section.ServiceName)
                        throw new Exception("配置Nacos:ServiceName不能为空且必须与ProjectId相同");
                }
            }
        }
    }
}
