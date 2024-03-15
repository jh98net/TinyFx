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
            _expireSpan = ConfigUtil.Environment.IsDebug
                ? TimeSpan.FromMinutes(10) // 没有设置或Debug时10分钟
                : TimeSpan.FromMilliseconds(ConfigUtil.Host.HeathInterval * 3);
        }

        public async Task RegisterData()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("ServiceId", ServiceId);
            dict.Add("HostIp", ConfigUtil.Service.HostIp);
            dict.Add("HostPort", ConfigUtil.Service.HostPort);
            dict.Add("HostUrl", $"{ConfigUtil.Service.HostIp}:{ConfigUtil.Service.HostPort}");
            dict.Add("RegisterDate", DateTime.UtcNow.UtcToCNString());
            await SetAsync(dict);
            await KeyExpireAsync(TimeSpan.FromSeconds(30));
        }
        /// <summary>
        /// 激活服务
        /// </summary>
        /// <returns></returns>
        public async Task ActiveData()
        {
            await SetAsync("ActiveDate", DateTime.UtcNow.ToTimestamp());
            await KeyExpireAsync(_expireSpan);
        }

        public async Task<bool> IsValid()
        {
            if (!await KeyExistsAsync())
                return false;
            var lastTS = await GetOrDefaultAsync<long>("ActiveDate", 0);
            if (lastTS == 0)
                return false;
            var lastTime = DateTimeUtil.ParseTimestamp(lastTS);
            var ret = DateTime.UtcNow - lastTime < _expireSpan;
            return ret;
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
