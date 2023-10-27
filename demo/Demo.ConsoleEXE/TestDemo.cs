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
using TinyFx.Extensions.IP2Country;
using Google.Protobuf.WellKnownTypes;
using TinyFx.Data.SqlSugar;
using TinyFx.Reflection;
using Demo.ConsoleEXE;
using Demo.ConsoleEXE.DAL;
using SqlSugar;
using TinyFx.Demos.Redis;
using TinyFx.DbCaching;

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var a = GetAppByProviderAppId("pgsoft", "126");
            //var stopwatch = new Stopwatch();
            //var appList = await DbUtil.CreateRepository<Ss_appEO>().GetListAsync();
            //var operList = await DbUtil.CreateRepository<Ss_operator_appEO>().GetListAsync();
            //foreach (var app in appList)
            //{
            //    var i = 0;
            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    var sAppEo = DbCacheUtil.GetApp(app.AppID);
            //    var provider = DbCacheUtil.GetProvider(sAppEo.ProviderID);
            //    foreach (var oper in operList)
            //    {
            //        var item = DbCacheUtil.GetOperatorApp(oper.OperatorID, app.AppID);
            //        if (item == null)
            //            i++;
            //    }
            //    Console.WriteLine($"{stopwatch.ElapsedMilliseconds} count:{i}");
            //    stopwatch.Stop();
            //}
        }
        public static Ss_appEO GetAppByProviderAppId(string providerId, string providerAppId)
        {
            var ret = DbCachingUtil.GetSingle<Ss_appEO>(it => new { it.ProviderID, it.ProviderAppId }, new Ss_appEO
            {
                ProviderID = providerId,
                ProviderAppId = providerAppId
            });
            if (ret == null)
                throw new Exception($"AppId不存在: providerId:{providerId} providerAppId:{providerAppId}");
            return ret;
        }
    }
}
