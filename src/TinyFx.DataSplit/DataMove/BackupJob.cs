using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.DataSplit.Common;
using TinyFx.DataSplit.DAL;

namespace TinyFx.DataSplit.DataMove
{
    internal class BackupJob : BaseDataMove
    {
        private int LIMIT_ROWS = 500000;
        public BackupJob(Ss_split_tableEO option) : base(option)
        {
            LIMIT_ROWS = option.BathPageSize > 0 ? option.BathPageSize : 500000;
        }
        protected override Task ExecuteJob()
        {
            throw new NotImplementedException();
        }
    }
}
