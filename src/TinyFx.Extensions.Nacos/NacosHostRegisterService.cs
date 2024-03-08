using Nacos.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Hosting.Services;
using static System.Collections.Specialized.BitVector32;

namespace TinyFx.Extensions.Nacos
{
    public class NacosHostRegisterService : ITinyFxHostRegisterService
    {
        public bool UseHeartbeat => false;
        private const string METADATA_PREFIX = "TINYFX:";
        public async Task Register()
        {
        }
        public async Task Unregister()
        {
        }

        #region NotImplementedException
        public Task Health()
        {
            throw new NotImplementedException();
        }

        public Task Heartbeat()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从nacos获取指定服务地址
        /// </summary>
        /// <param name="serviceName">注册的服务名</param>
        /// <param name="isWebsocket"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> GetServiceUrl(string serviceName, bool isWebsocket = false)
        {
            var instance = await DIUtil.GetRequiredService<INacosNamingService>()
                .SelectOneHealthyInstance(serviceName, NacosUtil.Section.GroupName);
            if (instance == null)
                throw new Exception($"NacosUtil.GetClientServiceUrl时没有有效实例。serviceName:{serviceName}");
            var host = $"{instance.Ip}:{instance.Port}";
            var secure = instance.Metadata.TryGetValue("secure", out var value)
                ? value.ToBoolean(false) : false;
            string ret = null;
            if (isWebsocket)
                ret = secure ? $"wss://{host}" : $"ws://{host}";
            else
                ret = secure ? $"https://{host}" : $"http://{host}";
            return ret;
        }
        #endregion
    }
}
