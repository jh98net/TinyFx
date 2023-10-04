using Grpc.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Reflection;
using TinyFx.Serialization;
using TinyFx.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TinyFx.Net
{
    /// <summary>
    /// HttpClient封装类 (使用配置文件中的配置访问HTTP)
    /// HttpClientExFactory.Create()创建
    /// </summary>
    public class HttpClientEx
    {
        #region Properties
        internal HttpClientConfig Config { get; }
        private IHttpClientFactory _clientFactory;

        public string ClientName { get; private set; }
        public HttpMessageHandler HttpHandler { get; set; }
        public int Timeout { get; private set; }
        public string BaseAddress { get; private set; }
        public List<KeyValueItem> RequestHeaders { get; private set; } = new List<KeyValueItem>();

        /// <summary>
        /// 请求返回时是否保留RequestBody和ResponseBody信息
        /// </summary>
        public bool ReserveBody { get; private set; }
        public SerializeMode SerializeMode { get; private set; }
        /// <summary>
        /// 字符集
        /// </summary>
        public virtual Encoding Encoding { get; private set; }
        public virtual JsonSerializerSettings JsonOptions { get; set; }

        internal HttpClientEx(HttpClientConfig config)
        {
            Config = config;
            _clientFactory = DIUtil.GetService<IHttpClientFactory>();

            // HttpClient
            ClientName = !string.IsNullOrEmpty(config.Name) ? config.Name : "default";
            if (config.UseCookies)
                HttpHandler = new SocketsHttpHandler() { UseCookies = true };
            Timeout = config.Timeout > 1000 ? config.Timeout : 100000;
            BaseAddress = ParseBaseAddress(config.BaseAddress);
            config.RequestHeaders?.ForEach(x => RequestHeaders.Add(x));

            // 
            ReserveBody = Config.ReserveBody;
            SerializeMode = config.SerializeMode;
            Encoding = string.IsNullOrEmpty(config.Encoding)
                ? Encoding.UTF8 : Encoding.GetEncoding(config.Encoding);
            JsonOptions = SerializerUtil.DefaultJsonNetSettings;
        }
        #endregion

        #region Utils
        private string ParseBaseAddress(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            return value.EndsWith("/") ? value : $"{value}/";
        }
        public HttpClientEx AddBaseAddress(string baseAddress)
        {
            BaseAddress = ParseBaseAddress(baseAddress);
            return this;
        }
        public HttpClientEx AddDefaultRequestHeaders(string key, string value)
        {
            RequestHeaders.Add(new KeyValueItem(key, value));
            return this;
        }
        public HttpClientEx SetTimeout(int timeout)
        {
            Timeout = timeout;
            return this;
        }
        public HttpClientEx SetReserveBody(bool reserveBody = true)
        {
            ReserveBody = reserveBody;
            return this;
        }

        public HttpClientEx SetAllowSSLConnect()
        {
            if (Config.UseCookies)
                throw new Exception("HttpClientEx使用cookie,不支持SetAllowSSLConnect");
            if (HttpHandler == null)
                HttpHandler = new HttpClientHandler();
            if (HttpHandler is HttpClientHandler)
                ((HttpClientHandler)HttpHandler).ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
            else
                throw new Exception("HttpClientEx.SetAllowSSLConnect时HttpHandler不是HttpClientHandler");
            return this;
        }

        #endregion

        #region Request
        internal async Task<HttpResponseResult> RequestAsync(HttpRequestMessage request, Dictionary<string, string> parameters, object requestContent)
        {
            var ret = new HttpResponseResult();
            ret.RequestUtcTime = DateTime.UtcNow;
            try
            {
                if (ReserveBody)
                {
                    ret.Request = new HttpRequestBody()
                    {
                        Method = request.Method,
                        RequestUri = $"{BaseAddress}{request.RequestUri?.ToString()}",
                        RequestParams = parameters,
                        Content = request.Content,
                        RequestContent = requestContent,
                        Headers = request.Headers,
                        Properties = request.Options,
                        Version = request.Version.ToString()
                    };
                }

                //var task = GetClient().SendAsync(request);
                //if (!task.Wait(Timeout))
                //    throw new Exception($"HttpClient请求超时。url: {request.RequestUri} timeout:{Timeout}");
                using (var response = CreateClient().SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult())
                {
                    ret.Success = response.IsSuccessStatusCode;
                    ret.ResponseUtcTime = DateTime.UtcNow;
                    if (ReserveBody)
                    {
                        ret.Response = new HttpResponseBody()
                        {
                            Content = response.Content,
                            Headers = response.Headers,
                            Success = response.IsSuccessStatusCode,
                            ReasonPhrase = response.ReasonPhrase,
                            StatusCode = response.StatusCode,
                            Version = response.Version.ToString(),
                            ResponseString = ret.ResultString
                        };
                    }
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    using (var reader = new StreamReader(stream, Encoding))
                    {
                        ret.ResultString = await reader.ReadToEndAsync().ConfigureAwait(false);
                        if (ret.Response != null)
                            ret.Response.ResponseString = ret.ResultString;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, "HttpClientEx.RequestAsync resultString:{resultString}", ret.ResultString);
                ret.Success = false;
                ret.Exception = ex;
                ret.ExceptionString += ex.Message;
            }
            return ret;
        }
        internal async Task<HttpResponseResult<TSuccess, TError>> RequestAsync<TSuccess, TError>(HttpRequestMessage request, Dictionary<string, string> parameters, object requestContent)
        {
            var ret = new HttpResponseResult<TSuccess, TError>();
            var rsp = await RequestAsync(request, parameters, requestContent);
            ret.Success = rsp.Success;
            ret.Request = rsp.Request;
            ret.Response = rsp.Response;
            ret.Exception = rsp.Exception;
            ret.ResultString = rsp.ResultString;
            if (HttpHandler != null)
            {
                if (Config.UseCookies)
                {
                    ret.Cookies = ((SocketsHttpHandler)HttpHandler).CookieContainer.GetCookies(new Uri(BaseAddress)).Cast<Cookie>().ToList();
                }
            }
            try
            {
                if (rsp.Success)
                    ret.SuccessResult = Deserialize<TSuccess>(rsp.ResultString);
                else if (rsp.Exception == null)
                    ret.ErrorResult = Deserialize<TError>(rsp.ResultString);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, "HttpClientEx.RequestAsync时反序列化失败。success:{httpClient.success} resultString:{httpClient.resultString} TSuccess:{httpClient.TSuccess} TError:{httpClient.TError}"
                    , rsp.Success, ret.ResultString, typeof(TSuccess).FullName, typeof(TError).FullName);
                ret.Success = false;
                ret.Exception = ex;
                ret.ExceptionString += ex.Message;
            }
            return ret;
        }

        internal T Deserialize<T>(string result)
        {
            T ret;
            switch (SerializeMode)
            {
                case SerializeMode.Json:
                    ret = SerializerUtil.DeserializeJsonNet<T>(result, JsonOptions);
                    break;
                case SerializeMode.Xml:
                    ret = SerializerUtil.DeserializeXml<T>(result, Encoding);
                    break;
                default:
                    throw new Exception($"目前HttpClient只支持json和xml反序列化。{this.GetType().FullName}");
            }
            return ret;
        }
        #endregion

        #region Utils
        /// <summary>
        /// 创建HttpClient代理
        /// </summary>
        /// <returns></returns>
        public ClientAgent CreateAgent()
            => new ClientAgent(this);
        private Uri GetBaseAddress()
        {
            if (string.IsNullOrEmpty(BaseAddress))
                return null;
            return new Uri(BaseAddress);
        }
        protected HttpClient CreateClient()
        {
            HttpClient ret = null;
            if (HttpHandler != null)
            {
                ret = new HttpClient(HttpHandler);
            }
            else
            {
                ret = _clientFactory != null
                    ? _clientFactory.CreateClient(ClientName)
                    : new HttpClient();
            }
            ret.Timeout = TimeSpan.FromMilliseconds(Timeout);
            ret.BaseAddress = GetBaseAddress();
            RequestHeaders.ForEach(x => ret.DefaultRequestHeaders.Add(x.Key, x.Value));

            return ret;
        }
        public T GetSettingValue<T>(string key)
        {
            if (!Config.Settings.TryGetValue(key, out string ret))
                throw new Exception($"配置HttpClient:Clients Key不存在。 key: {key}");
            return ret.To<T>();
        }
        #endregion
    }

    public enum PostContentType
    {
        Unknow,
        StringContent,
        JsonContent,
        FormUrlEncoded,
        MultipartFormData,
        ByteArrayContent
    }
}
