using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Net;
using TinyFx.OAuth.Providers;

namespace TinyFx.OAuth
{
    public static class OAuthUtil
    {
        /// <summary>
        /// 获取验证url
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public static Task<string> GetOAuthUrl(OAuthProviders provider, string redirectUri)
        {
            return DIUtil.GetRequiredService<OAuthService>().GetOAuthUrl(provider, redirectUri);
        }

        /// <summary>
        /// 第三方登录回调后获取用户信息
        /// </summary>
        /// <param name="ipo"></param>
        /// <returns></returns>
        public static Task<ResponseResult<OAuthUserDto>> GetUserInfo(OAuthUserIpo ipo)
        {
            return DIUtil.GetRequiredService<OAuthService>().GetUserInfo(ipo);
        }

        internal static IOAuthProviderElement GetProviderElement(OAuthProviders provider)
        {
            var section = ConfigUtil.GetSection<OAuthSection>();
            var key = provider.ToString();
            if (!section.Providers.TryGetValue(key, out var ret))
                throw new Exception($"配置文件OAuth:Providers不存在key: {provider}");
            return ret;
        }
    }
}
