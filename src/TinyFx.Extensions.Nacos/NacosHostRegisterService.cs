using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Hosting.Services;

namespace TinyFx.Extensions.Nacos
{
    public class NacosHostRegisterService : ITinyFxHostRegisterService
    {
        public bool UseHeartbeat => false;
        private const string METADATA_PREFIX = "TINYFX:";
        public async Task Register()
        {
        }
        public async Task Unregister()
        {
        }

        #region NotImplementedException
        public Task Health()
        {
            throw new NotImplementedException();
        }

        public Task Heartbeat()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
