using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.IP2Country;
using TinyFx.Logging;

namespace TinyFx
{
    public static class IP2CountryHostBuilderExtensions
    {
        public static IHostBuilder AddIP2CountryEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<IP2CountrySection>();
            if (section == null || !section.Enabled)
                return builder;

            var service = new IP2CountryService();
            builder.ConfigureServices(services => 
            {
                service.Init();
                services.AddSingleton<IIP2CountryService>(service);
            });
            LogUtil.Info($"配置 => [IP2Country]");
            return builder;
        }
    }
}
