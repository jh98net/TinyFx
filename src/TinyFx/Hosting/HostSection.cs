using Microsoft.Extensions.Configuration;
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
        /// 是否注册Host服务
        /// </summary>
        public bool RegisterEnabled { get; set; }
        /// <summary>
        /// 主机注册心跳间隔，默认5秒
        /// </summary>
        public int HeartbeatInterval { get; set; } = 5000;
        /// <summary>
        /// 主机检查间隔，默认10分钟
        /// </summary>
        public int HeathInterval { get; set; } = 600000;

        /// <summary>
        /// 主机Timer最小Delay时间, 默认200最小100
        /// </summary>
        public int TimerMinDelay { get; set; } = 200;
        /// <summary>
        /// 主机Timer关闭等待超时，默认20000最小5000
        /// </summary>
        public int TimerWaitTimeout { get; set; } = 20000;

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);

            if (HeartbeatInterval <= 1000)
                HeartbeatInterval = 5000;
            if (HeathInterval <= 60000)
                HeathInterval = 600000;
            if (TimerMinDelay <= 100)
                TimerMinDelay = 200;
            if (TimerWaitTimeout <= 5000)
                TimerWaitTimeout = 20000;
        }
    }
}
