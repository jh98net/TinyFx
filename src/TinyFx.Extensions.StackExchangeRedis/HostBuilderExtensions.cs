using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Logging;

namespace TinyFx
{
    public static class StackExchangeRedisHostBuilderExtensions
    {
        /// <summary>
        /// 注册Redis分布式缓存：IDistributedCache
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="connectionStringName">IDistributedCache使用的连接名称</param>
        /// <returns></returns>
        public static IHostBuilder UseRedisEx(this IHostBuilder builder, string connectionStringName = null)
        {
            var section = ConfigUtil.GetSection<RedisSection>();
            if (section != null && section.ConnectionStrings != null && section.ConnectionStrings.Count > 0)
            {
                //ConnectionMultiplexer.SetFeatureFlag("preventthreadtheft", true);
                builder.ConfigureServices((context, services) =>
                {
                    // 支持IDistributedCache。（AddRequestLimitEx 和 AddRedisSessionEx 需要）
                    services.AddStackExchangeRedisCache(options =>
                    {
                        var name = string.IsNullOrEmpty(connectionStringName) ? section.DefaultConnectionStringName : connectionStringName;
                        var connStr = section.ConnectionStrings[name].ConnectionString;
                        options.ConfigurationOptions = ConfigurationOptions.Parse(connStr);
                        options.InstanceName = $"{ConfigUtil.Project.ProjectId}:";
                    });
                    services.AddSingleton((sp)=> new ConsumerContainer(section.ConsumerAssemblies));
                });
                //redis 资源释放
                TinyFxHost.RegisterOnStopped(()=> 
                {
                    RedisUtil.ReleaseAllRedis();
                    return Task.CompletedTask;
                });
            }
            LogUtil.Trace("Redis 配置启动");
            return builder;
        }
    }
}
