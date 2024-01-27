using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Text;


namespace TinyFx.BIZ.DataSplit.DataMove
{
    internal class BackupJob : BaseDataMoveJob
    {
        public BackupJob(Ss_split_tableEO option, DateTime execTime) : base(option, execTime)
        {
            if ((HandleMode)option.HandleMode != HandleMode.Backup)
                throw new Exception("DataMove.BackupJob时HandleMode必须是Backup");
        }

        protected override async Task ExecuteJob()
        {
            var list = await GetBackupDataList();
            if (list == null || list.Count == 0)
                return;
            _logEo.BeginDate = list.First().BeginDate;
            _logEo.EndDate = list.Last().EndDate;
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                AddHandleLog($"[任务{i + 1}]-开始备份: [{_option.TableName} => {item.BackupTableName}] [{item.BeginDate.ToString("yyyy-MM-dd")} => {item.EndDate.ToString("yyyy-MM-dd")}]");
                _logEo.HandleTables += $"{item.BackupTableName}{Environment.NewLine}";
                await CreateTable(item.BackupTableName);

                var dateList = GetDayList(item.BeginDate, item.EndDate);
                foreach (var currDate in dateList)
                {
                    await BackupDayRows(item.BackupTableName, currDate);
                    //await Task.Delay(100);
                }
                AddHandleLog($"[任务{i + 1}]-完成备份: [{_option.TableName} => {item.BackupTableName}] [{item.BeginDate} => {item.EndDate}] count: {_logEo.RowNum} {Environment.NewLine}");
            }
        }

        protected async Task<int> BackupDayRows(string backTableName, DateTime currDate)
        {
            var where = GetWhereByDay(currDate);
            var watch = new Stopwatch();

            // 1) 获取备份的数据
            // SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
            var selectSql = $"SELECT * FROM `{_option.TableName}`";
            var selectCount = await _database.SqlQueryable<object>(selectSql).Where(where.Content).CountAsync();
            if (selectCount == 0)
                return 0;
            // page
            var dtList = new List<DataTable>();
            int pageCount = (selectCount + BATCH_PAGE_SIZE - 1) / BATCH_PAGE_SIZE;
            AddHandleLog($"==> [{currDate.ToString("yyyy-MM-dd")}]备份天数据开始: {_option.TableName} rowCount:{selectCount} pageSize:{BATCH_PAGE_SIZE} pageCount:{pageCount}");
            AddHandleLog($"  1) 读取备份数据开始...");
            var readCount = 0;
            for (int idx = 1; idx <= pageCount; idx++)
            {
                watch.Restart();
                var dt = _database.SqlQueryable<object>(selectSql).Where(where.Content).ToDataTablePage(idx, BATCH_PAGE_SIZE);
                if (dt == null || dt.Rows.Count == 0)
                    break;
                readCount += dt.Rows.Count;
                dtList.Add(dt);
                watch.Stop();
                AddHandleLog($"  SQL:[{(int)watch.Elapsed.TotalSeconds}秒] {selectSql} pageSize:{BATCH_PAGE_SIZE} pageCount:{dtList.Count}");
                //await Task.Delay(100);
            }
            if (readCount != selectCount)
                throw new Exception($"分页读取时行记录不一致: {selectSql} selectCount:{selectCount} readCount:{readCount}");
            AddHandleLog($"  1) 读取备份数据结束... readCount:{readCount}");

            try
            {
                AddHandleLog($"  ======== 事务开始 ========");
                _database.Ado.BeginTran();

                // 2) 删除备份表旧数据
                AddHandleLog($"  2) 删除备份表旧数据开始: {backTableName}");
                var oldDeleteSql = $"DELETE FROM `{backTableName}` WHERE {where.Content}";
                watch.Restart();
                var oldDeleteCount = await _database.Ado.ExecuteCommandAsync(oldDeleteSql);
                watch.Stop();
                AddHandleLog($"  SQL:[{(int)watch.Elapsed.TotalSeconds}秒] {oldDeleteSql}");
                AddHandleLog($"  2) 删除备份表旧数据结束: {backTableName} deleteCount: {oldDeleteCount}");

                // 3) 插入数据
                AddHandleLog($"  3) 插入备份表数据开始: {backTableName}");
                var insertCount = 0;
                foreach (var dt in dtList)
                {
                    watch.Restart();
                    insertCount += await _database.Fastest<DataTable>().AS(backTableName).BulkCopyAsync(dt);
                    watch.Stop();
                    AddHandleLog($"  SQL:[{(int)watch.Elapsed.TotalSeconds}秒] INSERT INTO `{backTableName}` total: {insertCount}");
                    //await Task.Delay(100);
                }
                if (selectCount != insertCount)
                    throw new Exception($"DataMove插入时总记录数不相同。fromTable: {_option.TableName} toTable:{backTableName}");
                AddHandleLog($"  3) 插入备份表数据结束: {backTableName} insertCount: {insertCount}");

                // 4) 删除原始表数据
                AddHandleLog($"  4) 删除原始表数据开始: {_option.TableName}");
                var deleteSql = $"DELETE FROM `{_option.TableName}` WHERE {where.Content}";
                var deleteCount = 0;
                while (true)
                {
                    watch.Restart();
                    var rows = await _database.Ado.ExecuteCommandAsync($"{deleteSql} LIMIT {BATCH_PAGE_SIZE * 5}");
                    watch.Stop();
                    AddHandleLog($"  SQL:[{(int)watch.Elapsed.TotalSeconds}秒] {deleteSql} LIMIT {BATCH_PAGE_SIZE * 5}");

                    if (rows == 0) break;
                    deleteCount += rows;
                    //await Task.Delay(100);
                }
                if (deleteCount != selectCount)
                    throw new Exception($"DataMove删除记录数不等于插入记录数。{_option.TableName} => {backTableName} currDate: {currDate} insertCount: {selectCount} deleteCount: {deleteCount}");
                AddHandleLog($"  4) 删除原始表数据结束: {_option.TableName} deleteCount: {deleteCount}");
                _logEo.RowNum += deleteCount;

                // 5) 保存detail记录
                var oid = ObjectId.NextId();
                var detailEo = new Ss_split_table_detailEO
                {
                    DetailID = oid.Id,
                    LogID = _logEo.LogID,
                    DatabaseId = _option.DatabaseId,
                    TableName = _option.TableName,
                    ColumnName = _option.ColumnName,
                    ColumnType = _option.ColumnType,
                    HandleMode = _option.HandleMode,
                    SplitTableName = backTableName,
                    ColumnMin = where.Begin,
                    ColumnMax = where.End,
                    RowCount = selectCount,
                    Status = 1,
                    RecDate = oid.UtcDate,
                };
                await _database.Insertable(detailEo).ExecuteCommandAsync();

                // 提交事务
                _database.Ado.CommitTran();
                AddHandleLog($"  ======== 事务提交 ========");
            }
            catch
            {
                _database.Ado.RollbackTran();
                AddHandleLog($"  ==>事务失败: {_option.TableName} => {backTableName} currDate: {currDate} count: {selectCount}");
                throw;
            }
            AddHandleLog($"==> [{currDate.ToString("yyyy-MM-dd")}]备份天数据结束: rowCount:{selectCount} pageSize:{BATCH_PAGE_SIZE} pageCount:{pageCount}");
            return selectCount;
        }
        private async Task CreateTable(string backTableName)
        {
            if (_database.DbMaintenance.IsAnyTable(backTableName))
                return;
            var createSql = $"CREATE TABLE if not exists `{backTableName}` like `{_option.TableName}`";
            AddHandleLog($"==> 创建备份表SQL: {createSql}");
            await _database.Ado.ExecuteCommandAsync(createSql);
        }

