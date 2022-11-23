using Microsoft.AspNetCore.Http;

namespace System.Web
{
    public static class HttpContextEx
    {
        private static IHttpContextAccessor _contextAccessor;


        public static HttpContext Current => _contextAccessor.HttpContext;


        internal static void Configure(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
    }
}