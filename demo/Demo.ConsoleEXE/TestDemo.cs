using Demo.ConsoleEXE;
using Demo.ConsoleEXE.DAL;
using TinyFx.Common;
using TinyFx.DbCaching;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.IP2Country;
using TinyFx.Randoms;
using TinyFx.ShortId;
using TinyFx.Text;

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
        }
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
