using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Reflection;

namespace TinyFx.Extensions.StackExchangeRedis.PubSub
{
    internal class ConsumerContainer
    {
        private readonly List<object> _list = new();
        public ConsumerContainer(List<string> asms) 
        { 
            foreach(var asm in asms)
            {
                if (string.IsNullOrEmpty(asm))
                    continue;
                var types = from t in GetAssemblyTypes(asm)
                            where t.IsSubclassOfGeneric(typeof(RedisSubscribeConsumer<>))
                                || t.IsSubclassOfGeneric(typeof(RedisQueueConsumer<>))
                            select t;
                foreach (var type in types)
                {
                    _list.Add(ReflectionUtil.CreateInstance(type));
                }
            }
        }
        private static Type[] GetAssemblyTypes(string asm)
        {
            if (string.IsNullOrEmpty(asm) || !asm.EndsWith(".dll"))
                throw new Exception($"配置文件Redis:ConsumerAssemblies中的项不能为空并且需要以.dll结尾。");
            var file = asm;
            if (!File.Exists(file))
                file = Path.Combine(AppContext.BaseDirectory, asm);
            if (!File.Exists(file))
                throw new Exception($"配置文件Redis:ConsumerAssemblies中{asm}不存在:{file}");
            return Assembly.LoadFrom(file).GetTypes();
        }
    }
}
