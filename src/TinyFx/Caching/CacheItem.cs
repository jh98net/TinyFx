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
        private static long MaxTime = DateTime.MaxValue.DateTimeToTimestamp();
        public T Value { get; set; }
        public long ExpireTime { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsExpired => (DateTime.Now.DateTimeToTimestamp() - ExpireTime) > 0;
        public TimeSpan ExpireSpan { get; private set; }
        public CacheItem(T value)
        {
            Value = value;
            ExpireTime = MaxTime;
        }
        public CacheItem(T value, DateTime expireAt)
        {
            Value = value;
            SetExpire(expireAt);
        }
        public CacheItem(T value, TimeSpan expireSpan)
        {
            Value = value;
            SetExpire(expireSpan);
        }
        public void SetExpire(DateTime expireAt)
        {
            ExpireSpan = (expireAt - DateTime.Now);
            ExpireTime = expireAt.DateTimeToTimestamp();
        }
        public void SetExpire(TimeSpan expireSpan)
        {
            ExpireSpan = expireSpan;
            ExpireTime = DateTime.Now.AddMilliseconds(expireSpan.TotalMilliseconds)
                .DateTimeToTimestamp();
        }
    }

}
