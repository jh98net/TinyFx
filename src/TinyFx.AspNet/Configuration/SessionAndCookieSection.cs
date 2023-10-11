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
        /// 默认ProjectId，如需跨应用共享session或cookie，需设置相同值
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// redis连接字符串名，对应配置：Redis:ConnectionStrings:Name
        /// </summary>
        public string RedisConnectionStringName { get; set; }

        /// <summary>
        /// 是否启用Cookie Identity
        /// </summary>
        public bool UseCookieIdentity { get; set; } = true;
        /// <summary>
        /// cookie过期时间天
        /// </summary>
        public int CookieTimeout { get; set; } = 3;
        /// <summary>
        /// cookie保存的domain，跨域如: .xxyy.com
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// https使用None，其他Unspecified
        /// </summary>
        public SameSiteMode SameSiteMode { get; set; } = SameSiteMode.Unspecified;

        /// <summary>
        /// 是否使用session
        /// </summary>
        public bool UseSession { get; set; } = false;
        /// <summary>
        /// session过期(默认20分钟)
        /// </summary>
        public int SessionTimeout { get; set; } = 20;
    }
}
