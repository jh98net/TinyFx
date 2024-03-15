using Demo.ConsoleEXE;
using Demo.ConsoleEXE.DAL;
using Demo.Shared;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System.Globalization;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Common;
using TinyFx.Data;
using TinyFx.DbCaching;
using TinyFx.Extensions.AutoMapper;
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
            var url = "http://localhost:5000";
            using var channel = GrpcChannel.ForAddress(url);
            var client = channel.CreateGrpcService<IGreeterService>();

            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "GreeterClient" });

            Console.WriteLine($"Greeting: {reply.Message}");

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
