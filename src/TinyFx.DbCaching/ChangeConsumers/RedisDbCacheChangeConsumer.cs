using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.DbCaching.ChangeConsumers
{
    [RedisConsumerRegisterIgnore]
    internal class RedisDbCacheChangeConsumer : RedisSubscribeConsumer<DbCacheChangeMessage>
    {
        private DbCachingUpdator _uploader;
        public RedisDbCacheChangeConsumer(string redisConnectionStringName)
        {
            ConnectionStringName = redisConnectionStringName;
            _uploader = new DbCachingUpdator(DbCachingPublishMode.Redis);
        }
        protected override async Task OnMessage(DbCacheChangeMessage message)
        {
            await _uploader.Execute(message);
        }
        protected override Task OnError(DbCacheChangeMessage message, Exception ex)
        {
            return Task.CompletedTask;
        }
    }
}
