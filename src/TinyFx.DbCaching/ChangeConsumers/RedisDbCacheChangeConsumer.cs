using TinyFx.Extensions.StackExchangeRedis;

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
