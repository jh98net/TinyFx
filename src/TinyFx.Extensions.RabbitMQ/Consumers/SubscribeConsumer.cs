using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;

namespace TinyFx.Extensions.RabbitMQ
{
    /// <summary>
    /// 消息订阅(Publish => Subscribe)模式基类
    ///     子类继承sub结尾
    ///     对应MQUtil.Publish方法
    /// </summary>
    /// <typeparam name="TMessage">接收的消息类型</typeparam>
    public abstract class SubscribeConsumer<TMessage> : ConsumerBase
        where TMessage : class
    {
        private string _subIdSuffix = StringUtil.GetGuidString();
        /// <summary>
        /// 是否广播模式
        /// true:  广播到所有Consumer(广播)
        /// false: 轮询到每一个Consumer(负载)
        /// </summary>
        protected abstract bool IsBroadcast { get; }
        private string _subId;
        /// <summary>
        /// 订阅ID。默认值: consumerTypeName:suffix
        ///     相同subId: 轮流接收消息（负载均衡）
        ///     不同subId: 同时接收消息（广播）
        /// </summary>
        protected string SubId
        {
            get
            {
                if (string.IsNullOrEmpty(_subId))
                {
                    _subId = IsBroadcast
                        ? $"{GetType().FullName}:{_subIdSuffix}"
                        : $"{GetType().FullName}";
                }
                return _subId;
            }
            set { _subId = value; }
        }
        private SubscriptionResult? _subResult;
        public override void Register()
        {
            Func<TMessage, CancellationToken, Task> onMessage = async (msg, cancellationToken) =>
            {
                await OnMessage(msg, cancellationToken).ContinueWith(task => {
                    if (task.IsCompleted && !task.IsFaulted)
                    {
                        // Everything worked out ok
                    }
                    else
                    {
                        var errMsg = $"SubscribeConsumer异常。type:{GetType().FullName} subId:{SubId} msg:{msg}";
                        LogUtil.Error(task.Exception, errMsg);
                        // 不要catch，此异常将导致被发送到默认错误代理队列 error queue (broker)
                        throw new EasyNetQException(errMsg);
                    }
                });
            };
            _subResult = Bus.PubSub.Subscribe(SubId, onMessage, Configuration);
        }
        protected abstract Task OnMessage(TMessage message, CancellationToken cancellationToken);


        protected abstract void Configuration(ISubscriptionConfiguration x);
        public override void Dispose()
        {
            _subResult?.Dispose();
        }
    }
}
