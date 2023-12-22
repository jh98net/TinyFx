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

            builder.ConfigureServices((context, services) =>
            {
                switch (section.PublishMode)
                {
                    case DbCachingPublishMode.Redis:
                        var redisConsumer = new RedisDbCacheChangeConsumer(section.RedisConnectionStringName);
                        redisConsumer.Register();
                        services.AddSingleton(redisConsumer);
                        break;
                    case DbCachingPublishMode.MQ:
                        var mqConsumer = new MQDbCacheChangeConsumer(section.MQConnectionStringName);
                        mqConsumer.Register().ConfigureAwait(false).GetAwaiter().GetResult();
                        services.AddSingleton(mqConsumer);
                        break;
                }
                if (section.RefleshTables?.Count > 0)
                {
                    services.AddHostedService<DbCachingHostedService>();
                }
            });
            var consumer = section.PublishMode == DbCachingPublishMode.Redis ? "RedisDbCacheChangeConsumer" : "MQDbCacheChangeConsumer";
            LogUtil.Info("配置 [DbCaching] ChangeConsumer: {ChangeConsumer}", consumer);
            return builder;
        }
    }
}
