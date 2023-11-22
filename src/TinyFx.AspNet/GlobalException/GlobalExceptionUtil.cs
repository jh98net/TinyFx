using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx.AspNet
{
    internal static class GlobalExceptionUtil
    {
        /// <summary>
        /// 保存返回客户端的错误码：HttpContext.Items添加的Key
        /// </summary>
        public const string ERROR_CODE_KEY = "ERROR_CODE";
        public const string ERROR_MESSAGE_KEY = "ERROR_MESSAGE";

        static GlobalExceptionUtil()
        {
        }
        public static ApiResult BuildApiResult(Exception ex, ILogBuilder logger, HttpContext context)
        {
            ApiResult ret;
            logger.AddException(ex);
            // 获取异常链中的 CustomException
            CustomException exc = null;
            if (ex != null&& ex.Message == "Unexpected end of request content.")
            {
                exc = new CustomException(ResponseCode.G_BadRequest, "客户端中断请求");
                if (AspNetUtil.TryGetUnhandledExceptionCode(out var code))
                    exc.Code = code;
            }
            if (exc == null)
                exc = ExceptionUtil.GetException<CustomException>(ex);

            if (exc != null)
            {
                ret = new ApiResult()
                {
                    Success = false,
                    Status = context.Response.StatusCode,
                    Code = exc.Code,
                    Result = exc.Result,
                    TraceId = context.GetTraceId(),
                    Exception = null
                };
                if (string.IsNullOrEmpty(ret.Code) && AspNetUtil.TryGetUnhandledExceptionCode(out var code))
                    ret.Code = code;

                if (ConfigUtil.Project?.ResponseErrorMessage ?? false)
                    ret.Message = exc.Message;
            }
            else
            {
                ret = new ApiResult()
                {
                    Success = false,
                    Status = context.Response.StatusCode,
                    Code = AspNetUtil.TryGetUnhandledExceptionCode(out var code)
                        ? code : ResponseCode.G_InternalServerError,
                    Message = (ConfigUtil.Project?.ResponseErrorMessage ?? false)
                        ? ex.Message : null,
                    Result = null,
                    TraceId = context.GetTraceId(),
                    Exception = AspNetUtil.GetResponseExceptionDetail() ? ex : null
                };
            }
            return ret;
        }
    }
}
