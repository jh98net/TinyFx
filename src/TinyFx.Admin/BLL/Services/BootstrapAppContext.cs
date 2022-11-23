using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;

namespace TinyFx.Admin.BLL.Services
{
    /// <summary>
    /// AppContext 实体类
    /// </summary>
    public class BootstrapAppContext
    {
        /// <summary>
        /// 获得/设置 当前网站 AppId
        /// </summary>
        public string AppId { get; }
        public string UserID { get; set; }
        /// <summary>
        /// 获得/设置 当前登录账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获得/设置 当前用户显示名称
        /// </summary>
        public string DisplayName { get; internal set; }

        /// <summary>
        /// 获得/设置 应用程序基础地址 如 http://localhost:5210
        /// </summary>
        public Uri BaseUri { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BootstrapAppContext()
        {
            AppId = ConfigUtil.Project.ProjectId;
        }
    }

}
