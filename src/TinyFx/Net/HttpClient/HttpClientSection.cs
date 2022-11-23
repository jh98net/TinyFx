using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;
using System.Collections.Concurrent;
using TinyFx.Net;

namespace TinyFx.Configuration
{
    /// <summary>
    /// HttpClient配置节
    /// </summary>
    public class HttpClientSection : ConfigSection
    {
        public override string SectionName => "HttpClient";
        //public string DefaultClientName { get; set; }
        public Dictionary<string, HttpClientsElement> Clients = new Dictionary<string, HttpClientsElement>();
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            var items = configuration.GetSection("Clients").Get<HttpClientsElement[]>();
            foreach (var item in items)
            {
                if (Clients.ContainsKey(item.Name))
                    throw new Exception($"配置文件HttpClient:Clients:Name中存在重复记录: {item.Name}");
                if(item.Name == "") // HttpClientEx默认
                    throw new Exception($"配置文件HttpClient:Clients:Name不能为 空");
                foreach (var setting in item.Settings)
                {
                    item.SettingsDic.TryAdd(setting.Key, setting.Value);
                }
                Clients.Add(item.Name, item);
            }
        }
    }
}

namespace TinyFx.Net
{
    /// <summary>
    /// HttpClient配置信息
    /// </summary>
    public class HttpClientsElement
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 序列化方式
        /// </summary>
        public SerializeMode SerializeMode { get; set; } = SerializeMode.Json;
        /// <summary>
        /// 基地址URL
        /// </summary>
        public string BaseAddress { get; set; }
        /// <summary>
        /// Headers
        /// </summary>
        public List<KeyValueItem> RequestHeaders { get; set; }
        /// <summary>
        /// 请求超时时长（毫秒）默认3秒
        /// </summary>
        public int Timeout { get; set; } = 3000;
        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; }
        public string Encoding { get; set; }
        public List<KeyValueItem> Settings { get; set; } = new List<KeyValueItem>();

        public readonly ConcurrentDictionary<string, string> SettingsDic = new ConcurrentDictionary<string, string>();
        public T GetSettingsValue<T>(string key)
        {
            if (!SettingsDic.TryGetValue(key, out string ret))
                throw new Exception($"配置HttpClient:Clients[{Name}]:Key不存在。 key: {key}");
            return ret.To<T>();
        }
    }
}
