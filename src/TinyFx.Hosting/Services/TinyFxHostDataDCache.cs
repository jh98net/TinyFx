using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.Hosting.Services
{
    /// <summary>
    /// service服务数据
    /// </summary>
    internal class TinyFxHostDataDCache : RedisHashMultiClient
    {
        public string ServiceId { get; }
        private TimeSpan _expireSpan;
        public TinyFxHostDataDCache(string serviceId, string connectionStringName = null)
        {
            ServiceId = serviceId;
            Options.ConnectionStringName = connectionStringName;
            RedisKey = $"{RedisPrefixConst.HOSTS}:Data:{serviceId}";
            _expireSpan = ConfigUtil.IsDebugEnvironment
                ? TimeSpan.FromMinutes(10) // 没有设置或Debug时10分钟
                : TimeSpan.FromMilliseconds(ConfigUtil.Host.HeathInterval * 3);
        }

        public async Task RegisterData()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("ServiceId", ServiceId);
            dict.Add("HostIp", ConfigUtil.ServiceInfo.HostIp);
            dict.Add("HostPort", ConfigUtil.ServiceInfo.HostPort);
            dict.Add("HostUrl", $"{ConfigUtil.ServiceInfo.HostIp}:{ConfigUtil.ServiceInfo.HostPort}");
            dict.Add("RegisterDate", DateTime.UtcNow.UtcToBeijingDateTime().ToFormatString(true));
            await SetAsync(dict);
            await KeyExpireAsync(_expireSpan);
        }
        /// <summary>
        /// 激活服务
        /// </summary>
        /// <returns></returns>
        public async Task ActiveData()
        {
            await SetAsync("ActiveDate", DateTime.UtcNow.UtcToBeijingDateTime().ToFormatString(true));
            await KeyExpireAsync(_expireSpan);
        }

        public async Task<bool> IsValid()
        {
            if (!await KeyExistsAsync())
                return false;
            var lastDate = await GetOrDefaultAsync<string>("ActiveDate", null);
            if (string.IsNullOrEmpty(lastDate))
                return false;
            var lastTime = lastDate.ToFormatDateTime();
            return DateTime.UtcNow - lastTime < _expireSpan;
        }
        public async Task DeleteData()
        {
            await KeyDeleteAsync();
        }

        /// <summary>
        /// 设置服务数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task SetData<T>(string field, T value)
        {
            await SetAsync(field, value);
        }

        /// <summary>
        /// 获取服务数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <returns></returns>
        public async Task<CacheValue<T>> GetData<T>(string field)
        {
            return await GetAsync<T>(field);
        }
    }
}
