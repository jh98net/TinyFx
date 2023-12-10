using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.IDGenerator;
using TinyFx.Extensions.IDGenerator.Common;
using TinyFx.Logging;

namespace TinyFx
{
    public static class IDGeneratorHostBuilderExtensions
    {
        public static IHostBuilder AddIDGenerator(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<IDGeneratorSection>();
            if (section == null || !section.Enabled)
                return builder;

            if (section.UseRedis)
            {
                var redisSecion = ConfigUtil.GetSection<RedisSection>();
                if (redisSecion == null)
                    throw new Exception("启动IDGenerator必须启用Redis");
                if (string.IsNullOrEmpty(section.RedisConnectionStringName))
                    section.RedisConnectionStringName = redisSecion.DefaultConnectionStringName;
                if (!redisSecion.ConnectionStrings.ContainsKey(section.RedisConnectionStringName))
                    throw new Exception($"启动IDGenerator时不存在redisConnectionName: {section.RedisConnectionStringName}");
            }

            IDGeneratorUtil.Init();
            if (section.UseRedis)
            {
                builder.ConfigureServices((ctx, services) =>
                {
                    services.AddHostedService<IDGeneratorHostedService>();
                });
            }
            LogUtil.Debug($"IDGenerator 配置启动");
            return builder;
        }
    }
}
