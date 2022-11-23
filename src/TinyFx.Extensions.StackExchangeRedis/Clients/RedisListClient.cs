using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Redis;
using TinyFx.Caching;

namespace TinyFx.Extensions.StackExchangeRedis
{
    /// <summary>
    /// Redis List双向链表结构（左右两边操作）,可以被继承，也可以直接构建
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RedisListClient<T> : RedisClientBase
    {
        public override RedisType RedisType => RedisType.List;

        #region Constructors
        /// <summary>
        /// 继承调用
        /// </summary>
        public RedisListClient(RedisClientOptions options = null) : base(options) { }
        #endregion

        #region LoadAllValuesWhenRedisNotExists
        public delegate IEnumerable<T> LoadAllValuesDelegate();
        public LoadAllValuesDelegate LoadAllValuesHandler;
        /// <summary>
        /// 调用GetAllOrLoad()时，当RedisKey不存在，则调用此方法返回并存储全部list值到redis中，需要时子类实现override
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<T> LoadAllValuesWhenRedisNotExists()
        {
            if (LoadAllValuesHandler != null)
                return LoadAllValuesHandler();
            throw new NotImplementedException();
        }
        #endregion

        #region LeftPop & LeftPush & RightPop & RightPush & RightPopLeftPush
        /// <summary>
        /// 删除并返回存储在key处的列表的第一个元素
        /// </summary>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public T LeftPop(CommandFlags commandFlags = CommandFlags.None)
        {
            var redisValue = Database.ListLeftPop(RedisKey, commandFlags);
            return Deserialize<T>(redisValue);
        }
        /// <summary>
        /// 删除并返回列表的多个元素
        /// </summary>
        /// <param name="start">0代表第一个元素</param>
        /// <param name="stop">-1代表最后一个元素，-2倒数第二个</param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public List<T> LeftPopList(long start = 0, long stop = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            var values = Database.ListRange(RedisKey, 0, -1, commandFlags);
            return values.Select(value => Deserialize<T>(value)).ToList();
        }
        /// <summary>
        /// 将所有指定的值插入存储在key的列表的开头(左边第一个）。 如果键不存在，则在执行推入操作之前将其创建为空列表。 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long LeftPush(T value, When when = When.Always, CommandFlags flags = CommandFlags.None)
            => Database.ListLeftPush(RedisKey, Serialize(value), when, flags);

        /// <summary>
        /// 将所有指定的值插入存储在key的列表的开头(左边第一个）。 如果键不存在，则在执行推入操作之前将其创建为空列表。 
        /// 元素从最左边的元素到最右边的元素一个接一个地插入列表的开头。 
        /// 因此，例如，命令LPUSH mylist a b c将导致一个包含c作为第一元素，b作为第二元素和a作为第三元素的列表
        /// </summary>
        /// <param name="values"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long LeftPushList(IEnumerable<T> values, CommandFlags flags = CommandFlags.None)
            => Database.ListLeftPush(RedisKey, values.Select(x => Serialize(x)).ToArray(), flags);

        /// <summary>
        /// 删除并返回键处存储的列表的右边最后一个元素
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T RightPop(CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.ListRightPop(RedisKey, flags);
            return Deserialize<T>(redisValue);
        }
        /// <summary>
        /// 将指定的值插入存储在key的列表的末尾。 如果键不存在，则在执行推入操作之前将其创建为空列表
        /// </summary>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long RightPush(T value, When when = When.Always, CommandFlags flags = CommandFlags.None)
            => Database.ListRightPush(RedisKey, Serialize(value), when, flags);
        /// <summary>
        /// 将所有指定的值插入存储在key的列表的末尾。 如果键不存在，则在执行推入操作之前将其创建为空列表。 
        /// 元素从最左边的元素到最右边的元素一个接一个插入到列表的末尾。 
        /// 因此，例如命令RPUSH mylist a b c将导致一个列表，其中包含a作为第一元素，b作为第二元素，c作为第三元素。
        /// </summary>
        /// <param name="values"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long RightPushList(IEnumerable<T> values, CommandFlags flags = CommandFlags.None)
            => Database.ListRightPush(RedisKey, values.Select(x => Serialize(x)).ToArray(), flags);
        /// <summary>
        /// 以原子方式返回并删除源中存储的列表的最后一个元素（尾部），并将该元素推入存储在目标位置的列表的第一个元素（头）
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T RightPopLeftPush(string destination, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.ListRightPopLeftPush(RedisKey, destination, flags);
            return Deserialize<T>(redisValue);
        }
        #endregion

