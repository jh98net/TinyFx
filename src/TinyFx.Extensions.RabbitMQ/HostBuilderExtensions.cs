using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TinyFx.Configuration;
using TinyFx.Extensions.RabbitMQ;
using TinyFx.Hosting;
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

            var container = new MQContainer();
            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton(container);
            });
            HostingUtil.RegisterStarting(async () => 
            {
                await container.InitAsync();
                LogUtil.Info("启动 => [RabbitMQ]资源加载");
            });
            HostingUtil.RegisterStopping(async () => 
            {
                container.Dispose();
                LogUtil.Info("停止 => [RabbitMQ]资源释放");
            });
            HostingUtil.RegisterDelayTimer(TimeSpan.FromSeconds(section.SACBindDelay)
                , async (_) => await container.BindSACConsumer());


            LogUtil.Info("配置 => [RabbitMQ] ConsumerAssemblies: {ConsumerAssemblies}", string.Join('|', section.ConsumerAssemblies));
            return builder;
        }
    }
}
