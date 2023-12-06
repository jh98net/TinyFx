using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Nacos.Microsoft.Extensions.Configuration;
using Nacos.V2.DependencyInjection;
using Nacos.V2.Naming.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Configuration.Common;

namespace TinyFx.Extensions.Nacos
{
    public class NacosConfigSourceProvider : IConfigSourceProvider
    {
        public IConfigurationBuilder CreateConfigBuilder(IHostBuilder hostBuilder, IConfiguration config)
        {
            IConfigurationBuilder ret = null;
            var hasNacos = config.GetValue("Nacos:Enabled", false);
            if (hasNacos)
            {
                var failoverDir = config.GetValue("Nacos:FailoverDir", "");
                if (!string.IsNullOrEmpty(failoverDir))
                {
                    var ns = config.GetValue("Nacos:Namespace", "");
                    var file = Path.Combine(failoverDir, "nacos", "naming", ns, "failover", UtilAndComs.FAILOVER_SWITCH);
                    var path = Path.GetDirectoryName(file);
                    try
                    {
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        if (!File.Exists(file))
                            File.WriteAllText(file, "0");
                        System.Environment.SetEnvironmentVariable("JM.SNAPSHOT.PATH", failoverDir);
                    }
                    catch { }
                }
                // 是否启用config
                var listeners = config.GetSection("Nacos:Listeners").Get<List<ConfigListener>>();
                if (listeners != null && listeners.Count > 0)
                {
                    ret = new ConfigurationBuilder();
                    ret.AddConfiguration(config, false);
                    ret.AddNacosV2Configuration(config.GetSection("Nacos"));
                    ret.AddEnvironmentVariables();
                }
                // 是否启用naming
                var clients = config.GetSection("Nacos:Clients").Get<Dictionary<string, NacosClientElement>>();
                if (clients != null && clients.Count > 0)
                {
                    hostBuilder.ConfigureServices((context, services) =>
                    {
                        services.AddNacosV2Naming(context.Configuration, sectionName: "Nacos");
                    });
                }
            }
            return ret;
        }
    }
}
