using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Configuration;

namespace TinyFx.Hosting.Services
{
    public interface ITinyFxHostDataService
    {
        Task SetData(string field, object value);
        Task<CacheValue<object>> GetData(string field);
    }


    public class DefaultTinyFxHostDataService : ITinyFxHostDataService
    {
        private ConcurrentDictionary<string, object> _dict = new();
        private string _serviceId;
        public DefaultTinyFxHostDataService()
        {
            _serviceId = ConfigUtil.ServiceId;
        }

        public Task SetData(string field, object value)
        {
            var key = GetKey(field);
            _dict.AddOrUpdate(key, value, (k, v) => value);
            return Task.CompletedTask;
        }

        public async Task<CacheValue<object>> GetData(string field)
        {
            var ret = new CacheValue<object>();
            var key = GetKey(field);
            if (_dict.TryGetValue(key, out var value))
            {
                ret.HasValue = true;
                ret.Value = value;
            }
            return ret;
        }
        private string GetKey(string field)
            => $"{_serviceId}|{field}";
    }
}