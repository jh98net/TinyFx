using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.OAuth;

namespace TinyFx
{
    public static class OAuthHostBuilderExtensions
    {
        public static IHostBuilder AddOAuthEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<OAuthSection>();
            if (section == null) return builder;

            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<OAuthService>();
            });

            LogUtil.Debug($"OAuth 配置");
            return builder;
        }
    }
}
