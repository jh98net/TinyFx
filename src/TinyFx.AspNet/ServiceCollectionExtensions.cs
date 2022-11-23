using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using StackExchange.Redis;
using TinyFx;
using TinyFx.AspNet;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Logging;
using TinyFx.Security;

namespace TinyFx
{
    public static class AspNetServiceCollectionExtensions
    {
        public static IServiceCollection AddTinyFxEx(this IServiceCollection services, AspNetType type)
        {
            if ((type & AspNetType.Api) != 0)
            {
                services.AddControllersEx();
                LogUtil.Trace($"注册 AddControllers");
            }
            if ((type & AspNetType.Api) != 0)
            {
                services.AddRazorPages();
                LogUtil.Trace($"注册 AddRazorPages");
            }
            if ((type & AspNetType.ServerBlazor) != 0)
            {
                services.AddServerSideBlazorEx();
                LogUtil.Trace($"注册 AddServerSideBlazor");
            }

            return services
                .AddCorsEx()                    // Cors
                .AddApiVersioningEx()           // ApiVersion
                .AddSwaggerGenEx()              // Swagger
                .AddApiJwtAuthEx()              // Jwt
                .AddRedisSessionEx()            // RedisSession  
                .AddResponseCachingEx()         // ResponseCaching
                .AddResponseCompressionEx()     // ResponseCompression

                .AddOptions()                   // IOptions
                .AddHttpClient()                // IHttpClientFactory
                .AddHttpContextAccessor();      // IHttpContextAccessor
        }
        public static IMvcBuilder AddControllersEx(this IServiceCollection services)
        {
            return services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ApiResponseFilter));
            }).AddJsonOptions(options =>
            {
                SerializerUtil.ConfigJsonSerializerOptions(options.JsonSerializerOptions);
            });
        }
        public static IMvcBuilder AddRazorPagesEx(this IServiceCollection services)
        {
            return services.AddRazorPages(options =>
            {
                //options..Filters.Add(typeof(CustomExceptionFilter));
            }).AddJsonOptions(options =>
            {
                SerializerUtil.ConfigJsonSerializerOptions(options.JsonSerializerOptions);
            });
        }
        public static IServerSideBlazorBuilder AddServerSideBlazorEx(this IServiceCollection services)
        {
            return services.AddServerSideBlazor(opts => 
            { 
            });
        }
        /// <summary>
        /// CORS
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<CorsSection>();
            if (section != null)
            {
                services.AddCors(opts =>
                {
                    foreach (var item in section.Policies)
                    {
                        var policy = item.Value;
                        if (policy.IsDefault)
                            opts.AddDefaultPolicy(GetPolicyBuilder(policy));
                        else
                            opts.AddPolicy(policy.Name, GetPolicyBuilder(policy));
                    }
                });
                LogUtil.Trace($"Cors 配置启动");
            }
            return services;

            Action<CorsPolicyBuilder> GetPolicyBuilder(CorsPolicyElement element)
            {
                return new Action<CorsPolicyBuilder>(builder =>
                {
                    // Origins
                    if (!string.IsNullOrEmpty(element.Origins))
                    {
                        if (element.Origins.Trim() == "*")
                            builder.AllowAnyOrigin();
                        else
                            builder.WithOrigins(element.Origins.Split(';'));
                    }
                    // Methods
                    if (!string.IsNullOrEmpty(element.Methods))
                    {
                        if (element.Methods.Trim() == "*")
                            builder.AllowAnyMethod();
                        else
                            builder.WithMethods(element.Methods.Split(';'));
                    }
                    // Headers
                    if (!string.IsNullOrEmpty(element.Headers))
                    {
                        if (element.Headers.Trim() == "*")
                            builder.AllowAnyHeader();
                        else
                            builder.WithHeaders(element.Headers.Split(';'));
                    }
                });
            }
        }
        /// <summary>
        /// Versioning
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiVersioningEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<ApiVersioningSection>();
            if (section != null)
            {
                services.AddApiVersioning(options =>
                {
                    IApiVersionReader reader = null;
                    switch (section.Mode)
                    {
                        case VersioningMode.QueryString:
                            reader = new QueryStringApiVersionReader();
                            break;
                        case VersioningMode.Header:
                            reader = new HeaderApiVersionReader();
                            break;
                        case VersioningMode.URL:
                            reader = new UrlSegmentApiVersionReader();
                            break;
                        case VersioningMode.MediaType:
                            reader = new MediaTypeApiVersionReader();
                            break;
                        default:
                            reader = new QueryStringApiVersionReader();
                            break;
                    }
                    options.ApiVersionReader = reader;
                    //options.ReportApiVersions = true; //可选，为true时API返回支持的版本信息
                    options.AssumeDefaultVersionWhenUnspecified = true; // 不提供版本时，默认为1.0
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });
                //services.AddVersionedApiExplorer(options =>
                //{
                //    options.GroupNameFormat = "'v'V";
                //    options.AssumeDefaultVersionWhenUnspecified = true;
                //});
                LogUtil.Trace($"ApiVersioning 配置启动");
            }
            return services;
        }

        /// <summary>
        /// Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGenEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<SwaggerSection>();
            if (section != null && section.Enabled)
            {
                services.AddEndpointsApiExplorer();
                // OpenAPI v3 document
                services.AddOpenApiDocument(config =>
                {
                    //config.DocumentName = desc.GroupName;
                    //config.Version = desc.GroupName;
                    //config.ApiGroupNames = new string[] { desc.GroupName };
                    config.UseControllerSummaryAsTagDescription = true;
                    // 允许返回null
                    config.DefaultResponseReferenceTypeNullHandling = NJsonSchema.Generation.ReferenceTypeNullHandling.Null;
                    config.Title = ConfigUtil.Project.ProjectId;
                    config.Description = ConfigUtil.Project.Description;

                    // JWT
                    var apiScheme = new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT", // for documentation purposes (OpenAPI only)
                        Description = "复制JWT Token值到输入框中: {token}"
                    };
                    config.AddSecurity("Bearer", apiScheme);

                    config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
                });
                // Swagger v2 document
                //services.AddSwaggerDocument(config =>
                //{
                //});
                LogUtil.Trace($"Swagger 配置启动");
            }
            return services;
        }

        /// <summary>
        /// Api采用jwt验证的设置。
        /// Configure中添加
        ///     app.UseAuthentication();
        ///     app.UseAuthorization();
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiJwtAuthEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<JwtAuthSection>();
            if (section != null)
            {
                if (string.IsNullOrEmpty(section.SignSecret))
                    throw new Exception("配置文件ApiJwtAuth:SignSecret不能为空");
                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = JwtUtil.GetParameters(section);
                    // 处理jwt事件
                    x.Events = new TinyFxJwtBearerEvents();
                });
                LogUtil.Trace($"JwtAuth 配置启动");
            }

            return services;
        }
        /// <summary>
        /// Session保存在Redis中
        /// Configure中添加
        ///     app.UseSession();
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisSessionEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<SessionToRedisSection>();
            if (section != null && section.Enabled)
            {
                var redisSection = ConfigUtil.GetSection<RedisSection>();
                if (redisSection != null)
                {
                    var defaultRedisName = string.IsNullOrEmpty(section.ConnectionStringName)
                        ? redisSection.DefaultConnectionStringName : section.ConnectionStringName;
                    if (!redisSection.ConnectionStrings.TryGetValue(defaultRedisName, out ConnectionStringElement element))
                        throw new Exception($"配置中Redis:ConnectionStrings:Name 不存在。Name: {defaultRedisName}");
                    var builder = services.AddDataProtection();
                    if (ConfigUtil.Project == null)
                        throw new Exception("配置文件tinyfx:Project配置项没有配置");
                    builder.SetApplicationName(ConfigUtil.Project.ProjectId);
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = element.ConnectionString;
                        options.InstanceName = $"{ConfigUtil.Project.ProjectId}:Session:";
                        if (section.DatabaseIndex != -1)
                            options.ConfigurationOptions.DefaultDatabase = section.DatabaseIndex;
                    });
                    services.AddSession(options =>
                    {
                        options.Cookie.Name = $".{ConfigUtil.Project.ProjectId}.Session";
                        options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                        options.IdleTimeout = TimeSpan.FromMinutes(section.IdleTimeout);
                        options.Cookie.HttpOnly = section.CookieHttpOnly;
                    });
                    LogUtil.Trace($"SessionToRedis 配置启动");
                }
            }
            return services;
        }

        public static IServiceCollection AddResponseCachingEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<ResponseCachingSection>();
            if (section != null && section.Enabled)
            {
                services.AddResponseCaching(option =>
                {
                });
                LogUtil.Trace($"ResponseCaching 配置启动");
            }
            return services;
        }
        public static IServiceCollection AddResponseCompressionEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<ResponseCompressionSection>();
            if (section != null && section.Enabled)
            {
                services.AddResponseCompression(options =>
                {
                    options.Providers.Add<BrotliCompressionProvider>();
                    options.Providers.Add<GzipCompressionProvider>();
                    options.MimeTypes =
                        ResponseCompressionDefaults.MimeTypes.Concat(
                            new[] { "image/svg+xml" });
                });
                LogUtil.Trace($"ResponseCompression 配置启动");
            }
            return services;
        }
    }
}
