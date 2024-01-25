using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Data.SqlSugar;
using TinyFx.Text;

namespace TinyFx.BIZ.DataSplit.Common
{
    public class DemoHelper
    {
        public async Task InitData(string dbId = null)
        {
            var db = DbUtil.GetDbById(dbId);
            db.DbMaintenance.TruncateTable<Ss_split_demoEO>();

            var list = new List<Ss_split_demoEO>();
            var begin = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var end = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var idx = 1;
            var curr = begin;
            while (curr <= end)
            {
                var eo = new Ss_split_demoEO
                {
                    ObjectID = ObjectId.NewId(curr),
                    ObjectIdDate = curr,
                    OrderNum = idx,
                    RecDate = curr,
                };
                list.Add(eo);

                var curr1 = curr.AddHours(new Random().Next(23));
                var eo1 = new Ss_split_demoEO
                {
                    ObjectID = ObjectId.NewId(curr1),
                    ObjectIdDate = curr1,
                    OrderNum = idx,
                    RecDate = curr1,
                };
                list.Add(eo1);
                //
                curr = curr.AddDays(1);
                idx++;
            }
            await db.Fastest<Ss_split_demoEO>().BulkCopyAsync(list);
        }
    }
}
