﻿using System;
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
            db.DbMaintenance.TruncateTable<Sdemo_tfx_splitEO>();

            var list = new List<Sdemo_tfx_splitEO>();
            var begin = new DateTime(2022, 12, 29, 0, 0, 0, DateTimeKind.Utc);
            var end = DateTime.UtcNow;
            var idx = 1;
            var curr = begin;
            while (curr <= end)
            {
                var eo = new Sdemo_tfx_splitEO
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

                var curr1 = curr.AddHours(new Random().Next(23)+1);
                var eo1 = new Sdemo_tfx_splitEO
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
            await db.Fastest<Sdemo_tfx_splitEO>().BulkCopyAsync(list);
            await db.Deleteable<Stfx_split_table_logEO>().ExecuteCommandAsync();
            await db.Deleteable<Stfx_split_table_detailEO>().ExecuteCommandAsync();
        }
    }
}
