using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.AWS.LoadBalancing;
using TinyFx.Hosting.Services;

namespace TinyFx.Extensions.AWS.Common
{
    internal class AwsHostRegisterProvider : ITinyFxHostRegisterProvider
    {
        private TargetGroupRegisterService _targetGroupRegistor;

        public bool IsExternal => true;

        public AwsHostRegisterProvider()
        {
            _targetGroupRegistor = new TargetGroupRegisterService();
        }
        public async Task Register()
        {
            await _targetGroupRegistor.Register();
        }

        public async Task Deregister()
        {
            await _targetGroupRegistor.Deregister();
        }

        public async Task Heartbeat()
        {
        }

        public async Task Health()
        {
        }
    }
}
