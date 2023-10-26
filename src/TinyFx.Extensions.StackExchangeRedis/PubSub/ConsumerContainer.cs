using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Logging;
using TinyFx.Reflection;

namespace TinyFx.Extensions.StackExchangeRedis
{
    internal class ConsumerContainer
    {
        private readonly List<object> _list = new();
        public ConsumerContainer(List<string> asms)
        {
            foreach (var asm in asms)
            {
                if (string.IsNullOrEmpty(asm))
                    continue;
                var msg = $"加载配置文件Redis:ConsumerAssemblies中项失败。name:{asm}";

                var ignoreAssemblyError = asm.StartsWith('+');
                var file = asm.TrimStart('+');
                var types = from t in ReflectionUtil.GetAssemblyTypes(file, ignoreAssemblyError, msg)
                                .Where(x => x.GetCustomAttribute<RedisConsumerRegisterIgnoreAttribute>() == null)
                            where t.IsSubclassOfGeneric(typeof(RedisSubscribeConsumer<>))
                                || t.IsSubclassOfGeneric(typeof(RedisQueueConsumer<>))
                            select t;
                foreach (var type in types)
                {
                    var obj = ReflectionUtil.CreateInstance(type);
                    ((IRedisConsumer)obj).Register();
                    _list.Add(obj);
                }
            }
        }
    }
}
