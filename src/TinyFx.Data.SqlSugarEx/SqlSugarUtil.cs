using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;

namespace TinyFx.Data.SqlSugarEx
{
    public static class SqlSugarUtil
    {
        #region Db
        /// <summary>
        /// 全局DB，仅用作事务
        /// </summary>
        public static SqlSugarScope GlobalDb
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
                    GlobalDb.AddConnection(config);
                    ret = GlobalDb.GetConnection(configId);
                }
            }
            return ret;
        }
        public static ISqlSugarClient GetDb<T>(params object[] routingData)
        {
            var configId = DIUtil.GetRequiredService<IDbRoutingProvider>()
                .RouteDb<T>(routingData);
            return GetDb(configId);
        }
        #endregion

        #region 事务
        public static void BeginTran(IsolationLevel level) => GlobalDb.BeginTran(level);
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
        /// <param name="routingData">分库标识</param>
        /// <returns></returns>
        public static Repository<T> CreateRepository<T>(params object[] routingData)
         where T : class, new()
        {
            return new Repository<T>(routingData);
        }
        #endregion
    }
}
