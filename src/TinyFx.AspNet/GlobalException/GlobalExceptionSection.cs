using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TinyFx.AspNet;
using TinyFx.Configuration;

namespace TinyFx.Configuration
{
    public class GlobalExceptionSection : ConfigSection
    {
        public override string SectionName => "GlobalException";

        /// <summary>
        /// 异常处理方式
        /// </summary>
        [JsonIgnore]
        public ExceptionHandleType HandleType { get; set; }

        /// <summary>
        /// Json处理方式的Url关键字
        /// <para>仅HandleType=Both时生效</para>
        /// </summary>
        public IList<PathString> JsonHandleUrlKeys { get; set; }

        /// <summary>
        /// 错误跳转页面
        /// </summary>
        public PathString ErrorHandingPath { get; set; }
        /// <summary>
        /// 预定义的StatusCode码
        /// </summary>
        public Dictionary<int, string> ExceptionStatusCodeDic { get; set; }

        public override void Bind(IConfiguration configuration)
        {
            //base.Bind(configuration);
            //
            var el = configuration.GetValue<string>("HandleType")?.ToLower();
            switch (el)
            {
                case "json":
                    HandleType = ExceptionHandleType.JsonHandle;
                    break;
                case "page":
                    HandleType = ExceptionHandleType.PageHandle;
                    break;
                default:
                    HandleType = ExceptionHandleType.Both;
                    break;
            }
            //
            el = configuration.GetValue<string>("JsonHandleUrlKeys");
            if (!string.IsNullOrEmpty(el))
            {
                JsonHandleUrlKeys = new List<PathString>();
                foreach (var item in el.Split(';'))
                {
                    JsonHandleUrlKeys.Add(item.Trim());
                }
            }
            //
            ErrorHandingPath = configuration.GetValue<string>("ErrorHandingPath");
            //
            var els = configuration.GetSection("exceptionStatusCodeDic").Get<ExceptionStatusCodeDicElement[]>();
            if (els != null)
            {
                ExceptionStatusCodeDic = new Dictionary<int, string>();
                foreach (var item in els)
                {
                    ExceptionStatusCodeDic.Add(item.StatusCode, item.Message);
                }
            }
        }
    }
}

namespace TinyFx.AspNet
{
    public class ExceptionStatusCodeDicElement
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
    /// <summary>
    /// 错误处理方式
    /// </summary>
    public enum ExceptionHandleType
    {
        JsonHandle = 0,   //Json形式处理
        PageHandle = 1,   //跳转网页处理
        Both = 2          //根据Url关键字自动处理
    }
}
