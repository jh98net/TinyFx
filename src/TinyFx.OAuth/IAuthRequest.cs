
using System.ComponentModel;
using TinyFx.Configuration;


namespace TinyFx.OAuth
{
    /// <summary>
    /// 三方对接统一接口
    /// </summary>
    public interface  IAuthRequest
    {
        /// <summary>
        /// 获取第三方授权地址url
        /// </summary>
        /// <returns></returns>
        Task<AuthUrlDto> GetOAuthUrl(string redirectUri);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="authCallback">三方授权token</param>
        /// <returns></returns>
        Task<AuthUserDto?> GetUserInfo(AuthCallbackIpo authCallback);


    }

   public abstract class AbAuthRequest
   {
       public virtual string AuthConfigName { get; set; }

        private IAuthConfig? _Config;

        public AbAuthRequest()
        {
            AuthConfigName = GetType().Name.Replace("Service","Config");
            DefaultConfig();
        }

        public IAuthConfig? Config
        {
            get =>
                _Config==null?  AppDependencyResolver.Current.GetServices<IAuthConfig>()
                    .FirstOrDefault(a => a.GetType().Name == AuthConfigName): _Config;
            set =>_Config=value;
        }
        public virtual void DefaultConfig()
        {
            Config = ConfigUtil.GetSection<OAuthConfig>().OAuthConfigDic[AuthConfigName];
        }
    }

    /// <summary>
    /// 前端传入三方返回授权信息
    /// </summary>
    public class AuthCallbackIpo
    {
        /// <summary>
        /// 访问AuthorizeUrl后回调时带的参数code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 访问AuthorizeUrl后回调时带的参数state，用于和请求AuthorizeUrl前的state比较，防止CSRF攻击
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///  回调后返回的oauth_token
        /// </summary>
        public string Access_token { get; set; }
        /// <summary>
        ///  Google = 2
        ///  Facebook = 1,
        /// </summary>
        public OAuthEnum OAuthType { get; set; }

    }
    /// <summary>
    /// 三方返回用户信息
    /// </summary>
    public class AuthUserDto
    {
        /// <summary>
        ///  用户第三方系统的唯一id。在调用方集成改组件时，可以用uuid + source唯一确定一个用户
        /// </summary>
        public string OAuthID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 用户网址
        /// </summary>
        public string Blog { get; set; }
        /// <summary>
        /// 所在公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户备注（各平台中的用户个人介绍）
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 用户来源
        /// </summary>
        public OAuthEnum OAuthType { get; set; }

        /// <summary>
        /// 原有的用户信息(第三方返回的)
        /// </summary>
        public object originalUser { get; set; }

        public string originalUserStr { get; set; }

    }

    public class AuthUrlIpo
    {
        /// <summary>
        /// Google=2, Facebook=1,Twitter=3,
        /// </summary>
        public OAuthEnum OAuthType { get; set; }
        /// <summary>
        /// 三方跳转url
        /// </summary>
        public string RedirectUri { get; set; }
    }
    public class AuthUrlDto
    {
         
        public string State { get; set; }
        /// <summary>
        /// 三方跳转url
        /// </summary>
        public string OAuthUrl { get; set; }
    }
    /// <summary>
    /// 三方枚举类型
    /// </summary>
    public enum OAuthEnum
    {
        /// <summary>
        ///  Facebook = 1,
        /// </summary>
        [Description("FaceBook")]
        Facebook = 1,
        /// <summary>
        ///   Google = 2
        /// </summary>
        [Description("Google")]
        Google = 2,
        /// <summary>
        ///  Twitter = 3,
        /// </summary>
        [Description("Twitter")]
        Twitter = 3,
    }
}
