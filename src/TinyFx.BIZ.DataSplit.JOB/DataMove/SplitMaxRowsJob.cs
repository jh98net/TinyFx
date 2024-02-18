using EasyNetQ.Events;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Data;
using TinyFx.Data.SqlSugar;
using TinyFx.Text;

namespace TinyFx.BIZ.DataSplit.JOB.DataMove
{
    internal class SplitMaxRowsJob : BaseDataMoveJob
    {
        public SplitMaxRowsJob(Ss_split_tableEO item, string defaultConfigId, DateTime execTime) : base(item, defaultConfigId, execTime)
        {
            if ((HandleMode)item.HandleMode != HandleMode.SplitMaxRows)
                throw new Exception("DataMove.SplitMaxRowsJob时HandleMode必须是SplitMaxRows");
            if (_item.SplitMaxRowCount <= 0)
                throw new Exception("DataMove.SplitMaxRowsJob时SplitMaxRowHours必须大于0");
            var columnType = (ColumnType)_item.ColumnType;
            if (_item.SplitMaxRowHours == 0 && (columnType == ColumnType.DateTime || columnType == ColumnType.ObjectId))
                throw new Exception("DataMove.SplitMaxRowsJob时SplitMaxRowHours必须大于0");
        }

        protected override async Task ExecuteJob()
        {
            var lastItem = await GetMainDb().Queryable<Ss_split_table_detailEO>()
                .Where(it => it.DatabaseId == _item.DatabaseId && it.TableName == _item.TableName && it.Status == 1)
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
            var count = await GetItemDb().Queryable<object>().AS(_item.TableName).With(SqlWith.NoLock).CountAsync();
            if (count == 0 || count < _item.SplitMaxRowCount)
                return;
            _logEo.RowNum = count;

            var tableData = await GetTableData(_item.TableName);
            var detailList = new List<Ss_split_table_detailEO>();
            // 原始表
            detailList.Add(new Ss_split_table_detailEO
            {
                DetailID = ObjectId.NewId(),
                LogID = _logEo.LogID,
                DatabaseId = _item.DatabaseId,
                TableName = _item.TableName,
                ColumnName = _item.ColumnName,
                ColumnType = _item.ColumnType,
                HandleMode = _item.HandleMode,
                SplitTableName = _item.TableName,
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
                DatabaseId = _item.DatabaseId,
                TableName = _item.TableName,
                ColumnName = _item.ColumnName,
                ColumnType = _item.ColumnType,
                HandleMode = _item.HandleMode,
                SplitTableName = tableData.SplitTableName,
                BeginValue = tableData.Next.Value,
                BeginDate = tableData.Next.Date,
                EndValue = null,
                EndDate = null,
                RowNum = 0,
                Status = 1,
                RecDate = DateTime.UtcNow
            });
            _logEo.BeginDate = tableData.Next.Date;
            _logEo.BeginValue = tableData.Next.Value;
            _logEo.HandleTables += $"{tableData.SplitTableName}{Environment.NewLine}";

            var tm = new DbTransactionManager();
            try
            {
                await CreateTable(tableData.SplitTableName, tm);
                await GetMainDb(tm).Insertable(detailList).ExecuteCommandAsync();
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
            var count = await GetItemDb().Queryable<object>().AS(lastEo.SplitTableName).With(SqlWith.NoLock).CountAsync();
            if (count == 0 || count < _item.SplitMaxRowCount)
                return;
            _logEo.RowNum = count;
            lastEo.RowNum = count;

            var tableData = await GetTableData(lastEo.SplitTableName);
            var newEo = new Ss_split_table_detailEO
            {
                DetailID = ObjectId.NewId(),
                LogID = _logEo.LogID,
                DatabaseId = _item.DatabaseId,
                TableName = _item.TableName,
                ColumnName = _item.ColumnName,
                ColumnType = _item.ColumnType,
                HandleMode = _item.HandleMode,
                SplitTableName = tableData.SplitTableName,
                BeginValue = tableData.Next.Value,
                BeginDate = tableData.Next.Date,
                EndValue = null,
                EndDate = null,
                RowNum = 0,
                Status = 1,
                RecDate = DateTime.UtcNow
            };
            _logEo.BeginDate = tableData.Next.Date;
            _logEo.BeginValue = tableData.Next.Value;
            _logEo.HandleTables += $"{tableData.SplitTableName}{Environment.NewLine}";
            var tm = new DbTransactionManager();
            try
            {
                await GetMainDb(tm).Updateable(lastEo)
                    .UpdateColumns(it => new { it.RowNum })
                    .ExecuteCommandAsync();
                await CreateTable(tableData.SplitTableName, tm);
                await GetMainDb(tm).Insertable(newEo).ExecuteCommandAsync();
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
            var query = GetItemDb().Queryable<object>().AS(tableName).With(SqlWith.NoLock);
            switch ((ColumnType)_item.ColumnType)
            {
                case ColumnType.DateTime: // DateTime
                    ret.Begin.Date = await query.MinAsync<DateTime>(_item.ColumnName);
                    ret.Begin.Value = ret.Begin.Date.ToFormatString();
                    ret.End.Date = await query.MaxAsync<DateTime>(_item.ColumnName);
                    ret.End.Value = ret.End.Date.ToFormatString();
                    var dt = ret.End.Date.AddHours(_item.SplitMaxRowHours);
                    ret.Next.Date = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ret.Next.Date.ToFormatString();
                    break;
                case ColumnType.ObjectId: // ObjectId
                    query = query.Where($"LENGTH(`{_item.ColumnName}`)=24");
                    ret.Begin.Value = await query.MinAsync<string>(_item.ColumnName);
                    ret.Begin.Date = ObjectId.ParseTimestamp(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_item.ColumnName);
                    ret.End.Date = ObjectId.ParseTimestamp(ret.End.Value);
                    var dt1 = ret.End.Date.AddHours(_item.SplitMaxRowHours);
                    ret.Next.Date = new DateTime(dt1.Year, dt1.Month, dt1.Day, dt1.Hour, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ObjectId.TimestampId(ret.Next.Date);
                    break;
                case ColumnType.NumDay:
                    query = query.Where($"LENGTH(`{_item.ColumnName}`)=8");
                    ret.Begin.Value = await query.MinAsync<string>(_item.ColumnName);
                    ret.Begin.Date = _columnHelper.ColumnValueToDate(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_item.ColumnName);
                    ret.End.Date = _columnHelper.ColumnValueToDate(ret.End.Value);
                    var dt2 = ret.End.Date.AddDays(1);
                    ret.Next.Date = new DateTime(dt2.Year, dt2.Month, dt2.Day, 0, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ret.Next.Date.ToString("yyyy-MM-dd");
                    break;
                case ColumnType.NumWeek:
                    query = query.Where($"LENGTH(`{_item.ColumnName}`)=6");
                    ret.Begin.Value = await query.MinAsync<string>(_item.ColumnName);
                    ret.Begin.Date = _columnHelper.ColumnValueToDate(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_item.ColumnName);
                    ret.End.Date = _columnHelper.ColumnValueToDate(ret.End.Value);
                    var dt3 = DateTimeUtil.BeginDayOfNextWeek(ret.End.Date);
                    ret.Next.Date = new DateTime(dt3.Year, dt3.Month, dt3.Day, 0, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ret.Next.Date.ToYearWeekString();
                    break;
                case ColumnType.NumMonth:
                    query = query.Where($"LENGTH(`{_item.ColumnName}`)=6");
                    ret.Begin.Value = await query.MinAsync<string>(_item.ColumnName);
                    ret.Begin.Date = _columnHelper.ColumnValueToDate(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_item.ColumnName);
                    ret.End.Date = _columnHelper.ColumnValueToDate(ret.End.Value);
                    var dt4 = ret.End.Date.AddMonths(1);
                    ret.Next.Date = new DateTime(dt4.Year, dt4.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ret.Next.Date.ToString("yyyyMM");
                    break;
                case ColumnType.NumQuarter:
                    query = query.Where($"LENGTH(`{_item.ColumnName}`)=5");
                    ret.Begin.Value = await query.MinAsync<string>(_item.ColumnName);
                    ret.Begin.Date = _columnHelper.ColumnValueToDate(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_item.ColumnName);
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
                    query = query.Where($"LENGTH(`{_item.ColumnName}`)=4");
                    ret.Begin.Value = await query.MinAsync<string>(_item.ColumnName);
                    ret.Begin.Date = _columnHelper.ColumnValueToDate(ret.Begin.Value);
                    ret.End.Value = await query.MaxAsync<string>(_item.ColumnName);
                    ret.End.Date = _columnHelper.ColumnValueToDate(ret.End.Value);
                    var dt5 = ret.End.Date.AddYears(1);
                    ret.Next.Date = new DateTime(dt5.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    ret.Next.Value = ret.Next.Date.Year.ToString();
                    break;
            }
            ret.SplitTableName = $"{_item.TableName}_{ret.Next.Date.ToString("yyyyMMddHH")}";
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
