using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TinyFx.Common
{
    public class TimerWork
    {
        public string WorkId { get; set; }
        private int _interval;
        /// <summary>
        /// 执行间隔
        /// </summary>
        public int Interval
        {
            get => _interval;
            set
            {
                _interval = value;
                _remain = value;
            }
        }
        private int _remain;
        /// <summary>
        /// 下次执行剩余毫秒数
        /// </summary>
        public int Remain => _remain;
        /// <summary>
        /// 是否应该立刻执行
        /// </summary>
        public bool CanExecute => Remain <= 0;
        private int _cycleCount = 0;
        /// <summary>
        /// 循环次数
        /// </summary>
        public int CycleCount { get; set; }
        /// <summary>
        /// 是否循环完毕（移除Work）
        /// </summary>
        public bool IsCycleEnd => CycleCount > 0 && CycleCount <= _cycleCount;
        /// <summary>
        /// 执行任务
        /// </summary>
        public Func<CancellationToken, Task> Action { get; set; }
        protected CancellationToken StoppingToken { get; set; }
        internal void SetStoppingToken(CancellationToken stoppingToken)
            => StoppingToken = stoppingToken;
        public bool TryExecute(int processInterval, out Task task)
        {
            task = null;
            Interlocked.Add(ref _remain, -processInterval);
            if (!CanExecute)
                return false;
            Interlocked.Exchange(ref _remain, Interval);
            _cycleCount++;
            task = Action(StoppingToken);
            return true;
        }
    }
}
