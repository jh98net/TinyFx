using Com.Ctrip.Framework.Apollo;
using Elasticsearch.Net.Specification.MachineLearningApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.IO;
using TinyFx.Logging;
using TinyFx.Net;
using static System.Collections.Specialized.BitVector32;

namespace TinyFx.AspNet.RequestLogging
{
    /// <summary>
    /// 记录配置文件RequestLogging:Urls中的请求日志
    /// </summary>
    internal class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private static HashSet<string> _innerUrl = new HashSet<string> 
        {
            "/healthz","/env"
        };
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, ILogBuilder logger)
        {
            var requestUrl = context.Request.Path.ToString().ToLower();
            if(_innerUrl.Contains(requestUrl))
            {
                await _next(context); // 继续执行
                return;
            }
            Stopwatch stopwatch = null;
            var section = ConfigUtil.GetSection<RequestLoggingSection>();
            if (section != null && section.Enabled)
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
                logger.AddField("Request.StartTime", DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                var urlDict = section.GetUrlDict();
                if (urlDict.Contains("*") || urlDict.Contains(requestUrl))
                {
                    logger.Level = section.LogLevel;
                    logger.CustomeExceptionLevel = section.CustomeExceptionLevel;
                    logger.LogRequestHeaders = section.LogRequestHeaders;
                    logger.LogRequestBody = section.LogRequestBody;
                    logger.LogResponseBody = section.LogResponseBody;
                }
            }
            await _next(context); // 继续执行

            logger.AddField("Request.UserId", context?.User?.Identity?.Name);
            logger.AddField("Request.TraceId", context.GetTraceId());
            logger.AddField("Request.Url", context.Request.Path.ToString());
            logger.AddField("Request.Method", context.Request.Method);
            if (logger.LogRequestHeaders)
                logger.AddField("Request.Headers", context.Request.Headers.ToDictionary(x => x.Key, v => string.Join(";", v.Value.ToList())));
            if (logger.LogRequestBody || logger.Exception != null)
                await LogRequestBody(context.Request, logger);

            /*
              if (section != null)
              {
                  var urlDict = section.GetUrlDict();
                  useConfig = urlDict.Contains("*") || urlDict.Contains(context.Request.Path.ToString().ToLower());
              }
              if (useConfig)
              {
                  logger.Level = section.LogLevel;
                  logger.LogRequestHeaders = section.LogRequestHeaders;
                  logger.LogRequestBody = section.LogRequestBody;
                  logger.LogResponseBody = section.LogResponseBody;


                  // response.body
                  var originalResponseBody = context.Response.Body;
                  using (var swapStream = new MemoryStream())
                  {
                      context.Response.Body = swapStream;
                      await _next(context); // 继续执行

                      if (logger.LogResponseBody && logger.Exception == null)
                          logger.AddField("Response.Body", await GetResponse(context.Response));
                      //data.Add("end.memory", GC.GetTotalAllocatedBytes());
                      await swapStream.CopyToAsync(originalResponseBody);
                      context.Response.Body = originalResponseBody;
                  }
              }
              else
              {
                  await _next(context); // 继续执行
              }
              */
            var rspState = new ResponseCompletedState
            {
                Logger = logger,
                Stopwatch = stopwatch
            };
            // 响应完成记录时间和存入日志
            context.Response.OnCompleted((data) =>
            {
                var state = (ResponseCompletedState)data;
                if (state.Stopwatch != null)
                {
                    state.Logger.AddField("Request.EndTime", DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    state.Stopwatch.Stop();
                    state.Logger.AddField("Request.ElaspedTime", state.Stopwatch.ElapsedMilliseconds);
                }
                state.Logger.Log();
                return Task.CompletedTask;
            }, rspState);
        }
        private static async Task LogRequestBody(HttpRequest request, ILogBuilder logger)
        {
            // 获取请求body内容
            if (request.Method == "POST")
            {
                try 
                {
                    logger.AddField("Request.Body", await request.GetRawBodyAsync());
                }
                catch(Exception ex)
                {
                    logger.AddField("Request.Body", ex.Message);
                }
            }
            else if (request.Method == "GET")
                logger.AddField("Request.Body", request.QueryString.Value);
        }
        private static async Task<string> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(response.Body, Encoding.UTF8, false, 1024, true);
            var text = await reader.ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }

        class ResponseCompletedState
        {
            public ILogBuilder Logger { get; set; }
            public Stopwatch Stopwatch { get; set; }
        }
    }
}
