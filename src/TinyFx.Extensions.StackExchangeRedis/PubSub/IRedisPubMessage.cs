using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.StackExchangeRedis
{
    public interface IRedisPubMessage
    {
        /// <summary>
        /// 模式值
        /// </summary>
        public string PatternKey { get; set; }
    }
}
