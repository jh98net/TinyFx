﻿using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
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
            var value = $"{DateTime.UtcNow.UtcDateTimeToTimestamp(false)}|{uuid}";
            await _dcache.SetStringAsync(state, value);
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

            var value = await _dcache.GetStringAsync(ipo.State);
            await _dcache.RemoveAsync(ipo.State);
            var keys = value?.Split('|');
            if (string.IsNullOrEmpty(value)
                || (TimeSpan.FromMilliseconds(DateTime.UtcNow.UtcDateTimeToTimestamp(false) - keys[0].ToInt64()).TotalSeconds > EXPIRY_SECONDS)
                || (!string.IsNullOrEmpty(ipo.Uuid) && ipo.Uuid != keys[1]))
            {
                ret.Success = false;
                ret.Message = "登录超时";
                return ret;
            }

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
