using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;

namespace TinyFx.Configuration
{
    public class HostSection : ConfigSection
    {
        public override string SectionName => "Host";

        /// <summary>
        /// 主机注册心跳间隔，默认5秒, 0-无心跳
        /// </summary>
        public int HeartbeatInterval { get; set; } = 5000;
        /// <summary>
        /// 主机检查间隔，默认10分钟, 0-无检查
        /// </summary>
        public int HeathInterval { get; set; } = 600000;
        /// <summary>
        /// 主机数据有效期，默认15秒,0-10分钟
        /// DataExpire=0或者ConfigUtil.IsDebugEnvironment=true时有效期为10分钟
        /// </summary>
        public int DataExpire { get; set; } = 15000;

        /// <summary>
        /// 主机Timer最小Delay时间, 0-无Timer
        /// </summary>
        public int TimerMinDelay { get; set; } = 200;
        /// <summary>
        /// 主机Timer关闭等待超时
        /// </summary>
        public int TimerWaitTimeout { get; set; } = 20000;
    }
}
