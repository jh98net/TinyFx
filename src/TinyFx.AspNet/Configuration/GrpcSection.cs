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
        /// <summary>
        /// 端口，取环境变量ENV_GRPC_PORT或HostPort+10000
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Grpc服务类所在Assembly。见:https://github.com/protobuf-net/protobuf-net.Grpc
        /// </summary>
        public List<string> Assemblies { get; set; } = new();

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
        }
    }
}
