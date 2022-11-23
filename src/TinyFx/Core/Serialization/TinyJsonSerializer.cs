using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TinyFx
{
    public class TinyJsonSerializer : ISerializer
    {
        private static JsonSerializerOptions Options = SerializerUtil.DefaultJsonOptions;

        public byte[] Serialize<T>(T item)
            => JsonSerializer.SerializeToUtf8Bytes<T>(item, Options);

        public Task<byte[]> SerializeAsync<T>(T item)
            => Task.FromResult(JsonSerializer.SerializeToUtf8Bytes(item, Options));

        public byte[] Serialize(object value, Type inputType)
            => JsonSerializer.SerializeToUtf8Bytes(value, inputType, Options);

        public Task<byte[]> SerializeAsync(object value, Type inputType)
            => Task.FromResult(Serialize(value, inputType));


        public T Deserialize<T>(byte[] utf8Bytes)
            => JsonSerializer.Deserialize<T>(utf8Bytes, Options);

        public Task<T> DeserializeAsync<T>(byte[] utf8Bytes)
        {
            using (var stream = new MemoryStream(utf8Bytes))
            {
                return JsonSerializer.DeserializeAsync<T>(stream, Options).AsTask();
            }
        }

        public object Deserialize(byte[] utf8Bytes, Type returnType)
            => JsonSerializer.Deserialize(utf8Bytes, returnType, Options);
        public Task<object> DeserializeAsync(byte[] utf8Bytes, Type returnType)
        {
            using (var stream = new MemoryStream(utf8Bytes))
            {
                return JsonSerializer.DeserializeAsync(stream, returnType, Options).AsTask();
            }
        }
    }
}
