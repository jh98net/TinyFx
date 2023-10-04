using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Collections;

namespace TinyFx.Configuration
{
    public class NacosSection : ConfigSection
    {
        public override string SectionName => "Nacos";
        public bool Enabled { get; set; }
        /// <summary>
        /// 故障转移目录
        /// </summary>
        public string FailoverDir { get; set; }

        public string ServiceName { get; set; }
        public string GroupName { get; set; }
        public string ClusterName { get; set; }

        public Dictionary<string, NacosClientElement> Clients { get; set; }
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            if (Clients?.Count > 0)
            {
                Clients.ForEach(x =>
                {
                    if (string.IsNullOrEmpty(x.Value.ServiceName))
                        x.Value.ServiceName = x.Key;
                    if (string.IsNullOrEmpty(x.Value.GroupName))
                        x.Value.GroupName = GroupName;
                    if ((x.Value.Clusters == null || x.Value.Clusters.Count == 0)
                        && !string.IsNullOrEmpty(ClusterName))
                        x.Value.Clusters = new List<string> { ClusterName };
                });
            }
        }
    }
    public class NacosClientElement
    {
        public string ServiceName { get; set; }
        public string GroupName { get; set; }
        public List<string> Clusters { get; set; }
        public bool Subscribe { get; set; }
    }
}
