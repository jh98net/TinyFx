using Demo.ConsoleEXE;
using Demo.ConsoleEXE.DAL;
using TinyFx.DbCaching;
using TinyFx.ShortId;

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var ret = DbCachingUtil.GetSingle<Ss_appEO>(it => new { it.ProviderID, it.ProviderAppId }, new Ss_appEO
            {
                ProviderID = "pgsoft",
                ProviderAppId = "126"
            });

        }
    }
}
