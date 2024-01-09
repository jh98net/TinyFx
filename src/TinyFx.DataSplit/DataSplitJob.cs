using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Data.SqlSugar;
using TinyFx.DataSplit.Common;
using TinyFx.DataSplit.DAL;
using TinyFx.DataSplit.DataMove;

namespace TinyFx.DataSplit
{
    /// <summary>
    /// 数据分库分表定时任务
    /// </summary>
    public class DataSplitJob
    {
        public async Task Execute()
        {
            var list = await DbUtil.GetRepository<Ss_split_tableEO>().AsQueryable()
                .Where(it => it.Status == 1).OrderBy(it => it.HandleOrder).ToListAsync();
            foreach (var item in list)
            {
                var mode = item.HandleMode.ToEnum(HandleMode.Unknow);
                switch(mode)
                {
                    case HandleMode.Delete:
                        await new DeleteJob(item).Execute();
                        break;
                    case HandleMode.Move:
                        break;
                }
            }
        }
    }
}
