using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration
{
    public class AccessVerifySection : ConfigSection
    {
        public override string SectionName => "AccessVerify";
        public bool Enabled { get; set; }
        public string AccessKeySeed { get; set; }
        public string AccessKeyIndexes { get; set; }
        public string BothKeySeed { get; set; }
        public string BothKeyIndexes { get; set; }
    }
}