        #region SetByIndex & InsertAfter & InsertBefore
        /// <summary>
        /// 将索引处的列表元素设置为value。 有关index参数的更多信息，请参见List GetByIndex。 超出范围的索引将返回错误
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        public void SetByIndex(long index, T value, CommandFlags flags = CommandFlags.None)
            => Database.ListSetByIndex(RedisKey, index, Serialize(value), flags);
        /// <summary>
        /// 在指定列表缓存项后插入缓存项，如果键不存在，则将其视为空列表，并且不执行任何操作。
        /// </summary>
        /// <param name="pivot"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long InsertAfter(T pivot, T value, CommandFlags flags = CommandFlags.None)
            => Database.ListInsertAfter(RedisKey, Serialize(pivot), Serialize(value), flags);
        /// <summary>
        /// 在指定列表缓存项前插入缓存项，如果键不存在，则将其视为空列表，并且不执行任何操作。
        /// </summary>
        /// <param name="pivot"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long InsertBefore(T pivot, T value, CommandFlags flags = CommandFlags.None)
            => Database.ListInsertBefore(RedisKey, Serialize(pivot), Serialize(value), flags);
        #endregion

        #region GetByIndex & GetByIndexOrDefault & GetByIndexOrException & GetAll & GetAllOrLoad & Range
        /// <summary>
        /// 返回键存储在列表中索引index处的元素。 
        /// </summary>
        /// <param name="index">索引从零开始，0表示第一个元素，1表示第二个元素。 
        /// 负索引可用于指定从列表末尾开始的元素。 -1表示最后一个元素，-2表示倒数第二个。</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T GetByIndex(long index, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.ListGetByIndex(RedisKey, index, flags);
            return Deserialize<T>(redisValue);
        }
        public T GetByIndexOrDefault(long index, T defaultValue, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.ListGetByIndex(RedisKey, index, flags);
            return TryDeserialize(redisValue, out T ret) ? ret : defaultValue;
        }
        public T GetByIndexOrException(long index, CommandFlags flags = CommandFlags.None)
        {
            var redisValue = Database.ListGetByIndex(RedisKey, index, flags);
            if (!TryDeserialize(redisValue, out T ret))
                throw new CacheNotFound($"[Redis List]index不存在。Key:{RedisKey}, Index:{index} type:{GetType().FullName}");
            return ret;
        }
        /// <summary>
        /// 获取全部list缓存值
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(CommandFlags flags = CommandFlags.None)
            => Range(0, -1, flags);
        /// <summary>
        /// 获取所有list缓存值，如果RedisKey不存在，则调用LoadAllValuesWhenRedisNotExists()返回并保存到redis中
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAllOrLoad(CommandFlags flags = CommandFlags.None)
        {
            IEnumerable<T> ret = default;
            if (!KeyExists(flags))
            {
                ret = LoadAllValuesWhenRedisNotExists();
                RightPushList(ret, flags);
            }
            else
            {
                ret = Range(0, -1, flags);
            }
            return ret;
        }

        /// <summary>
        /// 返回存储在key处的列表的指定元素。 
        /// 偏移量start和stop是从零开始的索引，0是列表的第一个元素（列表的开头），1是下一个元素，依此类推。 
        /// 这些偏移量也可以是负数，表示从列表末尾开始的偏移量，例如-1是列表的最后一个元素，-2是倒数第二个，依此类推。 
        /// 请注意，如果您有一个从0到100的数字列表，则LRANGE list 0 10将返回11个元素，即，其中包括最右边的项目。
        /// </summary>
        /// <param name="start">0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="flags"></param>
        /// <returns>返回包含最右边的项</returns>
        public IEnumerable<T> Range(long start = 0, long stop = -1, CommandFlags flags = CommandFlags.None)
            => Database.ListRange(RedisKey, start, stop, flags).Select(x => Deserialize<T>(x));
        #endregion

        #region Remove & Trim & GetLength
        /// <summary>
        /// 从存储的列表中删除等于value的元素的第一个计数出现。 
        /// count参数通过以下方式影响操作：
        ///     count > 0：删除等于从头到尾移动的值的元素。 
        ///     count 小于 0：删除等于从尾到头的值的元素。 
        ///     count  0：删除所有等于value的元素
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count">count大于0 从头删除count个元素; count=0 删除全部; count小于0 从后删除count个元素</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long Remove(T value, long count = 0, CommandFlags flags = CommandFlags.None)
            => Database.ListRemove(RedisKey, Serialize(value), count, flags);

        /// <summary>
        /// 修剪现有列表，使其仅包含指定范围的指定元素。 
        /// 开始和停止都是从零开始的索引，其中0是列表的第一个元素（头），1是下一个元素，依此类推。 
        /// 例如：LTRIM foobar 0 2将修改存储在foobar的列表，以便仅保留列表的前三个元素。 
        /// start和end也可以是负数，指示与列表末尾的偏移量，其中-1是列表的最后一个元素，-2是倒数第二个元素，依此类推。
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="flags"></param>
        public void Trim(long start, long stop, CommandFlags flags = CommandFlags.None)
            => Database.ListTrim(RedisKey, start, stop, flags);

        /// <summary>
        /// 返回键处存储的列表的长度。 如果key不存在，则将其解释为空列表并返回0。
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long GetLength(CommandFlags flags = CommandFlags.None)
            => Database.ListLength(RedisKey, flags);
        #endregion
    }
}
