using EasyNetQ.Events;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Data;
using TinyFx.Data.SqlSugar;
using TinyFx.Text;

namespace TinyFx.BIZ.DataSplit.DataMove
{
    internal class SplitMaxRowsJob : BaseDataMoveJob
    {
        public SplitMaxRowsJob(Ss_split_tableEO option, DateTime execTime) : base(option, execTime)
        {
            if ((HandleMode)option.HandleMode != HandleMode.SplitMaxRows)
                throw new Exception("DataMove.SplitMaxRowsJob时HandleMode必须是SplitMaxRows");
            if (_option.SplitMaxRowCount <= 0)
                throw new Exception("DataMove.SplitMaxRowsJob时SplitMaxRowHours必须大于0");
            if (_option.SplitMaxRowHours == 0)
                throw new Exception("DataMove.SplitMaxRowsJob时SplitMaxRowHours必须大于0");
        }

        protected override async Task ExecuteJob()
        {
            var lastItem = await DbUtil.GetQueryable<Ss_split_table_detailEO>()
                .Where(it => it.DatabaseId == _option.DatabaseId && it.TableName == _option.TableName && it.Status == 1)
                .OrderBy(it => it.RecDate, OrderByType.Desc)
                .FirstAsync();
            if (lastItem == null)
            {
                await ExecFirst();
            }
            else
            {
                await ExecNext(lastItem);
            }
        }

        private async Task ExecFirst()
        {
            var count = await _database.Queryable<object>().AS(_option.TableName).With(SqlWith.NoLock).CountAsync();
            if (count == 0 || count < _option.SplitMaxRowCount)
                return;

            var tableData = await GetTableData(_option.TableName);
            var detailList = new List<Ss_split_table_detailEO>();
            // 原始表
            detailList.Add(new Ss_split_table_detailEO
            {
                DetailID = ObjectId.NewId(),
                LogID = _logEo.LogID,
                DatabaseId = _option.DatabaseId,
                TableName = _option.TableName,
                ColumnName = _option.ColumnName,
                ColumnType = _option.ColumnType,
                HandleMode = _option.HandleMode,
                SplitTableName = _option.TableName,
                BeginValue = tableData.Begin.Value,
                BeginDate = tableData.Begin.Date,
                EndValue = null,
                EndDate = null,
                RowNum = count,
                Status = 1,
                RecDate = DateTime.UtcNow
            });
            // 新分表
            detailList.Add(new Ss_split_table_detailEO
            {
                DetailID = ObjectId.NewId(),
                LogID = _logEo.LogID,
                DatabaseId = _option.DatabaseId,
                TableName = _option.TableName,
                ColumnName = _option.ColumnName,
                ColumnType = _option.ColumnType,
                HandleMode = _option.HandleMode,
                SplitTableName = tableData.SplitTableName,
                BeginValue = tableData.Next.Value,
                BeginDate = tableData.Next.Date,
                EndValue = null,
                EndDate = null,
                RowNum = count,
                Status = 1,
                RecDate = DateTime.UtcNow
            });
            var tm = new DbTransactionManager();
            try
            {
                await CreateTable(tableData.SplitTableName, GetDb(tm));
                await tm.GetDb().Insertable(detailList).ExecuteCommandAsync();
                tm.Commit();
            }
            catch
            {
                tm.Rollback();
                throw;
            }
        }

