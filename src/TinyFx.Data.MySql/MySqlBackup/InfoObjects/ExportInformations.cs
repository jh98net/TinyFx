using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySql.Data.MySqlClientEx
{
    /// <summary>
    /// Informations and Settings of MySQL Database Export Process
    /// </summary>
    public class ExportInformations
    {
        int _interval = 50;
        string _delimiter = "|";

        List<string> _documentHeaders = null;
        List<string> _documentFooters = null;

        Dictionary<string, string> _customTable = new Dictionary<string, string>();

        List<string> _lstExcludeTables = null;

        /// <summary>
        /// 获取或设置要排除在导出中的表（黑名单）。这些表的行也不会导出
        /// </summary>
        public List<string> ExcludeTables
        {
            get
            {
                if (_lstExcludeTables == null)
                    _lstExcludeTables = new List<string>();
                return _lstExcludeTables;
            }
            set
            {
                _lstExcludeTables = value;
            }
        }

        /// <summary>
        /// 获取文档标题列表
        /// </summary>
        /// <param name="cmd">The MySqlCommand that will be used to retrieve the database default character set.</param>
        /// <returns>List of document headers.</returns>
        public List<string> GetDocumentHeaders(MySqlCommand cmd)
        {
            if (_documentHeaders == null)
            {
                _documentHeaders = new List<string>();
                string databaseCharSet = QueryExpress.ExecuteScalarStr(cmd, "SHOW variables LIKE 'character_set_database';", 1);

                _documentHeaders.Add("/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;");
                _documentHeaders.Add("/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;");
                _documentHeaders.Add("/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;");
                _documentHeaders.Add(string.Format("/*!40101 SET NAMES {0} */;", databaseCharSet));
                //_documentHeaders.Add("/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;");
                //_documentHeaders.Add("/*!40103 SET TIME_ZONE='+00:00' */;");
                _documentHeaders.Add("/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;");
                _documentHeaders.Add("/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;");
                _documentHeaders.Add("/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;");
                _documentHeaders.Add("/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;");
            }

            return _documentHeaders;
        }

        /// <summary>
        /// 设置文档标题
        /// </summary>
        /// <param name="lstHeaders">List of document headers</param>
        public void SetDocumentHeaders(List<string> lstHeaders)
        {
            _documentHeaders = lstHeaders;
        }

        /// <summary>
        /// 获取文档页脚
        /// </summary>
        /// <returns>List of document footers.</returns>
        public List<string> GetDocumentFooters()
        {
            if (_documentFooters == null)
            {
                _documentFooters = new List<string>();
                //_documentFooters.Add("/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;");
                _documentFooters.Add("/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;");
                _documentFooters.Add("/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;");
                _documentFooters.Add("/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;");
                _documentFooters.Add("/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;");
                _documentFooters.Add("/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;");
                _documentFooters.Add("/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;");
                _documentFooters.Add("/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;");
            }

            return _documentFooters;
        }

        /// <summary>
        /// 设置文档标题
        /// </summary>
        /// <param name="lstFooters">List of document footers.</param>
        public void SetDocumentFooters(List<string> lstFooters)
        {
            _documentFooters = lstFooters;
        }

        /// <summary>
        /// 获取或设置将要导出的表的列表。如果不存在，将导出所有表。
        /// </summary>
        public List<string> TablesToBeExportedList
        {
            get
            {
                List<string> lst = new List<string>();
                foreach (KeyValuePair<string, string> kv in _customTable)
                {
                    lst.Add(kv.Key);
                }
                return lst;
            }
            set
            {
                _customTable.Clear();
                foreach (string s in value)
                {
                    _customTable[s] = string.Format("SELECT * FROM `{0}`;", s);
                }
            }
        }

        /// <summary>
        /// 获取或设置将使用定义的自定义SELECT导出的表。如果为空或为空，则将导出所有表和行。
        /// key=表名 value=自定义SELECT语句。
        ///     示例：key=product value=SELECT* FROM product WHERE category = 1;
        /// </summary>
        public Dictionary<string, string> TablesToBeExportedDic
        {
            get
            {
                return _customTable;
            }
            set
            {
                _customTable = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否应该在转储文件中记录转储时间
        /// </summary>
        public bool RecordDumpTime = true;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应将“ CREATE DATABASE”的SQL语句添加到转储文件中。
        /// </summary>
        public bool AddCreateDatabase = false;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应将“ DROP DATABASE”的SQL语句添加到转储文件中
        /// </summary>
        public bool AddDropDatabase = false;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应导出表结构（CREATE TABLE）。
        /// </summary>
        public bool ExportTableStructure = true;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应将“ DROP TABLE”的SQL语句添加到转储文件中。
        /// </summary>
        public bool AddDropTable = true;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应将每个表的自动增量值重置为1
        /// </summary>
        public bool ResetAutoIncrement = false;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应导出行
        /// </summary>
        public bool ExportRows = true;

        /// <summary>
        /// 获取或设置将多个INSERT组合到单个sql中的最大长度。默认值为5MB。
        ///  仅在RowsExportMode =“ Insert”或“ InsertIgnore”或“ Replace”时适用。
        ///  如果RowsExportMode = OnDuplicateKeyUpdate或Update，则将忽略此值。
        /// </summary>
        public int MaxSqlLength = 5 * 1024 * 1024;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应导出存储过程
        /// </summary>
        public bool ExportProcedures = true;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应导出存储的函数
        /// </summary>
        public bool ExportFunctions = true;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应导出存储的触发器
        /// </summary>
        public bool ExportTriggers = true;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应导出存储的视图
        /// </summary>
        public bool ExportViews = true;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应导出存储的事件
        /// </summary>
        public bool ExportEvents = true;

        /// <summary>
        /// 获取或设置一个值，该值指示引发ExportProgressChanged事件的时间间隔（以毫秒为单位）
        /// </summary>
        public int IntervalForProgressReport { get { if (_interval == 0) return 100; return _interval; } set { _interval = value; } }

        /// <summary>
        /// 获取或设置用于导出 Procedures, Functions, Events and Triggers的定界符. Default delimiter is "|".
        /// </summary>
        public string ScriptsDelimiter { get { if (string.IsNullOrEmpty(_delimiter)) return "|"; return _delimiter; } set { _delimiter = value; } }

        /// <summary>
        /// 获取或设置一个值，该值指示导出的脚本（过程，函数，事件，触发器，事件）是否应排除DEFINER.
        /// </summary>
        public bool ExportRoutinesWithoutDefiner = true;

        /// <summary>
        /// 获取或设置枚举值指示应如何导出每个表的行。
        ///     Insert =默认选项。如果导出到新数据库，则建议使用。如果存在主键，则该过程将停止；
        ///     InsertIgnore = 如果主键存在，则跳过它；
        ///     Replace =如果存在主键，则删除该行并插入新数据；
        ///     OnDuplicateKeyUpdate =如果存在主键，请更新该行。如果所有字段都是主键，它将更改为INSERT IGNORE；
        ///     Update =如果主键不存在，请跳过它，并且如果所有字段都是主键，则不会导出任何行。
        /// </summary>
        public RowsDataExportMode RowsExportMode = RowsDataExportMode.Insert;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应使用事务包装行转储。
        /// 如果使用RowsExportMode =“ INSERT”或“ INSERTIGNORE”或“ REPLACE”，否则建议将此值设置为FALSE，否则为TRUE。
        /// </summary>
        public bool WrapWithinTransaction = false;

        /// <summary>
        /// 获取或设置一个值，该值指示用于导出转储的编码。默认值= UTF8Coding（false）
        /// </summary>
        public Encoding TextEncoding = new UTF8Encoding(false);

        /// <summary>
        /// Gets or Sets a enum value indicates how the BLOB should be exported. HexString = Hexa Decimal String (default); BinaryChar = char format.
        /// 默认值：BlobDataExportMode.HexString
        /// 获取或设置一个枚举值，该值指示应如何导出BLOB。
        /// HexString =十六进制十进制字符串（默认）；
        /// BinaryChar = char格式。
        /// 注意：目前，不打算将BLOB导出到Binary Char中用于实际部署。导出到BinaryChar将引发异常，该异常试图警告开发人员此功能旨在用于开发和调试目的。
        /// </summary>
        public BlobDataExportMode BlobExportMode = BlobDataExportMode.HexString;

        /// <summary>
        /// 如果希望帮助调试，修复或开发将BLOB导出为二进制char格式（BlobExportMode=BinaryChar）的功能，请将此值设置为true
        /// </summary>
        public bool BlobExportModeForBinaryStringAllow = false;

        /// <summary>
        /// Gets or Sets a value indicates the method of how the total rows value is being obtained. InformationSchema = Fast, but approximate value; SelectCount = Slow but accurate; Skip = Skip obtaining total rows.
        /// 获取或设置一个值，该值指示如何获取总行值的方法。
        /// 如果要开发进度条，此功能很有用
        /// InformationSchema =快速，但近似值；
        /// SelectCount =缓慢但准确；
        /// 跳过=跳过获取总行数。如果您不执行任何进度报告，请使用此选项
        /// </summary>
        public GetTotalRowsMethod GetTotalRowsMode = GetTotalRowsMethod.InformationSchema;

        public ExportInformations()
        {

        }
    }
}
