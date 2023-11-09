using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using TinyFx.Configuration;
using Serilog.Extensions.Logging;
using Serilog.Exceptions;

namespace TinyFx.Logging
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public static class LogUtil
    {
        public static Serilog.ILogger CreateBootstrapLogger()
        {
            var config = new Serilog.LoggerConfiguration()
             .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
             .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
             .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
             .MinimumLevel.Debug()
             .Enrich.WithExceptionDetails()
             .Enrich.FromLogContext();
            // debug
            config = Serilog.LoggerSinkConfigurationDebugExtensions.Debug(config.WriteTo
                , outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");
            // console
            config = Serilog.ConsoleLoggerConfigurationExtensions.Console(config.WriteTo
                , outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");
            //config = Serilog.FileLoggerConfigurationExtensions.File(config.WriteTo
            //    , "./logs/ext.log"
            //    , restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
            //    , rollingInterval:Serilog.RollingInterval.Day
            //    , retainedFileCountLimit: 7);
            Serilog.Log.Logger = Serilog.LoggerConfigurationExtensions.CreateBootstrapLogger(config);
            return Serilog.Log.Logger;
        }

        private static ILoggerFactory _factory;
        public static ILoggerFactory Factory
        {
            get
            {
                if (_factory == null)
                    _factory = DIUtil.GetService<ILoggerFactory>() ?? new SerilogLoggerFactory(Serilog.Log.Logger);
                return _factory;
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
                return _defaultLogger;
            }
        }
        internal static void Rebuild()
        {
            _factory = DIUtil.GetService<ILoggerFactory>() ?? new SerilogLoggerFactory(Serilog.Log.Logger);
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
        public static LoggerWrapper Create<T>()
            => new LoggerWrapper(Factory?.CreateLogger<T>());

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

        public static ILogBuilder CreateLogBuilder(LogLevel level = LogLevel.Debug, string flag = null)
        {
            return new LogBuilder(level, flag);
        }
    }
}
