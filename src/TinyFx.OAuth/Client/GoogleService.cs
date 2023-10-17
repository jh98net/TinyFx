using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Net;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TinyFx.OAuth.Client
{
    public class GoogleService : AbAuthRequest, IAuthRequest
    {
        private readonly string _authUrl = "https://accounts.google.com/o/oauth2/v2/auth";
        private readonly string _tokenUrl = "https://www.googleapis.com/oauth2/v3/token";
        private readonly string _infoUrl = "https://www.googleapis.com/oauth2/v3/userinfo";

        private readonly HttpClient _client = HttpClientExFactory.CreateClient(nameof(GoogleService));

        public GoogleService()
        {

        }

        /// <summary>
        ///  检查响应内容是否正确
        /// </summary>
        /// <param name="dic"></param>
        /// <exception cref="Exception"></exception>
        private void CheckResponse(Dictionary<string, object> dic)
        {
            if (dic.ContainsKey("error") || dic.ContainsKey("error_description"))
            {
                throw new Exception($"{dic.GetString("error")}: {dic.GetString("error_description")}");
            }
        }
        /// <summary>
        /// 跳转第三方授权地址
        /// </summary>
        /// <returns></returns>
        public Task<AuthUrlDto> GetOAuthUrl(string redirectUri)
        {
            AuthUrlDto model = new AuthUrlDto();
            //state 参数
            string state = Guid.NewGuid().ToString().Replace("-", "");
            //LoginCacheUtil.SetOAuthUserUrlState(state).GetAwaiter().GetResult();
            string url = $"{_authUrl}?response_type=code%20token&client_id={Config.ClientId}&redirect_uri={redirectUri}&scope=openid%20email%20profile&state=" + state;
            model.State = state;
            model.OAuthUrl = url;
            return Task.FromResult(model);
        }

        public async Task<AuthUserDto?> GetUserInfo(AuthCallbackIpo authCallback)
        {
            string response = string.Empty;
            try
            {
                response = await _client.GetAsync($"{_infoUrl}?access_token={authCallback.Access_token}").ConfigureAwait(false).GetAwaiter().GetResult().Content.ReadAsStringAsync();
                // var response = HttpUtils.RequestGet($"{this._infoUrl}?access_token={authCallback.Access_token}");
                var userObj = response.ParseObject();
                CheckResponse(userObj);
                var authUser = new AuthUserDto
                {
                    OAuthID = userObj.GetString("sub"),
                    UserName = "",
                    NickName = userObj.GetString("name"),
                    Avatar = userObj.GetString("picture"),
                    Location = userObj.GetString("locale"),
                    Email = userObj.GetString("email"),
                    OAuthType = OAuthEnum.Google,
                    originalUser = userObj,
                    originalUserStr = response
                };
                return authUser;
            }
            catch (Exception e)
            {
                LogUtil.Error("OAuth2 获取用户信息失败" + response + e.ToString());
            }
            return null;
        }
    }
}
