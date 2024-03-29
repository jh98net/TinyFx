﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SL = Serilog;
using Serilog.Core;
using Serilog.Events;
using TinyFx.Configuration;
using Serilog;
using TinyFx.Logging;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;

namespace TinyFx.Extensions.Serilog
{
    /// <summary>
    /// Serilog辅助类
    /// </summary>
    public static class SerilogUtil
    {
        public const string ProjectIdPropertyName = "ProjectId";
        public const string EnvironmentNamePropertyName = "EnvironmentName";
        public const string MachineIPPropertyName = "MachineIP";
        public const string IndexNamePropertyName = "IndexName";
        //public const string MachineNamePropertyName = "MachineName";
        //public const string ThreadIdPropertyName = "ThreadId";
        //public const string ThreadNamePropertyName = "ThreadName";
        /*
                /// <summary>
                /// 创建保存Log的数据库表
                /// </summary>
                /// <param name="connectionString"></param>
                /// <param name="tableName"></param>
                /// <returns></returns>
                public static bool CreateLogTable(string connectionString, string tableName = "s_log")
                {
                    var sql = $@"create table {tableName}
        (
           LogID                varchar(36) not null  comment '日志主键GUID',
           Timestamp            datetime not null  comment '日志发生时间',
           Level                varchar(15)  comment '日志等级(Verbose|Debug|Information|Warning|Error|Fatal)',
           LevelNum             tinyint not null default 0  comment '日志等级数字
                     0-Verbose
                     1-Debug
                     2-Information
                     3-Warning
                     4-Error
                     5-Fatal',
           Template             text  comment '消息模板',
           Message              text  comment '消息内容',
           Exception            text  comment '异常',
           Properties           text  comment '属性',
           ProjectID            varchar(100)  comment '程序标识',
           Environment          varchar(20)  comment '环境',
           MachineIP            varchar(20)  comment '服务器IP',
           TemplateHash         bigint  comment '消息模板hash',
           primary key (LogID)
        );
        alter table {tableName} comment '系统日志';
        create index index_1 on {tableName}
        (
           Timestamp,
           Level
        );
        create index Index_2 on {tableName}
        (
           Timestamp,
           LevelNum
        );
        ";
                    var db = new MySqlDatabase(connectionString, 30);
                    var tb = db.ExecSqlScalar<string>($"SELECT table_name FROM information_schema.TABLES WHERE table_schema='{db.ConnectionStringInfo.Database}' and table_name ='{tableName}';");
                    var ret = string.IsNullOrEmpty(tb);
                    if (ret)
                    {
                        db.ExecSqlNonQuery(sql);
                    }
                    return ret;
                }*/
    }

}
