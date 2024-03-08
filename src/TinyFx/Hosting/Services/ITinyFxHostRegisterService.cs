using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;

namespace TinyFx.Hosting.Services
{
    /// <summary>
    /// host注册服务
    /// </summary>
    public interface ITinyFxHostRegisterService
    {
        bool UseHeartbeat { get; }
        Task Register();
        Task Unregister();
        Task Heartbeat();
        Task Health();
    }
}
