using Microsoft.Extensions.Configuration;

namespace TinyFx.Configuration
{
    public class SqlSugarSection : ConfigSection
    {
        public override string SectionName => "SqlSugar";
        public string DefaultConnectionStringName { get; set; }
        public Dictionary<string, SqlSugarConnectionStringElement> ConnectionStrings = new ();
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
        }
    }
    public class SqlSugarConnectionStringElement
    {
        
    }
}