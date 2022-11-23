using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration
{
    /// <summary>
    /// http输出缓存设置。暂时不成熟，未实现
    /// </summary>
    public class ResponseCachingSection : ConfigSection
    {
        public override string SectionName => "ResponseCaching";
        /// <summary>
        /// 是否启用缓存
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 缓存秒数
        /// </summary>
        public int MaxAge { get; set; }
    }
}
