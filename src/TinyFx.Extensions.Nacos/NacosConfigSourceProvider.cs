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
using static System.Collections.Specialized.BitVector32;

namespace TinyFx.Extensions.Nacos
{
    public class NacosConfigSourceProvider : BaseConfigSourceProvider
    {
        public NacosSection _section;
        public bool Enabled => _section.Enabled;
        public string Namespace => _section.Namespace;
        public NacosConfigSourceProvider(IConfiguration config) : base(config)
        {
            var nacosConfig = config.GetSection("Nacos");
            if (nacosConfig != null)
            {
                _section.Bind(nacosConfig);
                NacosUtil.Section.Bind(nacosConfig);
            }
            _section = NacosUtil.Section;
        }

        public string GetServerAddresses()
            => string.Join('|', _section.ServerAddresses);

        public override IConfigurationBuilder CreateBuilder(IHostBuilder hostBuilder)
        {
            if (!Enabled) return null;
                

            var hostIp = ConfigUtil.ServiceInfo.HostIp;
            if (string.IsNullOrEmpty(_section.Ip))
            {
                if (!string.IsNullOrEmpty(hostIp))
                {
                    _section.Ip = hostIp;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(hostIp))
                {
                    ConfigUtil.ServiceInfo.HostIp = _section.Ip;
                }
            }
            var hostPort = ConfigUtil.ServiceInfo.HostPort;
            if (_section.Port <= 0)
            {
                if (hostPort > 0)
                {
                    _section.Port = hostPort;
                }
            }
            else
            {
                if (hostPort <= 0)
                {
                    ConfigUtil.ServiceInfo.HostPort = _section.Port;
                }
            }

            if (!string.IsNullOrEmpty(_section.FailoverDir))
            {
                var file = Path.Combine(_section.FailoverDir, "nacos", "naming", Namespace, "failover", UtilAndComs.FAILOVER_SWITCH);
                var path = Path.GetDirectoryName(file);
                try
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    if (!File.Exists(file))
                        File.WriteAllText(file, "0");
                    System.Environment.SetEnvironmentVariable("JM.SNAPSHOT.PATH", _section.FailoverDir);
                }
                catch { }
            }

            // 是否启用config
            var ret = new ConfigurationBuilder();
            ret.AddNacosV2Configuration(InitConfiguration.GetSection("Nacos"));
            return ret;
        }
    }
}
