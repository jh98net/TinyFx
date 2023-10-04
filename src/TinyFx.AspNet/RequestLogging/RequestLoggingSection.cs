using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using static System.Collections.Specialized.BitVector32;

namespace TinyFx.AspNet.RequestLogging
{
    public class RequestLoggingSection : ConfigSection
    {
        public override string SectionName => "RequestLogging";

        public bool Enabled { get; set; }
        public LogLevel LogLevel { get; set; }
        public LogLevel CustomeExceptionLevel { get; set; } = LogLevel.Information;
        public bool LogRequestHeaders { get; set; }
        public bool LogRequestBody { get; set; } = false;
        public bool LogResponseBody { get; set; } = false;
        public List<string> Urls { get; set; }

        private HashSet<string> _urlDict;
        public HashSet<string> GetUrlDict()
        {
            if (_urlDict == null)
                _urlDict = Urls?.Select(x => x.ToLower()).ToHashSet() ?? new HashSet<string>();
            return _urlDict;
        }

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
        }
    }
}
