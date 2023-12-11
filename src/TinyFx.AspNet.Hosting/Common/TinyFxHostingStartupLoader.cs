﻿using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.RabbitMQ;
using TinyFx.Logging;
using TinyFx.Reflection;
using static System.Collections.Specialized.BitVector32;

namespace TinyFx.AspNet.Hosting
{
    public class TinyFxHostingStartupLoader
    {
        public static TinyFxHostingStartupLoader Instance = new();
        private List<ITinyFxHostingStartup> _startups = new();
        private TinyFxHostingStartupLoader()
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section?.HostingStartupAssemblies?.Any() ?? false)
            {
                foreach (var asm in section.HostingStartupAssemblies)
                {
                    if (string.IsNullOrEmpty(asm)) continue;
                    var msg = $"加载配置文件AspNet:HostingStartupAssemblies中项失败。name:{asm}";
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
        }

        public void ConfigureServices(WebApplicationBuilder webApplicationBuilder)
        {
            if (_startups.Count == 0)
                return;
            var asms = ConfigUtil.GetSection<AspNetSection>()?.HostingStartupAssemblies;
            var asmStr =  string.Join('|', asms);
            LogUtil.Info($"注册 TinyFxHostingStartupLoader。HostingStartupAssemblies:{asmStr}");
            _startups.ForEach(x => x.ConfigureServices(webApplicationBuilder));
        }

        public void Configure(WebApplication webApplication)
        {
            if (_startups.Count == 0)
                return;
            _startups.ForEach(x => x.Configure(webApplication));
        }
    }
}
