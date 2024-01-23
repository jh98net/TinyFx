using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;


namespace TinyFx.BIZ.DataSplit.DataMove
{
    internal class BackupJob : BaseDataMove
    {
        private int LIMIT_ROWS = 500000;
        public BackupJob(Ss_split_tableEO option) : base(option)
        {
            if ((HandleMode)option.HandleMode != HandleMode.Move)
                throw new Exception("DataMove.DeleteJob时HandleMode必须是Move");
            LIMIT_ROWS = option.BathPageSize > 0 ? option.BathPageSize : 500000;
        }
        protected override Task ExecuteJob()
        {
            throw new NotImplementedException();
        }
    }
}
