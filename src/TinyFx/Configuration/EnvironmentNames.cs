using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFx.Configuration
{
    /// <summary>
    /// 软件开发环境
    /// </summary>
    public enum EnvironmentNames
    {
        /// <summary>
        /// 自定义
        /// </summary>
        Unknown,
        /// <summary>
        /// 开发人员电脑
        /// </summary>
        Local,
        /// <summary>
        /// dev 开发测试环境
        /// </summary>
        Development,
        /// <summary>
        /// sit 集成测试环境
        /// </summary>
        Testing,
        /// <summary>
        /// qa fat 测试人员测试环境
        /// </summary>
        QA,
        /// <summary>
        /// uat 用户验收、仿真环境
        /// </summary>
        Staging,
        /// <summary>
        /// prod 生产环境
        /// </summary>
        Production
    }
}
