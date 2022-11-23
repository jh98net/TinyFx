using EasyNetQ;
using EasyNetQ.Internals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx.Extensions.RabbitMQ
{
    /// <summary>
    /// 请求响应(Request => Respond)模式
    ///     基类rsp结尾 => Req/Rsp
    ///     对应MQUtil.Request方法
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class RespondConsumer<TRequest, TResponse> : ConsumerBase
        where TRequest : class
    {
        private IDisposable _dispos;
        public override void Register()
        {
            Func<TRequest, CancellationToken, Task<ResponseResult<TResponse>>> responder = async (request, cancellationToken) =>
            {
                try
                {
                    var result = await Respond(request, cancellationToken);
                    return new ResponseResult<TResponse>(result);
                }
                catch (Exception ex)
                {
                    var exc = ExceptionUtil.GetException<CustomException>(ex);
                    var ret = new ResponseResult<TResponse>();
                    ret.Success = false;
                    ret.Message = ex.Message;
                    if (ConfigUtil.Project.ResponseErrorDetail)
                        ret.Exception = ex;
                    if (exc != null)
                    {
                        ret.Code = exc.Code;
                    }
                    else
                    {
                        ret.Code = ResponseCode.G_InternalServerError;
                        LogUtil.Error(exc, $"RespondConsumer异常- 查看默认错误队列(broker): type:{GetType().FullName} request:{request}");
                    }
                    return ret;
                }
            };
            _dispos = Bus.Rpc.Respond(responder, Configuration);
        }
        protected abstract void Configuration(IResponderConfiguration x);
        protected abstract Task<TResponse> Respond(TRequest request, CancellationToken cancellationToken);
        public override void Dispose()
        {
            _dispos?.Dispose();
        }
    }
}
