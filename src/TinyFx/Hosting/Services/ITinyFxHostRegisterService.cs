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
        bool RegisterEnabled { get; }
        void AddProvider(ITinyFxHostRegisterProvider provider);
        Task Register();
        Task Deregister();
        Task DeregisterExternal();
        Task Heartbeat();
        Task Health();
    }
    internal class DefaultHostRegisterService : ITinyFxHostRegisterService
    {
        private List<ITinyFxHostRegisterProvider> _providers = new();

        public bool RegisterEnabled => _providers.Count > 0;

        public void AddProvider(ITinyFxHostRegisterProvider provider)
        {
            _providers.Add(provider);
        }
        public async Task Register()
        {
            _providers.ForEach(async x => await x.Register());
        }
        public async Task DeregisterExternal()
        {
            _providers.ForEach(async x =>
            {
                if (x.IsExternal)
                    await x.Deregister();
            });
        }
        public async Task Deregister()
        {
            _providers.ForEach(async x =>
            {
                if (!x.IsExternal)
                    await x.Deregister();
            });
        }
        public async Task Heartbeat()
        {
            _providers.ForEach(async x => await x.Heartbeat());
        }
        public async Task Health()
        {
            _providers.ForEach(async x => await x.Health());
        }
    }
    public interface ITinyFxHostRegisterProvider
    {
        bool IsExternal { get; }
        Task Register();
        Task Deregister();
        Task Heartbeat();
        Task Health();
    }
}
