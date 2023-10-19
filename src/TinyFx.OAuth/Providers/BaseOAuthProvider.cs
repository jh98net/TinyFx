using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx.OAuth.Providers
{
    internal interface IOAuthProvider
    {
        Task<string> GetOAuthUrl(string redirectUri, string uuid);
        Task<ResponseResult<OAuthUserDto>> GetUserInfo(OAuthUserIpo ipo);
    }

    internal abstract class BaseOAuthProvider : IOAuthProvider
    {
        private const int EXPIRY_SECONDS = 600;
        protected abstract string OAuthUrl { get; }
        protected abstract string TokenUrl { get; }
        protected abstract string UserInfoUrl { get; }

        private IDistributedCache _dcache;
        public IOAuthProviderElement Config { get; }
        private HttpClientEx _client;

        public BaseOAuthProvider()
        {
            _dcache = DIUtil.GetRequiredService<IDistributedCache>();
            var provider = EnumUtil.ToEnum<OAuthProviders>(GetType().Name.TrimEnd("Provider"));
            Config = OAuthUtil.GetProviderElement(provider);
            _client = HttpClientExFactory.CreateClientEx(GetType().FullName);
        }

        protected abstract string AppendOAuthUrl();
        public async Task<string> GetOAuthUrl(string redirectUri, string uuid)
        {
            var state = StringUtil.GetGuidString();
            await _dcache.SetAsync(state, Encoding.UTF8.GetBytes(uuid ?? "default"), new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(EXPIRY_SECONDS)
            });
            var ret = $"{OAuthUrl}?response_type=code%20token&client_id={Config.ClientId}&redirect_uri={redirectUri}&state={state}";
            var append = AppendOAuthUrl();
            if (!string.IsNullOrEmpty(append))
                ret += $"&{append.TrimStart('&')}";
            return ret;
        }

        protected abstract string AppendUserUrl();

        protected async Task<ResponseResult<OAuthUserDto>> RequestUser<TSuccess, TError>(OAuthUserIpo ipo, Func<TSuccess, OAuthUserDto> convertFunc)
        {
            var ret = new ResponseResult<OAuthUserDto>();

            var log = LogUtil.GetContextLog();
            log.AddField("oauth.ipo", SerializerUtil.SerializeJson(ipo));
            var value = await _dcache.GetStringAsync(ipo.State);
            log.AddField("oauth.uuid", value);
            await _dcache.RemoveAsync(ipo.State);
            if (string.IsNullOrEmpty(value) || (!string.IsNullOrEmpty(ipo.Uuid) && value != "default" && ipo.Uuid != value))
            {
                ret.Success = false;
                ret.Message = "OAuth请求异常";
                return ret;
            }

            var rsp = await _client.CreateAgent()
                .AddUrl($"{UserInfoUrl}?access_token={ipo.AccessToken}")
                .AppendUrl(AppendUserUrl())
                .GetAsync<TSuccess, TError>();
            log.AddField("oauth.rsp", SerializerUtil.SerializeJson(rsp));
            if (rsp.Success)
            {
                ret.Success = true;
                ret.Result = convertFunc(rsp.SuccessResult);
                log.AddField("oauth.result", SerializerUtil.SerializeJson(ret.Result));
            }
            else
            {
                ret.Success = false;
                ret.Exception = rsp.Exception;
                ret.Message = rsp.ResultString;
                log.AddField("oauth.resultString", rsp.ResultString);
                log.AddException(rsp.Exception);
            }
            if (!log.IsContextLog)
                log.SetFlag("OAUTH").Save();
            return ret;
        }

        public abstract Task<ResponseResult<OAuthUserDto>> GetUserInfo(OAuthUserIpo ipo);
    }
}
