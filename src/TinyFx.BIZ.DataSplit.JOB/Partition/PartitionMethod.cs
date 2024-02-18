using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.BIZ.DataSplit.JOB.Partition
{
    public enum PartitionMethod
    {
        None = 0,
        Range = 1,
        List = 2,
        Hash = 3,
        Key = 4,
        RangeColumns = 5,
        ListColumns = 6,
        Custom = 7
    }
}
