using EasyNetQ.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TinyFx.Logging;
using TinyFx.Configuration;

namespace TinyFx.Extensions.RabbitMQ
{
    public class MQLogger<T> : EasyNetQ.Logging.ILogger<T>
    {
        private RabbitMQSection _section;
        private bool _enabled;
        public MQLogger()
        {
            var section = ConfigUtil.GetSection<RabbitMQSection>();
            _enabled = section != null && section.LogEnabled;
        }
        public bool Log(EasyNetQ.Logging.LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            if (!_enabled)
                return false;
            if (messageFunc == null)
            {
                return true;
            }
            var msg = messageFunc();
            var lv = EnumUtil.ToEnum((int)logLevel, Microsoft.Extensions.Logging.LogLevel.Warning);
            LogUtil.Log(lv, msg, exception, formatParameters);
            return true;
        }
    }
}
