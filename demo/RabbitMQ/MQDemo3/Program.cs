using Microsoft.Extensions.Hosting;
using MQDemoLib;
using TinyFx;
using TinyFx.Extensions.RabbitMQ;

TinyFxHost.CreateBuilder()
    .UseRedisEx()
    .UseRabbitMQEx()
    .Build()
    .UseTinyFxEx()
    .Run();