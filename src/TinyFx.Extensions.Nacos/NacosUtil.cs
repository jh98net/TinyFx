using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Nacos.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Net;

namespace TinyFx.Extensions.Nacos
{
    public static class NacosUtil
    {
        public static readonly NacosSection Section = new();
    }
}
