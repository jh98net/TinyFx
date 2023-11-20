using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;

namespace TinyFx.Extensions.RabbitMQ
{
    internal class MQConventions : Conventions
    {
        public MQConventions(ITypeNameSerializer typeNameSerializer) : base(typeNameSerializer)
        {
            ExchangeNamingConvention = (msgType) =>
            {
                return msgType.Name;
            };
            QueueNamingConvention = (msgType, subId) =>
            {
                var projectId = ConfigUtil.Project.ProjectId;
                // 获取subId中.后面的名称
                var idx = subId.LastIndexOf('.');
                var subName = idx >= 0
                    ? subId.Substring(idx + 1)
                    : subId;
                // 除了Multicast外后面加hashCode防止重复
                if (!subName.Contains("-MC-"))
                    subName += $"-{subId.GetHashCode()}";
                return $"[{msgType.Name}]=>[{projectId}]{subName}";
            };
        }
    }
}
