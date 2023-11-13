using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.DbCaching;

namespace TinyFx.AspNet.Hosting
{
    public abstract class DbCachingCorsProvider<TEntity> : ICorsPoliciesProvider
        where TEntity : class, new()
    {
        public DbCachingCorsProvider()
        {
        }
        protected void UpdateCallback(List<TEntity> oldList, List<TEntity> newList)
        {
            var section = ConfigUtil.GetSection<CorsSection>();
            var policies = GetPolicies(newList);
            var opts = DIUtil.GetRequiredService<IOptions<CorsOptions>>();
            foreach (var policy in policies)
            {
                if (policy.Name == section.UseCors.DefaultPolicy)
                {
                    opts.Value.AddDefaultPolicy(AspNetUtil.GetPolicyBuilder(policy));
                }
                else
                {
                    opts.Value.AddPolicy(policy.Name, AspNetUtil.GetPolicyBuilder(policy));
                }
            }
        }
        protected virtual object[] GetSplitDbKeys() => null;
        protected abstract List<CorsPolicyElement> GetPolicies(List<TEntity> list);

        private DbCacheMemory<TEntity> _cache;
        private DbCacheMemory<TEntity> GetCache()
        {
            if (_cache == null)
            {
                _cache = DbCachingUtil.GetNamedCache<TEntity>(this.GetType().FullName, GetSplitDbKeys());
                _cache.UpdateCallback = UpdateCallback;
            }
            return _cache;
        }
        public async Task<List<CorsPolicyElement>> GetPoliciesAsync()
        {
            var list = GetCache().GetAllList();
            var ret = GetPolicies(list);
            return ret;
        }
    }
}
