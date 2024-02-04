using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.DbCaching
{
    public class DbCacheCheckResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        /// <summary>
        /// 数据库和redis缓存不一致列表
        /// </summary>
        public List<DbCacheItem> RedisAndDbDiffs { get; set; } = new();
        /// <summary>
        /// 数据库和服务内缓存不一致列表
        /// </summary>
        public List<DbCacheCheckServiceCache> CacheAndDbDiffs { get; set; } = new();
    }

    public class DbCacheCheckServiceCache
    {
        public string ServiceId { get; set; }
        public bool Success { get; set; }
        public List<DbCacheCheckServiceItem> Items { get; set; } = new();
        public string ProjectId => Parse().id;
        public string ServiceHash => Parse().hash;
        public string Error { get; set; }
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
    public class DbCacheCheckServiceItem
    {
        public string ConfigId { get; set; }
        public string TableName { get; set; }

        public string CacheHash { get; set; }
        public string CacheUpdate { get; set; }
        public string DbHash { get; set; }
    }
}
