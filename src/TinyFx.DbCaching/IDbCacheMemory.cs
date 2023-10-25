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
    /// <typeparam name="TEntity"></typeparam>
    public interface IDbCacheMemory<TEntity>
        where TEntity : class, new()
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
        List<TEntity> DbData { get; }
        /// <summary>
        /// 缓存的list
        /// </summary>
        ConcurrentDictionary<string, Dictionary<string, List<TEntity>>> ListDict { get; }
        /// <summary>
        /// 缓存的single
        /// </summary>
        ConcurrentDictionary<string, Dictionary<string, TEntity>> SingleDict { get; }
        ConcurrentDictionary<string, object> CustomDict { get; }

        /// <summary>
        /// 获取全部缓存项
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAllList();
        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetSingle(object id);
        TEntity GetSingle(Expression<Func<TEntity>> expr);
        /// <summary>
        /// 获取单个缓存项
        /// </summary>
        /// <param name="expr"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity GetSingle<TResult>(Expression<Func<TEntity, TResult>> expr, TEntity entity);
        public TEntity GetSingle<TResult>(Expression<Func<TEntity, TResult>> fieldsExpr, object singleValue);
        TEntity GetSingleByKey(string dictKey, string valueKey);

        List<TEntity> GetList(Expression<Func<TEntity>> expr);
        /// <summary>
        /// 获取一组缓存项
        /// </summary>
        /// <param name="expr"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        List<TEntity> GetList<TResult>(Expression<Func<TEntity, TResult>> expr, TEntity entity);
        public List<TEntity> GetList<TResult>(Expression<Func<TEntity, TResult>> fieldsExpr, object singleValue);
        List<TEntity> GetListByKey(string dictKey, string valueKey);
        /// <summary>
        /// 自定义单字典缓存，name唯一
        /// </summary>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Dictionary<string, TEntity> GetOrAddCustom(string name, Func<List<TEntity>, Dictionary<string, TEntity>> func);
        /// <summary>
        /// 自定义列表字典缓存，name唯一
        /// </summary>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Dictionary<string, List<TEntity>> GetOrAddCustom(string name, Func<List<TEntity>, Dictionary<string, List<TEntity>>> func);
        /// <summary>
        /// 自定义对象缓存，name唯一
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        TCache GetOrAddCustom<TCache>(string name, Func<List<TEntity>, TCache> func);
    }
}
