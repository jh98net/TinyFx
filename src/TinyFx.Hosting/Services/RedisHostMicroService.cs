using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Hosting;
using Nacos.V2.Naming.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Net;
using TinyFx.Randoms;

namespace TinyFx.Hosting.Services
{
    public class RedisHostMicroService : ITinyFxHostMicroService
    {
        private RedisSetClient<string> _namesDCache;
        public RedisHostMicroService()
        {
            _namesDCache = RedisUtil.CreateSetClient<string>(RedisHostRegisterService.HOST_NAMES_KEY);
        }

        public async Task<List<string>> GetAllServiceNames()
            => (await _namesDCache.GetAllAsync()).ToList();

        public async Task<string> SelectOneServiceUrl(string serviceName, bool isWebsocket = false)
        {
            string ret = null;
            var idsDCache = RedisUtil.CreateSetClient<string>($"{RedisHostRegisterService.HOST_IDS_KEY}:{serviceName}");
            var serviceIds = (await idsDCache.GetAllAsync()).ToList() ?? new List<string>();
            while (serviceIds.Count > 0)
            {
                var idx = RandomUtil.NextInt(serviceIds.Count);
                var serviceId = serviceIds[idx];
                var dataDCache = new TinyFxHostDataDCache(serviceId);
                var url = await dataDCache.GetAsync<string>("HostUrl");
                if (url.HasValue && !string.IsNullOrEmpty(url.Value))
                {
                    ret = url.Value;
                    break;
                }
                else
                {
                    serviceIds.Remove(serviceId);
                }
            }
            if (!string.IsNullOrEmpty(ret))
            {
                if (isWebsocket)
                    ret = $"ws://{ret}";
                else
                    ret = $"http://{ret}";
            }
            return null;
        }
    }
}
