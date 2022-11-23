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

namespace TinyFx
{
    public static class AspNetApplicationBuilderExtensions
    {
        public static WebApplication UseTinyFxEx(this WebApplication app)
        {
            app.UseGlobalExceptionEx();
            app.UseJwtAuthEx();
            app.UseCorsEx();
            app.UseSwaggerEx();
            app.UseRedisSessionEx();
            app.UseResponseCachingEx();
            app.UseResponseCompressionEx();
            app.UseStaticHttpContextEx();
            app.UseSerilogRequestLoggingEx();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.Lifetime.ApplicationStarted.Register(() => {
                LogUtil.Debug("ProjectId: {ProjectId} Environment: {EnvironmentString} URL: {Urls}"
                    , ConfigUtil.Project?.ProjectId
                    , ConfigUtil.EnvironmentString
                    , app.Urls);
            });
            return app;
        }
        /// <summary>
        /// CORS
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplication UseCorsEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<CorsSection>();
            if (section != null && section.UseCors.Enabled)
            {
                var policy = section.UseCors.PolicyName;
                if (string.IsNullOrEmpty(policy))
                    app.UseCors();
                else
                    app.UseCors(policy);

            }
            return app;
        }

        /// <summary>
        /// Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplication UseSwaggerEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<SwaggerSection>();
            if (section != null)
            {
                // /swagger/v1/swagger.json
                app.UseOpenApi(config =>
                {
                    config.PostProcess = (document, http) =>
                    {
                        document.Info.Title += $" -- [{ConfigUtil.EnvironmentString}]";
                    };
                });
                // /swagger UI
                app.UseSwaggerUi3(config =>
                {
                });
                //app.UseReDoc(config => {
                //    config.Path = "/recdoc";
                //});
            }
            return app;
        }
        /// <summary>
        /// 支持Session保存到Redis
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplication UseRedisSessionEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<SessionToRedisSection>();
            if (section != null && section.Enabled)
            {
                app.UseCookiePolicy();
                app.UseSession();
            }
            return app;
        }

        /// <summary>
        /// 使用全局异常处理
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplication UseGlobalExceptionEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<GlobalExceptionSection>();
            if (section != null)
            {
                app.UseMiddleware<GlobalExceptionMiddleware>();
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

        public static WebApplication UseResponseCachingEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<ResponseCachingSection>();
            if (section != null && section.Enabled)
            {
                app.Use(async (context, next) =>
                {
                    context.Response.GetTypedHeaders().CacheControl =
                        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                        {
                            Public = true,
                            MaxAge = TimeSpan.FromSeconds(section.MaxAge)
                        };
                    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                        new string[] { "Accept-Encoding" };

                    await next();
                });
            }
            return app;
        }

        public static WebApplication UseResponseCompressionEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<ResponseCompressionSection>();
            if (section != null && section.Enabled)
            {
                app.UseResponseCompression();
            }
            return app;
        }
        public static WebApplication UseStaticHttpContextEx(this WebApplication app)
        {
            var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
            HttpContextEx.Configure(httpContextAccessor);
            return app;
        }

        public static WebApplication UseSerilogRequestLoggingEx(this WebApplication app)
        {
            if (SerilogUtil.ExistSection && SerilogUtil.GetCustomProperty<bool>("RequestLogging"))
            {
                app.UseSerilogRequestLogging();
            }
            return app;
        }
    }
}
