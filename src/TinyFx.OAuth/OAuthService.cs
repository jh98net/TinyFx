using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Net;
using TinyFx.OAuth.Providers;
using TinyFx.Reflection;

namespace TinyFx.OAuth
{
    public class OAuthService
    {
        private ConcurrentDictionary<OAuthProviders, IOAuthProvider> _dict = new();
        public OAuthService() 
        {
            var list = EnumUtil.GetInfo<OAuthProviders>();
            foreach (var item in list.GetList())
            {
                var type = $"TinyFx.OAuth.Providers.{item.Name}Provider";
                var obj = ReflectionUtil.CreateInstance(type) as IOAuthProvider;
                if (obj == null)
                    throw new Exception($"创建IOAuthProvider失败: {type}");
                _dict.TryAdd((OAuthProviders)item.Value, obj);
            }
        }
        public async Task<string> GetOAuthUrl(OAuthProviders provider, string redirectUri)
        {
            return await GetProvider(provider).GetOAuthUrl(redirectUri);
        }
        public async Task<ResponseResult<OAuthUserDto>> GetUserInfo(OAuthUserIpo ipo)
        {
            return await GetProvider(ipo.OAuthProvider).GetUserInfo(ipo);
        }
        private IOAuthProvider GetProvider(OAuthProviders provider)
        {
            if (!_dict.TryGetValue(provider, out var ret))
                throw new Exception($"IOAuthProvider不存在。provider: {provider}");
            return ret;
        }
    }
}
