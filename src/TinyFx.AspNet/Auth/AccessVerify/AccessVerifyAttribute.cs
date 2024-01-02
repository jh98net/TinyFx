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
                await _verifySvc.VerifyAccessKeyByHeader(context.HttpContext);
            }
            await next.Invoke();
        }
    }
}
