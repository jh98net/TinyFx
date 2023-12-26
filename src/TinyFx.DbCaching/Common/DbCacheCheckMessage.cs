using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.DbCaching
{
    public class DbCacheCheckMessage
    {
        /// <summary>
        /// 唯一好
        /// </summary>
        public string TraceId { get; set; }
        public string RedisConnectionStringName { get; set; }
        public string CheckDate { get; set; }
    }
}
