using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration
{
    public class SwaggerSection : ConfigSection
    {
        public override string SectionName => "Swagger";
        public bool Enabled { get; set; }

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
        }
    }
}
