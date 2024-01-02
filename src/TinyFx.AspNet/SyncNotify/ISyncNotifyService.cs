using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.AspNet
{
    /// <summary>
    /// 客户端同步通知服务
    /// </summary>
    public interface ISyncNotifyService
    {
        /// <summary>
        /// 设置通知值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetNotify(string id, bool value);
        
        /// <summary>
        /// 获取通知值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> GetNotify(string id);
    }
}
