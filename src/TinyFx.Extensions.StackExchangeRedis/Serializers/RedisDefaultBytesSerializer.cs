using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.StackExchangeRedis.Serializers
{
    /// <summary>
    /// 尽量序列化成bytes
    /// </summary>
    public class RedisDefaultBytesSerializer : RedisDefaultSerializer
    {
        public override byte[] Serialize(object item)
        {
            var ret = Serialize(item, out bool isSuccess);
            if (isSuccess) return ret;

            var type = item.GetType();
            switch (type.FullName)
            {
                case SimpleTypeNames.Decimal:
                    return TinyFxUtil.DecimalToByteArray((decimal)item);
                case SimpleTypeNames.DateTime:
                    return BitConverter.GetBytes(TinyFxUtil.DateTimeToTimestamp((DateTime)item));
                case SimpleTypeNames.TimeSpan:
                    return BitConverter.GetBytes(((TimeSpan)item).Ticks);
                case SimpleTypeNames.DateTimeOffset:
                    return BitConverter.GetBytes(TinyFxUtil.DateTimeOffsetToTimestamp((DateTimeOffset)item));

            }
            throw new Exception($"不支持此类型RedisNoneSerializer: {type.FullName}");
        }

        public override object Deserialize(byte[] serializedObject, Type returnType)
        {
            var ret = base.Deserialize(serializedObject, returnType, out bool isSuccess);
            if (isSuccess) return ret;

            switch (returnType.FullName)
            {
                case SimpleTypeNames.Decimal:
                    return TinyFxUtil.ByteArrayToDecimal(serializedObject);
                case SimpleTypeNames.DateTime:
                    return TinyFxUtil.TimestampToDateTime(BitConverter.ToInt64(serializedObject, 0));
                case SimpleTypeNames.DateTimeOffset:
                    return TinyFxUtil.TimestampToDateTimeOffset(BitConverter.ToInt64(serializedObject, 0));
                case SimpleTypeNames.TimeSpan:
                    return new TimeSpan(BitConverter.ToInt64(serializedObject, 0));
            }
            throw new Exception($"不支持此类型反序列化RedisNoneSerializer: {returnType.FullName}");
        }
    }
}
