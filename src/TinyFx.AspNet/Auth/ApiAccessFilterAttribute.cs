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
            var userIp = AspNetUtil.GetRemoteIpAddress();
            if (!CheckAllow(userIp))
            {
                context.Result = new UnauthorizedResult();
                LogUtil.Warning("ApiAccessFilterAttribute禁止访问。filterName:{filterName} userIp:{userIp}"
                    , _name, userIp?.ToString());
                return;
            }
            await base.OnActionExecutionAsync(context, next);
        }
        private bool CheckAllow(IPAddress userIp)
        {
            var section = ConfigUtil.GetSection<ApiAccessFilterSection>();
            if (section == null || !section.Filters.TryGetValue(_name ?? section.DefaultFilterName, out var element))
                throw new Exception($"配置中ApiAccessFilter:Filters未定义。name:{_name}");
            // 不限制
            if (!element.Enabled)
                return true;
            // 无IP有问题
            if (userIp == null)
                return false;
            var ipStr = Convert.ToString(userIp);
            if (string.IsNullOrEmpty(ipStr))
                return false;
            // 允许
            if (element.GetAllowIpDict().Contains(ipStr))
                return true;

            var ipMode = NetUtil.GetIpMode(ipStr);
            // 本机 => 放行
            if (ipMode == IpAddressMode.Local)
                return true;
            // 不允许局域网
            if (!element.EnableIntranet)
                return false;
            // 
            return NetUtil.GetIpMode(ipStr) == IpAddressMode.Intranet;
        }
    }
}
