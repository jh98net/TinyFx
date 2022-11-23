using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TinyFx;
using TinyFx.IO;
using TinyFx.Reflection;

namespace TinyFx.Net
{
    /// <summary>
    /// Web API访问客户端
    /// TODO: 写的不好需要改造
    /// </summary>
    public class WebApiClient
    {
        /// <summary>
        /// 压缩模式
        /// </summary>
        public DecompressionMethods DecompressionMethod { get; set; } = DecompressionMethods.None;
        /// <summary>
        /// Web API传输格式
        /// </summary>
        public WebApiResultFormatters Formatter { get; set; } = WebApiResultFormatters.Json;

        /// <summary>
        /// Web API基地址
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseAddress"></param>
        public WebApiClient(string baseAddress)
        {
            BaseAddress = baseAddress;
        }

        // 转换Result
        private object ConvertResult(Type type, Stream result)
        {
            object ret = null;
            switch (Formatter)
            {
                case WebApiResultFormatters.Json:
                    ret = SerializerUtil.DeserializeJson(type, result);
                    break;
                case WebApiResultFormatters.Xml:
                    ret = SerializerUtil.DeserializeXml(type, result);
                    break;
                case WebApiResultFormatters.Text:
                    ret = IOUtil.ReadStreamToString(result);
                    break;
            }
            return ret;
        }

        // 获得HttpClient
        private HttpClient GetClient()
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethod };
            return new HttpClient(handler);
        }

        #region Get
        private Stream GetResultStream(string method, params object[] parameters)
        {
            if (parameters != null && parameters.Length != 1 && parameters.Length % 2 != 0)
                throw new Exception("参数必须是1个或者是偶数，例如：Get(\"id\", 123);");
            using (var client = GetClient())
            {
                var url = new UriBuilderEx(BaseAddress+method);
                if (parameters != null)
                {
                    if (parameters.Length == 1)
                    {
                        var type = parameters[0].GetType();
                        if (ReflectionUtil.IsSimpleType(type)) // 简单类型应使用2个参数赋值
                            throw new Exception("一个简单类型需要使用2个参数赋值。");
                        foreach (var property in type.GetProperties())
                        {
                            var value = Convert.ToString(property.GetValue(parameters[0]));
                            url.AppendQueryString(property.Name, value);
                        }
                    }
                    else // 偶数
                    {
                        for (int i = 0; i < parameters.Length; i = i + 2)
                            url.AppendQueryString(parameters[i].ToString(), parameters[i + 1].ToString());
                    }
                }
                var request = new HttpRequestMessage() {
                    RequestUri = new Uri(url.ToString()),
                    Method = HttpMethod.Get,
                };
                //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.SendAsync(request).Result;
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"错误代码: {response.StatusCode}");
                return response.Content.ReadAsStreamAsync().Result;
            }
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T Get<T>(string method, params object[] parameters)
            => (T)ConvertResult(typeof(T), GetResultStream(method, parameters));
        /// <summary>
        /// GetString
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string GetString(string method, params object[] parameters)
            => IOUtil.ReadStreamToString(GetResultStream(method, parameters));
        #endregion

        #region Post
        private Stream PostResultStream(string method, params object[] parameters)
        {
            if (parameters != null && parameters.Length != 1 && parameters.Length % 2 != 0)
                throw new Exception("参数必须是1个或者是偶数，例如：Get(\"id\", 123);");
            using (var client = GetClient())
            {
                HttpContent content = null;
                if (parameters != null)
                {
                    if (parameters.Length == 1) // 1个参数
                    {
                        var type = parameters[0].GetType();
                        if (ReflectionUtil.IsSimpleType(type)) // 简单数据类型
                            content = new FormUrlEncodedContent(new Dictionary<string, string>() {
                                { "", Convert.ToString(parameters[0]) }
                            });
                        else
                            content = new StringContent(SerializerUtil.SerializeJson(parameters[0]));
                    }
                    else // 偶数参数
                    {
                        var pairs = new List<KeyValuePair<string, string>>();
                        for (int i = 0; i < parameters.Length; i = i + 2)
                            pairs.Add(new KeyValuePair<string, string>(parameters[i].ToString(), parameters[i + 1].ToString()));
                        content = new FormUrlEncodedContent(pairs);
                    }
                }
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(BaseAddress+method),
                    Method = HttpMethod.Post,
                    Content = content
                };
                //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.SendAsync(request).Result;
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"错误代码: {response.StatusCode}");
                return response.Content.ReadAsStreamAsync().Result;
            }
        }
        /// <summary>
        /// Post返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T Post<T>(string method, params object[] parameters)
            => (T)ConvertResult(typeof(T), PostResultStream(method, parameters));
        /// <summary>
        /// Post获得返回字符串
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string PostString(string method, params object[] parameters)
            => IOUtil.ReadStreamToString(GetResultStream(method, parameters));
        #endregion

    }

    /// <summary>
    /// Web API返回值格式
    /// </summary>
    public enum WebApiResultFormatters
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text,
        /// <summary>
        /// XML
        /// </summary>
        Xml,
        /// <summary>
        /// JSON
        /// </summary>
        Json
    }
}
