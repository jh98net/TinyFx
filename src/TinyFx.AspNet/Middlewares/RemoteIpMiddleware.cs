using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.AspNetCore
{
    public class RemoteIpMiddleware
    {
        private readonly RequestDelegate next;

        public RemoteIpMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            LogContext.PushProperty("RemoteIp", context.Connection.RemoteIpAddress);
            return next(context);
        }
    }
}
