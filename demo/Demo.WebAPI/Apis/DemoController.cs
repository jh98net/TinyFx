using Demo.WebAPI.BLL.Demo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Buffers;
using System.ComponentModel;
using System.IO.Pipelines;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using TinyFx;
using TinyFx.AspNet;
using TinyFx.AspNet.Filters;
using TinyFx.AspNet.RequestLogging;
using TinyFx.AspNet.ResponseCaching;
using TinyFx.Configuration;
using TinyFx.Extensions.IDGenerator;
using TinyFx.Logging;
using TinyFx.Net;
using TinyFx.Randoms;
using TinyFx.Security;

namespace Demo.WebAPI.Apis
{
    /// <summary>
    /// 测试Demo API
    ///     无返回值: void
    ///     有具体返回值：ApiResult(T)
    /// </summary>
    //[ApiAccessFilter()]
    //[IgnoreActionFilter]
    //[ApiController]
    //[Route("api/[controller]/[action]")]
    //[EnableCors()]
    public class DemoController : TinyFxControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public string GetJwtToken()
        {
            var logger = AspNetUtil.GetContextLogBuilder();
            logger.AddMessage("AAAAAAAAAAAA");
            var uip = AspNetUtil.GetRemoteIpString();
            return JwtUtil.GenerateJwtToken(RandomUtil.NextInt(10), UserRole.User, uip);
        }
        [HttpGet]
        [AllowAnonymous]
        public string t1()
        {
            throw new Exception("ASDFASDF234");
        }
        [HttpGet]
        [AllowAnonymous]
        public void T2()
        {
            throw new CustomException("code...", "bbb");
        }
        /*
         #region Base
         /// <summary>
         /// 获取JwtToken
         /// </summary>
         /// <returns></returns>
         [HttpGet]
         [AllowAnonymous]
         public string GetJwtToken()
         {
             var uip = AspNetUtil.GetRemoteIpString();
             return JwtUtil.GenerateJwtToken(RandomUtil.NextInt(10), UserRole.User, uip);
         }

         [HttpGet]
         public string CheckJwtToken() => $"成功! UserId: {UserId} UserRole: {UserRole}";

         /// <summary>
         /// 返回结果
         /// </summary>
         /// <param name="type">返回类型 </param>
         [HttpGet]
         [AllowAnonymous]
         public IActionResult GetResult(int type = 0)
         {
             switch (type)
             {
                 case 0:
                     return Ok();
                 case 1:
                     return Ok(type);
                 case 2:
                     return StatusCode(404, type);
                 case 3:
                     return Content("文本内容", "text/plain", Encoding.UTF8);
                 case 4:
                     return NoContent();
                 case 5:
                     var filePath = "appsettings.json";
                     var provider = new FileExtensionContentTypeProvider();
                     if (!provider.TryGetContentType(filePath, out var contentType))
                         contentType = "application/octet-stream";
                     return File(System.IO.File.ReadAllBytes(filePath), contentType, Path.GetFileName(filePath));
                 case 6:
                     return Unauthorized(type);
                 case 7:
                     return NotFound();
                 case 8:
                     return BadRequest(type);
                 case 9:
                     throw new CustomException("customErrCode", "客户端Action=1", null, "其他数据");
                 case 10:
                     var i = 0;
                     var j = 1;
                     var k = j / i;
                     break;
                 case 11:
                     return (ObjectResult)new ApiResult<int>(123);
             }
             return Ok();
         }
         #endregion

         [HttpGet]
         [AllowAnonymous]
         public void Get01(int id, string name)
         {
             var a = IDGeneratorUtil.NextId();
             LogUtil.Debug($"id: {id} name: {name}");
         }
         [HttpPost]
         [AllowAnonymous]
         public void Get02([FromQuery] DemoIpo ipo, [FromForm] DemoIpo2 ipo2)
         {
             return;
         }

         [HttpPost]
         [AllowAnonymous]
         [ApiAccessFilter()]
         public void Test()
         {
             var i = 0;
             var a = 100 / i;
             //throw new CustomException("sdf", null);
         }

         [HttpPost]
         [AllowAnonymous]
         public DemoIpo2 PostIpo(DemoIpo ipo)
         {
             return new DemoIpo2 { Id = ipo.Name.Length };
         }

         [HttpPost]
         [AllowAnonymous]
         public DemoIpo2 PostIpo2(Demo.WebAPI.Apis2.DemoIpo ipo)
         {
             return new DemoIpo2 { Id = ipo.Name.Length };
         }

         [HttpGet]
         [AllowAnonymous]
         [RequestLogging]
         public string Version()
         {
             return "1.0";
         }

         [HttpGet]
         [AllowAnonymous]
         //[ResponseCacheEx("default")]
         [ResponseCacheKeys(10, "*")]
         public string Check(int id, string name)
         {
             return DateTime.Now.ToString("HH:mm:ss");
         }

         [HttpGet]
         [AllowAnonymous]
         public string Test1(string origin)
         {
             return "ok";
         }
         [HttpGet]
         [AllowAnonymous]
         [EnableCors()]
         public string Test2()
         {
             return "test2";
         }
         [HttpGet]
         [AllowAnonymous]
         [EnableCors("aaa")]
         public string Test3()
         {
             return "test3";
         }
         */
    }

    public class DemoIpo
    {
        public string Name { get; set; }
    }
    public class DemoIpo2
    {
        public int Id { get; set; }
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

namespace Demo.WebAPI.Apis2
{
    public class DemoIpo
    {
        public string Name { get; set; }
    }
}
