using MySqlConnector;
using OfficeOpenXml;
using SQLitePCL;
using SqlSugar;
using SS = SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Data;
using TinyFx.Data.SqlSugar;
using TinyFx.Logging;
using static OfficeOpenXml.ExcelErrorValue;

namespace TinyFx.Extensions.EPPlus
{
    public class ExcelImportDb
    {
        private ISqlSugarClient _db;
        public ExcelImportDb(string connectionStringName = null)
        {
            _db = DbUtil.GetDbById(connectionStringName);
        }
        public ExcelImportDb(SS.DbType dbType, string connectionString)
        {
            _db = DbUtil.GetDb(dbType, connectionString);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="fromExcelFile">Excel文件</param>
        /// <param name="tableName">表名</param>
        /// <param name="sheetName">sheet名</param>
        /// <param name="isSame">是否和excel完全一样。false:没有的插入，有的更新</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public async Task Import(string fromExcelFile, string tableName, string sheetName = null, bool isSame = true)
        {
            if (!File.Exists(fromExcelFile))
                throw new FileNotFoundException($"ExcelImportDb导入时Excel文件不存在:{fromExcelFile}");
            sheetName ??= tableName;
            EPPlusUtil.NoLicense();

            using var pkg = new ExcelPackage(new FileInfo(fromExcelFile), true);
            try
            {
                _db.AsTenant().BeginTran();

                var sheet = pkg.Workbook.Worksheets[sheetName];
                if (sheet == null)
                    throw new Exception($"ExcelImportDb导入时sheet不存在: {sheetName}");
                var config = new ExcelReadConfig()
                {
                    StartColumnIndex = 2,
                    HeaderRowIndex = 3,
                    StartRowIndex = 4
                };
                config.SetEndRowChecker(CheckerMode.Empty);
                var srcDt = EPPlusUtil.ReadToTable(sheet, config);
                if (srcDt.Rows.Count == 0)
                {
                    _db.AsTenant().RollbackTran();
                    return;
                }

                //保存
                if (isSame)
                {
                    _db.DbMaintenance.TruncateTable(tableName);
                    await _db.Fastest<DataTable>().AS(tableName).BulkCopyAsync(srcDt);
                }
                else
                {
                    var reuslt = _db.Storageable(srcDt).ToStorage();
                    await reuslt.AsUpdateable.AS(tableName).PageSize(10000).ExecuteCommandAsync();
                    await reuslt.AsInsertable.AS(tableName).PageSize(10000).ExecuteCommandAsync();
                }

                _db.AsTenant().CommitTran();
            }
            catch (Exception ex)
            {
                _db.AsTenant().RollbackTran();
                LogUtil.Error(ex, $"ExcelImportDb导入时Excel文件失败。excel:{fromExcelFile} table:{tableName}");
                throw;
            }
        }
    }
}
