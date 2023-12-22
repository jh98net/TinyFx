using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.IDGenerator.Caching;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Net;
using TinyFx.Logging;

namespace TinyFx.IDGenerator.Common
{
    internal class RedisWorkerIdProvider : IWorkerIdProvider
    {
        private int _maxWorkerId;
        private WorkerIdCurrentDCache _idDCache;
        private WorkerIdsDO _workerDo;

        private IDGeneratorSection _section;

        public int WorkerId;
        public RedisWorkerIdProvider()
        {
            _section = ConfigUtil.GetSection<IDGeneratorSection>();
            _maxWorkerId = 1 << _section.WorkerIdBits;
            _idDCache = WorkerIdCurrentDCache.Create();
            _workerDo = new WorkerIdsDO()
            {
                ip = NetUtil.GetLocalIP(),
                pid = Process.GetCurrentProcess().Id,
                env = ConfigUtil.EnvironmentString
            };
        }

        public async Task<int> GetNextWorkId()
        {
            using (var redlock = await RedisUtil.LockAsync("IDGeneratorWorkerId", 20))
            {
                if (!redlock.IsLocked)
                {
                    redlock.Release();
                    throw new Exception("Redis没有能够获取WorkerId分布式锁");
                }
                for (int i = 0; i < _maxWorkerId; i++)
                {
                    var workerId = await _idDCache.GetNextWorkId() - 1;
                    if (workerId >= _maxWorkerId)
                    {
                        await _idDCache.SetAsync(0);
                        workerId = 0;
                    }
                    var dcache = new WorkerIdsDCache(workerId);
                    if (await dcache.KeyExistsAsync())
                        continue;
                    await dcache.SetWorkerIdsDo(_workerDo);
                    WorkerId = workerId;
                    return workerId;
                }
            }
            throw new Exception("Redis没有足够的WorkerId分配");
        }
        public Task Active()
        {
            new WorkerIdsDCache(WorkerId).Active();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            new WorkerIdsDCache(WorkerId).KeyDeleteAsync().GetTaskResult();
        }
    }
}
