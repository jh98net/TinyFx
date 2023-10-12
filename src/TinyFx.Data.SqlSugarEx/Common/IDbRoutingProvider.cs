using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Data.SqlSugarEx
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
    }
}
