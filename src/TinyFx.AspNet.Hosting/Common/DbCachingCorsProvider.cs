using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Collections;
using TinyFx.Configuration;
using TinyFx.DbCaching;

namespace TinyFx.AspNet.Hosting
{
    public abstract class DbCachingCorsProvider<TEntity> : ICorsPoliciesProvider
        where TEntity : class, new()
    {
        protected virtual object[] GetSplitDbKeys() => null;
        protected abstract List<CorsPolicyElement> GetPolicies(List<TEntity> list);

        public List<CorsPolicyElement> GetPolicies()
        {
            var list = GetCache().GetAllList();
            var ret = GetPolicies(list);
            return ret;
        }

        private DbCacheMemory<TEntity> _cache;
        private DbCacheMemory<TEntity> GetCache()
        {
            if (_cache == null)
            {
                _cache = DbCachingUtil.GetNamedCache<TEntity>(null, GetSplitDbKeys());
            }
            return _cache;
        }

        public void SetAutoRefresh()
        {
            GetCache().UpdateCallback = UpdateCallback;
        }
        protected void UpdateCallback(List<TEntity> oldList, List<TEntity> newList)
        {
            var opts = DIUtil.GetRequiredService<IOptions<CorsOptions>>();
            var section = ConfigUtil.GetSection<CorsSection>();

            //
            var policies = new Dictionary<string, CorsPolicyElement>();
            foreach (var item in section.Policies)
            {
                policies.Add(item.Key, item.Value);
            }

            //
            var newPolicies = GetPolicies(newList);
            newPolicies.ForEach(x =>
            {
                x.Name ??= "default";
                if (policies.ContainsKey(x.Name))
                {
                    var oldOrigins = policies[x.Name].Origins.Trim().TrimEnd(';');
                    var newOrigins = $"{oldOrigins};{x.Origins?.Trim()}";
                    var originsSet = newOrigins.Split(';', StringSplitOptions.RemoveEmptyEntries).ToHashSet();
                    policies[x.Name].Origins = string.Join(';', originsSet);
                }
                else
                {
                    policies.Add(x.Name, x);
                }
            });

            //
            foreach (var policy in policies.Values)
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
    }
}
