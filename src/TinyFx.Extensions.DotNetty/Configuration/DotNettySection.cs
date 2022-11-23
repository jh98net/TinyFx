using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TinyFx.Configuration;
using TinyFx.Extensions.DotNetty;

namespace TinyFx.Configuration
{
    public class DotNettySection : ConfigSection
    {
        public override string SectionName => "DotNetty";
        public ServerOptions Server { get; set; }
        public Dictionary<string, ClientOptions> Clients = new Dictionary<string, ClientOptions>();

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            if (Server.Assemblies == null || Server.Assemblies.Count == 0)
                Server.Assemblies = new List<string>() { 
                    $"{Path.GetFileName(Assembly.GetEntryAssembly().Location)}"
                };
            //if (Server != null && Server.Port == 0)
            //{
            //    var port = Environment.GetEnvironmentVariable("ENV_PORT");
            //    if (!string.IsNullOrEmpty(port))
            //        Server.Port = port.ToInt32();
            //    else throw new Exception("请配置DotNetty:Server:Port监听端口或者配置环境变量ENV_PORT");
            //}
            Clients = BindDictionary<ClientOptions>(configuration, "Clients");
        }
    }
}
