using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.DbCaching
{
    public class DbCacheCheckResult
    {
        public string ServiceId { get; set; }
        public bool Success { get; set; }
        public List<DbCacheCheckItem> Items { get; set; }
        public string ProjectId => Parse().id;
        public string ServiceHash => Parse().hash;
        private (string id, string hash) Parse()
        {
            string id = null;
            string hash = null;
            if (!string.IsNullOrEmpty(ServiceId) && ServiceId.Contains(':'))
            {
                var keys = ServiceId.Split(':');
                id = keys[0];
                hash = keys[1];
            }
            return (id, hash);
        }
    }
    public class DbCacheCheckItem
    {
        public string ConfigId { get; set; }
        public string TableName { get; set; }
        public string RedisHash { get; set; }
        public string CacheHash { get; set; }
        public string RedisUpdate { get; set; }
        public string CacheUpdate { get; set; }
    }

}
