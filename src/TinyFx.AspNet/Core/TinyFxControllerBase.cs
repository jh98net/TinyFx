using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Net;
using TinyFx.Security;

namespace TinyFx.AspNet
{
    //[ApiVersion("1.0")]
    //[Area("CMS")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize]
    //[AllowAnonymous]
    public class TinyFxControllerBase : ControllerBase
    {
        #region UserId & Role
        /// <summary>
        /// 当前用户编码
        /// </summary>
        public string UserId => User?.Identity?.Name;
        /// <summary>
        /// 当前用户编码Int64
        /// </summary>
        public long UserIdInt64
        {
            get
            {
                if (string.IsNullOrEmpty(UserId))
                    throw new Exception("UserId为空，当前没有登录用户");
                return UserId.ToInt64();
            }
        }
        /// <summary>
        /// 当前用户编码Int32
        /// </summary>
        public long UserIdInt32
        {
            get
            {
                if (string.IsNullOrEmpty(UserId))
                    throw new Exception("UserId为空，当前没有登录用户");
                return UserId.ToInt32();
            }
        }

        private UserRole? _userRole;
        /// <summary>
        /// 用户角色类型
        /// </summary>
        public UserRole UserRole
        {
            get
            {
                if (!_userRole.HasValue)
                {
                    var role = User?.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Role);
                    _userRole = role == null ? UserRole.Unknow : role.Value.ToEnum(UserRole.Unknow);
                }
                return _userRole.Value;
            }
        }

        /// <summary>
        /// 当前用户是否有Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [NonAction]
        public bool IsInRole(string role) => User.IsInRole(role);
        #endregion

        /*
        /// <summary>
        /// 保存Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [NonAction]
        public void AddSession<T>(string key, T value)
        {
            var data = Encoding.UTF8.GetBytes(SerializerUtil.SerializeJson(value));
            HttpContext.Session.Set(key, data);
        }
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [NonAction]
        public T GetSession<T>(string key, T defaultValue = default(T))
        {
            T ret = defaultValue;
            if (HttpContext.Session.TryGetValue(key, out byte[] data))
            {
                ret = SerializerUtil.DeserializeJson<T>(Encoding.UTF8.GetString(data));
            }
            return ret;
        }
        /// <summary>
        /// 无返回值
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public new ApiResult Ok() => new ApiResult(null);

        /// <summary>
        /// 返回成功200：Api统一对象ApiResult
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        public new ApiResult Ok(object result) => new ApiResult(result);

        /// <summary>
        /// 返回ApiResult错误：定义与客户端协商的code。
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        public ApiResult Error(string code, string message, Exception ex = null, object result = null)
            => new ApiResult(code, message, ex, result);

        /// <summary>
        /// 返回ApiResult错误：定义客户端的message。code = G_BadRequest。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        public ApiResult Error(string message, Exception ex = null, object result = null)
            => new ApiResult(ResponseCode.G_BadRequest, message, ex, result);

        /// <summary>
        /// 返回ApiResult错误：定义与客户端协商的action。code = G_BadRequest。
        /// </summary>
        /// <param name="action"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        public ApiResult Error(int action, string message = null, Exception ex = null, object result = null)
            => new ApiResult(action, message, ex, result);

        /// <summary>
        /// 返回ApiResult错误：定义与客户端协商的code和action
        /// </summary>
        /// <param name="code"></param>
        /// <param name="action"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        public ApiResult Error(string code, int action, string message = null, Exception ex = null, object result = null)
            => new ApiResult(code, action, message, ex, result);
        */
    }
}
