using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.Hosting.Services
{
    internal class TinyFxHostHealthDCache : RedisStringClient<long>
    {
        private int HEALTH_INTERVAL;
        private TinyFxHostListDCache _listDCache = new();
        public TinyFxHostHealthDCache()
        {
            RedisKey = $"{RedisPrefixConst.HOSTS}:Health";
            HEALTH_INTERVAL = ConfigUtil.Host.HeathInterval;
        }
        public async Task HealthHosts()
        {
            // 检查
            var lastTs = await GetOrDefaultAsync(0);
            var utcTs = DateTime.UtcNow.UtcDateTimeToTimestamp(false);
            if (utcTs - lastTs < HEALTH_INTERVAL)
                return;
            await SetAsync(utcTs);

            //
            var list = new List<string>();
            foreach (var serviceId in await _listDCache.GetAllAsync())
            {
                var isValid = await new TinyFxHostDataDCache(serviceId).IsValid();
                if (!isValid)
                    list.Add(serviceId);
            }
            foreach (var serviceId in list)
            {
                await _listDCache.RemoveHost(serviceId);
            }
        }


    }
}
