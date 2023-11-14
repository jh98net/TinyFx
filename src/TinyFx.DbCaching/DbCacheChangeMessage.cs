using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.DbCaching
{
    public class DbCacheChangeMessage
    {
        public List<DbCacheItem> Changed { get; set; }
    }
    public class DbCacheItem
    {
        public string ConfigId { get; set; }
        public string TableName { get; set; }
    }
}
