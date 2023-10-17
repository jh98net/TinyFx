using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyFx.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using TinyFx;
using System.Web;
using TinyFx.AspNet;
using Microsoft.AspNetCore.HttpOverrides;
using TinyFx.Extensions.Serilog;
using Serilog;
using TinyFx.Logging;
using Microsoft.AspNetCore.Cors.Infrastructure;
using TinyFx.AspNet.Auth.Cors;
using static System.Collections.Specialized.BitVector32;
using Serilog.Events;
using TinyFx.AspNet.RequestLogging;

namespace TinyFx
{
    public static class AspNetWebApplicationExtensions
    {
        public static WebApplication UseAspNetEx(this WebApplication app)
        {
            app.UseTinyFxEx();
            // 中间件顺序
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-7.0#middleware-order
            app.UseEnableBufferingEx();
            app.UseForwardedHeaders();
            app.UseResponseCompressionEx();
            //app.UseSerilogRequestLoggingEx();
            app.UseRequestLoggingEx();
            app.UseGlobalExceptionEx();
            app.UsePathBaseEx();
            app.UseCookiePolicyEx();
            app.UseRouting();
            //app.UseRateLimiter(); .net 7
            app.UseCorsEx();
            app.UseJwtAuthEx();
            app.UseSessionEx();
            app.UseResponseCachingEx(); // 必须放在UseCors之后
            app.UseSwaggerEx();
            app.UseInternalMap();

            TinyFxHost.RegisterOnStarted(() => 
            {
                LogUtil.Info("ProjectId: {ProjectId} Environment: {EnvironmentString} URL: {Urls}"
                    , ConfigUtil.Project?.ProjectId
                    , ConfigUtil.EnvironmentString
                    , app.Urls);
                return Task.CompletedTask;
            });
            return app;
        }

        public static WebApplication UseEnableBufferingEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section != null && section.UseRequestBuffering)
            {
                app.Use((context, next) =>
                {
                    context.Request.EnableBuffering();
                    return next(context);
                });
            }
            return app;
        }
        public static WebApplication UseResponseCompressionEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section != null && section.UseResponseCompression)
            {
                app.UseResponseCompression();
            }
            return app;
        }
        public static WebApplication UseRequestLoggingEx(this WebApplication app)
        {
            app.UseMiddleware<RequestLoggingMiddleware>();
            return app;
        }
        public static WebApplication UseGlobalExceptionEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section?.UseApiActionResultFilter ?? false)
            {
                app.UseMiddleware<GlobalExceptionMiddleware>();
            }
            return app;
        }
        public static WebApplication UsePathBaseEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section != null && !string.IsNullOrEmpty(section.PathBase))
            {
                app.UsePathBase(section.PathBase);
            }
            return app;
        }
        public static WebApplication UseCookiePolicyEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<SessionAndCookieSection>();
            if (section != null && section.UseCookieIdentity)
            {
                app.UseCookiePolicy();
            }
            return app;
        }
        public static WebApplication UseCorsEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<CorsSection>();
            if (section != null && section.UseCors.Enabled)
            {
                app.UseCors();
                if (section.UseCors.EnabledReferer)
                    app.UseMiddleware<RefererMiddleware>();
            }
            return app;
        }
        public static WebApplication UseJwtAuthEx(this WebApplication app)
        {
            /*
            var section = ConfigUtil.GetSection<JwtAuthSection>();
            if (section != null)
            {
                app.UseMiddleware<JwtMiddleware>();
            }*/
            return app;
        }
        public static WebApplication UseSessionEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<SessionAndCookieSection>();
            if (section != null && section.UseSession)
            {
                app.UseSession();
            }
            return app;
        }
        public static WebApplication UseResponseCachingEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<ResponseCachingSection>();
            if (section != null && section.Enabled)
            {
                app.UseResponseCaching();
            }
            return app;
        }
        public static WebApplication UseSwaggerEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section != null && section.Swagger != null && section.Swagger.Enabled)
            {
                app.UseSwagger(opts =>
                {
                });
                var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
                app.UseSwaggerUI(options =>
                {
                    if (provider != null)
                    {
                        //var list = provider.ApiVersionDescriptions.Reverse();
                        var pathBase = !string.IsNullOrEmpty(section.PathBase)
                            ? $"/{section.PathBase.Trim().TrimStart('/')}" : null;
                        var list = provider.ApiVersionDescriptions;
                        foreach (var description in list)
                        {
                            options.SwaggerEndpoint($"{pathBase}/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                        }
                    }
                });
            }
            return app;
        }
        public static WebApplication UseInternalMap(this WebApplication app)
        {
            app.MapHealthChecks("/healthz");
            app.MapGet("/env", () => AspNetHost.MapEnvPath());
            return app;
        }

        //public static WebApplication UseSerilogRequestLoggingEx(this WebApplication app)
        //{
        //    if (SerilogUtil.ExistSection && SerilogUtil.GetCustomProperty<bool>("RequestLogging"))
        //    {
        //        app.UseSerilogRequestLogging((opts =>
        //        {
        //            opts.GetLevel = (ctx, _, ex) =>
        //            {
        //                if (ex == null)
        //                {
        //                    if (ctx.Response.StatusCode <= 499)
        //                    {
        //                        return LogEventLevel.Information;
        //                    }

        //                    return LogEventLevel.Error;
        //                }

        //                return LogEventLevel.Error;
        //            };
        //            opts.EnrichDiagnosticContext = async (diagnostic, httpContext) =>
        //            {
        //                diagnostic.Set("request.body", await httpContext.Request.GetRawBodyAsync());
        //                diagnostic.Set("response.body", await httpContext.Request.GetRawBodyAsync());
        //            };
        //        }));
        //    }
        //    return app;
        //}
    }
}
