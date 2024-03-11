using Demo.ConsoleEXE;
using Demo.ConsoleEXE.DAL;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Common;
using TinyFx.Data;
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
            var list = new List<string>()
            {
                "0.0.0.0:9200->9200/tcp, :::9200->9200/tcp, 0.0.0.0:9300->9300/tcp, :::9300->9300/tcp",
                "0.0.0.0:5601->5601/tcp, :::5601->5601/tcp",
                "4369/tcp, 5671/tcp, 0.0.0.0:5672->5672/tcp, :::5672->5672/tcp, 15671/tcp, 15691-15692/tcp, 25672/tcp, 0.0.0.0:15672->15672/tcp, :::15672->15672/tcp",
                "0.0.0.0:7848->7848/tcp, :::7848->7848/tcp, 0.0.0.0:8848->8848/tcp, :::8848->8848/tcp, 0.0.0.0:9848-9849->9848-9849/tcp, :::9848-9849->9848-9849/tcp",
                "0.0.0.0:8081->8081/tcp, :::8081->8081/tcp",
            };
            foreach(var aaa  in list)
            {
                var port = aaa;
                if (!string.IsNullOrEmpty(port))
                {
                    var portSet = new HashSet<string>();
                    foreach (var item in port.Split(','))
                    {
                        var it = item.Trim().TrimEnd("/tcp").TrimStart("0.0.0.0:").TrimStart(":::");
                        var idx = it.IndexOf("->");
                        if (idx > 0)
                            it = it.Substring(0, idx);
                        if (!portSet.Contains(it))
                            portSet.Add(it);
                    }
                    port = string.Join('|', portSet);
                }
                Console.WriteLine(port);
            }
        }
    }

    public class UserInfo
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
    }
}
