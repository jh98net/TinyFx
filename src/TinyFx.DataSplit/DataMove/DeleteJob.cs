using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.DataSplit.Common;
using TinyFx.DataSplit.DAL;

namespace TinyFx.DataSplit.DataMove
{
    internal class DeleteJob : BaseDataMove
    {
        private int LIMIT_ROWS = 1000000;
        public DeleteJob(Ss_split_tableEO option) : base(option)
        {
            if ((HandleMode)option.HandleMode != HandleMode.Delete)
                throw new Exception("DataMove.DeleteJob时HandleMode必须是Delete");
            LIMIT_ROWS = option.BathPageSize > 0 ? option.BathPageSize : 1000000;
        }
        protected override async Task ExecuteJob()
        {
            //实际操作数据：beginDate <= target < endDate
            var endDate = GetKeepEndDate();
            var beginDate = await GetTableMinDate(endDate);
            if (beginDate == DateTime.MaxValue)
                return;
            AddHandlerLog($"==>开始删除{_option.TableName} {beginDate} => {endDate}");

            _logEo.BeginDate = beginDate;
            _logEo.EndDate = endDate;
            var dateList = GetDayList(beginDate, endDate);
            foreach (var currDate in dateList)
            {
                var sql = $"DELETE FROM `{_option.TableName}` WHERE {GetWhereByDay(currDate)}";
                AddHandlerLog($"SQL: {sql}");
                while (true)
                {
                    var rows = await _database.Ado.ExecuteCommandAsync($"{sql} LIMIT {LIMIT_ROWS}");
                    if (rows == 0) break;
                    _logEo.RowNum += rows;
                    await Task.Delay(200);
                }
            }
            AddHandlerLog($"==>完成删除{_option.TableName} {beginDate} => {endDate} count: {_logEo.RowNum}");
        }
    }
}
