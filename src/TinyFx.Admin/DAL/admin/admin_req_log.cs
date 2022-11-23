/******************************************************
 * 此代码由代码生成器工具自动生成，请不要修改
 * TinyFx代码生成器核心库版本号：1.0.0.0
 * git: https://github.com/jh98net/TinyFx
 * 文档生成时间：2022-11-13 22: 11:29
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

namespace TinyFx.Admin.DAL
{
	#region EO
	/// <summary>
	/// 登录请求日志
	/// 【表 admin_req_log 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_req_logEO : IRowMapper<Admin_req_logEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_req_logEO()
		{
			this.RecDate = DateTime.Now;
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
		/// 编码GUID
		/// 【主键 varchar(40)】
		/// </summary>
		[DataMember(Order = 1)]
		public string LogID { get; set; }
		/// <summary>
		/// 管理用户ID
		/// 【字段 varchar(40)】
		/// </summary>
		[DataMember(Order = 2)]
		public string UserID { get; set; }
		/// <summary>
		/// 类型0-登录1-请求
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 3)]
		public int Type { get; set; }
		/// <summary>
		/// 结果
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 4)]
		public string Result { get; set; }
		/// <summary>
		/// 请求地址
		/// 【字段 varchar(255)】
		/// </summary>
		[DataMember(Order = 5)]
		public string RequestUrl { get; set; }
		/// <summary>
		/// IP地址
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 6)]
		public string IP { get; set; }
		/// <summary>
		/// 系统
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 7)]
		public string OS { get; set; }
		/// <summary>
		/// 浏览器
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 8)]
		public string Browser { get; set; }
		/// <summary>
		/// 地址
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 9)]
		public string Location { get; set; }
		/// <summary>
		/// 其他
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 10)]
		public string UserAgent { get; set; }
		/// <summary>
		/// 记录日期
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 11)]
		public DateTime RecDate { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_req_logEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_req_logEO MapDataReader(IDataReader reader)
		{
		    Admin_req_logEO ret = new Admin_req_logEO();
			ret.LogID = reader.ToString("LogID");
			ret.OriginalLogID = ret.LogID;
			ret.UserID = reader.ToString("UserID");
			ret.Type = reader.ToInt32("Type");
			ret.Result = reader.ToString("Result");
			ret.RequestUrl = reader.ToString("RequestUrl");
			ret.IP = reader.ToString("IP");
			ret.OS = reader.ToString("OS");
			ret.Browser = reader.ToString("Browser");
			ret.Location = reader.ToString("Location");
			ret.UserAgent = reader.ToString("UserAgent");
			ret.RecDate = reader.ToDateTime("RecDate");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 登录请求日志
	/// 【表 admin_req_log 的操作类】
	/// </summary>
	public class Admin_req_logMO : MySqlTableMO<Admin_req_logEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_req_log`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_req_logMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_req_logMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_req_logMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_req_logEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_req_logEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_req_logEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_req_log` (`LogID`, `UserID`, `Type`, `Result`, `RequestUrl`, `IP`, `OS`, `Browser`, `Location`, `UserAgent`, `RecDate`) VALUE (@LogID, @UserID, @Type, @Result, @RequestUrl, @IP, @OS, @Browser, @Location, @UserAgent, @RecDate);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", item.LogID, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", item.UserID != null ? item.UserID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Type", item.Type, MySqlDbType.Int32),
				Database.CreateInParameter("@Result", item.Result != null ? item.Result : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RequestUrl", item.RequestUrl != null ? item.RequestUrl : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@IP", item.IP != null ? item.IP : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@OS", item.OS != null ? item.OS : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Browser", item.Browser != null ? item.Browser : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Location", item.Location != null ? item.Location : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserAgent", item.UserAgent != null ? item.UserAgent : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@RecDate", item.RecDate, MySqlDbType.DateTime),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
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
			sql_ = @"DELETE FROM `admin_req_log` WHERE `LogID` = @LogID";
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
		public int Remove(Admin_req_logEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.LogID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_req_logEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.LogID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByUserID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByUserID(string userID, TransactionManager tm_ = null)
		{
			RepairRemoveByUserIDData(userID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByUserIDAsync(string userID, TransactionManager tm_ = null)
		{
			RepairRemoveByUserIDData(userID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByUserIDData(string userID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_req_log` WHERE " + (userID != null ? "`UserID` = @UserID" : "`UserID` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (userID != null)
				paras_.Add(Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar));
		}
		#endregion // RemoveByUserID
		#region RemoveByType
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "type">类型0-登录1-请求</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByType(int type, TransactionManager tm_ = null)
		{
			RepairRemoveByTypeData(type, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTypeAsync(int type, TransactionManager tm_ = null)
		{
			RepairRemoveByTypeData(type, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTypeData(int type, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_req_log` WHERE `Type` = @Type";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Type", type, MySqlDbType.Int32));
		}
		#endregion // RemoveByType
		#region RemoveByResult
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "result">结果</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByResult(string result, TransactionManager tm_ = null)
		{
			RepairRemoveByResultData(result, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByResultAsync(string result, TransactionManager tm_ = null)
		{
			RepairRemoveByResultData(result, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByResultData(string result, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_req_log` WHERE " + (result != null ? "`Result` = @Result" : "`Result` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (result != null)
				paras_.Add(Database.CreateInParameter("@Result", result, MySqlDbType.VarChar));
		}
		#endregion // RemoveByResult
		#region RemoveByRequestUrl
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "requestUrl">请求地址</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByRequestUrl(string requestUrl, TransactionManager tm_ = null)
		{
			RepairRemoveByRequestUrlData(requestUrl, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByRequestUrlAsync(string requestUrl, TransactionManager tm_ = null)
		{
			RepairRemoveByRequestUrlData(requestUrl, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByRequestUrlData(string requestUrl, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_req_log` WHERE " + (requestUrl != null ? "`RequestUrl` = @RequestUrl" : "`RequestUrl` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (requestUrl != null)
				paras_.Add(Database.CreateInParameter("@RequestUrl", requestUrl, MySqlDbType.VarChar));
		}
		#endregion // RemoveByRequestUrl
		#region RemoveByIP
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "iP">IP地址</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByIP(string iP, TransactionManager tm_ = null)
		{
			RepairRemoveByIPData(iP, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByIPAsync(string iP, TransactionManager tm_ = null)
		{
			RepairRemoveByIPData(iP, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByIPData(string iP, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_req_log` WHERE " + (iP != null ? "`IP` = @IP" : "`IP` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (iP != null)
				paras_.Add(Database.CreateInParameter("@IP", iP, MySqlDbType.VarChar));
		}
		#endregion // RemoveByIP
		#region RemoveByOS
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "oS">系统</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByOS(string oS, TransactionManager tm_ = null)
		{
			RepairRemoveByOSData(oS, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByOSAsync(string oS, TransactionManager tm_ = null)
		{
			RepairRemoveByOSData(oS, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByOSData(string oS, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_req_log` WHERE " + (oS != null ? "`OS` = @OS" : "`OS` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (oS != null)
				paras_.Add(Database.CreateInParameter("@OS", oS, MySqlDbType.VarChar));
		}
		#endregion // RemoveByOS
		#region RemoveByBrowser
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "browser">浏览器</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByBrowser(string browser, TransactionManager tm_ = null)
		{
			RepairRemoveByBrowserData(browser, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByBrowserAsync(string browser, TransactionManager tm_ = null)
		{
			RepairRemoveByBrowserData(browser, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByBrowserData(string browser, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_req_log` WHERE " + (browser != null ? "`Browser` = @Browser" : "`Browser` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (browser != null)
				paras_.Add(Database.CreateInParameter("@Browser", browser, MySqlDbType.VarChar));
		}
		#endregion // RemoveByBrowser
		#region RemoveByLocation
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "location">地址</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByLocation(string location, TransactionManager tm_ = null)
		{
			RepairRemoveByLocationData(location, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByLocationAsync(string location, TransactionManager tm_ = null)
		{
			RepairRemoveByLocationData(location, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByLocationData(string location, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_req_log` WHERE " + (location != null ? "`Location` = @Location" : "`Location` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (location != null)
				paras_.Add(Database.CreateInParameter("@Location", location, MySqlDbType.VarChar));
		}
		#endregion // RemoveByLocation
		#region RemoveByUserAgent
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "userAgent">其他</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByUserAgent(string userAgent, TransactionManager tm_ = null)
		{
			RepairRemoveByUserAgentData(userAgent, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByUserAgentAsync(string userAgent, TransactionManager tm_ = null)
		{
			RepairRemoveByUserAgentData(userAgent, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByUserAgentData(string userAgent, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_req_log` WHERE " + (userAgent != null ? "`UserAgent` = @UserAgent" : "`UserAgent` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (userAgent != null)
				paras_.Add(Database.CreateInParameter("@UserAgent", userAgent, MySqlDbType.Text));
		}
		#endregion // RemoveByUserAgent
		#region RemoveByRecDate
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByRecDate(DateTime recDate, TransactionManager tm_ = null)
		{
			RepairRemoveByRecDateData(recDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByRecDateAsync(DateTime recDate, TransactionManager tm_ = null)
		{
			RepairRemoveByRecDateData(recDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByRecDateData(DateTime recDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_req_log` WHERE `RecDate` = @RecDate";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
		}
		#endregion // RemoveByRecDate
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
		public int Put(Admin_req_logEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_req_logEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_req_logEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_req_log` SET `LogID` = @LogID, `UserID` = @UserID, `Type` = @Type, `Result` = @Result, `RequestUrl` = @RequestUrl, `IP` = @IP, `OS` = @OS, `Browser` = @Browser, `Location` = @Location, `UserAgent` = @UserAgent WHERE `LogID` = @LogID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", item.LogID, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", item.UserID != null ? item.UserID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Type", item.Type, MySqlDbType.Int32),
				Database.CreateInParameter("@Result", item.Result != null ? item.Result : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RequestUrl", item.RequestUrl != null ? item.RequestUrl : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@IP", item.IP != null ? item.IP : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@OS", item.OS != null ? item.OS : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Browser", item.Browser != null ? item.Browser : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Location", item.Location != null ? item.Location : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserAgent", item.UserAgent != null ? item.UserAgent : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@LogID_Original", item.HasOriginal ? item.OriginalLogID : item.LogID, MySqlDbType.VarChar),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_req_logEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_req_logEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "logID">编码GUID</param>
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
		/// /// <param name = "logID">编码GUID</param>
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
		/// /// <param name = "logID">编码GUID</param>
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
		#region PutUserID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUserIDByPK(string logID, string userID, TransactionManager tm_ = null)
		{
			RepairPutUserIDByPKData(logID, userID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutUserIDByPKAsync(string logID, string userID, TransactionManager tm_ = null)
		{
			RepairPutUserIDByPKData(logID, userID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutUserIDByPKData(string logID, string userID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_req_log` SET `UserID` = @UserID  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID != null ? userID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUserID(string userID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `UserID` = @UserID";
			var parameter_ = Database.CreateInParameter("@UserID", userID != null ? userID : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutUserIDAsync(string userID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `UserID` = @UserID";
			var parameter_ = Database.CreateInParameter("@UserID", userID != null ? userID : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutUserID
		#region PutType
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// /// <param name = "type">类型0-登录1-请求</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTypeByPK(string logID, int type, TransactionManager tm_ = null)
		{
			RepairPutTypeByPKData(logID, type, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTypeByPKAsync(string logID, int type, TransactionManager tm_ = null)
		{
			RepairPutTypeByPKData(logID, type, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTypeByPKData(string logID, int type, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_req_log` SET `Type` = @Type  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Type", type, MySqlDbType.Int32),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "type">类型0-登录1-请求</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutType(int type, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `Type` = @Type";
			var parameter_ = Database.CreateInParameter("@Type", type, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTypeAsync(int type, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `Type` = @Type";
			var parameter_ = Database.CreateInParameter("@Type", type, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutType
		#region PutResult
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// /// <param name = "result">结果</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutResultByPK(string logID, string result, TransactionManager tm_ = null)
		{
			RepairPutResultByPKData(logID, result, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutResultByPKAsync(string logID, string result, TransactionManager tm_ = null)
		{
			RepairPutResultByPKData(logID, result, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutResultByPKData(string logID, string result, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_req_log` SET `Result` = @Result  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Result", result != null ? result : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "result">结果</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutResult(string result, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `Result` = @Result";
			var parameter_ = Database.CreateInParameter("@Result", result != null ? result : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutResultAsync(string result, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `Result` = @Result";
			var parameter_ = Database.CreateInParameter("@Result", result != null ? result : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutResult
		#region PutRequestUrl
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// /// <param name = "requestUrl">请求地址</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRequestUrlByPK(string logID, string requestUrl, TransactionManager tm_ = null)
		{
			RepairPutRequestUrlByPKData(logID, requestUrl, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRequestUrlByPKAsync(string logID, string requestUrl, TransactionManager tm_ = null)
		{
			RepairPutRequestUrlByPKData(logID, requestUrl, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRequestUrlByPKData(string logID, string requestUrl, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_req_log` SET `RequestUrl` = @RequestUrl  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RequestUrl", requestUrl != null ? requestUrl : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "requestUrl">请求地址</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRequestUrl(string requestUrl, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `RequestUrl` = @RequestUrl";
			var parameter_ = Database.CreateInParameter("@RequestUrl", requestUrl != null ? requestUrl : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRequestUrlAsync(string requestUrl, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `RequestUrl` = @RequestUrl";
			var parameter_ = Database.CreateInParameter("@RequestUrl", requestUrl != null ? requestUrl : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRequestUrl
		#region PutIP
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// /// <param name = "iP">IP地址</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIPByPK(string logID, string iP, TransactionManager tm_ = null)
		{
			RepairPutIPByPKData(logID, iP, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutIPByPKAsync(string logID, string iP, TransactionManager tm_ = null)
		{
			RepairPutIPByPKData(logID, iP, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutIPByPKData(string logID, string iP, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_req_log` SET `IP` = @IP  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@IP", iP != null ? iP : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "iP">IP地址</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIP(string iP, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `IP` = @IP";
			var parameter_ = Database.CreateInParameter("@IP", iP != null ? iP : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutIPAsync(string iP, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `IP` = @IP";
			var parameter_ = Database.CreateInParameter("@IP", iP != null ? iP : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutIP
		#region PutOS
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// /// <param name = "oS">系统</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutOSByPK(string logID, string oS, TransactionManager tm_ = null)
		{
			RepairPutOSByPKData(logID, oS, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutOSByPKAsync(string logID, string oS, TransactionManager tm_ = null)
		{
			RepairPutOSByPKData(logID, oS, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutOSByPKData(string logID, string oS, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_req_log` SET `OS` = @OS  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@OS", oS != null ? oS : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "oS">系统</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutOS(string oS, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `OS` = @OS";
			var parameter_ = Database.CreateInParameter("@OS", oS != null ? oS : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutOSAsync(string oS, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `OS` = @OS";
			var parameter_ = Database.CreateInParameter("@OS", oS != null ? oS : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutOS
		#region PutBrowser
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// /// <param name = "browser">浏览器</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutBrowserByPK(string logID, string browser, TransactionManager tm_ = null)
		{
			RepairPutBrowserByPKData(logID, browser, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutBrowserByPKAsync(string logID, string browser, TransactionManager tm_ = null)
		{
			RepairPutBrowserByPKData(logID, browser, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutBrowserByPKData(string logID, string browser, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_req_log` SET `Browser` = @Browser  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Browser", browser != null ? browser : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "browser">浏览器</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutBrowser(string browser, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `Browser` = @Browser";
			var parameter_ = Database.CreateInParameter("@Browser", browser != null ? browser : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutBrowserAsync(string browser, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `Browser` = @Browser";
			var parameter_ = Database.CreateInParameter("@Browser", browser != null ? browser : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutBrowser
		#region PutLocation
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// /// <param name = "location">地址</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLocationByPK(string logID, string location, TransactionManager tm_ = null)
		{
			RepairPutLocationByPKData(logID, location, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutLocationByPKAsync(string logID, string location, TransactionManager tm_ = null)
		{
			RepairPutLocationByPKData(logID, location, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutLocationByPKData(string logID, string location, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_req_log` SET `Location` = @Location  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Location", location != null ? location : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "location">地址</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLocation(string location, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `Location` = @Location";
			var parameter_ = Database.CreateInParameter("@Location", location != null ? location : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutLocationAsync(string location, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `Location` = @Location";
			var parameter_ = Database.CreateInParameter("@Location", location != null ? location : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutLocation
		#region PutUserAgent
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// /// <param name = "userAgent">其他</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUserAgentByPK(string logID, string userAgent, TransactionManager tm_ = null)
		{
			RepairPutUserAgentByPKData(logID, userAgent, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutUserAgentByPKAsync(string logID, string userAgent, TransactionManager tm_ = null)
		{
			RepairPutUserAgentByPKData(logID, userAgent, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutUserAgentByPKData(string logID, string userAgent, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_req_log` SET `UserAgent` = @UserAgent  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserAgent", userAgent != null ? userAgent : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "userAgent">其他</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUserAgent(string userAgent, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `UserAgent` = @UserAgent";
			var parameter_ = Database.CreateInParameter("@UserAgent", userAgent != null ? userAgent : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutUserAgentAsync(string userAgent, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `UserAgent` = @UserAgent";
			var parameter_ = Database.CreateInParameter("@UserAgent", userAgent != null ? userAgent : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutUserAgent
		#region PutRecDate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRecDateByPK(string logID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(logID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRecDateByPKAsync(string logID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(logID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRecDateByPKData(string logID, DateTime recDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_req_log` SET `RecDate` = @RecDate  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRecDate(DateTime recDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `RecDate` = @RecDate";
			var parameter_ = Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRecDateAsync(DateTime recDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_req_log` SET `RecDate` = @RecDate";
			var parameter_ = Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRecDate
		#endregion // PutXXX
		#endregion // Put
	   
		#region Set
		
		/// <summary>
		/// 插入或者更新数据
		/// </summary>
		/// <param name = "item">要更新的实体对象</param>
		/// <param name="tm">事务管理对象</param>
		/// <return>true:插入操作；false:更新操作</return>
		public bool Set(Admin_req_logEO item, TransactionManager tm = null)
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
		public async Task<bool> SetAsync(Admin_req_logEO item, TransactionManager tm = null)
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
		/// /// <param name = "logID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_req_logEO GetByPK(string logID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(logID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		public async Task<Admin_req_logEO> GetByPKAsync(string logID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(logID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
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
		/// 按主键查询 UserID（字段）
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetUserIDByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`UserID`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetUserIDByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`UserID`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Type（字段）
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetTypeByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`Type`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<int> GetTypeByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`Type`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Result（字段）
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetResultByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Result`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetResultByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Result`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RequestUrl（字段）
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetRequestUrlByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`RequestUrl`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetRequestUrlByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`RequestUrl`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 IP（字段）
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetIPByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`IP`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetIPByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`IP`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 OS（字段）
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetOSByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`OS`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetOSByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`OS`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Browser（字段）
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetBrowserByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Browser`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetBrowserByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Browser`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Location（字段）
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetLocationByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Location`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetLocationByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Location`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 UserAgent（字段）
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetUserAgentByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`UserAgent`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetUserAgentByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`UserAgent`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RecDate（字段）
		/// </summary>
		/// /// <param name = "logID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public DateTime GetRecDateByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (DateTime)GetScalar("`RecDate`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<DateTime> GetRecDateByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (DateTime)await GetScalarAsync("`RecDate`", "`LogID` = @LogID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByUserID
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserID(string userID)
		{
			return GetByUserID(userID, 0, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByUserIDAsync(string userID)
		{
			return await GetByUserIDAsync(userID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserID(string userID, TransactionManager tm_)
		{
			return GetByUserID(userID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByUserIDAsync(string userID, TransactionManager tm_)
		{
			return await GetByUserIDAsync(userID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserID(string userID, int top_)
		{
			return GetByUserID(userID, top_, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByUserIDAsync(string userID, int top_)
		{
			return await GetByUserIDAsync(userID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserID(string userID, int top_, TransactionManager tm_)
		{
			return GetByUserID(userID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByUserIDAsync(string userID, int top_, TransactionManager tm_)
		{
			return await GetByUserIDAsync(userID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserID(string userID, string sort_)
		{
			return GetByUserID(userID, 0, sort_, null);
		}
		public async Task<List<Admin_req_logEO>> GetByUserIDAsync(string userID, string sort_)
		{
			return await GetByUserIDAsync(userID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserID(string userID, string sort_, TransactionManager tm_)
		{
			return GetByUserID(userID, 0, sort_, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByUserIDAsync(string userID, string sort_, TransactionManager tm_)
		{
			return await GetByUserIDAsync(userID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserID(string userID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(userID != null ? "`UserID` = @UserID" : "`UserID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (userID != null)
				paras_.Add(Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		public async Task<List<Admin_req_logEO>> GetByUserIDAsync(string userID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(userID != null ? "`UserID` = @UserID" : "`UserID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (userID != null)
				paras_.Add(Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		#endregion // GetByUserID
		#region GetByType
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-登录1-请求</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByType(int type)
		{
			return GetByType(type, 0, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByTypeAsync(int type)
		{
			return await GetByTypeAsync(type, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-登录1-请求</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByType(int type, TransactionManager tm_)
		{
			return GetByType(type, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByTypeAsync(int type, TransactionManager tm_)
		{
			return await GetByTypeAsync(type, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-登录1-请求</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByType(int type, int top_)
		{
			return GetByType(type, top_, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByTypeAsync(int type, int top_)
		{
			return await GetByTypeAsync(type, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-登录1-请求</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByType(int type, int top_, TransactionManager tm_)
		{
			return GetByType(type, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByTypeAsync(int type, int top_, TransactionManager tm_)
		{
			return await GetByTypeAsync(type, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-登录1-请求</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByType(int type, string sort_)
		{
			return GetByType(type, 0, sort_, null);
		}
		public async Task<List<Admin_req_logEO>> GetByTypeAsync(int type, string sort_)
		{
			return await GetByTypeAsync(type, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-登录1-请求</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByType(int type, string sort_, TransactionManager tm_)
		{
			return GetByType(type, 0, sort_, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByTypeAsync(int type, string sort_, TransactionManager tm_)
		{
			return await GetByTypeAsync(type, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-登录1-请求</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByType(int type, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Type` = @Type", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Type", type, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		public async Task<List<Admin_req_logEO>> GetByTypeAsync(int type, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Type` = @Type", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Type", type, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		#endregion // GetByType
		#region GetByResult
		
		/// <summary>
		/// 按 Result（字段） 查询
		/// </summary>
		/// /// <param name = "result">结果</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByResult(string result)
		{
			return GetByResult(result, 0, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByResultAsync(string result)
		{
			return await GetByResultAsync(result, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Result（字段） 查询
		/// </summary>
		/// /// <param name = "result">结果</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByResult(string result, TransactionManager tm_)
		{
			return GetByResult(result, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByResultAsync(string result, TransactionManager tm_)
		{
			return await GetByResultAsync(result, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Result（字段） 查询
		/// </summary>
		/// /// <param name = "result">结果</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByResult(string result, int top_)
		{
			return GetByResult(result, top_, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByResultAsync(string result, int top_)
		{
			return await GetByResultAsync(result, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Result（字段） 查询
		/// </summary>
		/// /// <param name = "result">结果</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByResult(string result, int top_, TransactionManager tm_)
		{
			return GetByResult(result, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByResultAsync(string result, int top_, TransactionManager tm_)
		{
			return await GetByResultAsync(result, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Result（字段） 查询
		/// </summary>
		/// /// <param name = "result">结果</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByResult(string result, string sort_)
		{
			return GetByResult(result, 0, sort_, null);
		}
		public async Task<List<Admin_req_logEO>> GetByResultAsync(string result, string sort_)
		{
			return await GetByResultAsync(result, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Result（字段） 查询
		/// </summary>
		/// /// <param name = "result">结果</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByResult(string result, string sort_, TransactionManager tm_)
		{
			return GetByResult(result, 0, sort_, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByResultAsync(string result, string sort_, TransactionManager tm_)
		{
			return await GetByResultAsync(result, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Result（字段） 查询
		/// </summary>
		/// /// <param name = "result">结果</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByResult(string result, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(result != null ? "`Result` = @Result" : "`Result` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (result != null)
				paras_.Add(Database.CreateInParameter("@Result", result, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		public async Task<List<Admin_req_logEO>> GetByResultAsync(string result, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(result != null ? "`Result` = @Result" : "`Result` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (result != null)
				paras_.Add(Database.CreateInParameter("@Result", result, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		#endregion // GetByResult
		#region GetByRequestUrl
		
		/// <summary>
		/// 按 RequestUrl（字段） 查询
		/// </summary>
		/// /// <param name = "requestUrl">请求地址</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRequestUrl(string requestUrl)
		{
			return GetByRequestUrl(requestUrl, 0, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByRequestUrlAsync(string requestUrl)
		{
			return await GetByRequestUrlAsync(requestUrl, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RequestUrl（字段） 查询
		/// </summary>
		/// /// <param name = "requestUrl">请求地址</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRequestUrl(string requestUrl, TransactionManager tm_)
		{
			return GetByRequestUrl(requestUrl, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByRequestUrlAsync(string requestUrl, TransactionManager tm_)
		{
			return await GetByRequestUrlAsync(requestUrl, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RequestUrl（字段） 查询
		/// </summary>
		/// /// <param name = "requestUrl">请求地址</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRequestUrl(string requestUrl, int top_)
		{
			return GetByRequestUrl(requestUrl, top_, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByRequestUrlAsync(string requestUrl, int top_)
		{
			return await GetByRequestUrlAsync(requestUrl, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RequestUrl（字段） 查询
		/// </summary>
		/// /// <param name = "requestUrl">请求地址</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRequestUrl(string requestUrl, int top_, TransactionManager tm_)
		{
			return GetByRequestUrl(requestUrl, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByRequestUrlAsync(string requestUrl, int top_, TransactionManager tm_)
		{
			return await GetByRequestUrlAsync(requestUrl, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RequestUrl（字段） 查询
		/// </summary>
		/// /// <param name = "requestUrl">请求地址</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRequestUrl(string requestUrl, string sort_)
		{
			return GetByRequestUrl(requestUrl, 0, sort_, null);
		}
		public async Task<List<Admin_req_logEO>> GetByRequestUrlAsync(string requestUrl, string sort_)
		{
			return await GetByRequestUrlAsync(requestUrl, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RequestUrl（字段） 查询
		/// </summary>
		/// /// <param name = "requestUrl">请求地址</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRequestUrl(string requestUrl, string sort_, TransactionManager tm_)
		{
			return GetByRequestUrl(requestUrl, 0, sort_, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByRequestUrlAsync(string requestUrl, string sort_, TransactionManager tm_)
		{
			return await GetByRequestUrlAsync(requestUrl, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RequestUrl（字段） 查询
		/// </summary>
		/// /// <param name = "requestUrl">请求地址</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRequestUrl(string requestUrl, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(requestUrl != null ? "`RequestUrl` = @RequestUrl" : "`RequestUrl` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (requestUrl != null)
				paras_.Add(Database.CreateInParameter("@RequestUrl", requestUrl, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		public async Task<List<Admin_req_logEO>> GetByRequestUrlAsync(string requestUrl, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(requestUrl != null ? "`RequestUrl` = @RequestUrl" : "`RequestUrl` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (requestUrl != null)
				paras_.Add(Database.CreateInParameter("@RequestUrl", requestUrl, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		#endregion // GetByRequestUrl
		#region GetByIP
		
		/// <summary>
		/// 按 IP（字段） 查询
		/// </summary>
		/// /// <param name = "iP">IP地址</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByIP(string iP)
		{
			return GetByIP(iP, 0, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByIPAsync(string iP)
		{
			return await GetByIPAsync(iP, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 IP（字段） 查询
		/// </summary>
		/// /// <param name = "iP">IP地址</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByIP(string iP, TransactionManager tm_)
		{
			return GetByIP(iP, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByIPAsync(string iP, TransactionManager tm_)
		{
			return await GetByIPAsync(iP, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IP（字段） 查询
		/// </summary>
		/// /// <param name = "iP">IP地址</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByIP(string iP, int top_)
		{
			return GetByIP(iP, top_, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByIPAsync(string iP, int top_)
		{
			return await GetByIPAsync(iP, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 IP（字段） 查询
		/// </summary>
		/// /// <param name = "iP">IP地址</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByIP(string iP, int top_, TransactionManager tm_)
		{
			return GetByIP(iP, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByIPAsync(string iP, int top_, TransactionManager tm_)
		{
			return await GetByIPAsync(iP, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IP（字段） 查询
		/// </summary>
		/// /// <param name = "iP">IP地址</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByIP(string iP, string sort_)
		{
			return GetByIP(iP, 0, sort_, null);
		}
		public async Task<List<Admin_req_logEO>> GetByIPAsync(string iP, string sort_)
		{
			return await GetByIPAsync(iP, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 IP（字段） 查询
		/// </summary>
		/// /// <param name = "iP">IP地址</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByIP(string iP, string sort_, TransactionManager tm_)
		{
			return GetByIP(iP, 0, sort_, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByIPAsync(string iP, string sort_, TransactionManager tm_)
		{
			return await GetByIPAsync(iP, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 IP（字段） 查询
		/// </summary>
		/// /// <param name = "iP">IP地址</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByIP(string iP, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(iP != null ? "`IP` = @IP" : "`IP` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (iP != null)
				paras_.Add(Database.CreateInParameter("@IP", iP, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		public async Task<List<Admin_req_logEO>> GetByIPAsync(string iP, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(iP != null ? "`IP` = @IP" : "`IP` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (iP != null)
				paras_.Add(Database.CreateInParameter("@IP", iP, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		#endregion // GetByIP
		#region GetByOS
		
		/// <summary>
		/// 按 OS（字段） 查询
		/// </summary>
		/// /// <param name = "oS">系统</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByOS(string oS)
		{
			return GetByOS(oS, 0, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByOSAsync(string oS)
		{
			return await GetByOSAsync(oS, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 OS（字段） 查询
		/// </summary>
		/// /// <param name = "oS">系统</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByOS(string oS, TransactionManager tm_)
		{
			return GetByOS(oS, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByOSAsync(string oS, TransactionManager tm_)
		{
			return await GetByOSAsync(oS, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 OS（字段） 查询
		/// </summary>
		/// /// <param name = "oS">系统</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByOS(string oS, int top_)
		{
			return GetByOS(oS, top_, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByOSAsync(string oS, int top_)
		{
			return await GetByOSAsync(oS, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 OS（字段） 查询
		/// </summary>
		/// /// <param name = "oS">系统</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByOS(string oS, int top_, TransactionManager tm_)
		{
			return GetByOS(oS, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByOSAsync(string oS, int top_, TransactionManager tm_)
		{
			return await GetByOSAsync(oS, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 OS（字段） 查询
		/// </summary>
		/// /// <param name = "oS">系统</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByOS(string oS, string sort_)
		{
			return GetByOS(oS, 0, sort_, null);
		}
		public async Task<List<Admin_req_logEO>> GetByOSAsync(string oS, string sort_)
		{
			return await GetByOSAsync(oS, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 OS（字段） 查询
		/// </summary>
		/// /// <param name = "oS">系统</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByOS(string oS, string sort_, TransactionManager tm_)
		{
			return GetByOS(oS, 0, sort_, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByOSAsync(string oS, string sort_, TransactionManager tm_)
		{
			return await GetByOSAsync(oS, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 OS（字段） 查询
		/// </summary>
		/// /// <param name = "oS">系统</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByOS(string oS, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(oS != null ? "`OS` = @OS" : "`OS` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (oS != null)
				paras_.Add(Database.CreateInParameter("@OS", oS, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		public async Task<List<Admin_req_logEO>> GetByOSAsync(string oS, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(oS != null ? "`OS` = @OS" : "`OS` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (oS != null)
				paras_.Add(Database.CreateInParameter("@OS", oS, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		#endregion // GetByOS
		#region GetByBrowser
		
		/// <summary>
		/// 按 Browser（字段） 查询
		/// </summary>
		/// /// <param name = "browser">浏览器</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByBrowser(string browser)
		{
			return GetByBrowser(browser, 0, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByBrowserAsync(string browser)
		{
			return await GetByBrowserAsync(browser, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Browser（字段） 查询
		/// </summary>
		/// /// <param name = "browser">浏览器</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByBrowser(string browser, TransactionManager tm_)
		{
			return GetByBrowser(browser, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByBrowserAsync(string browser, TransactionManager tm_)
		{
			return await GetByBrowserAsync(browser, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Browser（字段） 查询
		/// </summary>
		/// /// <param name = "browser">浏览器</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByBrowser(string browser, int top_)
		{
			return GetByBrowser(browser, top_, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByBrowserAsync(string browser, int top_)
		{
			return await GetByBrowserAsync(browser, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Browser（字段） 查询
		/// </summary>
		/// /// <param name = "browser">浏览器</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByBrowser(string browser, int top_, TransactionManager tm_)
		{
			return GetByBrowser(browser, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByBrowserAsync(string browser, int top_, TransactionManager tm_)
		{
			return await GetByBrowserAsync(browser, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Browser（字段） 查询
		/// </summary>
		/// /// <param name = "browser">浏览器</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByBrowser(string browser, string sort_)
		{
			return GetByBrowser(browser, 0, sort_, null);
		}
		public async Task<List<Admin_req_logEO>> GetByBrowserAsync(string browser, string sort_)
		{
			return await GetByBrowserAsync(browser, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Browser（字段） 查询
		/// </summary>
		/// /// <param name = "browser">浏览器</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByBrowser(string browser, string sort_, TransactionManager tm_)
		{
			return GetByBrowser(browser, 0, sort_, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByBrowserAsync(string browser, string sort_, TransactionManager tm_)
		{
			return await GetByBrowserAsync(browser, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Browser（字段） 查询
		/// </summary>
		/// /// <param name = "browser">浏览器</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByBrowser(string browser, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(browser != null ? "`Browser` = @Browser" : "`Browser` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (browser != null)
				paras_.Add(Database.CreateInParameter("@Browser", browser, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		public async Task<List<Admin_req_logEO>> GetByBrowserAsync(string browser, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(browser != null ? "`Browser` = @Browser" : "`Browser` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (browser != null)
				paras_.Add(Database.CreateInParameter("@Browser", browser, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		#endregion // GetByBrowser
		#region GetByLocation
		
		/// <summary>
		/// 按 Location（字段） 查询
		/// </summary>
		/// /// <param name = "location">地址</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByLocation(string location)
		{
			return GetByLocation(location, 0, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByLocationAsync(string location)
		{
			return await GetByLocationAsync(location, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Location（字段） 查询
		/// </summary>
		/// /// <param name = "location">地址</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByLocation(string location, TransactionManager tm_)
		{
			return GetByLocation(location, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByLocationAsync(string location, TransactionManager tm_)
		{
			return await GetByLocationAsync(location, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Location（字段） 查询
		/// </summary>
		/// /// <param name = "location">地址</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByLocation(string location, int top_)
		{
			return GetByLocation(location, top_, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByLocationAsync(string location, int top_)
		{
			return await GetByLocationAsync(location, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Location（字段） 查询
		/// </summary>
		/// /// <param name = "location">地址</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByLocation(string location, int top_, TransactionManager tm_)
		{
			return GetByLocation(location, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByLocationAsync(string location, int top_, TransactionManager tm_)
		{
			return await GetByLocationAsync(location, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Location（字段） 查询
		/// </summary>
		/// /// <param name = "location">地址</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByLocation(string location, string sort_)
		{
			return GetByLocation(location, 0, sort_, null);
		}
		public async Task<List<Admin_req_logEO>> GetByLocationAsync(string location, string sort_)
		{
			return await GetByLocationAsync(location, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Location（字段） 查询
		/// </summary>
		/// /// <param name = "location">地址</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByLocation(string location, string sort_, TransactionManager tm_)
		{
			return GetByLocation(location, 0, sort_, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByLocationAsync(string location, string sort_, TransactionManager tm_)
		{
			return await GetByLocationAsync(location, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Location（字段） 查询
		/// </summary>
		/// /// <param name = "location">地址</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByLocation(string location, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(location != null ? "`Location` = @Location" : "`Location` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (location != null)
				paras_.Add(Database.CreateInParameter("@Location", location, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		public async Task<List<Admin_req_logEO>> GetByLocationAsync(string location, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(location != null ? "`Location` = @Location" : "`Location` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (location != null)
				paras_.Add(Database.CreateInParameter("@Location", location, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		#endregion // GetByLocation
		#region GetByUserAgent
		
		/// <summary>
		/// 按 UserAgent（字段） 查询
		/// </summary>
		/// /// <param name = "userAgent">其他</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserAgent(string userAgent)
		{
			return GetByUserAgent(userAgent, 0, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByUserAgentAsync(string userAgent)
		{
			return await GetByUserAgentAsync(userAgent, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserAgent（字段） 查询
		/// </summary>
		/// /// <param name = "userAgent">其他</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserAgent(string userAgent, TransactionManager tm_)
		{
			return GetByUserAgent(userAgent, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByUserAgentAsync(string userAgent, TransactionManager tm_)
		{
			return await GetByUserAgentAsync(userAgent, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserAgent（字段） 查询
		/// </summary>
		/// /// <param name = "userAgent">其他</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserAgent(string userAgent, int top_)
		{
			return GetByUserAgent(userAgent, top_, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByUserAgentAsync(string userAgent, int top_)
		{
			return await GetByUserAgentAsync(userAgent, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserAgent（字段） 查询
		/// </summary>
		/// /// <param name = "userAgent">其他</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserAgent(string userAgent, int top_, TransactionManager tm_)
		{
			return GetByUserAgent(userAgent, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByUserAgentAsync(string userAgent, int top_, TransactionManager tm_)
		{
			return await GetByUserAgentAsync(userAgent, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserAgent（字段） 查询
		/// </summary>
		/// /// <param name = "userAgent">其他</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserAgent(string userAgent, string sort_)
		{
			return GetByUserAgent(userAgent, 0, sort_, null);
		}
		public async Task<List<Admin_req_logEO>> GetByUserAgentAsync(string userAgent, string sort_)
		{
			return await GetByUserAgentAsync(userAgent, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 UserAgent（字段） 查询
		/// </summary>
		/// /// <param name = "userAgent">其他</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserAgent(string userAgent, string sort_, TransactionManager tm_)
		{
			return GetByUserAgent(userAgent, 0, sort_, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByUserAgentAsync(string userAgent, string sort_, TransactionManager tm_)
		{
			return await GetByUserAgentAsync(userAgent, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 UserAgent（字段） 查询
		/// </summary>
		/// /// <param name = "userAgent">其他</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByUserAgent(string userAgent, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(userAgent != null ? "`UserAgent` = @UserAgent" : "`UserAgent` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (userAgent != null)
				paras_.Add(Database.CreateInParameter("@UserAgent", userAgent, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		public async Task<List<Admin_req_logEO>> GetByUserAgentAsync(string userAgent, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(userAgent != null ? "`UserAgent` = @UserAgent" : "`UserAgent` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (userAgent != null)
				paras_.Add(Database.CreateInParameter("@UserAgent", userAgent, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		#endregion // GetByUserAgent
		#region GetByRecDate
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRecDate(DateTime recDate)
		{
			return GetByRecDate(recDate, 0, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByRecDateAsync(DateTime recDate)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRecDate(DateTime recDate, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByRecDateAsync(DateTime recDate, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRecDate(DateTime recDate, int top_)
		{
			return GetByRecDate(recDate, top_, string.Empty, null);
		}
		public async Task<List<Admin_req_logEO>> GetByRecDateAsync(DateTime recDate, int top_)
		{
			return await GetByRecDateAsync(recDate, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRecDate(DateTime recDate, int top_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByRecDateAsync(DateTime recDate, int top_, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRecDate(DateTime recDate, string sort_)
		{
			return GetByRecDate(recDate, 0, sort_, null);
		}
		public async Task<List<Admin_req_logEO>> GetByRecDateAsync(DateTime recDate, string sort_)
		{
			return await GetByRecDateAsync(recDate, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRecDate(DateTime recDate, string sort_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, sort_, tm_);
		}
		public async Task<List<Admin_req_logEO>> GetByRecDateAsync(DateTime recDate, string sort_, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_req_logEO> GetByRecDate(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		public async Task<List<Admin_req_logEO>> GetByRecDateAsync(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_req_logEO.MapDataReader);
		}
		#endregion // GetByRecDate
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
