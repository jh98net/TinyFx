using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using TinyFx.Collections;
using TinyFx.Configuration;
using TinyFx.Extensions.RabbitMQ;
using TinyFx.Logging;

namespace TinyFx.Configuration
{
    public class RabbitMQSection : ConfigSection
    {
        public override string SectionName => "RabbitMQ";
        public bool Enabled { get; set; } = true;
        public bool LogEnabled { get; set; }
        /// <summary>
        /// 顺序订阅消费者SAC检查延迟时间（秒）默认2分钟
        /// </summary>
        public int SACBindDelay { get; set; } = 120;
        /// <summary>
        /// SAC使用的Redis
        /// </summary>
        public string RedisConnectionStringName { get; set; }
        public string DefaultConnectionStringName { get; set; }
        public Dictionary<string, MQConnectionStringElement> ConnectionStrings = new();
        /// <summary>
        /// ReceiveConsumer、RespondConsumer、SubscribeConsumer所在的程序集,用于消费注册
        /// </summary>
        public List<string> ConsumerAssemblies { get; set; } = new List<string>();
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            ConnectionStrings = configuration.GetSection("ConnectionStrings")
                .Get<Dictionary<string, MQConnectionStringElement>>() ?? new();
            ConnectionStrings.ForEach(x =>
            {
                x.Value.Name = x.Key;
            });

            if (string.IsNullOrEmpty(DefaultConnectionStringName) && ConnectionStrings.Count == 1)
                DefaultConnectionStringName = ConnectionStrings.First().Key;
            // Assemblies
            ConsumerAssemblies = configuration?.GetSection("ConsumerAssemblies").Get<List<string>>() 
                ?? new List<string>();
        }
    }
}

namespace TinyFx.Extensions.RabbitMQ
{
    public class MQConnectionStringElement
    {
        public string Name { get; internal set; }
        /// <summary>
        /// 是否使用简短的名字定义ExchangeName和QueueName(MQConventions)
        /// </summary>
        public bool UseShortNaming { get; set; }
        /// <summary>
        /// 是否使用ConfigUtil.EnvironmentString作为VirtualHost
        /// </summary>
        public bool UseEnvironmentVirtualHost { get; set; }
        /// <summary>
        /// 是否启用高可用(仅MQ群集使用)
        /// </summary>
        public bool UseQuorum { get; set; }
        public string ConnectionString { get; set; }
    }
}
