using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Logging;

namespace TinyFx.Extensions.RabbitMQ
{
    /// <summary>
    /// 发送接收(Send=>Receive)模式基类(命令管道，可处理多个不同消息类型)
    ///     子类继承Rec结尾
    ///     对应MQUtil.Send方法
    /// </summary>
    public abstract class ReceiveConsumer : ConsumerBase
    {
        /// <summary>
        /// 对应MQMessageAttribute的QueueName
        /// </summary>
        public abstract string QueueName { get; }
        private IDisposable _dispos;
        public override void Register()
        {
            _dispos = Bus.SendReceive.Receive(QueueName, OnMessage, Configuration);
        }
        protected abstract void OnMessage(IReceiveRegistration addHandlers);
        protected abstract void Configuration(IReceiveConfiguration x);
        
        public override void Dispose()
        {
            _dispos?.Dispose();
        }
    }
}
