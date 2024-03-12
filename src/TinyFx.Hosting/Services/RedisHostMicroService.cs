using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Randoms;

namespace TinyFx.Hosting.Services
{
    public class RedisHostMicroService : ITinyFxHostMicroService
    {
        private string _connectionStringName;
        private RedisSetClient<string> _namesDCache;
        public RedisHostMicroService(string connectionStringName = null)
        {
            _connectionStringName = connectionStringName;
            _namesDCache = RedisUtil.CreateSetClient<string>(RedisHostRegisterService.HOST_NAMES_KEY, _connectionStringName);
        }

        public async Task<List<string>> GetAllServiceNames()
            => (await _namesDCache.GetAllAsync()).ToList();

        public async Task<string> SelectOneServiceUrl(string serviceName, bool isWebsocket = false)
        {
            string ret = null;
            var idsDCache = RedisUtil.CreateSetClient<string>($"{RedisHostRegisterService.HOST_IDS_KEY}:{serviceName}", _connectionStringName);
            var serviceIds = (await idsDCache.GetAllAsync()).ToList() ?? new List<string>();
            while (serviceIds.Count > 0)
            {
                var idx = RandomUtil.NextInt(serviceIds.Count);
                var serviceId = serviceIds[idx];
                var dataDCache = new TinyFxHostDataDCache(serviceId, _connectionStringName);
                var url = await dataDCache.GetAsync<string>("HostUrl");
                if (url.HasValue && !string.IsNullOrEmpty(url.Value))
                {
                    ret = url.Value;
                    break;
                }
                else
                {
                    serviceIds.RemoveAt(idx);
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
