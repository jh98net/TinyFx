using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.HCaptcha;

namespace TinyFx
{
    public static class HCaptchaHostBuilderExtensions
    {
        public static IHostBuilder AddHCaptchaEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<HCaptchaSection>();
            if (section?.Enabled ?? false)
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IHCaptchaService>(new HCaptchaService());
                });
            }
            return builder;
        }
    }
}
