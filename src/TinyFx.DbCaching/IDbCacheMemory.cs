using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.DbCaching
{
    /// <summary>
    ///支持通知更新的内存缓存对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDbCacheMemory<T>
        where T : class, new()
    {
        /// <summary>
        /// 表描述信息
        /// </summary>
        SugarTable TableAttribute { get; }
        /// <summary>
        /// 数据库id
        /// </summary>
        string ConfigId { get; }
        /// <summary>
        /// redis缓存DbCacheDataDCache的 field key
        /// </summary>
        string CachKey { get; }
        /// <summary>
        /// 主键
        /// </summary>
        List<string> PrimaryKeys { get; }
        /// <summary>
        /// 数据库原始数据
        /// </summary>
        List<T> DbData { get; }
        /// <summary>
        /// 缓存的list
        /// </summary>
        ConcurrentDictionary<string, Dictionary<string, List<T>>> ListDict { get; }
        /// <summary>
        /// 缓存的single
        /// </summary>
        ConcurrentDictionary<string, Dictionary<string, T>> SingleDict { get; }

        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetSingle(object id);
        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T>> expr);
        /// <summary>
        /// 获取一组缓存项
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T>> expr);
        /// <summary>
        /// 获取全部缓存项
        /// </summary>
        /// <returns></returns>
        List<T> GetAllList();
    }
}
