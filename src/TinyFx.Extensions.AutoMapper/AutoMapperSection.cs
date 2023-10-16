using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using TinyFx.Configuration;

namespace TinyFx.Configuration
{
    public class AutoMapperSection : ConfigSection
    {
        public override string SectionName => "AutoMapper";
        /// <summary>
        /// 忽略消费类的assembly异常
        /// </summary>
        public bool IgnoreAssemblyError { get; set; }
        public List<string> Assemblies { get; set; } = new List<string>();
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            Assemblies.Clear();
            Assemblies = configuration?.GetSection("Assemblies")
                .Get<List<string>>();
        }
    }
}
