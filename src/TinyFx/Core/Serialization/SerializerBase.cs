using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Serialization
{
    public abstract class SerializerBase : ISerializer
    {
        public abstract byte[] Serialize(object value);
        public virtual Task<byte[]> SerializeAsync(object value)
        {
            return Task.Run(() => { 
                return Serialize(value);
            });
        }

        public abstract object Deserialize(byte[] utf8Bytes, Type returnType);
        public Task<object> DeserializeAsync(byte[] utf8Bytes, Type returnType)
        {
            return Task.Run(() => {
                return Deserialize(utf8Bytes, returnType);
            });
        }

        public T Deserialize<T>(byte[] utf8Bytes)
        {
            return (T)Deserialize(utf8Bytes, typeof(T));
        }

        public Task<T> DeserializeAsync<T>(byte[] utf8Bytes)
        {
            return Task.Run(() => {
                return Deserialize<T>(utf8Bytes);
            });
        }
    }
}
