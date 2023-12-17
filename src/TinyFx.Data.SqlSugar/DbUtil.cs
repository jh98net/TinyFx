using SqlSugar;
using SS = SqlSugar;
using TinyFx.Configuration;
using TinyFx.Logging;
using System.Data;

namespace TinyFx.Data.SqlSugar
{
    public static class DbUtil
    {
        #region Properties
        private static SqlSugarScope _mainDb;
        /// <summary>
        /// 全局DB，仅用作事务
        /// </summary>
        internal static SqlSugarScope MainDb
            => _mainDb ??= (SqlSugarScope)DIUtil.GetRequiredService<ISqlSugarClient>();
        /// <summary>
        /// 默认configId
        /// </summary>
        public static string DefaultConfigId
            => Convert.ToString(MainDb.CurrentConnectionConfig.ConfigId);
        #endregion

        #region GetDb
        public static ISqlSugarClient GetDb(SS.DbType dbType, string connectionString)
        {
            var ret = new SqlSugarClient(new ConnectionConfig
            {
                DbType = dbType,
                ConnectionString = connectionString,
                IsAutoCloseConnection = true,
            });
            InitDb(ret, null);
            return ret;
        }

        /// <summary>
        /// 根据IDbSplitProvider获取ISqlSugarClient
        /// </summary>
        /// <param name="splitDbKeys"></param>
        /// <returns></returns>
        public static ISqlSugarClient GetDb(params object[] splitDbKeys)
            => GetDb<object>(splitDbKeys);

        /// <summary>
        /// 根据IDbSplitProvider获取ISqlSugarClient
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="splitDbKeys"></param>
        /// <returns></returns>
        public static ISqlSugarClient GetDb<T>(params object[] splitDbKeys)
        {
            var configId = DIUtil.GetRequiredService<IDbSplitProvider>()
                .SplitDb<T>(splitDbKeys);
            return GetDbById(configId);
        }

        /// <summary>
        /// 获取指定configId的ISqlSugarClient
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public static ISqlSugarClient GetDbById(string configId = null)
        {
            // 主库
            if (string.IsNullOrEmpty(configId) || configId == DefaultConfigId)
                return MainDb.GetConnectionScope(DefaultConfigId);

            var config = GetConfig(configId);
            TryAddDb(config);
            return MainDb.GetConnectionScope(configId);
        }
/*
        /// <summary>
        /// 根据IDbSplitProvider获取ISqlSugarClient
        /// </summary>
        /// <param name="splitDbKeys"></param>
        /// <returns></returns>
        public static ISqlSugarClient GetNewDb(params object[] splitDbKeys)
            => GetNewDb<object>(splitDbKeys);

        /// <summary>
        /// 根据IDbSplitProvider获取ISqlSugarClient
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="splitDbKeys"></param>
        /// <returns></returns>
        public static ISqlSugarClient GetNewDb<T>(params object[] splitDbKeys)
        {
            var configId = DIUtil.GetRequiredService<IDbSplitProvider>()
                .SplitDb<T>(splitDbKeys);
            return GetNewDbById(configId);
        }

        /// <summary>
        /// 获取指定configId的ISqlSugarClient
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public static ISqlSugarClient GetNewDbById(string configId = null)
        {
            // 主库
            if (string.IsNullOrEmpty(configId) || configId == DefaultConfigId)
                return MainDb.GetConnection(DefaultConfigId).CopyNew();

            var config = GetConfig(configId);
            TryAddDb(config);
            return MainDb.GetConnection(configId).CopyNew();
        }
*/
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
        public static Repository<T> GetRepository<T>(string configId = null)
            where T : class, new()
        {
            var db = GetDbById(configId);
            return new Repository<T>(db);
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
        private static bool TryAddDb(ConnectionElement config)
        {
            if (Convert.ToString(config.ConfigId) == DefaultConfigId)
                return false;
            if (!MainDb.IsAnyConnection(config.ConfigId))
            {
                MainDb.AddConnection(config);
                var currDb = MainDb.GetConnection(config.ConfigId);
                InitDb(currDb, config);
                return true;
            }
            return false;
        }
        internal static void InitDb(ISqlSugarClient db, ConnectionElement config)
        {
            // log
            if (config?.LogEnabled ?? false)
            {
                db.Aop.OnLogExecuting = (sql, paras) =>
                {
                    var tmpSql = config.LogSqlMode switch
                    {
                        0 => sql,
                        1 => UtilMethods.GetNativeSql(sql, paras),
                        2 => UtilMethods.GetSqlString(config.DbType, sql, paras),
                        _ => throw new Exception($"未知LogSqlMode模式: {config.LogSqlMode}")
                    };
                    LogUtil.Log(config.LogLevel, $"执行SQL: {tmpSql}");
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
                var tmpSql = config?.LogSqlMode == 2
                    ? UtilMethods.GetSqlString(config.DbType, ex.Sql, (SugarParameter[])ex.Parametres)
                    : UtilMethods.GetNativeSql(ex.Sql, (SugarParameter[])ex.Parametres);

                var log = DIUtil.GetService<ILogBuilder>();
                if (log != null)
                {
                    log.AddMessage("SqlSugar SQL执行异常");
                    log.AddField("SqlSugar.ConfigId", config?.ConfigId);
                    log.AddField("SqlSugar.SQL", tmpSql);
                    log.AddException(ex);
                }
                LogUtil.Error(ex, "异常SQL: {SQL}", tmpSql);
            };
        }
        #endregion
    }

}
