using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Data.SqlSugar
{
    public class Repository<T> : SimpleClient<T>
        where T : class, new()
    {
        #region Properties
        public string ConfigId => Convert.ToString(Context.CurrentConnectionConfig.ConfigId);
        public Repository(ISqlSugarClient db)
        {
            base.Context = db;
        }
        public Repository(object splitDbKey = null)
        {
            var splitProvider = DIUtil.GetRequiredService<IDbSplitProvider>();
            var configId = splitProvider.SplitDb<T>(splitDbKey);
            base.Context = DbUtil.GetDbById(configId);
        }
        #endregion

        public void SetCommandTimeout(int timeoutSeconds)
        {
            base.Context.Ado.CommandTimeOut = timeoutSeconds;
        }
        
        /// <summary>
        /// 根据联合主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByIdAsync(T id)
        {
            var ret = await Context.Deleteable(id).ExecuteCommandAsync();
            return ret == 1;
        }
        public virtual async Task<bool> DeleteByIdsAsync(List<T> ids)
        {
            var ret = await Context.Deleteable(ids).ExecuteCommandAsync();
            return ret > 0;
        }

        /// <summary>
        /// 根据联合主键查询
        /// </summary>
        /// <param name="id">联合主键值</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<T> GetByIdAsync(T id)
        {
            var ret = await Context.Queryable<T>().WhereClassByPrimaryKey(id).ToListAsync();
            if (ret.Count > 1)
                throw new Exception($"Repository.GetByIdAsync()多主键查询不唯一。type:{typeof(T).FullName} pkeys: {SerializerUtil.SerializeJson(id)}");
            return ret.Count == 0 ? null : ret[0];
        }
        /// <summary>
        /// 根据联合主键查询
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetByIdsAsync(List<T> ids)
            => await Context.Queryable<T>().WhereClassByPrimaryKey(ids).ToListAsync();

        public virtual Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression, OrderByType orderByType = OrderByType.Asc)
        {
            var query = Context.Queryable<T>();
            if (orderByExpression != null)
                query.OrderBy(orderByExpression, orderByType);
            return query.FirstAsync(whereExpression);
        }

        public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression, OrderByType orderByType = OrderByType.Asc, int top = 0)
        {
            var query = Context.Queryable<T>().Where(whereExpression);
            if (orderByExpression != null)
                query.OrderBy(orderByExpression, orderByType);
            if (top > 0)
                query.Take(top);
            return query.ToListAsync();
        }

        public Task<TResult> GetMaxAsync<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereExpression = null)
        {
            var query = Context.Queryable<T>();
            if (whereExpression != null)
                query.Where(whereExpression);
            return query.MaxAsync(expression);
        }
        public Task<TResult> GetMinAsync<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereExpression = null)
        {
            var query = Context.Queryable<T>();
            if (whereExpression != null)
                query.Where(whereExpression);
            return query.MinAsync(expression);
        }
        public Task<TResult> GetSumAsync<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereExpression = null)
        {
            var query = Context.Queryable<T>();
            if (whereExpression != null)
                query.Where(whereExpression);
            return query.SumAsync(expression);
        }

        public Task<TResult> GetAvgAsync<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereExpression = null)
        {
            var query = Context.Queryable<T>();
            if (whereExpression != null)
                query.Where(whereExpression);
            return query.AvgAsync(expression);
        }
    }
}
