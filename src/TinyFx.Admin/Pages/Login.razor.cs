using BootstrapBlazor.Components;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;

namespace TinyFx.Admin.Pages
{
    public partial class Login: IDisposable
    {
        #region Properties
        [SupplyParameterFromQuery]
        [Parameter]
        public string? ReturnUrl { get; set; }

        protected string Title { get; set; }
        protected string PostUrl { get; set; }
        protected ElementReference LoginForm { get; set; }
        private JSInterop<Login>? Interop { get; set; }

        protected bool UseMobileLogin { get; set; }
        protected string? ClassString => CssBuilder.Default("login-wrap")
            .AddClass("is-mobile", UseMobileLogin)
            .Build();
        protected bool AllowMobile { get; set; } = true;
        protected bool RememberPassword { get; set; }
        protected bool AllowOAuth { get; set; }
        #endregion

        #region override
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Title = ConfigUtil.Project.Description;
            PostUrl = QueryHelper.AddQueryString("Account/Login", new Dictionary<string, string?>
            {
                ["ReturnUrl"] = ReturnUrl,
                ["AppId"] = ConfigUtil.Project.ProjectId
            });
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                // register javascript
                Interop = new JSInterop<AdminLogin>(JSRuntime);
                await Interop.InvokeVoidAsync(this, LoginForm, "login", "api/Login", nameof(Log));
            }
        }
        #endregion

        #region events
        protected void OnClickSwitchButton()
        {
            var rem = RememberPassword ? "true" : "false";
            PostUrl = QueryHelper.AddQueryString(UseMobileLogin ? "Account/Mobile" : "Account/Login", new Dictionary<string, string?>()
            {
                [nameof(ReturnUrl)] = ReturnUrl,
                ["AppId"] = AppId,
                ["remember"] = rem
            });
        }
        protected Task OnRememberPassword(bool remember)
        {
            OnClickSwitchButton();
            return Task.CompletedTask;
        }
        protected void OnSignUp()
        {

        }
        protected void OnForgotPassword()
        {

        }
        #endregion

        [JSInvokable]
        public async Task Log(string userName, bool result)
        {
            var clientInfo = await WebClientService.GetClientInfo();
            var city = "XX XX";
            if (!string.IsNullOrEmpty(clientInfo.Ip))
            {
                city = await IPLocatorProvider.Locate(clientInfo.Ip);
            }
            LoginService.Log(userName, clientInfo.Ip, clientInfo.OS, clientInfo.Browser, city, clientInfo.UserAgent, result);
        }

        #region dispose
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Interop != null)
                {
                    Interop.Dispose();
                    Interop = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
