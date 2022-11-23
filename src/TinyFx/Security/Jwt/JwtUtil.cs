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

namespace TinyFx.Security
{
    public static class JwtUtil
    {
        /// <summary>
        /// 创建JWT Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <param name="userIp"></param>
        /// <param name="signSecret"></param>
        /// <returns></returns>
        public static string GenerateJwtToken(object userId, UserRole role, string userIp = null, string signSecret = null)
            => GenerateJwtToken(userId, Convert.ToString(role), userIp, signSecret);

        /// <summary>
        /// 创建JWT Token
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="role">角色UserRole</param>
        /// <param name="userIp">用户IP</param>
        /// <param name="signSecret">签名秘钥</param>
        /// <returns></returns>
        public static string GenerateJwtToken(object userId, string role = null, string userIp = null, string signSecret = null)
        {
            var section = GetSection(userIp, signSecret, out string finalSecret);
            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(finalSecret));
            //
            var uid = Convert.ToString(userId);
            if (string.IsNullOrEmpty(uid))
                throw new Exception("获取Jwt Token时userId不能为空");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, uid),
                    //new Claim(ClaimTypes.Role, role),
                    //new Claim("uip", userIp)
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
            var expire = (section?.ExpiresMinute) ?? 0;
            if (expire > 0)
                tokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(expire);

            //if (!string.IsNullOrEmpty(section?.EncryptSecret))
            //{
            //    var encryptKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section.EncryptSecret));
            //    tokenDescriptor.EncryptingCredentials = new EncryptingCredentials(encryptKey, JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512);
            //}
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// 解码（读取）JWT Token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userIp"></param>
        /// <param name="signSecret"></param>
        /// <returns></returns>
        public static JwtTokenInfo ReadJwtToken(string token, string userIp = null, string signSecret = null)
        {
            var ret = new JwtTokenInfo();
            try
            {
                // signSecret
                var section = GetSection(userIp, signSecret, out string finalSecret);
                var parameters = GetParameters(section, finalSecret);
                var handler = new JwtSecurityTokenHandler();
                var principal = handler.ValidateToken(token, parameters, out SecurityToken stoken);
                ret = ReadJwtToken(principal);
                if (ret.Expires.HasValue && ret.Expires.Value < DateTime.Now)
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
            var ret = new JwtTokenInfo() { 
                Status = JwtTokenStatus.Success,
                Principal = principal,
            };
            // userId
            ret.UserId = principal.Identity.Name;
            var claims = ret.Principal.Claims;
            // role
            var role = claims.FirstOrDefault(item => item.Type == ClaimTypes.Role);
            ret.RoleString = role.Value;
            ret.Role = role == null ? UserRole.Unknow : role.Value.ToEnum(UserRole.Unknow);
            // iat
            var iat = claims.FirstOrDefault(item => item.Type == "iat")?.Value;
            if (iat != null)
                ret.IssuedAt = TinyFxUtil.TimestampToDateTime(iat);
            // userIp
            ret.UserIp = claims.FirstOrDefault(item => item.Type == "uip")?.Value;
            // exp
            var exp = claims.FirstOrDefault(item => item.Type == "exp")?.Value;
            if (exp != null)
                ret.Expires = TinyFxUtil.TimestampToDateTime(exp);
            return ret;
        }
        private static JwtAuthSection GetSection(string userIp, string signSecret, out string finalSecret)
        {
            var section = ConfigUtil.GetSection<JwtAuthSection>()??new JwtAuthSection();
            if (!string.IsNullOrEmpty(signSecret))
                finalSecret = signSecret;
            else if (!string.IsNullOrEmpty(section.SignSecret))
                finalSecret = section.SignSecret;
            else
                throw new Exception("请在配置文件中配置JwtAuth:SignSecret");

            if (section.DynamicSignSecret)
            {
                if (string.IsNullOrEmpty(userIp))
                    throw new Exception("动态SignSecret时用户IP不能为空");
                finalSecret = SecurityUtil.EncryptPassword(finalSecret, userIp);
            }
            return section;
        }

        public static TokenValidationParameters GetParameters(JwtAuthSection section, string signSecret = null)
        {
            var ret = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.FromMinutes(5), // 时钟偏斜可补偿服务器时间漂移
                ValidateIssuerSigningKey = true, //是否验证SecurityKey
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signSecret ?? section.SignSecret)),
                RequireSignedTokens = true,
            };
            //// Token加密
            //if (!string.IsNullOrEmpty(section.EncryptSecret))
            //{
            //    var encryptKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section.EncryptSecret));
            //    x.TokenValidationParameters.TokenDecryptionKey = encryptKey;
            //}

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
