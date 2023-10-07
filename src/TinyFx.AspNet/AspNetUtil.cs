using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Buffers;
using System.IO.Pipelines;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TinyFx.Security;
using Org.BouncyCastle.Ocsp;
using Microsoft.Extensions.Hosting;
using System.Security.Policy;
using TinyFx.Configuration;
using System.Diagnostics;
using TinyFx.Logging;
using Nacos;
using System.Xml.Linq;
using TinyFx.IO;
using System.Runtime;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace TinyFx.AspNet
{
    public static class AspNetUtil
    {
        #region RemoteIpAddress
        /// <summary>
        /// 获取客户端IP需要 services.AddSingleton(HttpContextAccessor);
        /// </summary>
        /// <returns></returns>
        public static string GetRemoteIpString()
        {
            return GetRemoteIpAddress()?.ToString();
        }

        /// <summary>
        /// 获取客户端IP需要 services.AddSingleton(HttpContextAccessor);
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetRemoteIpAddress()
        {
            var headers = HttpContextEx.Request.Headers;
            if (headers.ContainsKey("X-Forwarded-For"))
            {
                var ips = Convert.ToString(headers["X-Forwarded-For"]);
                if (!string.IsNullOrEmpty(ips))
                {
                    var ip = ips.Split(',', StringSplitOptions.RemoveEmptyEntries)[0];
                    if (IPAddress.TryParse(ip, out var ret))
                        return ret;
                }
            }
            return HttpContextEx.Connection.RemoteIpAddress?.MapToIPv4();
        }
        #endregion

        #region Referer Url
        /// <summary>
        /// 获取Referer路径
        /// </summary>
        /// <returns></returns>
        public static string GetRefererUrl()
        {
            return HttpContextEx.GetHeaderValue("Referer");
        }
        public static Uri GetRefererUri()
        {
            var url = GetRefererUrl();
            return url != null ? new Uri(url) : null;
        }
        /// <summary>
        /// 获取Referer基础路径，如: https://www.abc.com
        /// </summary>
        /// <param name="hasPort"></param>
        /// <returns></returns>
        public static string GetRefererBaseUrl(bool hasPort = false)
            => GetBaseUrl(GetRefererUrl(), hasPort);
        /// <summary>
        /// 获取Referer一级域名，如: abc.com
        /// </summary>
        /// <returns></returns>
        public static string GetRefererTopDomain()
        {
            var uri = GetRefererUri();
            return uri != null
                ? string.Join('.', uri.Host.Split('.').TakeLast(2))
                : null;
        }
        /// <summary>
        /// 获取相对Referer域名的二级域名,如：https://xxx.abc.com
        /// </summary>
        /// <param name="secondDomain"></param>
        /// <returns></returns>
        public static string GetRefererSecondDomainUrl(string secondDomain)
        {
            var uri = GetRefererUri();
            if (uri == null) return null;
            var topDomain = string.Join('.', uri.Host.Split('.').TakeLast(2));
            return $"{uri.Scheme}://{secondDomain}.{topDomain}";
        }

        /// <summary>
        /// 获取基础url，如:https://www.abc.com
        /// </summary>
        /// <param name="url"></param>
        /// <param name="hasPort"></param>
        /// <returns></returns>
        public static string GetBaseUrl(string url, bool hasPort = false)
        {
            if (string.IsNullOrEmpty(url)) return null;
            var uri = new Uri(url);
            var domain = hasPort
                ? $"{uri.Host}:{uri.Port}"
                : uri.Host;
            return $"{uri.Scheme}://{domain}";
        }
        #endregion

        #region Request Url
        /// <summary>
        /// 获得当前请求的主机URL（只包含Scheme,Host和端口）
        /// </summary>
        /// <param name="hasPort">是否包含端口</param>
        /// <returns></returns>
        public static string GetRequestBaseUrl(bool hasPort = false)
        {
            var schema = GetRequestSchema();
            var domain = hasPort
                ? HttpContextEx.Request.Host.Value
                : HttpContextEx.Request.Host.Host;
            return $"{schema}://{domain}";
        }

        /// <summary>
        /// 获取当前请求的顶级域名（有可能带端口），如：exemple.com
        /// </summary>
        /// <param name="hasPort">是否包含端口</param>
        /// <returns></returns>
        public static string GetRequestTopDomain(bool hasPort = false)
        {
            var host = hasPort
                ? HttpContextEx.Request.Host.Value
                : HttpContextEx.Request.Host.Host;
            return string.Join('.', host.Split('.').TakeLast(2));
        }

        /// <summary>
        /// 获取当前请求指定二级域名URL，如：http://xxx.exemple.com
        /// </summary>
        /// <param name="secondDomain"></param>
        /// <param name="hasPort">是否包含端口</param>
        /// <returns></returns>
        public static string GetRequestSecondDomainUrl(string secondDomain, bool hasPort = false)
        {
            var schema = GetRequestSchema();
            return $"{schema}://{secondDomain}.{GetRequestTopDomain(hasPort)}";
        }
        private static string GetRequestSchema()
        {
            var ret = HttpContextEx.Request.Headers.ContainsKey("X-Forwarded-Proto")
                ? HttpContextEx.Request.Headers["X-Forwarded-Proto"].FirstOrDefault() : null;
            if (ret == null)
                ret = HttpContextEx.Request.Scheme;
            return ret;
        }
        #endregion

        #region CONTEXT_ITEM
        internal const string CONTEXT_ITEM_SUCCESS_CODE = "CONTEXT_ITEM_SUCCESS_CODE";
        /// <summary>
        /// 设置api成功的code码
        /// </summary>
        /// <param name="code"></param>
        public static void SetSuccessCode(string code)
        {
            HttpContextEx.Items.TryAdd(CONTEXT_ITEM_SUCCESS_CODE, code);
        }
        internal static bool TryGetSuccessCode(out string code)
        {
            var ret = HttpContextEx.Items.TryGetValue(CONTEXT_ITEM_SUCCESS_CODE, out var v);
            code = ret ? (string)v : null;
            return ret;
        }

        internal const string CONTEXT_ITEM_UNHANDLED_EXCEPTION = "CONTEXT_ITEM_UNHANDLED_EXCEPTION";
        /// <summary>
        /// 设置api未处理异常的统一code码
        /// </summary>
        /// <param name="code"></param>
        public static void SetUnhandledExceptionCode(string code)
        {
            HttpContextEx.Items.TryAdd(CONTEXT_ITEM_UNHANDLED_EXCEPTION, code);
        }
        internal static bool TryGetUnhandledExceptionCode(out string code)
        {
            var ret = HttpContextEx.Items.TryGetValue(CONTEXT_ITEM_UNHANDLED_EXCEPTION, out var v);
            code = ret ? (string)v : null;
            return ret;
        }
        /// <summary>
        /// 设置api是否返回异常详细信息
        /// </summary>
        internal const string CONTEXT_ITEM_RESPONSE_EXCEPTION_DETAIL = "CONTEXT_ITEM_RESPONSE_EXCEPTION_DETAIL";
        public static void SetResponseExceptionDetail(bool detail = false)
        {
            HttpContextEx.Items.TryAdd(CONTEXT_ITEM_RESPONSE_EXCEPTION_DETAIL, detail);
        }
        internal static bool GetResponseExceptionDetail()
        {
            return HttpContextEx.Items.TryGetValue(CONTEXT_ITEM_RESPONSE_EXCEPTION_DETAIL, out var v)
                ? (bool)v : ConfigUtil.Project.ResponseErrorDetail;
        }

        public static async Task<string> GetRequestBodyString(Encoding encoding = null)
        {
            return await HttpContextEx.Request.GetRawBodyAsync(encoding);
        }

        /// <summary>
        /// 获取原始请求正文并转换成字符串
        /// </summary>
        /// <param name="request"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyAsync(this HttpRequest request, Encoding encoding = null)
        {
            request.Body.Seek(0, SeekOrigin.Begin);
            var ret = await IOUtil.GetStringFromPipe(request.BodyReader);
            request.Body.Seek(0, SeekOrigin.Begin);
            return ret;
            /*
            string ret = null;
            //request.EnableBuffering();
            request.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8))
            {
                ret = await reader.ReadToEndAsync();
                request.Body.Seek(0, SeekOrigin.Begin);
            }
            return ret;
            */
        }
        #endregion

        #region CORS
        internal static Action<CorsPolicyBuilder> GetPolicyBuilder(CorsPolicyElement element)
        {
            return new Action<CorsPolicyBuilder>(builder =>
            {
                builder.SetIsOriginAllowedToAllowWildcardSubdomains();
                // Origins
                if (!string.IsNullOrEmpty(element.Origins) && element.Origins.Trim() != "*")
                    builder.WithOrigins(element.Origins.Split(';'));
                else
                    builder.AllowAnyOrigin();
                // Methods
                if (!string.IsNullOrEmpty(element.Methods) && element.Origins.Trim() != "*")
                    builder.WithMethods(element.Methods.Split(';'));
                else
                    builder.AllowAnyMethod();
                // Headers
                if (!string.IsNullOrEmpty(element.Headers) && element.Origins.Trim() != "*")
                    builder.WithHeaders(element.Headers.Split(';'));
                else
                    builder.AllowAnyHeader();
                // MaxAge
                if (element.MaxAge > 0)
                    builder.SetPreflightMaxAge(TimeSpan.FromSeconds(element.MaxAge));
            });
        }
        #endregion

        /// <summary>
        /// 获取上下文日志
        /// </summary>
        /// <returns></returns>
        public static ILogBuilder GetContextLogBuilder()
            => HttpContextEx.GetLogBuilder();

        /// <summary>
        /// 验证通用的请求签名
        /// 签名规则：
        ///     request headers中包含指定headerName的sign信息。
        ///     验证sign => source是原始请求Body字符串，pkcs8 SHA256 Base64
        /// </summary>
        /// <param name="headerName"></param>
        /// <param name="publicKey">公钥(pkcs8)</param>
        /// <param name="keyMode">默认</param>
        /// <param name="hashName">默认SHA256</param>
        /// <param name="cipher">默认Base64</param>
        /// <param name="encoding">默认UTF8</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<bool> VerifyRequestHeaderSign(string headerName, string publicKey, RSAKeyMode keyMode = RSAKeyMode.PublicKey, HashAlgorithmName hashName = default, CipherEncode cipher = CipherEncode.Base64, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(headerName))
                throw new ArgumentNullException(nameof(headerName));
            if (string.IsNullOrEmpty(publicKey))
                throw new ArgumentNullException(nameof(publicKey));

            var request = HttpContextEx.Request;
            if (request.Headers.TryGetValue(headerName, out StringValues values))
            {
                var sign = values.FirstOrDefault();
                if (!string.IsNullOrEmpty(sign))
                {
                    // source
                    string source = await AspNetUtil.GetRawBodyAsync(request);
                    hashName = hashName == default ? HashAlgorithmName.SHA256 : hashName;
                    if (!string.IsNullOrEmpty(source))
                    {
                        // veriry
                        var ret = SecurityUtil.RSAVerifyData(source
                            , sign
                            , publicKey
                            , keyMode
                            , hashName
                            , cipher
                            , encoding
                        );
                        if (!ret)
                            LogUtil.Warning("AspNetUtil.VerifyRequestHeaderSign:验证异常.source:{rsa.source} sign:{rsa.sign} publicKey:{rsa.publicKey} keyMode:{rsa.keyMode} hashName:{rsa.hashName} cipher:{rsa.cipher}"
                                , source, sign, publicKey, keyMode, hashName, cipher);
                        return ret;
                    }
                    else
                        LogUtil.Warning("AspNetUtil.VerifyRequestHeaderSign:没有从请求body中获取值. url:{request.url}", request.Path.ToString());
                }
                else
                    LogUtil.Warning("AspNetUtil.VerifyRequestHeaderSign: 没有从HttpHeader中获取值。url:{request.url} headerName:{headerName}", request.Path.ToString(), headerName);
            }
            else
                LogUtil.Warning("AspNetUtil.VerifyRequestHeaderSign: 没有从HttpHeader中获取值。url:{request.url} headerName:{headerName}", request.Path.ToString(), headerName);
            return false;
        }

        internal static string MapEnvPath()
        {
            var dict = new Dictionary<string, object>
            {
                { "ConfigUtil.EnvironmentString", ConfigUtil.EnvironmentString },
                { "header:Host", HttpContextEx.Request.Headers["Host"].FirstOrDefault() },
                { "header:X-Forwarded-Proto", HttpContextEx.Request.Headers["X-Forwarded-Proto"].FirstOrDefault() },
                { "header:Referer", HttpContextEx.Request.Headers["Referer"].FirstOrDefault() },
                { "header:X-Real_IP", HttpContextEx.Request.Headers["X-Real_IP"].FirstOrDefault() },
                { "header:X-Forwarded-For", HttpContextEx.Request.Headers["X-Forwarded-For"].FirstOrDefault() },
                { "AspNetUtil.GetRequestBaseUrl()", AspNetUtil.GetRequestBaseUrl() },
                { "AspNetUtil.GetRefererUrl()", AspNetUtil.GetRefererUrl() },
                { "AspNetUtil.GetRemoteIpString()", AspNetUtil.GetRemoteIpString() },
                { "Process.GetCurrentProcess().Threads.Count", Process.GetCurrentProcess().Threads.Count },
                { "GCSettings.IsServerGC", GCSettings.IsServerGC },
                { "header总量", HttpContextEx.Request.Headers.Count },
            };
            foreach (var header in HttpContextEx.Request.Headers)
            {
                dict.Add($"headers.{header.Key}", header.Value);
            }
            ThreadPool.GetAvailableThreads(out var worker, out var completion);
            dict.Add("ThreadPool.GetAvailableThreads()", $"workerThreads:{worker} completionPortThreads:{completion}");

            return SerializerUtil.SerializeJsonNet(dict);
        }
    }
}
