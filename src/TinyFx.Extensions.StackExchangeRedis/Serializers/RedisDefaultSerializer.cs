using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.StackExchangeRedis.Serializers
{
    public abstract class RedisDefaultSerializer: ISerializer
    {
        public abstract byte[] Serialize(object item);
        public abstract object Deserialize(byte[] serializedObject, Type returnType);
        
        public byte[] Serialize(object item, out bool isSuccess)
        {
            isSuccess = true;
            var type = item.GetType();
            if (type.IsClass)
                return Encoding.UTF8.GetBytes(SerializerUtil.SerializeJson(item));
            switch (type.FullName)
            {
                case SimpleTypeNames.Boolean:
                    return BitConverter.GetBytes((bool)item);
                case SimpleTypeNames.Char:
                    return BitConverter.GetBytes((char)item);
                case SimpleTypeNames.Double:
                    return BitConverter.GetBytes((double)item);
                case SimpleTypeNames.Int16:
                    return BitConverter.GetBytes((short)item);
                case SimpleTypeNames.Int32:
                    return BitConverter.GetBytes((int)item);
                case SimpleTypeNames.Int64:
                    return BitConverter.GetBytes((long)item);
                case SimpleTypeNames.Single:
                    return BitConverter.GetBytes((float)item);
                case SimpleTypeNames.UInt16:
                    return BitConverter.GetBytes((ushort)item);
                case SimpleTypeNames.UInt32:
                    return BitConverter.GetBytes((uint)item);
                case SimpleTypeNames.UInt64:
                    return BitConverter.GetBytes((ulong)item);
                case SimpleTypeNames.String:
                    return Encoding.UTF8.GetBytes((string)item);
                case SimpleTypeNames.Guid:
                    return Encoding.UTF8.GetBytes(((Guid)item).ToString());
                case SimpleTypeNames.Bytes:
                    return (byte[])item;
            }
            isSuccess = false;
            return null;
        }
        public Task<byte[]> SerializeAsync(object item)
            => Task.Run(() => Serialize(item));

        public object Deserialize(byte[] serializedObject, Type returnType, out bool isSuccess)
        {
            isSuccess = true;
            if (returnType.IsClass)
                return SerializerUtil.DeserializeJson(Encoding.UTF8.GetString(serializedObject), returnType);
            switch (returnType.FullName)
            {
                case SimpleTypeNames.Boolean:
                    return BitConverter.ToBoolean(serializedObject, 0);
                case SimpleTypeNames.Char:
                    return BitConverter.ToChar(serializedObject, 0);
                case SimpleTypeNames.Double:
                    return BitConverter.ToDouble(serializedObject, 0);
                case SimpleTypeNames.Int16:
                    return BitConverter.ToInt16(serializedObject, 0);
                case SimpleTypeNames.Int32:
                    return BitConverter.ToInt32(serializedObject, 0);
                case SimpleTypeNames.Int64:
                    return BitConverter.ToInt64(serializedObject, 0);
                case SimpleTypeNames.Single:
                    return BitConverter.ToSingle(serializedObject, 0);
                case SimpleTypeNames.UInt16:
                    return BitConverter.ToUInt16(serializedObject, 0);
                case SimpleTypeNames.UInt32:
                    return BitConverter.ToUInt32(serializedObject, 0);
                case SimpleTypeNames.UInt64:
                    return BitConverter.ToUInt64(serializedObject, 0);
                case SimpleTypeNames.String:
                    return Encoding.UTF8.GetString(serializedObject);
                case SimpleTypeNames.Guid:
                    return new Guid(Encoding.UTF8.GetString(serializedObject));
                case SimpleTypeNames.Bytes:
                    return serializedObject;
            }
            isSuccess = false;
            return null;
        }

        public T Deserialize<T>(byte[] serializedObject)
            => (T)Deserialize(serializedObject, typeof(T));

        public Task<object> DeserializeAsync(byte[] serializedObject, Type returnType)
            => Task.Run(() => Deserialize(serializedObject, returnType));

        public Task<T> DeserializeAsync<T>(byte[] serializedObject)
            => Task.Run(() => Deserialize<T>(serializedObject));

    }
}
