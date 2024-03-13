using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System.Diagnostics;
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
        public static IHostBuilder AddRedisEx(this IHostBuilder builder, string connectionStringName = null)
        {
            var section = ConfigUtil.GetSection<RedisSection>();
            var useRedis = section?.ConnectionStrings?.Count > 0;

            var watch = new Stopwatch();
            watch.Start();
            //ConnectionMultiplexer.SetFeatureFlag("preventthreadtheft", true);
            builder.ConfigureServices((context, services) =>
            {
                // 数据保护
                var dpb = services.AddDataProtection()
                    .SetDefaultKeyLifetime(TimeSpan.FromDays(365 * 100)) //密钥生存期
                    .SetApplicationName(ConfigUtil.Project.ApplicationName); // ApplicationName共享
                if (!useRedis)
                {
                    dpb.PersistKeysToFileSystem(new DirectoryInfo(AppContext.BaseDirectory));
                }
                else
                {
                    var connName = string.IsNullOrEmpty(connectionStringName)
                       ? section.DefaultConnectionStringName
                       : connectionStringName;
                    var connStr = !string.IsNullOrEmpty(connName)
                        ? section.ConnectionStrings[connName].ConnectionString
                        : section.ConnectionStrings.First().Value.ConnectionString;
                    // DataProtection
                    dpb.PersistKeysToStackExchangeRedis(RedisUtil.GetRedisByConnectionString(connStr)
                        , "DataProtection-Keys");

                    // 支持IDistributedCache。（AddRequestLimitEx 和 AddRedisSessionEx 需要）
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.ConfigurationOptions = ConfigurationOptions.Parse(connStr);
                        // Key: InstanceName+自己定义的键
                        options.InstanceName = $"{ConfigUtil.Project.ApplicationName}:";
                        //options.InstanceName = $"{ConfigUtil.Project.ProjectId}:"; 
                    });
                    if (section.ConsumerAssemblies?.Count > 0)
                    {
                        services.AddSingleton(sp =>
                        {
                            var ret = new ConsumerContainer(section.ConsumerAssemblies);
                            //redis 资源释放
                            var lifetime = sp.GetService<IHostApplicationLifetime>();
                            lifetime?.ApplicationStopped.Register(() =>
                            {
                                RedisUtil.ReleaseAllRedis();
                            });
                            return ret;
                        });
                    }
                }
            });
            watch.Stop();
            if (useRedis)
            {
                var asm = section.ConsumerAssemblies?.Count > 0
                    ? string.Join('|', section.ConsumerAssemblies)
                    : "NULL";
                LogUtil.Info("配置 => [Redis] ConsumerAssemblies: {ConsumerAssemblies} [{ElapsedMilliseconds} 毫秒]"
                    , asm, watch.ElapsedMilliseconds);
            }
            return builder;
        }
    }
}
