using MySql.Data.MySqlClient;
using MySql.Data.MySqlClientEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TinyFx.Data.MySql
{
    /// <summary>
    /// https://github.com/MySqlBackupNET/MySqlBackup.Net
    /// </summary>
    public static class MySqlBackupUtil
    {
        public static void ExportToFile(string connectionString, string exportFile, ExportInformations exportInfo, Action<ExportProgressArgs> progress=null, Action<ExportCompleteArgs> complete=null)
        {
            var connStr = RepairConnectionString(connectionString);
            using (var conn = new MySqlConnection(connStr))
            {
                using (var cmd = new MySqlCommand())
                {
                    using (var mb = new MySqlBackup(cmd))
                    {
                        if (progress != null)
                            mb.ExportProgressChanged += (sender, e) => { progress(e); };
                        if (complete != null)
                            mb.ExportCompleted += (sender, e) => { complete(e); };
                        cmd.Connection = conn;
                        conn.Open();
                        if (exportInfo != null)
                            mb.ExportInfo = exportInfo;
                        mb.ExportToFile(exportFile);
                        conn.Close();
                    }
                }
            }
        }
        public static void ExportToFile(string connectionString, string optionFile, Action<ExportProgressArgs> progress = null, Action<ExportCompleteArgs> complete = null)
        {
            var content = File.ReadAllText(optionFile);
            var options = SerializerUtil.DeserializeJson<ExportOptions>(content);
            ExportToFile(connectionString, options.ExportFile, options.GetExportInformations(), progress, complete);
        }
        public static void ImportFromFile(string connectionString, string filePath, ImportInformations importInfo=null, Action<ImportProgressArgs> progress = null, Action<ImportCompleteArgs> complete=null)
        {
            var connStr = RepairConnectionString(connectionString);
            using (var conn = new MySqlConnection(connStr))
            {
                using (var cmd = new MySqlCommand())
                {
                    using (var mb = new MySqlBackup(cmd))
                    {
                        if (progress != null)
                            mb.ImportProgressChanged += (sender, e) => { progress(e); };
                        if (complete != null)
                            mb.ImportCompleted += (sender, e) => { complete(e); };
                        cmd.Connection = conn;
                        conn.Open();
                        if (importInfo != null)
                            mb.ImportInfo = importInfo;
                        mb.ImportFromFile(filePath);
                        conn.Close();
                    }
                }
            }
        }

        #region Util
        private static string RepairConnectionString(string connectionString)
        {
            return connectionString.Trim().TrimEnd(';') + ";charset=utf8;convertzerodatetime=true;";
        }
        #endregion
    }
}
