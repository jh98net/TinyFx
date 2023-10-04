using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFx.Net
{
    /// <summary>
    /// 平台约定的错误码:
    ///     CustomException.Code
    ///     ApiResult.Code
    ///     ProtoResponse.Code
    /// </summary>
    public class ResponseCode
    {
        #region 基础
        /// <summary>
        /// 请求错误
        /// </summary>
        public const string G_BadRequest = "G_BadRequest";
        /// <summary>
        /// 服务器未处理异常
        /// </summary>
        public const string G_InternalServerError = "G_InternalServerError";
        #endregion

        #region 认证和安全
        /// <summary>
        /// 授权错误:请求的资源需要身份验证
        /// </summary>
        public const string G_Unauthorized = "G_Unauthorized";
        /// <summary>
        /// Jwt Token无效
        /// </summary>
        public const string G_JwtTokenInvalid = "G_JwtTokenInvalid";
        /// <summary>
        /// Jwt Token过期
        /// </summary>
        public const string G_JwtTokenExpired = "G_JwtTokenExpired";
        /// <summary>
        /// Ticket无效
        /// </summary>
        public const string G_TicketInvalid = "G_TicketInvalid";
        /// <summary>
        /// IP请求频率超过限制。statusCode：429
        /// </summary>
        public const string G_RequestRateLimit = "G_RequestRateLimit";
        /// <summary>
        /// 用户IP或User被服务器封禁，客户端提示与管理员联系，不再允许连接
        /// </summary>
        public const string G_IpOrUserLimit = "G_IpOrUserLimit";
        /// <summary>
        /// 服务器拒绝客户端连接（疑似被攻击），客户端需要延迟倒数秒数再做后续尝试！
        /// </summary>
        public const string G_ServiceDenyConnect = "G_ServiceDenyConnect";
        #endregion

        #region 服务器
        /// <summary>
        /// 虽然建立连接但未准备好Session，稍等再通讯
        /// </summary>
        public const string G_ServiceNotReady = "G_ServiceNotReady";
        /// <summary>
        /// 服务器超载，需要重新请求服务地址
        /// </summary>
        public const string G_ServiceOverload = "G_ServiceOverload";
        /// <summary>
        /// 系统维护中
        /// </summary>
        public const string G_ServiceStopped = "G_ServiceStopped";
        /// <summary>
        /// 系统终止，客户端从头进入
        /// </summary>
        public const string G_ServiceError = "G_ServiceError";
        #endregion
    }
}
