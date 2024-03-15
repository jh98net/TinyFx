using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nacos.Naming.Utils;
using System;
using System.IO;
using System.Net.Http;
using TinyFx.Configuration;
using TinyFx.Logging;

namespace TinyFx.Extensions.Nacos
{
    /// <summary>
    /// API: https://nacos.io/zh-cn/docs/open-api.html
    /// nacos高可用: https://www.cnblogs.com/crazymakercircle/p/15393171.html
    /// nacos-sdk-csharp: https://github.com/nacos-group/nacos-sdk-csharp/tree/dev?tab=readme-ov-file#nacos-sdk-csharp-------%E4%B8%AD%E6%96%87
    /// </summary>
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
            section.Bind(nacosConfig);

            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            builder.AddNacosV2Configuration(nacosConfig);
            builder.AddConfiguration(sourceConfig, false);

            var ret = builder.Build();
            SetReturnConfig(section, ret);

            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(section);
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
            ConfigUtil.Service.HostSecure = section.Secure;

            // HostIp
            var envHostIp = ConfigUtil.Service.HostIp;
            if (!string.IsNullOrEmpty(section.Ip))
            {
                if (!string.IsNullOrEmpty(envHostIp) && envHostIp != section.Ip)
                    throw new Exception($"Nacos:Ip配置[{section.Ip}]与ConfigUtil.ServiceInfo.HostIp[{envHostIp}]不一致。");
                ConfigUtil.Service.HostIp = section.Ip;
            }
            else
            {
                nacosConfig["Ip"] = envHostIp;
            }

            // HostPort
            var envHostPort = ConfigUtil.Service.HostPort;
            if (section.Port > 0)
            {
                if (envHostPort > 0 && envHostPort != section.Port)
                    throw new Exception($"Nacos:Port配置[{section.Port}]与ConfigUtil.ServiceInfo.HostPort[{envHostPort}]不一致。");
                ConfigUtil.Service.HostPort = section.Port;
            }
            else
            {
                nacosConfig["Port"] = envHostPort.ToString();
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
