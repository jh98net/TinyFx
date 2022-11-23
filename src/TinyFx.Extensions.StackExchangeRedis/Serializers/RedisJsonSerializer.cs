using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TinyFx.Extensions.StackExchangeRedis.Serializers
{
    /// <summary>
    /// JSON序列化器
    /// </summary>
    public class RedisJsonSerializer : ISerializer
    {
        private static readonly Encoding _encoding = Encoding.UTF8;
        private static JsonSerializerOptions _jsonOptions;

        public RedisJsonSerializer(JsonSerializerOptions options = null)
        {
            _jsonOptions = _jsonOptions ?? SerializerUtil.DefaultJsonOptions;
        }


        public byte[] Serialize(object item)
        {
            return JsonSerializer.SerializeToUtf8Bytes(item, item.GetType(), _jsonOptions);
            //var json = JsonSerializer.Serialize(item, item.GetType(), _jsonOptions);
            //return _encoding .GetBytes(json);
        }

        public Task<byte[]> SerializeAsync(object item)
        {
            return Task.FromResult(Serialize(item));
        }

        public object Deserialize(byte[] serializedObject,Type returnType)
        {
            var reader = new Utf8JsonReader(serializedObject);
            return JsonSerializer.Deserialize(ref reader, returnType, _jsonOptions);
            //var json = _encoding.GetString(serializedObject);
            //return JsonSerializer.Deserialize(json, typeof(object), _jsonOptions);
        }

        public T Deserialize<T>(byte[] serializedObject)
            => (T)Deserialize(serializedObject, typeof(T));

        public Task<object> DeserializeAsync(byte[] serializedObject, Type returnType)
        {
            return Task.FromResult(Deserialize(serializedObject, returnType));
        }

        public Task<T> DeserializeAsync<T>(byte[] serializedObject)
        {
            return Task.FromResult(Deserialize<T>(serializedObject));
        }

    }
}
