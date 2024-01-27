using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;

namespace TinyFx.BIZ.DataSplit.DataMove
{
    internal class SplitMaxRowsJob : BaseDataMoveJob
    {
        public SplitMaxRowsJob(Ss_split_tableEO option, DateTime execTime) : base(option, execTime)
        {
            if ((HandleMode)option.HandleMode != HandleMode.SplitMaxRows)
                throw new Exception("DataMove.SplitMaxRowsJob时HandleMode必须是SplitMaxRows");
        }

        protected override async Task ExecuteJob()
        {
        }
    }
}
