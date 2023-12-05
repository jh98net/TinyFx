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
        private int _state = 0;//0-初始1-begin 2-open 3-end

        public DbTransactionManager(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            IsolationLevel = isolationLevel;
            _newDb = DbUtil.MainDb.CopyNew();
            var stack = new StackTrace(0, true);
            _exception = new Exception($"DbTransactionManager对象在析构函数中调用释放，请显示调用Commit()或Rollback()释放资源。StackTrace:{stack.ToString()}");
        }
        #endregion

        #region Method
        public void Begin()
        {
            SetBeginTran();
            _newDb.BeginTran(IsolationLevel);
        }
        public Task BeginAsync()
        {
            SetBeginTran();
            return _newDb.BeginTranAsync(IsolationLevel);
        }
        private void SetBeginTran()
        {
            if (_state != 0)
                throw new Exception("DbTransactionManager执行所有事务前必须先Begin()");
            _state = 1;
        }

        public ISqlSugarClient GetDb<T>(params object[] splitDbKeys)
        {
            var configId = DIUtil.GetRequiredService<IDbSplitProvider>()
               .SplitDb<T>(splitDbKeys);
            return GetDb(configId);
        }
        public ISqlSugarClient GetDb(string configId = null)
        {
            if (_state != 1 && _state != 2)
                throw new Exception("DbTransactionManager必须先Begin()再GetDb()或GetRepository()");
            _state = 2;
            // 主库
            if (string.IsNullOrEmpty(configId) || configId == DbUtil.DefaultConfigId)
                return _newDb.GetConnection(DbUtil.DefaultConfigId);

            var config = DbUtil.GetConfig(configId);
            TryAddDb(config);
            return _newDb.GetConnection(config.ConfigId);
        }
        public Repository<T> GetRepository<T>(params object[] splitDbKeys)
            where T : class, new()
        {
            var db = GetDb<T>(splitDbKeys);
            return new Repository<T>(db);
        }
        public void Commit()
        {
            if (!CheckStateWhenEnd()) return;
            _newDb.CommitTran();
        }
        public Task CommitAsync()
        {
            if (!CheckStateWhenEnd()) return Task.CompletedTask;
            return _newDb.CommitTranAsync();
        }
        public void Rollback()
        {
            if (!CheckStateWhenEnd()) return;
            _newDb.RollbackTran();
        }
        public Task RollbackAsync()
        {
            if (!CheckStateWhenEnd()) return Task.CompletedTask;
            return _newDb.RollbackTranAsync();
        }
        #endregion

        #region Utils
        private object _sync = new();
        private bool TryAddDb(ConnectionElement config)
        {
            if (Convert.ToString(config.ConfigId) == DbUtil.DefaultConfigId)
                return false;
            if (!_newDb.IsAnyConnection(config.ConfigId))
            {
                lock (_sync)
                {
                    if (!_newDb.IsAnyConnection(config.ConfigId))
                    {
                        _newDb.AddConnection(config);
                        var newDb = _newDb.GetConnection(config.ConfigId);
                        DbUtil.InitDb(newDb, config);
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CheckStateWhenEnd()
        {
            GC.SuppressFinalize(this);
            switch (_state)
            {
                case 0:
                    LogUtil.Warning(_exception, "DbTransactionManager被创建但没有使用");
                    _state = 3;
                    return false;
                case 1:
                    throw new Exception("DbTransactionManager在Commit或Rollback时，已经Begin()但没有GetDb()执行任何事务");
                case 2:
                    _state = 3;
                    return true;
                default:
                    throw new Exception($"DbTransactionManager在Commit或Rollback时，_state异常: {_state}");
            }
        }
        ~DbTransactionManager()
        {
            try
            {
                LogUtil.Error(_exception, "DbTransactionManager析构函数被执行!");
            }
            catch { }
        }
        #endregion
    }
}
