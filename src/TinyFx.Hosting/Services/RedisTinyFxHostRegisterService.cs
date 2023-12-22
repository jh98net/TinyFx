using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Hosting.Services
{
    public class RedisTinyFxHostRegisterService : ITinyFxHostRegisterService
    {
        public Task Register()
        {
            return Task.CompletedTask;
        }

        public Task Heartbeat()
        {
            return Task.CompletedTask;
        }

        public Task Unregister()
        {
            return Task.CompletedTask;
        }
    }
}
