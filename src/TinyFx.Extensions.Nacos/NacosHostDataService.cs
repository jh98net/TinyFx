using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Hosting.Services;

namespace TinyFx.Extensions.Nacos
{
    public class NacosHostDataService: ITinyFxHostDataService
    {
        public NacosHostDataService() { }
        public Task<CacheValue<T>> GetData<T>(string field)
        {
            throw new NotImplementedException();
        }

        public Task<CacheValue<T>> GetHostData<T>(string serviceId, string field, string connectionStringName = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetHosts(string connectionStringName = null)
        {
            throw new NotImplementedException();
        }

        public Task SetData<T>(string field, T value)
        {
            throw new NotImplementedException();
        }

        public Task SetHostData<T>(string serviceId, string field, T value, string connectionStringName = null)
        {
            throw new NotImplementedException();
        }
    }
}
