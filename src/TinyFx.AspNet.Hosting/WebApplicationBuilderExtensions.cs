using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Runtime.Loader;
using System.Xml.XPath;
using System.Linq;
using TinyFx;
using TinyFx.AspNet;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Logging;
using TinyFx.Security;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Google.Protobuf.WellKnownTypes;
using TinyFx.AspNet.Filters;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpOverrides;
using System.IO.Compression;
using TinyFx.AspNet.Common;
using Microsoft.Extensions.Configuration;
using Nacos.AspNetCore.V2;
using System.Configuration;
using Nacos.V2;
using Nacos.V2.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Hosting;
using TinyFx.Extensions.AppMetric;

namespace TinyFx
{
    public static class AspNetWebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAspNetEx(this WebApplicationBuilder builder, AspNetType type)
        {
            // Kestrel
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section != null && section.RequestBytesPerSecond > 0 && section.RequestPeriodSecond > 0)
            {
                builder.WebHost.ConfigureKestrel(opts =>
                {
                    var bytesPerSecond = section.RequestBytesPerSecond;
                    var gracePeriod = TimeSpan.FromSeconds(section.RequestPeriodSecond);
                    opts.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond, gracePeriod);
                });
            }
            AddAspNetEx(builder.Services, type);
            // AppMetric
            //builder.AddAppMetricEx(ConfigUtil.Configuration, ConfigUtil.Project.ProjectId);
            
            return builder;
        }
        private static IServiceCollection AddAspNetEx(this IServiceCollection services, AspNetType type)
        {
            if ((type & AspNetType.Api) != 0)
            {
                services.AddControllersEx()
                    .AddDynamicApi();
                LogUtil.Trace($"注册 AddControllers");
            }
            if ((type & AspNetType.Razor) != 0)
            {
                services.AddRazorPages();
                LogUtil.Trace($"注册 AddRazorPages");
            }
            if ((type & AspNetType.ServerBlazor) != 0)
            {
                services.AddServerSideBlazorEx();
                LogUtil.Trace($"注册 AddServerSideBlazor");
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
                ret.IsContextLog = true;
                return ret;
            });
            return services;
        }
        public static IServiceCollection AddNacosAspNetEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<NacosSection>();
            if (section != null && section.Enabled && !string.IsNullOrEmpty(section.ServiceName))
            {
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
                    LogUtil.Trace($"ResponseCaching 配置启动");
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
            if (section != null)
            {
                services.AddCors(opts =>
                {
                    if (section.Policies?.Count > 0)
                    {
                        foreach (var item in section.Policies)
                        {
                            var policy = item.Value;
                            if (policy.Name == section.UseCors?.DefaultPolicy)
                                opts.AddDefaultPolicy(AspNetUtil.GetPolicyBuilder(policy));
                            else
                                opts.AddPolicy(policy.Name, AspNetUtil.GetPolicyBuilder(policy));
                        }
                    }
                });
                LogUtil.Trace($"Cors 配置启动");
            }
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
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section != null && section.Swagger != null && section.Swagger.Enabled)
            {
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
        public static IServiceCollection AddSessionAndCookieEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<SessionAndCookieSection>();
            if (section != null && (section.UseSession || section.UseCookieIdentity))
            {
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

                LogUtil.Trace("SessionAndRedis 配置启动。session:{session} cookie:{cookie}"
                    , section.UseSession, section.UseCookieIdentity);
            }
            return services;
        }
        public static IServiceCollection AddResponseCachingEx(this IServiceCollection services)
        {
            var rcSection = ConfigUtil.GetSection<ResponseCachingSection>();
            if (rcSection != null && rcSection.Enabled)
            {
                if (rcSection.CacheProfiles?.Count > 0)
                {
                    services.AddResponseCaching();
                }
                LogUtil.Trace($"ResponseCaching 配置启动");
            }
            return services;
        }
        public static IServiceCollection AddResponseCompressionEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section != null && section.UseResponseCompression)
            {
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
                LogUtil.Trace($"ResponseCompression 配置启动");
            }
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
