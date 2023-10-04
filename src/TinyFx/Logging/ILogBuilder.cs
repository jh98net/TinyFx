using Microsoft.Extensions.Logging;
using System;

namespace TinyFx.Logging
{
    public interface ILogBuilder
    {
        LogLevel Level { get; set; }
        LogLevel CustomeExceptionLevel { get; set; }
        bool LogRequestHeaders { get; set; }
        bool LogRequestBody { get; set; }
        bool LogResponseBody { get; set; }

        Exception Exception { get; set; }
        string Flag { get; set; }

        ILogBuilder AddException(Exception ex, LogLevel level = LogLevel.Error);
        ILogBuilder AddField(string field, object value);
        ILogBuilder AddMessage(string msg);
        void Log();
        ILogBuilder SetLevel(LogLevel level);
        ILogBuilder SetLogRequestHeaders(bool isLog = true);
        ILogBuilder SetLogRequestBody(bool isLog = true);
        
        /// <summary>
        /// 如需记录ResponseBody，必须配置RequestLogging:Urls才能获取
        /// </summary>
        /// <param name="isLog"></param>
        /// <returns></returns>
        ILogBuilder SetLogResponseBody(bool isLog = true);
        ILogBuilder SetFlag(string flag);
        LogLevel GetCustomeExceptionLevel();
    }
}