using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Collections;
using TinyFx.DbCaching.Caching;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Hosting;

namespace TinyFx.DbCaching.ChangeConsumers
{
    [RedisConsumerRegisterIgnore]
    internal class RedisDbCacheCheckConsumer : RedisSubscribeConsumer<DbCacheCheckMessage>
    {
        protected override async Task OnMessage(DbCacheCheckMessage message)
        {
            var list = new List<DbCachingCheckItem>();
            var listDCache = new DbCacheListDCache(message.RedisConnectionStringName);
            foreach (var dict in DbCachingUtil.CacheDict.Values)
            {
                dict.Values.ForEach(async (x) =>
                {
                    var cacheData = ((IDbCacheMemoryUpdate)x).RedisData;
                    var key = DbCachingUtil.GetCacheKey(cacheData.ConfigId, cacheData.TableName);
                    var redisData = (await listDCache.GetAsync(key)).Value;
                    if (redisData.DataHash != cacheData.DataHash || redisData.UpdateDate != cacheData.UpdateDate)
                    {
                        list.Add(new DbCachingCheckItem
                        {
                            ConfigId = cacheData.ConfigId,
                            TableName = cacheData.TableName,
                            RedisHash = redisData.DataHash,
                            CacheHash = cacheData.DataHash,
                            RedisUpdate = redisData.UpdateDate,
                            CacheUpdate = cacheData.UpdateDate,
                        });
                    }
                });
            }
            await HostingUtil.SetHostData(DbCachingUtil.DB_CACHING_CHECK_KEY, list);
        }
        protected override Task OnError(DbCacheCheckMessage message, Exception ex)
        {
            return Task.CompletedTask;
        }
    }
}
