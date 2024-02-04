﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.BIZ.DataSplit.DataMove;
using TinyFx.Data.SqlSugar;


namespace TinyFx.BIZ.DataSplit
{
    /// <summary>
    /// 数据分库分表定时任务
    /// </summary>
    public class DataSplitJob
    {
        public async Task Execute(List<Ss_split_tableEO> list = null)
        {
            if (list == null || list.Count == 0)
            {
                list = await DbUtil.GetRepository<Ss_split_tableEO>().AsQueryable()
                    .Where(it => it.Status == 1).OrderBy(it => it.HandleOrder).ToListAsync();
            }
            var execTime = DateTime.UtcNow;
            // 执行
            foreach (var item in list)
            {
                var mode = item.HandleMode.ToEnum(HandleMode.None);
                switch (mode)
                {
                    case HandleMode.Delete:
                        await new DeleteJob(item, execTime).Execute();
                        break;
                    case HandleMode.Backup:
                        await new BackupJob(item, execTime).Execute();
                        break;
                    case HandleMode.SplitMaxRows:
                        await new SplitMaxRowsJob(item, execTime).Execute();
                        break;
                }
            }
            // 缓存
        }
        public Task Execute(Ss_split_tableEO item)
            => Execute(new List<Ss_split_tableEO> { item });
    }
}
