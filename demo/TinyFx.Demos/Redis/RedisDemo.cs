using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Extensions.StackExchangeRedis;

namespace TinyFx.Demos.Redis
{
    internal class RedisDemo : DemoBase
    {
        public override void Execute()
        {
            var cache = new DemoHashDCache();
            cache.Set("a", new TestInfo { Id=1,Name="aa"});
            Console.WriteLine(cache.GetOrDefault("a", null).Name);
        }
    }
    class DemoHashDCache : RedisHashClient<TestInfo>
    {
        
    }
    class TestInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
