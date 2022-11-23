using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using TinyFx.Configuration;
using System.Linq;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.Configuration
{
    /// <summary>
    /// Redis配置文件
    /// </summary>
    public class RedisSection : ConfigSection
    {
        /// <summary>
        /// Section名称
        /// </summary>
        public override string SectionName => "Redis";
        /// <summary>
        /// 默认redis连接
        /// </summary>
        public string DefaultConnectionStringName { get; set; }
        /// <summary>
        /// redis连接集合
        /// </summary>
        public Dictionary<string, ConnectionStringElement> ConnectionStrings = new Dictionary<string, ConnectionStringElement>();
        /// <summary>
        /// 命名空间与Redis连接字符串映射集合
        /// key: namespace value: connectionStringName
        /// </summary>
        public ConcurrentDictionary<string, string> ConnectionStringNamespaces = new ConcurrentDictionary<string, string>();
        /// <summary>
        /// 配置绑定
        /// </summary>
        /// <param name="configuration"></param>
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);

            var connStrs = configuration.GetSection("ConnectionStrings").Get<ConnectionStringElement[]>();
            ConnectionStrings.Clear();
            ConnectionStringNamespaces.Clear();
            foreach (var connStr in connStrs)
            {
                if (ConnectionStrings.ContainsKey(connStr.Name))
                    throw new Exception($"配置中Redis:ConnectionStrings:Name 重复。Name: {connStr.Name}");
                ConnectionStrings.Add(connStr.Name, connStr);
                // 
                if (!string.IsNullOrEmpty(connStr.NamespaceMap))
                {
                    foreach (var ns in connStr.NamespaceMap.Split(';'))
                    {
                        if (!ConnectionStringNamespaces.TryAdd(ns, connStr.Name))
                            throw new Exception($"tinyfx配置中Redis:ConnectionStrings:NamespaceMap配置重复。name: {connStr.Name} NamespaceMap: {ns}");
                    }
                }
            }
            if (string.IsNullOrEmpty(DefaultConnectionStringName) && ConnectionStrings.Count == 1)
                DefaultConnectionStringName = ConnectionStrings.First().Key;
        }
    }
}
