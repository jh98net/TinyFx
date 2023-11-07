using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Data.SqlSugar;
using TinyFx.Logging;
using TinyFx.Reflection;

namespace TinyFx
{
    public static class SqlSugarHostBuilderExtensions
    {
        public static IHostBuilder UseSqlSugarEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<SqlSugarSection>();
            if (section != null && section.Enabled)
            {
                builder.ConfigureServices((context, services) =>
                {
                    // IDbConfigProvider
                    IDbConfigProvider configProvider = !string.IsNullOrEmpty(section.DbConfigProvider)
                        ? (IDbConfigProvider)ReflectionUtil.CreateInstance(section.DbConfigProvider)
                        : new DefaultDbConfigProvider();
                    services.AddSingleton(configProvider);

                    // IDbRoutingProvider
                    IDbRoutingProvider routingProvider = !string.IsNullOrEmpty(section.DbRoutingProvider)
                        ? (IDbRoutingProvider)ReflectionUtil.CreateInstance(section.DbRoutingProvider)
                        : new DefaultDbRoutingProvider();
                    services.AddSingleton(routingProvider);

                    services.AddSingleton<ISqlSugarClient>(sp =>
                    {
                        var provider = sp.GetRequiredService<IDbConfigProvider>();
                        var config = provider.GetConfig(section.DefaultConnectionStringName);
                        if (config == null)
                            throw new Exception($"配置SqlSugar:ConnectionStrings没有找到默认连接。name:{section.DefaultConnectionStringName} type:{provider.GetType().FullName}");
                        config.LanguageType = LanguageType.Chinese;
                        config.IsAutoCloseConnection = true;
                        var ret = new SqlSugarScope(config, db =>
                        {
                            DbUtil.InitDb(db, config);
                        });
                        if (!config.SlaveEnabled)
                            ret.Ado.IsDisableMasterSlaveSeparation = true;
                        return ret;
                    });
                });
            }
            return builder;
        }
    }
}
