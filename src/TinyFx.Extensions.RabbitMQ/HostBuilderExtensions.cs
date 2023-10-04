using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;
using TinyFx.Extensions.RabbitMQ;
using TinyFx.Logging;
using EasyNetQ.Logging;
using EasyNetQ;
using EasyNetQ.ConnectionString;
using TinyFx.Net;
using Nacos.V2.Naming.Dtos;

namespace TinyFx
{
    public static class RabbitMQHostBuilderExtensions
    {
        public static IHostBuilder UseRabbitMQEx(this IHostBuilder builder)
        {
            var section = ConfigUtil.GetSection<RabbitMQSection>();
            if (section != null && section.Enabled && section.ConnectionStrings != null && section.ConnectionStrings.Count > 0)
            {
                builder.ConfigureServices((context, services) =>
                {
                    services.AddSingleton(new MQContainer());
                    services.AddHostedService<MQHostedService>();
                });
                LogUtil.Trace($"RabbitMQ 配置启动");
            }
            return builder;
        }
    }
}
