using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;

namespace TinyFx.Admin.BLL
{
    public class AdminSection : ConfigSection
    {
        public override string SectionName => "Admin";
        public int CookieExpireDays { get; set; }
    }
}
