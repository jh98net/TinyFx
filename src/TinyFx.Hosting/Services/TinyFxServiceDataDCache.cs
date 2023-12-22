using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.Hosting.Services
{
    /// <summary>
    /// service服务数据
    /// </summary>
    internal class TinyFxServiceDataDCache : RedisHashMultiClient
    {
        private TimeSpan _expireSpan;
        public TinyFxServiceDataDCache(string serviceId)
        {
            RedisKey = $"{RedisPrefixConst.TINYFX_HOSTS}:Data:{serviceId}";
            _expireSpan = TimeSpan.FromMilliseconds(ConfigUtil.Project.HostHeartbeatInterval * 3);
        }

        /// <summary>
        /// 激活服务
        /// </summary>
        /// <returns></returns>
        public async Task ActiveService()
        {
            await KeyExpireAsync(_expireSpan);
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
                ret.Value = (T)data.Value;
            return ret;
        }
    }
}
