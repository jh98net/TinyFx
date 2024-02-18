using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Data.SqlSugar;
using TinyFx.Text;

namespace TinyFx.BIZ.DataSplit
{
    public class DemoHelper
    {
        public async Task InitData(string dbId = null)
        {
            var db = DbUtil.GetDbById(dbId);
            db.DbMaintenance.TruncateTable<Ss_split_demoEO>();

            var list = new List<Ss_split_demoEO>();
            var begin = new DateTime(2023, 12, 29, 0, 0, 0, DateTimeKind.Utc);
            var end = DateTime.UtcNow;
            var idx = 1;
            var curr = begin;
            while (curr <= end)
            {
                var eo = new Ss_split_demoEO
                {
                    ObjectID = ObjectId.NewId(curr),
                    NumDay = curr.ToString("yyyyMMdd").ToInt32(),
                    NumWeek = DateTimeUtil.ToYearWeek(curr),
                    NumMonth = curr.ToString("yyyyMM").ToInt32(),
                    NumQuarter = $"{curr.Year}{(curr.Month + 2) / 3}".ToInt32(),
                    NumYear = curr.Year,
                    OrderNum = idx,
                    RecDate = curr,
                };
                list.Add(eo);
                idx++;

                var curr1 = curr.AddHours(new Random().Next(23));
                var eo1 = new Ss_split_demoEO
                {
                    ObjectID = ObjectId.NewId(curr1),
                    NumDay = curr.ToString("yyyyMMdd").ToInt32(),
                    NumWeek = DateTimeUtil.ToYearWeek(curr),
                    NumMonth = curr.ToString("yyyyMM").ToInt32(),
                    NumQuarter = $"{curr.Year}{(curr.Month + 2) / 3}".ToInt32(),
                    NumYear = curr.Year,
                    OrderNum = idx,
                    RecDate = curr1,
                };
                list.Add(eo1);
                idx++;
                //
                curr = curr.AddDays(1);
            }
            await db.Fastest<Ss_split_demoEO>().BulkCopyAsync(list);
            await db.Deleteable<Ss_split_table_logEO>().ExecuteCommandAsync();
            await db.Deleteable<Ss_split_table_detailEO>().ExecuteCommandAsync();
        }
    }
}
