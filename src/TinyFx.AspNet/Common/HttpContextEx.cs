using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims;
using TinyFx.Collections;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Net;
using TinyFx.Security;
using TinyFx.Serialization;

namespace TinyFx.AspNet
{
    /// <summary>
    /// 当前上下文
    /// </summary>
    public static class HttpContextEx
    {
        #region Base
        /// <summary>
        /// 当前Current
        /// </summary>
        public static HttpContext Current
        {
            get
            {
                var contextAccessor = DIUtil.GetRequiredService<IHttpContextAccessor>();
                return contextAccessor.HttpContext;
            }
        }

        public static HttpRequest Request => Current?.Request;
        public static HttpResponse Response => Current?.Response;
        public static ClaimsPrincipal User => Current?.User;
        /// <summary>
        /// 当前授权用户编码
        /// </summary>
        public static string IdentityUserId => User?.Identity?.Name;
        public static ConnectionInfo Connection => Current?.Connection;
        public static IDictionary<object, object> Items => Current?.Items;

        public static bool TryGetItem<T>(string key, out T value)
        {
            var ret = Items.TryGetValue(key, out object v);
            value = ret ? (T)v : default;
            return ret;
        }
        public static void SetItem(string key, object value)
            => Items.Add(key, value);

        /// <summary>
        /// 获取header值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetHeaderValue(string name)
        {
            return Request?.Headers?[name].FirstOrDefault();
        }
        #endregion

        #region Context
        private const string CONTEXT_KEY = "CONTEXT_KEY";

        /// <summary>
        /// 设置自定义上下文
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        public static void SetContext<T>(T context)
            where T : class
        {
            if (Items.ContainsKey(CONTEXT_KEY))
                Items[CONTEXT_KEY] = context;
            else
                Items.Add(CONTEXT_KEY, context);
        }
        /// <summary>
        /// 获取自定义上下文
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T GetContext<T>()
            where T : class
        {
            if (!Items.TryGetValue(CONTEXT_KEY, out var ret))
                throw new Exception($"获取自定义上下文Contexnt失败，没有初始化! type:{typeof(T).GetType().FullName}");
            return (T)ret;
        }
        #endregion

        public static ILogBuilder GetLogBuilder()
            => Current?.RequestServices?.GetService<ILogBuilder>();

        public static string GetTraceId(this HttpContext context)
            => context?.TraceIdentifier?.ToString().Replace(':', '_');

        public static string TraceId
            => Current?.TraceIdentifier?.ToString().Replace(':', '_');

        #region Session
        public static ISession Session => Current?.Session;

        private static ISerializer _serializer = new TinyJsonSerializer();
        public static void SetSession(string key, object value)
        {
            var data = _serializer.Serialize(value);
            Session.Set(key, data);
        }
        public static T GetSessionOrDefault<T>(string key, T defaultValue)
        {
            var data = Session.Get(key);
            if (data == null)
                return defaultValue;
            return _serializer.Deserialize<T>(data);
        }
        public static T GetSessionOrException<T>(string key)
        {
            var data = Session.Get(key);
            return _serializer.Deserialize<T>(data);
        }
        #endregion

        #region Cookie Identity
        /// <summary>
        /// 使用cookie登录验证（需要配置SessionOrCookie）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="enforceUse">是否强制使用,true:没有配置抛出异常</param>
        /// <returns></returns>
        public static async Task SignInUseCookie(string userId, bool enforceUse = false)
        {
            var section = ConfigUtil.GetSection<SessionAndCookieSection>();
            if (section == null || !section.UseCookieIdentity)
            {
                if (enforceUse)
                    throw new Exception("使用cookie登录时，必须设置SessionOrCookieSection");
                return;
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userId)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.MaxValue,//cookie不过期
                IsPersistent = true,
            };

            await Current.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        /// <summary>
        /// cookie验证登出
        /// </summary>
        /// <returns></returns>
        public static async Task SignOutUseCookie()
        {
            var section = ConfigUtil.GetSection<SessionAndCookieSection>();
            if (section == null || !section.UseCookieIdentity)
            {
                return;
            }
            await Current.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        #endregion

        #region JWT
        private const string JWT_CONTEXT_KEY = "JWT_CONTEXT_KEY";
        public static JwtTokenInfo GetJwtToken()
        {
            if (TryGetItem<JwtTokenInfo>(JWT_CONTEXT_KEY, out var ret))
                return ret;
            var token = GetHeaderValue("Authorization");
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                token = token.Substring(7).Trim();
                if (!string.IsNullOrEmpty(token))
                    ret = JwtUtil.ReadJwtToken(token);
            }
            SetItem(JWT_CONTEXT_KEY, ret);
            return ret;
        }
        #endregion
    }
}