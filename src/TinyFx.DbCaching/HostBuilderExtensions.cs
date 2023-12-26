using Microsoft.AspNetCore.DataProtection.KeyManagement;
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
using TinyFx.Hosting.Services;
using TinyFx.Logging;
using static System.Collections.Specialized.BitVector32;

namespace TinyFx
{
    public static class DbCachingHostBuilderExtensions
    {
        public static IHostBuilder AddDbCachingEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<DbCachingSection>();
            if (section == null || !section.Enabled)
                return builder;

            var checkConsumer = new RedisDbCacheCheckConsumer();
            IDbCacheChangeConsumer changeConsumer = null;
            switch (section.PublishMode)
            {
                case DbCachingPublishMode.Redis:
                    changeConsumer = new RedisDbCacheChangeConsumer(section.RedisConnectionStringName);
                    break;
                case DbCachingPublishMode.MQ:
                    changeConsumer = new MQDbCacheChangeConsumer(section.MQConnectionStringName);
                    break;
                default:
                    throw new Exception("未知的DbCachingPublishMode");
            }
            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton(changeConsumer!);
                services.AddSingleton(checkConsumer);
            });
            HostingUtil.RegisterStarting(async () =>
            {
                await changeConsumer!.RegisterConsumer();
                checkConsumer.Register();
                LogUtil.Info("启动 => 内存缓存[DbCaching]");
            });
            HostingUtil.RegisterStopping(async () =>
            {
                LogUtil.Info("停止 => 内存缓存[DbCaching]");
            });
            if (section.RefleshTables != null && section.RefleshTables.Count > 0)
            {
                foreach (var table in section.RefleshTables)
                {
                    var key = DbCachingUtil.GetCacheKey(table.ConfigId, table.TableName);
                    HostingUtil.RegisterTimer(new TinyFxHostTimerItem
                    {
                        Id = $"RefleshTables:{key}",
                        Title = "自动刷新内存缓存",
                        Interval = (int)TimeSpan.FromMinutes(table.Interval).TotalMilliseconds,
                        Callback = async (_) =>
                        {
                            var dataProvider = new PageDataProvider(table.ConfigId, table.TableName, section.RedisConnectionStringName);
                            await dataProvider.SetRedisValues();
                            // remove
                            DbCachingUtil.CachKeyDict.TryRemove(key, out var _);
                        }
                    });
                }
            }
            LogUtil.Info("配置 => [DbCaching] ChangeConsumer: {ChangeConsumer}", changeConsumer!.GetType().Name);
            return builder;
        }
    }
}
