using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Collections;
using TinyFx.Configuration;
using TinyFx.Text;

namespace TinyFx.OAuth
{
    public class OAuthConfig : ConfigSection
    {
        public override string SectionName => "OAuth";
        /// <summary>
        /// redis连接集合
        /// </summary>
        public Dictionary<string, AuthConfig> OAuthConfigDic { get; set; }

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);

            OAuthConfigDic = configuration
                .Get<Dictionary<string, AuthConfig>>() ?? new();
        }
    }
    public interface IAuthConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
    public class AuthConfig : IAuthConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

}
