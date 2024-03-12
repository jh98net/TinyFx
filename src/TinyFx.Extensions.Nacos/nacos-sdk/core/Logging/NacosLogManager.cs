using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using System;

namespace Nacos.Logging
{
    public class NacosLogManager
    {
        private static ILoggerFactory _loggerFactory;

        public static ILogger<T> CreateLogger<T>()
        {
            return _loggerFactory?.CreateLogger<T>() ?? NullLogger<T>.Instance;
        }

        public static ILogger CreateLogger(string categoryName)
        {
            return _loggerFactory?.CreateLogger(categoryName) ?? NullLogger.Instance;
        }

        public static void UseLoggerFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }
    }
}
