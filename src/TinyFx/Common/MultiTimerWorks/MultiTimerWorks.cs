using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Logging;

namespace TinyFx.Common
{
    /// <summary>
    /// 多Timer后台任务
    /// </summary>
    public class MultiTimerWorks
    {
        #region Properties
        /// <summary>
        /// 停止服务时，等待运行中的timer任务的timeout时间
        /// </summary>
        public int WaitTasksTimeout { get; set; } = 20000;
        private CancellationTokenSource _stoppingCts = new CancellationTokenSource(); //终止通知
        private CancellationTokenSource _changeCts = new CancellationTokenSource();//改变通知
        protected readonly HashSet<Task> RunningTasks = new HashSet<Task>();
        protected readonly ConcurrentDictionary<string, TimerWork> TimerWorks = new ConcurrentDictionary<string, TimerWork>();
        #endregion

        #region Methods
        public void RegisterWork(TimerWork work)
        {
            CheckWork(work);
            work.SetStoppingToken(_stoppingCts.Token);
            if (!TimerWorks.TryAdd(work.WorkId, work))
                throw new Exception("WorkId不能重复");
            _changeCts.Cancel();
        }
        public bool RegisterAndUpdateWork(TimerWork work)
        {
            CheckWork(work);
            work.SetStoppingToken(_stoppingCts.Token);
            TimerWorks.AddOrUpdate(work.WorkId, work, (k, v) => work);
            _changeCts.Cancel();
            return true;
        }
        private void CheckWork(TimerWork work)
        {
            if (work == null)
                throw new ArgumentNullException(nameof(work), $"注册MultiTimerWorks时work不能为空");
            if (string.IsNullOrEmpty(work.WorkId))
                throw new Exception("TimerWork参数错误。WorkId != null");
            if (work.Interval == 0)
                throw new Exception("TimerWork参数错误。Interval != 0");
            if (work.Action == null)
                throw new Exception("TimerWork参数错误。Action != null");
        }
        public bool UnregisterWork(string workId)
        {
            if (!TimerWorks.TryRemove(workId, out TimerWork _))
                return false;
            _changeCts.Cancel();
            return true;
        }
        public IEnumerable<Task> GetTasks()
        {
            var enumtor = RunningTasks.GetEnumerator();
            while (enumtor.MoveNext())
            {
                yield return enumtor.Current;
            }
        }
        public IEnumerable<TimerWork> GetWorks()
        {
            var enumtor = TimerWorks.GetEnumerator();
            while (enumtor.MoveNext())
            {
                yield return enumtor.Current.Value;
            }
        }
        #endregion

        public async Task StartAsync(CancellationToken stoppingToken = default)
        {
            // 外层停止，通知内层停止
            stoppingToken.Register(() =>
            {
                _stoppingCts.Cancel();
            });
            int interval = 0;
            while (!_stoppingCts.IsCancellationRequested)
            {
                int nextInterval = int.MaxValue;
                foreach (var work in GetWorks())
                {
                    if (work.TryExecute(interval, out Task task))
                    {
                        RunningTasks.Add(task);
                        var _ = task.ContinueWith(t =>
                        {
                            if (t.IsFaulted)
                                LogUtil.Error(t.Exception, "执行MultiTimerWorks任务失败。workId:{workId} message:{ex.message}", work.WorkId, t.Exception?.Message);
                            if (t.IsCompleted)
                                RunningTasks.Remove(t);
                        });
                    }
                    if (work.IsCycleEnd)//循环结束
                    {
                        TimerWorks.TryRemove(work.WorkId, out var _);
                    }
                    else
                    {
                        nextInterval = Math.Min(nextInterval, work.Remain);
                    }
                }
                interval = nextInterval;
                await Task.Delay(TimeSpan.FromMilliseconds(nextInterval), _changeCts.Token).ContinueWith((t) => {
                    if (t.Status == TaskStatus.Canceled)
                    {
                        interval = 0;
                        _changeCts = new CancellationTokenSource();
                    }
                }, stoppingToken);
            }
        }
        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            _stoppingCts.Cancel();
            _changeCts.Cancel();
            var tasks = GetTasks().ToArray();
            if (!Task.WaitAll(tasks, WaitTasksTimeout, cancellationToken))
            {
                string errMsg = null;
                foreach (var task in tasks)
                {
                    if (task.IsFaulted)
                        errMsg += task.Exception?.Message;
                }
                LogUtil.Error("{typeName} 等待停止运行任务时Timerout! {errorMessage}", GetType().FullName, errMsg);
            }
            return Task.CompletedTask;
        }

        #region CalcProcessInterval
        /*
        private int _interval = -1;
        /// <summary>
        /// 轮询最小执行间隔
        /// </summary>
        public int ProcessInterval { get; set; } = 200;
        private readonly object _locker = new object();
        // 计算合理interval间隔
        private void CalcProcessInterval()
        {
            lock (_locker)
            {
                var value = -1;
                if (!TimerWorks.IsEmpty)
                {
                    if (TimerWorks.Count == 1)
                    {
                        var item = TimerWorks.FirstOrDefault();
                        value = (item.Value != null) ? item.Value.Interval : ProcessInterval;
                    }
                    else
                    {
                        var list = GetWorks().Select(w => w.Interval).ToList();
                        list.Sort();
                        value = list[0];
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (i == list.Count - 1)
                                break;
                            var diff = list[i + 1] - list[i];
                            value = (diff < value) ? diff : value;
                        }
                    }
                    if (value < ProcessInterval)
                        value = ProcessInterval;
                }
                Interlocked.Exchange(ref _interval, value);
                _changeCts.Cancel(); //退出Delay，使用新的interval
            }
        }
        */
        #endregion
    }
}
