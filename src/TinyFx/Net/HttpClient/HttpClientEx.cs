﻿using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Http;
using Newtonsoft.Json;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Text.Json;
using System.Threading;
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
        public string ClientName => Config.Name;
        public Encoding Encoding => Config.Encoding;
        public JsonSerializerSettings JsonOptions { get; set; }
        private SocketsHttpHandler _httpHandler;
        public HttpClient Client { get; }
        private string _baseUrl;

        internal HttpClientEx(HttpClientConfig config)
        {
            config.Name ??= "default";
            config.Encoding ??= Encoding.UTF8;
            Config = config;
            JsonOptions = SerializerUtil.DefaultJsonNetSettings;

            _httpHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(15), // dns
                UseCookies = config.UseCookies // cookie
            };
            // ssl
            if (config.IgnoreSslValidation)
            {
                var sslOptions = new SslClientAuthenticationOptions
                {
                    RemoteCertificateValidationCallback = delegate { return true; }
                };
                _httpHandler.SslOptions = sslOptions;
            }
            // retry
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            var pollyHandler = new PolicyHttpMessageHandler(retryPolicy)
            {
                InnerHandler = _httpHandler,
            };
            Client = new HttpClient(pollyHandler);
            Client.Timeout = TimeSpan.FromMilliseconds(config.Timeout);
            Client.BaseAddress = GetBaseAddress();
            _baseUrl = Client.BaseAddress.ToString();
            config.RequestHeaders?.ForEach(x => Client.DefaultRequestHeaders.Add(x.Key, x.Value));
        }
        #endregion

        #region Request
        internal async Task<HttpResponseResult> RequestAsync(HttpRequestMessage request, Dictionary<string, string> parameters, object requestContent)
        {
            var ret = new HttpResponseResult();
            ret.RequestUtcTime = DateTime.UtcNow;
            try
            {
                if (Config.ReserveBody)
                {
                    ret.Request = new HttpRequestBody()
                    {
                        Method = request.Method,
                        RequestUri = $"{_baseUrl}{request.RequestUri?.ToString()}",
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
                using (var response = Client.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult())
                {
                    ret.Success = response.IsSuccessStatusCode;
                    ret.ResponseUtcTime = DateTime.UtcNow;
                    if (Config.ReserveBody)
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
                    using (var reader = new StreamReader(stream, Config.Encoding))
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
            if (Config.UseCookies)
                ret.Cookies = _httpHandler.CookieContainer.GetCookies(Client.BaseAddress).Cast<Cookie>().ToList();
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
            switch (Config.SerializeMode)
            {
                case SerializeMode.Json:
                    ret = SerializerUtil.DeserializeJsonNet<T>(result, JsonOptions);
                    break;
                case SerializeMode.Xml:
                    ret = SerializerUtil.DeserializeXml<T>(result, Config.Encoding);
                    break;
                default:
                    throw new Exception($"目前HttpClient只支持json和xml反序列化。{this.GetType().FullName}");
            }
            return ret;
        }
        #endregion

        #region Utils
        private Uri GetBaseAddress()
        {
            var value = Config.BaseAddress;
            if (string.IsNullOrEmpty(value))
                return null;
            value = value.EndsWith("/") ? value : $"{value}/";
            return new Uri(value);
        }
        /// <summary>
        /// 创建HttpClient代理
        /// </summary>
        /// <returns></returns>
        public ClientAgent CreateAgent()
            => new ClientAgent(this);
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
