﻿using Microsoft.Extensions.DependencyInjection;
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
                    var configProvider = !string.IsNullOrEmpty(section.DbConfigProvider)
                        ? (IDbConfigProvider)ReflectionUtil.CreateInstance(section.DbConfigProvider)
                        : new DefaultDbConfigProvider();
                    services.AddSingleton(configProvider);

                    // IDbSplitProvider
                    var splitProvider = !string.IsNullOrEmpty(section.DbSplitProvider)
                        ? (IDbSplitProvider)ReflectionUtil.CreateInstance(section.DbSplitProvider)
                        : new DefaultSplitProvider();
                    services.AddSingleton(splitProvider);

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
