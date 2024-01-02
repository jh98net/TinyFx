using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;

namespace TinyFx.AspNet
{
    /// <summary>
    /// 客户端同步请求通知Attribute
    /// </summary>
    public class SyncNotifyAttribute : Attribute, IAsyncActionFilter
    {
        public const string HEADER_NAME = "tinyfx-sync";
        private ISyncNotifyService _provider;
        private bool _enabled = false;
        public SyncNotifyAttribute()
        {
            _enabled = ConfigUtil.GetSection<AspNetSection>()?.UseSyncNotify ?? false;
            _provider = DIUtil.GetService<ISyncNotifyService>();
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_enabled && _provider != null)
            {
                var userId = context?.HttpContext?.User?.Identity?.Name;
                if (!string.IsNullOrEmpty(userId))
                {
                    var value = await _provider.GetNotify(userId);
                    context?.HttpContext?.Response?.Headers?.Add(HEADER_NAME, Convert.ToString(value));
                }
            }
        }
    }
}
