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
        public Repository(params object[] routingData)
        {
            var routingProvider = DIUtil.GetRequiredService<IDbRoutingProvider>();
            var configId = routingProvider.RouteDb<T>(routingData);
            var db = DbUtil.GetDb(configId).CopyNew();
            //
            db.CurrentConnectionConfig.ConfigureExternalServices.SplitTableService
                = routingProvider.RouteTable<T>();
            base.Context = db;
        }

        public virtual async Task<T> GetByIdsAsync(T data)
        {
            var ret = await Context.Queryable<T>().WhereClassByPrimaryKey(data).ToListAsync();
            if (ret.Count > 1)
                throw new Exception($"SqlSugar多主键查询不唯一。pkeys: {SerializerUtil.SerializeJson(data)}");
            return ret.Count == 0 ? null : ret[0];
        }
        public virtual Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression, OrderByType orderByType = OrderByType.Asc)
        {
            var query = Context.Queryable<T>();
            if (orderByExpression != null)
                query = query.OrderBy(orderByExpression, orderByType);
            return query.FirstAsync(whereExpression);
        }

        public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression, OrderByType orderByType = OrderByType.Asc, int top = 0)
        {
            var query = Context.Queryable<T>().Where(whereExpression);
            if (orderByExpression != null)
                query = query.OrderBy(orderByExpression, orderByType);
            if (top > 0)
                query = query.Take(top);
            return query.ToListAsync();
        }

        public Task<TResult> GetMax<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereExpression = null)
        {
            var query = Context.Queryable<T>();
            if (whereExpression != null)
                query = query.Where(whereExpression);
            return query.MaxAsync(expression);
        }
        public Task<TResult> GetMin<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereExpression = null)
        {
            var query = Context.Queryable<T>();
            if (whereExpression != null)
                query = query.Where(whereExpression);
            return query.MinAsync(expression);
        }
        public Task<TResult> GetSum<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereExpression = null)
        {
            var query = Context.Queryable<T>();
            if (whereExpression != null)
                query = query.Where(whereExpression);
            return query.SumAsync(expression);
        }
    }
}
