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

namespace TinyFx.Demos
{
    public static class AAA
    {
        public static void Parse(Expression<Func<V_demo_user_courseEO>> expr)
        {
            var visitor = new MyExprVisitor();
            visitor.Visit(expr);
            var v = visitor.GetKeys();
            Console.WriteLine(v.DictKey);
            Console.WriteLine(v.ValueKey);
        }
    }


    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var abc = "bbbb";
            AAA.Parse(() => new V_demo_user_courseEO
            {
                UserID = 1,
                ClassID = "aaaaaa"
            });
            AAA.Parse(() => new V_demo_user_courseEO
            {
                UserID = 2,
                ClassID = abc
            });
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
            if (ReflectionUtil.IsSimpleType(node.Type))
                ValueKeys.Add(Convert.ToString(node.Value));
            return base.VisitConstant(node);
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            var value = GetMemberValue(node);
            ValueKeys.Add(Convert.ToString(value));
            return base.VisitMember(node);
        }
        private static object GetMemberValue(MemberExpression expression)
        {
            if (expression == null)
                return null;
            var field = expression.Member as FieldInfo;
            if (field != null)
            {
                var constValue = GetConstantValue(expression.Expression);
                return field.GetValue(constValue);
            }
            var property = expression.Member as PropertyInfo;
            if (property == null)
                return null;
            var value = GetMemberValue(expression.Expression as MemberExpression);
            return property.GetValue(value);
        }

        private static object GetConstantValue(Expression expression)
        {
            var constantExpression = expression as ConstantExpression;
            if (constantExpression == null)
                return null;
            return constantExpression.Value;
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
