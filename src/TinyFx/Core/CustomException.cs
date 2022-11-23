using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using TinyFx.Net;

namespace TinyFx
{
    /// <summary>
    /// 自定义业务异常，服务器与客户端约定
    /// Code 默认 ResponseCode.G_BadRequest
    /// </summary>
    public class CustomException : Exception
    {
        /// <summary>
        /// 业务错误代码，WebAPI和Server通用
        /// </summary>
        public string Code { get; set; } = ResponseCode.G_BadRequest;
        /// <summary>
        /// 返回给客户端的数据
        /// </summary>
        public object Result { get; set; }
        private string _stackTrace;
        public override string StackTrace => string.IsNullOrEmpty(_stackTrace) ? base.StackTrace : _stackTrace;
        public void SetStackTrace(string value) => _stackTrace = value;
        private string _message;
        public override string Message => string.IsNullOrEmpty(_message) ? base.Message : _message;
        public void SetMessage(string value) => _message = value;

        public CustomException(string message) : base(message)
        {
            Code = ResponseCode.G_BadRequest;
        }
        public CustomException(string code, string message, Exception innerException = null, object result = null) : base(message, innerException)
        {
            Code = string.IsNullOrEmpty(code) ? ResponseCode.G_BadRequest: code;
            Result = result;
        }
    }
}
