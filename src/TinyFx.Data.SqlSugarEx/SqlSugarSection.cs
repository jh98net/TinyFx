using Microsoft.Extensions.Configuration;

namespace TinyFx.Configuration
{
    public class SqlSugarSection : ConfigSection
    {
        public override string SectionName => "SqlSugar";
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
        }
    }
}