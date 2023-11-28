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
using TinyFx.Extensions.IP2Country;
using Google.Protobuf.WellKnownTypes;
using TinyFx.Data.SqlSugar;
using TinyFx.Reflection;
using Demo.ConsoleEXE;
using Demo.ConsoleEXE.DAL;
using SqlSugar;
using TinyFx.Demos.Redis;
using TinyFx.DbCaching;
using MySql.Data.MySqlClient;

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var client = HttpClientExFactory.CreateClientEx();
            var ip = await HttpClientExFactory.CreateClientEx().CreateAgent().AddUrl("http://api.ip.sb/ip").GetStringAsync();
            //Console.WriteLine(rsp.ResultString);
            var uo = GetIPFromHtml(rsp.ResultString);
            Console.WriteLine(uo);
        }
        public static string GetIPFromHtml(String pageHtml)
        {
            //验证ipv4地址
            string reg = @"(?:(?:(25[0-5])|(2[0-4]\d)|((1\d{2})|([1-9]?\d)))\.){3}(?:(25[0-5])|(2[0-4]\d)|((1\d{2})|([1-9]?\d)))";
            string ip = "";
            Match m = Regex.Match(pageHtml, reg);
            if (m.Success)
            {
                ip = m.Value;
            }
            return ip;
        }
    }
    class A
    {
        public int Id { get; set; }
        public int Weight { get; set; }
    }
}
