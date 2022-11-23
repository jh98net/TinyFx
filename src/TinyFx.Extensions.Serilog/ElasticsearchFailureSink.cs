using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFx.Extensions.Serilog
{
    public class ElasticsearchFailureSink : ILogEventSink
    {
        public void Emit(LogEvent logEvent)
        {
            var log = SerilogUtil.ExtLogger;
            log.Error("Elasticsearch失败!");
            log.Write(logEvent);
        }
    }
}
