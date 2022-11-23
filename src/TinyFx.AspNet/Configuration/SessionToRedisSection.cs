using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration
{
    /// <summary>
    /// Session保存在Redis中
    /// </summary>
    public class SessionToRedisSection : ConfigSection
    {
        public override string SectionName => "SessionToRedis";
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// redis连接字符串名，对应配置：Redis:ConnectionStrings:Name
        /// </summary>
        public string ConnectionStringName { get; set; }
        public int DatabaseIndex { get; set; } = -1;
        /// <summary>
        /// Session过期时间,单位分钟
        /// </summary>
        public int IdleTimeout { get; set; }
        /// <summary>
        /// 指示客户端脚本是否可以访问cookie
        /// </summary>
        public bool CookieHttpOnly { get; set; }
    }
}
