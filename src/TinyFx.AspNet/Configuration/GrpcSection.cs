using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration
{
    public class GrpcSection : ConfigSection
    {
        public override string SectionName => "Grpc";
        public bool Enabled { get; set; }
        public int Port { get; set; }
        public List<string> Assemblies { get; set; } = new();

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
        }
    }
}
