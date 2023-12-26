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
        /// <summary>
        /// 设置本机host数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetData<T>(string field, T value);
        /// <summary>
        /// 获取本机host数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <returns></returns>
        Task<CacheValue<T>> GetData<T>(string field);
        /// <summary>
        /// 获取指定serviceId的host数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceId"></param>
        /// <param name="field"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        Task<CacheValue<T>> GetHostData<T>(string serviceId, string field, string connectionStringName = null);
        /// <summary>
        /// 获取所有host的serviceId
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        Task<List<string>> GetHosts(string connectionStringName = null);
    }


    public class DefaultTinyFxHostDataService : ITinyFxHostDataService
    {
        private ConcurrentDictionary<string, object> _dict = new();
        private string _serviceId;
        public DefaultTinyFxHostDataService()
        {
            _serviceId = ConfigUtil.ServiceId;
        }

        public Task SetData<T>(string field, T value)
        {
            var key = GetKey(field);
            _dict.AddOrUpdate(key, value, (k, v) => value);
            return Task.CompletedTask;
        }

        public async Task<CacheValue<T>> GetData<T>(string field)
        {
            var ret = new CacheValue<T>();
            var key = GetKey(field);
            if (_dict.TryGetValue(key, out var value))
            {
                ret.HasValue = true;
                ret.Value = (T)value;
            }
            return ret;
        }
        private string GetKey(string field)
            => $"{_serviceId}|{field}";

        public Task<CacheValue<T>> GetHostData<T>(string serviceId, string field, string connectionStringName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetHosts(string connectionStringName)
        {
            throw new NotImplementedException();
        }
    }
}