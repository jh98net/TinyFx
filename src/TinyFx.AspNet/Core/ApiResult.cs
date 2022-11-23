﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Net;

namespace TinyFx.AspNet
{
    public class ApiResultBase : IResponseBase
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 自定义异常码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; set; }
    }
    /// <summary>
    /// Api返回的统一结构
    /// </summary>
    public class ApiResult : ApiResultBase, IResponseResult
    {
        /// <summary>
        /// 返回给客户端的数据
        /// </summary>
        public object Result { get; set; }
        /// <summary>
        /// 构造函数-返回200
        /// </summary>
        /// <param name="result"></param>
        public ApiResult(object result = null)
        {
            Success = true;
            Status = 200;
            Result = result;
        }
        public ApiResult(string message)
        {
            Code = ResponseCode.G_BadRequest;
            Success = false;
            Status = 400;
            Message = message;
        }
        public ApiResult(string code, string message, Exception ex = null, object result = null)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException("ApiResult构造错误时Code不能为null", "code");
            Success = false;
            Status = 400;
            Code = code;
            Message = message;
            Exception = ex;
            Result = result;
        }
        public static implicit operator ObjectResult(ApiResult value)
        {
            var ret = new ObjectResult(value);
            return ret;
        }
    }
    /// <summary>
    /// Api返回的统一结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResultBase, IResponseResult<T>
    {
        /// <summary>
        /// 返回的数据
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// 构造函数-返回200
        /// </summary>
        /// <param name="result"></param>
        public ApiResult(T result = default(T))
        {
            Success = true;
            Status = 200;
            Result = result;
        }
        public ApiResult(string message)
        {
            Code = ResponseCode.G_BadRequest;
            Success = false;
            Status = 400;
            Message = message;
        }
        /// <summary>
        /// 构造函数-返回400
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public ApiResult(string code, string message, Exception ex = null)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException("ApiResult构造错误时Code不能为null", "code");
            Success = false;
            Status = 400;
            Code = code;
            Message = message;
            Exception = ex;
        }
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ApiResult<T>(T value)
            => new ApiResult<T>(value);
        public static implicit operator ApiResult<T>(ApiResult value)
        {
            var ret = new ApiResult<T>()
            {
                Success = value.Success,
                Status = value.Status,
                Code = value.Code,
                Message = value.Message,
                Exception = value.Exception,
                Result = (T)value.Result
            };
            return ret;
        }
        public static implicit operator ObjectResult(ApiResult<T> value)
        {
            var ret = new ObjectResult(value);
            return ret;
        }
    }
}
