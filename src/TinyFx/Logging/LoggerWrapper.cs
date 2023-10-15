using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Logging
{
    public class LoggerWrapper
    {
        private ILogger _logger;
        public LoggerWrapper(ILogger logger)
        {
            _logger = logger;
        }
        public void Trace(string message, params object[] args)
        {
            if (_logger != null && _logger.IsEnabled(LogLevel.Trace))
                _logger.LogTrace(message, args);
        }
        public void Debug(string message, params object[] args)
        {
            if (_logger != null && _logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug(message, args);
        }
        public void Error(Exception exception, string message, params object[] args)
        {
            if (_logger != null && _logger.IsEnabled(LogLevel.Error))
                _logger.LogError(exception, message, args);
        }
        public void Error(Exception exception, params object[] args)
            => Error(exception, exception.Message, args);
        public void Error(string message, params object[] args)
        {
            if (_logger != null && _logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message, args);
        }
        public void Info(string message, params object[] args)
        {
            if (_logger != null && _logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message, args);
        }
        public void Info(Exception exception, string message, params object[] args)
        {
            if (_logger != null && _logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(exception, message, args);
        }
        public void Warning(string message, params object[] args)
        {
            if (_logger != null && _logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message, args);
        }
        public void Warning(Exception exception, string message, params object[] args)
        {
            if (_logger != null && _logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(exception, message, args);
        }
    }
}
