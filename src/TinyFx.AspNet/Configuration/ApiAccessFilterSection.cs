using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration
{
    public class ApiAccessFilterSection : ConfigSection
    {
        public override string SectionName => "ApiAccessFilter";
        public string DefaultFilterName { get; set; }
        public Dictionary<string, ApiAccessFilterElement> Filters { get; set; } = new Dictionary<string, ApiAccessFilterElement>();

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            var filters = configuration.GetSection("Filters").Get<ApiAccessFilterElement[]>();
            foreach (var filter in filters)
            {
                if (Filters.ContainsKey(filter.Name))
                    throw new Exception($"配置中ApiAccessFilter:Filters:Name重复。name:{filter.Name}");
                Filters.Add(filter.Name, filter);
                if (!string.IsNullOrEmpty(filter.AllowIps))
                {
                    var ips = filter.AllowIps.Split('|', ';');
                    foreach (var ip in ips)
                    {
                        if (!filter.AllowIpsDic.Contains(ip))
                            filter.AllowIpsDic.Add(ip);
                    }
                }
            }
        }
    }
    public class ApiAccessFilterElement
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string AllowIps { get; set; }
        public HashSet<string> AllowIpsDic { get; private set; } = new HashSet<string>();
    }
}
