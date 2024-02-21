using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration
{
    public class AccessSignFilterSection : ConfigSection
    {
        public override string SectionName => "AccessSignFilter";
        public bool Enabled { get; set; }
        public string AccessKeySeed { get; set; }
        public string AccessKeyIndexes { get; set; }
        public string BothKeySeed { get; set; }
        public string BothKeyIndexes { get; set; }
    }
}
