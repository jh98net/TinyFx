using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.IDGenerator.Caching;

namespace TinyFx.Extensions.IDGenerator.Common
{
    internal class ConfigWorkerIdProvider : IWorkerIdProvider
    {
        private IDGeneratorSection _section;
        public ConfigWorkerIdProvider() 
        {
            _section = ConfigUtil.GetSection<IDGeneratorSection>();
        }
        public Task Active()
        {
            return Task.CompletedTask;
        }

        public Task<int> GetNextWorkId()
        {
            return Task.FromResult(_section.WorkerId);
        }

        public void Dispose()
        {
        }
    }
}
