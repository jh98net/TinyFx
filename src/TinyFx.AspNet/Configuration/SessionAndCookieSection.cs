using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration
{
    /// <summary>
    /// 启用Session或者Cookie Identity
    /// </summary>
    public class SessionAndCookieSection : ConfigSection
    {
        public override string SectionName => "SessionAndCookie";
        /// <summary>
        /// 是否使用session
        /// </summary>
        public bool UseSession { get; set; } = false;
        /// <summary>
        /// 是否启用Cookie Identity
        /// </summary>
        public bool UseCookieIdentity { get; set; } = true;
        /// <summary>
        /// 默认ProjectId，如需跨应用共享session或cookie，需设置相同值
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// cookie保存的domain，跨域如: .xxyy.com
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// redis连接字符串名，对应配置：Redis:ConnectionStrings:Name
        /// </summary>
        public string ConnectionStringName { get; set; }
        /// <summary>
        /// Session和cookie过期时间,单位分钟。0:不过期
        /// </summary>
        public int IdleTimeout { get; set; } = 0;
        /// <summary>
        /// 默认Lax:通过浏览器发送同站请求或跨站的部分GET请求时，可以携带Cookie
        /// Strict:只有通过浏览器发送同站请求时，才会携带Cookie
        /// </summary>
        public SameSiteMode SameSiteMode { get; set; } = SameSiteMode.Lax;
    }
}
