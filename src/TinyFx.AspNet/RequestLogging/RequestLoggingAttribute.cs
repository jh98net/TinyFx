using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TinyFx.Logging;

namespace TinyFx.AspNet.RequestLogging
{
    public class RequestLoggingAttribute : Attribute, IAsyncActionFilter
    {
        private LogLevel _level;
        private bool _headers;
        private bool _requestBody;
        private bool _responseBody;
        public RequestLoggingAttribute(LogLevel level = LogLevel.Debug, bool headers = true, bool requestBody = true, bool responseBody = true)
        {
            _level = level;
            _headers = headers;
            _requestBody = requestBody;
            _responseBody = responseBody;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = context.HttpContext.RequestServices?.GetService<ILogBuilder>();
            if(logger != null)
            {
                logger.Level = _level;
                logger.LogRequestHeaders = _headers;
                logger.LogResponseBody = _requestBody;
                logger.LogRequestBody = _responseBody;
            }
            await next.Invoke();
        }
    }
}
