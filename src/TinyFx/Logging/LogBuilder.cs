using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using TinyFx.Collections;

namespace TinyFx.Logging
{
    public class LogBuilder<T> : LogBuilder
    {
        public LogBuilder(LogLevel level = LogLevel.Debug)
            : base(level, typeof(T).Name)
        { }
    }
    /// <summary>
    /// 结构化日志构建器
    /// </summary>
    public class LogBuilder : ILogBuilder
    {
        public bool IsContext { get; set; }
        public LogLevel Level { get; set; } = LogLevel.Debug;
        public LogLevel CustomeExceptionLevel { get; set; } = LogLevel.Information;
        public bool LogRequestHeaders { get; set; }
        public bool LogRequestBody { get; set; }
        public bool LogResponseBody { get; set; }

        public string Flag { get; set; }
        public StringBuilder Message { get; set; } = new();
        public ConcurrentDictionary<string, string> Fields = new();
        public Exception Exception { get; set; }

        public LogBuilder(LogLevel level = LogLevel.Debug, string flag = null)
        {
            Level = level;
            Flag = flag;
        }
        public LogBuilder(string flag) : this(LogLevel.Debug, flag) { }

        #region Methods
        public ILogBuilder SetLevel(LogLevel level)
        {
            Level = level;
            return this;
        }
        public ILogBuilder SetLogRequestHeaders(bool isLog = true)
        {
            LogRequestHeaders = isLog;
            return this;
        }
        public ILogBuilder SetLogRequestBody(bool isLog = true)
        {
            LogRequestBody = isLog;
            return this;
        }
        public ILogBuilder SetLogResponseBody(bool isLog = true)
        {
            LogResponseBody = isLog;
            return this;
        }
        public ILogBuilder SetFlag(string flag)
        {
            Flag = flag; return this;
        }
        public ILogBuilder AddRequestBody(string body)
        {
            return AddField("Request.Body", body);
        }
        public ILogBuilder AddResponseBody(string body)
        {
            return AddField("Response.Body", body);
        }
        public ILogBuilder AddMessage(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
                Message.AppendLine(msg);
            return this;
        }

        /// <summary>
        /// 添加Field
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value">对象将被json序列化</param>
        /// <returns></returns>
        public ILogBuilder AddField(string field, object value)
        {
            var valueStr = SerializerUtil.SerializeJsonNet(value);
            Fields.AddOrUpdate(field, valueStr, (k, v) => $"{v}{Environment.NewLine}{valueStr}");
            return this;
        }
        public ILogBuilder AddException(Exception ex)
        {
            var exc = ExceptionUtil.GetException<CustomException>(ex);
            var level = exc == null ? LogLevel.Error : CustomeExceptionLevel;
            Level = Level > level ? Level : level;
            Exception = ex;
            return this;
        }
        #endregion

        public void Save()
        {
            var msg = string.Empty;
            var args = new List<string>(Fields.Count + 2);
            msg += "[{Flag}]";
            args.Add(Flag);
            msg += "{Message}";
            args.Add(Message.ToString().TrimEnd(Environment.NewLine));

            Fields.ForEach(x =>
            {
                msg += $"{{{x.Key}}}";
                args.Add(x.Value);
            });
            LogUtil.Log(Level, msg, Exception, args.ToArray());
        }
    }
}
