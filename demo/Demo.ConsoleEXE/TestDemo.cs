using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.ElasticLoadBalancingV2;
using Amazon.ElasticLoadBalancingV2.Model;
using AutoMapper;
using Demo.ConsoleEXE;
using Demo.ConsoleEXE.DAL;
using Demo.Shared;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using ProtoBuf.Grpc.Client;
using System.Globalization;
using System.Text.RegularExpressions;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Common;
using TinyFx.Configuration;
using TinyFx.Data;
using TinyFx.DbCaching;
using TinyFx.Extensions.AutoMapper;
using TinyFx.Extensions.AWS;
using TinyFx.Extensions.AWS.LoadBalancing;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.IP2Country;
using TinyFx.Randoms;
using TinyFx.ShortId;
using TinyFx.Text;

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var no = DateTime.Now;
            var no1 = DateTime.UtcNow;
            var a = no.ToTimestamp(true, true);
            var utc = DateTimeUtil.ParseTimestamp(a);
            Console.WriteLine(utc.ToFormatString());
            Console.WriteLine(no1.ToFormatString());
            var local = DateTimeUtil.UtcToCNTime(utc);
            Console.WriteLine(no.ToFormatString());
            Console.WriteLine(local.ToFormatString());
            Console.WriteLine(a);
            var a1 = no.ToTimestamp(true,false);
            Console.WriteLine(a1);
            //var client = new LoadBalancingService();
            //var result = await client.GetTargetGroup("my-alb2-grp");

            //var client = DIUtil.GetService<IAmazonEC2>();
            //var rsp = await client.DescribeVpcsAsync();

            //var url = "http://localhost:5000";
            //using var channel = GrpcChannel.ForAddress(url);
            //var client = channel.CreateGrpcService<IGreeterService>();

            //var reply = await client.SayHelloAsync(
            //    new HelloRequest { Name = "GreeterClient" });

            //Console.WriteLine($"Greeting: {reply.Message}");
            Console.WriteLine("OK");

            //var dir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //Console.WriteLine(dir);
        }
    }

    public class UserInfo
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
    }
}
