using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Data.SqlSugar
{
    public class ConnectionElement : ConnectionConfig
    {
        public bool LogEnabled { get; set; }
        /// <summary>
        /// SQL日志模式0-默认 1-原生 2-无参数化
        /// </summary>
        public int LogSqlMode { get; set; }
        /// <summary>
        /// 是否使用读写分离
        /// </summary>
        public bool SlaveEnabled { get; set; }
    }
}
