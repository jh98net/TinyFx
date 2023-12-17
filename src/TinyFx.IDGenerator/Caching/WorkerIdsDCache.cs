using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.IDGenerator.Caching
{
    internal class WorkerIdsDCache : RedisStringClient<WorkerIdsDO>
    {
        private IDGeneratorSection _section;
        private int EXPIRE_MINUTES;
        public WorkerIdsDCache(int workerId)
        {
            _section = ConfigUtil.GetSection<IDGeneratorSection>();
            Options.ConnectionStringName = _section.RedisConnectionStringName;
            RedisKey = $"{RedisPrefixConst.ID_GENERATOR}:WorkerIds:{workerId}";
            EXPIRE_MINUTES = ConfigUtil.IsDebugEnvironment
                ? (int)TimeSpan.FromHours(1).TotalMinutes : _section.RedisExpireMinutes;
        }
        public async Task SetWorkerIdsDo(WorkerIdsDO value)
        {
            await SetAndExpireMinutesAsync(value, EXPIRE_MINUTES);
        }
        public async Task Active()
        {
            await KeyExpireMinutesAsync(EXPIRE_MINUTES);
        }
    }
    internal class WorkerIdsDO
    {
        public string ip { get; set; }
        public int pid { get; set; }
        public string env { get; set; }
    }
}
