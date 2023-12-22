using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.IDGenerator.Common;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Net;
using static System.Collections.Specialized.BitVector32;
using TinyFx.Logging;

namespace TinyFx.IDGenerator
{
    public static class IDGeneratorUtil
    {
        internal static IWorkerIdProvider WorkerIdProvider { get; private set; }
        public static int DataCenterId { get; private set; }
        public static int MaxDataCenterId { get; private set; }
        public static int MaxWorkerId { get; private set; }
        /// <summary>
        /// 每毫秒最多产生的ID数
        /// </summary>
        public static int MaxSequence { get; private set; }
        private static bool _isInited = false;
        private static object _sync = new();

        public static int WorkerId => Generator.WorkerId;
        private static SnowflakeIdGenerator Generator;

        internal static void Init()
        {
            var section = ConfigUtil.GetSection<IDGeneratorSection>();
            if (section == null || !section.Enabled)
                throw new Exception("IDGeneratorUtil生成ID时，没有配置IDGeneratorSection或者Enabled=false");
            WorkerIdProvider = section.UseRedis
                ? new RedisWorkerIdProvider()
                : new ConfigWorkerIdProvider();

            DataCenterId = section.DataCenterId;
            MaxDataCenterId = 1 << section.DataCenterIdBits;
            if (DataCenterId > MaxDataCenterId)
                throw new Exception($"IDGenerator: DataCenterId大于允许范围. max: {MaxDataCenterId}");
            MaxWorkerId = 1 << section.WorkerIdBits;
            MaxSequence = 1 << (22 - section.DataCenterIdBits - section.WorkerIdBits);

            Generator = new SnowflakeIdGenerator(WorkerIdProvider);
            _isInited = true;
            LogUtil.Debug("IDGenerator 启动");
        }

        /// <summary>
        /// 生成雪花算法ID
        /// </summary>
        /// <returns></returns>
        public static long NextId()
        {
            if (!_isInited)
            {
                lock(_sync)
                {
                    if (!_isInited)
                    {
                        Init();
                    }
                }
            }
            return Generator.NextId();
        }

        public static async Task<int> GetNextWorkId()
        {
            return await WorkerIdProvider.GetNextWorkId();
        }
    }
}
