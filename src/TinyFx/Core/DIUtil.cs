using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace TinyFx
{
    /// <summary>
    /// DI辅助类
    /// </summary>
    public static class DIUtil
    {
        private static IServiceCollection _services;
        /// <summary>
        /// DI 服务集合
        /// </summary>
        public static IServiceCollection Services
        {
            get
            {
                if (_services == null)
                    _services = new ServiceCollection();
                return _services;
            }
            set
            {
                _services = value;
            }
        }

        /// <summary>
        /// DI 初始化
        /// </summary>
        /// <param name="services"></param>
        internal static void SetServices(IServiceCollection services = null)
        {
            Services = services ?? new ServiceCollection();
        }
        public static void SetServiceProvider(IServiceProvider provider = null)
        {
            _serviceProvider = provider ?? Services.BuildServiceProvider();
        }

        private static IServiceProvider _serviceProvider;
        private static IServiceProvider GetServiceProvider()
        {
            if (_serviceProvider == null)
                _serviceProvider = Services.BuildServiceProvider();
            var ihttp = _serviceProvider.GetService<IHttpContextAccessor>();
            if (ihttp != null && ihttp.HttpContext != null)
                return ihttp.HttpContext.RequestServices;
            return _serviceProvider.CreateScope().ServiceProvider;
        }

        /// <summary>
        /// 从System.IServiceProvider获取类型为T的Service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
            => GetServiceProvider().GetService<T>();

        /// <summary>
        /// 从System.IServiceProvider获取类型为T的Service，类型不存在则抛出异常InvalidOperationException
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRequiredService<T>()
            => GetServiceProvider().GetRequiredService<T>();

        public static object GetService(Type type)
            => GetServiceProvider().GetService(type);
        public static object GetRequiredService(Type type)
            => GetServiceProvider().GetRequiredService(type);

        #region AddService
        #region AddScoped
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddScoped<TService>() where TService : class
            => Services.AddScoped<TService>();
        public static IServiceCollection AddScoped(Type serviceType, Type implementationType)
            => Services.AddScoped(serviceType, implementationType);
        public static IServiceCollection AddScoped(Type serviceType, Func<IServiceProvider, object> implementationFactory)
            => Services.AddScoped(serviceType, implementationFactory);
        public static IServiceCollection AddScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => Services.AddScoped<TService, TImplementation>();
        public static IServiceCollection AddScoped(Type serviceType)
            => Services.AddScoped(serviceType);
        public static IServiceCollection AddScoped<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
            => Services.AddScoped<TService>(implementationFactory);
        public static IServiceCollection AddScoped<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
            => Services.AddScoped<TService, TImplementation>(implementationFactory);
        #endregion

        #region AddSingleton
        public static IServiceCollection AddSingleton<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
            => Services.AddSingleton<TService, TImplementation>(implementationFactory);
        public static IServiceCollection AddSingleton<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
            => Services.AddSingleton<TService>(implementationFactory);
        public static IServiceCollection AddSingleton<TService>() where TService : class
            => Services.AddSingleton<TService>();
        public static IServiceCollection AddSingleton(Type serviceType)
            => Services.AddSingleton(serviceType);
        public static IServiceCollection AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => Services.AddSingleton<TService, TImplementation>();
        public static IServiceCollection AddSingleton(Type serviceType, Func<IServiceProvider, object> implementationFactory)
            => Services.AddSingleton(serviceType, implementationFactory);
        public static IServiceCollection AddSingleton(Type serviceType, Type implementationType)
            => Services.AddSingleton(serviceType, implementationType);
        public static IServiceCollection AddSingleton<TService>(TService implementationInstance) where TService : class
            => Services.AddSingleton<TService>(implementationInstance);
        public static IServiceCollection AddSingleton(Type serviceType, object implementationInstance)
            => Services.AddSingleton(serviceType, implementationInstance);
        #endregion

        #region AddTransient
        public static IServiceCollection AddTransient<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
            => Services.AddTransient<TService, TImplementation>(implementationFactory);
        public static IServiceCollection AddTransient<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
            => Services.AddTransient<TService>(implementationFactory);
        public static IServiceCollection AddTransient<TService>() where TService : class
            => Services.AddTransient<TService>();
        public static IServiceCollection AddTransient(Type serviceType)
            => Services.AddTransient(serviceType);
        public static IServiceCollection AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => Services.AddTransient<TService, TImplementation>();
        public static IServiceCollection AddTransient(Type serviceType, Func<IServiceProvider, object> implementationFactory)
            => Services.AddTransient(serviceType, implementationFactory);
        public static IServiceCollection AddTransient(Type serviceType, Type implementationType)
            => Services.AddTransient(serviceType, implementationType);
        #endregion
        #endregion
    }
}
