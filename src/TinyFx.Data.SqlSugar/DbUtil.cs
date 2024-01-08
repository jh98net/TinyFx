using SqlSugar;
using SS = SqlSugar;
using TinyFx.Configuration;
using TinyFx.Logging;
using System.Data;
using System.Linq.Expressions;
using System.Reflection.Emit;

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
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public static ISqlSugarClient GetDb(object splitDbKey = null)
            => GetDb<object>(splitDbKey);

        /// <summary>
        /// 根据IDbSplitProvider获取ISqlSugarClient
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public static ISqlSugarClient GetDb<T>(object splitDbKey = null)
        {
            var configId = DIUtil.GetRequiredService<IDbSplitProvider>()
                .SplitDb<T>(splitDbKey);
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
        #endregion

        #region Repository
        /// <summary>
        /// 创建Repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="splitDbKey">分库标识</param>
        /// <returns></returns>
        public static Repository<T> GetRepository<T>(object splitDbKey = null)
            where T : class, new()
        {
            return new Repository<T>(splitDbKey);
        }
        public static Repository<T> GetRepositoryById<T>(string configId = null)
            where T : class, new()
        {
            var db = GetDbById(configId);
            return new Repository<T>(db);
        }
        #endregion

        #region Queryable
        public static async Task<T> GetByIdAsync<T>(dynamic id, object splitDbKey = null)
              where T : class, new()
          => await GetRepository<T>(splitDbKey).GetByIdAsync(id);
        public static async Task<T> GetByIdAsync<T>(T id, object splitDbKey = null)
             where T : class, new()
         => await GetRepository<T>(splitDbKey).GetByIdAsync(id);
        public static async Task<T> GetByIdsAsync<T>(List<T> ids, object splitDbKey = null)
             where T : class, new()
         => await GetRepository<T>(splitDbKey).GetByIdAsync(ids);

        /// <summary>
        /// 获取查询器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public static ISugarQueryable<T> GetQueryable<T>(object splitDbKey = null)
            => GetDb<T>(splitDbKey).Queryable<T>();
        public static ISugarQueryable<T, T2> GetQueryable<T, T2>(Expression<Func<T, T2, JoinQueryInfos>> joinExpression, object splitDbKey = null)
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public static ISugarQueryable<T, T2, T3> GetQueryable<T, T2, T3>(Expression<Func<T, T2, T3, JoinQueryInfos>> joinExpression, object splitDbKey = null)
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public static ISugarQueryable<T, T2, T3, T4> GetQueryable<T, T2, T3, T4>(Expression<Func<T, T2, T3, T4, JoinQueryInfos>> joinExpression, object splitDbKey = null)
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public static ISugarQueryable<T, T2, T3, T4, T5> GetQueryable<T, T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, JoinQueryInfos>> joinExpression, object splitDbKey = null)
            => GetDb<T>(splitDbKey).Queryable(joinExpression);

        /// <summary>
        /// 获取查询器(inner join)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public static ISugarQueryable<T, T2> GetQueryable<T, T2>(Expression<Func<T, T2, bool>> joinExpression, object splitDbKey = null)
            where T : class, new()
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public static ISugarQueryable<T, T2, T3> GetQueryable<T, T2, T3>(Expression<Func<T, T2, T3, bool>> joinExpression, object splitDbKey = null)
            where T : class, new()
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public static ISugarQueryable<T, T2, T3, T4> GetQueryable<T, T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> joinExpression, object splitDbKey = null)
            where T : class, new()
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public static ISugarQueryable<T, T2, T3, T4, T5> GetQueryable<T, T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, bool>> joinExpression, object splitDbKey = null)
            where T : class, new()
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        #endregion

        #region Delete & Insert & Update
        public static async Task<bool> DeleteByIdAsync<T>(dynamic id, object splitDbKey = null)
              where T : class, new()
          => await GetRepository<T>(splitDbKey).DeleteByIdAsync(id);
        public static async Task<bool> DeleteByIdsAsync<T>(dynamic[] ids, object splitDbKey = null)
              where T : class, new()
          => await GetRepository<T>(splitDbKey).DeleteByIdsAsync(ids);
        public static async Task<bool> DeleteByIdAsync<T>(T id, object splitDbKey = null)
              where T : class, new()
          => await GetRepository<T>(splitDbKey).DeleteByIdAsync(id);
        public static async Task<bool> DeleteByIdsAsync<T>(List<T> ids, object splitDbKey = null)
              where T : class, new()
          => await GetRepository<T>(splitDbKey).DeleteByIdsAsync(ids);
        public static async Task<bool> DeleteAsync<T>(T item, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).DeleteAsync(item);
        public static async Task<bool> DeleteAsync<T>(List<T> items, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).DeleteAsync(items);

        public static async Task<bool> InsertAsync<T>(T item, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).InsertAsync(item);
        public static async Task<bool> InsertAsync<T>(List<T> items, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).InsertRangeAsync(items);

        public static async Task<bool> UpdateAsync<T>(T item, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).UpdateAsync(item);
        public static async Task<bool> UpdateAsync<T>(List<T> items, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).UpdateRangeAsync(items);

        public static async Task<bool> InsertOrUpdateAsync<T>(T item, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).InsertOrUpdateAsync(item);
        public static async Task<bool> InsertOrUpdateAsync<T>(List<T> items, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).InsertOrUpdateAsync(items);
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
