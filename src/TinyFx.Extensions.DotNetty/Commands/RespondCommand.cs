using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Net;
using TinyFx.Reflection;

namespace TinyFx.Extensions.DotNetty
{
    public abstract class RespondCommand<IRequest, IResponse>: CommandBase
    {
        public override async Task ExecuteAsync(RequestContext ctx)
        {
            try
            {
                ProtoResponse<IResponse> result;
                try
                {
                    result = (!ctx.Session.IsLogin && CheckLogin)
                        ? new ProtoResponse<IResponse> { 
                            Success = false,
                            Code = ResponseCode.G_Unauthorized,
                            Message = "未登录用户无法访问"
                        }
                        : await Respond(ctx, (IRequest)ctx.Packet.Body);
                }
                catch (Exception ex)
                {
                    var exc = ExceptionUtil.GetException<CustomException>(ex);
                    if (exc != null)
                    {
                        if (string.IsNullOrEmpty(exc.Code))
                            throw new Exception("自定义异常CustomException的Code不能为null", exc);
                        result = new ProtoResponse<IResponse>()
                        {
                            Success = false,
                            Code = exc.Code,
                            Message = exc.Message,
                        };
                        LogUtil.Debug($"[CustomException] commandId:{CommandId} userId:{ctx.UserId} commandName:{this.GetType().FullName} code:{exc.Code} message:{exc.Message}");
                    }
                    else
                    {
                        result = new ProtoResponse<IResponse> {
                            Success = false,
                            Code = ResponseCode.G_UnhandledException,
                            Message = ConfigUtil.Project.ResponseErrorDetail 
                                ? $"{ex.Message}{Environment.NewLine}{ex.StackTrace}" : ex.Message
                        };
                        LogUtil.Error(ex, $"未处理异常：commandId:{CommandId} userId:{ctx.UserId} commandName:{this.GetType().FullName} message:{ex.Message}");
                    }
                }
                var rsp = new Packet();
                rsp.CommandId = CommandId;
                rsp.Body = typeof(IResponse) == typeof(object)
                    ? new ProtoResponse { 
                        Success = result.Success,
                        Code = result.Code,
                        Message= result.Message,
                        Exception = result.Exception
                    }
                    : result;
                await ctx.Session.SendAsync(rsp);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, $"未处理异常：commandId:{CommandId} userId:{ctx.UserId} commandName:{this.GetType().FullName} message:{ex.Message}");
                throw;
            }
        }
        public abstract Task<IResponse> Respond(RequestContext ctx, IRequest request);
    }
    public abstract class CommandBase
    {
        public AppSessionContainer Sessions;
        public CommandDescriptor Descriptor { get; internal set; }
        public int CommandId => Descriptor.CommandId;
        public bool CheckLogin => Descriptor.CheckLogin;
        public CommandBase()
        {
            Sessions = DIUtil.GetRequiredService<AppSessionContainer>();
            /*
            //Sessions = DotNettyExtensions.Sessions;
            var type = GetType();
            var Attribute = type.GetCustomAttribute<CommandAttribute>();
            if (Attribute == null)
                throw new Exception($"Command类没有声明 CommandAttribute: {type.FullName}");
            if (!ReflectionUtil.IsSubclassOfGeneric(type, typeof(RespondCommand<,>)))
                throw new Exception($"Command类没有继承 RespondCommand<IRequest, IResponse>: {type.FullName}");
            Descriptor = new CommandDescriptor
            {
                CommandType = type,
                CommandId = Attribute.Id,
                CheckLogin = Attribute.CheckLogin,
                RequestType = type.BaseType.GenericTypeArguments[0],
                ResponseType = type.BaseType.GenericTypeArguments[1],
            };
            Descriptor.PacketBodyType = Descriptor.ResponseType == typeof(object) ? typeof(ProtoResponse)
                : typeof(ProtoResponse<>).MakeGenericType(Descriptor.ResponseType);

            Descriptor.RespondExecute = ExecuteAsync;
            */
        }
        public abstract Task ExecuteAsync(RequestContext ctx);
    }
}
