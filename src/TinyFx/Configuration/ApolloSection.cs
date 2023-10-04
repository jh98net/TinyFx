using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Core.Utils;
using Com.Ctrip.Framework.Apollo.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;

namespace TinyFx.Configuration
{
    public class ApolloSection
    {
        public static readonly string SectionName = "Apollo";
        public bool Enabled { get; set; }
        public LogLevel LogLevel { get; set; }
        public string AppId { get; set; }
        public string MetaServer { get; set; }
        public List<string> ConfigServer { get; set; }
        public List<string> Namespaces { get; set; }
    }

    public class ApolloCacheMyProvider : ICacheFileProvider
    {
        public Properties Get(string configFile)
        {
            var key = $"APOLLO_CACHE_{Path.GetFileName(configFile)}";
            return CachingUtil.GetOrDefault<Properties>(key, null);
        }

        public void Save(string configFile, Properties properties)
        {
            var key = $"APOLLO_CACHE_{Path.GetFileName(configFile)}";
            CachingUtil.Set(key, properties);
        }
    }
}
