using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;

namespace TinyFx.Hosting.Services
{
    public class RedisTinyFxHostRegisterService : ITinyFxHostRegisterService
    {
        private string _serviceId;
        private TinyFxHostListDCache _listDCache = new();
        private TinyFxHostDataDCache _dataDCache;
        private TinyFxHostHealthDCache _healthDCache = new();
        public RedisTinyFxHostRegisterService() 
        {
            _serviceId = ConfigUtil.ServiceId;
            _dataDCache = new TinyFxHostDataDCache(_serviceId);
        }
        public async Task Register()
        {
            LogUtil.Info($"启动 => Host注册[RedisTinyFxHostRegisterService] ServerId:{_serviceId}");
            await _dataDCache.SetServiceId();
            await _listDCache.AddAsync(_serviceId);
        }

        public async Task Heartbeat()
        {
            await _dataDCache.ActiveData();
        }

        public async Task Health()
        {
            await _healthDCache.HealthHosts();
        }

        public async Task Unregister()
        {
            await _listDCache.RemoveHost(_serviceId);
            await _dataDCache.RemoveData();
            LogUtil.Info($"停止 => 注销Host[RedisTinyFxHostRegisterService] ServerId:{_serviceId}");
        }
    }
}
