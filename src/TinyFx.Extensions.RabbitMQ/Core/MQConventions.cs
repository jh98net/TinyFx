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
                var idx = subId.LastIndexOf('.');
                if (idx >= 0)
                    subId = subId.Substring(idx + 1);
                return $"[{msgType.Name}]=>[{projectId}]{subId}";
            };
        }
    }
}
