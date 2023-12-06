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
        private static bool _enabled;
        static MQLogger()
        {
            ConfigUtil.ConfigChanged += (_, _) =>
            {
                var section = ConfigUtil.GetSection<RabbitMQSection>();
                _enabled = section != null && section.LogEnabled;
            };
        }
        public MQLogger()
        {
            var section = ConfigUtil.GetSection<RabbitMQSection>();
            _enabled = section != null && section.LogEnabled;
        }
        public bool Log(EasyNetQ.Logging.LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            if (!_enabled || messageFunc == null)
                return false;
            var lv = logLevel switch
            {
                EasyNetQ.Logging.LogLevel.Trace => Microsoft.Extensions.Logging.LogLevel.Trace,
                EasyNetQ.Logging.LogLevel.Debug => Microsoft.Extensions.Logging.LogLevel.Debug,
                EasyNetQ.Logging.LogLevel.Info => Microsoft.Extensions.Logging.LogLevel.Information,
                EasyNetQ.Logging.LogLevel.Warn => Microsoft.Extensions.Logging.LogLevel.Warning,
                EasyNetQ.Logging.LogLevel.Error => Microsoft.Extensions.Logging.LogLevel.Error,
                EasyNetQ.Logging.LogLevel.Fatal => Microsoft.Extensions.Logging.LogLevel.Critical,
                _ => throw new ArgumentOutOfRangeException()
            };
            var msg = messageFunc();
            LogUtil.Log(lv, msg, exception, formatParameters);
            return true;
        }
    }
}
