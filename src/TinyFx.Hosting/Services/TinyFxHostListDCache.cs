using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.Hosting.Services
{
    internal class TinyFxHostListDCache : RedisSetClient<string>
    {
        public TinyFxHostListDCache()
        {
            RedisKey = $"{RedisPrefixConst.HOSTS}:List";
        }

        /// <summary>
        /// 获取所有服务id
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetAllHosts()
        {
            return (await GetAllAsync()).ToList();
        }

        public async Task<bool> AddHost(string serviceId)
        {
            return await AddAsync(serviceId);
        }

        /// <summary>
        /// 移除服务ID
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public async Task RemoveHost(string serviceId)
        {
            await RemoveAsync(serviceId);
        }

    }
}
