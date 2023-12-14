using Microsoft.Extensions.Logging;
using System;

namespace TinyFx.Logging
{
    public interface ILogBuilder
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        LogLevel Level { get; set; }
        /// <summary>
        /// 是否Context日志
        /// </summary>
        bool IsContext { get; }
        /// <summary>
        /// CustomeException时的日志级别，默认Infomation
        /// </summary>
        LogLevel CustomeExceptionLevel { get; set; }
        /// <summary>
        /// 是否记录请求headers
        /// </summary>
        bool LogRequestHeaders { get; set; }
        /// <summary>
        /// 是否记录请求body
        /// </summary>
        bool LogRequestBody { get; set; }
        /// <summary>
        /// 是否记录响应body
        /// </summary>
        bool LogResponseBody { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        Exception Exception { get; set; }
        /// <summary>
        /// 日志标记
        /// </summary>
        string Flag { get; set; }

        /// <summary>
        /// 添加请求body
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        ILogBuilder AddRequestBody(string body);
        /// <summary>
        /// 添加响应body
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        ILogBuilder AddResponseBody(string body);
        /// <summary>
        /// 添加异常信息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        ILogBuilder AddException(Exception ex);
        /// <summary>
        /// 添加自定义Field
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        ILogBuilder AddField(string field, object value);
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        ILogBuilder AddMessage(string msg);
        /// <summary>
        /// 设置日志级别
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        ILogBuilder SetLevel(LogLevel level);
        ILogBuilder SetLogRequestHeaders(bool isLog = true);
        ILogBuilder SetLogRequestBody(bool isLog = true);

        /// <summary>
        /// 如需记录ResponseBody，必须配置RequestLogging:Urls才能获取
        /// </summary>
        /// <param name="isLog"></param>
        /// <returns></returns>
        ILogBuilder SetLogResponseBody(bool isLog = true);
        /// <summary>
        /// 设置日志标记
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        ILogBuilder SetFlag(string flag);

        /// <summary>
        /// 保存日志
        /// </summary>
        void Save();
    }
}