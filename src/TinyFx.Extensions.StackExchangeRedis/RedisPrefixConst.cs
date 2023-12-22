﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Extensions.StackExchangeRedis
{
    public static class RedisPrefixConst
    {
        /// <summary>
        /// 主机服务注册前缀
        /// </summary>
        public const string TINYFX_HOSTS = "_TINYFX:HOSTS";

        /// <summary>
        /// 分布式锁
        /// </summary>
        public const string RED_LOCK = "_TINYFX:LOCK";
        /// <summary>
        /// 布隆过滤器
        /// </summary>
        public const string BLOOM_FILTER = "_TINYFX:BloomFilter";

        /// <summary>
        /// RabbitMQ 的 PubSub使用
        /// </summary>
        public const string MQ_SUB_QUEUE = "_TINYFX:MQSubQueue";
        /// <summary>
        /// TinyFx.Extensions.IDGenerator使用
        /// </summary>
        public const string ID_GENERATOR = "_TINYFX:IDGenerator";
        /// <summary>
        /// TinyFx.DbCaching.DbCacheDataDCache使用
        /// </summary>
        public const string DB_CACHING = "_TINYFX:DbCaching";
    }
}
