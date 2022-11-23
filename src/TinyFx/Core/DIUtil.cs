using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

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
        public static void Init(IServiceCollection services = null)
        {
            Services = services ?? new ServiceCollection();
        }

        //private static ServiceProvider _serviceProvider;
        /// <summary>
        /// 服务提供程序
        /// </summary>
        public static ServiceProvider ServiceProvider => Services.BuildServiceProvider();
        //{
        //    get
        //    {
        //        if (Services == null)
        //            throw new Exception("需要使用DIUtil.Init()初始化DI容器");
        //        if (_serviceProvider == null)
        //            _serviceProvider = Services.BuildServiceProvider();
        //        return _serviceProvider;
        //    }
        //}

        /// <summary>
        /// 从System.IServiceProvider获取类型为T的Service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
            => ServiceProvider.GetService<T>();

        /// <summary>
        /// 从System.IServiceProvider获取类型为T的Service，类型不存在则抛出异常InvalidOperationException
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRequiredService<T>()
            => ServiceProvider.GetRequiredService<T>();

        public static object GetService(Type type)
            => ServiceProvider.GetService(type);
        public static object GetRequiredService(Type type)
            => ServiceProvider.GetRequiredService(type);

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

        /// <summary>
        /// 在ConfigureServices中调用，以便之后可以使用IOption<typeparamref name="T"/>依赖注入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Configure<T>() where T : class, IConfigSection, new()
        {
            Services.Configure<T>(options => options = ConfigUtil.GetSection<T>());
        }
    }
}
