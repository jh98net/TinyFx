using Microsoft.AspNetCore.Mvc.Routing;
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
        /// <summary>
        /// 获取指定nacos服务地址
        /// </summary>
        /// <param name="clientName">配置文件Nacos:Clients:ServiceName</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> GetClientServiceUrl(string clientName)
        {
            var element = GetClientElement(clientName);
            var instance = await GetNamingService().SelectOneHealthyInstance(element.ServiceName, element.GroupName
                , element.Clusters ?? new List<string>(), element.Subscribe);
            if (instance == null)
                throw new Exception($"NacosClient创建时没有找到有效的服务实例。name:{clientName}");
            var host = $"{instance.Ip}:{instance.Port}";
            //var host = instance.ToInetAddr();
            var ret = instance.Metadata.TryGetValue("secure", out _)
                ? $"https://{host}"
                : $"http://{host}";
            return ret;
        }

        /// <summary>
        /// 获取指定nacos服务的HttpClient
        /// </summary>
        /// <param name="clientName">配置文件Nacos:Clients:ServiceName</param>
        /// <returns></returns>
        public static async Task<HttpClientEx> CreateHttpClient(string clientName)
        {
            var serviceUrl = await GetClientServiceUrl(clientName);
            var config = new HttpClientConfig
            {
                Name = $"nacos_clients_{clientName}",
                BaseAddress = serviceUrl
            };
            var ret = HttpClientExFactory.CreateClientEx(config);
            return ret;
        }

        #region Utils
        private static INacosNamingService _namingService;
        private static INacosNamingService GetNamingService()
        {
            if (_namingService == null)
                _namingService = DIUtil.GetRequiredService<INacosNamingService>();
            return _namingService;
        }
        private static NacosClientElement GetClientElement(string name)
        {
            var section = ConfigUtil.GetSection<NacosSection>();
            if (section == null || section.Clients == null || !section.Clients.TryGetValue(name, out var ret))
                throw new Exception($"Nacos配置文件没找到ClientElement。name:{name}");
            return ret;
        }
        #endregion
    }
}
