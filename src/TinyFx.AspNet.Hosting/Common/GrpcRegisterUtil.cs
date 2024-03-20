using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ProtoBuf.Grpc.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Reflection;
using static System.Collections.Specialized.BitVector32;

namespace TinyFx.AspNet.Hosting.Common
{
    internal static class GrpcRegisterUtil
    {
        private static List<Type> _grpcTypes;
        public static WebApplicationBuilder AddGrpcEx(this WebApplicationBuilder builder)
        {
            var section = ConfigUtil.GetSection<GrpcSection>();
            if (section != null && section.Enabled)
            {
                _grpcTypes = GetGrpcTypes(section.Assemblies);
                if (_grpcTypes == null || _grpcTypes.Count == 0)
                    return builder;

                var grpcPort = section.Port > 0
                    ? section.Port : ConfigUtil.Service.GrpcPort;
                if (grpcPort <= 0)
                {
                    grpcPort = ConfigUtil.Service.HostPort + 10000;
                    LogUtil.Warning($"启动GRPC服务时端口未指定，默认增加10000。hostPort:{ConfigUtil.Service.HostPort} grpcPort:{grpcPort}");
                }
                ConfigUtil.Service.GrpcPort = grpcPort;
                builder.WebHost.ConfigureKestrel(opts =>
                {
                    opts.ListenAnyIP(grpcPort, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
                });
                builder.Services.AddCodeFirstGrpc();
            }
            return builder;
        }
        public static WebApplication UseGrpcEx(this WebApplication app)
        {
            if (_grpcTypes?.Count > 0)
            {
                // 注册
                Type invokeType = typeof(GrpcEndpointRouteBuilderExtensions);
                _grpcTypes.ForEach(genericType =>
                {
                    // app.MapGrpcService<GreeterService>();
                    ReflectionUtil.InvokeStaticGenericMethod(invokeType, "MapGrpcService", genericType, app);
                });
            }
            return app;
        }


        private static List<Type> GetGrpcTypes(List<string> asms)
        {
            var ret = new List<Type>();
            if (asms != null && asms.Count > 0)
            {
                asms.ForEach(x =>
                {
                    var ams = DIUtil.GetService<IAssemblyContainer>().GetAssembly(x, "加载GRPC的Assemblies");
                    ret.AddRange(GetGrpcTypes(ams));
                });
            }
            else
            {
                ret.AddRange(GetGrpcTypes(Assembly.GetEntryAssembly()));
                ret.AddRange(GetGrpcTypes(Assembly.GetExecutingAssembly()));
            }
            return ret;
        }
        private static List<Type> GetGrpcTypes(Assembly? assembly)
        {
            var ret = new List<Type>();
            if (assembly != null)
            {
                foreach (var type in assembly.GetExportedTypes())
                {
                    if (!type.IsClass)
                        continue;
                    var itypes = type.GetInterfaces();
                    if (itypes.Length == 0)
                        continue;
                    foreach (var itype in itypes)
                    {
                        var attr = itype.GetCustomAttribute<ServiceContractAttribute>();
                        if (attr != null)
                        {
                            ret.Add(type);
                            break;
                        }
                    }
                }
            }
            return ret;
        }
    }
}
