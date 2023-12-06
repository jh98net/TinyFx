using EasyNetQ;
using EasyNetQ.Internals;
using Microsoft.VisualBasic;
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
    /// MQ请求响应(Request => Respond)模式的Respond基类
    /// 用于接收使用MQUtil.Request方法发出的MQ消息
    /// 继承的子类名建议使用MQRsp结尾
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class MQRespondConsumer<TRequest, TResponse> : BaseMQConsumer
        where TRequest : new()
    {
        private IDisposable _dispos;

        public MQRespondConsumer()
        {
        }
        protected override string GetConnectionStringName()
        {
            return MQUtil.GetMessageAttribute<MQRequestMessageAttribute>(typeof(TRequest))
                    ?.ConnectionStringName;
        }
        public override async Task Register()
        {
            Func<TRequest, CancellationToken, Task<MQResponseResult<TResponse>>> responder = async (request, cancellationToken) =>
            {
                var ret = new MQResponseResult<TResponse>();
                var req = request as IMQMessage;
                ret.MessageId = req?.MQMeta?.MessageId;
                try
                {
                    ret.Result = await Respond(request, cancellationToken);
                    ret.Success = true;
                    ret.MessageElasped = GetElaspedTime(req?.MQMeta?.Timestamp);
                    LogUtil.Debug("[MQ] RespondConsumer消费成功。{MQConsumerType}{MQRequestType}{MQMessageId}{MQElaspedTime}"
                        , GetType().FullName, request.GetType().FullName, req?.MQMeta?.MessageId, ret.MessageElasped);
                }
                catch (Exception ex)
                {
                    ret.Success = false;
                    ret.MessageElasped = GetElaspedTime(req?.MQMeta?.Timestamp);
                    var exc = ExceptionUtil.GetException<CustomException>(ex);
                    if (exc != null)
                    {
                        ret.Code = exc.Code;
                        ret.Message = exc.Message;
                    }
                    else
                    {
                        ret.Code = GResponseCodes.G_INTERNAL_SERVER_ERROR;
                        ret.Message = ex.Message;
                        ret.Exception = ex;
                        LogUtil.Error(ex, "[MQ] RespondConsumer消费异常。{MQConsumerType}{MQRequestBody}{MQMessageId}{MQElaspedTime}"
                            , GetType().FullName, SerializerUtil.SerializeJson(request), req?.MQMeta?.MessageId, ret.MessageElasped);
                    }
                }
                return ret;
            };
            Action<IResponderConfiguration> configAction = (x) =>
            {
                Configuration(x);
            };
            _dispos = await Bus.Rpc.RespondAsync(responder, configAction);
        }

        /// <summary>
        /// 配置设置（主要考虑设置QueueName）
        /// </summary>
        /// <param name="x"></param>
        protected abstract void Configuration(IResponderConfiguration x);
        protected abstract Task<TResponse> Respond(TRequest request, CancellationToken cancellationToken);
        public override void Dispose()
        {
            base.Dispose();
            _dispos?.Dispose();
        }
    }
}
