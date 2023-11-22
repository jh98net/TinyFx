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
using Microsoft.Diagnostics.NETCore.Client;
using TinyFx.IO;
using System.IO;

namespace TinyFx
{
    public static class AspNetHost
    {
        public static WebApplicationBuilder CreateBuilder(string[] args = null)
        {
            LogUtil.CreateBootstrapLogger();
            var builder = WebApplication.CreateBuilder(args);
            // 设置启动Serilog
            builder.Host.UseTinyFx();
            builder.Host.UseSerilogEx();
            builder.Host.UseAutoMapperEx();
            builder.Host.UseRedisEx();
            builder.Host.UseSqlSugarEx();
            builder.Host.UseRabbitMQEx();
            builder.Host.UseIDGenerator();
            builder.Host.UseDbCachingEx();
            builder.Host.UseOAuthEx();
            return builder;
        }
        internal static string MapEnvPath()
        {
            var processInfos = DiagnosticsClient.GetPublishedProcesses()
                       .Select(Process.GetProcessById)
                       .Where(process => process != null)
                       .Select(o => { return $"id:{o.Id} name:{o.ProcessName} threads:{o.Threads.Count}"; })
                       .ToList();
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
                { "AppContext.BaseDirectory", AppContext.BaseDirectory },
                { "ProcessInfos", processInfos },
                { "分配的内存总量GC.GetTotalMemory(false)-(gc-heap-size)", GC.GetTotalMemory(false) },
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

        // ASP.NET Core Identity共享身份验证cookie
        internal static string GetDataProtectionRedisConnectionString(string connectionStringName)
        {
            var redisSection = ConfigUtil.GetSection<RedisSection>();
            if (redisSection == null)
                return null;
            connectionStringName ??= redisSection.DefaultConnectionStringName;
            if (redisSection.ConnectionStrings.TryGetValue(connectionStringName, out var element))
                return element.ConnectionString;
            var ret = redisSection.ConnectionStrings.FirstOrDefault();
            return ret.Value.ConnectionString;
        }

        internal static Task<string> MapDumpPath(DumpType dtype = DumpType.Full)
        {
            var processId = DiagnosticsClient.GetPublishedProcesses()
                .Select(Process.GetProcessById)
                .Where(process => process != null)
                .Select(x => x.Id).ToList().FirstOrDefault();
            if (processId == 0)
                return null;
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section == null || string.IsNullOrEmpty(section.DumpPath))
                return null;
            if (!Directory.Exists(section.DumpPath))
                Directory.CreateDirectory(section.DumpPath);
            var file = Path.Combine(section.DumpPath, $"{ConfigUtil.Project.ProjectId}.{dtype.ToString()}.{DateTime.Now.ToString("yyyyMMddHHmmss")}.dmp");
            var client = new DiagnosticsClient(processId);
            client.WriteDumpAsync(dtype, file, false, CancellationToken.None);
            return Task.FromResult(file);
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
