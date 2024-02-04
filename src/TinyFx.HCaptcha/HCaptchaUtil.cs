using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.AspNet;

namespace TinyFx.HCaptcha
{
    public static class HCaptchaUtil
    {
        private static HCaptchaProvider _provider = new();

        /// <summary>
        /// 验证HCaptcha返回的token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="remoteIp"></param>
        /// <returns></returns>
        public static async Task<ApiResult<HCaptchaVerifyRsp>> Verify(string token, string? remoteIp = null)
        {
            return await _provider.Verify(token,remoteIp);
        }
    }
}
