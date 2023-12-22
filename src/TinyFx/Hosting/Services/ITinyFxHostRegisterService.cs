using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Hosting.Services
{
    public interface ITinyFxHostRegisterService
    {
        Task Register();
        Task Unregister();
        Task Heartbeat();
    }
}
