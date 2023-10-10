using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Data.SqlSugarEx;
using TinyFx.Logging;
using TinyFx.Reflection;

namespace TinyFx
{
    public static class SqlSugarHostBuilderExtensions
    {
        public static IHostBuilder UseSqlSugarEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<SqlSugarSection>();
            if (section != null)
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
                        var ret = new SqlSugarScope(config, db =>
                        {
                            if (config.LogEnabled)
                            {
                                db.Aop.OnLogExecuting = (sql, paras) =>
                                {
                                    var tmpSql = sql;
                                    if (ConfigUtil.IsDebugEnvironment || config.LogSqlMode == 2)
                                        tmpSql = UtilMethods.GetSqlString(config.DbType, sql, paras);
                                    else if (config.LogSqlMode == 1)
                                        tmpSql = UtilMethods.GetNativeSql(sql, paras);

                                    var log = LogUtil.GetContextOrCreate();
                                    log.AddMessage($"SQL执行前");
                                    log.AddField("SQL", tmpSql);
                                    if (!log.IsContextLog)
                                        log.SetFlag("SqlSugar").Save();
                                };
                                db.Aop.OnLogExecuted = (sql, paras) =>
                                {
                                    var log = LogUtil.GetContextOrCreate();
                                    log.AddMessage($"SQL执行时间: {db.Ado.SqlExecutionTime.TotalMilliseconds}ms");
                                    if (!log.IsContextLog)
                                        log.SetFlag("SqlSugar").AddField("SQL", sql).Save();
                                };
                            }
                            db.Aop.OnError = (ex) =>
                            {
                                var tmpSql = ex.Sql;
                                if (ConfigUtil.IsDebugEnvironment || config.LogSqlMode == 2)
                                    tmpSql = UtilMethods.GetSqlString(config.DbType, ex.Sql, (SugarParameter[])ex.Parametres);
                                else if (config.LogSqlMode == 1)
                                    tmpSql = UtilMethods.GetNativeSql(ex.Sql, (SugarParameter[])ex.Parametres);

                                var log = LogUtil.GetContextOrCreate();
                                log.AddMessage("SQL执行异常");
                                log.AddField("SQL", tmpSql);
                                log.AddException(ex);
                                if (!log.IsContextLog)
                                    log.SetFlag("SqlSugar").Save();
                            };
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
