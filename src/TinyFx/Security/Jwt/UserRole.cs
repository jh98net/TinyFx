using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFx.Security
{
    /// <summary>
    /// 用户类型权限
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknow = 0,
        /// <summary>
        /// 普通用户
        /// </summary>
        User = 1,
        /// <summary>
        /// 测试人员
        /// </summary>
        Tester = 2,
        /// <summary>
        /// 仿真测试人员
        /// </summary>
        StagingTester = 3,
        /// <summary>
        /// 管理员
        /// </summary>
        Admin = 9
    }
}
