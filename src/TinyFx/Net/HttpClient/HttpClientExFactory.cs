using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TinyFx.Configuration;

namespace TinyFx.Net
{
    public static class HttpClientExFactory
    {
        private static ConcurrentDictionary<string, HttpClientEx> _clients = new ConcurrentDictionary<string, HttpClientEx>();
        /// <summary>
        /// 创建HttpClientEx
        /// </summary>
        /// <param name="clientName">client名称，如果配置文件有配置，设置对应name，如果没有，每个场景使用各自的name公用</param>
        /// <param name="handlerBody">请求返回时是否保留RequestBody和ResponseBody信息</param>
        /// <returns></returns>
        public static HttpClientEx Create(string clientName, bool handlerBody = true)
        {
            if (string.IsNullOrEmpty(clientName))
                throw new ArgumentNullException($"clientName不能为空");
            if (!_clients.TryGetValue(clientName, out HttpClientEx ret))
            {
                var client = new HttpClient();
                ret = new HttpClientEx(clientName, handlerBody, client);
                var section = ConfigUtil.GetSection<HttpClientSection>();
                if (section != null && section.Clients != null && section.Clients.ContainsKey(clientName))
                {
                    var element = section.Clients[clientName];
                    ret.AddBaseAddress(element.BaseAddress);
                    element.RequestHeaders?.ForEach(kv =>
                    {
                        ret.AddDefaultRequestHeaders(kv.Key, kv.Value);
                    });
                    var timeout = element.Timeout < 1000 ? 3000 : element.Timeout;
                    ret.SetTimeout(timeout);

                    ret.SerializeMode = element.SerializeMode;
                    ret.Encoding = string.IsNullOrEmpty(element.Encoding) ? Encoding.UTF8 : Encoding.GetEncoding(element.Encoding);
                    ret.Settings = element.SettingsDic;
                    ret.IsConfigClient = true;
                }
                _clients.TryAdd(clientName, ret);
            }
            return ret;
        }

        public static HttpClientEx Create(bool handlerBody = false)
        {
            var client = CreateClient();
            return new HttpClientEx(null, handlerBody, client);
        }

        public static HttpClient CreateClient(string name = null)
        {
            var factory = DIUtil.GetRequiredService<IHttpClientFactory>();
            return factory.CreateClient(name);
        }
    }
}
