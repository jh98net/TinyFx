using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TinyFx.Extensions.StackExchangeRedis
{
    /// <summary>
    /// Redis Set集合（不重复集合）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RedisSetClient<T> : RedisClientBase
    {
        public override RedisType RedisType => RedisType.Set;

        #region Constructors
        public RedisSetClient(RedisClientOptions options = null) : base(options) { }
        #endregion

        #region LoadAllValuesWhenRedisNotExists
        public delegate IEnumerable<T> LoadAllValuesDelegate();
        public LoadAllValuesDelegate LoadAllValuesHandler;
        /// <summary>
        /// 调用GetAllOrLoad()时，当RedisKey不存在，则调用此方法返回并存储全部set值到redis中，需要时子类实现override
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<T> LoadAllValuesWhenRedisNotExists()
        {
            if (LoadAllValuesHandler != null)
                return LoadAllValuesHandler();
            throw new NotImplementedException();
        }
        #endregion

        #region GetAll & GetAllOrLoad
        /// <summary>
        /// 获取SET所有成员
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IEnumerable<T> Members(CommandFlags flags = CommandFlags.None)
            => Database.SetMembers(RedisKey, flags).Select(value => Deserialize<T>(value));
        /// <summary>
        /// 获取SET所有成员
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(CommandFlags flags = CommandFlags.None)
            => Members(flags);
        /// <summary>
        /// 获取所有SET缓存值，如果RedisKey不存在，则调用LoadAllValuesWhenRedisNotExists()返回并保存到redis中
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAllOrLoad(CommandFlags flags = CommandFlags.None)
        {
            IEnumerable<T> ret = default;
            if (!KeyExists(flags))
            {
                ret = LoadAllValuesWhenRedisNotExists();
                Add(ret, flags);
            }
            else
            {
                ret = Members(flags);
            }
            return ret;
        }
        #endregion

        #region Add & Remove & Move & Contains & GetLength
        /// <summary>
        /// 将指定的成员添加到存储在key的集合中。 存在则忽略，不存在则添加，RedisKey不存在则创建
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool Add(T value, CommandFlags flags = CommandFlags.None)
            => Database.SetAdd(RedisKey, Serialize(value), flags);

        public long Add(IEnumerable<T> values, CommandFlags flags = CommandFlags.None)
            => Database.SetAdd(RedisKey, values.Select(value => Serialize(value)).ToArray(), flags);
        /// <summary>
        /// 从SET中移除指定元素
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool Remove(T value, CommandFlags flags = CommandFlags.None)
            => Database.SetRemove(RedisKey, Serialize(value), flags);

        /// <summary>
        /// 从SET中移除多个指定元素
        /// </summary>
        /// <param name="values"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long Remove(IEnumerable<T> values, CommandFlags flags = CommandFlags.None)
            => Database.SetRemove(RedisKey, values.Select(value => Serialize(value)).ToArray(), flags);
        /// <summary>
        /// 将成员从源集合移到目标集合。 此操作是原子的。 
        /// 在每个给定的时刻，该元素将似乎是其他客户端的源或目标的成员。 如果指定的元素已存在于目标集中，则仅将其从源集中删除
        /// </summary>
        /// <param name="destinationKey">目标集合键</param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool Move(string destinationKey, T value, CommandFlags flags = CommandFlags.None)
            => Database.SetMove(RedisKey, destinationKey, Serialize(value), flags);

        /// <summary>
        /// SET是否存在此值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool Contains(T value, CommandFlags flags = CommandFlags.None)
            => Database.SetContains(RedisKey, Serialize(value), flags);

        /// <summary>
        /// SET缓存个数
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long GetLength(CommandFlags flags = CommandFlags.None)
            => Database.SetLength(RedisKey, flags);
        #endregion

        #region Pop & Random & Scan

        /// <summary>
        /// 随机从SET中删除并返回一个元素
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T Pop(CommandFlags flags = CommandFlags.None)
            => Deserialize<T>(Database.SetPop(RedisKey, flags));
        /// <summary>
        /// 随机从SET中删除并返回指定数量的元素
        /// </summary>
        /// <param name="count"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IEnumerable<T> Pop(long count, CommandFlags flags = CommandFlags.None)
            => Database.SetPop(RedisKey, count, flags).Select(value => Deserialize<T>(value));

        /// <summary>
        /// 随机从SET中取出一个元素
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T Random(CommandFlags flags = CommandFlags.None)
            => Deserialize<T>(Database.SetRandomMember(RedisKey, flags));
        /// <summary>
        /// 随机从SET中取出指定数量的元素.
        ///     如果count为正数，返回由count个不同元素组成的数组。
        ///     如果count为负数，则可以返回包含重复元素的数组，返回数量是count的绝对值
        /// </summary>
        /// <param name="count">取出的随机数量。正数：返回不同元素的数组。负数：返回可重复元素的数组，数组长度是此值的绝对值</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IEnumerable<T> Random(long count, CommandFlags flags = CommandFlags.None)
            => Database.SetRandomMembers(RedisKey, count, flags).Select(value => Deserialize<T>(value));

        /// <summary>
        /// 使用SSCAN命令遍历集合
        /// </summary>
        /// <param name="pattern">查询表达式</param>
        /// <param name="pageSize"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IEnumerable<T> Scan(string pattern, int pageSize, CommandFlags flags)
            => Database.SetScan(RedisKey, pattern, pageSize, flags).Select(value => Deserialize<T>(value));

        /// <summary>
        /// 使用SSCAN命令遍历集合
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <param name="cursor"></param>
        /// <param name="pageOffset"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IEnumerable<T> Scan(string pattern = default, int pageSize = 10, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
            => Database.SetScan(RedisKey, pattern, pageSize, cursor, pageOffset, flags).Select(value => Deserialize<T>(value));
        #endregion
    }
}
