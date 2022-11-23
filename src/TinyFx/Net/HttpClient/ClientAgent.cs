using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Reflection;

namespace TinyFx.Net
{
    public class ClientAgent
    {
        #region Properties
        private HttpClientEx _client;
        public string Url { get; set; }
        public readonly Dictionary<string, string> Parameters = new Dictionary<string, string>();
        public readonly Dictionary<string, string> RequestHeaders = new Dictionary<string, string>();

        private HttpContent PostContent;
        private object RequestContent;
        public PostContentType PostContentType { get; private set; } = PostContentType.Unknow;

        public ClientAgent(HttpClientEx client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public ClientAgent AddUrl(string url)
        {
            Url = url;
            return this;
        }
        /// <summary>
        /// 添加key/value数据
        ///     get时拼接url
        ///     post时服务器使用x-www-form-urlencoded接收数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ClientAgent AddParameter(string key, string value)
        {
            Parameters.Add(key, value);
            return this;
        }
        /// <summary>
        /// 解析对象的属性值并添加为请求参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objParams"></param>
        /// <returns></returns>
        public ClientAgent AddParameter<T>(T objParams)
        {
            foreach (var param in ParseKeyValueContent<T>(objParams))
                Parameters.Add(param.Key, param.Value);
            return this;
        }
        /// <summary>
        /// 添加请求Header
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ClientAgent AddRequestHeader(string key, string value)
        {
            RequestHeaders.Add(key, value);
            return this;
        }
        #endregion

        #region PostContent
        /// <summary>
        /// application/json
        /// asp.net core 默认使用此模式，等同[FromBody]
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public ClientAgent BuildJsonContent(string json)
        {
            CheckPostContent();
            PostContent = new StringContent(json, _client.Encoding, "application/json");
            RequestContent = json;
            return this;
        }
        /// <summary>
        /// application/json
        /// asp.net core 默认使用此模式，等同[FromBody]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ClientAgent BuildJsonContent(object obj)
            => BuildJsonContent(SerializerUtil.SerializeJson(obj));

        /// <summary>
        /// 服务器mutipart/form-data
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public ClientAgent BuildStringContent(string content)
        {
            CheckPostContent();
            PostContent = new StringContent(content);
            PostContentType = PostContentType.StringContent;
            RequestContent = content;
            return this;
        }
        /// <summary>
        /// 请求正文中的表单数据(即key/value方式)来绑定参数或属性，有大小限制
        /// 对应asp.net core [FromForm] => x-www-form-urlencoded
        /// </summary>
        /// <returns></returns>
        public ClientAgent BuildFormUrlEncodedContent()
        {
            CheckPostContent();
            PostContent = new FormUrlEncodedContent(Parameters);
            PostContentType = PostContentType.FormUrlEncoded;
            RequestContent = Parameters;
            return this;
        }
        /// <summary>
        /// 请求正文中的表单数据来绑定参数或属性
        /// 对应asp.net core [FromForm] => multipart/form-data
        /// </summary>
        /// <returns></returns>
        public ClientAgent BuildMultipartFormDataContent()
        {
            CheckPostContent();
            var content = new MultipartFormDataContent();
            foreach (var kvPair in Parameters)
            {
                content.Add(new StringContent(kvPair.Value), kvPair.Key);
            }
            PostContent = content;
            PostContentType = PostContentType.MultipartFormData;
            RequestContent = Parameters;
            return this;
        }
        /// <summary>
        /// 服务器mutipart/form-data
        /// </summary>
        /// <param name="content"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public ClientAgent BuildByteArrayContent(byte[] content, string mediaType)
        {
            PostContent = new ByteArrayContent(content);
            PostContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            RequestContent = Convert.ToBase64String(content);
            return this;
        }
        private void CheckPostContent()
        {
            if (PostContent != null)
                throw new Exception($"已设置当前PostContent: {PostContentType}");
        }
        #endregion

        #region GET & POST
        /// <summary>
        /// GET返回字符串
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseResult> GetStringAsync()
            => await _client.RequestAsync(BuildGetRequest(), Parameters,RequestContent);
        /// <summary>
        /// GET返回对象
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <returns></returns>
        public async Task<HttpResponseResult<TSuccess, TError>> GetAsync<TSuccess, TError>()
            => await _client.RequestAsync<TSuccess, TError>(BuildGetRequest(), Parameters, RequestContent);

        /// <summary>
        /// POST返回字符串
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseResult> PostStringAsync()
            => await _client.RequestAsync(BuildPostRequest(), Parameters, RequestContent);
        /// <summary>
        /// POST返回对象
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <returns></returns>
        public async Task<HttpResponseResult<TSuccess, TError>> PostAsync<TSuccess, TError>()
            => await _client.RequestAsync<TSuccess, TError>(BuildPostRequest(), Parameters, RequestContent);
        #endregion

        #region Utils
        private HttpRequestMessage BuildGetRequest()
        {
            var url = Url.Trim().Trim('?', '&');
            if (Parameters.Count > 0)
            {
                url = url.Contains("?") ? $"{url}&" : $"{url}?";
                foreach (var item in Parameters)
                {
                    url += $"{item.Key}={WebUtility.UrlEncode(item.Value)}&";
                }
                url = url.TrimEnd('&');
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            RequestContent = Parameters;
            AddRequestHeaders(request);
            return request;
        }
        private HttpRequestMessage BuildPostRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Url);
            AddRequestHeaders(request);
            if (PostContent == null)
                throw new Exception("PostContent不能为空，请通过对应BuildContent方法添加");
            request.Content = PostContent;
            return request;
        }
        private void AddRequestHeaders(HttpRequestMessage request)
        {
            foreach (var header in RequestHeaders)
                request.Headers.Add(header.Key, header.Value);
        }
        private static Dictionary<string, string> ParseKeyValueContent<T>(T paramObj)
        {
            var ret = new Dictionary<string, string>();
            var type = paramObj.GetType();
            if (ReflectionUtil.IsSimpleType(type))
            {
                ret.Add("", Convert.ToString(paramObj));
            }
            else
            {
                foreach (var property in type.GetProperties())
                {
                    var value = ReflectionUtil.GetPropertyValue(paramObj, property.Name);
                    ret.Add(property.Name, Convert.ToString(value));
                }
            }
            return ret;
        }
        #endregion
    }
}
