using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Common;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx.Extensions.IDGenerator.Common
{
    internal class IDGeneratorHostedService : BackgroundService
    {
        private IDGeneratorSection _section;
        private IWorkerIdProvider _provider;
        private TimeSpan _intervalSpan;
        public IDGeneratorHostedService()
        {
            _section = ConfigUtil.GetSection<IDGeneratorSection>();
            _provider = IDGeneratorUtil.WorkerIdProvider;
            _intervalSpan = TimeSpan.FromMinutes(_section.RedisExpireMinutes / 3);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //定时刷新机器id的存活状态
                await _provider.Active();
                await Task.Delay(_intervalSpan, stoppingToken);
            }
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _provider.Dispose();
            await base.StopAsync(cancellationToken);
        }
    }
}
