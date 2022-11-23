using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Randoms;

namespace TinyFx.Configuration
{
    public class RandomSection : ConfigSection
    {
        public override string SectionName => "Random";
        public string DefaultProviderName { get; set; }
        public Dictionary<string, RandomProviderElement> Providers { get; set; }
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            Providers = BindDictionary<RandomProviderElement>(configuration, "Providers");
        }
    }
    public class RandomProviderElement : IOnlyKeyConfigElement
    {
        public string Name { get; set; }
        public string RandomType { get; set; }
        public SamplingOptions Options { get; set; }
        public string GetConfigElementKey() => Name;
    }
}
