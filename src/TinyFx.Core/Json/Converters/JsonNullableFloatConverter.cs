﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TinyFx.Core.Json;

public class JsonNullableFloatConverter : JsonConverter<float?>
{
    public override float? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        float result;
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                if (reader.TryGetSingle(out result))
                    return result;
                break;
            case JsonTokenType.String:
                var fromString = reader.GetString();
                if (string.IsNullOrWhiteSpace(fromString))
                    return null;
                if (float.TryParse(fromString, out result))
                    return result;
                break;
            case JsonTokenType.Null: return null;
        }
        return null;
    }
    public override void Write(Utf8JsonWriter writer, float? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteNumberValue(value.Value);
        else writer.WriteNullValue();
    }
}