using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Configuration;
using TinyFx.Logging;

namespace TinyFx.Hosting.Services
{
    public class RedisTinyFxHostRegisterService : ITinyFxHostRegisterService
    {
        public string ServiceId { get; }

        private TinyFxHostListDCache _listDCache = new();
        private TinyFxHostDataDCache _dataDCache;
        private TinyFxHostHealthDCache _healthDCache = new();
        public RedisTinyFxHostRegisterService()
        {
            ServiceId = ConfigUtil.ServiceInfo.ServiceId;
            _dataDCache = new TinyFxHostDataDCache(ServiceId);
        }
        public async Task Register()
        {
            LogUtil.Info($"启动 => Host注册[RedisTinyFxHostRegisterService] ServerId:{ServiceId}");
            await _dataDCache.RegisterData();
            await _listDCache.AddAsync(ServiceId);
        }
        public async Task Unregister()
        {
            await _listDCache.RemoveHost(ServiceId);
            await _dataDCache.DeleteData();
            LogUtil.Info($"停止 => 注销Host[RedisTinyFxHostRegisterService] ServerId:{ServiceId}");
        }

        public async Task Heartbeat()
        {
            await _dataDCache.ActiveData();
        }

        public async Task Health()
        {
            await _healthDCache.HealthHosts();
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
