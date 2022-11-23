using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;

namespace TinyFx.Extensions.RabbitMQ
{
    public abstract class ConsumerBase: IConsumer
    {
        /// <summary>
        /// MQ链接字符串,不使用默认则子类需重写
        /// </summary>
        public virtual string ConnectionStringName { get; }

        public IBus Bus => MQUtil.CreateBus(ConnectionStringName);
        public ConsumerBase()
        {
        }
        public abstract void Register();

        public virtual void Dispose() 
        { }
    }
    public interface IConsumer : IDisposable
    {
        string ConnectionStringName { get; }
        IBus Bus { get; }
        void Register();
    }
}
