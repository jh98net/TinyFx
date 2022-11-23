/******************************************************
 * 此代码由代码生成器工具自动生成，请不要修改
 * TinyFx代码生成器核心库版本号：1.0.0.0
 * git: https://github.com/jh98net/TinyFx
 * 文档生成时间：2022-11-09 18: 12:12
 ******************************************************/
using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TinyFx;
using TinyFx.Data;
using MySql.Data.MySqlClient;
using TinyFx.Data.MySql;

namespace TinyFx.Demos.demo
{
	#region EO
	/// <summary>
	/// 系统级日志表
	/// 【表 s_log 的实体类】
	/// </summary>
	[DataContract]
	public class S_logEO : IRowMapper<S_logEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public S_logEO()
		{
			this.LevelNum = 0;
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
		private string _originalLogID;
		/// <summary>
		/// 【数据库中的原始主键 LogID 值的副本，用于主键值更新】
		/// </summary>
		public string OriginalLogID
		{
			get { return _originalLogID; }
			set { HasOriginal = true; _originalLogID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "LogID", LogID }, };
	    }
	    /// <summary>
	    /// 获取主键集合JSON格式
	    /// </summary>
	    /// <returns></returns>
	    public string GetPrimaryKeysJson() => SerializerUtil.SerializeJson(GetPrimaryKeys());
		#endregion // 主键原始值
		#region 所有字段
		/// <summary>
		/// 日志主键GUID
		/// 【主键 varchar(36)】
		/// </summary>
		[DataMember(Order = 1)]
		public string LogID { get; set; }
		/// <summary>
		/// 日志发生时间
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 2)]
		public DateTime Timestamp { get; set; }
		/// <summary>
		/// 日志等级(Verbose|Debug|Information|Warning|Error|Fatal)
		/// 【字段 varchar(15)】
		/// </summary>
		[DataMember(Order = 3)]
		public string Level { get; set; }
		/// <summary>
		/// 日志等级数字
		///              0-Verbose
		///              1-Debug
		///              2-Information
		///              3-Warning
		///              4-Error
		///              5-Fatal
		/// 【字段 tinyint】
		/// </summary>
		[DataMember(Order = 4)]
		public int LevelNum { get; set; }
		/// <summary>
		/// 消息模板
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 5)]
		public string Template { get; set; }
		/// <summary>
		/// 消息内容
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 6)]
		public string Message { get; set; }
		/// <summary>
		/// 异常
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 7)]
		public string Exception { get; set; }
		/// <summary>
		/// 属性
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 8)]
		public string Properties { get; set; }
		/// <summary>
		/// 程序标识
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 9)]
		public string ProjectID { get; set; }
		/// <summary>
		/// 环境
		/// 【字段 varchar(20)】
		/// </summary>
		[DataMember(Order = 10)]
		public string Environment { get; set; }
		/// <summary>
		/// 服务器IP
		/// 【字段 varchar(20)】
		/// </summary>
		[DataMember(Order = 11)]
		public string MachineIP { get; set; }
		/// <summary>
		/// 消息模板hash
		/// 【字段 bigint】
		/// </summary>
		[DataMember(Order = 12)]
		public long? TemplateHash { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public S_logEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static S_logEO MapDataReader(IDataReader reader)
		{
		    S_logEO ret = new S_logEO();
			ret.LogID = reader.ToString("LogID");
			ret.OriginalLogID = ret.LogID;
			ret.Timestamp = reader.ToDateTime("Timestamp");
			ret.Level = reader.ToString("Level");
			ret.LevelNum = reader.ToInt32("LevelNum");
			ret.Template = reader.ToString("Template");
			ret.Message = reader.ToString("Message");
			ret.Exception = reader.ToString("Exception");
			ret.Properties = reader.ToString("Properties");
			ret.ProjectID = reader.ToString("ProjectID");
			ret.Environment = reader.ToString("Environment");
			ret.MachineIP = reader.ToString("MachineIP");
			ret.TemplateHash = reader.ToInt64N("TemplateHash");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 系统级日志表
	/// 【表 s_log 的操作类】
	/// </summary>
	public class S_logMO : MySqlTableMO<S_logEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`s_log`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public S_logMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public S_logMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public S_logMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(S_logEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(S_logEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(S_logEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `s_log` (`LogID`, `Timestamp`, `Level`, `LevelNum`, `Template`, `Message`, `Exception`, `Properties`, `ProjectID`, `Environment`, `MachineIP`, `TemplateHash`) VALUE (@LogID, @Timestamp, @Level, @LevelNum, @Template, @Message, @Exception, @Properties, @ProjectID, @Environment, @MachineIP, @TemplateHash);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", item.LogID, MySqlDbType.VarChar),
				Database.CreateInParameter("@Timestamp", item.Timestamp, MySqlDbType.DateTime),
				Database.CreateInParameter("@Level", item.Level != null ? item.Level : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LevelNum", item.LevelNum, MySqlDbType.Byte),
				Database.CreateInParameter("@Template", item.Template != null ? item.Template : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Message", item.Message != null ? item.Message : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Exception", item.Exception != null ? item.Exception : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Properties", item.Properties != null ? item.Properties : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@ProjectID", item.ProjectID != null ? item.ProjectID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Environment", item.Environment != null ? item.Environment : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MachineIP", item.MachineIP != null ? item.MachineIP : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@TemplateHash", item.TemplateHash.HasValue ? item.TemplateHash.Value : (object)DBNull.Value, MySqlDbType.Int64),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(string logID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(logID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(string logID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(logID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(string logID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(S_logEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.LogID, tm_);
		}
		public async Task<int> RemoveAsync(S_logEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.LogID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByTimestamp
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "timestamp">日志发生时间</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByTimestamp(DateTime timestamp, TransactionManager tm_ = null)
		{
			RepairRemoveByTimestampData(timestamp, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTimestampAsync(DateTime timestamp, TransactionManager tm_ = null)
		{
			RepairRemoveByTimestampData(timestamp, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTimestampData(DateTime timestamp, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE `Timestamp` = @Timestamp";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Timestamp", timestamp, MySqlDbType.DateTime));
		}
		#endregion // RemoveByTimestamp
		#region RemoveByLevel
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "level">日志等级(Verbose|Debug|Information|Warning|Error|Fatal)</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByLevel(string level, TransactionManager tm_ = null)
		{
			RepairRemoveByLevelData(level, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByLevelAsync(string level, TransactionManager tm_ = null)
		{
			RepairRemoveByLevelData(level, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByLevelData(string level, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE " + (level != null ? "`Level` = @Level" : "`Level` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (level != null)
				paras_.Add(Database.CreateInParameter("@Level", level, MySqlDbType.VarChar));
		}
		#endregion // RemoveByLevel
		#region RemoveByLevelNum
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "levelNum">日志等级数字</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByLevelNum(int levelNum, TransactionManager tm_ = null)
		{
			RepairRemoveByLevelNumData(levelNum, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByLevelNumAsync(int levelNum, TransactionManager tm_ = null)
		{
			RepairRemoveByLevelNumData(levelNum, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByLevelNumData(int levelNum, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE `LevelNum` = @LevelNum";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@LevelNum", levelNum, MySqlDbType.Byte));
		}
		#endregion // RemoveByLevelNum
		#region RemoveByTemplate
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "template">消息模板</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByTemplate(string template, TransactionManager tm_ = null)
		{
			RepairRemoveByTemplateData(template, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTemplateAsync(string template, TransactionManager tm_ = null)
		{
			RepairRemoveByTemplateData(template, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTemplateData(string template, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE " + (template != null ? "`Template` = @Template" : "`Template` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (template != null)
				paras_.Add(Database.CreateInParameter("@Template", template, MySqlDbType.Text));
		}
		#endregion // RemoveByTemplate
		#region RemoveByMessage
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "message">消息内容</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByMessage(string message, TransactionManager tm_ = null)
		{
			RepairRemoveByMessageData(message, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByMessageAsync(string message, TransactionManager tm_ = null)
		{
			RepairRemoveByMessageData(message, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByMessageData(string message, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE " + (message != null ? "`Message` = @Message" : "`Message` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (message != null)
				paras_.Add(Database.CreateInParameter("@Message", message, MySqlDbType.Text));
		}
		#endregion // RemoveByMessage
		#region RemoveByException
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "exception">异常</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByException(string exception, TransactionManager tm_ = null)
		{
			RepairRemoveByExceptionData(exception, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByExceptionAsync(string exception, TransactionManager tm_ = null)
		{
			RepairRemoveByExceptionData(exception, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByExceptionData(string exception, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE " + (exception != null ? "`Exception` = @Exception" : "`Exception` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (exception != null)
				paras_.Add(Database.CreateInParameter("@Exception", exception, MySqlDbType.Text));
		}
		#endregion // RemoveByException
		#region RemoveByProperties
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "properties">属性</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByProperties(string properties, TransactionManager tm_ = null)
		{
			RepairRemoveByPropertiesData(properties, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPropertiesAsync(string properties, TransactionManager tm_ = null)
		{
			RepairRemoveByPropertiesData(properties, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByPropertiesData(string properties, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE " + (properties != null ? "`Properties` = @Properties" : "`Properties` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (properties != null)
				paras_.Add(Database.CreateInParameter("@Properties", properties, MySqlDbType.Text));
		}
		#endregion // RemoveByProperties
		#region RemoveByProjectID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "projectID">程序标识</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByProjectID(string projectID, TransactionManager tm_ = null)
		{
			RepairRemoveByProjectIDData(projectID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByProjectIDAsync(string projectID, TransactionManager tm_ = null)
		{
			RepairRemoveByProjectIDData(projectID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByProjectIDData(string projectID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE " + (projectID != null ? "`ProjectID` = @ProjectID" : "`ProjectID` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (projectID != null)
				paras_.Add(Database.CreateInParameter("@ProjectID", projectID, MySqlDbType.VarChar));
		}
		#endregion // RemoveByProjectID
		#region RemoveByEnvironment
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "environment">环境</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByEnvironment(string environment, TransactionManager tm_ = null)
		{
			RepairRemoveByEnvironmentData(environment, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByEnvironmentAsync(string environment, TransactionManager tm_ = null)
		{
			RepairRemoveByEnvironmentData(environment, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByEnvironmentData(string environment, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE " + (environment != null ? "`Environment` = @Environment" : "`Environment` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (environment != null)
				paras_.Add(Database.CreateInParameter("@Environment", environment, MySqlDbType.VarChar));
		}
		#endregion // RemoveByEnvironment
		#region RemoveByMachineIP
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "machineIP">服务器IP</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByMachineIP(string machineIP, TransactionManager tm_ = null)
		{
			RepairRemoveByMachineIPData(machineIP, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByMachineIPAsync(string machineIP, TransactionManager tm_ = null)
		{
			RepairRemoveByMachineIPData(machineIP, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByMachineIPData(string machineIP, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE " + (machineIP != null ? "`MachineIP` = @MachineIP" : "`MachineIP` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (machineIP != null)
				paras_.Add(Database.CreateInParameter("@MachineIP", machineIP, MySqlDbType.VarChar));
		}
		#endregion // RemoveByMachineIP
		#region RemoveByTemplateHash
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "templateHash">消息模板hash</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByTemplateHash(long? templateHash, TransactionManager tm_ = null)
		{
			RepairRemoveByTemplateHashData(templateHash.Value, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTemplateHashAsync(long? templateHash, TransactionManager tm_ = null)
		{
			RepairRemoveByTemplateHashData(templateHash.Value, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTemplateHashData(long? templateHash, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `s_log` WHERE " + (templateHash.HasValue ? "`TemplateHash` = @TemplateHash" : "`TemplateHash` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (templateHash.HasValue)
				paras_.Add(Database.CreateInParameter("@TemplateHash", templateHash.Value, MySqlDbType.Int64));
		}
		#endregion // RemoveByTemplateHash
		#endregion // RemoveByXXX
	    
		#region RemoveByFKOrUnique
		#endregion // RemoveByFKOrUnique
		#endregion //Remove
	    
		#region Put
		#region PutItem
		/// <summary>
		/// 更新实体到数据库
		/// </summary>
		/// <param name = "item">要更新的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(S_logEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(S_logEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(S_logEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `LogID` = @LogID, `Timestamp` = @Timestamp, `Level` = @Level, `LevelNum` = @LevelNum, `Template` = @Template, `Message` = @Message, `Exception` = @Exception, `Properties` = @Properties, `ProjectID` = @ProjectID, `Environment` = @Environment, `MachineIP` = @MachineIP, `TemplateHash` = @TemplateHash WHERE `LogID` = @LogID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", item.LogID, MySqlDbType.VarChar),
				Database.CreateInParameter("@Timestamp", item.Timestamp, MySqlDbType.DateTime),
				Database.CreateInParameter("@Level", item.Level != null ? item.Level : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LevelNum", item.LevelNum, MySqlDbType.Byte),
				Database.CreateInParameter("@Template", item.Template != null ? item.Template : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Message", item.Message != null ? item.Message : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Exception", item.Exception != null ? item.Exception : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Properties", item.Properties != null ? item.Properties : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@ProjectID", item.ProjectID != null ? item.ProjectID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Environment", item.Environment != null ? item.Environment : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MachineIP", item.MachineIP != null ? item.MachineIP : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@TemplateHash", item.TemplateHash.HasValue ? item.TemplateHash.Value : (object)DBNull.Value, MySqlDbType.Int64),
				Database.CreateInParameter("@LogID_Original", item.HasOriginal ? item.OriginalLogID : item.LogID, MySqlDbType.VarChar),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<S_logEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<S_logEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += await PutAsync(item, tm_);
			}
			return ret;
		}
		#endregion // PutItem
		
		#region PutByPK
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string logID, string set_, params object[] values_)
		{
			return Put(set_, "`LogID` = @LogID", ConcatValues(values_, logID));
		}
		public async Task<int> PutByPKAsync(string logID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`LogID` = @LogID", ConcatValues(values_, logID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string logID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`LogID` = @LogID", tm_, ConcatValues(values_, logID));
		}
		public async Task<int> PutByPKAsync(string logID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`LogID` = @LogID", tm_, ConcatValues(values_, logID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string logID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
	        };
			return Put(set_, "`LogID` = @LogID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(string logID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
	        };
			return await PutAsync(set_, "`LogID` = @LogID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutTimestamp
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// /// <param name = "timestamp">日志发生时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTimestampByPK(string logID, DateTime timestamp, TransactionManager tm_ = null)
		{
			RepairPutTimestampByPKData(logID, timestamp, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTimestampByPKAsync(string logID, DateTime timestamp, TransactionManager tm_ = null)
		{
			RepairPutTimestampByPKData(logID, timestamp, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTimestampByPKData(string logID, DateTime timestamp, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `Timestamp` = @Timestamp  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Timestamp", timestamp, MySqlDbType.DateTime),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "timestamp">日志发生时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTimestamp(DateTime timestamp, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Timestamp` = @Timestamp";
			var parameter_ = Database.CreateInParameter("@Timestamp", timestamp, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTimestampAsync(DateTime timestamp, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Timestamp` = @Timestamp";
			var parameter_ = Database.CreateInParameter("@Timestamp", timestamp, MySqlDbType.DateTime);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTimestamp
		#region PutLevel
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// /// <param name = "level">日志等级(Verbose|Debug|Information|Warning|Error|Fatal)</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLevelByPK(string logID, string level, TransactionManager tm_ = null)
		{
			RepairPutLevelByPKData(logID, level, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutLevelByPKAsync(string logID, string level, TransactionManager tm_ = null)
		{
			RepairPutLevelByPKData(logID, level, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutLevelByPKData(string logID, string level, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `Level` = @Level  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Level", level != null ? level : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "level">日志等级(Verbose|Debug|Information|Warning|Error|Fatal)</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLevel(string level, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Level` = @Level";
			var parameter_ = Database.CreateInParameter("@Level", level != null ? level : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutLevelAsync(string level, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Level` = @Level";
			var parameter_ = Database.CreateInParameter("@Level", level != null ? level : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutLevel
		#region PutLevelNum
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// /// <param name = "levelNum">日志等级数字</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLevelNumByPK(string logID, int levelNum, TransactionManager tm_ = null)
		{
			RepairPutLevelNumByPKData(logID, levelNum, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutLevelNumByPKAsync(string logID, int levelNum, TransactionManager tm_ = null)
		{
			RepairPutLevelNumByPKData(logID, levelNum, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutLevelNumByPKData(string logID, int levelNum, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `LevelNum` = @LevelNum  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LevelNum", levelNum, MySqlDbType.Byte),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "levelNum">日志等级数字</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLevelNum(int levelNum, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `LevelNum` = @LevelNum";
			var parameter_ = Database.CreateInParameter("@LevelNum", levelNum, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutLevelNumAsync(int levelNum, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `LevelNum` = @LevelNum";
			var parameter_ = Database.CreateInParameter("@LevelNum", levelNum, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutLevelNum
		#region PutTemplate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// /// <param name = "template">消息模板</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTemplateByPK(string logID, string template, TransactionManager tm_ = null)
		{
			RepairPutTemplateByPKData(logID, template, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTemplateByPKAsync(string logID, string template, TransactionManager tm_ = null)
		{
			RepairPutTemplateByPKData(logID, template, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTemplateByPKData(string logID, string template, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `Template` = @Template  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Template", template != null ? template : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "template">消息模板</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTemplate(string template, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Template` = @Template";
			var parameter_ = Database.CreateInParameter("@Template", template != null ? template : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTemplateAsync(string template, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Template` = @Template";
			var parameter_ = Database.CreateInParameter("@Template", template != null ? template : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTemplate
		#region PutMessage
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// /// <param name = "message">消息内容</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutMessageByPK(string logID, string message, TransactionManager tm_ = null)
		{
			RepairPutMessageByPKData(logID, message, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutMessageByPKAsync(string logID, string message, TransactionManager tm_ = null)
		{
			RepairPutMessageByPKData(logID, message, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutMessageByPKData(string logID, string message, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `Message` = @Message  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Message", message != null ? message : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "message">消息内容</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutMessage(string message, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Message` = @Message";
			var parameter_ = Database.CreateInParameter("@Message", message != null ? message : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutMessageAsync(string message, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Message` = @Message";
			var parameter_ = Database.CreateInParameter("@Message", message != null ? message : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutMessage
		#region PutException
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// /// <param name = "exception">异常</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutExceptionByPK(string logID, string exception, TransactionManager tm_ = null)
		{
			RepairPutExceptionByPKData(logID, exception, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutExceptionByPKAsync(string logID, string exception, TransactionManager tm_ = null)
		{
			RepairPutExceptionByPKData(logID, exception, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutExceptionByPKData(string logID, string exception, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `Exception` = @Exception  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Exception", exception != null ? exception : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "exception">异常</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutException(string exception, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Exception` = @Exception";
			var parameter_ = Database.CreateInParameter("@Exception", exception != null ? exception : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutExceptionAsync(string exception, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Exception` = @Exception";
			var parameter_ = Database.CreateInParameter("@Exception", exception != null ? exception : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutException
		#region PutProperties
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// /// <param name = "properties">属性</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPropertiesByPK(string logID, string properties, TransactionManager tm_ = null)
		{
			RepairPutPropertiesByPKData(logID, properties, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutPropertiesByPKAsync(string logID, string properties, TransactionManager tm_ = null)
		{
			RepairPutPropertiesByPKData(logID, properties, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutPropertiesByPKData(string logID, string properties, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `Properties` = @Properties  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Properties", properties != null ? properties : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "properties">属性</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutProperties(string properties, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Properties` = @Properties";
			var parameter_ = Database.CreateInParameter("@Properties", properties != null ? properties : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutPropertiesAsync(string properties, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Properties` = @Properties";
			var parameter_ = Database.CreateInParameter("@Properties", properties != null ? properties : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutProperties
		#region PutProjectID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// /// <param name = "projectID">程序标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutProjectIDByPK(string logID, string projectID, TransactionManager tm_ = null)
		{
			RepairPutProjectIDByPKData(logID, projectID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutProjectIDByPKAsync(string logID, string projectID, TransactionManager tm_ = null)
		{
			RepairPutProjectIDByPKData(logID, projectID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutProjectIDByPKData(string logID, string projectID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `ProjectID` = @ProjectID  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ProjectID", projectID != null ? projectID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "projectID">程序标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutProjectID(string projectID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `ProjectID` = @ProjectID";
			var parameter_ = Database.CreateInParameter("@ProjectID", projectID != null ? projectID : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutProjectIDAsync(string projectID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `ProjectID` = @ProjectID";
			var parameter_ = Database.CreateInParameter("@ProjectID", projectID != null ? projectID : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutProjectID
		#region PutEnvironment
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// /// <param name = "environment">环境</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEnvironmentByPK(string logID, string environment, TransactionManager tm_ = null)
		{
			RepairPutEnvironmentByPKData(logID, environment, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutEnvironmentByPKAsync(string logID, string environment, TransactionManager tm_ = null)
		{
			RepairPutEnvironmentByPKData(logID, environment, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutEnvironmentByPKData(string logID, string environment, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `Environment` = @Environment  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Environment", environment != null ? environment : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "environment">环境</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEnvironment(string environment, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Environment` = @Environment";
			var parameter_ = Database.CreateInParameter("@Environment", environment != null ? environment : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutEnvironmentAsync(string environment, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `Environment` = @Environment";
			var parameter_ = Database.CreateInParameter("@Environment", environment != null ? environment : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutEnvironment
		#region PutMachineIP
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// /// <param name = "machineIP">服务器IP</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutMachineIPByPK(string logID, string machineIP, TransactionManager tm_ = null)
		{
			RepairPutMachineIPByPKData(logID, machineIP, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutMachineIPByPKAsync(string logID, string machineIP, TransactionManager tm_ = null)
		{
			RepairPutMachineIPByPKData(logID, machineIP, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutMachineIPByPKData(string logID, string machineIP, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `MachineIP` = @MachineIP  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MachineIP", machineIP != null ? machineIP : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "machineIP">服务器IP</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutMachineIP(string machineIP, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `MachineIP` = @MachineIP";
			var parameter_ = Database.CreateInParameter("@MachineIP", machineIP != null ? machineIP : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutMachineIPAsync(string machineIP, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `MachineIP` = @MachineIP";
			var parameter_ = Database.CreateInParameter("@MachineIP", machineIP != null ? machineIP : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutMachineIP
		#region PutTemplateHash
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// /// <param name = "templateHash">消息模板hash</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTemplateHashByPK(string logID, long? templateHash, TransactionManager tm_ = null)
		{
			RepairPutTemplateHashByPKData(logID, templateHash, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTemplateHashByPKAsync(string logID, long? templateHash, TransactionManager tm_ = null)
		{
			RepairPutTemplateHashByPKData(logID, templateHash, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTemplateHashByPKData(string logID, long? templateHash, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `s_log` SET `TemplateHash` = @TemplateHash  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@TemplateHash", templateHash.HasValue ? templateHash.Value : (object)DBNull.Value, MySqlDbType.Int64),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "templateHash">消息模板hash</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTemplateHash(long? templateHash, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `TemplateHash` = @TemplateHash";
			var parameter_ = Database.CreateInParameter("@TemplateHash", templateHash.HasValue ? templateHash.Value : (object)DBNull.Value, MySqlDbType.Int64);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTemplateHashAsync(long? templateHash, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `s_log` SET `TemplateHash` = @TemplateHash";
			var parameter_ = Database.CreateInParameter("@TemplateHash", templateHash.HasValue ? templateHash.Value : (object)DBNull.Value, MySqlDbType.Int64);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTemplateHash
		#endregion // PutXXX
		#endregion // Put
	   
		#region Set
		
		/// <summary>
		/// 插入或者更新数据
		/// </summary>
		/// <param name = "item">要更新的实体对象</param>
		/// <param name="tm">事务管理对象</param>
		/// <return>true:插入操作；false:更新操作</return>
		public bool Set(S_logEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.LogID) == null)
			{
				Add(item, tm);
			}
			else
			{
				Put(item, tm);
				ret = false;
			}
			return ret;
		}
		public async Task<bool> SetAsync(S_logEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.LogID) == null)
			{
				await AddAsync(item, tm);
			}
			else
			{
				await PutAsync(item, tm);
				ret = false;
			}
			return ret;
		}
		
		#endregion // Set
		
		#region Get
		#region GetByPK
		/// <summary>
		/// 按 PK（主键） 查询
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public S_logEO GetByPK(string logID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(logID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<S_logEO> GetByPKAsync(string logID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(logID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		private void RepairGetByPKData(string logID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`LogID` = @LogID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 Timestamp（字段）
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public DateTime GetTimestampByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (DateTime)GetScalar("`Timestamp`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<DateTime> GetTimestampByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (DateTime)await GetScalarAsync("`Timestamp`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Level（字段）
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetLevelByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Level`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetLevelByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Level`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 LevelNum（字段）
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetLevelNumByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`LevelNum`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<int> GetLevelNumByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`LevelNum`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Template（字段）
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetTemplateByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Template`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetTemplateByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Template`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Message（字段）
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetMessageByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Message`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetMessageByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Message`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Exception（字段）
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetExceptionByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Exception`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetExceptionByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Exception`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Properties（字段）
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetPropertiesByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Properties`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetPropertiesByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Properties`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ProjectID（字段）
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetProjectIDByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`ProjectID`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetProjectIDByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`ProjectID`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Environment（字段）
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetEnvironmentByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Environment`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetEnvironmentByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Environment`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 MachineIP（字段）
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetMachineIPByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`MachineIP`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetMachineIPByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`MachineIP`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 TemplateHash（字段）
		/// </summary>
		/// /// <param name = "logID">日志主键GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public long? GetTemplateHashByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (long?)GetScalar("`TemplateHash`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<long?> GetTemplateHashByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (long?)await GetScalarAsync("`TemplateHash`", "`LogID` = @LogID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByTimestamp
		
		/// <summary>
		/// 按 Timestamp（字段） 查询
		/// </summary>
		/// /// <param name = "timestamp">日志发生时间</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTimestamp(DateTime timestamp)
		{
			return GetByTimestamp(timestamp, 0, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByTimestampAsync(DateTime timestamp)
		{
			return await GetByTimestampAsync(timestamp, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Timestamp（字段） 查询
		/// </summary>
		/// /// <param name = "timestamp">日志发生时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTimestamp(DateTime timestamp, TransactionManager tm_)
		{
			return GetByTimestamp(timestamp, 0, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByTimestampAsync(DateTime timestamp, TransactionManager tm_)
		{
			return await GetByTimestampAsync(timestamp, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Timestamp（字段） 查询
		/// </summary>
		/// /// <param name = "timestamp">日志发生时间</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTimestamp(DateTime timestamp, int top_)
		{
			return GetByTimestamp(timestamp, top_, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByTimestampAsync(DateTime timestamp, int top_)
		{
			return await GetByTimestampAsync(timestamp, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Timestamp（字段） 查询
		/// </summary>
		/// /// <param name = "timestamp">日志发生时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTimestamp(DateTime timestamp, int top_, TransactionManager tm_)
		{
			return GetByTimestamp(timestamp, top_, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByTimestampAsync(DateTime timestamp, int top_, TransactionManager tm_)
		{
			return await GetByTimestampAsync(timestamp, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Timestamp（字段） 查询
		/// </summary>
		/// /// <param name = "timestamp">日志发生时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTimestamp(DateTime timestamp, string sort_)
		{
			return GetByTimestamp(timestamp, 0, sort_, null);
		}
		public async Task<List<S_logEO>> GetByTimestampAsync(DateTime timestamp, string sort_)
		{
			return await GetByTimestampAsync(timestamp, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Timestamp（字段） 查询
		/// </summary>
		/// /// <param name = "timestamp">日志发生时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTimestamp(DateTime timestamp, string sort_, TransactionManager tm_)
		{
			return GetByTimestamp(timestamp, 0, sort_, tm_);
		}
		public async Task<List<S_logEO>> GetByTimestampAsync(DateTime timestamp, string sort_, TransactionManager tm_)
		{
			return await GetByTimestampAsync(timestamp, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Timestamp（字段） 查询
		/// </summary>
		/// /// <param name = "timestamp">日志发生时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTimestamp(DateTime timestamp, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Timestamp` = @Timestamp", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Timestamp", timestamp, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<List<S_logEO>> GetByTimestampAsync(DateTime timestamp, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Timestamp` = @Timestamp", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Timestamp", timestamp, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		#endregion // GetByTimestamp
		#region GetByLevel
		
		/// <summary>
		/// 按 Level（字段） 查询
		/// </summary>
		/// /// <param name = "level">日志等级(Verbose|Debug|Information|Warning|Error|Fatal)</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevel(string level)
		{
			return GetByLevel(level, 0, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByLevelAsync(string level)
		{
			return await GetByLevelAsync(level, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Level（字段） 查询
		/// </summary>
		/// /// <param name = "level">日志等级(Verbose|Debug|Information|Warning|Error|Fatal)</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevel(string level, TransactionManager tm_)
		{
			return GetByLevel(level, 0, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByLevelAsync(string level, TransactionManager tm_)
		{
			return await GetByLevelAsync(level, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Level（字段） 查询
		/// </summary>
		/// /// <param name = "level">日志等级(Verbose|Debug|Information|Warning|Error|Fatal)</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevel(string level, int top_)
		{
			return GetByLevel(level, top_, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByLevelAsync(string level, int top_)
		{
			return await GetByLevelAsync(level, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Level（字段） 查询
		/// </summary>
		/// /// <param name = "level">日志等级(Verbose|Debug|Information|Warning|Error|Fatal)</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevel(string level, int top_, TransactionManager tm_)
		{
			return GetByLevel(level, top_, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByLevelAsync(string level, int top_, TransactionManager tm_)
		{
			return await GetByLevelAsync(level, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Level（字段） 查询
		/// </summary>
		/// /// <param name = "level">日志等级(Verbose|Debug|Information|Warning|Error|Fatal)</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevel(string level, string sort_)
		{
			return GetByLevel(level, 0, sort_, null);
		}
		public async Task<List<S_logEO>> GetByLevelAsync(string level, string sort_)
		{
			return await GetByLevelAsync(level, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Level（字段） 查询
		/// </summary>
		/// /// <param name = "level">日志等级(Verbose|Debug|Information|Warning|Error|Fatal)</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevel(string level, string sort_, TransactionManager tm_)
		{
			return GetByLevel(level, 0, sort_, tm_);
		}
		public async Task<List<S_logEO>> GetByLevelAsync(string level, string sort_, TransactionManager tm_)
		{
			return await GetByLevelAsync(level, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Level（字段） 查询
		/// </summary>
		/// /// <param name = "level">日志等级(Verbose|Debug|Information|Warning|Error|Fatal)</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevel(string level, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(level != null ? "`Level` = @Level" : "`Level` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (level != null)
				paras_.Add(Database.CreateInParameter("@Level", level, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<List<S_logEO>> GetByLevelAsync(string level, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(level != null ? "`Level` = @Level" : "`Level` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (level != null)
				paras_.Add(Database.CreateInParameter("@Level", level, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		#endregion // GetByLevel
		#region GetByLevelNum
		
		/// <summary>
		/// 按 LevelNum（字段） 查询
		/// </summary>
		/// /// <param name = "levelNum">日志等级数字</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevelNum(int levelNum)
		{
			return GetByLevelNum(levelNum, 0, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByLevelNumAsync(int levelNum)
		{
			return await GetByLevelNumAsync(levelNum, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 LevelNum（字段） 查询
		/// </summary>
		/// /// <param name = "levelNum">日志等级数字</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevelNum(int levelNum, TransactionManager tm_)
		{
			return GetByLevelNum(levelNum, 0, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByLevelNumAsync(int levelNum, TransactionManager tm_)
		{
			return await GetByLevelNumAsync(levelNum, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 LevelNum（字段） 查询
		/// </summary>
		/// /// <param name = "levelNum">日志等级数字</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevelNum(int levelNum, int top_)
		{
			return GetByLevelNum(levelNum, top_, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByLevelNumAsync(int levelNum, int top_)
		{
			return await GetByLevelNumAsync(levelNum, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 LevelNum（字段） 查询
		/// </summary>
		/// /// <param name = "levelNum">日志等级数字</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevelNum(int levelNum, int top_, TransactionManager tm_)
		{
			return GetByLevelNum(levelNum, top_, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByLevelNumAsync(int levelNum, int top_, TransactionManager tm_)
		{
			return await GetByLevelNumAsync(levelNum, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 LevelNum（字段） 查询
		/// </summary>
		/// /// <param name = "levelNum">日志等级数字</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevelNum(int levelNum, string sort_)
		{
			return GetByLevelNum(levelNum, 0, sort_, null);
		}
		public async Task<List<S_logEO>> GetByLevelNumAsync(int levelNum, string sort_)
		{
			return await GetByLevelNumAsync(levelNum, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 LevelNum（字段） 查询
		/// </summary>
		/// /// <param name = "levelNum">日志等级数字</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevelNum(int levelNum, string sort_, TransactionManager tm_)
		{
			return GetByLevelNum(levelNum, 0, sort_, tm_);
		}
		public async Task<List<S_logEO>> GetByLevelNumAsync(int levelNum, string sort_, TransactionManager tm_)
		{
			return await GetByLevelNumAsync(levelNum, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 LevelNum（字段） 查询
		/// </summary>
		/// /// <param name = "levelNum">日志等级数字</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByLevelNum(int levelNum, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`LevelNum` = @LevelNum", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@LevelNum", levelNum, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<List<S_logEO>> GetByLevelNumAsync(int levelNum, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`LevelNum` = @LevelNum", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@LevelNum", levelNum, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		#endregion // GetByLevelNum
		#region GetByTemplate
		
		/// <summary>
		/// 按 Template（字段） 查询
		/// </summary>
		/// /// <param name = "template">消息模板</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplate(string template)
		{
			return GetByTemplate(template, 0, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByTemplateAsync(string template)
		{
			return await GetByTemplateAsync(template, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Template（字段） 查询
		/// </summary>
		/// /// <param name = "template">消息模板</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplate(string template, TransactionManager tm_)
		{
			return GetByTemplate(template, 0, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByTemplateAsync(string template, TransactionManager tm_)
		{
			return await GetByTemplateAsync(template, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Template（字段） 查询
		/// </summary>
		/// /// <param name = "template">消息模板</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplate(string template, int top_)
		{
			return GetByTemplate(template, top_, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByTemplateAsync(string template, int top_)
		{
			return await GetByTemplateAsync(template, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Template（字段） 查询
		/// </summary>
		/// /// <param name = "template">消息模板</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplate(string template, int top_, TransactionManager tm_)
		{
			return GetByTemplate(template, top_, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByTemplateAsync(string template, int top_, TransactionManager tm_)
		{
			return await GetByTemplateAsync(template, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Template（字段） 查询
		/// </summary>
		/// /// <param name = "template">消息模板</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplate(string template, string sort_)
		{
			return GetByTemplate(template, 0, sort_, null);
		}
		public async Task<List<S_logEO>> GetByTemplateAsync(string template, string sort_)
		{
			return await GetByTemplateAsync(template, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Template（字段） 查询
		/// </summary>
		/// /// <param name = "template">消息模板</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplate(string template, string sort_, TransactionManager tm_)
		{
			return GetByTemplate(template, 0, sort_, tm_);
		}
		public async Task<List<S_logEO>> GetByTemplateAsync(string template, string sort_, TransactionManager tm_)
		{
			return await GetByTemplateAsync(template, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Template（字段） 查询
		/// </summary>
		/// /// <param name = "template">消息模板</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplate(string template, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(template != null ? "`Template` = @Template" : "`Template` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (template != null)
				paras_.Add(Database.CreateInParameter("@Template", template, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<List<S_logEO>> GetByTemplateAsync(string template, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(template != null ? "`Template` = @Template" : "`Template` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (template != null)
				paras_.Add(Database.CreateInParameter("@Template", template, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		#endregion // GetByTemplate
		#region GetByMessage
		
		/// <summary>
		/// 按 Message（字段） 查询
		/// </summary>
		/// /// <param name = "message">消息内容</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMessage(string message)
		{
			return GetByMessage(message, 0, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByMessageAsync(string message)
		{
			return await GetByMessageAsync(message, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Message（字段） 查询
		/// </summary>
		/// /// <param name = "message">消息内容</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMessage(string message, TransactionManager tm_)
		{
			return GetByMessage(message, 0, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByMessageAsync(string message, TransactionManager tm_)
		{
			return await GetByMessageAsync(message, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Message（字段） 查询
		/// </summary>
		/// /// <param name = "message">消息内容</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMessage(string message, int top_)
		{
			return GetByMessage(message, top_, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByMessageAsync(string message, int top_)
		{
			return await GetByMessageAsync(message, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Message（字段） 查询
		/// </summary>
		/// /// <param name = "message">消息内容</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMessage(string message, int top_, TransactionManager tm_)
		{
			return GetByMessage(message, top_, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByMessageAsync(string message, int top_, TransactionManager tm_)
		{
			return await GetByMessageAsync(message, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Message（字段） 查询
		/// </summary>
		/// /// <param name = "message">消息内容</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMessage(string message, string sort_)
		{
			return GetByMessage(message, 0, sort_, null);
		}
		public async Task<List<S_logEO>> GetByMessageAsync(string message, string sort_)
		{
			return await GetByMessageAsync(message, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Message（字段） 查询
		/// </summary>
		/// /// <param name = "message">消息内容</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMessage(string message, string sort_, TransactionManager tm_)
		{
			return GetByMessage(message, 0, sort_, tm_);
		}
		public async Task<List<S_logEO>> GetByMessageAsync(string message, string sort_, TransactionManager tm_)
		{
			return await GetByMessageAsync(message, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Message（字段） 查询
		/// </summary>
		/// /// <param name = "message">消息内容</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMessage(string message, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(message != null ? "`Message` = @Message" : "`Message` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (message != null)
				paras_.Add(Database.CreateInParameter("@Message", message, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<List<S_logEO>> GetByMessageAsync(string message, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(message != null ? "`Message` = @Message" : "`Message` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (message != null)
				paras_.Add(Database.CreateInParameter("@Message", message, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		#endregion // GetByMessage
		#region GetByException
		
		/// <summary>
		/// 按 Exception（字段） 查询
		/// </summary>
		/// /// <param name = "exception">异常</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByException(string exception)
		{
			return GetByException(exception, 0, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByExceptionAsync(string exception)
		{
			return await GetByExceptionAsync(exception, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Exception（字段） 查询
		/// </summary>
		/// /// <param name = "exception">异常</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByException(string exception, TransactionManager tm_)
		{
			return GetByException(exception, 0, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByExceptionAsync(string exception, TransactionManager tm_)
		{
			return await GetByExceptionAsync(exception, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Exception（字段） 查询
		/// </summary>
		/// /// <param name = "exception">异常</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByException(string exception, int top_)
		{
			return GetByException(exception, top_, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByExceptionAsync(string exception, int top_)
		{
			return await GetByExceptionAsync(exception, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Exception（字段） 查询
		/// </summary>
		/// /// <param name = "exception">异常</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByException(string exception, int top_, TransactionManager tm_)
		{
			return GetByException(exception, top_, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByExceptionAsync(string exception, int top_, TransactionManager tm_)
		{
			return await GetByExceptionAsync(exception, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Exception（字段） 查询
		/// </summary>
		/// /// <param name = "exception">异常</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByException(string exception, string sort_)
		{
			return GetByException(exception, 0, sort_, null);
		}
		public async Task<List<S_logEO>> GetByExceptionAsync(string exception, string sort_)
		{
			return await GetByExceptionAsync(exception, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Exception（字段） 查询
		/// </summary>
		/// /// <param name = "exception">异常</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByException(string exception, string sort_, TransactionManager tm_)
		{
			return GetByException(exception, 0, sort_, tm_);
		}
		public async Task<List<S_logEO>> GetByExceptionAsync(string exception, string sort_, TransactionManager tm_)
		{
			return await GetByExceptionAsync(exception, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Exception（字段） 查询
		/// </summary>
		/// /// <param name = "exception">异常</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByException(string exception, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(exception != null ? "`Exception` = @Exception" : "`Exception` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (exception != null)
				paras_.Add(Database.CreateInParameter("@Exception", exception, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<List<S_logEO>> GetByExceptionAsync(string exception, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(exception != null ? "`Exception` = @Exception" : "`Exception` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (exception != null)
				paras_.Add(Database.CreateInParameter("@Exception", exception, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		#endregion // GetByException
		#region GetByProperties
		
		/// <summary>
		/// 按 Properties（字段） 查询
		/// </summary>
		/// /// <param name = "properties">属性</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProperties(string properties)
		{
			return GetByProperties(properties, 0, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByPropertiesAsync(string properties)
		{
			return await GetByPropertiesAsync(properties, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Properties（字段） 查询
		/// </summary>
		/// /// <param name = "properties">属性</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProperties(string properties, TransactionManager tm_)
		{
			return GetByProperties(properties, 0, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByPropertiesAsync(string properties, TransactionManager tm_)
		{
			return await GetByPropertiesAsync(properties, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Properties（字段） 查询
		/// </summary>
		/// /// <param name = "properties">属性</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProperties(string properties, int top_)
		{
			return GetByProperties(properties, top_, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByPropertiesAsync(string properties, int top_)
		{
			return await GetByPropertiesAsync(properties, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Properties（字段） 查询
		/// </summary>
		/// /// <param name = "properties">属性</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProperties(string properties, int top_, TransactionManager tm_)
		{
			return GetByProperties(properties, top_, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByPropertiesAsync(string properties, int top_, TransactionManager tm_)
		{
			return await GetByPropertiesAsync(properties, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Properties（字段） 查询
		/// </summary>
		/// /// <param name = "properties">属性</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProperties(string properties, string sort_)
		{
			return GetByProperties(properties, 0, sort_, null);
		}
		public async Task<List<S_logEO>> GetByPropertiesAsync(string properties, string sort_)
		{
			return await GetByPropertiesAsync(properties, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Properties（字段） 查询
		/// </summary>
		/// /// <param name = "properties">属性</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProperties(string properties, string sort_, TransactionManager tm_)
		{
			return GetByProperties(properties, 0, sort_, tm_);
		}
		public async Task<List<S_logEO>> GetByPropertiesAsync(string properties, string sort_, TransactionManager tm_)
		{
			return await GetByPropertiesAsync(properties, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Properties（字段） 查询
		/// </summary>
		/// /// <param name = "properties">属性</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProperties(string properties, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(properties != null ? "`Properties` = @Properties" : "`Properties` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (properties != null)
				paras_.Add(Database.CreateInParameter("@Properties", properties, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<List<S_logEO>> GetByPropertiesAsync(string properties, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(properties != null ? "`Properties` = @Properties" : "`Properties` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (properties != null)
				paras_.Add(Database.CreateInParameter("@Properties", properties, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		#endregion // GetByProperties
		#region GetByProjectID
		
		/// <summary>
		/// 按 ProjectID（字段） 查询
		/// </summary>
		/// /// <param name = "projectID">程序标识</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProjectID(string projectID)
		{
			return GetByProjectID(projectID, 0, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByProjectIDAsync(string projectID)
		{
			return await GetByProjectIDAsync(projectID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ProjectID（字段） 查询
		/// </summary>
		/// /// <param name = "projectID">程序标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProjectID(string projectID, TransactionManager tm_)
		{
			return GetByProjectID(projectID, 0, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByProjectIDAsync(string projectID, TransactionManager tm_)
		{
			return await GetByProjectIDAsync(projectID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ProjectID（字段） 查询
		/// </summary>
		/// /// <param name = "projectID">程序标识</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProjectID(string projectID, int top_)
		{
			return GetByProjectID(projectID, top_, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByProjectIDAsync(string projectID, int top_)
		{
			return await GetByProjectIDAsync(projectID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ProjectID（字段） 查询
		/// </summary>
		/// /// <param name = "projectID">程序标识</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProjectID(string projectID, int top_, TransactionManager tm_)
		{
			return GetByProjectID(projectID, top_, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByProjectIDAsync(string projectID, int top_, TransactionManager tm_)
		{
			return await GetByProjectIDAsync(projectID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ProjectID（字段） 查询
		/// </summary>
		/// /// <param name = "projectID">程序标识</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProjectID(string projectID, string sort_)
		{
			return GetByProjectID(projectID, 0, sort_, null);
		}
		public async Task<List<S_logEO>> GetByProjectIDAsync(string projectID, string sort_)
		{
			return await GetByProjectIDAsync(projectID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ProjectID（字段） 查询
		/// </summary>
		/// /// <param name = "projectID">程序标识</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProjectID(string projectID, string sort_, TransactionManager tm_)
		{
			return GetByProjectID(projectID, 0, sort_, tm_);
		}
		public async Task<List<S_logEO>> GetByProjectIDAsync(string projectID, string sort_, TransactionManager tm_)
		{
			return await GetByProjectIDAsync(projectID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ProjectID（字段） 查询
		/// </summary>
		/// /// <param name = "projectID">程序标识</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByProjectID(string projectID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(projectID != null ? "`ProjectID` = @ProjectID" : "`ProjectID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (projectID != null)
				paras_.Add(Database.CreateInParameter("@ProjectID", projectID, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<List<S_logEO>> GetByProjectIDAsync(string projectID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(projectID != null ? "`ProjectID` = @ProjectID" : "`ProjectID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (projectID != null)
				paras_.Add(Database.CreateInParameter("@ProjectID", projectID, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		#endregion // GetByProjectID
		#region GetByEnvironment
		
		/// <summary>
		/// 按 Environment（字段） 查询
		/// </summary>
		/// /// <param name = "environment">环境</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByEnvironment(string environment)
		{
			return GetByEnvironment(environment, 0, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByEnvironmentAsync(string environment)
		{
			return await GetByEnvironmentAsync(environment, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Environment（字段） 查询
		/// </summary>
		/// /// <param name = "environment">环境</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByEnvironment(string environment, TransactionManager tm_)
		{
			return GetByEnvironment(environment, 0, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByEnvironmentAsync(string environment, TransactionManager tm_)
		{
			return await GetByEnvironmentAsync(environment, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Environment（字段） 查询
		/// </summary>
		/// /// <param name = "environment">环境</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByEnvironment(string environment, int top_)
		{
			return GetByEnvironment(environment, top_, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByEnvironmentAsync(string environment, int top_)
		{
			return await GetByEnvironmentAsync(environment, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Environment（字段） 查询
		/// </summary>
		/// /// <param name = "environment">环境</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByEnvironment(string environment, int top_, TransactionManager tm_)
		{
			return GetByEnvironment(environment, top_, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByEnvironmentAsync(string environment, int top_, TransactionManager tm_)
		{
			return await GetByEnvironmentAsync(environment, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Environment（字段） 查询
		/// </summary>
		/// /// <param name = "environment">环境</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByEnvironment(string environment, string sort_)
		{
			return GetByEnvironment(environment, 0, sort_, null);
		}
		public async Task<List<S_logEO>> GetByEnvironmentAsync(string environment, string sort_)
		{
			return await GetByEnvironmentAsync(environment, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Environment（字段） 查询
		/// </summary>
		/// /// <param name = "environment">环境</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByEnvironment(string environment, string sort_, TransactionManager tm_)
		{
			return GetByEnvironment(environment, 0, sort_, tm_);
		}
		public async Task<List<S_logEO>> GetByEnvironmentAsync(string environment, string sort_, TransactionManager tm_)
		{
			return await GetByEnvironmentAsync(environment, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Environment（字段） 查询
		/// </summary>
		/// /// <param name = "environment">环境</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByEnvironment(string environment, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(environment != null ? "`Environment` = @Environment" : "`Environment` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (environment != null)
				paras_.Add(Database.CreateInParameter("@Environment", environment, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<List<S_logEO>> GetByEnvironmentAsync(string environment, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(environment != null ? "`Environment` = @Environment" : "`Environment` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (environment != null)
				paras_.Add(Database.CreateInParameter("@Environment", environment, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		#endregion // GetByEnvironment
		#region GetByMachineIP
		
		/// <summary>
		/// 按 MachineIP（字段） 查询
		/// </summary>
		/// /// <param name = "machineIP">服务器IP</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMachineIP(string machineIP)
		{
			return GetByMachineIP(machineIP, 0, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByMachineIPAsync(string machineIP)
		{
			return await GetByMachineIPAsync(machineIP, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 MachineIP（字段） 查询
		/// </summary>
		/// /// <param name = "machineIP">服务器IP</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMachineIP(string machineIP, TransactionManager tm_)
		{
			return GetByMachineIP(machineIP, 0, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByMachineIPAsync(string machineIP, TransactionManager tm_)
		{
			return await GetByMachineIPAsync(machineIP, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 MachineIP（字段） 查询
		/// </summary>
		/// /// <param name = "machineIP">服务器IP</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMachineIP(string machineIP, int top_)
		{
			return GetByMachineIP(machineIP, top_, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByMachineIPAsync(string machineIP, int top_)
		{
			return await GetByMachineIPAsync(machineIP, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 MachineIP（字段） 查询
		/// </summary>
		/// /// <param name = "machineIP">服务器IP</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMachineIP(string machineIP, int top_, TransactionManager tm_)
		{
			return GetByMachineIP(machineIP, top_, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByMachineIPAsync(string machineIP, int top_, TransactionManager tm_)
		{
			return await GetByMachineIPAsync(machineIP, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 MachineIP（字段） 查询
		/// </summary>
		/// /// <param name = "machineIP">服务器IP</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMachineIP(string machineIP, string sort_)
		{
			return GetByMachineIP(machineIP, 0, sort_, null);
		}
		public async Task<List<S_logEO>> GetByMachineIPAsync(string machineIP, string sort_)
		{
			return await GetByMachineIPAsync(machineIP, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 MachineIP（字段） 查询
		/// </summary>
		/// /// <param name = "machineIP">服务器IP</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMachineIP(string machineIP, string sort_, TransactionManager tm_)
		{
			return GetByMachineIP(machineIP, 0, sort_, tm_);
		}
		public async Task<List<S_logEO>> GetByMachineIPAsync(string machineIP, string sort_, TransactionManager tm_)
		{
			return await GetByMachineIPAsync(machineIP, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 MachineIP（字段） 查询
		/// </summary>
		/// /// <param name = "machineIP">服务器IP</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByMachineIP(string machineIP, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(machineIP != null ? "`MachineIP` = @MachineIP" : "`MachineIP` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (machineIP != null)
				paras_.Add(Database.CreateInParameter("@MachineIP", machineIP, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<List<S_logEO>> GetByMachineIPAsync(string machineIP, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(machineIP != null ? "`MachineIP` = @MachineIP" : "`MachineIP` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (machineIP != null)
				paras_.Add(Database.CreateInParameter("@MachineIP", machineIP, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		#endregion // GetByMachineIP
		#region GetByTemplateHash
		
		/// <summary>
		/// 按 TemplateHash（字段） 查询
		/// </summary>
		/// /// <param name = "templateHash">消息模板hash</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplateHash(long? templateHash)
		{
			return GetByTemplateHash(templateHash, 0, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByTemplateHashAsync(long? templateHash)
		{
			return await GetByTemplateHashAsync(templateHash, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 TemplateHash（字段） 查询
		/// </summary>
		/// /// <param name = "templateHash">消息模板hash</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplateHash(long? templateHash, TransactionManager tm_)
		{
			return GetByTemplateHash(templateHash, 0, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByTemplateHashAsync(long? templateHash, TransactionManager tm_)
		{
			return await GetByTemplateHashAsync(templateHash, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 TemplateHash（字段） 查询
		/// </summary>
		/// /// <param name = "templateHash">消息模板hash</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplateHash(long? templateHash, int top_)
		{
			return GetByTemplateHash(templateHash, top_, string.Empty, null);
		}
		public async Task<List<S_logEO>> GetByTemplateHashAsync(long? templateHash, int top_)
		{
			return await GetByTemplateHashAsync(templateHash, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 TemplateHash（字段） 查询
		/// </summary>
		/// /// <param name = "templateHash">消息模板hash</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplateHash(long? templateHash, int top_, TransactionManager tm_)
		{
			return GetByTemplateHash(templateHash, top_, string.Empty, tm_);
		}
		public async Task<List<S_logEO>> GetByTemplateHashAsync(long? templateHash, int top_, TransactionManager tm_)
		{
			return await GetByTemplateHashAsync(templateHash, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 TemplateHash（字段） 查询
		/// </summary>
		/// /// <param name = "templateHash">消息模板hash</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplateHash(long? templateHash, string sort_)
		{
			return GetByTemplateHash(templateHash, 0, sort_, null);
		}
		public async Task<List<S_logEO>> GetByTemplateHashAsync(long? templateHash, string sort_)
		{
			return await GetByTemplateHashAsync(templateHash, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 TemplateHash（字段） 查询
		/// </summary>
		/// /// <param name = "templateHash">消息模板hash</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplateHash(long? templateHash, string sort_, TransactionManager tm_)
		{
			return GetByTemplateHash(templateHash, 0, sort_, tm_);
		}
		public async Task<List<S_logEO>> GetByTemplateHashAsync(long? templateHash, string sort_, TransactionManager tm_)
		{
			return await GetByTemplateHashAsync(templateHash, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 TemplateHash（字段） 查询
		/// </summary>
		/// /// <param name = "templateHash">消息模板hash</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<S_logEO> GetByTemplateHash(long? templateHash, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(templateHash.HasValue ? "`TemplateHash` = @TemplateHash" : "`TemplateHash` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (templateHash.HasValue)
				paras_.Add(Database.CreateInParameter("@TemplateHash", templateHash.Value, MySqlDbType.Int64));
			return Database.ExecSqlList(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		public async Task<List<S_logEO>> GetByTemplateHashAsync(long? templateHash, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(templateHash.HasValue ? "`TemplateHash` = @TemplateHash" : "`TemplateHash` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (templateHash.HasValue)
				paras_.Add(Database.CreateInParameter("@TemplateHash", templateHash.Value, MySqlDbType.Int64));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, S_logEO.MapDataReader);
		}
		#endregion // GetByTemplateHash
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
