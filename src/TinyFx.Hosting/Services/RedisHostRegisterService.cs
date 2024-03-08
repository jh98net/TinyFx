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
    public class RedisHostRegisterService : ITinyFxHostRegisterService
    {
        public bool UseHeartbeat => true;
        public string ServiceId { get; }

        private TinyFxHostListDCache _listDCache = new();
        private TinyFxHostDataDCache _dataDCache;
        private TinyFxHostHealthDCache _healthDCache = new();
        public RedisHostRegisterService()
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
    }
}
