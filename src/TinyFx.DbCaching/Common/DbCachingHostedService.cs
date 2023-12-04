using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TinyFx.DbCaching
{
    internal class DbCachingHostedService : IHostedService
    {
        private System.Timers.Timer _timer;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = 60000; //1分钟
            _timer.Enabled = true;
            _timer.Elapsed += async (_, _) =>
            {
                var section = ConfigUtil.GetSection<DbCachingSection>();
                if (section.RefleshTables == null || section.RefleshTables.Count == 0)
                    return;
                foreach(var table in section.RefleshTables)
                {
                    try
                    {
                        var key = DbCachingUtil.GetCacheKey(table.ConfigId, table.TableName);
                        if (!DbCachingUtil._cachKeyDict.ContainsKey(key))
                            continue;
                        var nowTs = DateTime.Now.UtcDateTimeToTimestamp(false);
                        if (TimeSpan.FromMilliseconds(nowTs - table.LastExecTime) < TimeSpan.FromMinutes(table.Interval))
                            continue;
                        // load
                        var dataProvider = new PageDataProvider(table.ConfigId, table.TableName, section.RedisConnectionStringName);
                        await dataProvider.SetRedisValues();
                        // remove
                        DbCachingUtil._cachKeyDict.TryRemove(key, out var _);

                        table.LastExecTime = nowTs;
                        LogUtil.Debug($"执行DbCachingHostedService: configId:{table.ConfigId} tableName:{table.TableName} exec:{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}");
                        await Task.Delay(TimeSpan.FromSeconds(10));
                    }
                    catch (Exception ex)
                    {
                        LogUtil.Error(ex, $"执行DbCachingHostedService: configId:{table.ConfigId} tableName:{table.TableName} exec:{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}");
                    }
                }
            };
            _timer.Start();

            LogUtil.Info("DbCachingHostedService服务启动");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Stop();
            _timer.Dispose();
            LogUtil.Debug("DbCachingHostedService服务停止");
            return Task.CompletedTask;
        }
    }
}
