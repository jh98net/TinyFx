using Demo.DotNettyHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Tracing;
using TinyFx;
using TinyFx.Extensions.DotNetty;
using TinyFx.Extensions.DotNetty.NettyInfoCommand;
using TinyFx.Logging;

var host = Host.CreateDefaultBuilder(args)
    .UseTinyFx()
    .UseSerilogEx()
    .UseAutoMapperEx()
    .UseRedisEx()
    .UseRabbitMQEx()
    .UseDotNettyWebSocket(new ServerEventListener())
    .Build();

host.Start();

host.WaitForShutdown();
