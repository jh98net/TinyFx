using Org.BouncyCastle.Ocsp;
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
        protected ColumnValueHelper _columnHelper;
        public BaseDataMoveJob(Ss_split_tableEO option, DateTime execTime)
        {
            _logger = new LogBuilder("DataMove")
                .AddField("DataMove.Option", option);
            _option = option;
            _execTime = execTime;
            if (option.DbTimeout > 0)
                DB_TIMEOUT_SECONDS = option.DbTimeout;
            if (option.BathPageSize > 0)
                BATCH_PAGE_SIZE = option.BathPageSize;

            _database = GetDb();
            _columnHelper = new ColumnValueHelper(option);
        }
        protected ISqlSugarClient GetDb(DbTransactionManager tm = null)
        {
            var ret = tm == null ? DbUtil.GetDb(_option.DatabaseId) : tm.GetDbById(_option.DatabaseId);
            ret.Ado.CommandTimeOut = DB_TIMEOUT_SECONDS;
            return ret;
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
                MoveMode = _option.MoveMode,
                MoveKeepValue = _option.MoveKeepValue,
                MoveWhere = _option.MoveWhere,
                SplitMaxRowCount = _option.SplitMaxRowCount,
                SplitMaxRowHours = _option.SplitMaxRowHours,
                HandleOrder = _option.HandleOrder,
                DbTimeout = DB_TIMEOUT_SECONDS,
                BathPageSize = BATCH_PAGE_SIZE,
                ExecTime = _execTime,
                Status = 0, //状态 0-运行中1-成功2-失败
                RecDate = oid.UtcDate, //当天仅运行一条
                HandleLog = string.Empty,
                Exception = string.Empty,
                HandleTables = string.Empty,
            };
            await DbUtil.InsertAsync(_logEo);
        }

        protected async Task CreateTable(string backTableName, ISqlSugarClient db = null)
        {
            db ??= _database;
            if (db.DbMaintenance.IsAnyTable(backTableName))
                return;
            var createSql = $"CREATE TABLE if not exists `{backTableName}` like `{_option.TableName}`";
            AddHandleLog($"==> 创建备份表SQL: {createSql}");
            await db.Ado.ExecuteCommandAsync(createSql);
        }

        protected void AddHandleLog(string msg)
        {
            LogUtil.Debug(msg);
            _logEo.HandleLog += msg + Environment.NewLine;
        }

        protected async Task<ColumnValue> GetBeginValue(ColumnValue endValue)
        {
            var sql = $"SELECT MIN(`{_option.ColumnName}`) FROM `{_option.TableName}` WHERE {_columnHelper.GetColumnWhere(null, endValue.Value)}";
            var value = await _database.Ado.GetScalarAsync(sql);
            return _columnHelper.ParseColumnValue(value);
        }
    }
}
