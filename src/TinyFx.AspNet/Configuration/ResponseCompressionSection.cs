using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration
{
    public class ResponseCompressionSection : ConfigSection
    {
        public override string SectionName => "ResponseCompression";
        public bool Enabled { get; set; }
    }
}
