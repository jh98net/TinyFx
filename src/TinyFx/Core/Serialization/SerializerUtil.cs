using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;
using TinyFx.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using TinyFx.Configuration;

namespace TinyFx
{
    /// <summary>
    /// 序列化辅助类,提供Xml和Json序列化
    /// </summary>
    public static class SerializerUtil
    {
        #region Xml Serializer

        /// <summary>
        /// Xml序列化到bytes
        /// </summary>
        /// <param name="type">序列化对象类型</param>
        /// <param name="source">序列化对象</param>
        /// <returns></returns>
        public static byte[] SerializeXmlToBytes(Type type, object source)
        {
            byte[] ret = null;
            XmlSerializer ser = new XmlSerializer(type);
            using (MemoryStream ms = new MemoryStream())
            {
                ser.Serialize(ms, source);
                ret = ms.ToArray();
            }
            return ret;
        }

        /// <summary>
        /// Xml序列化到bytes
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="source">序列化对象</param>
        /// <returns></returns>
        public static byte[] SerializeXmlToBytes<T>(T source)
            => SerializeXmlToBytes(typeof(T), source);

        /// <summary>
        /// Xml序列化到string
        /// </summary>
        /// <param name="type">序列化对象类型</param>
        /// <param name="source">序列化对象</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        public static string SerializeXml(Type type, object source, Encoding encoding = null)
            => (encoding ?? Encoding.UTF8).GetString(SerializeXmlToBytes(type, source));

        /// <summary>
        /// Xml序列化string
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="source">序列化对象</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        public static string SerializeXml<T>(T source, Encoding encoding = null)
            => (encoding ?? Encoding.UTF8).GetString(SerializeXmlToBytes(typeof(T), source));

        /// <summary>
        /// Xml反序列化从bytes
        /// </summary>
        /// <param name="type">反序列化对象类型</param>
        /// <param name="input">反序列化对象的bytes</param>
        /// <returns></returns>
        public static object DeserializeXmlFromBytes(Type type, byte[] input)
        {
            object ret = null;
            XmlSerializer ser = new XmlSerializer(type);
            using (MemoryStream ms = new MemoryStream(input))
            {
                ret = ser.Deserialize(ms);
            }
            return ret;
        }

        /// <summary>
        /// Xml反序列化从bytes
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="input">序列化对象</param>
        /// <returns></returns>
        public static T DeserializeXmlFromBytes<T>(byte[] input) where T : class
            => DeserializeXmlFromBytes(typeof(T), input) as T;

        /// <summary>
        /// Xml反序列化从string
        /// </summary>
        /// <param name="type">反序列化对象类型</param>
        /// <param name="input">可反序列化的字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        public static object DeserializeXml(Type type, string input, Encoding encoding = null)
            => DeserializeXmlFromBytes(type, (encoding ?? Encoding.UTF8).GetBytes(input));

        /// <summary>
        /// Xml反序列化从Stream
        /// </summary>
        /// <param name="type"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static object DeserializeXml(Type type, Stream input)
            => new XmlSerializer(type).Deserialize(input);

        /// <summary>
        /// Xml反序列化从Stream
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T DeserializeXml<T>(Stream input)
            => (T)DeserializeXml(typeof(T), input);

        /// <summary>
        /// Xml反序列化
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="input">可反序列化的字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        public static T DeserializeXml<T>(string input, Encoding encoding = null)
            => (T)DeserializeXmlFromBytes(typeof(T), (encoding ?? Encoding.UTF8).GetBytes(input));
        #endregion //Xml Serializer

        #region JSON Serializer 使用 System.Text.Json

        /// <summary>
        /// 设置System.Text.Json的Options
        /// </summary>
        /// <param name="options"></param>
        public static JsonSerializerOptions ConfigJsonSerializerOptions(JsonSerializerOptions options)
        {
            // 属性名称不区分大小写
            options.PropertyNameCaseInsensitive = true;
            // 属性名称CamelCase
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            // 数字可使用字符串
            //options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

            // 序列化所有语言集(如中文)而不进行转义! Content-Type: application/json; charset=utf-8
            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            // 排除只读属性
            //IgnoreReadOnlyProperties = true; 
            // 排除所有null属性
            //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            // 允许在JSON中添加注释
            //options.ReadCommentHandling = JsonCommentHandling.Skip;
            // 允许尾随逗号
            //options.AllowTrailingCommas = true;
            // 缩进
            options.WriteIndented = true;
            // 枚举转换成名称
            //options.Converters.Add(new JsonStringEnumConverter());
            // 支持Exception序列化
            options.Converters.Add(new JsonExceptionConverter());
            return options;
        }

        private static JsonSerializerOptions _jsonOptions;

        /// <summary>
        /// 提供与System.Text.Json.JsonSerializer一起使用的选项。
        /// </summary>
        public static JsonSerializerOptions DefaultJsonOptions
        {
            get
            {
                if (_jsonOptions == null)
                {
                    _jsonOptions = ConfigJsonSerializerOptions(new JsonSerializerOptions());
                }
                return _jsonOptions;
            }
        }

        /// <summary>
        /// 序列化JSON对象，对象需要 DataContractAttribute,DataMember,IgnoreDataMember
        /// </summary>
        /// <param name="source">对象</param>
        /// <returns></returns>
        public static string SerializeJson(object source)
        {
            return JsonSerializer.Serialize(source, DefaultJsonOptions);
        }

        public static void SerializeJsonFile(object source, string file, Encoding encoding = null)
            => File.WriteAllText(file, SerializeJson(source), encoding ?? Encoding.UTF8);

        /// <summary>
        /// 反序列化JSON对象
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <param name="type">JSON类型</param>
        /// <returns></returns>
        public static object DeserializeJson(string json, Type type)
            => JsonSerializer.Deserialize(json, type, DefaultJsonOptions);

        public static object DeserializeJsonFile(string file, Type type)
            => JsonSerializer.Deserialize(File.ReadAllText(file), type, DefaultJsonOptions);

        /// <summary>
        /// 反序列化JSON对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns></returns>
        public static T DeserializeJson<T>(string json)
            => JsonSerializer.Deserialize<T>(json, DefaultJsonOptions);

        public static T DeserializeJsonFile<T>(string file, Encoding encoding = null)
            => JsonSerializer.Deserialize<T>(File.ReadAllText(file, encoding ?? Encoding.UTF8), DefaultJsonOptions);

        /// <summary>
        /// 反序列化JSON对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static object DeserializeJson(Type type, Stream stream, Encoding encoding = null)
        {
            using (var reader = new StreamReader(stream, encoding))
            {
                var json = reader.ReadToEnd();
                return DeserializeJson(json, type);
            }
        }
        #endregion

        #region Json Newtonsoft.Json
        /// <summary>
        /// Newtonsoft.Json序列化
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string SerializeJsonNet(object src)
            => Newtonsoft.Json.JsonConvert.SerializeObject(src, Newtonsoft.Json.Formatting.Indented);

        /// <summary>
        /// Newtonsoft.Json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeJsonNet<T>(string json)
            => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        public static T DeserializeJsonNetFile<T>(string file)
            => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(File.ReadAllText(file));

        #endregion
    }
}
