using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Nacos.AspNetCore.V2;
using Nacos.V2.DependencyInjection;
using System.IO.Compression;
using System.Runtime.Loader;
using TinyFx.AspNet;
using TinyFx.AspNet.Common;
using TinyFx.AspNet.Filters;
using TinyFx.AspNet.Hosting;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Logging;
using TinyFx.Security;

namespace TinyFx
{
    public static class AspNetWebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAspNetEx(this WebApplicationBuilder builder, AspNetType type = AspNetType.Api)
        {
            // Kestrel
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section != null)
            {
                builder.WebHost.ConfigureKestrel(opts =>
                {
                    if (section.RequestBytesPerSecond > 0 && section.RequestPeriodSecond > 0)
                    {
                        var bytesPerSecond = section.RequestBytesPerSecond;
                        var gracePeriod = TimeSpan.FromSeconds(section.RequestPeriodSecond);
                        opts.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond, gracePeriod);
                    }
                    else
                    {
                        opts.Limits.MinRequestBodyDataRate = null;
                    }
                });
            }
            AddAspNetExDetail(builder.Services, type);
            //
            TinyFxHostingStartupLoader.Instance.ConfigureServices(builder);
            return builder;
        }
        private static IServiceCollection AddAspNetExDetail(this IServiceCollection services, AspNetType type)
        {
            if ((type & AspNetType.Api) != 0)
            {
                services.AddControllersEx()
                    .AddDynamicApi();
                LogUtil.Info($"注册 AddControllers");
            }
            if ((type & AspNetType.Razor) != 0)
            {
                services.AddRazorPages();
                LogUtil.Info($"注册 AddRazorPages");
            }
            if ((type & AspNetType.ServerBlazor) != 0)
            {
                services.AddServerSideBlazorEx();
                LogUtil.Info($"注册 AddServerSideBlazor");
            }
            services.AddHealthChecks();         // health
            return services
                .AddRequestLoggingEx()          // LogBuilder
                .AddNacosAspNetEx()
                .AddCorsEx()                    // Cors
                .AddApiVersioningEx()           // ApiVersion
                .AddSwaggerGenEx()              // Swagger
                .AddApiJwtAuthEx()              // Jwt
                .AddSessionAndCookieEx()        // SessionOrCookie 
                .AddResponseCachingEx()         // ResponseCaching
                .AddResponseCompressionEx()     // ResponseCompression
                .AddForwardedHeaders()          // ForwardedHeaders

                .AddOptions()                   // IOptions
                .AddHttpClient()                // IHttpClientFactory
                .AddHttpContextAccessor();//.AddOAuth();      // IHttpContextAccessor
        }
        public static IServiceCollection AddRequestLoggingEx(this IServiceCollection services)
        {
            services.AddScoped<ILogBuilder>((_) =>
            {
                var ret = new LogBuilder("ASPNET_CONTEXT");
                ret.IsContext = true;
                return ret;
            });
            return services;
        }
        public static IServiceCollection AddNacosAspNetEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<NacosSection>();
            if (section != null && section.Enabled && !string.IsNullOrEmpty(section.ServiceName))
            {
                if (section.ServiceName != ConfigUtil.Project.ProjectId)
                    LogUtil.Warning($"Nacose ServiceName 和 ProjectId 不相同。ServiceName: {section.ServiceName} ProjectId: {ConfigUtil.Project.ProjectId}");
                services.Configure<NacosAspNetOptions>(ConfigUtil.Configuration.GetSection("Nacos"));
                services.AddNacosV2Naming(ConfigUtil.Configuration, sectionName: "Nacos");
                services.AddHostedService<RegSvcBgTask>();
            }
            return services;
        }
        public static IMvcBuilder AddControllersEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            return services.AddControllers(options =>
            {
                if (section == null || section.UseApiActionResultFilter)
                    options.Filters.Add(typeof(ApiActionResultFilter));
                if (section == null || section.UseModelStateFilter)
                    options.Filters.Add(typeof(ValidateModelFilter));

                // 等同设置Nullable=false
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                // 设置缓存
                var rcSection = ConfigUtil.GetSection<ResponseCachingSection>();
                if (rcSection != null && rcSection.Enabled)
                {
                    if (rcSection.CacheProfiles?.Count > 0)
                    {
                        foreach (var profile in rcSection.CacheProfiles)
                        {
                            options.CacheProfiles.Add(profile.Key, profile.Value);
                        }
                    }
                    LogUtil.Trace($"ResponseCaching 配置完成");
                }
            }).AddJsonOptions(options =>
            {
                SerializerUtil.ConfigJsonSerializerOptions(options.JsonSerializerOptions);
            }).ConfigureApiBehaviorOptions(options =>
            {
                // 禁用[ApiController]的自动 400 响应
                if (section != null && section.UseModelStateFilter)
                {
                    options.SuppressModelStateInvalidFilter = true;
                }
            }); ;
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

        public static IMvcBuilder AddDynamicApi(this IMvcBuilder builder)
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            var list = section?.DynamicApiAssemblies?.FindAll(x => !string.IsNullOrEmpty(x?.Trim()));
            if (list?.Count > 0)
            {

                builder.ConfigureApplicationPartManager(mgr =>
                {
                    foreach (var path in list)
                    {
                        DynamicApiUtil.Add(path, mgr);
                    }
                });
#pragma warning disable ASP5001 // 类型或成员已过时
#pragma warning disable CS0618 // 类型或成员已过时
                builder.SetCompatibilityVersion(CompatibilityVersion.Latest);
#pragma warning restore CS0618 // 类型或成员已过时
#pragma warning restore ASP5001 // 类型或成员已过时
            }
            return builder;
        }
        /// <summary>
        /// CORS
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<CorsSection>();
            if (section == null || !section.UseCors.Enabled)
                return services;

            services.AddCors(opts =>
            {
                section.AddPolicies(opts);
            });
            LogUtil.Info($"注册 Cors");
            return services;
        }
        /// <summary>
        /// Versioning
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiVersioningEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section != null && section.UseApiVersioning)
            {
                services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true; // 不提供版本时，默认为1.0
                    options.ReportApiVersions = true; //API返回支持的版本信息
                    options.ApiVersionReader = ApiVersionReader.Combine(
                        new UrlSegmentApiVersionReader(),
                        new HeaderApiVersionReader("x-api-version")
                    //new MediaTypeApiVersionReader("x-api-version"),
                    );
                });
                LogUtil.Trace($"注册 ApiVersioning");
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
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section == null || section.Swagger == null || !section.Swagger.Enabled)
                return services;

            if (section.UseApiVersioning)
            {
                services.AddVersionedApiExplorer(setup =>
                {
                    setup.GroupNameFormat = "'v'VVV";
                    setup.SubstituteApiVersionInUrl = true;
                });
            }

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opts =>
            {
                if (section.Swagger.UseSchemaFullName)
                    opts.CustomSchemaIds(x => x.FullName?.Replace('+', '-'));
                var scheme = new OpenApiSecurityScheme()
                {
                    Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Authorization"
                    },
                    Scheme = "oauth2",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                };
                opts.AddSecurityDefinition("Authorization", scheme);
                opts.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    [scheme] = new List<string>()
                });
                opts.IncludeXmlComments(() =>
                {
                    var xmlFiles = new List<string>();
                    foreach (var asm in AssemblyLoadContext.Default.Assemblies)
                    {
                        if (asm.IsDynamic || asm.GetName().GetPublicKey()?.Length > 0)
                            continue;
                        var name = $"{Path.GetFileNameWithoutExtension(asm.Location)}.xml";
                        var path = Path.Combine(AppContext.BaseDirectory, name);
                        if (File.Exists(path))
                            xmlFiles.Add(path);
                    }
                    var xmlParser = new XmlDocumentParser(xmlFiles);
                    return xmlParser.Document;
                }, true);
            });
            if (section.UseApiVersioning)
            {
                services.ConfigureOptions<ConfigureSwaggerOptions>();
            }
            LogUtil.Info($"注册 Swagger");
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
            if (section != null && section.Enabled)
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
                    x.Events = new TinyJwtBearerEvents();
                });
                LogUtil.Info($"注册 JwtAuth");
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
        public static IServiceCollection AddSessionAndCookieEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<SessionAndCookieSection>();
            if (section == null || (!section.UseSession && !section.UseCookieIdentity))
                return services;

            var appName = !string.IsNullOrEmpty(section.ApplicationName)
                ? section.ApplicationName : ConfigUtil.Project.ProjectId;

            // 配置数据保护和应用程序名称(分布式session和cookie)
            var dpb = services.AddDataProtection().SetApplicationName(appName);
            var redisConnStr = AspNetHost.GetDataProtectionRedisConnectionString(section.RedisConnectionStringName);
            if (!string.IsNullOrEmpty(redisConnStr))
            {
                dpb.PersistKeysToStackExchangeRedis(RedisUtil.GetRedisByConnectionString(redisConnStr)
                    , "DataProtection-Keys");
            }
            else
            {
                dpb.PersistKeysToFileSystem(new DirectoryInfo(AppContext.BaseDirectory));
            }

            // 配置Cookie登录
            if (section.UseCookieIdentity)
            {
                services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(opts =>
                    {
                        opts.SlidingExpiration = true; //自动延期
                        opts.Cookie.HttpOnly = true; //禁止js访问
                        opts.Cookie.IsEssential = true;//绕过GDPR

                        opts.Cookie.Name = $".{appName}.Identity";
                        opts.ExpireTimeSpan = (section.CookieTimeout == 0)
                            ? TimeSpan.FromDays(3)
                            : TimeSpan.FromDays(section.CookieTimeout);
                        opts.Cookie.Path = "/";// 跨基路径共享
                        if (!string.IsNullOrEmpty(section.Domain))//跨不同子域共享 .xxx.com
                            opts.Cookie.Domain = section.Domain;
                        opts.Cookie.SameSite = section.SameSiteMode;
                        opts.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    });
            }

            // 配置Sesion
            if (section.UseSession)
            {
                services.AddSession(opts =>
                {
                    opts.Cookie.HttpOnly = true; //禁止js访问
                    opts.Cookie.IsEssential = true;//绕过GDPR

                    opts.Cookie.Name = $".{appName}.Session";
                    opts.IdleTimeout = (section.SessionTimeout == 0)
                                ? TimeSpan.FromMinutes(20)
                                : TimeSpan.FromMinutes(section.SessionTimeout);
                    opts.Cookie.Path = "/";
                    if (!string.IsNullOrEmpty(section.Domain))
                        opts.Cookie.Domain = section.Domain;
                    opts.Cookie.SameSite = section.SameSiteMode;
                    opts.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                });
            }

            LogUtil.Info("注册 SessionAndRedis [UseSession: {session} UseCookie: {cookie}]"
                , section.UseSession, section.UseCookieIdentity);
            return services;
        }
        public static IServiceCollection AddResponseCachingEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<ResponseCachingSection>();
            if (section == null || !section.Enabled)
                return services;

            if (section.CacheProfiles?.Count > 0)
            {
                services.AddResponseCaching();
            }
            LogUtil.Debug($"注册 ResponseCaching");
            return services;
        }
        public static IServiceCollection AddResponseCompressionEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section == null || !section.UseResponseCompression)
                return services;

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            });
            LogUtil.Info($"注册 ResponseCompression");
            return services;
        }
        public static IServiceCollection AddForwardedHeaders(this IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                //options.KnownNetworks.Clear();
                //options.KnownProxies.Clear();
                // ASPNETCORE_FORWARDEDHEADERS_ENABLED true
            });
            return services;
        }
    }
}
