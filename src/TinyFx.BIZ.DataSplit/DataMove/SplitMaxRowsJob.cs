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
            var count = await _database.Queryable<object>().AS(_option.TableName).CountAsync();
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
                ColumnMin = tableData.MinValue,
                ColumnMax = null,
                RowCount = count,
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
                ColumnMin = tableData.NextMinValue,
                ColumnMax = null,
                RowCount = count,
                Status = 1,
                RecDate = DateTime.UtcNow
            });
            var tm = new DbTransactionManager();
            try
            {
                await tm.GetDb().Insertable(detailList).ExecuteCommandAsync();
                _database.Ado.CommitTran();
            }
            catch
            {
                _database.Ado.RollbackTran();
                throw;
            }
        }

        private async Task ExecNext(Ss_split_table_detailEO lastEo)
        {
            var count = await _database.Queryable<object>().AS(lastEo.SplitTableName).CountAsync();
            if (count == 0 || count < _option.SplitMaxRowCount)
                return;
            lastEo.RowCount = count;

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
                ColumnMin = tableData.NextMinValue,
                ColumnMax = null,
                RowCount = 0,
                Status = 1,
                RecDate = DateTime.UtcNow
            };

            var tm = new DbTransactionManager();
            try
            {
                await tm.GetDb().Updateable(lastEo)
                    .UpdateColumns(it => new { it.RowCount })
                    .ExecuteCommandAsync();
                await tm.GetDb().Insertable(newEo).ExecuteCommandAsync();
                await CreateTable(tableData.SplitTableName, GetDb(tm));
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
            var ret = new SplitMaxRowTableData()
            {
                ColumnName = _option.ColumnName,
                ColumnType = _option.ColumnType,
            };
            var query = _database.Queryable<object>().AS(tableName);
            switch (_option.ColumnType)
            {
                case 0: // DateTime
                    ret.MinDate = await query.MinAsync<DateTime>(_option.ColumnName);
                    ret.MaxDate = await query.MaxAsync<DateTime>(_option.ColumnName);
                    ret.MinValue = ret.MinDate.ToFormatString();
                    ret.MaxValue = ret.MaxDate.ToFormatString();
                    ret.NextDate = ret.MaxDate.AddHours(_option.SplitMaxRowHours);
                    ret.NextMinValue = ObjectId.TimestampId(ret.NextDate);
                    break;
                case 1: // ObjectId
                    query = query.Where($"LENGTH(`{_option.ColumnName}`)=24");
                    ret.MinValue = await query.MinAsync<string>(_option.ColumnName);
                    ret.MaxValue = await query.MaxAsync<string>(_option.ColumnName);
                    ret.MinDate = ObjectId.ParseTimestamp(ret.MinValue);
                    ret.MaxDate = ObjectId.ParseTimestamp(ret.MaxValue);
                    ret.NextDate = ret.MaxDate.AddHours(_option.SplitMaxRowHours);
                    ret.NextMinValue = ret.NextDate.ToFormatString();
                    break;
            }
            ret.SplitTableName = $"{_option.TableName}_{ret.NextDate.ToString("yyyyMMddHHmmss")}";
            return ret;
        }
    }
    class SplitMaxRowTableData
    {
        public string ColumnName { get; set; }
        public int ColumnType { get; set; }
        public string MinValue { get; set; }
        public DateTime MinDate { get; set; }
        public string MaxValue { get; set; }
        public DateTime MaxDate { get; set; }
        public DateTime NextDate { get; set; }
        public string NextMinValue { get; set; }
        public string SplitTableName { get; set; }
    }
}
