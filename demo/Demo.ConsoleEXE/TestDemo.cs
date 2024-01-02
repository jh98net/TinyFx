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
            var client = RedisUtil.CreateStringClient<object>("TEST_SyncNotify");
            var count = 0;
            for (int i = 0; i < 1000000; i++)
            {
                var hashCode = Guid.NewGuid().ToString().GetHashCode();
                var id = (long)hashCode + int.MaxValue + 1;
                var result = await client.GetBitAsync(id);
                if (result)
                    count++;
                await client.SetBitAsync(id, true);
                Console.WriteLine($"{i}=>{count}");
            }
            Console.WriteLine(count);
        }
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
