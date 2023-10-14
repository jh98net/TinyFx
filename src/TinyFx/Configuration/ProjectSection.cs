using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TinyFx.Configuration
{
    /// <summary>
    /// 项目信息配置节
    /// </summary>
    public class ProjectSection : ConfigSection
    {
        /// <summary>
        /// 配置节名称
        /// </summary>
        public override string SectionName => "Project";

        /// <summary>
        /// 项目标识
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 项目描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 默认Console日志级别
        /// </summary>
        public LogLevel ConsoleLogLevel { get; set; } = LogLevel.Debug;
        /// <summary>
        /// 线程池最小线程数（一般100-300）
        /// </summary>
        public int MinThreads { get; set; } = 100;
        /// <summary>
        /// 是否返回客户端错误信息(自定义异常和未处理异常的message)
        /// </summary>
        public bool ResponseErrorMessage { get; set; } = true;
        /// <summary>
        /// 是否返回客户端异常详细信息（exception序列化信息）
        /// </summary>
        public bool ResponseErrorDetail { get; set; } = false;
        /// <summary>
        /// 是否测试环境
        /// </summary>
        public bool IsDebugEnvironment { get; set; }
    }
}
