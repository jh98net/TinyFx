using Microsoft.Extensions.Configuration;
using TinyFx.OAuth;
using TinyFx.Reflection;

namespace TinyFx.Configuration
{
    public class OAuthSection : ConfigSection
    {
        public override string SectionName => "OAuth";
        public string ProvidersProvider { get; set; }
        /// <summary>
        /// redis连接集合
        /// </summary>
        public Dictionary<string, IOAuthProviderElement> Providers { get; set; }

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            if (!string.IsNullOrEmpty(ProvidersProvider))
            {
                var prov = ReflectionUtil.CreateInstance(ProvidersProvider) as IOAuthProvidersProvider;
                if (prov == null)
                    throw new Exception($"配置文件OAuth:ProvidersProvider必须继承IOAuthProvidersProvider: {ProvidersProvider}");
                Providers = prov.GetProvidersAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            else
            {
                Providers = configuration
                    .Get<Dictionary<string, IOAuthProviderElement>>() ?? new();
            }
        }
    }
}
namespace TinyFx.OAuth
{
    public interface IOAuthProviderElement
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
    public class OAuthProviderElement : IOAuthProviderElement
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
