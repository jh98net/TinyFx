using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.BIZ.DataSplit.JOB.DataMove;
using TinyFx.Data.SqlSugar;
using TinyFx.DbCaching;


namespace TinyFx.BIZ.DataSplit
{
    /// <summary>
    /// 数据分库分表定时任务
    /// </summary>
    public class DataSplitJob
    {
        public async Task Execute(List<Stfx_split_tableEO> list = null, string defaultConfigId = null)
        {
            if (list == null || list.Count == 0)
            {
                list = await DbUtil.GetDbById(defaultConfigId).Queryable<Stfx_split_tableEO>()
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
                        await new DeleteJob(item, defaultConfigId, execTime).Execute();
                        break;
                    case HandleMode.Backup:
                        await new BackupJob(item, defaultConfigId, execTime).Execute();
                        break;
                    case HandleMode.SplitMaxRows:
                        await new SplitMaxRowsJob(item, defaultConfigId, execTime).Execute();
                        break;
                }
            }
            // 缓存
            var msg = new DbCacheChangeMessage();
            msg.Changed.Add(new DbCacheItem
            {
                ConfigId = DbUtil.DefaultConfigId,
                TableName = "s_split_table"
            });
            msg.Changed.Add(new DbCacheItem
            {
                ConfigId = DbUtil.DefaultConfigId,
                TableName = "s_split_table_detail"
            });
            await DbCachingUtil.PublishUpdate(msg);
        }
        public Task Execute(Stfx_split_tableEO item, string defaultConfigId = null)
            => Execute(new List<Stfx_split_tableEO> { item }, defaultConfigId);
    }
}
