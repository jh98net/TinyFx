using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;

namespace TinyFx.Hosting.Services
{
    public class RedisTinyFxHostDataService : ITinyFxHostDataService
    {
        public Task<CacheValue<object>> GetData(string field)
        {
            throw new NotImplementedException();
        }

        public Task SetData(string field, object value)
        {
            throw new NotImplementedException();
        }
    }
}
