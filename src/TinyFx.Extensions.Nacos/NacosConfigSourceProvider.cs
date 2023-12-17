using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Nacos.Microsoft.Extensions.Configuration;
using Nacos.V2.DependencyInjection;
using Nacos.V2.Naming.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Configuration.Common;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TinyFx.Extensions.Nacos
{
    public class NacosConfigSourceProvider : BaseConfigSourceProvider
    {
        public NacosConfigSourceProvider(IConfiguration config) : base(config)
        {
        }

        public string GetServerAddresses(IConfiguration config)
        {
            var servers = config.GetSection("Nacos:ServerAddresses")?.Get<List<string>>();
            return servers != null ? string.Join('|', servers) : null;
        }
        
        public string GetNamespace(IConfiguration config)
            => config.GetValue("Nacos:Namespace", "");

        public override IConfigurationBuilder CreateBuilder(IHostBuilder hostBuilder)
        {
            IConfigurationBuilder ret = null;
            var hasNacos = InitConfiguration.GetValue("Nacos:Enabled", false);
            if (hasNacos)
            {
                var failoverDir = InitConfiguration.GetValue("Nacos:FailoverDir", "");
                if (!string.IsNullOrEmpty(failoverDir))
                {
                    var ns = InitConfiguration.GetValue("Nacos:Namespace", "");
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
                ret = new ConfigurationBuilder();
                ret.AddConfiguration(InitConfiguration, false);
                ret.AddNacosV2Configuration(InitConfiguration.GetSection("Nacos"));
                ret.AddEnvironmentVariables();

                // 是否启用naming
                //var clients = config.GetSection("Nacos:Clients").Get<Dictionary<string, NacosClientElement>>();
                //if (clients != null && clients.Count > 0)
                //{
                //    hostBuilder.ConfigureServices((context, services) =>
                //    {
                //        services.AddNacosV2Naming(context.Configuration, sectionName: "Nacos");
                //    });
                //}
            }
            return ret;
        }
    }
}
