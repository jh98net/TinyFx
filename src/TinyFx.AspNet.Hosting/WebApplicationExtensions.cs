using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.ServiceModel;
using TinyFx.AspNet;
using TinyFx.AspNet.Auth.Cors;
using TinyFx.AspNet.Hosting;
using TinyFx.AspNet.RequestLogging;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Reflection;

namespace TinyFx
{
    public static class AspNetWebApplicationExtensions
    {
        public static WebApplication UseAspNetEx(this WebApplication app)
        {
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
            app.UseGrpcEx();
            //
            TinyFxHostingStartupLoader.Instance.Configure(app);

            app.Lifetime.ApplicationStarted.Register(() =>
            {
                app.UseTinyFx(serviceProvider =>
                {
                    var ihttp = serviceProvider?.GetService<IHttpContextAccessor>();
                    return (ihttp != null && ihttp.HttpContext != null)
                        ? ihttp.HttpContext.RequestServices
                        : null;
                });
                LogUtil.Warning("===> 【AspNet服务已启动】 ProjectId:{ProjectId} Env:{EnvironmentName}({EnvironmentString}) URL:{Urls} PathBase:{PathBase} ServiceId:{ServiceId}"
                    , ConfigUtil.Project?.ProjectId
                    , ConfigUtil.Environment.Type
                    , ConfigUtil.Environment.Name
                    , ConfigUtil.Service.ServiceUrl
                    , ConfigUtil.GetSection<AspNetSection>()?.PathBase
                    , ConfigUtil.Service.ServiceId);
            });
            app.Lifetime.ApplicationStopped.Register(() =>
            {
                LogUtil.Warning("===> 【AspNet服务已停止】 ProjectId:{ProjectId} Env:{EnvironmentName}({EnvironmentString}) URL:{Urls} PathBase:{PathBase} ServiceId:{ServiceId}"
                    , ConfigUtil.Project?.ProjectId
                    , ConfigUtil.Environment.Type
                    , ConfigUtil.Environment.Name
                    , ConfigUtil.Service.ServiceUrl
                    , ConfigUtil.GetSection<AspNetSection>()?.PathBase
                    , ConfigUtil.Service.ServiceId);
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
            if (section == null || string.IsNullOrEmpty(section.PathBase))
                return app;

            app.UsePathBase(section.PathBase);
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
                //
                section.PoliciesProvider?.SetAutoRefresh();
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
                app.UseSwaggerUI(opts =>
                {
                    var pathBase = !string.IsNullOrEmpty(section.PathBase)
                            ? $"/{section.PathBase.Trim().TrimStart('/')}" : null;
                    var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
                    if (provider != null)
                    {
                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            //var path = $"/swagger/{description.GroupName}/swagger.json";
                            var path = $"{pathBase}/swagger/{description.GroupName}/swagger.json";
                            opts.SwaggerEndpoint(path, description.GroupName.ToUpperInvariant());
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
            app.MapGet("/dump", (DumpType? t) => AspNetHost.MapDumpPath(t ?? DumpType.WithHeap));
            return app;
        }
        public static WebApplication UseGrpcEx(this WebApplication app)
        {
            var section = ConfigUtil.GetSection<GrpcSection>();
            if (section != null && section.Enabled)
            {
                var grpcTypes = new List<Type>();
                if (section.Assemblies != null && section.Assemblies.Count > 0)
                {
                    section.Assemblies.ForEach(x =>
                    {
                        var ams = DIUtil.GetService<IAssemblyContainer>().GetAssembly(x, "加载GRPC的Assemblies");
                        grpcTypes.AddRange(GetGrpcTypes(ams));
                    });
                }
                else
                {
                    grpcTypes.AddRange(GetGrpcTypes(Assembly.GetEntryAssembly()));
                    grpcTypes.AddRange(GetGrpcTypes(Assembly.GetExecutingAssembly()));
                }
                // 注册
                Type invokeType = typeof(GrpcEndpointRouteBuilderExtensions);
                grpcTypes.ForEach(genericType =>
                {
                    // app.MapGrpcService<GreeterService>();
                    ReflectionUtil.InvokeStaticGenericMethod(invokeType, "MapGrpcService", genericType, app);
                });
            }
            return app;
        }
        private static List<Type> GetGrpcTypes(Assembly? assembly)
        {
            var ret = new List<Type>();
            if (assembly != null)
            {
                foreach (var type in assembly.GetExportedTypes())
                {
                    if (!type.IsClass)
                        continue;
                    var itypes = type.GetInterfaces();
                    if (itypes.Length == 0)
                        continue;
                    foreach (var itype in itypes)
                    {
                        var attr = itype.GetCustomAttribute<ServiceContractAttribute>();
                        if (attr != null)
                        {
                            ret.Add(type);
                            break;
                        }
                    }
                }
            }
            return ret;
        }
    }
}
