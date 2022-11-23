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
        /// dev 开发环境，一般是开发人员电脑
        /// </summary>
        Development,
        /// <summary>
        /// test 测试环境，一般发布到测试服务器
        /// </summary>
        Testing,
        /// <summary>
        /// qa QA环境，一般发布到QA服务器
        /// </summary>
        QA,
        /// <summary>
        /// staging 客户演示、新功能预演、仿真环境
        /// </summary>
        Staging,
        /// <summary>
        /// prod 生产环境
        /// </summary>
        Production
    }
}
