using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Reflection;

namespace TinyFx.Net
{
    /// <summary>
    /// HttpClient封装类 (使用配置文件中的配置访问HTTP)
    /// HttpClientExFactory.Create()创建
    /// </summary>
    public class HttpClientEx
    {
        #region Properties
        public string ClientName { get; }
        /// <summary>
        /// 是否配置文件中配置的Client
        /// </summary>
        public bool IsConfigClient { get; internal set; }
        //private HttpClientsElement _element;
        /// <summary>
        /// 请求返回时是否保留RequestBody和ResponseBody信息
        /// </summary>
        public bool HandlerBody { get; set; } = true;
        private HttpClient _client { get; }
        public SerializeMode SerializeMode { get; set; } = SerializeMode.Json;
        /// <summary>
        /// 字符集
        /// </summary>
        public virtual Encoding Encoding { get; set; } = Encoding.UTF8;
        private int _timeout;
        public int Timeout
        {
            get { return _timeout; }
            set
            {
                _timeout = value;
                _client.Timeout = TimeSpan.FromMilliseconds(value);
            }
        }

        /// <summary>
        /// 第三方相关参数设置
        /// </summary>
        public ConcurrentDictionary<string, string> Settings { get; set; }

        public virtual JsonSerializerOptions JsonOptions { get; set; } = SerializerUtil.DefaultJsonOptions;

        internal HttpClientEx(string clientName, bool handlerBody, HttpClient client)
        {
            if (string.IsNullOrEmpty(clientName))
                throw new ArgumentNullException(nameof(clientName));
            ClientName = clientName;
            HandlerBody = handlerBody;
            _client = client;
        }
        #endregion
        public HttpClientEx AddBaseAddress(string baseAddress)
        {
            _client.BaseAddress = new Uri(baseAddress);
            return this;
        }
        public HttpClientEx AddDefaultRequestHeaders(string key, string value)
        {
            _client.DefaultRequestHeaders.Add(key, value);
            return this;
        }
        public HttpClientEx SetTimeout(int timeout)
        {
            Timeout = timeout;
            return this;
        }
        #region Request
        internal async Task<HttpResponseResult> RequestAsync(HttpRequestMessage request, Dictionary<string, string> parameters, object requestContent)
        {
            var ret = new HttpResponseResult();
            try
            {
                if (HandlerBody)
                {
                    ret.Request = new HttpRequestBody()
                    {
                        Method = request.Method,
                        RequestUri = request.RequestUri,
                        RequestParams = parameters,
                        Content = request.Content,
                        RequestContent = requestContent,
                        Headers = request.Headers,
#if !NETSTANDARD2_0
                        Properties = request.Options,
#else
                        Properties = request.Properties,
#endif
                        Version = request.Version.ToString()
                    };
                }
                // TODO: 必须使用Wait！！未知原因
                //var response = await GetClient().SendAsync(request).ConfigureAwait(false);
                var task = GetClient().SendAsync(request);
                if (!task.Wait(Timeout))
                    throw new Exception($"HttpClient请求超时。url: {request.RequestUri} timeout:{Timeout}");
                using (var response = task.Result)
                {
                    ret.Success = response.IsSuccessStatusCode;
                    using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync(), Encoding))
                    {
                        ret.ResultString = reader.ReadToEnd();
                    }
                    if (HandlerBody)
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
                }
            }
            catch (Exception ex)
            {
                ret.Success = false;
                ret.Exception = ex;
            }
            return ret;
        }
        internal async Task<HttpResponseResult<TSuccess, TError>> RequestAsync<TSuccess, TError>(HttpRequestMessage request, Dictionary<string, string> parameters, object requestContent)
        {
            var ret = new HttpResponseResult<TSuccess, TError>();
            try
            {
                var rsp = await RequestAsync(request, parameters, requestContent);
                ret.Success = rsp.Success;
                ret.Request = rsp.Request;
                ret.Response = rsp.Response;
                ret.Exception = rsp.Exception;
                ret.ResultString = rsp.ResultString;
                if (rsp.Success)
                    ret.SuccessResult = Deserialize<TSuccess>(rsp.ResultString);
                else
                    ret.ErrorResult = Deserialize<TError>(rsp.ResultString);
            }
            catch (Exception ex)
            {
                ret.Success = false;
                ret.Exception = ex;
            }
            return ret;
        }

        internal T Deserialize<T>(string result)
        {
            T ret;
            switch (SerializeMode)
            {
                case SerializeMode.Json:
                    ret = JsonSerializer.Deserialize<T>(result, JsonOptions);
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
        protected HttpClient GetClient()
        {
            return _client;
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
