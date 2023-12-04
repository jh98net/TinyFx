﻿using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TinyFx.Configuration;

namespace TinyFx.DbCaching
{
    public class DbCachingSection : ConfigSection
    {
        public override string SectionName => "DbCaching";
        public bool Enabled { get; set; }
        /// <summary>
        /// 数据变更通知模式
        /// </summary>
        public DbCachingPublishMode PublishMode { get; set; } = DbCachingPublishMode.Redis;
        public string RedisConnectionStringName { get; set; }
        public string MQConnectionStringName { get; set; }
        public List<DbCachingRefleshTable> RefleshTables { get; set; }
    }

    public class DbCachingRefleshTable
    {
        public string ConfigId { get; set; }
        public string TableName { get; set; }
        /// <summary>
        /// 刷新间隔，分钟
        /// </summary>
        public int Interval { get; set; }

        [JsonIgnore]
        public long LastExecTime { get; set; }
    }

    /// <summary>
    /// 内存缓存更新通知模式
    /// </summary>
    public enum DbCachingPublishMode
    {
        Redis,
        MQ,
        All
    }
}
