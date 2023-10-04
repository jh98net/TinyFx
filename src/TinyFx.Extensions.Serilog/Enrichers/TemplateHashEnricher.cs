using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

namespace Serilog.Enrichers
{
    public class TemplateHashEnricher : ILogEventEnricher
    {
        public const string PropertyName = "TemplateHash";
        // !内存泄漏
        //private static ConcurrentDictionary<string, LogEventProperty> _hashs = new ConcurrentDictionary<string, LogEventProperty>();
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            /*
            var key = logEvent.MessageTemplate.Text;
            if (!_hashs.TryGetValue(key, out LogEventProperty value))
            {
                var murmur = Murmur.MurmurHash.Create32();
                var bytes = Encoding.UTF8.GetBytes(key);
                var hash = murmur.ComputeHash(bytes);
                var numericHash = BitConverter.ToUInt32(hash, 0);
                value = propertyFactory.CreateProperty(PropertyName, numericHash);
                _hashs.TryAdd(key, value);
            }
            logEvent.AddPropertyIfAbsent(value);
            */
        }
    }
}
