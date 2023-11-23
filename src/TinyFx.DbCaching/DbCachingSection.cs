using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;

namespace TinyFx.DbCaching
{
    public class DbCachingSection : ConfigSection
    {
        public override string SectionName => "DbCaching";
        public bool Enabled { get; set; }
        public string RedisConnectionStringName { get; set; }
    }
}
