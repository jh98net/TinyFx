using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.AspNetCore
{
    public class UserNameMiddleware
    {
        private readonly RequestDelegate next;

        public UserNameMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            LogContext.PushProperty("UserName", context?.User?.Identity?.Name);

            return next(context);
        }
    }
}
