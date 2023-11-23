using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.DbCaching;

namespace TinyFx
{
    public static class DbCachingHostBuilderExtensions
    {
        public static IHostBuilder UseDbCachingEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<DbCachingSection>();
            if (section != null && section.Enabled)
            {
                builder.ConfigureServices((context, services) =>
                {
                    var consumer = new DbCacheChangeConsumer(section.RedisConnectionStringName);
                    consumer.Register();
                    services.AddSingleton(consumer);
                });
            }
            return builder;
        }
    }
}
