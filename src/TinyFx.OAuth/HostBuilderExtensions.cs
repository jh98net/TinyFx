
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nacos.V2.Naming.Dtos;
using TinyFx;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.OAuth;

namespace TinyFx
{
    public static class OAuthHostBuilderExtensions
    {
        public static IHostBuilder UseOAuthEx(this IHostBuilder builder)
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
