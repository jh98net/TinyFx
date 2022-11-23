using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Reflection;
using System.IO;
using System.Runtime.CompilerServices;

namespace TinyFx.Net
{
    /// <summary>
    /// HttpClient基类（常用语第三方接入）
    /// </summary>
    public abstract class HttpClientBase
    {
        /// <summary>
        /// 配置文件中的HttpClient:Clients:Name
        /// </summary>
        public abstract string ClientName { get; }
        private HttpClientEx _client;

        public HttpClientBase()
        {
            _client = HttpClientExFactory.Create(ClientName);
        }
        public string GetSettingsValue(string key)
        {
            return _client.Settings[key];
        }
        public T GetSettingsValue<T>(string key)
        {
            return GetSettingsValue(key).To<T>();
        }

        /// <summary>
        /// 创建HttpClient代理
        /// </summary>
        /// <returns></returns>
        protected ClientAgent CreateAgent()
        {
            return _client.CreateAgent();
        }
    }
}
