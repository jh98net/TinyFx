using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Logging;

namespace TinyFx.Data.SqlSugar
{
    /// <summary>
    /// SqlSugar事务管理对象
    /// </summary>
    public class DbTransactionManager
    {
        #region Properties
        private SqlSugarClient _newDb;
        public IsolationLevel IsolationLevel { get; }
        private readonly Exception _exception = null;
        private DbTransactionStatus _status = DbTransactionStatus.Init;

        public DbTransactionManager(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            IsolationLevel = isolationLevel;
            _newDb = DbUtil.MainDb.CopyNew();
            var stack = new StackTrace(0, true);
            _exception = new Exception($"DbTransactionManager对象在析构函数中调用释放，请显示调用Commit()或Rollback()释放资源。StackTrace:{stack.ToString()}");
        }
        #endregion

        #region Callback
        /// <summary>
        /// 执行Callback时是否抛出异常
        /// </summary>
        public bool ThrowCallbackException { get; set; } = true;
        private List<Action> _commitCallbacks = new List<Action>();
        private List<Action> _rollbackCallbacks = new List<Action>();
        public void AddCommitCallback(Action callback)
            => _commitCallbacks.Add(callback);
        public void AddRollbackCallback(Action callback)
            => _rollbackCallbacks.Add(callback);
        #endregion

        #region Begin
        public void Begin()
        {
            CheckBeginTran();
            _newDb.BeginTran(IsolationLevel);
            _status = DbTransactionStatus.TranBegined;
        }
        public async Task BeginAsync()
        {
            CheckBeginTran();
            await _newDb.BeginTranAsync(IsolationLevel);
            _status = DbTransactionStatus.TranBegined;
        }
        private void CheckBeginTran()
        {
            if (_status != DbTransactionStatus.Init)
                throw new Exception("DbTransactionManager执行Begin()时Status必须是Init");
        }
        #endregion

        #region GetDb & GetRepository
        public Repository<T> GetRepository<T>(object splitDbKey = null)
            where T : class, new()
        {
            var db = GetDb<T>(splitDbKey);
            return new Repository<T>(db);
        }
        public ISqlSugarClient GetDb(object splitDbKey = null)
            => GetDb<object>(splitDbKey);
        public ISqlSugarClient GetDb<T>(object splitDbKey = null)
        {
            var configId = DIUtil.GetRequiredService<IDbSplitProvider>()
               .SplitDb<T>(splitDbKey);
            return GetDbById(configId);
        }
        public ISqlSugarClient GetDbById(string configId = null)
        {
            if (_status == DbTransactionStatus.End)
                throw new Exception("DbTransactionManager执行GetDb()时已经Commit或者Rollback(Status=End)");
            if (_status == DbTransactionStatus.Init)
                Begin();
            _status = DbTransactionStatus.TranUsed;

            // 主库
            if (string.IsNullOrEmpty(configId) || configId == DbUtil.DefaultConfigId)
                return _newDb.GetConnection(DbUtil.DefaultConfigId);

            var config = DbUtil.GetConfig(configId);
            TryAddDb(config);
            return _newDb.GetConnection(config.ConfigId);
        }
        private bool TryAddDb(ConnectionElement config)
        {
            if (Convert.ToString(config.ConfigId) == DbUtil.DefaultConfigId)
                return false;
            if (!_newDb.IsAnyConnection(config.ConfigId))
            {
                _newDb.AddConnection(config);
                var newDb = _newDb.GetConnection(config.ConfigId);
                DbUtil.InitDb(newDb, config);
                return true;
            }
            return false;
        }
        #endregion

        #region 常用方法
        #region Queryable
        /// <summary>
        /// 获取查询器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public ISugarQueryable<T> GetQueryable<T>(object splitDbKey = null)
            => GetDb<T>(splitDbKey).Queryable<T>();
        public ISugarQueryable<T, T2> GetQueryable<T, T2>(Expression<Func<T, T2, JoinQueryInfos>> joinExpression, object splitDbKey = null)
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public ISugarQueryable<T, T2, T3> GetQueryable<T, T2, T3>(Expression<Func<T, T2, T3, JoinQueryInfos>> joinExpression, object splitDbKey = null)
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public ISugarQueryable<T, T2, T3, T4> GetQueryable<T, T2, T3, T4>(Expression<Func<T, T2, T3, T4, JoinQueryInfos>> joinExpression, object splitDbKey = null)
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public ISugarQueryable<T, T2, T3, T4, T5> GetQueryable<T, T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, JoinQueryInfos>> joinExpression, object splitDbKey = null)
            => GetDb<T>(splitDbKey).Queryable(joinExpression);

