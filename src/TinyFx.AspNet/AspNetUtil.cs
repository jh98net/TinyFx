using System.Net;
using System.Web;

namespace TinyFx.AspNet
{
    public static class AspNetUtil
    {
        /// <summary>
        /// 获取客户端IP需要 services.AddSingleton(HttpContextAccessor);
        /// </summary>
        /// <returns></returns>
        public static string GetRemoteIpString()
            => GetRemoteIpAddress().MapToIPv4().ToString();

        /// <summary>
        /// 获取客户端IP需要 services.AddSingleton(HttpContextAccessor);
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetRemoteIpAddress()
            => HttpContextEx.Current.Connection.RemoteIpAddress;

        /// <summary>
        /// 获得当前请求的主机URL（只包含Scheme和Host）
        /// </summary>
        /// <returns></returns>
        public static string GetRequestHostUrl()
        {
            var request = HttpContextEx.Current.Request;
            return $"{request.Scheme}://{request.Host}";
        }
    }
}
