using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Nacos.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Net;

namespace TinyFx.Extensions.Nacos
{
    public static class NacosUtil
    {
        public static readonly NacosSection Section = new();

        /// <summary>
        /// 从nacos获取指定服务地址
        /// </summary>
        /// <param name="serviceName">注册的服务名</param>
        /// <param name="isWebsocket"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> GetClientServiceUrl(string serviceName, bool isWebsocket = false)
        {
            var instance = await DIUtil.GetRequiredService<INacosNamingService>()
                .SelectOneHealthyInstance(serviceName, Section.GroupName);
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
