﻿using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration
{
    public class AspNetSection : ConfigSection
    {
        public override string SectionName => "AspNet";
        /// <summary>
        /// 是否缓存Request.Body，如果不需要读取Body设置为false
        /// </summary>
        public bool UseRequestBuffering { get; set; } = true;
        /// <summary>
        /// 是否使用ApiActionFilter
        /// </summary>
        public bool UseApiActionResultFilter { get; set; } = true;
        /// <summary>
        /// 是否使用ApiModelStateFilter（屏蔽[ApiController]的自动 400 响应） 
        /// </summary>
        public bool UseModelStateFilter { get; set; } = true;
        /// <summary>
        /// 是否启用压缩br,gzip
        /// </summary>
        public bool UseResponseCompression { get; set; } = true;
        /// <summary>
        /// 是否开启版本控制
        /// </summary>
        public bool UseApiVersioning { get; set; } = true;
        public SwaggerConfig Swagger { get; set; } = new();
        /// <summary>
        /// 项目基础路径
        /// </summary>
        public string PathBase { get; set; }
        /// <summary>
        /// 请求宽限期（秒）
        /// </summary>
        public int RequestPeriodSecond { get; set; } = 15;
        /// <summary>
        /// 请求每秒字节限制
        /// </summary>
        public int RequestBytesPerSecond { get; set; } = 100;
        /// <summary>
        /// 动态加载的API所在的Assembly列表
        /// </summary>
        public List<string> DynamicApiAssemblies { get; set; } = new();

        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
        }
    }

    public class SwaggerConfig
    {
        public bool Enabled { get; set; } = true;
        /// <summary>
        /// 生成接口用到的类型信息时，是否使用带命名空间的全名称，以避免重名异常
        /// </summary>
        public bool UseSchemaFullName { get; set; }
    }
}
