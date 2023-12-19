using Demo.ConsoleEXE;
using Demo.ConsoleEXE.DAL;
using TinyFx.Common;
using TinyFx.DbCaching;
using TinyFx.IP2Country;
using TinyFx.ShortId;

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var tmpl = "TEST{{UserId}}ASDF{{Name1}}";
            var str= new StringTemplateReplacer(tmpl).Set(new UserInfo 
            {
                UserId=2,
                Name="===="
            })
                .ToString();
            Console.WriteLine(str);
        }
    }
    public class UserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
