using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;

namespace TinyFx.Configuration
{
    /// <summary>
    /// Api采用jwt验证的设置
    /// </summary>
    public class JwtAuthSection : ConfigSection
    {
        public override string SectionName => "JwtAuth";

        /// <summary>
        /// 签名秘钥
        /// </summary>
        public string SignSecret { get; set; }
        /// <summary>
        /// 加密秘钥，非必须
        /// </summary>
        public string EncryptSecret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        /// <summary>
        /// 是否验证过期
        /// </summary>
        public bool ValidateLifetime { get; set; }
        /// <summary>
        /// Jwt Token过期时间(分钟）
        /// </summary>
        public int ExpiresMinute { get; set; }
        /// <summary>
        /// 是否使用动态SignSecret，默认使用UserIp作为动态参数
        /// </summary>
        public bool DynamicSignSecret { get; set; }

        /// <summary>
        /// Debug时的Token（仅Development和Testing有效，用于设置默认swagger的jwt）
        /// </summary>
        public string DebugToken { get; set; }
    }
}
