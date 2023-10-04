using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using static System.Collections.Specialized.BitVector32;
using TinyFx.Extensions.IDGenerator.Caching;

namespace TinyFx.Extensions.IDGenerator.Common
{
    internal class SnowflakeIdGenerator
    {
        private readonly object _async = new object();
        private IDGeneratorSection _section;
        private IWorkerIdProvider _provider;
        public static readonly DateTime DefaultEpoch = new(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly DefaultTimeSource _timeSource = new DefaultTimeSource(DefaultEpoch, TimeSpan.FromMilliseconds(1));

        private byte _timestampBits = 41;
        private byte _dataCenterIdBits;
        private byte _workerIdBits;
        private byte _sequenceBits;

        private int _maxDataCenterId;
        private readonly int _maxWorkerId;
        private int _maxSequence;

        private readonly int _shiftWorkerId;
        private readonly int _shiftDataCenterId;
        private readonly int _shiftTimestamp;

        private readonly long MASK_TIME;
        private readonly long MASK_SEQUENCE;
        private readonly long _dataCenterIdVal;

        private long _workerId;
        public int WorkerId => (int)_workerId;
        private int _sequence = 0;
        private long _lastgen = -1L;

        public SnowflakeIdGenerator(IWorkerIdProvider provider)
        {
            _section = ConfigUtil.GetSection<IDGeneratorSection>();
            _provider = provider;

            _dataCenterIdBits = _section.DataCenterIdBits;
            _workerIdBits = _section.WorkerIdBits;
            _sequenceBits = (byte)(63 - _timestampBits - _dataCenterIdBits - _workerIdBits);
            if (_sequenceBits < 1)
                throw new Exception("TimestampBits(41)+ DataCenterBits(3) + WorkerIdBits(10) + SequenceBits(9) 不能大于63");

            _maxDataCenterId = 1 << _dataCenterIdBits;
            _maxWorkerId = 1 << _workerIdBits;
            _maxSequence = 1 << _sequenceBits;

            _shiftWorkerId = _sequenceBits;
            _shiftDataCenterId = _sequenceBits + _workerIdBits;
            _shiftTimestamp = _sequenceBits + _workerIdBits + _dataCenterIdBits;
            _dataCenterIdVal = _section.DataCenterId << _shiftDataCenterId;

            MASK_TIME = GetMask(_timestampBits);
            MASK_SEQUENCE = GetMask(_sequenceBits);

            _workerId =  _provider.GetNextWorkId().ConfigureAwait(false).GetAwaiter().GetResult();
        }
        public long NextId()
        {
            lock (_async)
            {
                var ticks = GetTicks();
                var timestamp = ticks & MASK_TIME;
                if (timestamp < _lastgen || ticks < 0)
                {
                    //发生时钟回拨，切换workId，可解决。
                    _workerId = _provider.GetNextWorkId().ConfigureAwait(false).GetAwaiter().GetResult();
                    return NextId();
                }

                if (timestamp == _lastgen)
                {
                    if (_sequence >= MASK_SEQUENCE)
                    {
                        SpinWait.SpinUntil(() => _lastgen != GetTicks());
                        return NextId();
                    }
                    _sequence++;
                }
                else
                {
                    _sequence = 0;
                    _lastgen = timestamp;
                }
                return (timestamp << _shiftTimestamp) 
                    | (_dataCenterIdVal) 
                    | (_workerId << _shiftWorkerId) 
                    | (long)_sequence;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long GetTicks()
        {
            return _timeSource.GetTicks();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long GetMask(byte bits) => (1L << bits) - 1;
    }
}
