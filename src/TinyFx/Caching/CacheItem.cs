using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Caching
{
    /// <summary>
    /// 保存在缓存中的项（支持过期）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CacheItem<T>
    {
        public T Value { get; set; }
        public long? ExpireTime { get; set; }

        public CacheItem()
        {
        }

        public CacheItem(T value)
        {
            Value = value;
        }
        public CacheItem(T value, DateTime? expireAt)
        {
            Value = value;
            SetExpire(expireAt);
        }
        public CacheItem(T value, TimeSpan? expireSpan)
        {
            Value = value;
            SetExpire(expireSpan);
        }


        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public bool IsExpired => ExpireTime.HasValue
            ? (DateTime.UtcNow.DateTimeToTimestamp() - ExpireTime) > 0
            : false;
        public void SetExpire(DateTime? expireAt)
        {
            if (expireAt.HasValue)
                ExpireTime = expireAt.Value.DateTimeToTimestamp();
        }
        public void SetExpire(TimeSpan? expireSpan)
        {
            if (expireSpan.HasValue)
                ExpireTime = DateTime.UtcNow.Add(expireSpan.Value).DateTimeToTimestamp();
        }
        public TimeSpan? GetExpireSpan()
        {
            if (!ExpireTime.HasValue)
                return null;
            return ExpireTime.Value.TimestampToDateTime() - DateTime.UtcNow;
        }
        public DateTime? GetExpireUtcTime()
        {
            if (!ExpireTime.HasValue)
                return null;
            return ExpireTime.Value.TimestampToDateTime(false);
        }
        public DateTime? GetExpireTime()
        {
            if (!ExpireTime.HasValue)
                return null;
            return ExpireTime.Value.TimestampToDateTime(true);
        }
    }
}
