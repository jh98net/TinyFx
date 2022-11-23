using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Logging;

namespace TinyFx.Extensions.StackExchangeRedis
{
    /// <summary>
    /// 分布式锁对象
    /// </summary>
    public class RedLock : IDisposable
    {
        public IDatabase Database { get; }
        /// <summary>
        /// 要锁定资源的键值，针对每一个需要锁定的资源，名称必须唯一，如需要锁定操作用户coin，LockKey="lock_key_user_coin"
        /// </summary>
        public string LockKey { get; }
        private string _lockId { get; }

        public TimeSpan Expiry { get;  }
        public int RetryCount { get; }
        public TimeSpan RetryInterval { get; set; }

        public bool IsLocked { get; private set; }

        internal Type ClientType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="lockKey"></param>
        /// <param name="expiry"></param>
        /// <param name="retryCount"></param>
        /// <param name="retryInterval"></param>
        public RedLock(IDatabase database, string lockKey, TimeSpan? expiry, int retryCount = 0, TimeSpan? retryInterval = null)
        {
            Database = database;
            LockKey = lockKey;
            _lockId = Guid.NewGuid().ToString();
            Expiry = expiry.HasValue ? expiry.Value : TimeSpan.FromSeconds(10); ;
            RetryCount = retryCount;
            RetryInterval = retryInterval.HasValue ? retryInterval.Value : TimeSpan.FromMilliseconds(500);
        }

        public void Start()
        {
            IsLocked = false;
            int retry = RetryCount;
            do
            {
                if (Database.LockTake(LockKey, _lockId, Expiry))
                {
                    LogUtil.Debug($"RedLock申请锁成功。Type:{ClientType.FullName} LockKey:{LockKey} LockId:{_lockId}");
                    IsLocked = true;
                    return;
                }
                Thread.Sleep(RetryInterval);
                retry--;
            }
            while (retry > 0);
            LogLockError();
        }
        public async Task StartAsync()
        {
            IsLocked = false;
            int retry = RetryCount;
            do
            {
                if (await Database.LockTakeAsync(LockKey, _lockId, Expiry))
                {
                    LogUtil.Debug($"RedLock申请锁成功。Type:{ClientType.FullName} LockKey:{LockKey} LockId:{_lockId}");
                    IsLocked = true;
                    return;
                }
                await Task.Delay(RetryInterval);
                retry--;
            }
            while (retry > 0);
            LogLockError();
        }
        private void LogLockError()
        {
            LogUtil.Error($"RedLock申请锁失败。Type:{ClientType.FullName} LockKey:{LockKey} LockId:{_lockId}");
        }

        #region IDisposable
        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) return;
            if(IsLocked)
                Database.LockRelease(LockKey, _lockId);
            GC.SuppressFinalize(this);
            _disposed = true;
        }

        ~RedLock()
        {
            LogUtil.Error($"RedLock没有Dispose，请使用using调用。Type:{ClientType.FullName} LockKey:{LockKey} LockId:{_lockId}");
        }

        public void Close()
            => ((IDisposable)this).Dispose();
        #endregion
    }
}
