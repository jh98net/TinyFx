﻿using EasyNetQ;
using EasyNetQ.ConnectionString;
using EasyNetQ.DI;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using TinyFx.Collections;
using TinyFx.Configuration;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Net;
using TinyFx.Reflection;

namespace TinyFx.Extensions.RabbitMQ
{
    internal class MQContainer
    {
        private readonly ConcurrentDictionary<string, MQBusData> _busDataDict = new();

        private readonly ConcurrentDictionary<string, IMQConsumer> _consumerDict = new();
        // key: type.FullName + id
        private readonly ConcurrentDictionary<string, IMQSubscribeConsumer> _subConsumerDict = new();

        #region Init
        internal async Task InitAsync()
        {
            Dispose();
            var section = ConfigUtil.GetSection<RabbitMQSection>();
            // Bus
            InitBusDataDict(section);
            // Consumer
            await InitConsumerDict(section);
        }
        private void InitBusDataDict(RabbitMQSection section)
        {
            var connParser = new ConnectionStringParser();
            foreach (var element in section.ConnectionStrings.Values)
            {
                if (string.IsNullOrEmpty(element.ConnectionString))
                    throw new Exception($"配置文件RabbitMQ:ConnectionStrings:ConnectionString不能为空。Name:{element.Name}");
                var data = new MQBusData();
                data.Element = element;
                // conn
                var conn = connParser.Parse(element.ConnectionString);
                if (element.UseEnvironmentVirtualHost && conn.VirtualHost == "/")
                    conn.VirtualHost = ConfigUtil.EnvironmentString;
                conn.Product = ConfigUtil.Project.ProjectId;
                conn.Platform = NetUtil.GetLocalIP();
                data.Connection = conn;
                data.Bus = RabbitHutch.CreateBus(data.Connection, register =>
                {
                    register.EnableSystemTextJson();
                    register.Register(typeof(EasyNetQ.Logging.ILogger<>), typeof(MQLogger<>));
                    if (element.UseShortNaming)
                        register.Register<IConventions>(c => new MQConventions(c.Resolve<ITypeNameSerializer>()));
                });

                if (!_busDataDict.TryAdd(element.Name, data))
                    throw new Exception($"配置文件RabbitMQ:ConnectionStrings:Name重复: {element.Name}");
            }
        }
        private async Task InitConsumerDict(RabbitMQSection section)
        {
            if (section.ConsumerAssemblies == null || section.ConsumerAssemblies.Count == 0)
                return;
            foreach (var asm in section.ConsumerAssemblies)
            {
                if (string.IsNullOrEmpty(asm)) continue;
                var msg = $"加载配置文件RRabbitMQ:ConsumerAssemblies中项失败。name:{asm}";
                var ignoreAssemblyError = asm.StartsWith('+');
                var file = asm.TrimStart('+');
                var types = from t in ReflectionUtil.GetAssemblyTypes(file, ignoreAssemblyError, msg)
                            where t.IsSubclassOfGeneric(typeof(MQSubscribeConsumer<>))
                                || t.IsSubclassOfGeneric(typeof(MQRespondConsumer<,>))
                                || t.IsSubclassOfGeneric(typeof(MQReceiveConsumer))
                            select t;
                foreach (var type in types)
                {
                    var attr = type.GetCustomAttribute<MQConsumerIgnoreAttribute>();
                    if (attr != null)
                        continue;
                    if (type.IsSubclassOfGeneric(typeof(MQSubscribeConsumer<>)))
                    {
                        var obj = (IMQSubscribeConsumer)ReflectionUtil.CreateInstance(type);
                        switch (obj.SubscribeMode)
                        {
                            case MQSubscribeMode.OneQueue:
                            case MQSubscribeMode.Multicast:
                                await obj.Register();
                                _subConsumerDict.TryAdd(type.FullName, obj);
                                break;
                            case MQSubscribeMode.MultiQueue:
                                for (int idx = 1; idx < obj.QueueCount + 1; idx++)
                                {
                                    var newObj = (IMQSubscribeConsumer)ReflectionUtil.CreateInstance(type);
                                    ((IMQSubscribeSetQueueIndex)obj).SetQueueIndex(idx);
                                    await newObj.Register();
                                    _subConsumerDict.TryAdd($"{type.FullName}_{idx}", obj);
                                }
                                await new SubscribeQueueService(obj.MQMessageType).SetQueueCount(obj.QueueCount);
                                break;
                            case MQSubscribeMode.SAC:
                                var svc = new SubscribeQueueService(obj.MQMessageType);
                                var oldCount = await svc.GetQueueCount(false);
                                var newCount = obj.QueueCount;
                                await svc.SetQueueCount(newCount);
                                var queueIndex = await svc.GetQueueIndex();
                                queueIndex = (int)(queueIndex % newCount + 1);
                                ((IMQSubscribeSetQueueIndex)obj).SetQueueIndex((int)queueIndex);
                                await obj.Register(); // sac
                                _subConsumerDict.TryAdd($"{type.FullName}_{queueIndex}", obj);

                                //取大的确保以前的queue有消费
                                var currCount = Math.Max(oldCount, newCount);
                                for (int idx = 1; idx < currCount + 1; idx++)
                                {
                                    // sac已注册
                                    if (idx == queueIndex)
                                        continue;
                                    var newObj = (IMQSubscribeConsumer)ReflectionUtil.CreateInstance(type);
                                    ((IMQSubscribeSetQueueIndex)obj).SetQueueIndex(idx);
                                    _subConsumerDict.TryAdd($"{type.FullName}_{idx}", newObj);
                                }
                                break;
                        }
                    }
                    else
                    {
                        IMQConsumer obj = (IMQConsumer)ReflectionUtil.CreateInstance(type);
                        await obj.Register();
                        _consumerDict.TryAdd(obj.GetType().FullName, obj);
                    }
                }
            }
        }
        #endregion

        #region Method
        internal MQBusData GetBusData(string connectionStringName = null)
        {
            var section = ConfigUtil.GetSection<RabbitMQSection>();
            if (string.IsNullOrEmpty(connectionStringName))
                connectionStringName = section?.DefaultConnectionStringName;
            if (!_busDataDict.TryGetValue(connectionStringName, out var ret))
                throw new Exception($"配置文件RabbitMQ:ConnectionStrings:Name不存在：name={connectionStringName}");
            return ret;
        }
        internal async Task BindSACConsumer()
        {
            await _subConsumerDict.ForEachAsync(async x =>
            {
                if (!x.Value.IsRegisted)
                    await x.Value.Register();
            });
        }
        internal void Dispose()
        {
            _consumerDict.Values.ForEach(consumer => consumer.Dispose());
            _consumerDict.Clear();
            _busDataDict.Values.ForEach(data => data.Dispose());
            _busDataDict.Clear();
        }
        #endregion

    }
}
