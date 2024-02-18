using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Data.SqlSugar;
using TinyFx.Logging;

namespace TinyFx.BIZ.DataSplit.JOB.Partition
{
    internal class TablePartitionService
    {
        private Ss_split_partitionEO _item;
        private ILogBuilder _logger;
        public int DB_TIMEOUT_SECONDS = 1800; //
        public TablePartitionService(Ss_split_partitionEO item)
        {
            _item = item;
            _logger = new LogBuilder("TablePartition")
                .AddField("TablePartition.Item", item);
            if (item.DbTimeout > 0)
                DB_TIMEOUT_SECONDS = item.DbTimeout;
        }
        private ISqlSugarClient GetItemDb(DbTransactionManager tm = null)
        {
            var ret = tm == null ? DbUtil.GetDbById(_item.DatabaseId) : tm.GetDbById(_item.DatabaseId);
            ret.Ado.CommandTimeOut = DB_TIMEOUT_SECONDS;
            return ret;
        }
    }
}
