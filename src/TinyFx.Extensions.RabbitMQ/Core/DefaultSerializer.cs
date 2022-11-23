using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.RabbitMQ
{
    internal class DefaultSerializer : EasyNetQ.ISerializer
    {
        public IMemoryOwner<byte> MessageToBytes(Type messageType, object message)
        {
            var json = SerializerUtil.SerializeJson(message);
            var data = Encoding.UTF8.GetBytes(json);
            using var ret = MemoryPool<byte>.Shared.Rent(data.Length);
            data.CopyTo(ret.Memory);
            return ret;
        }

        public object BytesToMessage(Type messageType, in ReadOnlyMemory<byte> bytes)
        {
            var json = Encoding.UTF8.GetString(bytes.Span);
            return TinyFx.SerializerUtil.DeserializeJson(json, messageType);
        }
    }
}
