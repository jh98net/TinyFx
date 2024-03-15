using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration.Common;
using TinyFx.Text;

namespace TinyFx.Configuration
{
    /// <summary>
    /// Host注册信息
    ///     ENV_HOST_IP => HostIp
    ///     ENV_HOST_PORT => HostPort = HttpPort = GrpcPort+1
    /// </summary>
    public class ServiceInfo
    {
        #region Host
        /// <summary>
        /// 注册主机IP
        /// </summary>
        public string HostIp { get; set; }
        /// <summary>
        /// 注册主机端口
        /// </summary>
        public int HostPort { get; set; }
        public bool HostSecure { get; set; }
        #endregion

        public int HttpPort { get; set; }
        public int GrpcPort { get; set; }

        /// <summary>
        /// 服务的唯一标识，默认: projectId:guid
        /// </summary>
        public string ServiceId { get; internal set; }

        /// <summary>
        /// 服务外部访问地址
        /// </summary>
        public string ServiceUrl => HostSecure
            ? $"https://{HostIp}:{HostPort}"
            : $"http://{HostIp}:{HostPort}";

        public ServiceInfo(EnvironmentInfo env)
        {
            HostIp = new HostIpGetter(env).Get();

            var portGetter = new HostPortGetter(env);
            HostPort = portGetter.GetHostPort();
            HttpPort = portGetter.GetHttpPort();
            GrpcPort = portGetter.GetGrpcPort();
        }
    }
}
