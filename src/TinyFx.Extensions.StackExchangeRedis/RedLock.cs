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
        private System.Timers.Timer _timer;
        /// <summary>
        /// 要锁定资源的键值，针对每一个需要锁定的资源，名称必须唯一，如需要锁定操作用户coin，LockKey="lock_key_user_coin"
        /// </summary>
        public string LockKey { get; }
        private string _token { get; }

        public TimeSpan Expiry { get; }
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
            LockKey = $"{RedisPrefixConst.RED_LOCK}:{lockKey}";
            _token = Guid.NewGuid().ToString();
            Expiry = expiry.HasValue ? expiry.Value : TimeSpan.FromSeconds(2); ;
            RetryCount = retryCount <= 0 ? 6 : retryCount;
            RetryInterval = retryInterval.HasValue ? retryInterval.Value : TimeSpan.FromMilliseconds(500);
            _timer = new System.Timers.Timer(Expiry.TotalMilliseconds / 2);
        }

        public async Task StartAsync()
        {
            IsLocked = false;
            int retry = RetryCount;
            do
            {
                if (await Database.LockTakeAsync(LockKey, _token, Expiry))
                {
                    LogUtil.Debug("RedLock申请锁成功。clientType:{clientType} lockKey:{lockKey} token:{token}", ClientType.Name, LockKey, _token);
                    IsLocked = true;
                    _timer.Elapsed += async (sender, args) =>
                    {
                        await Database.LockExtendAsync(LockKey, _token, Expiry);//过期中间，延期
                    };
                    _timer.Start();
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
            LogUtil.Error("RedLock申请锁失败。clientType:{clientType} lockKey:{lockKey} token:{token}", ClientType.Name, LockKey, _token);
        }
        /// <summary>
        /// 手动释放锁
        /// </summary>
        public void Release()
        {
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
            Database.LockRelease(LockKey, _token);
            GC.SuppressFinalize(this);
            _disposed = true;
        }
        #region IDisposable
        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) return;
            Release();
        }

        ~RedLock()
        {
            Dispose();
            LogUtil.Error("RedLock没有Dispose，请使用using调用。clientType:{clientType} lockKey:{lockKey} token:{token}", ClientType.Name, LockKey, _token);
        }

        public void Close()
            => ((IDisposable)this).Dispose();
        #endregion
    }
}
