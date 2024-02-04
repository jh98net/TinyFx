using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Net;

namespace TinyFx.AspNet.HCaptcha.Common
{
    internal class HCaptchaProvider
    {
        private HCaptchaSection _section;
        private HttpClientEx _client;
        public HCaptchaProvider()
        {
            _section = ConfigUtil.GetSection<HCaptchaSection>();
            _client = HttpClientExFactory.CreateClientEx(new HttpClientConfig
            {
                BaseAddress = _section.ApiBaseUrl,
            });
        }

        public async Task<ApiResult<HCaptchaVerifyRsp>> Verify(string token, string? remoteIp = null)
        {
            var ret = new ApiResult<HCaptchaVerifyRsp>();
            try
            {
                var req = new HCaptchaVerifyReq
                {
                    Secret = _section.Secret,
                    Response = token,
                    RemoteIp = _section.VerifyRemoteIp ? remoteIp : null
                };
                var rsp = await _client.CreateAgent()
                    .AddUrl("/siteverify")
                    .BuildJsonContent(req)
                    .PostAsync<HCaptchaVerifyRsp, object>();
                if (!rsp.Success)
                {
                    ret.Success = false;
                    ret.Exception = rsp.Exception;
                }
                else
                {
                    ret.Success = rsp.Success && rsp.SuccessResult.Success;
                    ret.Result = rsp.SuccessResult;
                }
            }
            catch (Exception ex)
            {
                ret.Success = false;
                ret.Exception = ex;
            }
            return ret;
        }
    }
}
