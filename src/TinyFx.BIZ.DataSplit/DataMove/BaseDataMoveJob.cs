using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Data.SqlSugar;
using TinyFx.Logging;
using TinyFx.Text;

namespace TinyFx.BIZ.DataSplit.DataMove
{
    internal abstract class BaseDataMoveJob
    {
        public int DB_TIMEOUT_SECONDS = 1800; //
        public int BATCH_PAGE_SIZE = 1000000; //100万

        protected ILogBuilder _logger;
        protected Ss_split_tableEO _option;
        protected DateTime _execTime;
        protected Ss_split_table_logEO _logEo;
        protected ISqlSugarClient _database;
        public BaseDataMoveJob(Ss_split_tableEO option, DateTime execTime)
        {
            _logger = new LogBuilder("DataMove")
                .AddField("DataMove.Option", option);
            _option = option;
            _execTime = execTime;
            _database = DbUtil.GetDb(option.DatabaseId);

            if (option.DbTimeout > 0)
                DB_TIMEOUT_SECONDS = option.DbTimeout;
            if (option.BathPageSize > 0)
                BATCH_PAGE_SIZE = option.BathPageSize;
            _database.Ado.CommandTimeOut = DB_TIMEOUT_SECONDS;
        }

        protected abstract Task ExecuteJob();

        public async Task Execute()
        {
            // 有正在执行的任务就退出
            var oldLogEo = DbUtil.GetQueryable<Ss_split_table_logEO>()
                .Where(it => it.DatabaseId == _option.DatabaseId && it.TableName == _option.TableName && it.Status == 0)
                .Where("DATE_FORMAT(recdate,'%Y-%m-%d')=DATE_FORMAT(UTC_DATE(),'%Y-%m-%d')")
                .ToList();
            if (oldLogEo?.Count > 0)
            {
                LogUtil.Debug($"DataMove时当天有正在执行的任务。databaseId:{_option.DatabaseId} tableName:{_option.TableName}");
                //return;
            }

            await InsertLogEo();
            var sw = new Stopwatch();
            sw.Start();
            var logRo = DbUtil.GetRepository<Ss_split_table_logEO>();
            try
            {
                if (!_database.DbMaintenance.IsAnyTable(_option.TableName))
                    throw new Exception($"DataMove数据表不存在。databaseId:{_option.DatabaseId} tableName:{_option.TableName}");
                await ExecuteJob();
                sw.Stop();

                _logEo.Status = 1;
                _logEo.HandleSeconds = (int)sw.Elapsed.TotalSeconds;
                if (_logEo.RowNum == 0)
                    await logRo.DeleteByIdAsync(_logEo.LogID);
                else
                    await logRo.UpdateAsync(_logEo);
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.AddMessage("执行数据备份迁移出现异常")
                    .AddField("DataMove.ElaspedTime", sw.ElapsedMilliseconds)
                    .AddField("DataMove.LogEo", _logEo)
                    .AddException(ex);

                _logEo.Status = 2;
                _logEo.HandleSeconds = (int)sw.Elapsed.TotalSeconds;
                _logEo.Exception += SerializerUtil.SerializeJsonNet(ex);
                await logRo.UpdateAsync(_logEo);
            }
            _logger.Save();
        }
        private async Task InsertLogEo()
        {
            var oid = ObjectId.NextId();
            _logEo = new Ss_split_table_logEO()
            {
                LogID = oid.Id,
                DatabaseId = _option.DatabaseId,
                TableName = _option.TableName,
                ColumnName = _option.ColumnName,
                ColumnType = _option.ColumnType,
                HandleMode = _option.HandleMode,
                MoveKeepMode = _option.MoveKeepMode,
                MoveKeepValue = _option.MoveKeepValue,
                MoveTableMode = _option.MoveTableMode,
                MoveTableValue = _option.MoveTableValue,
                MoveWhere = _option.MoveWhere,
                SplitMaxRowCount = _option.SplitMaxRowCount,
                HandleOrder = _option.HandleOrder,
                DbTimeout = DB_TIMEOUT_SECONDS,
                BathPageSize = BATCH_PAGE_SIZE,
                ExecTime = _execTime,
                Status = 0, //状态 0-运行中1-成功2-失败
                RecDate = oid.UtcDate, //当天仅运行一条
                HandleLog = string.Empty
            };
            await DbUtil.InsertAsync(_logEo);
        }

