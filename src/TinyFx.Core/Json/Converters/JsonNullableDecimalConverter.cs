﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TinyFx.Core.Json;

public class JsonNullableDecimalConverter : JsonConverter<decimal?>
{
    public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        decimal result;
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                if (reader.TryGetDecimal(out result))
                    return result;
                break;
            case JsonTokenType.String:
                var fromString = reader.GetString();
                if (string.IsNullOrWhiteSpace(fromString))
                    return null;
                if (decimal.TryParse(fromString, out result))
                    return result;
                break;
            case JsonTokenType.Null:
                return null;
        }
        return null;
    }
    public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteNumberValue(value.Value);
        else writer.WriteNullValue();
    }
}