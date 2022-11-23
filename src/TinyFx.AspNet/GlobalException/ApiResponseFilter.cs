using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TinyFx.Net;

namespace TinyFx.AspNet
{
    public class ApiResponseFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
        private IActionResult BuildResult(int statusCode, object value, string code=null)
        {
            var ret = new ApiResult();
            ret.Status = statusCode;
            ret.Result = value;
            ret.Code = code;
            return new ObjectResult(ret);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                var statusCode = context.HttpContext.Response.StatusCode;
                //只处理2xx
                if (NetUtil.IsSuccessStatusCode(statusCode))
                {
                    if (context.Result is EmptyResult || context.Result is OkResult)
                    {
                        context.Result = BuildResult(statusCode, null);
                        return;
                    }
                    if(context.Result is IStatusCodeActionResult isc)
                        statusCode = isc.StatusCode.HasValue ? isc.StatusCode.Value : statusCode;

                    if (context.Result is ObjectResult objr)
                    {
                        if (objr.Value == null)
                        {
                            context.Result = BuildResult(statusCode, null);
                            return;
                        }
                        if (objr.Value is ApiResultBase)
                            return;
                        context.Result = BuildResult(statusCode, objr.Value);
                        return;
                    }
                }
            }
            else
            {
                // CustomException => StatusCode=400 未处理 => StatusCode=500
                if (!context.ExceptionHandled)
                {
                    var result = GlobalExceptionUtil.BuildApiResult(context.Exception);
                    context.Result = new ObjectResult(result);
                    //context.HttpContext.Response.StatusCode = result.Status;
                    // 设置为true，表示异常已经被处理了
                    context.Exception = null;
                    context.ExceptionHandled = true;
                }
            }
        }
    }

}
