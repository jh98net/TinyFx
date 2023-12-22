using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.DbCaching;
using TinyFx.DbCaching.ChangeConsumers;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Hosting;
using TinyFx.Logging;

namespace TinyFx
{
    public static class DbCachingHostBuilderExtensions
    {
        public static IHostBuilder AddDbCachingEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<DbCachingSection>();
            if (section == null || !section.Enabled)
                return builder;

            IDbCacheChangeConsumer consumer = null;
            switch (section.PublishMode)
            {
                case DbCachingPublishMode.Redis:
                    consumer = new RedisDbCacheChangeConsumer(section.RedisConnectionStringName);
                    break;
                case DbCachingPublishMode.MQ:
                    consumer = new MQDbCacheChangeConsumer(section.MQConnectionStringName);
                    break;
                default:
                    throw new Exception("未知的DbCachingPublishMode");
            }
            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton(consumer!);
                if (section.RefleshTables?.Count > 0)
                {
                    services.AddHostedService<DbCachingHostedService>();
                }
            });
            HostingUtil.RegisterStarting(async () => 
            {
                await consumer!.RegisterConsumer();
                LogUtil.Info("启动 => 内存缓存[DbCaching]");
            });
            HostingUtil.RegisterStopping(async () => 
            {
                LogUtil.Info("停止 => 内存缓存[DbCaching]");
            });
            LogUtil.Info("配置 => [DbCaching] ChangeConsumer: {ChangeConsumer}", consumer!.GetType().Name);
            return builder;
        }
    }
}
