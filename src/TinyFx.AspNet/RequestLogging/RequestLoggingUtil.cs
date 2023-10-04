using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.AspNet.RequestLogging
{
    internal static class RequestLoggingUtil
    {
        //private const string REQUEST_LOGGING_RULE = "REQUEST_LOGGING_RULE";
        //public static void SetRule(HttpContext context, RequestLoggingRule rule)
        //{
        //    context.Items.TryAdd(REQUEST_LOGGING_RULE, rule);
        //}
        //public static RequestLoggingRule GetRule(HttpContext context)
        //{
        //    if (!context.Items.TryGetValue(REQUEST_LOGGING_RULE, out var v))
        //        return null;
        //    return (RequestLoggingRule)v;
        //}
    }
}
