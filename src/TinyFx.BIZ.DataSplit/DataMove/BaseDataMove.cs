using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Data.SqlSugar;
using TinyFx.Logging;
using TinyFx.Text;

namespace TinyFx.BIZ.DataSplit.DataMove
{
    internal abstract class BaseDataMove
    {
        public const int DB_TIMEOUT_SECONDS = 1800;
        protected ILogBuilder _logger;
        protected Ss_split_tableEO _option;
        protected Ss_split_table_logEO _logEo;
        protected ISqlSugarClient _database;
        public BaseDataMove(Ss_split_tableEO option)
        {
            _logger = new LogBuilder("DataMove")
                .AddField("DataMove.Option", option);
            _option = option;
            _database = DbUtil.GetDb(option.DatabaseId);
            _database.Ado.CommandTimeOut = _option.DbTimeout > 0
                ? _option.DbTimeout : DB_TIMEOUT_SECONDS;
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
                return;
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
                _logEo.HandleTime = (int)sw.Elapsed.TotalSeconds;
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
                _logEo.HandleTime = (int)sw.Elapsed.TotalSeconds;
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
                DbTimeout = _option.DbTimeout,
                BathPageSize = _option.BathPageSize,
                Status = 0, //状态 0-运行中1-成功2-失败
                RecDate = oid.UtcDate, //当天仅运行一条
                HandleLog = string.Empty
            };
            await DbUtil.InsertAsync(_logEo);
        }

        protected void AddHandlerLog(string msg)
        {
            LogUtil.Debug(msg);
            _logEo.HandleLog += msg + Environment.NewLine;
        }
        protected DateTime GetKeepEndDate()
        {
            var now = DateTime.UtcNow;
            return _option.MoveKeepMode == 0
                ? now.AddDays(-_option.MoveKeepValue).Date
                : now.AddMonths(-_option.MoveKeepValue).Date;
        }
        protected async Task<DateTime> GetTableMinDate(DateTime endDate)
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
            var begin = await _database.Ado.GetScalarAsync(sql);
            if (begin is DBNull || begin == null)
            {
                return DateTime.MaxValue;
            }
            else
            {
                var ret = begin.ConvertTo<DateTime>();
                return new DateTime(ret.Year, ret.Month, ret.Day);
            }
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
        protected string GetWhereByDay(DateTime currDate)
        {
            var ret = string.Empty;
            switch (_option.ColumnType)
            {
                case 0: // DateTime
                    ret = $"`{_option.ColumnName}`>='{currDate.ToString("yyyy-MM-dd")}' AND `{_option.ColumnName}`<'{currDate.AddDays(1).ToString("yyyy-MM-dd")}'";
                    break;
                case 1: // ObjectId
                    ret = $"`{_option.ColumnName}`>='{ObjectId.TimestampId(currDate)}' AND `{_option.ColumnName}`<'{ObjectId.TimestampId(currDate.AddDays(1))}'";
                    break;
            }
            if (!string.IsNullOrEmpty(_option.MoveWhere))
            {
                var where = _option.MoveWhere.ToUpper().Trim().TrimStart("AND ");
                ret = $"{ret} AND {where}";
            }
            return ret;
        }
    }
}
