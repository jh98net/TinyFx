using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TinyFx.Configuration;
using System.Linq;
using TinyFx;
using TinyFx.Security;
using Microsoft.AspNetCore.DataProtection;

namespace TinyFx.Security
{
    public static class JwtUtil
    {
        /// <summary>
        /// 创建JWT Token
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="role">角色UserRole</param>
        /// <param name="userIp">用户IP</param>
        /// <param name="customData">自定义数据</param>
        /// <param name="signingKey">签名秘钥</param>
        /// <returns></returns>
        public static string GenerateJwtToken(object userId, UserRole role, string userIp = null, string customData = null, string signingKey = null)
            => GenerateJwtToken(userId, Convert.ToString(role), userIp, customData, signingKey);

        /// <summary>
        /// 创建JWT Token
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="role">角色UserRole</param>
        /// <param name="userIp">用户IP</param>
        /// <param name="customData">自定义数据</param>
        /// <param name="signingKey">签名秘钥</param>
        /// <returns></returns>
        public static string GenerateJwtToken(object userId, string role = null, string userIp = null, string customData = null, string signingKey = null)
        {
            var section = GetSection(signingKey);
            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section.SigningKey));
            //
            var uid = Convert.ToString(userId);
            if (string.IsNullOrEmpty(uid))
                throw new Exception("获取Jwt Token时userId不能为空");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, uid)
                }),
                Issuer = section?.Issuer,
                Audience = section?.Audience,
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256Signature)
            };
            if (!string.IsNullOrEmpty(role))
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            if (!string.IsNullOrEmpty(userIp))
                tokenDescriptor.Subject.AddClaim(new Claim("uip", userIp));
            if (!string.IsNullOrEmpty(customData))
                tokenDescriptor.Subject.AddClaim(new Claim("custom", customData));
            var expire = (section?.ExpireMinutes) ?? 0;
            if (expire > 0)
                tokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(expire);

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// 解码（读取）JWT Token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="signingKey"></param>
        /// <returns></returns>
        public static JwtTokenInfo ReadJwtToken(string token, string signingKey = null)
        {
            var ret = new JwtTokenInfo();
            try
            {
                // signSecret
                var section = GetSection(signingKey);
                var parameters = GetParameters(section);
                var handler = new JwtSecurityTokenHandler();
                var principal = handler.ValidateToken(token, parameters, out SecurityToken stoken);
                ret = ReadJwtToken(principal);
                if (ret.Expires.HasValue && ret.Expires.Value < DateTime.UtcNow)
                    ret.Status = JwtTokenStatus.Expired;
            }
            catch (SecurityTokenExpiredException)
            {
                ret.Status = JwtTokenStatus.Expired;
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                ret.Status = JwtTokenStatus.Invalid;
            }
            catch (Exception)
            {
                ret.Status = JwtTokenStatus.Invalid;
            }
            return ret;
        }

        public static JwtTokenInfo ReadJwtToken(ClaimsPrincipal principal)
        {
            var ret = new JwtTokenInfo()
            {
                Status = JwtTokenStatus.Success,
                Principal = principal,
            };
            // userId
            ret.UserId = principal.Identity.Name;
            var claims = ret.Principal.Claims;
            // role
            var role = claims.FirstOrDefault(item => item.Type == ClaimTypes.Role);
            ret.Role = role == null ? UserRole.Unknow : role.Value.ToEnum(UserRole.Unknow);
            ret.RoleString = role?.Value;
            // iat
            var iat = claims.FirstOrDefault(item => item.Type == "iat")?.Value;
            if (iat != null)
                ret.IssuedAt = TinyFxUtil.TimestampToDateTime(iat);
            // userIp
            ret.UserIp = claims.FirstOrDefault(item => item.Type == "uip")?.Value;
            // customData
            ret.CustomData = claims.FirstOrDefault(item => item.Type == "custom")?.Value;
            // exp
            var exp = claims.FirstOrDefault(item => item.Type == "exp")?.Value;
            if (exp != null)
                ret.Expires = TinyFxUtil.TimestampToUtcDateTime(exp);
            return ret;
        }
        private static JwtAuthSection GetSection(string signingKey = null)
        {
            var section = ConfigUtil.GetSection<JwtAuthSection>() ?? new JwtAuthSection();
            if (!string.IsNullOrEmpty(signingKey))
                section.SigningKey = signingKey;
            if (string.IsNullOrEmpty(section.SigningKey))
                throw new Exception("请在配置文件中配置JwtAuth:SignSecret"); 
            return section;
        }

        public static TokenValidationParameters GetParameters(JwtAuthSection section)
        {
            var ret = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.FromMinutes(10), // 时钟偏斜可补偿服务器时间漂移
                ValidateIssuerSigningKey = true, //是否验证SigningKey
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section.SigningKey)),
                RequireSignedTokens = true,
            };

            //是否验证失效时间
            ret.ValidateLifetime = section.ValidateLifetime;
            if (section.ValidateLifetime)
            {
                ret.RequireExpirationTime = true;
            }
            // 验证颁发者
            if (!string.IsNullOrEmpty(section.Issuer))
            {
                ret.ValidateIssuer = true;
                ret.ValidIssuer = section.Issuer;
            }

            // 验证授权
            if (!string.IsNullOrEmpty(section.Audience))
            {
                ret.ValidateAudience = true;
                ret.ValidAudience = section.Audience;
            }

            return ret;
        }
    }
}
