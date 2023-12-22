using System;
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
        private TimeSpan MaxExpireSpan = TimeSpan.FromMinutes(10);
        private TimeSpan _expireSpan;
        public TinyFxHostDataDCache(string serviceId)
        {
            ServiceId = serviceId;
            RedisKey = $"{RedisPrefixConst.HOSTS}:Data:{serviceId}";
            var expire = ConfigUtil.Host.DataExpire;
            _expireSpan = expire > 0 ? TimeSpan.FromMilliseconds(expire) : MaxExpireSpan;
        }

        public async Task SetServiceId()
        {
            await SetData("ServiceId", ServiceId);
            await ActiveData();
        }

        /// <summary>
        /// 激活服务
        /// </summary>
        /// <returns></returns>
        public async Task ActiveData()
        {
            await KeyExpireAsync(_expireSpan);
        }
        public async Task RemoveData()
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
            var data = await GetAsync(field);
            var ret = new CacheValue<T>();
            ret.HasValue = data.HasValue;
            if (data.HasValue)
                ret.Value = TinyFxUtil.ConvertTo<T>(data.Value);
            return ret;
        }
    }
}
