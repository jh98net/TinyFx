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
        Task Register();
        Task Unregister();
        Task Heartbeat();
        Task Health();
        Task<List<string>> GetAllServiceIds(string connectionStringName = null);
        Task SetHostData<T>(string key, T value, string serviceId = null, string connectionStringName = null);
        Task<CacheValue<T>> GetHostData<T>(string key, string serviceId = null, string connectionStringName = null);
    }
}
