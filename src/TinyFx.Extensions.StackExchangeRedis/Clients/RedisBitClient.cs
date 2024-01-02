using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.StackExchangeRedis.Clients
{
    /// <summary>
    /// Redis string结构Bit操作,可以被继承
    /// </summary>
    public class RedisBitClient : RedisClientBase
    {
        public override RedisType RedisType => RedisType.String;
        public RedisBitClient(object key = null, RedisClientOptions options = null) : base(key, options) { }

        public async Task<bool> GetBitAsync(long offset, CommandFlags flags = CommandFlags.None)
            => await Database.StringGetBitAsync(RedisKey, offset, flags);
        public async Task SetBitAsync(long offset, bool bit, CommandFlags flags = CommandFlags.None)
            => await Database.StringSetBitAsync(RedisKey, offset, bit, flags);

        /// <summary>
        /// 获取bit值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<bool> GetBitAsync(string id, CommandFlags flags = CommandFlags.None)
            => await GetBitAsync(GetOffset(id), flags);

        /// <summary>
        /// 设置bit值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bit"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task SetBitAsync(string id, bool bit, CommandFlags flags = CommandFlags.None)
            => await SetBitAsync(GetOffset(id), bit, flags);

        private long GetOffset(string id)
        {
            var code = id.GetHashCode();
            var offset = (long)code + int.MaxValue + 1;
            return offset;
        }
    }
}
