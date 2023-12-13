using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.RabbitMQ;

namespace TinyFx.DbCaching.ChangeConsumers
{
    [MQConsumerIgnore]
    internal class MQDbCacheChangeConsumer : MQSubscribeConsumer<DbCacheChangeMessage>
    {
        public override MQSubscribeMode SubscribeMode => MQSubscribeMode.Multicast;
        private string _connectionStringName;
        private DbCachingUpdator _uploader;
        public MQDbCacheChangeConsumer(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
            _uploader = new DbCachingUpdator(DbCachingPublishMode.MQ);
        }
        protected override string GetConnectionStringName()
        {
            return _connectionStringName;
        }

        protected override void Configuration(ISubscriptionConfiguration x)
        {
            x.WithPrefetchCount(1);
        }

        protected override async Task OnMessage(DbCacheChangeMessage message, CancellationToken cancellationToken)
        {
            await _uploader.Execute(message);
        }
    }
}
