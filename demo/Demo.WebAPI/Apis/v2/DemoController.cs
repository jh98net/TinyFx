using Demo.WebAPI.BLL.Demo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Primitives;
using System.Buffers;
using System.ComponentModel;
using System.IO.Pipelines;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using TinyFx;
using TinyFx.AspNet;
using TinyFx.Configuration;
using TinyFx.IDGenerator;
using TinyFx.Logging;
using TinyFx.Randoms;
using TinyFx.Security;

namespace Demo.WebAPI.Apis.v2
{
    /*
    [ApiVersion("2.0")]
    public class DemoController : TinyFxControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public string Version()
        {
            return "2.0";
        }
    }
    */
}
