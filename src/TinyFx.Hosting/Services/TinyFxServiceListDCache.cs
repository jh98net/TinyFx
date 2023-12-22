using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.Hosting.Services
{
    internal class TinyFxServiceListDCache : RedisSetClient<string>
    {
        public TinyFxServiceListDCache()
        {
            RedisKey = $"{RedisPrefixConst.TINYFX_HOSTS}:List";
        }

        /// <summary>
        /// 获取所有服务id
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetAllServices()
        {
            return (await GetAllAsync()).ToList();
        }

        /// <summary>
        /// 移除服务ID
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public async Task RemoveService(string serviceId)
        {
            await RemoveAsync(serviceId);
        }
    }
}
