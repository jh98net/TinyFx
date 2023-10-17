using Microsoft.Extensions.DependencyInjection;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Net;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TinyFx.OAuth.Client
{
    public class FaceBookService : AbAuthRequest, IAuthRequest
    {
        private readonly string _authUrl = "https://www.facebook.com/v17.0/dialog/oauth";
        private readonly string _tokenUrl = "https://graph.facebook.com/oauth/access_token";
        private readonly string _infoUrl = "https://graph.facebook.com/me";
        private readonly HttpClient _client = HttpClientExFactory.CreateClient(nameof(FaceBookService));

        public FaceBookService()
        {

        }

        private string GetUserPicture(Dictionary<string, object> userObj)
        {
            string picture = string.Empty;
            if (userObj.ContainsKey("picture"))
            {
                var pictureObj = userObj.GetString("picture").ParseObject();
                pictureObj = pictureObj.GetString("data").ParseObject();
                if (null != pictureObj)
                {
                    picture = pictureObj.GetString("url");
                }
            }
            return picture;
        }
        /// <summary>
        /// 检查响应内容是否正确
        /// </summary>
        /// <param name="dic"></param>
        /// <exception cref="Exception"></exception>
        private void CheckResponse(Dictionary<string, object> dic)
        {
            if (dic.ContainsKey("error"))
            {
                throw new Exception($"{dic.GetString("error").ParseObject().GetString("message")}");
            }
        }
        /// <summary>
        /// 跳转第三方授权地址
        /// </summary>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public Task<AuthUrlDto> GetOAuthUrl(string redirectUri)
        {
            AuthUrlDto model = new AuthUrlDto();
            //state 参数
            string state = Guid.NewGuid().ToString().Replace("-", "");
            //LoginCacheUtil.SetOAuthUserUrlState(state).GetAwaiter().GetResult();
            string url = $"{_authUrl}?response_type=code%20token&client_id={Config.ClientId}&redirect_uri={redirectUri}&state=" + state;
            model.State = state;
            model.OAuthUrl = url;
            return Task.FromResult(model);
        }

        public async Task<AuthUserDto?> GetUserInfo(AuthCallbackIpo authCallback)
        {
            string response = string.Empty;
            try
            {
                response = await _client.GetAsync($"{_infoUrl}?access_token={authCallback.Access_token}&fields=id,name,birthday,gender,hometown,email,devices,picture.width(400)").ConfigureAwait(false).GetAwaiter().GetResult().Content.ReadAsStringAsync();
                //var response = HttpUtils.RequestGet($"{this._infoUrl}?access_token={authCallback.Access_token}&fields=id,name,birthday,gender,hometown,email,devices,picture.width(400)");
                var userObj = response.ParseObject();
                CheckResponse(userObj);
                var authUser = new AuthUserDto
                {
                    OAuthID = userObj.GetString("id"),
                    UserName = "",
                    NickName = userObj.GetString("name"),
                    Avatar = GetUserPicture(userObj),
                    Location = userObj.GetString("locale"),
                    Email = userObj.GetString("email"),
                    OAuthType = OAuthEnum.Facebook,
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
