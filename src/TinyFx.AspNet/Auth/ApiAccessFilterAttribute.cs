using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Logging;
using TinyFx.Net;
using TinyFx.Configuration;
using System.Xml.Linq;
using TinyFx.Reflection;

namespace TinyFx.AspNet
{
    /// <summary>
    /// Api访问过滤器(限制访问api的ip)
    /// </summary>
    public class ApiAccessFilterAttribute : ActionFilterAttribute
    {
        private string _name;

        public ApiAccessFilterAttribute(string name = null)
        {
            _name = name;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userIp = AspNetUtil.GetRemoteIpString();
            if (!CheckAllow(userIp))
            {
                context.Result = new UnauthorizedResult();
                LogUtil.GetContextLogger()
                    .SetLevel(Microsoft.Extensions.Logging.LogLevel.Warning)
                    .AddField("ApiAccessFilter.UserIp", userIp)
                    .AddField("ApiAccessFilter.FilterName", _name)
                    .AddMessage("ApiAccessFilterAttribute禁止访问。");
                return;
            }
            await base.OnActionExecutionAsync(context, next);
        }
        private bool CheckAllow(string userIp)
        {
            // 无IP有问题
            if (userIp == null || string.IsNullOrEmpty(userIp))
                return false;

            var section = ConfigUtil.GetSection<ApiAccessFilterSection>();
            if (section == null || !section.Filters.TryGetValue(_name ?? section.DefaultFilterName, out var element))
                throw new Exception($"配置中ApiAccessFilter:Filters未定义。name:{_name}");
            // 不限制
            if (!element.Enabled)
                return true;
            // 允许
            if (element.GetAllowIpDict().Contains(userIp))
                return true;

            var ipMode = NetUtil.GetIpMode(userIp);
            // 本机 => 放行
            if (ipMode == IpAddressMode.Local || ipMode == IpAddressMode.Loopback)
                return true;
            // 局域网
            return element.EnableIntranet && ipMode == IpAddressMode.Intranet;
        }
    }
}