        #region GetBackupDataList
        private async Task<List<BackupData>> GetBackupDataList()
        {
            List<BackupData> list;
            switch ((MoveTableMode)_option.MoveTableMode)
            {
                case MoveTableMode.Day:
                    list = await BuildBackupDataList((endDate) =>
                    {
                        return endDate.AddDays(-1);
                    });
                    break;
                case MoveTableMode.Week:
                    list = await BuildBackupDataList((endDate) =>
                    {
                        var date = endDate.AddDays(-1);
                        return TinyFxUtil.BeginDayOfWeek(date);
                    });
                    break;
                case MoveTableMode.Month:
                    list = await BuildBackupDataList((endDate) =>
                    {
                        var date = endDate.AddDays(-1);
                        return new DateTime(date.Year, date.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                    });
                    break;
                case MoveTableMode.Quarter:
                    list = await BuildBackupDataList((endDate) =>
                    {
                        var date = endDate.AddDays(-1);
                        var quarterMonth = Math.DivRem(date.Month - 1, 3, out int _) * 3 + 1;
                        return new DateTime(date.Year, quarterMonth, 1, 0, 0, 0, DateTimeKind.Utc);
                    });
                    break;
                case MoveTableMode.Year:
                    list = await BuildBackupDataList((endDate) =>
                    {
                        var date = endDate.AddDays(-1);
                        return new DateTime(date.Year, 1, 1);
                    });
                    break;
                case MoveTableMode.Custom:
                    list = await BuildCustomBackupDataList();
                    break;
                default:
                    throw new Exception($"DataMove不支持此HandleMode: {_option.HandleMode}");
            }
            return list;
        }
        private async Task<List<BackupData>> BuildBackupDataList(Func<DateTime, DateTime> calcBeginDate)
        {
            var ret = new List<BackupData>();
            var endDate = GetKeepEndDate();
            var minDate = await GetTableMinDate(endDate);
            if (!minDate.HasValue)
                return ret;
            while (endDate > minDate.Value)
            {
                var beginDate = calcBeginDate(endDate);
                var item = new BackupData
                {
                    BackupTableName = $"{_option.TableName}_{beginDate:yyyyMMddHHmmss}",
                    BeginDate = beginDate < minDate.Value ? minDate.Value : beginDate,
                    EndDate = endDate
                };
                ret.Add(item);
                endDate = beginDate;
            }
            ret.Reverse();
            return ret;
        }
        private async Task<List<BackupData>> BuildCustomBackupDataList()
        {
            if (string.IsNullOrEmpty(_option.MoveTableValue))
                throw new Exception("[数据迁移-备份]MoveTableMode=5指定名称时MoveTableValue不能为空");
            var ret = new List<BackupData>();
            var endDate = GetKeepEndDate();
            var minDate = await GetTableMinDate(endDate);
            if (!minDate.HasValue)
                return ret;
            ret.Add(new BackupData
            {
                BackupTableName = _option.MoveTableValue!,
                BeginDate = minDate.Value,
                EndDate = endDate
            });
            return ret;
        }
        #endregion
    }
    internal class BackupData
    {
        public string BackupTableName { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
