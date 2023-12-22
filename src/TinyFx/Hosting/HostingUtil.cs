using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Hosting.Services;

namespace TinyFx.Hosting
{
    public static class HostingUtil
    {
        /// <summary>
        /// 获取Host生命周期事件注册服务
        /// </summary>
        /// <returns></returns>
        public static IHostApplicationLifetime GetLifetime()
        {
            return DIUtil.GetService<IHostApplicationLifetime>();
        }

        /// <summary>
        /// 获取Host数据操作服务
        /// </summary>
        /// <returns></returns>
        public static ITinyFxHostDataService GetDataService()
        {
            return DIUtil.GetService<ITinyFxHostDataService>();
        }

        /// <summary>
        /// 获取Host定时任务服务
        /// </summary>
        /// <returns></returns>
        public static ITinyFxHostTimerService GetTimerService()
        {
            return DIUtil.GetService<ITinyFxHostTimerService>();
        }
    }
}
