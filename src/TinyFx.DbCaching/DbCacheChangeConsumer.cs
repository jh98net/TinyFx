using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Collections;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Logging;

namespace TinyFx.DbCaching
{
    [RedisConsumerRegisterIgnore]
    internal class DbCacheChangeConsumer : RedisSubscribeConsumer<DbCacheChangeMessage>
    {
        public DbCacheChangeConsumer(string redisConnectionStringName)
        {
            ConnectionStringName = redisConnectionStringName;
        }
        protected override async Task OnMessage(DbCacheChangeMessage message)
        {
            LogUtil.Warning($"DbCacheChangeConsumer: 收到更新内存通知: {string.Join(", ",message.Changed.Select(x=>x.TableName))}");
            var list = new List<(IDbCacheMemoryUpdate cache, List<string> datas)>();
            try
            {
                foreach (var item in message.Changed)
                {
                    LogUtil.Warning($"DbCacheChangeConsumer:开始更新 {item.ConfigId}.{item.TableName}");
                    List<string> redisValues = null;
                    var key = DbCachingUtil.GetCacheKey(item.ConfigId, item.TableName);
                    var dataProvider = new PageDataProvider(item.ConfigId, item.TableName);
                    // 等3分钟，1秒申请一次
                    using (var redLock = await RedisUtil.LockAsync($"DbCacheChangeConsumer:{key}", 180))
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
                list.ForEach(x => x.cache.EndUpdate());
            }
            catch (Exception ex)
            {
                var log = new LogBuilder<DbCacheChangeConsumer>();
                log.AddMessage("处理内存缓存变更消息时出现异常");
                log.AddField("DbCacheChangeMessage", SerializerUtil.SerializeJson(message));
                log.AddException(ex);
                log.Save();
            }
        }
        protected override Task OnError(DbCacheChangeMessage message, Exception ex)
        {
            return Task.CompletedTask;
        }
    }
    internal interface IDbCacheMemoryUpdate
    {
        void BeginUpdate(List<string> datas);
        void EndUpdate();
    }
}
