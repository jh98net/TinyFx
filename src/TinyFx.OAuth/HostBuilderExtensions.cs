
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nacos.V2.Naming.Dtos;
using TinyFx;
using TinyFx.Configuration;
using TinyFx.OAuth;
using TinyFx.OAuth.Client;

namespace TinyFx
{
    public static class OAuthHostBuilderExtensions
    {
        public static WebApplicationBuilder AddOAuth(this WebApplicationBuilder builder)
        {
            builder.Host.ConfigureServices(services => services.AddOAuth());
            return builder;
        }

        /// <summary>
        /// 注入接口
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOAuth(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IAuthRequest))))
                .ToArray();
            foreach (var impType in types)
            {
                services.AddScoped(typeof(IAuthRequest), impType);
            }
            AppDependencyResolver.Init(services.BuildServiceProvider());
            return services;
        }
        /// <summary>
        /// 注入三方配置文件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOAuthConfig<M>(this IServiceCollection services, M types) where M : IAuthConfig
        {
            services.AddScoped(typeof(IAuthConfig), types.GetType());
            return services;
        }
    }

    public static class OAuthService
    {
        public static IAuthRequest? OAuthServiceInit(this IEnumerable<IAuthRequest> authRequests, int authType)
        {
            string serviceName = EnumUtil.GetInfo(typeof(OAuthEnum)).GetItem(authType).Description + "Service";
            return authRequests.FirstOrDefault(a => a.GetType().Name == serviceName);
        }
    }
    public class AppDependencyResolver
    {
        private static AppDependencyResolver _resolver;
        public static AppDependencyResolver Current
        {
            get
            {
                if (_resolver == null)
                    throw new Exception("AppDependencyResolver not initialized");
                return _resolver;
            }
        }
        public static void Init(IServiceProvider service)
        {
            _resolver = new AppDependencyResolver(service);
        }
        private AppDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        private readonly IServiceProvider _serviceProvider;
        public object GetService(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }
        public T GetService<T>()

        {
            return _serviceProvider.GetRequiredService<T>();
        }
        public IEnumerable<T> GetServices<T>()

        {
            return _serviceProvider.GetServices<T>();
        }
        public IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
    }
}
