using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using Google.Protobuf.WellKnownTypes;
using TinyFx.Security;

namespace Serilog.Enrichers
{
    public class TemplateHashEnricher : ILogEventEnricher
    {
        public const string PropertyName = "TemplateHash";
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var value = MurmurHash3.Hash32(logEvent.MessageTemplate.Text);
            var hash = propertyFactory.CreateProperty(PropertyName, value);
            logEvent.AddPropertyIfAbsent(hash);
        }
    }
}
