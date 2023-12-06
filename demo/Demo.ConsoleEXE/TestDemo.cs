using TinyFx.ShortId;

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var id = ShortIdUtil.Generate(15);
        }
    }
}
