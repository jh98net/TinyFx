using Nacos.V2.Naming.Dtos;
using SqlSugar;
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

        #region 注册
        public async Task Register()
        {
            using (var redLock = await GetRedLock())
            {
                await _dataDCache.RegisterData();
                await _idsDCache.AddAsync(ServiceId);
                await _namesDCache.AddAsync(ServiceName);
            }
            LogUtil.Info($"启动 => 注册Host[{GetType().Name}] ServerId:{ServiceId}");
        }
        public async Task Unregister()
        {
            using (var redLock = await GetRedLock())
            {
                await _dataDCache.DeleteData();
                await _idsDCache.RemoveAsync(ServiceId);
                var count = await _idsDCache.GetLengthAsync();
                if (count == 0)
                {
                    await _idsDCache.KeyDeleteAsync();
                    await _namesDCache.RemoveAsync(ServiceName);
                }
            }
            LogUtil.Info($"停止 => 注销Host[{GetType().Name}] ServerId:{ServiceId}");
        }

        public async Task Heartbeat()
        {
            await _dataDCache.ActiveData();
        }

        public async Task Health()
        {
            // 检查
            using (var redLock = await RedisUtil.LockAsync($"__HostRegister:_HEALTH", 20))
            {
                if (!redLock.IsLocked)
                {
                    redLock.Release();
                    throw new Exception($"RedisHostRegisterService获取缓存锁超时。key: __HostRegister:_HEALTH");
                }

                var lastTs = await _healthDCache.GetOrDefaultAsync(0);
                var utcTs = DateTime.UtcNow.UtcDateTimeToTimestamp(false);
                if (utcTs - lastTs < HEALTH_INTERVAL)
                    return;
                await _healthDCache.SetAsync(utcTs);
            }

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
        #endregion

        private async Task<RedLock> GetRedLock()
        {
            var ret = await RedisUtil.LockAsync($"__HostRegister:{ServiceName}", 20);
            if (!ret.IsLocked)
            {
                ret.Release();
                throw new Exception($"RedisHostRegisterService获取缓存锁超时。key: __HostRegister:{ServiceName}");
            }
            return ret;
        }
    }
}
