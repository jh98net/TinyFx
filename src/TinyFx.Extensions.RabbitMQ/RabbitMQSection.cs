using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyFx.Configuration;
using TinyFx.Extensions.RabbitMQ;

namespace TinyFx.Configuration
{
    public class RabbitMQSection : ConfigSection
    {
        public override string SectionName => "RabbitMQ";
        public string DefaultConnectionStringName { get; set; }
        public bool LogEnabled { get; set; }
        public Dictionary<string, MQConnectionStringElement> ConnectionStrings = new();
        public List<string> MessageAssemblies { get; set; } = new List<string>();
        /// <summary>
        /// ReceiveConsumer、RespondConsumer、SubscribeConsumer所在的程序集,用于消费注册
        /// </summary>
        public List<string> ConsumerAssemblies { get; set; } = new List<string>();
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            // ConnectionStrings
            var connStrs = configuration.GetSection("ConnectionStrings").Get<MQConnectionStringElement[]>();
            ConnectionStrings.Clear();
            foreach (var connStr in connStrs)
            {
                if (ConnectionStrings.ContainsKey(connStr.Name))
                    throw new Exception($"配置中RabbitMQ:ConnectionStrings:Name 重复。Name: {connStr.Name}");
                ConnectionStrings.Add(connStr.Name, connStr);
            }
            if (string.IsNullOrEmpty(DefaultConnectionStringName) && ConnectionStrings.Count == 1)
                DefaultConnectionStringName = ConnectionStrings.First().Key;
            // Assemblies
            MessageAssemblies.Clear();
            MessageAssemblies = configuration?.GetSection("MessageAssemblies").Get<List<string>>();
            ConsumerAssemblies.Clear();
            ConsumerAssemblies = configuration?.GetSection("ConsumerAssemblies").Get<List<string>>();
        }
    }
}

namespace TinyFx.Extensions.RabbitMQ
{
    public class MQConnectionStringElement
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}
