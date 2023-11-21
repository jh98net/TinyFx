using SqlSugar;
using System.Data;
using TinyFx.Configuration;
using TinyFx.Logging;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static System.Collections.Specialized.BitVector32;

namespace TinyFx.Data.SqlSugar
{
    public static class DbUtil
    {
        #region Properties
        private static SqlSugarScope _db;
        /// <summary>
        /// 全局DB，仅用作事务
        /// </summary>
        internal static SqlSugarScope Db
            => _db ??= (SqlSugarScope)DIUtil.GetRequiredService<ISqlSugarClient>();
        /// <summary>
        /// 默认configId
        /// </summary>
        public static string DefaultConfigId
            => Convert.ToString(Db.CurrentConnectionConfig.ConfigId);
        #endregion

        #region GetDb
        /// <summary>
        /// 获取DB --> GetConnectionScope()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="splitDbKeys"></param>
        /// <returns></returns>
        public static ISqlSugarClient GetDb<T>(params object[] splitDbKeys)
        {
            var configId = DIUtil.GetRequiredService<IDbSplitProvider>()
                .SplitDb<T>(splitDbKeys);
            return GetDb(configId);
        }
        public static ISqlSugarClient GetDb(string configId = null)
        {
            // 主库
            if (string.IsNullOrEmpty(configId) || configId == DefaultConfigId)
                return Db.GetConnectionScope(DefaultConfigId);

            var config = GetConfig(configId);
            TryAddDb(config);
            return Db.GetConnectionScope(configId);
        }

        public static ISqlSugarClient GetNewDb<T>(params object[] splitDbKeys)
        {
            var configId = DIUtil.GetRequiredService<IDbSplitProvider>()
                .SplitDb<T>(splitDbKeys);
            return GetNewDb(configId);
        }
        public static ISqlSugarClient GetNewDb(string configId = null)
        {
            // 主库
            if (string.IsNullOrEmpty(configId) || configId == DefaultConfigId)
                return Db.GetConnection(DefaultConfigId).CopyNew();

            var config = GetConfig(configId);
            TryAddDb(config);
            return Db.GetConnection(configId).CopyNew();
        }
        #endregion

        #region Repository
        /// <summary>
        /// 创建Repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="splitDbKeys">分库标识</param>
        /// <returns></returns>
        public static Repository<T> GetRepository<T>(params object[] splitDbKeys)
         where T : class, new()
        {
            return new Repository<T>(splitDbKeys);
        }
        #endregion

        #region Utils
        internal static ConnectionElement GetConfig(string configId)
        {
            var provider = DIUtil.GetRequiredService<IDbConfigProvider>();
            var config = provider.GetConfig(configId);
            if (config == null)
                throw new Exception($"配置SqlSugar:ConnectionStrings没有找到连接。configId:{configId} type:{provider.GetType().FullName}");

            config.LanguageType = LanguageType.Chinese;
            config.IsAutoCloseConnection = true;
            return config;
        }
        private static object _sync = new();
        private static HashSet<object> _configDbDict = new();
        private static bool TryAddDb(ConnectionElement config)
        {
            if (Convert.ToString(config.ConfigId) == DefaultConfigId)
                return false;
            if (_configDbDict.Contains(config.ConfigId))
                return false;
            if (!Db.IsAnyConnection(config.ConfigId))
            {
                lock (_sync)
                {
                    if (!Db.IsAnyConnection(config.ConfigId))
                    {
                        Db.AddConnection(config);
                        var newDb = Db.GetConnection(config.ConfigId);
                        InitDb(newDb, config);
                        _configDbDict.Add(config.ConfigId);
                        return true;
                    }
                }
            }
            return false;
        }
        internal static void InitDb(ISqlSugarClient db, ConnectionElement config)
        {
            // split table
            db.CurrentConnectionConfig.ConfigureExternalServices.SplitTableService
                = DIUtil.GetRequiredService<IDbSplitProvider>().SplitTable();

            // log
            if (config.LogEnabled)
            {
                db.Aop.OnLogExecuting = (sql, paras) =>
                {
                    var tmpSql = sql;
                    if (ConfigUtil.IsDebugEnvironment || config.LogSqlMode == 2)
                        tmpSql = UtilMethods.GetSqlString(config.DbType, sql, paras);
                    else
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
                var tmpSql = config.LogSqlMode == 2
                    ? UtilMethods.GetSqlString(config.DbType, ex.Sql, (SugarParameter[])ex.Parametres)
                    : UtilMethods.GetNativeSql(ex.Sql, (SugarParameter[])ex.Parametres);

                var log = DIUtil.GetService<ILogBuilder>();
                if (log != null)
                {
                    log.AddMessage("SqlSugar SQL执行异常");
                    log.AddField("SqlSugar.ConfigId", config.ConfigId);
                    log.AddField("SqlSugar.SQL", tmpSql);
                    log.AddException(ex);
                }
                LogUtil.Error(ex, "异常SQL: {SQL}", tmpSql);
            };
        }
        #endregion
    }

}
