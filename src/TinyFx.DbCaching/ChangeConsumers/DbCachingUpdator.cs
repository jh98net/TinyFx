using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Collections;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Logging;

namespace TinyFx.DbCaching.ChangeConsumers
{
    internal class DbCachingUpdator
    {
        private DbCachingPublishMode _mode;
        public DbCachingUpdator(DbCachingPublishMode mode)
        {
            _mode = mode;
        }
        public async Task Execute(DbCacheChangeMessage message)
        {
            var logger = new LogBuilder(Microsoft.Extensions.Logging.LogLevel.Warning, "内存缓存更新通知消费")
                .AddField("DbCaching.PublishMode", _mode.ToString())
                .AddField("DbCaching.Message", SerializerUtil.SerializeJson(message));
            var list = new List<(IDbCacheMemoryUpdate cache, List<string> datas)>();
            try
            {
                foreach (var item in message.Changed)
                {
                    logger.AddMessage($"开始更新: {item.ConfigId}.{item.TableName}");
                    List<string> redisValues = null;
                    var key = DbCachingUtil.GetCacheKey(item.ConfigId, item.TableName);
                    var dataProvider = new PageDataProvider(item.ConfigId, item.TableName);
                    // 等3分钟，1秒申请一次
                    using (var redLock = await RedisUtil.LockAsync($"RedisDbCacheChangeConsumer:{key}", 180))
                    {
                        if (!redLock.IsLocked)
                            throw new Exception($"DbCacheDataDCache获取缓存锁超时。key:{key}");
                        redisValues = await dataProvider.GetRedisValues();
                    }
                    if (DbCachingUtil.CacheDict.TryGetValue(key, out var dict))
                    {
                        dict.Values.ForEach(x =>
                        {
                            list.Add(((IDbCacheMemoryUpdate)x, redisValues));
                        });
                    }
                }
                list.ForEach(x => x.cache.BeginUpdate(x.datas));
                list.ForEach(x =>
                {
                    x.cache.EndUpdate();
                    logger.AddMessage($"更新成功: {x.cache.ConfigId}.{x.cache.TableName} count:{x.cache.DbDataCount}");
                });
            }
            catch (Exception ex)
            {
                logger.AddMessage("内存缓存更新通知消费异常")
                    .AddException(ex);
            }
            logger.Save();
        }
    }
}
