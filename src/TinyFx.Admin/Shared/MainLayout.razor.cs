using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Admin.BLL.Services;
using TinyFx.Admin.DAL;

namespace TinyFx.Admin.Shared
{
    public partial class MainLayout
    {
        #region Inject
        [Inject]
        [NotNull]
        private WebClientService? WebClientService { get; set; }

        [Inject]
        [NotNull]
        private NavigationManager? NavigationManager { get; set; }
        [Inject]
        [NotNull]
        private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
        [Inject]
        [NotNull]
        private BootstrapAppContext? AppContext { get; set; }
        [Inject]
        [NotNull]
        private ToastService? ToastService { get; set; }
        [Inject]
        [NotNull]
        private IIPLocatorProvider? IPLocatorProvider { get; set; }
        #endregion

        #region Properties
        private IEnumerable<MenuItem>? MenuItems { get; set; }
        private string? Title { get; set; }

        private string? Footer { get; set; }

        private string? UserName { get; set; }

        private bool Lock { get; set; }

        private int LockInterval { get; set; }

        [NotNull]
        private string? Icon { get; set; }
        #endregion

        protected override void OnInitialized()
        {
            base.OnInitialized();
            AppContext.BaseUri = NavigationManager.ToAbsoluteUri(NavigationManager.BaseUri);
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }
        
        private void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            _ = Task.Run(async () =>
            {
                // TODO: 可考虑加入队列中，通过任务管理定时插入提高效率
                var clientInfo = await WebClientService.GetClientInfo();
                var city = "XX XX";
                if (!string.IsNullOrEmpty(clientInfo.Ip))
                {
                    city = await IPLocatorProvider.Locate(clientInfo.Ip) ?? "None";
                }
                new Admin_req_logMO().Add(new Admin_req_logEO { 
                    LogID = new Guid().ToString(),
                    UserID = AppContext.UserID,
                    Type = 1,
                    RequestUrl = NavigationManager.ToBaseRelativePath(e.Location),
                    IP = clientInfo.Ip,
                    OS = clientInfo.OS,
                    Browser = clientInfo.Browser,
                    Location = city,
                    UserAgent = clientInfo.UserAgent
                });
            });
        }

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = state.User.Identity?.Name;

            Title = "后台管理";
            Footer = "2022 © 通用后台管理系统";
        }
    }
}
