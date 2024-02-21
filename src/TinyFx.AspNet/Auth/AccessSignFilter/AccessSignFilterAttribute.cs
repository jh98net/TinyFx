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
    public class AccessSignFilterAttribute : Attribute, IAsyncActionFilter
    {
        private IAccessSignFilterService _verifySvc;
        public AccessSignFilterAttribute()
        {
            _verifySvc = DIUtil.GetService<IAccessSignFilterService>();
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var section = ConfigUtil.GetSection<AccessSignFilterSection>();
            if (section != null && section.Enabled && _verifySvc != null)
            {
                await _verifySvc.VerifyAccessKeyByHeader(context.HttpContext);
            }
            await next.Invoke();
        }
    }
}
