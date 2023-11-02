using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.DbCaching;

namespace TinyFx.AspNet.Hosting
{
    public abstract class DbCachingCorsProvider<TEntity> : ICorsPoliciesProvider
        where TEntity : class, new()
    {
        private DbCacheMemory<TEntity> _cache;
        public DbCachingCorsProvider(params object[] routingDbKeys)
        {
            _cache = new DbCacheMemory<TEntity>(routingDbKeys);
        }
        public abstract Task<List<CorsPolicyElement>> GetPoliciesAsync();


    }
}
