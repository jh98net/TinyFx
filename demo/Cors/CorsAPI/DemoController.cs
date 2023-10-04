using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TinyFx;
using TinyFx.AspNet;

namespace CorsAPI
{
    [AllowAnonymous]
    public class DemoController : TinyFxControllerBase
    {
        [HttpGet]
        [EnableCors("xxyy")]
        public string test()
        {
            return "cors OK";
        }

        [HttpGet]
        public void add(string origin=null)
        {
            origin ??= "http://localhost:5030";
            var element = new CorsPolicyElement
            {
                Name = "xxyy",
                Origins = origin
            };

            //var policy = AspNetUtil.GetPolicyBuilder(element);
            //DIUtil.Services.Configure<CorsOptions>((opts) => 
            //{
            //    opts.AddDefaultPolicy(policy);
            //});
        }
    }
}
