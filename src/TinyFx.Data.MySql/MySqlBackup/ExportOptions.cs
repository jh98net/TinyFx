using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySql.Data.MySqlClientEx
{
    public class ExportOptions
    {
        public string ExportFile { get; set; }
        public List<string> Tables { get; set; } = new List<string>();
        public Dictionary<string, string> TableSqls { get; set; } = new Dictionary<string, string>();
        public bool AddCreateDatabase { get; set; } = false;
        public bool AddDropDatabase { get; set; } = false;
        public bool ExportTableStructure { get; set; } = true;
        public bool AddDropTable { get; set; } = true;
        public bool ResetAutoIncrement { get; set; } = false;
        public bool ExportRows { get; set; } = true;
        public bool ExportProcedures { get; set; } = true;
        public bool ExportFunctions { get; set; } = true;
        public bool ExportTriggers { get; set; } = true;
        public bool ExportViews { get; set; } = true;
        public bool ExportEvents { get; set; } = true;
        public RowsDataExportMode RowsExportMode { get; set; } = RowsDataExportMode.Insert;
        [System.Text.Json.Serialization.JsonIgnore]
        public bool WrapWithinTransaction
        {
            get
            {
                return (RowsExportMode == RowsDataExportMode.OnDuplicateKeyUpdate
                    || RowsExportMode == RowsDataExportMode.Update);
            }
        }

        public ExportInformations GetExportInformations()
        {
            var ret = new ExportInformations
            {
                TablesToBeExportedList = Tables,
                AddCreateDatabase = AddCreateDatabase,
                AddDropDatabase = AddDropDatabase,
                ExportTableStructure = ExportTableStructure,
                AddDropTable = AddDropTable,
                ResetAutoIncrement = ResetAutoIncrement,
                ExportRows = ExportRows,
                ExportProcedures = ExportProcedures,
                ExportFunctions = ExportFunctions,
                ExportTriggers = ExportTriggers,
                ExportViews = ExportViews,
                ExportEvents = ExportEvents,
                RowsExportMode = RowsExportMode,
                WrapWithinTransaction = WrapWithinTransaction,
            };
            foreach (var pair in TableSqls)
            {
                if (ret.TablesToBeExportedDic.ContainsKey(pair.Key))
                    ret.TablesToBeExportedDic[pair.Key] = pair.Value;
                else
                    ret.TablesToBeExportedDic.Add(pair.Key, pair.Value);
            }
            return ret;
        }
    }
}