        protected void AddHandleLog(string msg)
        {
            LogUtil.Debug(msg);
            _logEo.HandleLog += msg + Environment.NewLine;
        }
        protected DateTime GetKeepEndDate()
        {
            switch ((MoveKeepMode)_option.MoveKeepMode)
            {
                case MoveKeepMode.Day:
                    return _execTime.AddDays(-_option.MoveKeepValue).Date;
                case MoveKeepMode.Week:
                    var weekDate = _execTime.AddDays(-_option.MoveKeepValue * 7).Date;
                    return TinyFxUtil.BeginDayOfWeek(weekDate);
                case MoveKeepMode.Month:
                    var monthDate = _execTime.AddMonths(-_option.MoveKeepValue).Date;
                    return new DateTime(monthDate.Year, monthDate.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                case MoveKeepMode.Quarter:
                    var quarterDate = _execTime.AddMonths(-_option.MoveKeepValue * 3);
                    var quarterMonth = Math.DivRem(quarterDate.Month - 1, 3, out int _) * 3 + 1;
                    return new DateTime(quarterDate.Year, quarterMonth, 1, 0, 0, 0, DateTimeKind.Utc);
                case MoveKeepMode.Year:
                    return new DateTime(_execTime.Year - _option.MoveKeepValue, 1, 1);
                default:
                    throw new Exception("未知的MoveKeepMode");
            }
        }
        protected async Task<DateTime?> GetTableMinDate(DateTime endDate)
        {
            var sql = string.Empty;
            switch (_option.ColumnType)
            {
                case 0: // DateTime
                    sql = $"SELECT MIN(`{_option.ColumnName}`) FROM `{_option.TableName}` WHERE `{_option.ColumnName}` < '{endDate.ToString("yyyy-MM-dd")}'";
                    break;
                case 1: // ObjectId
                    // select FROM_UNIXTIME(CAST(CONV(SUBSTR(UserID, 1, 8), 16, 10) AS UNSIGNED)) from s_user
                    sql = $"SELECT MIN(`{_option.ColumnName}`) FROM `{_option.TableName}` WHERE `{_option.ColumnName}` < '{ObjectId.TimestampId(endDate)}'";
                    break;
            }
            DateTime? ret = null;
            var begin = await _database.Ado.GetScalarAsync(sql);
            if (begin != null && begin is not DBNull)
            {
                switch (_option.ColumnType)
                {
                    case 0:
                        var dt = begin.ConvertTo<DateTime>();
                        ret = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, DateTimeKind.Utc);
                        break;
                    case 1:
                        var dt1 = ObjectId.ParseTimestamp(begin.ToString());
                        ret = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0, DateTimeKind.Utc);
                        break;
                }
            }
            return ret;
        }
        protected List<DateTime> GetDayList(DateTime beginDate, DateTime endDate)
        {
            var ret = new List<DateTime>();
            var currDate = beginDate;
            while (currDate < endDate)
            {
                ret.Add(currDate);
                currDate = currDate.AddDays(1);
            }
            return ret;
        }
        protected WhereByDayData GetWhereByDay(DateTime currDate)
        {
            var ret = new WhereByDayData();
            switch (_option.ColumnType)
            {
                case 0: // DateTime
                    ret.Begin = currDate.ToString("yyyy-MM-dd");
                    ret.End = currDate.AddDays(1).ToString("yyyy-MM-dd");
                    ret.Content = $"`{_option.ColumnName}`>='{ret.Begin}' AND `{_option.ColumnName}`<'{ret.End}'";
                    break;
                case 1: // ObjectId
                    ret.Begin = ObjectId.TimestampId(currDate);
                    ret.End = ObjectId.TimestampId(currDate.AddDays(1));
                    ret.Content = $"`{_option.ColumnName}`>='{ret.Begin}' AND `{_option.ColumnName}`<'{ret.End}'";
                    break;
            }
            if (!string.IsNullOrEmpty(_option.MoveWhere))
            {
                var where = _option.MoveWhere.ToUpper().Trim().TrimStart("AND ");
                ret.Content = $"{ret.Content} AND {where}";
            }
            return ret;
        }
    }
    internal class WhereByDayData
    {
        public string Content { get; set; }
        public string Begin { get; set; }
        public string End { get; set; }
    }
}