        private async Task ExecNext(Ss_split_table_detailEO lastEo)
        {
            var count = await _database.Queryable<object>().AS(lastEo.SplitTableName).With(SqlWith.NoLock).CountAsync();
            if (count == 0 || count < _option.SplitMaxRowCount)
                return;
            lastEo.RowNum = count;

            var tableData = await GetTableData(lastEo.SplitTableName);
            var newEo = new Ss_split_table_detailEO
            {
                DetailID = ObjectId.NewId(),
                LogID = _logEo.LogID,
                DatabaseId = _option.DatabaseId,
                TableName = _option.TableName,
                ColumnName = _option.ColumnName,
                ColumnType = _option.ColumnType,
                HandleMode = _option.HandleMode,
                SplitTableName = tableData.SplitTableName,
                BeginValue = tableData.Next.Value,
                BeginDate = tableData.Next.Date,
                EndValue = null,
                EndDate = null,
                RowNum = 0,
                Status = 1,
                RecDate = DateTime.UtcNow
            };

            var tm = new DbTransactionManager();
            try
            {
                await tm.GetDb().Updateable(lastEo)
                    .UpdateColumns(it => new { it.RowNum })
                    .ExecuteCommandAsync();
                await CreateTable(tableData.SplitTableName, GetDb(tm));
                await tm.GetDb().Insertable(newEo).ExecuteCommandAsync();
                tm.Commit();
            }
            catch
            {
                tm.Rollback();
                throw;
            }
        }
        private async Task<SplitMaxRowTableData> GetTableData(string tableName)
        {
            var ret = new SplitMaxRowTableData();
            var query = _database.Queryable<object>().AS(tableName).With(SqlWith.NoLock);
            switch ((ColumnType)_option.ColumnType)
            {
                case ColumnType.DateTime: // DateTime
                    ret.Begin.Date = await query.MinAsync<DateTime>(_option.ColumnName);
                    ret.Begin.Value = ret.Begin.Date.ToFormatString();
                    ret.End.Date = await query.MaxAsync<DateTime>(_option.ColumnName);
                    ret.End.Value = ret.End.Date.ToFormatString();
                    var dt = ret.End.Date.AddHours(_option.SplitMaxRowHours);
                    ret.Next.Date = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ret.Next.Date.ToFormatString();
                    break;
                case ColumnType.ObjectId: // ObjectId
                    query = query.Where($"LENGTH(`{_option.ColumnName}`)=24");
                    ret.Begin.Value = await query.MinAsync<string>(_option.ColumnName);
                    ret.Begin.Date = ObjectId.ParseTimestamp(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_option.ColumnName);
                    ret.End.Date = ObjectId.ParseTimestamp(ret.End.Value);
                    var dt1 = ret.End.Date.AddHours(_option.SplitMaxRowHours);
                    ret.Next.Date = new DateTime(dt1.Year, dt1.Month, dt1.Day, dt1.Hour, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ObjectId.TimestampId(ret.Next.Date);
                    break;
                case ColumnType.NumDay:
                    query = query.Where($"LENGTH(`{_option.ColumnName}`)=8");
                    ret.Begin.Value = await query.MinAsync<string>(_option.ColumnName);
                    ret.Begin.Date = _columnHelper.ColumnValueToDate(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_option.ColumnName);
                    ret.End.Date = _columnHelper.ColumnValueToDate(ret.End.Value);
                    var dt2 = ret.End.Date.AddDays(1);
                    ret.Next.Date = new DateTime(dt2.Year, dt2.Month, dt2.Day, 0, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ret.Next.Date.ToString("yyyy-MM-dd");
                    break;
                case ColumnType.NumWeek:
                    query = query.Where($"LENGTH(`{_option.ColumnName}`)=6");
                    ret.Begin.Value = await query.MinAsync<string>(_option.ColumnName);
                    ret.Begin.Date = _columnHelper.ColumnValueToDate(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_option.ColumnName);
                    ret.End.Date = _columnHelper.ColumnValueToDate(ret.End.Value);
                    var dt3 = DateTimeUtil.BeginDayOfNextWeek(ret.End.Date);
                    ret.Next.Date = new DateTime(dt3.Year, dt3.Month, dt3.Day, 0, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ret.Next.Date.ToYearWeekString();
                    break;
                case ColumnType.NumMonth:
                    query = query.Where($"LENGTH(`{_option.ColumnName}`)=6");
                    ret.Begin.Value = await query.MinAsync<string>(_option.ColumnName);
                    ret.Begin.Date = _columnHelper.ColumnValueToDate(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_option.ColumnName);
                    ret.End.Date = _columnHelper.ColumnValueToDate(ret.End.Value);
                    var dt4 = ret.End.Date.AddMonths(1);
                    ret.Next.Date = new DateTime(dt4.Year, dt4.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ret.Next.Date.ToString("yyyyMM");
                    break;
                case ColumnType.NumQuarter:
                    query = query.Where($"LENGTH(`{_option.ColumnName}`)=5");
                    ret.Begin.Value = await query.MinAsync<string>(_option.ColumnName);
                    ret.Begin.Date = _columnHelper.ColumnValueToDate(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_option.ColumnName);
                    ret.End.Date = _columnHelper.ColumnValueToDate(ret.End.Value);
                    var q = DateTimeUtil.QuarterOfYear(ret.End.Date);
                    int y = ret.End.Date.Year;
                    if (q == 4)
                    {
                        y = y + 1;
                        q = 1;
                    }
                    var m = q * 3 - 2;
                    ret.Next.Date = new DateTime(y, m, 1, 0, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = $"{y}{q}";
                    break;
                case ColumnType.NumYear:
                    query = query.Where($"LENGTH(`{_option.ColumnName}`)=4");
                    ret.Begin.Value = await query.MinAsync<string>(_option.ColumnName);
                    ret.Begin.Date = _columnHelper.ColumnValueToDate(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_option.ColumnName);
                    ret.End.Date = _columnHelper.ColumnValueToDate(ret.End.Value);
                    var dt5 = ret.End.Date.AddYears(1);
                    ret.Next.Date = new DateTime(dt5.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ret.Next.Date.Year.ToString();
                    break;
            }
            ret.SplitTableName = $"{_option.TableName}_{ret.Next.Date.ToString("yyyyMMddHH")}";
            return ret;
        }
    }
    internal class SplitMaxRowTableData
    {
        public ColumnValue Begin { get; set; } = new();
        public ColumnValue End { get; set; } = new();
        public ColumnValue Next { get; set; } = new();

        public string SplitTableName { get; set; }
    }
}
