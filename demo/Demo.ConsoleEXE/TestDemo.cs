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

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            Expression<Func<V_demo_user_courseEO>> expr = () => new V_demo_user_courseEO
            {
                ClassID="a",
                UserID=1
            };
            var visitor = new MyExprVisitor();
            visitor.Visit(expr);
            var a = visitor.GetKeys();
        }
    }
    public class MyExprVisitor : ExpressionVisitor
    {
        private List<string> DictKeys = new();
        private List<string> ValueKeys = new();

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            DictKeys.Add(node.Member.Name);
            return base.VisitMemberAssignment(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            ValueKeys.Add(Convert.ToString(node.Value));
            return base.VisitConstant(node);
        }
        public (string DictKey, string ValueKey) GetKeys()
        {
            return (string.Join('|', DictKeys), string.Join('|', ValueKeys));
        }
    }

    public class A
    {
        public A()
        {
            Name = "bas";
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
