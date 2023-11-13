using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Data.SqlSugar
{
    public interface IDbSplitProvider
    {
        /// <summary>
        /// 分库(根据类型和分库标识)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="splitDbKeys"></param>
        /// <returns></returns>
        string SplitDb<T>(params object[] splitDbKeys);

        /// <summary>
        /// 分表(根据实体类型和表数据)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISplitTableService SplitTable();
    }
    public class DefaultSplitProvider : IDbSplitProvider
    {
        public string SplitDb<T>(params object[] splitDbKeys)
        {
            return null;
        }

        public ISplitTableService SplitTable()
        {
            return null;
        }
    }
}
