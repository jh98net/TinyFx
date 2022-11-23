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
        /// 是否返回客户端错误详细信息
        /// </summary>
        public bool ResponseErrorDetail { get; set; } = false;
    }
}
