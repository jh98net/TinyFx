using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL = Serilog;

namespace TinyFx.Extensions.Serilog
{
    public class LoggerWrapper : ILogger
    {
        private SL.ILogger _logger;
        private LogLevel _logLevel;
        public LoggerWrapper(SL.ILogger logger, LogLevel logLevel)
        {
            _logger = logger;
            _logLevel = logLevel;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _logLevel;

        public void Log<TState>(LogLevel logLevel,EventId eventId,TState state,Exception? exception,Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            var msg = formatter(state, exception);
            switch (logLevel)
            {
                case LogLevel.Trace:
                    _logger.Verbose(exception, msg);
                    break;
                case LogLevel.Debug:
                    _logger.Debug(exception, msg);
                    break;
                case LogLevel.Information:
                    _logger.Information(exception, msg);
                    break;
                case LogLevel.Warning:
                    _logger.Warning(exception, msg);
                    break;
                case LogLevel.Error:
                case LogLevel.Critical:
                    _logger.Error(exception, msg);
                    break;
            }

        }
    }
}
