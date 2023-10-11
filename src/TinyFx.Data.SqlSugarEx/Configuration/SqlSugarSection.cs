using Microsoft.Extensions.Configuration;
using SqlSugar;
using TinyFx.Collections;
using TinyFx.Data.SqlSugarEx;

namespace TinyFx.Configuration
{
    public class SqlSugarSection : ConfigSection
    {
        public override string SectionName => "SqlSugar";
        public bool Enabled { get; set; } = true;
        /// <summary>
        /// 连接配置提供者
        /// </summary>
        public string DbConfigProvider { get; set; }
        /// <summary>
        /// 数据路由提供者
        /// </summary>
        public string DbRoutingProvider { get; set; }

        public string DefaultConnectionStringName { get; set; }

        public Dictionary<string, ConnectionElement> ConnectionStrings = new();
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            ConnectionStrings = configuration.GetSection("ConnectionStrings")
                .Get<Dictionary<string, ConnectionElement>>() ?? new();
            ConnectionStrings.ForEach(x =>
            {
                x.Value.ConfigId = x.Key;
                x.Value.LanguageType = LanguageType.Chinese;
            });
        }
    }
}
