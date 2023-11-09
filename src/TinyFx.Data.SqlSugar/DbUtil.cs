using SqlSugar;
using System.Data;
using TinyFx.Configuration;
using TinyFx.Logging;

namespace TinyFx.Data.SqlSugar
{
    public static class DbUtil
    {
        #region Db
        /// <summary>
        /// 默认configId
        /// </summary>
        public static string DefaultConfigId
            => GlobalDb.CurrentConnectionConfig.ConfigId;

        /// <summary>
        /// 全局DB，仅用作事务
        /// </summary>
        internal static SqlSugarScope GlobalDb
        {
            get
            {
                return (SqlSugarScope)DIUtil.GetRequiredService<ISqlSugarClient>();
            }
        }

        /// <summary>
        /// 获取DB
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public static ISqlSugarClient GetDb(string configId = null)
        {
            ISqlSugarClient ret = null;
            var section = ConfigUtil.GetSection<SqlSugarSection>();
            if (configId == null || configId == section.DefaultConnectionStringName)
            {
                ret = GlobalDb.GetConnection(section.DefaultConnectionStringName);
            }
            else
            {
                if (GlobalDb.IsAnyConnection(configId))
                {
                    ret = GlobalDb.GetConnection(configId);
                }
                else
                {
                    var provider = DIUtil.GetRequiredService<IDbConfigProvider>();
                    var config = provider.GetConfig(configId);
                    if (config == null)
                        throw new Exception($"配置SqlSugar:ConnectionStrings没有找到连接。name:{section.DefaultConnectionStringName} type:{provider.GetType().FullName}");
                    config.LanguageType = LanguageType.Chinese;
                    config.IsAutoCloseConnection = true;
                    GlobalDb.AddConnection(config);
                    ret = GlobalDb.GetConnection(configId);
                    InitDb(ret, config);
                }
            }
            return ret;
        }
        public static ISqlSugarClient GetDb<T>(params object[] routingDbKeys)
        {
            var configId = DIUtil.GetRequiredService<IDbRoutingProvider>()
                .RouteDb<T>(routingDbKeys);
            return GetDb(configId);
        }
        #endregion

        #region 事务
        public static void BeginTran(IsolationLevel level = IsolationLevel.ReadCommitted) => GlobalDb.BeginTran(level);
        public static void CommitTran() => GlobalDb.CommitTran();
        public static void RollbackTran() => GlobalDb.RollbackTran();
        public static Task BeginTranAsync(IsolationLevel level) => GlobalDb.BeginTranAsync(level);
        public static Task CommitTranAsync() => GlobalDb.CommitTranAsync();
        public static Task RollbackTranAsync() => GlobalDb.RollbackTranAsync();
        #endregion

        #region Repository
        /// <summary>
        /// 创建Repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="routingDbKeys">分库标识</param>
        /// <returns></returns>
        public static Repository<T> CreateRepository<T>(params object[] routingDbKeys)
         where T : class, new()
        {
            return new Repository<T>(routingDbKeys);
        }
        #endregion

        #region Utils
        internal static void InitDb(ISqlSugarClient db, ConnectionElement config)
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

                    LogUtil.Debug("执行SQL: {SQL}", tmpSql);
                };
                db.Aop.OnLogExecuted = (sql, paras) =>
                {
                    //var log = LogUtil.GetContextLog();
                    //log.AddMessage($"SQL执行时间: {db.Ado.SqlExecutionTime.TotalMilliseconds}ms");
                    //if (!log.IsContextLog)
                    //    log.SetFlag("SqlSugar").Save();
                };
            }
            db.Aop.OnError = (ex) =>
            {
                // 无参数化
                var tmpSql = UtilMethods.GetSqlString(config.DbType, ex.Sql, (SugarParameter[])ex.Parametres);

                var log = LogUtil.GetContextLog();
                log.AddMessage("SQL执行异常");
                log.AddField("SqlSugar.ConfigId", config.ConfigId);
                log.AddField("SqlSugar.SQL", tmpSql);
                log.AddException(ex);
                if (!log.IsContextLog)
                    log.SetFlag("SqlSugar").Save();
                LogUtil.Error(ex, $"SQL: {tmpSql}");
            };
        }
        #endregion
    }

}
