using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx.AspNet
{
    /// <summary>
    /// 全局异常处理中间件
    /// </summary>
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private IWebHostEnvironment _environment;
        private GlobalExceptionSection _option;
        /// <summary>
        /// 需要处理的状态码字典
        /// </summary>
        private IDictionary<int, string> _exceptionStatusCodeDic;
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _option = ConfigUtil.GetSection<GlobalExceptionSection>();
            _logger = logger;
            _environment = environment;
            _exceptionStatusCodeDic = new Dictionary<int, string>
            {
                { 401, "未授权的请求" },
                { 404, "找不到该页面" },
                { 403, "访问被拒绝" },
                { 500, "服务器发生意外的错误" }
                //其余状态自行扩展
            };
        }
        public async Task Invoke(HttpContext context)
        {
            bool handled = false;
            try
            {
                await _next(context);   //调用管道执行下一个中间件
            }
            catch (Exception ex)
            {
                handled = true;
                var msg = "全局未处理异常";
                try
                {
                    var result = GlobalExceptionUtil.BuildApiResult(ex);
                    if (result.Code == ResponseCode.G_InternalServerError)
                        result.Code = ResponseCode.G_UnhandledException;
                    await ResponseApiResult(context, result);
                }
                catch (Exception exi)
                {
                    msg = $"全局未处理异常-GlobalExceptionMiddleware处理异常时错，必须处理:{exi.Message}";
                    var result = new ApiResult(ResponseCode.G_UnhandledException, msg);
                    await ResponseApiResult(context, result);
                }
                try
                {
                    // 正常情况下，异常已在ApiResponseFilter中处理，在此处不应该捕获到异常
                    LogUtil.Error(ex, msg);
                }
                catch
                { }
            }
            finally
            {
                if (!handled // 未处理过
                    && context.Items.ContainsKey(ResponseCode.ErrorCodeKey) // 自定义返回Code
                    //&& _exceptionStatusCodeDic.ContainsKey(context.Response.StatusCode) // 非正常StatusCode
                    )
                {
                    string errorCode = null;
                    if (context.Items.ContainsKey(ResponseCode.ErrorCodeKey))
                        errorCode = Convert.ToString(context.Items[ResponseCode.ErrorCodeKey]);
                    string errorMessage = null;
                    if (context.Items.ContainsKey(ResponseCode.ErrorMessageKey))
                        errorMessage = Convert.ToString(context.Items[ResponseCode.ErrorMessageKey]);
                    if (string.IsNullOrEmpty(errorMessage))
                        errorMessage = _exceptionStatusCodeDic[context.Response.StatusCode];
                    await HandleException(context, errorCode, errorMessage, null);
                }
            }
        }
        private async Task HandleException(HttpContext context, string errorCode, string errorMessage, Exception exception)
        {
            var handleType = _option.HandleType;
            if (handleType == ExceptionHandleType.Both)   //根据Url关键字决定异常处理方式
            {
                var requestPath = context.Request.Path;
                handleType = _option.JsonHandleUrlKeys != null && _option.JsonHandleUrlKeys.Count(
                    k => requestPath.StartsWithSegments(k, StringComparison.CurrentCultureIgnoreCase)) > 0 ?
                    ExceptionHandleType.JsonHandle :
                    ExceptionHandleType.PageHandle;
            }

            if (handleType == ExceptionHandleType.JsonHandle)
                await JsonHandle(context, errorCode, errorMessage, exception);
            else
                await PageHandle(context, exception, _option.ErrorHandingPath);
        }


        /// <summary>
        /// 处理方式：返回Json格式
        /// </summary>
        /// <param name="context"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task JsonHandle(HttpContext context, string errorCode, string errorMessage, Exception ex)
        {
            if (string.IsNullOrEmpty(errorCode))
                errorCode = $"{context.Response.StatusCode}";
            var rsp = new ApiResult()
            {
                Success = false,
                Status = context.Response.StatusCode,
                Code = errorCode,
                Message = errorMessage
            };
            await ResponseException(context, rsp, ex);
        }
        private async Task ResponseException(HttpContext context, ApiResult result, Exception ex)
        {
            if (ConfigUtil.Project.ResponseErrorDetail)
            {
                result.Exception = ex;
                if (string.IsNullOrEmpty(result.Message))
                    result.Message = ex?.Message;
            }
            await ResponseApiResult(context, result);
        }

        /// <summary>
        /// 处理方式：跳转网页
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task PageHandle(HttpContext context, Exception ex, PathString path)
        {
            context.Items.Add("Exception", ex);
            var originPath = context.Request.Path;
            context.Request.Path = path;   //设置请求页面为错误跳转页面
            try
            {
                await _next(context);
            }
            catch { }
            finally
            {
                context.Request.Path = originPath;   //恢复原始请求页面
            }
        }

        private async Task ResponseApiResult(HttpContext context, ApiResult result)
        {
            
            var json = SerializerUtil.SerializeJson(result);
            context.Response.Clear();
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(json, Encoding.UTF8);
        }
    }
}