        /// <summary>
        /// 获取查询器(inner join)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public ISugarQueryable<T, T2> GetQueryable<T, T2>(Expression<Func<T, T2, bool>> joinExpression, object splitDbKey = null)
            where T : class, new()
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public ISugarQueryable<T, T2, T3> GetQueryable<T, T2, T3>(Expression<Func<T, T2, T3, bool>> joinExpression, object splitDbKey = null)
            where T : class, new()
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public ISugarQueryable<T, T2, T3, T4> GetQueryable<T, T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> joinExpression, object splitDbKey = null)
            where T : class, new()
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        public ISugarQueryable<T, T2, T3, T4, T5> GetQueryable<T, T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, bool>> joinExpression, object splitDbKey = null)
            where T : class, new()
            => GetDb<T>(splitDbKey).Queryable(joinExpression);
        #endregion

        #region GetByIdAsync
        /// <summary>
        /// 按单一主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync<T>(dynamic id, object splitDbKey = null)
              where T : class, new()
          => await GetRepository<T>(splitDbKey).GetByIdAsync(id);
        /// <summary>
        /// 按单一主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByIdAsync<T>(List<dynamic> ids, object splitDbKey = null)
               where T : class, new()
          => await GetRepository<T>(splitDbKey).GetByIdAsync(ids);
        /// <summary>
        /// 按联合主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync<T>(T id, object splitDbKey = null)
             where T : class, new()
         => await GetRepository<T>(splitDbKey).GetByIdAsync(id);
        /// <summary>
        /// 按联合主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByIdAsync<T>(List<T> ids, object splitDbKey = null)
             where T : class, new()
         => await GetRepository<T>(splitDbKey).GetByIdAsync(ids);
        #endregion

        #region DeleteByIdAsync
        /// <summary>
        /// 按单一主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync<T>(dynamic id, object splitDbKey = null)
              where T : class, new()
          => await GetRepository<T>(splitDbKey).DeleteByIdAsync(id);
        /// <summary>
        /// 按单一主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync<T>(List<dynamic> ids, object splitDbKey = null)
              where T : class, new()
          => await GetRepository<T>(splitDbKey).DeleteByIdAsync(ids);
        /// <summary>
        /// 按联合主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync<T>(T id, object splitDbKey = null)
              where T : class, new()
          => await GetRepository<T>(splitDbKey).DeleteByIdAsync(id);
        /// <summary>
        /// 按联合主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync<T>(List<T> ids, object splitDbKey = null)
              where T : class, new()
          => await GetRepository<T>(splitDbKey).DeleteByIdAsync(ids);
        #endregion

        #region Delete & Insert & Update

        public async Task<bool> DeleteAsync<T>(T item, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).DeleteAsync(item);
        public async Task<bool> DeleteAsync<T>(List<T> items, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).DeleteAsync(items);

        public async Task<bool> InsertAsync<T>(T item, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).InsertAsync(item);
        public async Task<bool> InsertAsync<T>(List<T> items, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).InsertRangeAsync(items);

        public async Task<bool> UpdateAsync<T>(T item, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).UpdateAsync(item);
        public async Task<bool> UpdateAsync<T>(List<T> items, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).UpdateRangeAsync(items);

        public async Task<bool> InsertOrUpdateAsync<T>(T item, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).InsertOrUpdateAsync(item);
        public async Task<bool> InsertOrUpdateAsync<T>(List<T> items, object splitDbKey = null)
              where T : class, new()
            => await GetRepository<T>(splitDbKey).InsertOrUpdateAsync(items);
        #endregion
        #endregion

        #region Commit & Rollback
        public void Commit()
        {
            if (NeedSubmit())
            {
                _newDb.CommitTran();
                SubmitCallback(true);
            }
        }
        public async Task CommitAsync()
        {
            if (NeedSubmit())
            {
                await _newDb.CommitTranAsync();
                SubmitCallback(true);
            }
        }
        public void Rollback()
        {
            if (NeedSubmit())
            {
                _newDb.RollbackTran();
                SubmitCallback(false);
            }
        }
        public async Task RollbackAsync()
        {
            if (NeedSubmit())
            {
                await _newDb.RollbackTranAsync();
                SubmitCallback(false);
            }
        }
        private bool NeedSubmit()
        {
            var ret = false;
            GC.SuppressFinalize(this);
            switch (_status)
            {
                case DbTransactionStatus.Init:
                    LogUtil.Info(_exception, "DbTransactionManager在Commit或Rollback时已创建但没有使用");
                    break;
                case DbTransactionStatus.TranBegined:
                    LogUtil.Warning(_exception, "DbTransactionManager在Commit或Rollback时已Begin()但没有调用GetDb使用事务");
                    break;
                case DbTransactionStatus.TranUsed:
                    ret = true;
                    break;
                case DbTransactionStatus.End:
                    throw new Exception("DbTransactionManager在Commit或Rollback时已经调用Commit或Rollback提交过事务", _exception);
            }
            _status = DbTransactionStatus.End;
            return ret;
        }
        private void SubmitCallback(bool isCommit)
        {
            int idx = 0;
            try
            {
                if (isCommit)
                    _commitCallbacks.ForEach(x => { idx++; x(); });
                else
                    _rollbackCallbacks.ForEach(x => { idx++; x(); });
            }
            catch (Exception ex)
            {
                if (ThrowCallbackException)
                {
                    throw;
                }
                else
                {
                    LogUtil.GetContextLogger()
                        .SetLevel(Microsoft.Extensions.Logging.LogLevel.Warning)
                        .AddField($"DbTransactionManager.Callback.{idx}", ex)
                        .Save();
                }
            }
        }
        #endregion

        ~DbTransactionManager()
        {
            try
            {
                LogUtil.Error(_exception, $"DbTransactionManager析构函数被执行，请Commit或Rollback。status:{_status}");
            }
            catch { }
        }
    }
    internal enum DbTransactionStatus
    {
        /// <summary>
        /// 初始
        /// </summary>
        Init = 0,
        /// <summary>
        /// 事务已开启
        /// </summary>
        TranBegined = 1,
        /// <summary>
        /// 事务已使用
        /// </summary>
        TranUsed = 2,
        /// <summary>
        /// 结束
        /// </summary>
        End = 3
    }
}
