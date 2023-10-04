using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization.Policy;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IO.Pipelines;
using System.Buffers;
using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography;
using TinyFx.Security;

namespace TinyFx.AspNet
{
    /// <summary>
    /// 验证Headers中的签名有效性
    ///     sign = Headers[HeaderName]
    ///     source = Request.Body
    ///     验证结果：verify(source, sign, publicKey)
    /// 默认验证采用：openssl publick key + SHA256 + Base64
    /// </summary>
    public abstract class AuthHeaderSignFilterBase : ActionFilterAttribute
    {
        public abstract string HeaderName { get; }
        public abstract string PublicKey { get; }
        
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // AllowAnonymous
            var attr1 = context.Controller.GetType()
                .GetCustomAttributes<AllowAnonymousAttribute>().FirstOrDefault();
            if (attr1 != null)
            {
                await next();
                return;
            }
            var attr2 = (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo
                .GetCustomAttributes<AllowAnonymousAttribute>().FirstOrDefault();
            if (attr2 != null)
            {
                await next();
                return;
            }

            // sign
            var request = context.HttpContext.Request;
            if (!request.Headers.TryGetValue(HeaderName, out StringValues values))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var sign = values.FirstOrDefault();
            if (string.IsNullOrEmpty(sign))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // source
            string source = await AspNetUtil.GetRawBodyAsync(context.HttpContext.Request);
            if (string.IsNullOrEmpty(source))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // veriry
            if (!VerifySign(source, sign))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }

        protected virtual bool VerifySign(string source, string sign)
        {
            return SecurityUtil.RSAVerifyData(source, sign, PublicKey, RSAKeyMode.RSAPublicKey
                , HashAlgorithmName.SHA256, CipherEncode.Base64);
        }
    }
}
