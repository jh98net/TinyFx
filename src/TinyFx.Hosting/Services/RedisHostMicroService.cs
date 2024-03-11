using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Net;

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
            var section = ConfigUtil.GetSection<JsonHttpClientSection>();
            if (section?.Clients?.TryGetValue(serviceName, out var element) ?? false)
            {
                return element.BaseAddress;
            }
            return null;
        }
    }
}
