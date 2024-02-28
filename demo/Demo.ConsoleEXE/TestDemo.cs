﻿using Demo.ConsoleEXE;
using Demo.ConsoleEXE.DAL;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Common;
using TinyFx.Data;
using TinyFx.DbCaching;
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
            var ret = await DbCachingUtil.PublishCheck();
            Console.WriteLine("OK");
        }
    }

    public class UserInfo
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
    }
}
