using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TinyFx.Reflection;
using System.Collections.Concurrent;

namespace TinyFx.Data.DataMapping
{
    /// <summary>
    /// 通过 IRowMapper 接口定义实现 IDataReader ==> Entity 的映射类
    /// </summary>
    internal class InterfaceDataMapping
    {
        private static readonly ConcurrentDictionary<Type, InterfaceMapperBuilder> _builderCache = new ConcurrentDictionary<Type, InterfaceMapperBuilder>();
        private static readonly HashSet<Type> _unmapCache = new HashSet<Type>();
        /// <summary>
        /// 获取IDataReader映射到实体类的构建方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Func<IDataReader, T> GetRowMapper<T>()
        {
            Func<IDataReader, T> ret = null;
            Type type = typeof(T);
            if (_builderCache.ContainsKey(type))
                return _builderCache[type].Build<T>;
            if (_unmapCache.Contains(type))
                return null;
            if (type.GetInterfaces().Contains(typeof(IRowMapper<T>)))
            {
                InterfaceMapperBuilder builder = new InterfaceMapperBuilder(type);
                _builderCache.TryAdd(type, builder);
                ret = builder.Build<T>;
            }
            else
                _unmapCache.Add(type);
            return ret;
        }
    }
    internal class InterfaceMapperBuilder
    {
        private object _mapper;
        public InterfaceMapperBuilder(Type t)
        {
            _mapper = ReflectionUtil.CreateInstance(t);
        }
        public T Build<T>(IDataReader reader)
        {
            IRowMapper<T> ret = (IRowMapper<T>)_mapper;
            return ret.MapRow(reader);
        }
    }

}
