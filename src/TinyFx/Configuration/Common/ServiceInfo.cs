using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Text;

namespace TinyFx.Configuration
{
    public class ServiceInfo
    {
        /// <summary>
        /// 服务启动时分配的GUID
        /// </summary>
        public string ServiceGuid { get; }

        /// <summary>
        /// 主机IP
        /// </summary>
        public string HostIp { get; set; }
        /// <summary>
        /// 主机端口
        /// </summary>
        public int HostPort { get; set; }
        /// <summary>
        /// 服务的唯一标识，默认: projectId|guid
        /// </summary>
        public string ServiceId { get; internal set; }
        /// <summary>
        /// 服务外部访问地址
        /// </summary>
        public string ServiceUrl { get; set; }
        public ServiceInfo()
        {
            ServiceGuid = ObjectId.NewId();
        }
    }
}
