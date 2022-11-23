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
            return builder;
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
