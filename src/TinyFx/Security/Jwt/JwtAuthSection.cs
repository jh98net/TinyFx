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
        /// 是否起效
        /// </summary>
        public bool Enabled { get; set; } = true;
        /// <summary>
        /// 签名秘钥
        /// </summary>
        public string SignSecret { get; set; } = "Sh7d97oQYx3ufffKasV8q";
        /// <summary>
        /// 加密秘钥，非必须
        /// </summary>
        public string EncryptSecret { get; set; }
        public string Audience { get; set; } = "tinyfx.com";
        public string Issuer { get; set; } = "tinyfx.com";
        /// <summary>
        /// 是否验证过期
        /// </summary>
        public bool ValidateLifetime { get; set; } = false;
        /// <summary>
        /// Jwt Token过期时间(分钟）
        /// </summary>
        public int ExpireMinutes { get; set; } = int.MaxValue;
        /// <summary>
        /// 是否使用动态SignSecret，默认使用UserIp作为动态参数
        /// </summary>
        public bool DynamicSignSecret { get; set; } = false;

        /// <summary>
        /// Debug时的Token（仅Development和Testing有效，用于设置默认swagger的jwt）
        /// </summary>
        public string DebugToken { get; set; }
    }
}
