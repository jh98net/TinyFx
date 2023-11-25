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
            var aaa = NetUtil.GetIpMode("127.0.0.1");
            //var provider = new WeightRandomProvider<A>();
            //provider.AddItem(1, new A { Id = 1 });
            //provider.AddItem(2, new A { Id = 2 });
            //provider.AddItem(3, new A { Id = 3 });
            //var result = new int[] { 0, 0, 0 };
            //for (int i = 0; i < 10000; i++)
            //{
            //    var item = provider.Next();
            //    result[item.Id - 1] = result[item.Id - 1] + 1;
            //}
            //foreach (var item in result)
            //{
            //    Console.WriteLine((decimal)item/ 10000);
            //}
        }
    }
    class A
    {
        public int Id { get; set; }
        public int Weight { get; set; }
    }
}
