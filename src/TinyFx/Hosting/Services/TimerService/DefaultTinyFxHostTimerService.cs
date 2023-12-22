using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Common;
using TinyFx.Logging;
using TinyFx.Text;

namespace TinyFx.Hosting.Services
{
    public class DefaultTinyFxHostTimerService : ITinyFxHostTimerService
    {
        /// <summary>
        /// 停止服务时，等待运行中的timer任务的timeout时间
        /// </summary>
        public int WaitTasksTimeout { get; set; } = 20000;
        public int MinDelayInterval = 200;

        private ConcurrentDictionary<string, TinyFxHostTimerJob> _jobs = new();
        private readonly HashSet<Task> _tasks = new();
        private CancellationTokenSource _stoppingCts = new CancellationTokenSource(); //终止通知
        private CancellationTokenSource _changeCts = new CancellationTokenSource();//改变通知

        public DefaultTinyFxHostTimerService() { }

        #region Method
        /// <summary>
        /// 注册timer任务
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tryUpdate"></param>
        /// <returns>如果tryUpdate=true，则返回false意味着update</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public bool Register(TinyFxHostTimerItem item, bool tryUpdate = false)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), $"DefaultTinyFxHostTimerService.Register时item不能为空");
            if (item.Callback == null)
                throw new Exception("DefaultTinyFxHostTimerService.Register时Callback不能为空");

            var ret = true;
            item.Id ??= ObjectId.NewId();
            var value = new TinyFxHostTimerJob
            {
                Id = item.Id,
                Title = item.Title,
                Interval = item.Interval,
                Callback = item.Callback,
                Remain = item.Interval,
                Timestamp = DateTime.UtcNow.UtcDateTimeToTimestamp(false)
            };
            if (tryUpdate)
            {
                _jobs.AddOrUpdate(item.Id, value, (k, v) =>
                {
                    ret = false;
                    return value;
                });
            }
            else
            {
                if (!_jobs.TryAdd(item.Id, value))
                    throw new Exception($"DefaultTinyFxHostTimerService.Register时Id重复,可使用RegisterOrUpdate。Id:{item.Id}");
            }
            _changeCts.Cancel();
            return ret;
        }

        /// <summary>
        /// 注销timer任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Unregister(string id)
        {
            var ret = _jobs.TryRemove(id, out var job);
            if (ret)
                _changeCts.Cancel();
            return ret;
        }
        #endregion

        public async Task StartAsync(CancellationToken stoppingToken = default)
        {
            // 外层停止，通知内层停止
            stoppingToken.Register(() =>
            {
                _stoppingCts.Cancel();
            });
            LogUtil.Info("ITinyFxHostTimerService 启动");
            int interval = 0;
            while (!_stoppingCts.IsCancellationRequested)
            {
                int nextInterval = int.MaxValue;
                //var jobs = GetJobs().ToList().OrderBy(x => x.Timestamp);
                var jobs = GetJobs();
                foreach (var job in jobs)
                {
                    try
                    {
                        if (job.TryExecute(interval, _stoppingCts.Token, out Task task))
                        {
                            _tasks.Add(task);
                            var _ = task.ContinueWith(t =>
                            {
                                if (t.IsFaulted)
                                {
                                    // 超过重试次数移除!
                                    job.ErrorCount++;
                                    LogUtil.Error(t.Exception, "HostTimer任务执行异常: [{Id}]-{Title} Interval:{Interval} Count:{CurrentCount}/{ExecuteCount} Error:{ErrorCount}/{TryCount} ThreadId:{ThreadId}"
                                        , job.Id, job.Title, job.Interval, job.CurrentCount, job.ExecuteCount, job.ErrorCount, job.TryCount, Thread.CurrentThread.ManagedThreadId);
                                    if (job.TryCount == 0 || job.ErrorCount >= job.TryCount)
                                        _jobs.TryRemove(job.Id, out var _);
                                }
                                else
                                {
                                    LogUtil.Debug("HostTimer任务执行结束: [{Id}]-{Title} Interval:{Interval} Count:{CurrentCount}/{ExecuteCount} Error:{ErrorCount}/{TryCount} ThreadId:{ThreadId}"
                                        , job.Id, job.Title, job.Interval, job.CurrentCount, job.ExecuteCount, job.ErrorCount, job.TryCount, Thread.CurrentThread.ManagedThreadId);
                                    job.ErrorCount = 0;
                                }
                                //if (t.IsCompleted)
                                _tasks.Remove(t);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtil.Error(ex, $"DefaultTinyFxHostTimerService.StartAsync执行中出现异常，必须处理!!! id:{job.Id} title:{job.Title}");
                    }

                    if (job.IsCycleEnd)//循环结束
                    {
                        _jobs.TryRemove(job.Id, out var _);
                    }
                    else
                    {
                        nextInterval = Math.Min(nextInterval, job.Remain);
                    }
                }
                interval = Math.Max(MinDelayInterval, nextInterval);
                await Task.Delay(TimeSpan.FromMilliseconds(nextInterval), _changeCts.Token).ContinueWith((t) =>
                {
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
                foreach (var task in tasks)
                {
                    if (!task.IsCompletedSuccessfully)
                    {
                        LogUtil.Error(task.Exception, $"DefaultTinyFxHostTimerService.StopAsync时异常: status: {task.Status}");
                    }
                }
            }
            LogUtil.Info("ITinyFxHostTimerService 终止");
            return Task.CompletedTask;
        }

        #region Utils
        private IEnumerable<Task> GetTasks()
        {
            var enumtor = _tasks.GetEnumerator();
            while (enumtor.MoveNext())
            {
                yield return enumtor.Current;
            }
        }
        private IEnumerable<TinyFxHostTimerJob> GetJobs()
        {
            var enumtor = _jobs.GetEnumerator();
            while (enumtor.MoveNext())
            {
                yield return enumtor.Current.Value;
            }
        }
        #endregion
    }
}
