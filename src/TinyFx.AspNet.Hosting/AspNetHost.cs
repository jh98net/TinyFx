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
using TinyFx.Net;
using TinyFx.Extensions.Serilog;
using System.Reflection;
using TinyFx.Text;

namespace TinyFx
{
    public static class AspNetHost
    {
        public static WebApplicationBuilder CreateBuilder(string envString = null, string[] args = null)
        {
            ConfigUtil.HostType = TinyFxHostType.AspNet;
            SerilogUtil.CreateBootstrapLogger();
            var builder = WebApplication.CreateBuilder(args);
            // 设置启动Serilog
            builder.Host.AddTinyFx(envString)
                .AddSerilogEx()
                .AddAutoMapperEx()
                .AddRedisEx()
                .AddSqlSugarEx()
                .AddRabbitMQEx()
                .AddSnowflakeIdEx()
                .AddDbCachingEx()
                .AddOAuthEx();
            return builder;
        }
        internal static async Task<string> MapEnvPath()
        {
            var processInfos = DiagnosticsClient.GetPublishedProcesses()
                       .Select(Process.GetProcessById)
                       .Where(process => process != null)
                       .Select(o => { return $"id:{o.Id} name:{o.ProcessName} threads:{o.Threads.Count}"; })
                       .ToList();
            var lastBuildTime = File.GetLastWriteTimeUtc(Assembly.GetEntryAssembly().Location).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
            var startTime = ((long)ObjectId.Parse(ConfigUtil.ServiceGuid).Timestamp).TimestampToUtcDateTime().ToFormatString();
            string outputIp = null;
            try
            {
                outputIp = (await HttpClientExFactory.CreateClientEx().CreateAgent().AddUrl("http://api.ip.sb/ip").GetStringAsync()).ResultString.TrimEnd('\n');
            }
            catch { }
            var dict = new Dictionary<string, object>
            {
                { "服务启动UTC时间", startTime },
                { "ConfigUtil.ServiceId", ConfigUtil.ServiceId },
                { "ConfigUtil.ServiceUrl", ConfigUtil.ServiceUrl },
                { "ConfigUtil.ServiceGuid", ConfigUtil.ServiceGuid },
                { "ConfigUtil.EnvironmentString", ConfigUtil.EnvironmentString },
                { "ConfigUtil.Environment", ConfigUtil.Environment.ToString() },
                { "header:Host", HttpContextEx.GetHeaderValue("Host") },
                { "header:X-Forwarded-Proto",HttpContextEx.GetHeaderValue("X-Forwarded-Proto")},
                { "header:Referer", HttpContextEx.GetHeaderValue("Referer") },
                { "header:X-Real_IP", HttpContextEx.GetHeaderValue("X-Real_IP") },
                { "header:X-Forwarded-For", HttpContextEx.GetHeaderValue("X-Forwarded-For") },
                { "AspNetUtil.GetRequestBaseUrl()", AspNetUtil.GetRequestBaseUrl() },
                { "AspNetUtil.GetRefererUrl()", AspNetUtil.GetRefererUrl() },
                { "AspNetUtil.GetRemoteIpString()", AspNetUtil.GetRemoteIpString() },
                { "AppContext.BaseDirectory", AppContext.BaseDirectory },
                { "DiagnosticsClient.GetPublishedProcesses()", processInfos },
                { "分配的内存总量: GC.GetTotalMemory(false)-(gc-heap-size)", GC.GetTotalMemory(false) },
                { "GCSettings.IsServerGC", GCSettings.IsServerGC },
                { "服务器本机IP", NetUtil.GetLocalIPs() },
                { "服务器出口IP", outputIp },
                { "代码最后一次编译时间", lastBuildTime },
            };
            ThreadPool.GetAvailableThreads(out var worker, out var completion);
            dict.Add("ThreadPool.GetAvailableThreads()", $"workerThreads:{worker} completionPortThreads:{completion}");
            
            dict.Add("headers总量", HttpContextEx.Request.Headers.Count);
            foreach (var header in HttpContextEx.Request.Headers)
            {
                dict.Add($"headers.{header.Key}", header.Value);
            }

            return SerializerUtil.SerializeJsonNet(dict);
        }

        /// <summary>
        /// 崩溃自动dump，环境变量
        /// COMPlus_DbgEnableMiniDump=1
        /// DOTNET_DbgMiniDumpType=2
        /// COMPlus_DbgMiniDumpName=./dumps/crash-%p-%e-%h-%t.dmp
        /// COMPlus_EnableCrashReport = 1
        /// </summary>
        /// <param name="dtype"></param>
        /// <returns></returns>
        internal static Task<string> MapDumpPath(DumpType dtype)
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
            var file = Path.Combine(section.DumpPath, $"{ConfigUtil.Project.ProjectId}.{dtype.ToString()}.{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.dmp");
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
