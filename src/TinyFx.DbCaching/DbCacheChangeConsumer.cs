using System;
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
                    var data = await _dataDCache.GetOrLoadAsync(key);
                    if (!data.HasValue)
                        throw new Exception($"DbCacheDataDCache缓存没有值。key:{key}");
                    if (DbCacheUtil.CacheDict.TryGetValue(key, out var value))
                        list.Add(((IDbCacheMemoryUpdate)value, data.Value));
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
