using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TinyFx.Data.MySql;

namespace TinyFx.Extensions.EPPlus.MySql
{
    public class DbExportExcel
    {
        public string ConnectionString { get; set; }
        public string ExcelFile { get; set; }
        public List<string> Tables { get; set; }
        private MySqlDatabase _db;
        private MySqlSchemaProvider _provider;

        public DbExportExcel(string connStr, string file, List<string> tables)
        {
            ConnectionString = connStr;
            _db = new MySqlDatabase(connStr, 30);
            ExcelFile = file;
            Tables = tables;
            _provider = new MySqlSchemaProvider(_db);
        }

        public void Execute()
        {
            using (var pkg = GetPackage())
            {
                for (int i = 0; i < Tables.Count; i++)
                {
                    var tableName = Tables[i];
                    var schema = _provider.GetTable(tableName);
                    if (schema == null)
                        throw new Exception($"数据表{tableName}不存在");
                    var dt = _db.GetTable(tableName);
                    OnProcess(new ExportExcelProcessArgs
                    {
                        TableIndex = i + 1,
                        TableName = dt.TableName
                    });

                    ExcelWorksheet sheet = null;
                    var config = new ExcelWriteConfig()
                    {
                        StartColumnIndex = 2,
                        HeaderRowIndex = 3,
                        StartRowIndex = 4,
                        IsWriteHeader = true
                    };
                    sheet = pkg.Workbook.Worksheets[dt.TableName]
                        ?? pkg.Workbook.Worksheets.Add(dt.TableName);
                    EPPlusUtil.Write(sheet, dt, config);

                    // 添加类型和注释
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        var column = dt.Columns[j];
                        var colSchema = schema.Columns[column.ColumnName];

                        var commentCells = sheet.Cells[1, config.StartColumnIndex + j];
                        commentCells.Style.Font.Color.SetColor(System.Drawing.Color.DarkGray);
                        commentCells.Style.WrapText = true;//自动换行
                        commentCells.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        commentCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //commentCells.Value = colSchema.Comment;
                        commentCells.RichText.Clear();
                        commentCells.RichText.Add(colSchema.Comment);

                        var typeCells = sheet.Cells[2, config.StartColumnIndex + j];
                        typeCells.Style.Font.Color.SetColor(System.Drawing.Color.DarkGray);
                        typeCells.Style.WrapText = true;//自动换行
                        typeCells.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        typeCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        typeCells.Value = colSchema.EngineTypeStringFull;
                    }
                    // 表头样式
                    /*
                    var range = sheet.Cells[config.HeaderRowIndex, config.StartColumnIndex
                        , dt.Rows.Count + config.HeaderRowIndex, config.StartColumnIndex + dt.Columns.Count - 1];
                    var table = sheet.Tables[dt.TableName];
                    if (table != null)
                    {
                        sheet.Tables.Delete(table);
                    }
                    table = sheet.Tables.Add(range, dt.TableName);
                    table.TableStyle = OfficeOpenXml.Table.TableStyles.Medium2;
                    range.AutoFitColumns();
                    */
                    using (var r = sheet.Cells[config.HeaderRowIndex, config.StartColumnIndex
                        , config.HeaderRowIndex, config.StartColumnIndex + dt.Columns.Count - 1])
                    {
                        r.Style.Font.Bold = true;
                        r.Style.ShrinkToFit = false;
                        r.Style.WrapText = false;
                        //r.AutoFitColumns();
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        r.Style.Fill.SetBackground(OfficeOpenXml.Drawing.eThemeSchemeColor.Accent4);
                        //r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    }
                    // 添加单元格边框
                    using (var r = sheet.Cells[config.HeaderRowIndex, config.StartColumnIndex
                        , dt.Rows.Count + config.HeaderRowIndex, config.StartColumnIndex + dt.Columns.Count - 1])
                    {
                        r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                        r.AutoFitColumns();
                    }
                }
                File.WriteAllBytes(ExcelFile, pkg.GetAsByteArray());
            }
        }
        private ExcelPackage GetPackage()
        {
            EPPlusUtil.NoLicense();
            return !File.Exists(ExcelFile)
                ? new ExcelPackage(new FileInfo(ExcelFile))
                : new ExcelPackage(new FileInfo(ExcelFile), true);
        }

        public EventHandler<ExportExcelProcessArgs> Process;
        public void OnProcess(ExportExcelProcessArgs args)
        {
            Console.WriteLine($"{args.TableIndex} -- {args.TableName}");
            Process?.Invoke(this, args);
        }

    }
    public class ExportExcelProcessArgs
    {
        public int TableIndex { get; set; }
        public string TableName { get; set; }
    }

}
