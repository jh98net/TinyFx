using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Extensions.StackExchangeRedis.Clients;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.AspNet.Hosting
{
    public class RedisSyncNotifyService : ISyncNotifyService
    {
        private RedisBitClient _client;
        public RedisSyncNotifyService()
        {
            _client = RedisUtil.CreateBitClient(RedisPrefixConst.SYNC_NOTIFY);
        }
        public async Task SetNotify(string id, bool value)
        {
            await _client.SetBitAsync(id, value);
        }
        public async Task<bool> GetNotify(string id)
        {
            return await _client.GetBitAsync(id);
        }
    }
}
