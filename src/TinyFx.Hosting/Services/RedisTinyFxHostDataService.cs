using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Configuration;

namespace TinyFx.Hosting.Services
{
    public class RedisTinyFxHostDataService : ITinyFxHostDataService
    {
        private string _serviceId;
        private TinyFxHostDataDCache _dataDCache;
        public RedisTinyFxHostDataService()
        {
            _serviceId = ConfigUtil.ServiceId;
            _dataDCache = new TinyFxHostDataDCache(_serviceId);
        }

        public async Task SetData<T>(string field, T value)
        {
            await _dataDCache.SetData(field, value);
        }

        public async Task<CacheValue<T>> GetData<T>(string field)
        {
            return await _dataDCache.GetData<T>(field);
        }
    }
}
