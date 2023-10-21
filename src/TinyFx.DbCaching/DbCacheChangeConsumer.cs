﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Logging;

namespace TinyFx.DbCaching
{
    internal class DbCacheChangeConsumer : RedisSubscribeConsumer<DbCacheChangeMessage>
    {
        private DbCacheDataDCache _dataDCache = new();
        protected override async Task OnMessage(DbCacheChangeMessage message)
        {
            var list = new List<(IDbCacheMemoryUpdate cache, string data)>();
            try
            {
                foreach (var item in message.Changed)
                {
                    var key = DbCacheUtil.GetCacheKey(item.ConfigId, item.TableName);
                    string redisValue = null;
                    // 等3分钟，1秒申请一次
                    using (var redLock = await RedisUtil.LockWaitAsync($"DbCacheChangeConsumer:{key}", 180, 1000))
                    {
                        if (redLock.IsLocked)
                        {
                            var data = await _dataDCache.GetOrLoadAsync(key);
                            if (!data.HasValue)
                                throw new Exception($"DbCacheDataDCache缓存没有值。key:{key}");
                            redisValue = data.Value;
                        }
                        else
                            throw new Exception($"DbCacheDataDCache获取缓存锁超时。key:{key}");
                    }
                    if (DbCacheUtil.CacheDict.TryGetValue(key, out var value))
                        list.Add(((IDbCacheMemoryUpdate)value, redisValue));
                }
                list.ForEach(x => x.cache.BeginUpdate(x.data));
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
        void BeginUpdate(string data);
        void EndUpdate();
    }
}
