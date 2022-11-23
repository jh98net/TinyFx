using System;
using System.Collections.Generic;
using System.Text;

namespace MySql.Data.MySqlClientEx
{
    public class ImportInformations
    {
        int _interval = 100;

        /// <summary>
        /// 获取或设置一个值，该值指示引发ExportProgressChanged事件的时间间隔（以毫秒为单位）默认值：100
        /// </summary>
        public int IntervalForProgressReport { get { if (_interval == 0) return 100; return _interval; } set { _interval = value; } }

        /// <summary>
        /// 获取或设置一个值，该值指示是否应忽略导入过程中发生的SQL错误
        /// </summary>
        public bool IgnoreSqlError = false;

        /// <summary>
        /// 获取或设置用于记录错误消息的文件路径
        /// </summary>
        public string ErrorLogFile = "";

        public ImportInformations()
        { }
    }
}
