using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace TinyFx.Configuration
{
    /// <summary>
    /// 项目自定义配置
    /// </summary>
    public class AppSettingsSection : ConfigSection
    {
        /// <summary>
        /// 配置节名称
        /// </summary>
        public override string SectionName => "AppSettings";

        /// <summary>
        /// 配置集合
        /// </summary>
        public Dictionary<string, string> Settings = new Dictionary<string, string>();
        /// <summary>
        /// 获取配置值，没有配置返回null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]=>Get(key);
        /// <summary>
        /// 绑定Section数据
        /// </summary>
        /// <param name="configuration"></param>
        public override void Bind(IConfiguration configuration)
        {
            var items = configuration.Get<KeyValueItem[]>();
            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item.Key))
                    continue;
                if (Settings.ContainsKey(item.Key))
                    throw new Exception($"tinyfx配置中AppSettings:Key重复！key: {item.Key}");
                Settings.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 获取配置值，没有配置返回null
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string Get(string key, string defaultValue=null)
        {
            string ret;
            if (!Settings.TryGetValue(key, out ret))
                ret = defaultValue;
            return ret;
        }
        
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
            => Get(key).To<T>();

        /// <summary>
        /// 获取配置值，不存在或错误提供默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetOrDefault<T>(string key, T defaultValue)
            => Get(key).To<T>(defaultValue);
        
    }
    /// <summary>
    /// KeyValue类型
    /// </summary>
    public class KeyValueItem
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
    }
}
