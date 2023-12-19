using Demo.ConsoleEXE;
using Demo.ConsoleEXE.DAL;
using TinyFx.DbCaching;
using TinyFx.IP2Country;
using TinyFx.ShortId;

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var ip = IP2CountryUtil.GetContryId("123.125.255.133");

        }
    }
}
