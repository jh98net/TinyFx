using Demo.ConsoleEXE;
using TinyFx.ShortId;

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var a =DbCacheUtil.GetApp("best_shooter");
        }
    }
}
