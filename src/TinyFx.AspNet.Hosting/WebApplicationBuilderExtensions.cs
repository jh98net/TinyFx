using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Nacos.AspNetCore.V2;
using System.IO.Compression;
using System.Runtime.Loader;
using TinyFx.AspNet;
using TinyFx.AspNet.Common;
using TinyFx.AspNet.Filters;
using TinyFx.AspNet.Hosting;
using TinyFx.AspNet.Hosting.Common;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Security;
using TinyFx.Xml;

namespace TinyFx
{
    public static class AspNetWebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAspNetEx(this WebApplicationBuilder builder)
        {
            // Kestrel
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section != null)
            {
                builder.WebHost.ConfigureKestrel(opts =>
                {
                    var httpPort = ConfigUtil.Service.HttpPort > 0
                        ? ConfigUtil.Service.HttpPort : 80;
                    ConfigUtil.Service.HttpPort = httpPort;
                    opts.ListenAnyIP(httpPort, listenOptions => listenOptions.Protocols = HttpProtocols.Http1);
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
            builder.AddGrpcEx();
            AddAspNetExDetail(builder.Services);
            //
            TinyFxHostingStartupLoader.Instance.ConfigureServices(builder);
            return builder;
        }
        private static WebApplicationBuilder AddGrpcEx(this WebApplicationBuilder builder)
        {
            GrpcRegisterUtil.AddGrpcEx(builder);
            return builder;
        }
        private static IServiceCollection AddAspNetExDetail(this IServiceCollection services)
        {
            services.AddControllersEx()
                .AddDynamicApi();
            // 解决Multipart body length limit 134217728 exceeded
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });
            LogUtil.Info($"注册 => AddControllers");

            services.AddHealthChecks();         // health
            return services
                .AddRequestLoggingEx()          // LogBuilder
                .AddCorsEx()                    // Cors
                .AddApiVersioningEx()           // ApiVersion
                .AddSwaggerGenEx()              // Swagger
                .AddApiJwtAuthEx()              // Jwt
                .AddSessionAndCookieEx()        // SessionOrCookie 
                .AddResponseCachingEx()         // ResponseCaching
                .AddResponseCompressionEx()     // ResponseCompression
                .AddForwardedHeaders()          // ForwardedHeaders

                .AddHttpClient()                // IHttpClientFactory
                .AddHttpContextAccessor()       // .AddOAuth();      // IHttpContextAccessor
                .AddAccessVerifyEx()            // AccessVerify
                .AddSyncNotifyEx()              // SyncNotify

                .AddNacosAspNetEx();            // Nacos
        }
        public static IServiceCollection AddRequestLoggingEx(this IServiceCollection services)
        {
            services.AddScoped<ILogBuilder>((_) =>
            {
                var ret = new LogBuilder("ASPNET_REQUEST");
                ret.IsContext = true;
                return ret;
            });
            return services;
        }
        public static IServiceCollection AddNacosAspNetEx(this IServiceCollection services)
        {
            var section = DIUtil.GetService<NacosSection>();
            if (section != null && section.Enabled)
            {
                if (string.IsNullOrEmpty(section.ServiceName))
                    throw new Exception("配置Nacos:ServiceName不能为空且必须与ProjectId相同");
                if (section.ServiceName != ConfigUtil.Project.ProjectId)
                    LogUtil.Warning($"Nacose ServiceName 和 ProjectId 不相同。ServiceName: {section.ServiceName} ProjectId: {ConfigUtil.Project.ProjectId}");

                ConfigUtil.Configuration["Nacos:Metadata:tinyfx.SERVICE_ID"] = ConfigUtil.Service.ServiceId;
                ConfigUtil.Configuration["Nacos:Metadata:tinyfx.REGISTER_DATE"] = DateTime.UtcNow.UtcToCNString();
                services.AddNacosAspNet(ConfigUtil.Configuration, "Nacos");
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
                    LogUtil.Trace($"配置 => [ResponseCaching]");
                }
            }).AddJsonOptions(options =>
            {
                SerializerUtil.ConfigJsonOptions(options.JsonSerializerOptions);
            }).ConfigureApiBehaviorOptions(options =>
            {
                // 禁用[ApiController]的自动 400 响应
                if (section != null && section.UseModelStateFilter)
                {
                    options.SuppressModelStateInvalidFilter = true;
                }
            });
        }
        public static IMvcBuilder AddRazorPagesEx(this IServiceCollection services)
        {
            return services.AddRazorPages(options =>
            {
                //options..Filters.Add(typeof(CustomExceptionFilter));
            }).AddJsonOptions(options =>
            {
                SerializerUtil.ConfigJsonOptions(options.JsonSerializerOptions);
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
            LogUtil.Info($"注册 => Cors");
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
                services.AddApiVersioning(opts =>
                {
                    opts.DefaultApiVersion = new ApiVersion(1, 0);
                    opts.AssumeDefaultVersionWhenUnspecified = true; // 不提供版本时，默认为1.0
                    opts.ReportApiVersions = true; //API返回支持的版本信息
                    opts.ApiVersionReader = new UrlSegmentApiVersionReader();
                    //options.ApiVersionReader = ApiVersionReader.Combine(
                    //    new UrlSegmentApiVersionReader(),
                    //    new QueryStringApiVersionReader("api-version"),
                    //    new HeaderApiVersionReader("x-api-version"),
                    //    new MediaTypeApiVersionReader("x-api-version")
                    //);

                    //默认以当前最高版本进行访问
                    //opts.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opts);
                })
                .AddMvc(options =>
                {
                    // 根据定义控制器的命名空间的名称自动应用 api 版本
                    options.Conventions.Add(new VersionByNamespaceConvention());
                })
                .AddApiExplorer(setup =>
                {
                    setup.GroupNameFormat = "'v'VVV";
                    setup.SubstituteApiVersionInUrl = true;
                });
                LogUtil.Trace($"注册 => ApiVersioning");
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

            //services.AddEndpointsApiExplorer(); //只有最小 API 调用
            services.AddSwaggerGen(opts =>
            {
                if (section.Swagger.UseSchemaFullName)
                    opts.CustomSchemaIds(x => x.FullName?.Replace('+', '-'));

                // 添加承载身份验证的安全定义和要求
                opts.AddSecurityDefinition("JwtAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT授权 ==> 输入框输入token"
                });
                opts.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "JwtAuth" }
                        },
                        new string[] {}
                    }
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
                        if (System.IO.File.Exists(path))
                            xmlFiles.Add(path);
                    }
                    var xmlParser = new XmlDocumentParser(xmlFiles);
                    return xmlParser.Document;
                }, true);
            });
            if (section.UseApiVersioning)
            {
                services.ConfigureOptions<NamedSwaggerGenOptions>();
            }
            LogUtil.Info($"注册 => Swagger");
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
                if (string.IsNullOrEmpty(section.SigningKey))
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
                LogUtil.Info($"注册 => JwtAuth");
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

            // 配置Cookie登录
            if (section.UseCookieIdentity)
            {
                services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(opts =>
                    {
                        opts.SlidingExpiration = true; //自动延期
                        opts.Cookie.HttpOnly = true; //禁止js访问
                        opts.Cookie.IsEssential = true;//绕过GDPR

                        opts.Cookie.Name = $".{ConfigUtil.Project.ApplicationName}.Identity";
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

                    opts.Cookie.Name = $".{ConfigUtil.Project.ApplicationName}.Session";
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

            LogUtil.Info("注册 => SessionAndRedis [UseSession: {session} UseCookie: {cookie}]"
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
            LogUtil.Debug($"注册 => ResponseCaching");
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
            LogUtil.Info($"注册 => ResponseCompression");
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
        public static IServiceCollection AddAccessVerifyEx(this IServiceCollection services)
        {
            //var section = ConfigUtil.GetSection<AccessVerifySection>();
            //if (section != null && section.Enabled)
            //{
            //    services.AddSingleton<IAccessVerifyService>(new AccessVerifyService());
            //}
            services.AddSingleton<IAccessSignFilterService>(new AccessSignFilterService());
            return services;
        }
        public static IServiceCollection AddSyncNotifyEx(this IServiceCollection services)
        {
            var section = ConfigUtil.GetSection<AspNetSection>();
            if (section != null && section.UseSyncNotify)
            {
                services.AddSingleton<ISyncNotifyService>(new RedisSyncNotifyService());
            }
            return services;
        }
    }
}
