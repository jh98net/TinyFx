using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Collections;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Logging;

namespace TinyFx.DbCaching.ChangeConsumers
{
    [RedisConsumerRegisterIgnore]
    internal class RedisDbCacheChangeConsumer : RedisSubscribeConsumer<DbCacheChangeMessage>
    {
        private DbCachingUpdator _executor;
        public RedisDbCacheChangeConsumer(string redisConnectionStringName)
        {
            ConnectionStringName = redisConnectionStringName;
            _executor = new DbCachingUpdator(DbCachingPublishMode.Redis);
        }
        protected override async Task OnMessage(DbCacheChangeMessage message)
        {
            await _executor.Execute(message);
        }
        protected override Task OnError(DbCacheChangeMessage message, Exception ex)
        {
            return Task.CompletedTask;
        }
    }
}
