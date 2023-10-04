using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TinyFx.Configuration
{
    public class AppConfigsSection : ConfigSection
    {
        public override string SectionName => "AppConfigs";
        private Dictionary<string, IConfigurationSection> _configsDict = new Dictionary<string, IConfigurationSection>();

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            var items = configuration.GetChildren().ToList();
            foreach (var item in items)
            {
                _configsDict.Add(item.Key, item);
            }
        }

        public T Get<T>(string key) where T : new()
        {
            if (!_configsDict.TryGetValue(key, out IConfigurationSection section))
                throw new Exception($"配置文件中AppConfigs:Key {key} 不存在");
            return section.Get<T>();
        }

        public T GetOrDefault<T>(string key, T defaultValue = default)
            => _configsDict.TryGetValue(key, out IConfigurationSection section)
            ? section.Get<T>() : defaultValue;
    }
}
