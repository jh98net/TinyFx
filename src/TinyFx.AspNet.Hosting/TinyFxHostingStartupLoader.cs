using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.RabbitMQ;
using TinyFx.Reflection;

namespace TinyFx.AspNet.Hosting
{
    public class TinyFxHostingStartupLoader
    {
        private bool _init = false;
        private List<ITinyFxHostingStartup> _startups;
        public TinyFxHostingStartupLoader()
        {
            _startups = new();
        }
        private void Init()
        {
            if(_init) return;
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section?.HostingStartupAssemblies?.Any() ?? false)
            {
                foreach (var asm in section.HostingStartupAssemblies)
                {
                    if (string.IsNullOrEmpty(asm)) continue;
                    var msg = $"加载配置文件AspNet:ConsumerAssemblies中项失败。name:{asm}";
                    var ignoreAssemblyError = asm.StartsWith('+');
                    var file = asm.TrimStart('+');
                    var types = from t in ReflectionUtil.GetAssemblyTypes(file, ignoreAssemblyError, msg)
                                where t.IsSubclassOfGeneric(typeof(ITinyFxHostingStartup))
                                select t;
                    foreach (var type in types)
                    {
                        _startups.Add((ITinyFxHostingStartup)ReflectionUtil.CreateInstance(type));
                    }
                }
            }
            _init = true;
        }

        public void ConfigureServices(WebApplicationBuilder webApplicationBuilder)
        {
            Init();
            _startups.ForEach(x => x.ConfigureServices(webApplicationBuilder));
        }

        public void Configure(WebApplication webApplication)
        {
            Init();
            _startups.ForEach(x => x.Configure(webApplication));
        }
    }
}
