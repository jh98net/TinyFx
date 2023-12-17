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
        private IConfiguration _nacosConfig;
        public NacosSection _section;
        public bool Enabled => _section.Enabled;
        public string Namespace =>_section.Namespace;
        public NacosConfigSourceProvider(IConfiguration config) : base(config)
        {
            _nacosConfig = config.GetSection("Nacos");
            if(_nacosConfig != null)
            {
                NacosUtil.Section.Bind(_nacosConfig);
            }
            _section = NacosUtil.Section;
        }

        public string GetServerAddresses()
            => string.Join('|', _section.ServerAddresses);
        
        public override IConfigurationBuilder CreateBuilder(IHostBuilder hostBuilder)
        {
            if (!Enabled) return null;
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
            ret.AddNacosV2Configuration(_nacosConfig);
            return ret;
        }
    }
}
