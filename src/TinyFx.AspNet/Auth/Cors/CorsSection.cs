using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TinyFx.AspNet;
using TinyFx.AspNet.Auth.Cors;
using TinyFx.Collections;
using TinyFx.Configuration;
using TinyFx.Reflection;

namespace TinyFx.Configuration
{
    /// <summary>
    /// asp.net core 跨域访问配置
    /// </summary>
    public class CorsSection : ConfigSection
    {
        public override string SectionName => "Cors";

        /// <summary>
        /// 是否使用cors中间件
        /// </summary>
        public CorsUseElement UseCors { get; set; }
        private ICorsPoliciesProvider _policiesProvider;

        /// <summary>
        /// 策略集合
        /// </summary>
        public Dictionary<string, CorsPolicyElement> Policies = new Dictionary<string, CorsPolicyElement>();

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            Policies = configuration.GetSection("Policies")
                .Get<Dictionary<string, CorsPolicyElement>>() ?? new();
            
            // PolicyProvider
            if (!string.IsNullOrEmpty(UseCors.PoliciesProvider))
            {
                _policiesProvider = ReflectionUtil.CreateInstance(UseCors.PoliciesProvider) as ICorsPoliciesProvider;
                if (_policiesProvider == null)
                    throw new Exception($"配置文件Cors:UseCors:PolicyProvider必须继承ICorsPoliciesProvider. value:{UseCors.PoliciesProvider}");
                var policies = _policiesProvider.GetPoliciesAsync().GetTaskResult(true);
                policies.ForEach(x =>
                {
                    if (!Policies.TryAdd(x.Name, x))
                        Policies[x.Name] = x;
                });
            }
            Policies.ForEach(x =>
            {
                x.Value.Name = x.Key;
            });
            if (string.IsNullOrEmpty(UseCors.DefaultPolicy))
            {
                if (Policies?.Count == 1)
                {
                    UseCors.DefaultPolicy = Policies.First().Key;
                }
                else
                {
                    if (Policies.TryGetValue("default", out var v))
                        UseCors.DefaultPolicy = v.Name;
                }
            }
            if (UseCors.Enabled && string.IsNullOrEmpty(UseCors.DefaultPolicy))
                throw new Exception("tinyfx配置错误，UseCors.Enabled Policies没有值");
        }
    }
}

namespace TinyFx.AspNet
{
    public class CorsUseElement
    {
        public bool Enabled { get; set; }
        public bool EnabledReferer { get; set; }
        public string PoliciesProvider { get; set; }
        public string DefaultPolicy { get; set; }
    }

    /// <summary>
    /// CORS策略项
    /// </summary>
    public class CorsPolicyElement
    {
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
        /// Access-Control-Max-Age 时间(秒)
        /// </summary>
        public int MaxAge { get; set; } = 86400;

        private HashSet<string> _originSet = null;
        internal HashSet<string> OriginSet
        {
            get
            {
                if (_originSet == null)
                {
                    _originSet = ParseOriginSet();
                }
                return _originSet;
            }
            set { _originSet = value; }
        }
        private HashSet<string> ParseOriginSet()
        {
            var ret = new HashSet<string>();
            if (string.IsNullOrEmpty(Origins) || Origins == "*")
                return ret;
            Origins.Split(';', StringSplitOptions.RemoveEmptyEntries)
                .ForEach(x => ret.Add(x));
            return ret;
        }
    }
}
