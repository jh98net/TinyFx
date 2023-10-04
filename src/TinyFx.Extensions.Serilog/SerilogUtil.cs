using System;
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
using TinyFx.Data.MySql;

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

        /// <summary>
        /// 特殊情况下的日志：./logs/ext.log
        /// 如当应用程序启动时或者写入Elasticsearch失败时
        /// </summary>
        public static Logger ExtLogger { get; private set; }
        public const string CommonMessageTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {NewLine}{Exception}";
        static SerilogUtil()
        {
            ExtLogger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("./logs/ext.log"
                        , restrictedToMinimumLevel: LogEventLevel.Information
                        , rollingInterval: RollingInterval.Day
                        , retainedFileCountLimit: 31)
                .CreateLogger();
        }

        /// <summary>
        /// 应用程序启动时创建日志并启动应用程序
        /// </summary>
        /// <param name="actioin"></param>
        /// <returns></returns>
        public static int RunDefaultApplication(Action actioin)
        {
            string projectId = "未知程序";
            try
            {
                projectId = ConfigUtil.Project?.ProjectId;
                if (string.IsNullOrEmpty(projectId))
                    projectId = "未知程序";
                ExtLogger.Information($"正在启动 [{projectId}] 程序...");
                actioin();
                return 0;
            }
            catch (Exception ex)
            {
                ExtLogger.Fatal(ex, $"程序 [{projectId}] 意外终止!");
                return 1;
            }
            finally
            {
                ExtLogger.Information($"程序 [{projectId}] 已停止。");
                //ExtLogger.Dispose();
            }
        }
        private static IConfigurationSection _customSection;
        /// <summary>
        /// 获取Serilog中的自定义配置。Serilog:Custom:name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetCustomProperty<T>(string name, T defaultValue = default(T))
        {
            if (_customSection == null)
                _customSection = ConfigUtil.Configuration.GetSection("Serilog:Custom");
            var ret = _customSection?[name];
            return !string.IsNullOrEmpty(ret)
                ? ret.To<T>() : defaultValue;
        }

        public static bool ExistSection
            => ConfigUtil.Configuration.GetSection("Serilog") != null;

        public static void Register()
        {
            var providers = new LoggerProviderCollection();
            var config = new LoggerConfiguration();
            if (ExistSection)
            {
                config.ReadFrom.Configuration(ConfigUtil.Configuration);
            }
            else
            {
                config.WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Verbose)
                    .WriteTo.File("./logs/log.txt", restrictedToMinimumLevel: LogEventLevel.Verbose
                        , rollingInterval: RollingInterval.Day, retainedFileCountLimit: 31
                        , rollOnFileSizeLimit: true, shared: true, buffered: false);
            }

            Log.Logger = config.WriteTo.Providers(providers).CreateLogger();
            var factory = new SerilogLoggerFactory(null, true, providers);
            DIUtil.AddSingleton<ILoggerFactory>(factory);
            LogUtil.Rebuild();
        }

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
        }
    }
}
