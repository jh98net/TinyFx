using EasyNetQ;
using EasyNetQ.ConnectionString;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Net;
using TinyFx.Reflection;
using TinyFx.Collections;
using TinyFx.Logging;

namespace TinyFx.Extensions.RabbitMQ
{
    internal class MQContainer
    {
        private RabbitMQSection _section;
        private readonly ConcurrentDictionary<string, IBus> _busCache = new();
        private readonly ConcurrentDictionary<Type, MQMessageAttribute> _messageCache = new();
        private readonly ConcurrentDictionary<Type, IConsumer> _consumerCache = new();
        private EasyNetQ.ISerializer _serializer = new DefaultSerializer();
        private ConnectionStringParser _connParser = new();

        public MQContainer()
        {
            _section = ConfigUtil.GetSection<RabbitMQSection>();
            if (_section == null || _section.ConnectionStrings == null || _section.ConnectionStrings.Count == 0)
                throw new Exception("RabbitMQ配置节不能为空");
        }
        #region Init
        internal void Init()
        {
            // Bus
            InitBusCache(_section);
            // Message
            InitMessageCache(_section);
            // Consumer
            InitConsumerCache(_section);
        }
        private void InitBusCache(RabbitMQSection section)
        {
            foreach (var conn in section.ConnectionStrings.Values)
            {
                if (string.IsNullOrEmpty(conn.ConnectionString))
                    throw new Exception($"配置文件RabbitMQ:ConnectionStrings:ConnectionString不能为空。Name:{conn.Name}");
                var connConfig = _connParser.Parse(conn.ConnectionString);
                connConfig.Product = ConfigUtil.Project.ProjectId;
                connConfig.Platform = NetUtil.GetLocalIP();
                //var bus = RabbitHutch.CreateBus(connConfig, x => x.Register(_serializer));
                var bus = RabbitHutch.CreateBus(connConfig, register => {
                    register.Register(typeof(EasyNetQ.Logging.ILogger<>), typeof(MQLogger<>));
                    register.Register(typeof(EasyNetQ.ISerializer), typeof(DefaultSerializer));
                });
                if (!_busCache.TryAdd(conn.Name, bus))
                    throw new Exception($"配置文件RabbitMQ:ConnectionStrings:Name重复: {conn.Name}");
            }
        }
        private void InitMessageCache(RabbitMQSection section)
        {
            if (section == null || section.MessageAssemblies == null || section.MessageAssemblies.Count == 0)
                return;
            foreach (var asm in section.MessageAssemblies)
            {
                foreach (var type in GetAssemblyTypes(asm, true))
                {
                    var attr = type.GetCustomAttribute<MQMessageAttribute>();
                    if (attr != null)
                    {
                        _messageCache.TryAdd(type, attr);
                    }
                }
            }
        }
        private void InitConsumerCache(RabbitMQSection section)
        {
            if (section.ConsumerAssemblies == null || section.ConsumerAssemblies.Count == 0)
                return;
            foreach (var asm in section.ConsumerAssemblies)
            {
                var types = from t in GetAssemblyTypes(asm, false)
                            where t.IsSubclassOfGeneric(typeof(SubscribeConsumer<>))
                                || t.IsSubclassOfGeneric(typeof(RespondConsumer<,>))
                                || t.IsSubclassOfGeneric(typeof(ReceiveConsumer))
                            select t;
                foreach (var type in types)
                {
                    IConsumer obj = (IConsumer)ReflectionUtil.CreateInstance(type);
                    obj.Register();
                    _consumerCache.TryAdd(obj.GetType(), obj);
                }
            }
        }
        #endregion

        #region Utils
        private static Type[] GetAssemblyTypes(string asm, bool isMsg)
        {
            var name = "RabbitMQ:" + (isMsg ? "MessageAssemblies" : "ConsumerAssemblies");
            if (string.IsNullOrEmpty(asm) || !asm.EndsWith(".dll"))
                throw new Exception($"配置文件{name}中的项不能为空并且需要以.dll结尾。");
            var file = asm;
            if (!File.Exists(file))
                file = Path.Combine(AppContext.BaseDirectory, asm);
            if (!File.Exists(file))
                throw new Exception($"配置文件{name}中{asm}不存在:{file}");
            return Assembly.LoadFrom(file).GetTypes();
        }
        #endregion

        #region Method
        internal MQMessageAttribute GetMessageAttribute<T>()
        {
            return _messageCache.GetOrAdd(typeof(T), (t) =>
            {
                var ret = t.GetCustomAttribute<MQMessageAttribute>();
                if (ret == null)
                    throw new Exception($"类型{t.FullName}没有定义MQMessageAttribute");
                return ret;
            });
        }
        internal IBus GetBus(string connectionStringName = null)
        {
            if (string.IsNullOrEmpty(connectionStringName))
                connectionStringName = _section?.DefaultConnectionStringName;
            if (!_busCache.TryGetValue(connectionStringName, out IBus ret))
                throw new Exception($"配置文件RabbitMQ:ConnectionStrings:Name不存在：name={connectionStringName}");
            return ret;
        }
        internal IConsumer GetConsumer<T>()
            where T : IConsumer, new()
        {
            return _consumerCache.GetOrAdd(typeof(T), (t) => {
                return (IConsumer)ReflectionUtil.CreateInstance(t);
            });
        }
        internal bool DisposeConsumer<T>()
            where T : IConsumer, new()
        {
            var ret = _consumerCache.TryRemove(typeof(T), out var consumer);
            if(ret)
                consumer.Dispose();
            return ret;
        }

        internal MQConnectionStringElement GetConnection(string connectionStringName = null)
        {
            if (string.IsNullOrEmpty(connectionStringName))
                connectionStringName = _section.DefaultConnectionStringName;
            return _section.ConnectionStrings[connectionStringName];
        }
        internal void Dispose()
        {
            _busCache.Values.ForEach(bus => bus.Dispose());
            LogUtil.Debug("RabbitMQ释放资源: MQContainer.Dispose()");
        }
        #endregion

    }
}
