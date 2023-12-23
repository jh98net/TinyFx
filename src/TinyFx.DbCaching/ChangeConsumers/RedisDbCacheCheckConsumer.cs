using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var sb = new StringBuilder();
            var listDCache = new DbCacheListDCache(message.RedisConnectionStringName);
            foreach (var item in DbCachingUtil.CacheDict.Values)
            {
                var cacheData = ((IDbCacheMemoryUpdate)item).RedisData;
                var key = DbCachingUtil.GetCacheKey(cacheData.ConfigId, cacheData.TableName);
                var redisData = (await listDCache.GetAsync(key)).Value;
                if (redisData.DataHash != cacheData.DataHash)
                {
                    sb.AppendLine($"DataHash不同：ConfigId:{cacheData.ConfigId} TableName:{cacheData.TableName} RedisHash:{redisData.DataHash} CacheHash:{cacheData.DataHash}");
                }
                if (redisData.UpdateDate != cacheData.UpdateDate)
                {
                    sb.AppendLine($"UpdateDate不同：ConfigId:{cacheData.ConfigId} TableName:{cacheData.TableName} RedisUpdateDate:{redisData.UpdateDate} CacheUpdateDate:{cacheData.UpdateDate}");
                }
            }
            await HostingUtil.SetHostData(DbCachingUtil.DB_CACHING_CHECK_KEY, sb.ToString());
        }
        protected override Task OnError(DbCacheCheckMessage message, Exception ex)
        {
            return Task.CompletedTask;
        }
    }
}
