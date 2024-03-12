using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.Hosting.Services
{
    public class RedisHostRegDataService : ITinyFxHostRegDataService
    {
        public async Task<List<string>> GetAllServiceIds(string connectionStringName = null)
        {
            var ret = new List<string>();
            var namesDCache = RedisUtil.CreateSetClient<string>(RedisHostRegisterService.HOST_NAMES_KEY, connectionStringName);
            var serviceNames = (await namesDCache.GetAllAsync()).ToList();
            foreach (var serviceName in serviceNames)
            {
                var idsDCache = RedisUtil.CreateSetClient<string>($"{RedisHostRegisterService.HOST_IDS_KEY}:{serviceName}", connectionStringName);
                var serviceIds = (await idsDCache.GetAllAsync()).ToList();
                foreach (var serviceId in serviceIds)
                {
                    var isValid = await new TinyFxHostDataDCache(serviceId, connectionStringName).IsValid();
                    if (!isValid)
                        await idsDCache.RemoveAsync(serviceId);
                    else
                        ret.Add(serviceId);
                }
            }
            return ret;
        }
        public async Task<List<string>> GetServiceIds(string serviceName = null, string connectionStringName = null)
        {
            serviceName ??= ConfigUtil.Project.ProjectId;
            if (string.IsNullOrEmpty(serviceName))
                throw new Exception($"RedisHostRegDataService.GetServiceIds时serviceName不能为空。serviceName:{serviceName}");
            var idsDCache = RedisUtil.CreateSetClient<string>($"{RedisHostRegisterService.HOST_IDS_KEY}:{serviceName}", connectionStringName);
            return (await idsDCache.GetAllAsync()).ToList();
        }

        public async Task SetHostData<T>(string key, T value, string serviceId = null, string connectionStringName = null)
        {
            serviceId ??= ConfigUtil.ServiceInfo.ServiceId;
            if (string.IsNullOrEmpty(serviceId) || string.IsNullOrEmpty(key))
                throw new Exception($"RedisHostRegDataService.SetHostData时ServiceId和Key不能为空。serviceId:{serviceId} key:{key}");
            var dcache = new TinyFxHostDataDCache(serviceId, connectionStringName);
            await dcache.SetData(key, value);
        }
        public async Task<CacheValue<T>> GetHostData<T>(string key, string serviceId = null, string connectionStringName = null)
        {
            serviceId ??= ConfigUtil.ServiceInfo.ServiceId;
            if (string.IsNullOrEmpty(serviceId) || string.IsNullOrEmpty(key))
                throw new Exception($"RedisHostRegDataService.GetHostData时ServiceId和Key不能为空。serviceId:{serviceId} key:{key}");
            var dcache = new TinyFxHostDataDCache(serviceId, connectionStringName);
            return await dcache.GetData<T>(key);
        }
    }
}
