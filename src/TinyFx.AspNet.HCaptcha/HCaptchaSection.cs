using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;

namespace TinyFx.AspNet.HCaptcha
{
    public class HCaptchaSection : ConfigSection
    {
        public override string SectionName => "HCaptcha";
        /// <summary>
        /// hCaptcha Site Key
        /// </summary>
        public string SiteKey { get; set; } = "";

        /// <summary>
        /// hCaptcha Site Secret
        /// </summary>
        public string Secret { get; set; } = "";

        /// <summary>
        /// 用于获取token的 HTTP Post Form Key
        /// </summary>
        public string HttpPostResponseKeyName { get; set; } = "h-captcha-response";

        /// <summary>
        /// 是否验证客户端IP
        /// </summary>
        public bool VerifyRemoteIp { get; set; } = true;

        /// <summary>
        ///  hCaptchy JavaScript 的完整 URL
        /// </summary>
        public string JavaScriptUrl { get; set; } = "https://hcaptcha.com/1/api.js";

        /// <summary>
        /// hCaptcha 基本 URL
        /// </summary>
        public string ApiBaseUrl { get; set; } = "https://hcaptcha.com/";
    }
}
