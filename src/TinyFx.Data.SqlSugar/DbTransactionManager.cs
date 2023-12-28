using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
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
        public Repository<T> GetRepository<T>(params object[] splitDbKeys)
            where T : class, new()
        {
            var db = GetDb<T>(splitDbKeys);
            return new Repository<T>(db);
        }
        public ISqlSugarClient GetDb(params object[] splitDbKeys)
            => GetDb<object>(splitDbKeys);
        public ISqlSugarClient GetDb<T>(params object[] splitDbKeys)
        {
            var configId = DIUtil.GetRequiredService<IDbSplitProvider>()
               .SplitDb<T>(splitDbKeys);
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

        #region Commit & Rollback
        public void Commit()
        {
            if (NeedSubmit())
                _newDb.CommitTran();
        }
        public async Task CommitAsync()
        {
            if (NeedSubmit())
                await _newDb.CommitTranAsync();
        }
        public void Rollback()
        {
            if (NeedSubmit())
                _newDb.RollbackTran();
        }
        public async Task RollbackAsync()
        {
            if (NeedSubmit())
                await _newDb.RollbackTranAsync();
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
