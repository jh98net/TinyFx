using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using TinyFx.Configuration;

namespace TinyFx.Logging
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public static class LogUtil
    {
        private static LogLevel? _consoleLogLevel = null;
        /// <summary>
        /// 设置ConsoleLogger日志级别
        /// </summary>
        public static LogLevel ConsoleLogLevel
        {
            get
            {
                if (!_consoleLogLevel.HasValue)
                    _consoleLogLevel = ConfigUtil.Project?.ConsoleLogLevel ?? LogLevel.Trace;
                return _consoleLogLevel.Value;
            }
            set
            {
                _consoleLogLevel = value;
            }
        }

        private static ILogger _consoleLogger;
        /// <summary>
        /// 系统默认的Console日志（未加载Serilog之前）
        /// </summary>
        public static ILogger ConsoleLogger
        {
            get
            {
                if (_consoleLogger == null)
                {
                    var factory = LoggerFactory.Create(builder =>
                    {
                        builder.AddFilter("Microsoft", LogLevel.Warning)
                            .AddFilter("System", LogLevel.Warning);
                        if (ConsoleLogLevel != LogLevel.Trace)
                            builder.AddFilter(level => level >= ConsoleLogLevel);
                        builder.AddConsole();
                    });
                    _consoleLogger = factory.CreateLogger("ConsoleLogger");
                }
                return _consoleLogger;
            }
        }

        private static ILoggerFactory _factory;
        public static ILoggerFactory Factory
        {
            get
            {
                if (_factory == null)
                    _factory = DIUtil.GetService<ILoggerFactory>();
                return _factory;
            }
            set
            {
                _factory = value;
            }
        }

        private static ILogger _defaultLogger;
        /// <summary>
        /// 默认Logger
        /// </summary>
        public static ILogger DefaultLogger
        {
            get
            {
                if (_defaultLogger == null)
                    _defaultLogger = Factory?.CreateLogger("DefaultLogger");
                if (_defaultLogger == null)
                    _defaultLogger = ConsoleLogger;
                return _defaultLogger;
            }
        }
        public static void Rebuild()
        {
            _factory = null;
            _defaultLogger = null;
        }
        /// <summary>
        /// 创建Logger
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILogger<T> CreateLogger<T>()
            => Factory?.CreateLogger<T>();

        public static ILogger CreateLogger(string categoryName)
            => Factory?.CreateLogger(categoryName);
        public static ILogger CreateLogger(Type type)
            => CreateLogger(type.Name);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        public static void Log(LogLevel lv, string message, Exception ex = null, params object[] args)
        {
            DefaultLogger?.Log(lv, ex, message, args);
        }

        /// <summary>
        /// 记录Debug
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Trace(string message, params object[] args)
        {
            if (DefaultLogger != null && DefaultLogger.IsEnabled(LogLevel.Trace))
                DefaultLogger.LogTrace(message, args);
        }

        /// <summary>
        /// 记录Debug
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Debug(string message, params object[] args)
        {
            if (DefaultLogger != null && DefaultLogger.IsEnabled(LogLevel.Debug))
                DefaultLogger.LogDebug(message, args);
        }

        /// <summary>
        /// 记录Error
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Error(Exception exception, string message, params object[] args)
        {
            if (DefaultLogger != null && DefaultLogger.IsEnabled(LogLevel.Error))
                DefaultLogger.LogError(exception, message, args);
        }

        /// <summary>
        /// 记录Error
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void Error(Exception exception, params object[] args)
            => Error(exception, exception.Message, args);

        /// <summary>
        /// 记录Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Error(string message, params object[] args)
        {
            if (DefaultLogger != null && DefaultLogger.IsEnabled(LogLevel.Error))
                DefaultLogger.LogError(message, args);
        }

        /// <summary>
        /// 记录Information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Info(string message, params object[] args)
        {
            if (DefaultLogger != null && DefaultLogger.IsEnabled(LogLevel.Information))
                DefaultLogger.LogInformation(message, args);
        }
        public static void Info(Exception exception, string message, params object[] args)
        {
            if (DefaultLogger != null && DefaultLogger.IsEnabled(LogLevel.Information))
                DefaultLogger.LogInformation(exception, message, args);
        }
        /// <summary>
        /// 记录Warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Warning(string message, params object[] args)
        {
            if (DefaultLogger != null && DefaultLogger.IsEnabled(LogLevel.Warning))
                DefaultLogger.LogWarning(message, args);
        }
        /// <summary>
        /// 记录Warning
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Warning(Exception exception, string message, params object[] args)
        {
            if (DefaultLogger != null && DefaultLogger.IsEnabled(LogLevel.Warning))
                DefaultLogger.LogWarning(exception, message, args);
        }

        /// <summary>
        /// 获取上下文LogBuilder或者创建
        /// </summary>
        /// <returns></returns>
        public static ILogBuilder GetContextLog()
        {
            var context = DIUtil.GetService<ILogBuilder>();
            if (context != null)
                return context;
            var ret = new LogBuilder();
            ret.IsContextLog = false;
            return ret;
        }
    }
}
