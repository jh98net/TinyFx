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
        private static GlobalExceptionSection _option;
        static GlobalExceptionUtil()
        {
            _option = ConfigUtil.GetSection<GlobalExceptionSection>();
        }
        public static ApiResult BuildApiResult(Exception ex)
        {
            ApiResult ret;
            // 获取异常链中的 CustomException
            var exc = ExceptionUtil.GetException<CustomException>(ex);
            if (exc != null)
            {
                ret = new ApiResult()
                {
                    Success = false,
                    Status = 400,// (int)HttpStatusCode.BadRequest,
                    Code = exc.Code,
                    Message = string.IsNullOrEmpty(exc.Message) ? ex.Message : exc.Message,
                    Result = exc.Result,
                    Exception = null
                };
                LogUtil.Info($"[CustomException] code:{exc.Code} message:{exc.Message} result:{exc.Result}");
            }
            else
            {
                ret = new ApiResult()
                {
                    Success = false,
                    Status = 500,//(int)HttpStatusCode.InternalServerError,
                    Code = ResponseCode.G_InternalServerError,
                    Message = ex.Message,
                    Result = null,
                    Exception = ConfigUtil.Project.ResponseErrorDetail ? ex : null
                };
                LogUtil.Error(ex, "未处理异常");
            }
            return ret;
        }
    }
}
