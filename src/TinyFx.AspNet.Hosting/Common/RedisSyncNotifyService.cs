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
        public RedisSyncNotifyService(string connectionStringName = null)
        {
            _client = RedisUtil.CreateBitClient(RedisPrefixConst.SYNC_NOTIFY, connectionStringName);
        }
        public async Task SetNotify(string id, bool value)
        {
            var offset = GetOffset(id);
            await _client.SetBitAsync(offset, value);
        }
        public async Task<bool> GetNotify(string id)
        {
            var offset = GetOffset(id);
            return await _client.GetBitAsync(offset);
        }
        private long GetOffset(string id)
        {
            var code = id.GetHashCode();
            var offset = (long)code + int.MaxValue + 1;
            return offset;
        }
    }
}
