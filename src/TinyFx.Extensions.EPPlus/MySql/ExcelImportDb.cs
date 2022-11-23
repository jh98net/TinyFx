using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TinyFx.Data.MySql;
using System.Linq;
using System.Data;
using TinyFx.Extensions.EPPlus;
using TinyFx.Data;
using MySql.Data.MySqlClient;

namespace TinyFx.Extensions.EPPlus.MySql
{
    public class ExcelImportDb
    {
        public string ConnectionString { get; set; }
        public string ExcelFile { get; set; }
        public List<string> Tables { get; set; }
        private MySqlDatabase _db;
        private MySqlOrmProvider _ormProvider;
        public ExcelImportDb(string connStr, string file, List<string> tables = null)
        {
            ConnectionString = connStr;
            _db = new MySqlDatabase(connStr, 30);
            ExcelFile = file;
            Tables = tables;
            _ormProvider = new MySqlOrmProvider();
        }
        public void Execute()
        {
            if (!File.Exists(ExcelFile))
                throw new FileNotFoundException($"Excel文件不存在:{ExcelFile}");
            EPPlusUtil.NoLicense();
            using (var pkg = new ExcelPackage(new FileInfo(ExcelFile), true))
            {
                var isSelect = Tables != null && Tables.Count > 0;
                if (!isSelect)
                    Tables = pkg.Workbook.Worksheets.Select(item => item.Name).ToList();
                var schemas = new MySqlSchemaProvider(_db).GetTables(Tables).ToList();
                if (isSelect)
                {
                    for (int i = 0; i < Tables.Count; i++)
                    {
                        if (schemas[i] == null)
                            throw new Exception($"excel文件 {ExcelFile} 中sheet名称为 {Tables[i]} 对应的数据表不存在");
                    }
                }
                else
                {
                    // 过滤不存在的table
                    schemas = schemas.SkipWhile(t => t == null).ToList();
                    Tables = schemas.Select(s => s.TableName).ToList();
                }
                var tm = new TransactionManager();
                try
                {
                    for (int i = Tables.Count - 1; i >= 0; i--)
                    {
                        _db.ExecSqlNonQuery($"delete from {Tables[i]}", tm);
                    }
                    for (int i = 0; i < schemas.Count; i++)
                    {
                        var table = schemas[i];
                        if (table == null)
                            throw new Exception($"excel文件 {ExcelFile} 中sheet名称为 {Tables[i]} 对应的数据表不存在");
                        try
                        {
                            var sheet = pkg.Workbook.Worksheets[table.TableName];
                            if (sheet == null)
                                throw new Exception($"TableName: {table.TableName} 在Excel中对应的Sheet不存在。");
                            var config = new ExcelReadConfig()
                            {
                                StartColumnIndex = 2,
                                HeaderRowIndex = 3,
                                StartRowIndex = 4
                            };
                            config.SetEndRowChecker(CheckerMode.Empty);
                            var srcDt = EPPlusUtil.ReadToTable(sheet, config);
                            if (srcDt.Rows.Count == 0)
                                continue;

                            if (table.HasAutoIncrementColumn)
                            {
                                _db.ExecSqlNonQuery($"alter table {table.TableName} modify column {table.AutoIncrementColumn.ColumnName} {table.AutoIncrementColumn.EngineTypeStringFull}", tm);
                            }
                            using (var dao = new MySqlSqlDao(_ormProvider.BuildInsertSql(table), _db))
                            {
                                for (int j = 0; j < srcDt.Columns.Count; j++)
                                {
                                    var columnSchema = table.Columns[srcDt.Columns[j].ColumnName];
                                    var dbType = ((MySqlColumnSchema)columnSchema).DbType;
                                    dao.AddInParameter(columnSchema.SqlParamName, null, dbType);
                                }
                                for (int k = 0; k < srcDt.Rows.Count; k++)
                                {
                                    var row = srcDt.Rows[k];
                                    for (int j = 0; j < srcDt.Columns.Count; j++)
                                    {
                                        var columnSchema = (MySqlColumnSchema)table.Columns[srcDt.Columns[j].ColumnName];
                                        var parameter = dao.Command.Parameters[columnSchema.SqlParamName] as MySqlParameter;
                                        var valueStr = Convert.ToString(row[j]);
                                        if (!columnSchema.AllowDBNull && string.IsNullOrEmpty(valueStr) && !columnSchema.HasDefaultValue)
                                            throw new Exception($"表{table.TableName}行{config.StartRowIndex + k}列{config.StartColumnIndex + j}-{columnSchema.ColumnName}不能为空");
                                        try
                                        {
                                            parameter.Value = !string.IsNullOrEmpty(valueStr)
                                                ? valueStr.To(columnSchema.DotNetType)
                                                : columnSchema.OrmDefaultValue;
                                        }
                                        catch (Exception ex)
                                        {
                                            throw new Exception($"表{table.TableName}行{config.StartRowIndex + k}列{config.StartColumnIndex + j}-{columnSchema.ColumnName}\n值{valueStr}转换类型{columnSchema.DotNetType}异常! \n异常消息:{ex.Message}");
                                        }
                                    }
                                    dao.ExecNonQuery(tm);
                                }
                            }
                            if (table.HasAutoIncrementColumn)
                            {
                                _db.ExecSqlNonQuery($"alter table {table.TableName} modify column {table.AutoIncrementColumn.ColumnName} {table.AutoIncrementColumn.EngineTypeStringFull} auto_increment", tm);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Excel导入Db失败。File:{ExcelFile} TableName:{table.TableName} \nErr:{ex.Message}");
                        }
                    }
                    tm.Commit();
                }
                catch
                {
                    tm.Rollback();
                    throw;
                }
            }
        }
    }
}
