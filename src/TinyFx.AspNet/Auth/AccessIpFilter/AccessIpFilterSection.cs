using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TinyFx.Collections;
using static System.Collections.Specialized.BitVector32;
using TinyFx.Reflection;
using TinyFx.AspNet;

namespace TinyFx.Configuration
{
    public class AccessIpFilterSection : ConfigSection
    {
        public override string SectionName => "AccessIpFilter";
        public string DefaultFilterName { get; set; }
        public string FiltersProvider { get; set; }
        public Dictionary<string, ApiAccessFilterElement> Filters { get; set; } = new Dictionary<string, ApiAccessFilterElement>();

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            if (!string.IsNullOrEmpty(FiltersProvider))
            {
                var provider = ReflectionUtil.CreateInstance(FiltersProvider) as IAccessIpFiltersProvider;
                if (provider == null)
                    throw new Exception($"配置中AccessIpFilter:FiltersProvider不存在或未实现IAccessIpFiltersProvider: {FiltersProvider}");
                var list = provider.Build();
                list.ForEach(x => Filters.Add(x.Name, x));
            }
            else
            {
                Filters = configuration.GetSection("Filters")
                    .Get<Dictionary<string, ApiAccessFilterElement>>() ?? new();
                Filters.ForEach(x =>
                {
                    x.Value.Name = x.Key;
                });
            }
        }
    }
    public class ApiAccessFilterElement
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        /// <summary>
        /// 是否允许内网访问
        /// </summary>
        public bool EnableIntranet { get; set; } = true;
        public string AllowIps { get; set; }

        private HashSet<string> _allowIpDict;
        public HashSet<string> GetAllowIpDict()
        {
            if (_allowIpDict != null)
                return _allowIpDict;
            var ret = new HashSet<string>();
            if (!string.IsNullOrEmpty(AllowIps))
            {
                var ips = AllowIps.Split('|', ';', ',');
                foreach (var ip in ips)
                {
                    if (!ret.Contains(ip))
                        ret.Add(ip);
                }
            }
            _allowIpDict = ret;
            return ret;
        }
    }
}
