using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;

namespace TinyFx.BIZ.DataSplit.DataMove
{
    internal class DeleteJob : BaseDataMoveJob
    {
        public DeleteJob(Ss_split_tableEO option, DateTime execTime) : base(option, execTime)
        {
            if ((HandleMode)option.HandleMode != HandleMode.Delete)
                throw new Exception($"{GetType().FullName}时HandleMode必须是Delete");
            if (option.BathPageSize == 0)
                BATCH_PAGE_SIZE = 0;
        }
        protected override async Task ExecuteJob()
        {
            //实际操作数据：beginDate <= target < endDate
            var endDate = GetKeepEndDate();
            var beginDate = await GetTableMinDate(endDate);
            if (!beginDate.HasValue)
                return;
            AddHandleLog($"==>开始删除{_option.TableName} {beginDate} => {endDate}");

            _logEo.BeginDate = beginDate;
            _logEo.EndDate = endDate;
            var dateList = GetDayList(beginDate.Value, endDate);
            foreach (var currDate in dateList)
            {
                var sql = $"DELETE FROM `{_option.TableName}` WHERE {GetWhereByDay(currDate).Content}";
                if (BATCH_PAGE_SIZE > 0)
                    sql += $" LIMIT {BATCH_PAGE_SIZE}";
                AddHandleLog($"SQL: {sql}");
                while (true)
                {
                    //var rows = 0;
                    var rows = await _database.Ado.ExecuteCommandAsync(sql);
                    if (rows == 0) break;
                    _logEo.RowNum += rows;
                    await Task.Delay(200);
                }
            }
            AddHandleLog($"==>完成删除{_option.TableName} {beginDate} => {endDate} count: {_logEo.RowNum}");
        }
    }
}
