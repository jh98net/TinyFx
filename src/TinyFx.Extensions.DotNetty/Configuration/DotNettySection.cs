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

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            if (Server.Assemblies == null || Server.Assemblies.Count == 0)
                Server.Assemblies = new List<string>() { 
                    $"{Path.GetFileName(Assembly.GetEntryAssembly().Location)}"
                };
        }
    }
}
