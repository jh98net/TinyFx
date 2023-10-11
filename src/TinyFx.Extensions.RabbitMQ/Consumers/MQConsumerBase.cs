using EasyNetQ;
using EasyNetQ.ConnectionString;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;

namespace TinyFx.Extensions.RabbitMQ
{
    public abstract class MQConsumerBase : IMQConsumer
    {
        public MQBusData BusData { get; }
        protected IBus Bus => BusData!.Bus;
        protected MQConnectionStringElement ConnectionStringElement => BusData!.Element;

        public MQConsumerBase()
        {
            var connStrName = GetConnectionStringName();
            BusData = MQUtil.GetBusData(connStrName);
        }
        /// <summary>
        /// MQ链接字符串,不使用默认则子类需重写
        /// </summary>
        /// <returns></returns>
        protected virtual string GetConnectionStringName() { return null; }
        public abstract Task Register();

        public virtual void Dispose()
        {
        }

        protected long GetElaspedTime(long? beginTimestamp)
            => beginTimestamp.HasValue ? DateTime.UtcNow.UtcDateTimeToTimestamp(false) - beginTimestamp.Value : 0;
    }
    public interface IMQConsumer : IDisposable
    {
        Task Register();
    }
}
