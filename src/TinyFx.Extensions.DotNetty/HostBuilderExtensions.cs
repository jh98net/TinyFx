using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProtoBuf;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using TinyFx.Configuration;
using TinyFx.Extensions.DotNetty;
using TinyFx.Logging;
using static System.Collections.Specialized.BitVector32;

[module: CompatibilityLevel(CompatibilityLevel.Level300)]
namespace TinyFx
{
    public static class DotNettyHostBuilderExtensions
    {
        public static IHostBuilder UseDotNettyWebSocket(this IHostBuilder builder, IServerEventListener eventListener = null)
        {
            // Options
            var section = ConfigUtil.GetSection<DotNettySection>();
            if (section == null)
                throw new Exception("启动UseDotNettyWebSocket服务时，配置文件不存在DotNetty配置节。");
            if (section.Server == null)
                throw new Exception("启动UseDotNettyWebSocket服务时，配置文件不存在DotNetty:Server配置节。");
            var options = section.Server;
            // Consul
            /*
            ConsulClientEx consulClient = null;
            if (consulSection == null)
                consulSection = ConfigUtil.GetSection<ConsulSection>();
            if (consulSection != null)
                consulClient = new ConsulClientEx(consulSection);
            */
            builder.ConfigureServices((ctx, services) =>
            {
                /*
                if(consulClient!= null)
                    services.AddSingleton(consulClient);
                */
                services.AddSingleton(options);
                services.AddSingleton(AppSessionContainer.Instance);
                services.AddSingleton(GetCommandContainer(options));
                services.AddSingleton<IBodySerializer, ProtobufNetSerializer>();

                var packetSerializer = new PacketSerializer(options.IsLittleEndian);
                services.AddSingleton<IPacketSerializer>(packetSerializer);

                if (eventListener != null)
                    services.AddSingleton(eventListener);
                services.AddSingleton(new DefaultServerEventListener());
                services.AddHostedService<WebSocketHostedService>();
            });
            LogUtil.Debug($"DotNetty 配置启动");
            return builder;
        }
        private static CommandContainer GetCommandContainer(ServerOptions options)
        {
            var asms = new List<Assembly>();
            if (options.Assemblies == null || options.Assemblies.Count == 0)
            {
                asms.Add(Assembly.GetEntryAssembly());
            }
            else
            {
                foreach (var asm in options.Assemblies)
                {
                    var file = Path.Combine(AppContext.BaseDirectory, asm);
                    if (!File.Exists(file))
                        throw new Exception($"配置文件中DotNetty:Server:Assemblies 中配置项{asm}不存在。");
                    asms.Add(Assembly.LoadFrom(file));
                }
            }
            var asmNetty = Assembly.GetExecutingAssembly();
            if (!asms.Contains(asmNetty))
                asms.Add(asmNetty);
            CommandContainer.Instance.AddCommands(asms);
            return CommandContainer.Instance;
        }
    }
}
