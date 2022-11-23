using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Logging;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Admin.BLL.Services;
using TinyFx.Admin.DAL;
using TinyFx.AspNet;
using TinyFx.Caching;
using TinyFx.Configuration;
using TinyFx.Security;

namespace TinyFx.Admin.Controllers
{
    public class AccountController : TinyFxControllerBase
    {
        #region login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginIpo ipo, [FromServices] BootstrapAppContext context)
        {
            if (string.IsNullOrEmpty(ipo.UserName) || string.IsNullOrEmpty(ipo.Password))
                return RedirectLogin();

            var mo = new Admin_userMO();
            var user = await mo.GetSingleAsync("status=1 and username=@username", ipo.UserName);
            if (user == null)
                throw new CustomException("用户名不存在");
            if (!SecurityUtil.ValidatePassword(ipo.Password, user.Password, user.PasswordSalt))
                throw new CustomException("用户名或密码错误");
            var period = ipo.Remember ? AdminUtil.Options.CookieExpireDays : 0;
            context.UserID = user.UserID;
            context.UserName = user.UserName;
            context.BaseUri = new Uri($"{Request.Scheme}://{Request.Host}/");

            return await SignInAsync(ipo.UserName, ipo.ReturnUrl ?? "/index", ipo.Remember, period);
        }
        private async Task<IActionResult> SignInAsync(string userName, string returnUrl, bool persistent, int period = 0, string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme)
        {
            var identity = new ClaimsIdentity(authenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, userName));

            var properties = new AuthenticationProperties();
            if (persistent)
                properties.IsPersistent = true;
            if (period != 0)
                properties.ExpiresUtc = DateTimeOffset.Now.AddDays(period);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

            return Redirect(returnUrl);
        }
        private IActionResult RedirectLogin(string? returnUrl = null)
        {
            var url = returnUrl;
            if (string.IsNullOrEmpty(url))
            {
                var query = Request.Query.Aggregate(new Dictionary<string, string?>(), (d, v) =>
                {
                    d.Add(v.Key, v.Value.ToString());
                    return d;
                });
                url = QueryHelpers.AddQueryString(Request.PathBase + CookieAuthenticationDefaults.LoginPath, query);
            }
            return Redirect(url);
        }
        #endregion

        #region Logout
        [HttpGet]
        [AllowAnonymous]
        public async Task<RedirectResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect(Request.PathBase + CookieAuthenticationDefaults.LoginPath);
        }
        #endregion

        #region Mobile Login
        /// <summary>
        /// 短信验证登陆方法
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> MobileLogin(MobileLoginIpo ipo, [FromServices] BootstrapAppContext context)
        {
            if (string.IsNullOrEmpty(ipo.Phone) || string.IsNullOrEmpty(ipo.Code))
                return RedirectLogin();

            var auth = TicketCacheUtil.ValidateTicket(ipo.Phone, ipo.Code);
            if(!auth)
                throw new CustomException("验证码错误");

            var period = ipo.Remember ? AdminUtil.Options.CookieExpireDays : 0;
            var mo = new Admin_userMO();
            var eo = await mo.GetByPKAsync(ipo.Phone);
            if (eo == null)
            {
                eo = new Admin_userEO {
                    UserID = new Guid().ToString(),
                    UserName = ipo.Phone,
                    Mobile = ipo.Phone,
                    DisplayName = "手机用户",
                    IsAdmin = false,
                    RegisterDate = DateTime.Now,
                    ApprovedDate = DateTime.Now,
                    ApprovedBy = "Mobile",
                    Status = 1,
                    Icon = "default.jpg"
                };
                mo.Add(eo);
            }

            context.UserName = ipo.Phone;
            context.BaseUri = new Uri(Request.Path.Value!);
            return await SignInAsync(ipo.Phone, ipo.ReturnUrl ?? "/index", ipo.Remember, period);
        }

        #endregion
    }
    public class LoginIpo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
        public string ReturnUrl { get; set; }
    }
    public class MobileLoginIpo
    {
        public string Phone { get; set; }
        public string Code { get; set; }
        public bool Remember { get; set; }
        public string ReturnUrl { get; set; }
    }
}
