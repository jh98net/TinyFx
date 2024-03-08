using Nacos.V2.Naming.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Configuration;

namespace TinyFx.Hosting.Services
{
    public class RedisHostDataService: ITinyFxHostDataService
    {
        public string ServiceId { get; }
        private TinyFxHostDataDCache _dataDCache;
        public RedisHostDataService()
        {
            ServiceId = ConfigUtil.ServiceInfo.ServiceId;
            _dataDCache = new TinyFxHostDataDCache(ServiceId);
        }
        public async Task SetData<T>(string field, T value)
        {
            await _dataDCache.SetData(field, value);
        }

        public async Task<CacheValue<T>> GetData<T>(string field)
        {
            return await _dataDCache.GetData<T>(field);
        }

        public async Task<List<string>> GetHosts(string connectionStringName = null)
        {
            var ret = new List<string>();
            var listDCache = new TinyFxHostListDCache(connectionStringName);
            var hosts = await listDCache.GetAllAsync();
            foreach (var serviceId in hosts.ToList())
            {
                var isValid = await new TinyFxHostDataDCache(serviceId, connectionStringName).IsValid();
                if (isValid)
                    ret.Add(serviceId);
                else
                    await listDCache.RemoveHost(serviceId);
            }
            return ret;
        }

        public Task SetHostData<T>(string serviceId, string field, T value, string connectionStringName = null)
        {
            var dataDCache = new TinyFxHostDataDCache(serviceId, connectionStringName);
            return dataDCache.SetData<T>(field, value);
        }

        public Task<CacheValue<T>> GetHostData<T>(string serviceId, string field, string connectionStringName = null)
        {
            var dataDCache = new TinyFxHostDataDCache(serviceId, connectionStringName);
            return dataDCache.GetData<T>(field);
        }
    }
}
