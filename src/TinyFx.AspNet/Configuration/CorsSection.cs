using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.AspNet;
using TinyFx.Configuration;

namespace TinyFx.Configuration
{
    /// <summary>
    /// asp.net core 跨域访问配置
    /// </summary>
    public class CorsSection : ConfigSection
    {
        public override string SectionName => "Cors";
        /// <summary>
        /// 策略集合
        /// </summary>
        public Dictionary<string, CorsPolicyElement> Policies = new Dictionary<string, CorsPolicyElement>();
        /// <summary>
        /// 是否使用cors中间件
        /// </summary>
        public CorsUseElement UseCors { get; set; }

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            var eles = configuration.GetSection("Policies").Get<CorsPolicyElement[]>();
            Policies.Clear();
            foreach (var ele in eles)
            {
                if (Policies.ContainsKey(ele.Name))
                    throw new Exception($"配置中Cors:Policies:Name 重复。Name: {ele.Name}");
                Policies.Add(ele.Name, ele);
            }
            //UseCors = configuration.GetSection("UseCors").Get<CorsUseElement>();
        }
    }
}

namespace TinyFx.AspNet
{
    public class CorsUseElement
    {
        public bool Enabled { get; set; }
        public string PolicyName { get; set; }
    }

    /// <summary>
    /// CORS策略项
    /// </summary>
    public class CorsPolicyElement
    {
        /// <summary>
        /// 策略名称，默认(default)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 允许的来源,分号;分隔（下同）
        /// </summary>
        public string Origins { get; set; }
        /// <summary>
        /// 允许的HTTP方法
        /// </summary>
        public string Methods { get; set; }
        /// <summary>
        /// 允许的请求标头
        /// </summary>
        public string Headers { get; set; }
        /// <summary>
        /// 是否属于默认Policy
        /// </summary>
        public bool IsDefault => Name == "(default)";
    }
}
