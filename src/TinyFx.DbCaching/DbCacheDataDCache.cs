using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Data.SqlSugar;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.DbCaching
{
    internal class DbCacheDataDCache : RedisHashClient<string>
    {
        public DbCacheDataDCache()
        {
            RedisKey = GetGlobalRedisKey();
        }
        protected override async Task<CacheValue<string>> LoadValueWhenRedisNotExistsAsync(string field)
        {
            var ret = new CacheValue<string>();
            var keys = DbCachingUtil.ParseCacheKey(field);
            var list = await DbUtil.GetDb(keys.ConfigId).Queryable<object>()
                .AS(keys.TableName).ToListAsync() ?? new List<object>();
            ret.Value = SerializerUtil.SerializeJson(list);
            ret.HasValue = true;
            return ret;
        }
    }
}
