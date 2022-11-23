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

namespace TinyFx.AspNet
{
    /// <summary>
    /// Api访问过滤器(限制访问api的ip)
    /// </summary>
    public class ApiAccessFilterAttribute : ActionFilterAttribute
    {
        private ApiAccessFilterElement _element;
        private bool _allowIntranet;
        public ApiAccessFilterAttribute(string name = null)
        {
            var section = ConfigUtil.GetSection<ApiAccessFilterSection>();
            if (section == null || !section.Filters.TryGetValue(name ?? section.DefaultFilterName, out ApiAccessFilterElement element))
                throw new Exception($"配置中ApiAccessFilter:Filters:Name不存在。name:{name}");
            _element = element;
            _allowIntranet = _element.AllowIpsDic.Contains("intranet");
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 有效过滤
            if (_element.Enabled)
            {
                var userIp = AspNetUtil.GetRemoteIpAddress();
                var isLoopback = IPAddress.IsLoopback(userIp);
                // 本机 => 放行
                if (!isLoopback)
                {
                    try
                    {
                        var ipStr = userIp.MapToIPv4().ToString();
                        // AllowIps不包含 => 禁止
                        if (!_element.AllowIpsDic.Contains(ipStr))
                        {
                            // 内网访问
                            if (!_allowIntranet || new IpAddressParser(ipStr).IpMode != IpAddressMode.Intranet)
                            {
                                context.Result = new UnauthorizedResult();
                                LogUtil.Warning($"ApiAccessFilterAttribute禁止访问。name: {_element.Name} ip: {ipStr}");
                                return;
                            }
                        }
                    }
                    catch
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                }
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
