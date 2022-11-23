using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Collections;

namespace TinyFx
{
    internal class JsonExceptionConverter : JsonConverter<Exception>
    {
        public override Exception Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var ret = new CustomException("json反序列化Exception");
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    switch (propertyName)
                    {
                        case "Message":
                            ret.SetMessage(reader.GetString());
                            break;
                        case "StackTrace":
                            ret.SetStackTrace(reader.GetString());
                            break;
                        case "Data":
                            var datas = SerializerUtil.DeserializeJson<Dictionary<string, string>>(reader.GetString());
                            if (datas == null) continue;
                            foreach (var item in datas)
                                ret.Data.Add(item.Key, item.Value);
                            break;
                        case "Code":
                            ret.Code = reader.GetString();
                            break;
                        case "Result":
                            ret.Result = reader.GetString();
                            break;
                    }
                }
            }
            return ret;
        }

        public override void Write(Utf8JsonWriter writer, Exception value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Message", value.Message);
            writer.WriteString("StackTrace", value.StackTrace);
            writer.WriteString("Data", SerializerUtil.SerializeJson(value.Data));
            if (value is CustomException ex)
            {
                writer.WriteString("Code", ex.Code);
                writer.WriteString("Result", SerializerUtil.SerializeJson(ex.Result));
            }
            writer.WriteEndObject();
        }
    }
}
