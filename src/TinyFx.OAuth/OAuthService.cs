using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Net;

namespace TinyFx.OAuth
{
    public class OAuthService
    {
        private OAuthProviderContainer _container;
        public OAuthService() 
        {
            _container = DIUtil.GetRequiredService<OAuthProviderContainer>();
        }
        public async Task<string> GetOAuthUrl(OAuthProviders provider, string redirectUri)
        {
            return await _container.GetProvider(provider).GetOAuthUrl(redirectUri);
        }
        public async Task<ResponseResult<OAuthUserDto>> GetUserInfo(OAuthUserIpo ipo)
        {
            return await _container.GetProvider(ipo.OAuthProvider).GetUserInfo(ipo);
        }
    }
}
