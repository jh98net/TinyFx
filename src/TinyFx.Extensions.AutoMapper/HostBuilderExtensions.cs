using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;
using TinyFx.Extensions.AutoMapper;
using TinyFx.Logging;

namespace TinyFx
{
    public static class AutoMapperHostBuilderExtensions
    {
        public static IHostBuilder AddAutoMapperEx(this IHostBuilder builder) 
        {
            var section = ConfigUtil.GetSection<AutoMapperSection>();
            if (section == null || section.Assemblies == null || section.Assemblies.Count == 0)
                return builder;

            builder.ConfigureServices((context, services) => {
                var registered = AutoMapperUtil.Register();
                if (registered)
                {
                    services.TryAddSingleton(AutoMapperUtil.Expression);
                    services.TryAddSingleton(AutoMapperUtil.Configuration);
                    services.TryAddSingleton(AutoMapperUtil.Mapper);
                }
            });
            LogUtil.Info($"AutoMapper 配置完成。Assemblies:{string.Join('|', section.Assemblies)}");
            return builder;
        }
    }
}