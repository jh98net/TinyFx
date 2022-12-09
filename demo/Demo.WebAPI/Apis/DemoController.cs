using Demo.WebAPI.BLL.Demo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Text;
using TinyFx;
using TinyFx.AspNet;
using TinyFx.Logging;
using TinyFx.Randoms;
using TinyFx.Security;

namespace Demo.WebAPI.Apis
{
    /// <summary>
    /// 测试Demo API
    ///     无返回值: void
    ///     有具体返回值：ApiResult(T)
    /// </summary>
    [ApiAccessFilter()]
    public class DemoController : TinyFxControllerBase
    {
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
                    throw new CustomException("customErr","客户端Action=1", null, "其他数据");
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
            LogUtil.Debug($"id: {id} name: {name}");
        }

        [HttpGet]
        [AllowAnonymous]
        public DemoDto Get02(DemoIpo ipo) => new DemoDto() { Name = ipo.Value };

        [HttpPost]
        [AllowAnonymous]
        public Demo_userEO Post01()
        {
            return new Demo_userMO().GetTop(null, 1).FirstOrDefault();
        }
    }
}
