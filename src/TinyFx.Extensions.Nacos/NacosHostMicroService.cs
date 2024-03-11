using Nacos.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Hosting.Services;
using static System.Collections.Specialized.BitVector32;

namespace TinyFx.Extensions.Nacos
{
    public class NacosHostMicroService: ITinyFxHostMicroService
    {
        /// <summary>
        /// 获取所有服务名
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetAllServiceNames()
            => await new NacosOpenApiService().GetServices();

        /// <summary>
        /// 从nacos获取指定服务名的有效服务地址
        /// </summary>
        /// <param name="serviceName">注册的服务名</param>
        /// <param name="isWebsocket"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> SelectOneServiceUrl(string serviceName, bool isWebsocket = false)
        {
            var section = DIUtil.GetService<NacosSection>();
            var instance = await DIUtil.GetRequiredService<INacosNamingService>()
                .SelectOneHealthyInstance(serviceName, section.GroupName);
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
    }
}
