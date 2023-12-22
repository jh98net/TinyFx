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
using TinyFx.Hosting;
using TinyFx.IDGenerator;
using TinyFx.IDGenerator.Common;
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
            HostingUtil.RegisterStarted(async () => IDGeneratorUtil.Init());
            HostingUtil.RegisterStopped(async () => IDGeneratorUtil.WorkerIdProvider.Dispose());
            if (section.UseRedis)
            {
                HostingUtil.RegisterTimer(new Hosting.Services.TinyFxHostTimerItem
                {
                    ExecuteCount = 0,
                    Id = "IDGenerator.Heartbeat",
                    Title = "IDGenerator心跳",
                    Interval = section.RedisExpireMinutes * 60 * 1000 / 3,
                    TryCount = 5,
                    Callback = async (_) => await IDGeneratorUtil.WorkerIdProvider.Active()
                });
            }
            LogUtil.Info($"配置 [IDGenerator]");
            return builder;
        }
    }
}
