using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Text;

namespace TinyFx.Serialization
{
    public class TinyJsonSerializer : SerializerBase
    {
        public JsonSerializerSettings JsonOptions;
        public TinyJsonSerializer()
        {
            JsonOptions = SerializerUtil.ConfigJsonNetSettings(new JsonSerializerSettings());
        }
        public override byte[] Serialize(object value)
        {
            //return JsonSerializer.SerializeToUtf8Bytes(value, value.GetType(), _options);
            //return Encoding.UTF8.GetBytes(SerializerUtil.SerializeJson(value, JsonOptions));
            return Encoding.UTF8.GetBytes(SerializerUtil.SerializeJsonNet(value, JsonOptions));
            //return Encoding.UTF8.GetBytes(SerializerUtil.SerializeJsonNet(value));
        }

        public override object Deserialize(byte[] utf8Bytes, Type returnType)
        {
            //var reader = new Utf8JsonReader(utf8Bytes);
            //return JsonSerializer.Deserialize(ref reader, returnType, _options);
            return SerializerUtil.DeserializeJsonNet(Encoding.UTF8.GetString(utf8Bytes), returnType, JsonOptions);
            //return SerializerUtil.DeserializeJson(Encoding.UTF8.GetString(utf8Bytes), returnType, JsonOptions);
            //return SerializerUtil.DeserializeJsonNet(utf8Bytes, returnType);

        }
    }
}
