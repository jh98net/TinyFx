using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Admin.BLL.Services;

namespace TinyFx
{
    public static class AdminHost
    {
        public static WebApplicationBuilder CreateBuilder(string[] args = null)
        {
            var builder = AspNetHost.CreateBuilder(args);
            var services = builder.Services;
            services.AddTinyFxEx(AspNetType.Api | AspNetType.Razor | AspNetType.ServerBlazor);

            // 增加 健康检查服务
            //services.AddAdminHealthChecks();

            // 增加 BootstrapBlazor 组件
            services.AddBootstrapBlazor();

            // 配置地理位置定位器
            services.ConfigureIPLocatorOption(opt => opt.LocatorFactory = (sp) => new DefaultIPLocator());

            // 增加手机短信服务
            //services.AddSingleton<ISMSProvider, TencentSMSProvider>();

            // 增加认证授权服务
            //services.AddBootstrapAdminSecurity<AdminService>();

            // 增加 BootstrapApp 上下文服务
            services.AddScoped<BootstrapAppContext>();
            return builder;
        }
    }
}
