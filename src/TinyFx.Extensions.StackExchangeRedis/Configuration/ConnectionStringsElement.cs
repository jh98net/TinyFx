using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFx.Extensions.StackExchangeRedis
{
    public class ConnectionStringElement
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public RedisSerializeMode SerializeMode { get; set; } = RedisSerializeMode.Json;
        public string NamespaceMap { get; set; }
    }
}
