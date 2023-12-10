using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TinyFx.Configuration;
using TinyFx.Extensions.RabbitMQ;
using TinyFx.Logging;

namespace TinyFx
{
    public static class RabbitMQHostBuilderExtensions
    {
        public static IHostBuilder AddRabbitMQEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<RabbitMQSection>();
            if (section == null || !section.Enabled || section.ConnectionStrings == null || section.ConnectionStrings.Count == 0)
                return builder;

            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton(new MQContainer());
                services.AddHostedService<MQHostedService>();
            });

            LogUtil.Debug($"RabbitMQ 配置启动");
            return builder;
        }
    }
}
