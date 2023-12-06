using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TinyFx.Configuration;
using TinyFx.OAuth;

namespace TinyFx
{
    public static class OAuthHostBuilderExtensions
    {
        public static IHostBuilder AddOAuthEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<OAuthSection>();
            if (section != null)
            {
                builder.ConfigureServices((context, services) =>
                {
                    services.AddSingleton<OAuthService>();
                });
            }
            return builder;
        }
    }
}
