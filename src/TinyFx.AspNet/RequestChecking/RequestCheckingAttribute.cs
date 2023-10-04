using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Net;

namespace TinyFx.AspNet.RequestChecking
{
    /*
    public class RequestCheckingAttribute : Attribute, IAsyncActionFilter
    {
        private RequestCheckingSection _section;
        public RequestCheckingAttribute()
        {
            _section = ConfigUtil.GetSection<RequestCheckingSection>();
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_section != null && _section.Enabled)
            {
                // 取值
                if (!context.HttpContext.Request.Headers.TryGetValue(_section.HeaderName, out var value))
                    throw new CustomException(ResponseCode.G_Unauthorized, $"无效的xxyy-sign, header:{_section.HeaderName}不存在");
                var sign = Convert.ToString(value);
                if (string.IsNullOrEmpty(sign))
                    throw new CustomException(ResponseCode.G_Unauthorized, $"无效的xxyy-sign, header:{_section.HeaderName}不存在");
                var key = HttpContextEx.GetSessionOrDefault<string>(_section.SessionKey, null);
                if (string.IsNullOrEmpty(key))
                    throw new CustomException(ResponseCode.G_Unauthorized, $"无效的xxyy-sign, key不存在，sessionKey:{_section.SessionKey}");
                var content = await AspNetUtil.GetRawBodyAsync(context.HttpContext.Request);

                //验证
                var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
                var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(content)));
                if (hash != sign)
                    throw new CustomException(ResponseCode.G_Unauthorized, "无效的xxyy-sign，sign错误。");
            }

            await next.Invoke();
        }
    }
    */
}
