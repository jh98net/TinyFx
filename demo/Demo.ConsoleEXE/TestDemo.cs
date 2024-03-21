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
            string input = "hello^world!@123";
            string pattern = "[^a-zA-Z0-9]";
            string replacement = "-";
            string result = Regex.Replace(input, pattern, replacement);

            Console.WriteLine(result);  // 输出：hello_world_123

            //var client = new LoadBalancingService();
            //var result = await client.GetTargetGroup("my-alb2-grp");

            //var client = DIUtil.GetService<IAmazonEC2>();
            //var rsp = await client.DescribeVpcsAsync();

            Console.WriteLine("OK");
            //var url = "http://localhost:5000";
            //using var channel = GrpcChannel.ForAddress(url);
            //var client = channel.CreateGrpcService<IGreeterService>();

            //var reply = await client.SayHelloAsync(
            //    new HelloRequest { Name = "GreeterClient" });

            //Console.WriteLine($"Greeting: {reply.Message}");

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
