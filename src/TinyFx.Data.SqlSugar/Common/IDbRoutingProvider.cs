using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Data.SqlSugar
{
    public interface IDbRoutingProvider
    {
        /// <summary>
        /// 路由数据库(根据类型和分库标识)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="routingDbKeys"></param>
        /// <returns></returns>
        string RouteDb<T>(params object[] routingDbKeys);

        /// <summary>
        /// 路由表(根据实体类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISplitTableService RouteTable<T>();

        /// <summary>
        /// 路由所有表名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<string> RouteTables<T>();
    }
    public class DefaultDbRoutingProvider : IDbRoutingProvider
    {
        public string RouteDb<T>(params object[] routingDbKeys)
        {
            return null;
        }

        public ISplitTableService RouteTable<T>()
        {
            return null;
        }

        public List<string> RouteTables<T>()
        {
            var attr = typeof(T).GetCustomAttribute<SugarTable>();
            if (attr == null) return null;
            return new List<string> { attr.TableName };
        }
    }
}
