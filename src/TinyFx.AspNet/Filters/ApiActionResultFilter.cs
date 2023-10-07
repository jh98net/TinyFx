using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Reflection;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx.AspNet
{
    public class ApiActionResultFilter : Attribute, IActionFilter
    {
        private static ConcurrentDictionary<Type, bool> _ignoreControllerDic = new ConcurrentDictionary<Type, bool>();
        private static ConcurrentDictionary<string, bool> _ignoreActionDic = new ConcurrentDictionary<string, bool>();
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // IgnoreActionFilterAttribute
            var controllerType = context.Controller.GetType();
            var isIgnore = _ignoreControllerDic.GetOrAdd(controllerType, (t) =>
            {
                return t.GetCustomAttributes<IgnoreActionFilterAttribute>().FirstOrDefault() != null;
            });
            if (!isIgnore)
            {
                var actionId = context.ActionDescriptor.Id;
                isIgnore = _ignoreActionDic.GetOrAdd(actionId, (_) =>
                {
                    var method = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo;
                    return method.GetCustomAttributes<IgnoreActionFilterAttribute>().FirstOrDefault() != null;
                });
            }
            if (isIgnore)
                context.HttpContext.Items.Add(IgnoreActionFilterAttribute.ITEM_NAME, true);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (IgnoreActionFilterAttribute.CheckIgnore(context.HttpContext))
                return;

            if (context.Exception == null)
            {
                var statusCode = context.HttpContext.Response.StatusCode;
                //只处理2xx
                if (NetUtil.IsSuccessStatusCode(statusCode))
                {
                    if (context.Result is EmptyResult || context.Result is OkResult)
                    {
                        context.Result = BuildResult(statusCode, null, context.HttpContext);
                        return;
                    }
                    if (context.Result is IStatusCodeActionResult isc)
                        statusCode = isc.StatusCode.HasValue ? isc.StatusCode.Value : statusCode;

                    if (context.Result is ObjectResult objr)
                    {
                        if (objr.Value == null)
                        {
                            context.Result = BuildResult(statusCode, null, context.HttpContext);
                            return;
                        }
                        if (objr.Value is ApiResultBase)
                            return;
                        context.Result = BuildResult(statusCode, objr.Value, context.HttpContext);
                        return;
                    }
                }
            }
        }
        private IActionResult BuildResult(int statusCode, object value, HttpContext context)
        {
            var ret = new ApiResult();
            ret.Success = true;
            ret.Status = statusCode;
            ret.Result = value;
            if (AspNetUtil.TryGetSuccessCode(out var code))
                ret.Code = code;
            ret.TraceId = context.GetTraceId();

            var logger = context.RequestServices?.GetService<ILogBuilder>();
            if (logger != null && logger.LogResponseBody)
                logger.AddField("Response.Body", SerializerUtil.SerializeJson(ret));
            return new ObjectResult(ret);
        }
    }
}
