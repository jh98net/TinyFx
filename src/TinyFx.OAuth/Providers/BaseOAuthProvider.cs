using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Net;

namespace TinyFx.OAuth.Providers
{
    internal interface IOAuthProvider
    {
        Task<string> GetOAuthUrl(string redirectUri);
        Task<ResponseResult<OAuthUserDto>> GetUserInfo(OAuthUserIpo ipo);
    }

    internal abstract class BaseOAuthProvider : IOAuthProvider
    {
        protected abstract string OAuthUrl { get; }
        protected abstract string TokenUrl { get; }
        protected abstract string UserInfoUrl { get; }

        public IOAuthProviderElement Config { get; }
        private HttpClientEx _client;

        public BaseOAuthProvider()
        {
            var provider = EnumUtil.ToEnum<OAuthProviders>(GetType().Name.TrimEnd("Provider"));
            Config = OAuthUtil.GetProviderElement(provider);
            _client = HttpClientExFactory.CreateClientEx(GetType().FullName);
        }

        protected abstract string AppendOAuthUrl();
        public Task<string> GetOAuthUrl(string redirectUri)
        {
            var state = StringUtil.GetGuidString();
            var ret = $"{OAuthUrl}?response_type=code%20token&client_id={Config.ClientId}&redirect_uri={redirectUri}&state={state}";
            var append = AppendOAuthUrl();
            if (!string.IsNullOrEmpty(append))
                ret += $"&{append.TrimStart('&')}";
            return Task.FromResult(ret);
        }

        protected abstract string AppendUserUrl();

        protected async Task<ResponseResult<OAuthUserDto>> RequestUser<TSuccess, TError>(OAuthUserIpo ipo, Func<TSuccess, OAuthUserDto> convertFunc)
        {
            var ret = new ResponseResult<OAuthUserDto>();
            var rsp = await _client.CreateAgent()
                .AddUrl($"{UserInfoUrl}?access_token={ipo.AccessToken}")
                .AppendUrl(AppendUserUrl())
                .GetAsync<TSuccess, TError>();
            if (rsp.Success)
            {
                ret.Success = true;
                ret.Result = convertFunc(rsp.SuccessResult);
            }
            else
            {
                ret.Success = false;
                ret.Exception = rsp.Exception;
                ret.Message = rsp.ResultString;
            }
            return ret;
        }

        public abstract Task<ResponseResult<OAuthUserDto>> GetUserInfo(OAuthUserIpo ipo);
    }
}
