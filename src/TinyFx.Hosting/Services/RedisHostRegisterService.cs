using Nacos.V2.Naming.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx.Hosting.Services
{
    public class RedisHostRegisterService : ITinyFxHostRegisterService
    {
        public const string HOST_NAMES_KEY = $"{RedisPrefixConst.HOSTS}:ServiceNames";
        public const string HOST_IDS_KEY = $"{RedisPrefixConst.HOSTS}:ServiceIds";
        public const string HOST_HEALTH_KEY = $"{RedisPrefixConst.HOSTS}:Health";

        public string ServiceName { get; }
        public string ServiceId { get; }
        private int HEALTH_INTERVAL;

        private RedisSetClient<string> _namesDCache;
        private RedisSetClient<string> _idsDCache;
        private RedisStringClient<long> _healthDCache;
        private TinyFxHostDataDCache _dataDCache;
        public RedisHostRegisterService()
        {
            ServiceName = ConfigUtil.Project.ProjectId;
            ServiceId = ConfigUtil.ServiceInfo.ServiceId;
            HEALTH_INTERVAL = ConfigUtil.Host.HeathInterval;

            _namesDCache = RedisUtil.CreateSetClient<string>(HOST_NAMES_KEY);
            _idsDCache = RedisUtil.CreateSetClient<string>($"{HOST_IDS_KEY}:{ServiceName}");
            _healthDCache = RedisUtil.CreateStringClient<long>(HOST_HEALTH_KEY);
            _dataDCache = new TinyFxHostDataDCache(ServiceId);
        }

        public async Task Register()
        {
            await _dataDCache.RegisterData();
            await _idsDCache.AddAsync(ServiceId);
            await _namesDCache.AddAsync(ServiceName);
            LogUtil.Info($"启动 => 注册Host[{GetType().Name}] ServerId:{ServiceId}");
        }
        public async Task Unregister()
        {
            await _idsDCache.RemoveAsync(ServiceId);
            await _dataDCache.DeleteData();
            LogUtil.Info($"停止 => 注销Host[{GetType().Name}] ServerId:{ServiceId}");
        }

        public async Task Heartbeat()
        {
            await _dataDCache.ActiveData();
        }

        public async Task Health()
        {
            // 检查
            var lastTs = await _healthDCache.GetOrDefaultAsync(0);
            var utcTs = DateTime.UtcNow.UtcDateTimeToTimestamp(false);
            if (utcTs - lastTs < HEALTH_INTERVAL)
                return;
            await _healthDCache.SetAsync(utcTs);

            var serviceNames = (await _namesDCache.GetAllAsync()).ToList();
            foreach (var serviceName in serviceNames)
            {
                var idsDCache = RedisUtil.CreateSetClient<string>($"{HOST_IDS_KEY}:{serviceName}");
                var serviceIds = (await idsDCache.GetAllAsync()).ToList();
                foreach (var serviceId in serviceIds)
                {
                    var isValid = await new TinyFxHostDataDCache(serviceId).IsValid();
                    if (!isValid)
                        await idsDCache.RemoveAsync(serviceId);
                }
                var count = await idsDCache.GetLengthAsync();
                if (count == 0)
                {
                    await idsDCache.KeyDeleteAsync();
                    await _namesDCache.RemoveAsync(serviceName);
                }
            }
        }

        public async Task<List<string>> GetAllServiceIds(string connectionStringName = null)
        {
            var ret = new List<string>();
            var serviceNames = (await _namesDCache.GetAllAsync()).ToList();
            foreach (var serviceName in serviceNames)
            {
                var idsDCache = RedisUtil.CreateSetClient<string>($"{HOST_IDS_KEY}:{serviceName}");
                var serviceIds = (await idsDCache.GetAllAsync()).ToList();
                foreach (var serviceId in serviceIds)
                {
                    var isValid = await new TinyFxHostDataDCache(serviceId).IsValid();
                    if (!isValid)
                        await idsDCache.RemoveAsync(serviceId);
                    else
                        ret.Add(serviceId);
                }
            }
            return ret;
        }
        public async Task SetHostData<T>(string key, T value, string serviceId = null, string connectionStringName = null)
        {
            serviceId ??= ServiceId;
            var dcache = new TinyFxHostDataDCache(serviceId, connectionStringName);
            await dcache.SetData(key, value);
        }
        public async Task<CacheValue<T>> GetHostData<T>(string key, string serviceId = null, string connectionStringName = null)
        {
            serviceId ??= ServiceId;
            var dcache = new TinyFxHostDataDCache(serviceId, connectionStringName);
            return await dcache.GetData<T>(key);
        }
    }
}
