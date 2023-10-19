using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TinyFx.Security;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Math;
using System.Security.Permissions;
using System.Diagnostics;
using TinyFx.Randoms;
using TinyFx.Configuration;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using TinyFx.Collections;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Org.BouncyCastle.Crypto.Paddings;
using TinyFx.Net;
using System.Net;
using Microsoft.CodeAnalysis;
using TinyFx.Data.MySql;
using TinyFx.Data;
using Renci.SshNet;
using Renci.SshNet.Security.Cryptography.Ciphers;
using Renci.SshNet.Security.Cryptography;
using Renci.SshNet.Security;
using System.Reflection;
using TinyFx.Logging;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using TinyFx.Extensions.StackExchangeRedis;
using Serilog;
using TinyFx.Demos.demo;
using TinyFx.Common.Nacos;
using EasyNetQ;
using TinyFx.Text;
using Newtonsoft.Json.Linq;
using Nacos.V2;
using Grpc.Core;
using TinyFx.Demos.Patterns.Behavioral;
using TinyFx.Extensions.RabbitMQ;
using EasyNetQ.Topology;
using Renci.SshNet.Messages;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var str = "{\"id\":\"122125719806031296\",\"name\":\"\\u9ad8\\u6d2a\\u9501\",\"picture\":{\"data\":{\"height\":180,\"is_silhouette\":true,\"url\":\"https:\\/\\/scontent-sea1-1.xx.fbcdn.net\\/v\\/t1.30497-1\\/84628273_176159830277856_972693363922829312_n.jpg?stp=dst-jpg_s480x480&_nc_cat=1&ccb=1-7&_nc_sid=810bd0&_nc_ohc=PMtp3IjkGpMAX_Crqc6&_nc_ht=scontent-sea1-1.xx&edm=AP4hL3IEAAAA&oh=00_AfA-Qi7Iu03_LuesYBJuJD3Bv3x9I-KoB4QNXoz6Ct8CMQ&oe=65581099\",\"width\":180}}}";
            var json = SerializerUtil.DeserializeJsonNet<SuccessRsp>(str);
            //var json = SerializerUtil.DeserializeJson<SuccessRsp>(str);
            //var pic = SerializerUtil.DeserializeJson<PictureData>(Convert.ToString(json.picture.data));

        }
    }
    class SuccessRsp
    {
        public string id { get; set; }
        public string name { get; set; }
        public Picture picture { get; set; }
        public class Picture
        {
            public PictureData data { get; set; }
        }
        public class PictureData
        {
            public int height { get; set; }
            public int width { get; set; }
            public bool is_silhouette { get; set; }
            public string url { get; set; }
        }
    }
}
