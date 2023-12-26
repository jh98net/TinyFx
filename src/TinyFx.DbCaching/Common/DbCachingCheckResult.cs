using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.DbCaching
{
    public class DbCachingCheckResult
    {
        public string ServiceId { get; set; }
        public List<DbCachingCheckItem> Items { get; set; }
    }
    public class DbCachingCheckItem
    {
        public string ConfigId { get; set; }
        public string TableName { get; set; }
        public string RedisHash { get; set; }
        public string CacheHash { get; set; }
        public string RedisUpdate { get; set; }
        public string CacheUpdate { get; set; }
    }

}
