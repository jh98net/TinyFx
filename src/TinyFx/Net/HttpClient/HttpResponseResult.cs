using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TinyFx.Net
{
    public class HttpResponseResult<TSuccess, TError>: HttpResponseResult
    {
        public TSuccess SuccessResult { get; set; }
        public TError ErrorResult { get; set; }
    }
    public class HttpResponseResult
    {
        public bool Success { get; set; }
        public string ResultString { get; set; }
        public HttpRequestBody Request { get; set; }
        public HttpResponseBody Response { get; set; }
        public Exception Exception { get; set; }
    }
}
