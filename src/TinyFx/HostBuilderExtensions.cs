using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Randoms;
using TinyFx.Reflection;

namespace TinyFx
{
    public static class TinyFxHostBuilderExtensions
    {
        private static bool _inited = false;
        /// <summary>
        /// 应用程序中配置TinyFx，优先使用应用程序的配置文件，其次使用tinyfx.json
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="envString"></param>
        /// <returns></returns>
        public static IHostBuilder UseTinyFx(this IHostBuilder builder, string envString = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (_inited)
                return builder;

            ConfigUtil.Init(envString);
            builder.ConfigureHostOptions((context,opts) => {
                context.HostingEnvironment.EnvironmentName = ConfigUtil.EnvironmentString;
                context.HostingEnvironment.ApplicationName = ConfigUtil.Project.ProjectId;
                context.Configuration = ConfigUtil.Configuration;
            });
            builder.ConfigureServices((context, services) =>
            {
                services.AddDistributedMemoryCache();
                // DI
                DIUtil.Init(services);
                LogUtil.Rebuild();
            });
            _inited = true;
            LogUtil.Trace("TinyFx 配置启动");
            return builder;
        }
    }
}
