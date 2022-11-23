using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.AspNet;
using TinyFx.Configuration;

namespace TinyFx.Configuration
{
    /// <summary>
    /// 版本控制
    /// </summary>
    public class ApiVersioningSection : ConfigSection
    {
        public override string SectionName => "ApiVersioning";
        /// <summary>
        /// 版本控制模式
        /// </summary>
        public VersioningMode Mode { get; set; }

    }
}

namespace TinyFx.AspNet
{
    public enum VersioningMode
    {
        QueryString,
        Header,
        URL,
        MediaType
    }
}
