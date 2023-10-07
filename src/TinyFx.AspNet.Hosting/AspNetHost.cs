using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Extensions.Logging;
using Serilog;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using TinyFx.Logging;
using TinyFx.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Diagnostics;
using System.Runtime;
using TinyFx.AspNet;

namespace TinyFx
{
    public static class AspNetHost
    {
        public static WebApplicationBuilder CreateBuilder(string[] args = null)
        {
            var builder = WebApplication.CreateBuilder(args);
            // 设置启动Serilog
            builder.Host.UseTinyFx();
            builder.Host.UseSerilogEx();
            builder.Host.UseAutoMapperEx();
            builder.Host.UseRedisEx();
            builder.Host.UseRabbitMQEx();
            builder.Host.UseIDGenerator();
            return builder;
        }
        internal static string MapEnvPath()
        {
            var dict = new Dictionary<string, object>
            {
                { "ConfigUtil.EnvironmentString", ConfigUtil.EnvironmentString },
                { "header:Host", HttpContextEx.GetHeaderValue("Host") },
                { "header:X-Forwarded-Proto",HttpContextEx.GetHeaderValue("X-Forwarded-Proto")},
                { "header:Referer", HttpContextEx.GetHeaderValue("Referer") },
                { "header:X-Real_IP", HttpContextEx.GetHeaderValue("X-Real_IP") },
                { "header:X-Forwarded-For", HttpContextEx.GetHeaderValue("X-Forwarded-For") },
                { "AspNetUtil.GetRequestBaseUrl()", AspNetUtil.GetRequestBaseUrl() },
                { "AspNetUtil.GetRefererUrl()", AspNetUtil.GetRefererUrl() },
                { "AspNetUtil.GetRemoteIpString()", AspNetUtil.GetRemoteIpString() },
                { "Process.GetCurrentProcess().Threads.Count", Process.GetCurrentProcess().Threads.Count },
                { "GCSettings.IsServerGC", GCSettings.IsServerGC },
                { "header总量", HttpContextEx.Request.Headers.Count },
            };
            foreach (var header in HttpContextEx.Request.Headers)
            {
                dict.Add($"headers.{header.Key}", header.Value);
            }
            ThreadPool.GetAvailableThreads(out var worker, out var completion);
            dict.Add("ThreadPool.GetAvailableThreads()", $"workerThreads:{worker} completionPortThreads:{completion}");

            return SerializerUtil.SerializeJsonNet(dict);
        }
    }

    [Flags]
    public enum AspNetType
    {
        Api = 1,
        Razor = 2,
        ServerBlazor = 4,
    }
}
