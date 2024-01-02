using Microsoft.AspNetCore.Mvc.Filters;
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
    /// <summary>
    /// Access访问验证Attribute
    /// </summary>
    public class AccessVerifyAttribute : Attribute, IAsyncActionFilter
    {
        public const string HEADER_NAME = "tinyfx-sign";
        private IAccessVerifyService _verifySvc;
        public AccessVerifyAttribute()
        {
            _verifySvc = DIUtil.GetService<IAccessVerifyService>();
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var section = ConfigUtil.GetSection<AccessVerifySection>();
            if (section != null && section.Enabled && _verifySvc != null)
            {
                if (!context.HttpContext.Request.Headers.TryGetValue(HEADER_NAME, out var value))
                    throw new CustomException(GResponseCodes.G_UNAUTHORIZED, $"header不存在: {HEADER_NAME}");
                var data = Convert.ToString(value)?.Split('|');
                if (data == null || data.Length != 2)
                    throw new CustomException(GResponseCodes.G_UNAUTHORIZED, $"header {HEADER_NAME} 值格式错误: {value}");

                var sourceKey = data[0];
                var sign = data[1];
                var content = await AspNetUtil.GetRawBodyAsync(context.HttpContext.Request);
                content = string.IsNullOrEmpty(content) ? "null" : content;

                var isValid = _verifySvc.VerifyAccessKey(sourceKey, content, sign);
                if (!isValid)
                {
                    var msg = $"header {HEADER_NAME} 值无效: {value}";
                    LogUtil.GetContextLogger()
                        .SetLevel(Microsoft.Extensions.Logging.LogLevel.Warning)
                        .AddMessage(msg)
                        .AddField("BothKeyVerify.HeaderValue", data)
                        .AddField("BothKeyVerify.SourceKey", sourceKey)
                        .AddField("BothKeyVerify.Sign", sign)
                        .AddField("BothKeyVerify.Content", content);

                    throw new CustomException(GResponseCodes.G_UNAUTHORIZED, msg);
                }
            }
            await next.Invoke();
        }
    }
}
