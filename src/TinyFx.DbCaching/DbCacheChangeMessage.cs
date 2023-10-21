using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.DbCaching
{
    public class DbCacheChangeMessage
    {
        public List<DbCacheChangeItem> Changed { get; set; }
    }
    public class DbCacheChangeItem
    {
        public string ConfigId { get; set; }
        public string TableName { get; set; }
        public long RowCount { get; set; }
    }
}
