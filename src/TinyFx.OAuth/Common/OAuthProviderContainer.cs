using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.OAuth.Providers;
using TinyFx.Reflection;

namespace TinyFx.OAuth
{
    internal class OAuthProviderContainer
    {
        private ConcurrentDictionary<OAuthProviders, IOAuthProvider> _dict = new();
        internal void Init()
        {
            var list = EnumUtil.GetInfo<OAuthProviders>();
            foreach (var item in list.GetList())
            {
                var obj = ReflectionUtil.CreateInstance($"TinyFx.OAuth.Providers.{item.Name}Provider") as IOAuthProvider;
                if (obj == null)
                    throw new Exception();
                _dict.TryAdd((OAuthProviders)item.Value, obj);
            }
        }

        public IOAuthProvider GetProvider(OAuthProviders provider)
        {
            if (!_dict.TryGetValue(provider, out var ret))
                throw new Exception("");
            return ret;
        }
    }
}
