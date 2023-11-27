using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Text;
using TinyFx.Configuration;
using TinyFx.Logging;

namespace TinyFx.AspNet.RequestLogging
{
    /// <summary>
    /// 记录配置文件RequestLogging:Urls中的请求日志
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private static HashSet<string> _innerUrl = new HashSet<string> 
        {
            "/healthz","/env","/dump"
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            logger.AddField("Request.StartTime", DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            logger.AddField("Request.TraceId", context.GetTraceId());
            logger.AddField("Request.Url", context.Request.Path.ToString());
            logger.AddField("Request.Method", context.Request.Method);

            var section = ConfigUtil.GetSection<RequestLoggingSection>();
            if (section != null && section.Enabled)
            {
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
            if (logger.LogRequestHeaders)
                logger.AddField("Request.Headers", context.Request.Headers.ToDictionary(x => x.Key, v => string.Join(";", v.Value.ToList())));
            if (logger.LogRequestBody)
                await LogRequestBody(context.Request, logger);

            await _next(context); // 继续执行

            logger.AddField("Request.UserId", context?.User?.Identity?.Name);
            logger.AddField("Request.EndTime", DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            if (!logger.LogRequestBody && logger.Exception != null)
                await LogRequestBody(context.Request, logger);

            stopwatch.Stop();
            logger.AddField("Request.ElaspedTime", stopwatch.ElapsedMilliseconds);
            logger.Save();
        }
        private static async Task LogRequestBody(HttpRequest request, ILogBuilder logger)
        {
            // 获取请求body内容
            if (request.Method == "POST")
            {
                try 
                {
                    logger.AddRequestBody(await AspNetUtil.GetRawBodyAsync(request));
                }
                catch(Exception ex)
                {
                    logger.AddRequestBody(ex.Message);
                }
            }
            else if (request.Method == "GET")
                logger.AddRequestBody(request.QueryString.Value);
        }
        private static async Task<string> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(response.Body, Encoding.UTF8, false, 1024, true);
            var text = await reader.ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}
