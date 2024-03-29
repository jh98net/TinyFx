﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class TinyJwtBearerEvents : JwtBearerEvents
    {
        protected ILogger<TinyJwtBearerEvents> Logger;
        public TinyJwtBearerEvents()
        {
            Logger = LogUtil.CreateLogger<TinyJwtBearerEvents>();
        }
        /// <summary>
        /// 在第一次收到协议消息时调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task MessageReceived(MessageReceivedContext context)
        {
            var section = ConfigUtil.GetSection<JwtAuthSection>();
            if (string.IsNullOrEmpty(context.Token)
                && !string.IsNullOrEmpty(section.DebugToken)
                && (ConfigUtil.IsDebugEnvironment)
              )
            {
                context.Token = section.DebugToken;
            }
            if (section?.DynamicSignSecret ?? false)
            {
                var uip = AspNetUtil.GetRemoteIpString();
                var key = SecurityUtil.EncryptPassword(section.SignSecret, uip);
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
                context.HttpContext.Items.Add(GlobalExceptionUtil.ERROR_CODE_KEY, ResponseCode.G_JwtTokenExpired);
                context.HttpContext.Items.Add(GlobalExceptionUtil.ERROR_MESSAGE_KEY, "jwt token过期");
            }
            else
            {
                context.HttpContext.Items.Add(GlobalExceptionUtil.ERROR_CODE_KEY, ResponseCode.G_JwtTokenInvalid);
                context.HttpContext.Items.Add(GlobalExceptionUtil.ERROR_MESSAGE_KEY, "jwt token无效");
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
