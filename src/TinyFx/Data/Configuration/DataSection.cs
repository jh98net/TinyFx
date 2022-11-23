using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using TinyFx.Configuration;
using TinyFx.Data.Instrumentation;
using System.Linq;
using TinyFx.Reflection;
using TinyFx.Data;

namespace TinyFx.Configuration
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DataSection : ConfigSection
    {
        #region Section
        /// <summary>
        /// Section名称
        /// </summary>
        public override string SectionName => "Data";
        /// <summary>
        /// 默认连接字符串名
        /// </summary>
        public string DefaultConnectionStringName { get; set; }

        /// <summary>
        /// 数据路由提供程序
        /// </summary>
        public string DataRouter { get; set; }

        /// <summary>
        /// 跟踪服务提供程序
        /// </summary>
        public string InstProvider { get; set; }
        /// <summary>
        /// 连接字符串集合
        /// </summary>
        public Dictionary<string, ConnectionStringElement> ConnectionStrings = new Dictionary<string, ConnectionStringElement>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            var conns = configuration.GetSection("ConnectionStrings")
                .Get<ConnectionStringElement[]>();
            foreach (var conn in conns)
            {
                if (ConnectionStrings.ContainsKey(conn.Name))
                    throw new Exception($"tinyfx配置中Data:ConnectionStrings:Name重复。Name: {conn.Name}");
                ConnectionStrings.Add(conn.Name, conn);
            }
            LoadConnectionStringConfigs();
        }
        #endregion 

        #region ConnectionStringConfigs
        /// <summary>
        /// 数据库连接字符串配置集合。
        /// key: connectionStringName
        /// </summary>
        public ConcurrentDictionary<string, ConnectionStringConfig> ConnectionStringConfigs = new ConcurrentDictionary<string, ConnectionStringConfig>();
        /// <summary>
        /// 命名空间与数据库连接字符串映射集合
        /// key: namespace value: connectionStringName
        /// </summary>
        public ConcurrentDictionary<string, ConnectionStringConfig> ConnectionStringNamespaces = new ConcurrentDictionary<string, ConnectionStringConfig>();
        private void LoadConnectionStringConfigs()
        {
            ConnectionStringConfigs.Clear();
            ConnectionStringNamespaces.Clear();
            foreach (var sett in ConnectionStrings.Values)
            {
                if (string.IsNullOrEmpty(sett.Name) || string.IsNullOrEmpty(sett.ConnectionString))
                    continue;
                string instType = !string.IsNullOrEmpty(sett.InstProvider) ? sett.InstProvider : InstProvider;
                var config = new ConnectionStringConfig
                {
                    ConnectionStringName = sett.Name,
                    Provider = DbDataProviderUtil.GetProvider(sett.ProviderName),
                    ConnectionString = sett.ConnectionString,
                    ReadConnectionString = sett.ReadConnectionString,
                    CommandTimeout = sett.CommandTimeout,
                    InstProvider = string.IsNullOrEmpty(instType) ? DefaultDataInstProvider.Instance
                        : (IDataInstProvider)ReflectionUtil.CreateInstance(Type.GetType(instType))
                };
                if (!ConnectionStringConfigs.TryAdd(config.ConnectionStringName, config))
                    throw new Exception("tinyfx配置中Data:ConnectionStrings:Name重复。Name: " + config.ConnectionStringName);

                if (!string.IsNullOrEmpty(sett.OrmMap))
                {
                    foreach (var ns in sett.OrmMap.Split(';'))
                    {
                        if (!ConnectionStringNamespaces.TryAdd(ns, config))
                            throw new Exception($"tinyfx配置中Data:ConnectionStrings:OrmMap配置重复。name: {sett.Name} ormMap: {ns}");
                    }
                }
            }
            if (string.IsNullOrEmpty(DefaultConnectionStringName) && ConnectionStringConfigs.Count == 1)
                DefaultConnectionStringName = ConnectionStringConfigs.First().Key;
        }
        #endregion
    }
}
