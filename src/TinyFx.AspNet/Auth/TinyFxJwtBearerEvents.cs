using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Net;
using TinyFx.Security;

namespace TinyFx.AspNet
{
    public class TinyFxJwtBearerEvents : JwtBearerEvents
    {
        protected ILogger<TinyFxJwtBearerEvents> Logger;
        protected JwtAuthSection _section;
        public TinyFxJwtBearerEvents()
        {
            Logger = LogUtil.CreateLogger<TinyFxJwtBearerEvents>();
            _section = ConfigUtil.GetSection<JwtAuthSection>();
        }
        /// <summary>
        /// 在第一次收到协议消息时调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task MessageReceived(MessageReceivedContext context)
        {
            if (!string.IsNullOrEmpty(_section.DebugToken)
                && (ConfigUtil.Environment == EnvironmentNames.Development || ConfigUtil.Environment == EnvironmentNames.Testing)
                && !context.Request.Headers.ContainsKey("Authorization")
              )
            {
                context.Token = _section.DebugToken;
            }
            if (_section?.DynamicSignSecret ?? false)
            {
                var uip = AspNetUtil.GetRemoteIpString();
                var key = SecurityUtil.EncryptPassword(_section.SignSecret, uip);
                context.Options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            }
            return base.MessageReceived(context);
        }
        /// <summary>
        /// 在将质询发送回调用方之前调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task Challenge(JwtBearerChallengeContext context)
        {
            if (context.AuthenticateFailure is SecurityTokenExpiredException)
            {
                context.HttpContext.Items.Add(ResponseCode.ErrorCodeKey, ResponseCode.G_JwtTokenExpired);
                context.HttpContext.Items.Add(ResponseCode.ErrorMessageKey, "过期token");
            }
            else
            {
                context.HttpContext.Items.Add(ResponseCode.ErrorCodeKey, ResponseCode.G_JwtTokenInvalid);
                context.HttpContext.Items.Add(ResponseCode.ErrorMessageKey, "无效token");
            }
            return base.Challenge(context);
        }
        /// <summary>
        /// 在安全令牌通过验证并生成 ClaimsIdentity 后调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenValidated(TokenValidatedContext context)
        {
            /*
            var token = context.SecurityToken as JwtSecurityToken;
            if (token != null)
            {
                var jwt = JwtUtil.ReadJwtToken(context.Principal);
            }
            */
            return base.TokenValidated(context);
        }
        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            return base.AuthenticationFailed(context);
        }
        public override Task Forbidden(ForbiddenContext context)
        {
            return base.Forbidden(context);
        }
    }

}
