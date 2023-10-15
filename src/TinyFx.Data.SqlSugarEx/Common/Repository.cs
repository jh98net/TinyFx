using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Data.SqlSugarEx
{
    public class Repository<T> : SimpleClient<T>
        where T : class, new()
    {
        public Repository(params object[] routingDbKeys)
        {
            var routingProvider = DIUtil.GetRequiredService<IDbRoutingProvider>();
            var configId = routingProvider.RouteDb<T>(routingDbKeys);
            var db = DbUtil.GetDb(configId).CopyNew();
            //
            db.CurrentConnectionConfig.ConfigureExternalServices.SplitTableService
                = routingProvider.RouteTable<T>();
            base.Context = db;
        }
        public virtual async Task<List<T>> GetByIdsAsync(List<T> keysList)
        {
            return await Context.Queryable<T>().WhereClassByPrimaryKey(keysList).ToListAsync();
        }
        public virtual async Task<T> GetByIdsAsync(T keys)
        {
            var ret = await Context.Queryable<T>().WhereClassByPrimaryKey(keys).ToListAsync();
            if (ret.Count > 1)
                throw new Exception($"SqlSugar多主键查询不唯一。pkeys: {SerializerUtil.SerializeJson(keys)}");
            return ret.Count == 0 ? null : ret[0];
        }
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
    

    }
}
