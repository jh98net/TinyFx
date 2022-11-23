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
	/// 后台管理操作日志
	/// 【表 admin_oper_log 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_oper_logEO : IRowMapper<Admin_oper_logEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_oper_logEO()
		{
			this.OperType = "0";
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
		/// 日志编码GUID
		/// 【主键 varchar(40)】
		/// </summary>
		[DataMember(Order = 1)]
		public string LogID { get; set; }
		/// <summary>
		/// 操作的种类（根据业务自行约定）
		///              sql --数据库操作
		/// 【字段 varchar(150)】
		/// </summary>
		[DataMember(Order = 2)]
		public string OperType { get; set; }
		/// <summary>
		/// 操作描述
		/// 【字段 varchar(255)】
		/// </summary>
		[DataMember(Order = 3)]
		public string Title { get; set; }
		/// <summary>
		/// 内容
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 4)]
		public string Content { get; set; }
		/// <summary>
		/// 标记1
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 5)]
		public string Tag1 { get; set; }
		/// <summary>
		/// 标记2
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 6)]
		public string Tag2 { get; set; }
		/// <summary>
		/// 标记3
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 7)]
		public string Tag3 { get; set; }
		/// <summary>
		/// 标记3
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 8)]
		public string Tag4 { get; set; }
		/// <summary>
		/// 记录时间
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 9)]
		public DateTime RecDate { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_oper_logEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_oper_logEO MapDataReader(IDataReader reader)
		{
		    Admin_oper_logEO ret = new Admin_oper_logEO();
			ret.LogID = reader.ToString("LogID");
			ret.OriginalLogID = ret.LogID;
			ret.OperType = reader.ToString("OperType");
			ret.Title = reader.ToString("Title");
			ret.Content = reader.ToString("Content");
			ret.Tag1 = reader.ToString("Tag1");
			ret.Tag2 = reader.ToString("Tag2");
			ret.Tag3 = reader.ToString("Tag3");
			ret.Tag4 = reader.ToString("Tag4");
			ret.RecDate = reader.ToDateTime("RecDate");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 后台管理操作日志
	/// 【表 admin_oper_log 的操作类】
	/// </summary>
	public class Admin_oper_logMO : MySqlTableMO<Admin_oper_logEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_oper_log`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_oper_logMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_oper_logMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_oper_logMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_oper_logEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_oper_logEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_oper_logEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_oper_log` (`LogID`, `OperType`, `Title`, `Content`, `Tag1`, `Tag2`, `Tag3`, `Tag4`, `RecDate`) VALUE (@LogID, @OperType, @Title, @Content, @Tag1, @Tag2, @Tag3, @Tag4, @RecDate);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", item.LogID, MySqlDbType.VarChar),
				Database.CreateInParameter("@OperType", item.OperType != null ? item.OperType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Title", item.Title != null ? item.Title : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Content", item.Content != null ? item.Content : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Tag1", item.Tag1 != null ? item.Tag1 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Tag2", item.Tag2 != null ? item.Tag2 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Tag3", item.Tag3 != null ? item.Tag3 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Tag4", item.Tag4 != null ? item.Tag4 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RecDate", item.RecDate, MySqlDbType.DateTime),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
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
			sql_ = @"DELETE FROM `admin_oper_log` WHERE `LogID` = @LogID";
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
		public int Remove(Admin_oper_logEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.LogID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_oper_logEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.LogID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByOperType
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "operType">操作的种类（根据业务自行约定）</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByOperType(string operType, TransactionManager tm_ = null)
		{
			RepairRemoveByOperTypeData(operType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByOperTypeAsync(string operType, TransactionManager tm_ = null)
		{
			RepairRemoveByOperTypeData(operType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByOperTypeData(string operType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_oper_log` WHERE " + (operType != null ? "`OperType` = @OperType" : "`OperType` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (operType != null)
				paras_.Add(Database.CreateInParameter("@OperType", operType, MySqlDbType.VarChar));
		}
		#endregion // RemoveByOperType
		#region RemoveByTitle
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "title">操作描述</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByTitle(string title, TransactionManager tm_ = null)
		{
			RepairRemoveByTitleData(title, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTitleAsync(string title, TransactionManager tm_ = null)
		{
			RepairRemoveByTitleData(title, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTitleData(string title, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_oper_log` WHERE " + (title != null ? "`Title` = @Title" : "`Title` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (title != null)
				paras_.Add(Database.CreateInParameter("@Title", title, MySqlDbType.VarChar));
		}
		#endregion // RemoveByTitle
		#region RemoveByContent
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByContent(string content, TransactionManager tm_ = null)
		{
			RepairRemoveByContentData(content, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByContentAsync(string content, TransactionManager tm_ = null)
		{
			RepairRemoveByContentData(content, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByContentData(string content, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_oper_log` WHERE " + (content != null ? "`Content` = @Content" : "`Content` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (content != null)
				paras_.Add(Database.CreateInParameter("@Content", content, MySqlDbType.Text));
		}
		#endregion // RemoveByContent
		#region RemoveByTag1
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "tag1">标记1</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByTag1(string tag1, TransactionManager tm_ = null)
		{
			RepairRemoveByTag1Data(tag1, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTag1Async(string tag1, TransactionManager tm_ = null)
		{
			RepairRemoveByTag1Data(tag1, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTag1Data(string tag1, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_oper_log` WHERE " + (tag1 != null ? "`Tag1` = @Tag1" : "`Tag1` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (tag1 != null)
				paras_.Add(Database.CreateInParameter("@Tag1", tag1, MySqlDbType.VarChar));
		}
		#endregion // RemoveByTag1
		#region RemoveByTag2
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "tag2">标记2</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByTag2(string tag2, TransactionManager tm_ = null)
		{
			RepairRemoveByTag2Data(tag2, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTag2Async(string tag2, TransactionManager tm_ = null)
		{
			RepairRemoveByTag2Data(tag2, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTag2Data(string tag2, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_oper_log` WHERE " + (tag2 != null ? "`Tag2` = @Tag2" : "`Tag2` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (tag2 != null)
				paras_.Add(Database.CreateInParameter("@Tag2", tag2, MySqlDbType.VarChar));
		}
		#endregion // RemoveByTag2
		#region RemoveByTag3
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "tag3">标记3</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByTag3(string tag3, TransactionManager tm_ = null)
		{
			RepairRemoveByTag3Data(tag3, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTag3Async(string tag3, TransactionManager tm_ = null)
		{
			RepairRemoveByTag3Data(tag3, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTag3Data(string tag3, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_oper_log` WHERE " + (tag3 != null ? "`Tag3` = @Tag3" : "`Tag3` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (tag3 != null)
				paras_.Add(Database.CreateInParameter("@Tag3", tag3, MySqlDbType.VarChar));
		}
		#endregion // RemoveByTag3
		#region RemoveByTag4
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "tag4">标记3</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByTag4(string tag4, TransactionManager tm_ = null)
		{
			RepairRemoveByTag4Data(tag4, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTag4Async(string tag4, TransactionManager tm_ = null)
		{
			RepairRemoveByTag4Data(tag4, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTag4Data(string tag4, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_oper_log` WHERE " + (tag4 != null ? "`Tag4` = @Tag4" : "`Tag4` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (tag4 != null)
				paras_.Add(Database.CreateInParameter("@Tag4", tag4, MySqlDbType.VarChar));
		}
		#endregion // RemoveByTag4
		#region RemoveByRecDate
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "recDate">记录时间</param>
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
			sql_ = @"DELETE FROM `admin_oper_log` WHERE `RecDate` = @RecDate";
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
		public int Put(Admin_oper_logEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_oper_logEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_oper_logEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_oper_log` SET `LogID` = @LogID, `OperType` = @OperType, `Title` = @Title, `Content` = @Content, `Tag1` = @Tag1, `Tag2` = @Tag2, `Tag3` = @Tag3, `Tag4` = @Tag4 WHERE `LogID` = @LogID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", item.LogID, MySqlDbType.VarChar),
				Database.CreateInParameter("@OperType", item.OperType != null ? item.OperType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Title", item.Title != null ? item.Title : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Content", item.Content != null ? item.Content : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Tag1", item.Tag1 != null ? item.Tag1 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Tag2", item.Tag2 != null ? item.Tag2 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Tag3", item.Tag3 != null ? item.Tag3 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Tag4", item.Tag4 != null ? item.Tag4 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID_Original", item.HasOriginal ? item.OriginalLogID : item.LogID, MySqlDbType.VarChar),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_oper_logEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_oper_logEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "logID">日志编码GUID</param>
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
		/// /// <param name = "logID">日志编码GUID</param>
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
		/// /// <param name = "logID">日志编码GUID</param>
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
		#region PutOperType
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// /// <param name = "operType">操作的种类（根据业务自行约定）</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutOperTypeByPK(string logID, string operType, TransactionManager tm_ = null)
		{
			RepairPutOperTypeByPKData(logID, operType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutOperTypeByPKAsync(string logID, string operType, TransactionManager tm_ = null)
		{
			RepairPutOperTypeByPKData(logID, operType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutOperTypeByPKData(string logID, string operType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_oper_log` SET `OperType` = @OperType  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@OperType", operType != null ? operType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "operType">操作的种类（根据业务自行约定）</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutOperType(string operType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `OperType` = @OperType";
			var parameter_ = Database.CreateInParameter("@OperType", operType != null ? operType : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutOperTypeAsync(string operType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `OperType` = @OperType";
			var parameter_ = Database.CreateInParameter("@OperType", operType != null ? operType : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutOperType
		#region PutTitle
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// /// <param name = "title">操作描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTitleByPK(string logID, string title, TransactionManager tm_ = null)
		{
			RepairPutTitleByPKData(logID, title, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTitleByPKAsync(string logID, string title, TransactionManager tm_ = null)
		{
			RepairPutTitleByPKData(logID, title, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTitleByPKData(string logID, string title, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_oper_log` SET `Title` = @Title  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Title", title != null ? title : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "title">操作描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTitle(string title, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Title` = @Title";
			var parameter_ = Database.CreateInParameter("@Title", title != null ? title : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTitleAsync(string title, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Title` = @Title";
			var parameter_ = Database.CreateInParameter("@Title", title != null ? title : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTitle
		#region PutContent
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// /// <param name = "content">内容</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutContentByPK(string logID, string content, TransactionManager tm_ = null)
		{
			RepairPutContentByPKData(logID, content, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutContentByPKAsync(string logID, string content, TransactionManager tm_ = null)
		{
			RepairPutContentByPKData(logID, content, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutContentByPKData(string logID, string content, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_oper_log` SET `Content` = @Content  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Content", content != null ? content : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutContent(string content, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Content` = @Content";
			var parameter_ = Database.CreateInParameter("@Content", content != null ? content : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutContentAsync(string content, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Content` = @Content";
			var parameter_ = Database.CreateInParameter("@Content", content != null ? content : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutContent
		#region PutTag1
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// /// <param name = "tag1">标记1</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTag1ByPK(string logID, string tag1, TransactionManager tm_ = null)
		{
			RepairPutTag1ByPKData(logID, tag1, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTag1ByPKAsync(string logID, string tag1, TransactionManager tm_ = null)
		{
			RepairPutTag1ByPKData(logID, tag1, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTag1ByPKData(string logID, string tag1, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_oper_log` SET `Tag1` = @Tag1  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Tag1", tag1 != null ? tag1 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "tag1">标记1</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTag1(string tag1, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Tag1` = @Tag1";
			var parameter_ = Database.CreateInParameter("@Tag1", tag1 != null ? tag1 : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTag1Async(string tag1, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Tag1` = @Tag1";
			var parameter_ = Database.CreateInParameter("@Tag1", tag1 != null ? tag1 : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTag1
		#region PutTag2
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// /// <param name = "tag2">标记2</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTag2ByPK(string logID, string tag2, TransactionManager tm_ = null)
		{
			RepairPutTag2ByPKData(logID, tag2, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTag2ByPKAsync(string logID, string tag2, TransactionManager tm_ = null)
		{
			RepairPutTag2ByPKData(logID, tag2, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTag2ByPKData(string logID, string tag2, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_oper_log` SET `Tag2` = @Tag2  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Tag2", tag2 != null ? tag2 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "tag2">标记2</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTag2(string tag2, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Tag2` = @Tag2";
			var parameter_ = Database.CreateInParameter("@Tag2", tag2 != null ? tag2 : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTag2Async(string tag2, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Tag2` = @Tag2";
			var parameter_ = Database.CreateInParameter("@Tag2", tag2 != null ? tag2 : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTag2
		#region PutTag3
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// /// <param name = "tag3">标记3</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTag3ByPK(string logID, string tag3, TransactionManager tm_ = null)
		{
			RepairPutTag3ByPKData(logID, tag3, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTag3ByPKAsync(string logID, string tag3, TransactionManager tm_ = null)
		{
			RepairPutTag3ByPKData(logID, tag3, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTag3ByPKData(string logID, string tag3, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_oper_log` SET `Tag3` = @Tag3  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Tag3", tag3 != null ? tag3 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "tag3">标记3</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTag3(string tag3, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Tag3` = @Tag3";
			var parameter_ = Database.CreateInParameter("@Tag3", tag3 != null ? tag3 : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTag3Async(string tag3, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Tag3` = @Tag3";
			var parameter_ = Database.CreateInParameter("@Tag3", tag3 != null ? tag3 : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTag3
		#region PutTag4
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// /// <param name = "tag4">标记3</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTag4ByPK(string logID, string tag4, TransactionManager tm_ = null)
		{
			RepairPutTag4ByPKData(logID, tag4, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTag4ByPKAsync(string logID, string tag4, TransactionManager tm_ = null)
		{
			RepairPutTag4ByPKData(logID, tag4, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTag4ByPKData(string logID, string tag4, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_oper_log` SET `Tag4` = @Tag4  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Tag4", tag4 != null ? tag4 : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "tag4">标记3</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTag4(string tag4, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Tag4` = @Tag4";
			var parameter_ = Database.CreateInParameter("@Tag4", tag4 != null ? tag4 : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTag4Async(string tag4, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `Tag4` = @Tag4";
			var parameter_ = Database.CreateInParameter("@Tag4", tag4 != null ? tag4 : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTag4
		#region PutRecDate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// /// <param name = "recDate">记录时间</param>
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
			sql_ = @"UPDATE `admin_oper_log` SET `RecDate` = @RecDate  WHERE `LogID` = @LogID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "recDate">记录时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRecDate(DateTime recDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `RecDate` = @RecDate";
			var parameter_ = Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRecDateAsync(DateTime recDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_oper_log` SET `RecDate` = @RecDate";
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
		public bool Set(Admin_oper_logEO item, TransactionManager tm = null)
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
		public async Task<bool> SetAsync(Admin_oper_logEO item, TransactionManager tm = null)
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
		/// /// <param name = "logID">日志编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_oper_logEO GetByPK(string logID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(logID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		public async Task<Admin_oper_logEO> GetByPKAsync(string logID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(logID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
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
		/// 按主键查询 OperType（字段）
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetOperTypeByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`OperType`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetOperTypeByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`OperType`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Title（字段）
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetTitleByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Title`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetTitleByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Title`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Content（字段）
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetContentByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Content`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetContentByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Content`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Tag1（字段）
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetTag1ByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Tag1`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetTag1ByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Tag1`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Tag2（字段）
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetTag2ByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Tag2`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetTag2ByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Tag2`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Tag3（字段）
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetTag3ByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Tag3`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetTag3ByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Tag3`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Tag4（字段）
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetTag4ByPK(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Tag4`", "`LogID` = @LogID", paras_, tm_);
		}
		public async Task<string> GetTag4ByPKAsync(string logID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LogID", logID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Tag4`", "`LogID` = @LogID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RecDate（字段）
		/// </summary>
		/// /// <param name = "logID">日志编码GUID</param>
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
		#region GetByOperType
		
		/// <summary>
		/// 按 OperType（字段） 查询
		/// </summary>
		/// /// <param name = "operType">操作的种类（根据业务自行约定）</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByOperType(string operType)
		{
			return GetByOperType(operType, 0, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByOperTypeAsync(string operType)
		{
			return await GetByOperTypeAsync(operType, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 OperType（字段） 查询
		/// </summary>
		/// /// <param name = "operType">操作的种类（根据业务自行约定）</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByOperType(string operType, TransactionManager tm_)
		{
			return GetByOperType(operType, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByOperTypeAsync(string operType, TransactionManager tm_)
		{
			return await GetByOperTypeAsync(operType, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 OperType（字段） 查询
		/// </summary>
		/// /// <param name = "operType">操作的种类（根据业务自行约定）</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByOperType(string operType, int top_)
		{
			return GetByOperType(operType, top_, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByOperTypeAsync(string operType, int top_)
		{
			return await GetByOperTypeAsync(operType, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 OperType（字段） 查询
		/// </summary>
		/// /// <param name = "operType">操作的种类（根据业务自行约定）</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByOperType(string operType, int top_, TransactionManager tm_)
		{
			return GetByOperType(operType, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByOperTypeAsync(string operType, int top_, TransactionManager tm_)
		{
			return await GetByOperTypeAsync(operType, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 OperType（字段） 查询
		/// </summary>
		/// /// <param name = "operType">操作的种类（根据业务自行约定）</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByOperType(string operType, string sort_)
		{
			return GetByOperType(operType, 0, sort_, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByOperTypeAsync(string operType, string sort_)
		{
			return await GetByOperTypeAsync(operType, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 OperType（字段） 查询
		/// </summary>
		/// /// <param name = "operType">操作的种类（根据业务自行约定）</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByOperType(string operType, string sort_, TransactionManager tm_)
		{
			return GetByOperType(operType, 0, sort_, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByOperTypeAsync(string operType, string sort_, TransactionManager tm_)
		{
			return await GetByOperTypeAsync(operType, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 OperType（字段） 查询
		/// </summary>
		/// /// <param name = "operType">操作的种类（根据业务自行约定）</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByOperType(string operType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(operType != null ? "`OperType` = @OperType" : "`OperType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (operType != null)
				paras_.Add(Database.CreateInParameter("@OperType", operType, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		public async Task<List<Admin_oper_logEO>> GetByOperTypeAsync(string operType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(operType != null ? "`OperType` = @OperType" : "`OperType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (operType != null)
				paras_.Add(Database.CreateInParameter("@OperType", operType, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		#endregion // GetByOperType
		#region GetByTitle
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">操作描述</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTitle(string title)
		{
			return GetByTitle(title, 0, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTitleAsync(string title)
		{
			return await GetByTitleAsync(title, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">操作描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTitle(string title, TransactionManager tm_)
		{
			return GetByTitle(title, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTitleAsync(string title, TransactionManager tm_)
		{
			return await GetByTitleAsync(title, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">操作描述</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTitle(string title, int top_)
		{
			return GetByTitle(title, top_, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTitleAsync(string title, int top_)
		{
			return await GetByTitleAsync(title, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">操作描述</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTitle(string title, int top_, TransactionManager tm_)
		{
			return GetByTitle(title, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTitleAsync(string title, int top_, TransactionManager tm_)
		{
			return await GetByTitleAsync(title, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">操作描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTitle(string title, string sort_)
		{
			return GetByTitle(title, 0, sort_, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTitleAsync(string title, string sort_)
		{
			return await GetByTitleAsync(title, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">操作描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTitle(string title, string sort_, TransactionManager tm_)
		{
			return GetByTitle(title, 0, sort_, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTitleAsync(string title, string sort_, TransactionManager tm_)
		{
			return await GetByTitleAsync(title, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">操作描述</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTitle(string title, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(title != null ? "`Title` = @Title" : "`Title` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (title != null)
				paras_.Add(Database.CreateInParameter("@Title", title, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		public async Task<List<Admin_oper_logEO>> GetByTitleAsync(string title, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(title != null ? "`Title` = @Title" : "`Title` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (title != null)
				paras_.Add(Database.CreateInParameter("@Title", title, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		#endregion // GetByTitle
		#region GetByContent
		
		/// <summary>
		/// 按 Content（字段） 查询
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByContent(string content)
		{
			return GetByContent(content, 0, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByContentAsync(string content)
		{
			return await GetByContentAsync(content, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Content（字段） 查询
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByContent(string content, TransactionManager tm_)
		{
			return GetByContent(content, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByContentAsync(string content, TransactionManager tm_)
		{
			return await GetByContentAsync(content, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Content（字段） 查询
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByContent(string content, int top_)
		{
			return GetByContent(content, top_, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByContentAsync(string content, int top_)
		{
			return await GetByContentAsync(content, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Content（字段） 查询
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByContent(string content, int top_, TransactionManager tm_)
		{
			return GetByContent(content, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByContentAsync(string content, int top_, TransactionManager tm_)
		{
			return await GetByContentAsync(content, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Content（字段） 查询
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByContent(string content, string sort_)
		{
			return GetByContent(content, 0, sort_, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByContentAsync(string content, string sort_)
		{
			return await GetByContentAsync(content, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Content（字段） 查询
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByContent(string content, string sort_, TransactionManager tm_)
		{
			return GetByContent(content, 0, sort_, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByContentAsync(string content, string sort_, TransactionManager tm_)
		{
			return await GetByContentAsync(content, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Content（字段） 查询
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByContent(string content, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(content != null ? "`Content` = @Content" : "`Content` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (content != null)
				paras_.Add(Database.CreateInParameter("@Content", content, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		public async Task<List<Admin_oper_logEO>> GetByContentAsync(string content, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(content != null ? "`Content` = @Content" : "`Content` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (content != null)
				paras_.Add(Database.CreateInParameter("@Content", content, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		#endregion // GetByContent
		#region GetByTag1
		
		/// <summary>
		/// 按 Tag1（字段） 查询
		/// </summary>
		/// /// <param name = "tag1">标记1</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag1(string tag1)
		{
			return GetByTag1(tag1, 0, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag1Async(string tag1)
		{
			return await GetByTag1Async(tag1, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Tag1（字段） 查询
		/// </summary>
		/// /// <param name = "tag1">标记1</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag1(string tag1, TransactionManager tm_)
		{
			return GetByTag1(tag1, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag1Async(string tag1, TransactionManager tm_)
		{
			return await GetByTag1Async(tag1, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Tag1（字段） 查询
		/// </summary>
		/// /// <param name = "tag1">标记1</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag1(string tag1, int top_)
		{
			return GetByTag1(tag1, top_, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag1Async(string tag1, int top_)
		{
			return await GetByTag1Async(tag1, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Tag1（字段） 查询
		/// </summary>
		/// /// <param name = "tag1">标记1</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag1(string tag1, int top_, TransactionManager tm_)
		{
			return GetByTag1(tag1, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag1Async(string tag1, int top_, TransactionManager tm_)
		{
			return await GetByTag1Async(tag1, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Tag1（字段） 查询
		/// </summary>
		/// /// <param name = "tag1">标记1</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag1(string tag1, string sort_)
		{
			return GetByTag1(tag1, 0, sort_, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag1Async(string tag1, string sort_)
		{
			return await GetByTag1Async(tag1, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Tag1（字段） 查询
		/// </summary>
		/// /// <param name = "tag1">标记1</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag1(string tag1, string sort_, TransactionManager tm_)
		{
			return GetByTag1(tag1, 0, sort_, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag1Async(string tag1, string sort_, TransactionManager tm_)
		{
			return await GetByTag1Async(tag1, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Tag1（字段） 查询
		/// </summary>
		/// /// <param name = "tag1">标记1</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag1(string tag1, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(tag1 != null ? "`Tag1` = @Tag1" : "`Tag1` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (tag1 != null)
				paras_.Add(Database.CreateInParameter("@Tag1", tag1, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag1Async(string tag1, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(tag1 != null ? "`Tag1` = @Tag1" : "`Tag1` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (tag1 != null)
				paras_.Add(Database.CreateInParameter("@Tag1", tag1, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		#endregion // GetByTag1
		#region GetByTag2
		
		/// <summary>
		/// 按 Tag2（字段） 查询
		/// </summary>
		/// /// <param name = "tag2">标记2</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag2(string tag2)
		{
			return GetByTag2(tag2, 0, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag2Async(string tag2)
		{
			return await GetByTag2Async(tag2, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Tag2（字段） 查询
		/// </summary>
		/// /// <param name = "tag2">标记2</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag2(string tag2, TransactionManager tm_)
		{
			return GetByTag2(tag2, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag2Async(string tag2, TransactionManager tm_)
		{
			return await GetByTag2Async(tag2, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Tag2（字段） 查询
		/// </summary>
		/// /// <param name = "tag2">标记2</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag2(string tag2, int top_)
		{
			return GetByTag2(tag2, top_, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag2Async(string tag2, int top_)
		{
			return await GetByTag2Async(tag2, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Tag2（字段） 查询
		/// </summary>
		/// /// <param name = "tag2">标记2</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag2(string tag2, int top_, TransactionManager tm_)
		{
			return GetByTag2(tag2, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag2Async(string tag2, int top_, TransactionManager tm_)
		{
			return await GetByTag2Async(tag2, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Tag2（字段） 查询
		/// </summary>
		/// /// <param name = "tag2">标记2</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag2(string tag2, string sort_)
		{
			return GetByTag2(tag2, 0, sort_, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag2Async(string tag2, string sort_)
		{
			return await GetByTag2Async(tag2, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Tag2（字段） 查询
		/// </summary>
		/// /// <param name = "tag2">标记2</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag2(string tag2, string sort_, TransactionManager tm_)
		{
			return GetByTag2(tag2, 0, sort_, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag2Async(string tag2, string sort_, TransactionManager tm_)
		{
			return await GetByTag2Async(tag2, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Tag2（字段） 查询
		/// </summary>
		/// /// <param name = "tag2">标记2</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag2(string tag2, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(tag2 != null ? "`Tag2` = @Tag2" : "`Tag2` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (tag2 != null)
				paras_.Add(Database.CreateInParameter("@Tag2", tag2, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag2Async(string tag2, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(tag2 != null ? "`Tag2` = @Tag2" : "`Tag2` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (tag2 != null)
				paras_.Add(Database.CreateInParameter("@Tag2", tag2, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		#endregion // GetByTag2
		#region GetByTag3
		
		/// <summary>
		/// 按 Tag3（字段） 查询
		/// </summary>
		/// /// <param name = "tag3">标记3</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag3(string tag3)
		{
			return GetByTag3(tag3, 0, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag3Async(string tag3)
		{
			return await GetByTag3Async(tag3, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Tag3（字段） 查询
		/// </summary>
		/// /// <param name = "tag3">标记3</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag3(string tag3, TransactionManager tm_)
		{
			return GetByTag3(tag3, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag3Async(string tag3, TransactionManager tm_)
		{
			return await GetByTag3Async(tag3, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Tag3（字段） 查询
		/// </summary>
		/// /// <param name = "tag3">标记3</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag3(string tag3, int top_)
		{
			return GetByTag3(tag3, top_, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag3Async(string tag3, int top_)
		{
			return await GetByTag3Async(tag3, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Tag3（字段） 查询
		/// </summary>
		/// /// <param name = "tag3">标记3</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag3(string tag3, int top_, TransactionManager tm_)
		{
			return GetByTag3(tag3, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag3Async(string tag3, int top_, TransactionManager tm_)
		{
			return await GetByTag3Async(tag3, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Tag3（字段） 查询
		/// </summary>
		/// /// <param name = "tag3">标记3</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag3(string tag3, string sort_)
		{
			return GetByTag3(tag3, 0, sort_, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag3Async(string tag3, string sort_)
		{
			return await GetByTag3Async(tag3, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Tag3（字段） 查询
		/// </summary>
		/// /// <param name = "tag3">标记3</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag3(string tag3, string sort_, TransactionManager tm_)
		{
			return GetByTag3(tag3, 0, sort_, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag3Async(string tag3, string sort_, TransactionManager tm_)
		{
			return await GetByTag3Async(tag3, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Tag3（字段） 查询
		/// </summary>
		/// /// <param name = "tag3">标记3</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag3(string tag3, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(tag3 != null ? "`Tag3` = @Tag3" : "`Tag3` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (tag3 != null)
				paras_.Add(Database.CreateInParameter("@Tag3", tag3, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag3Async(string tag3, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(tag3 != null ? "`Tag3` = @Tag3" : "`Tag3` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (tag3 != null)
				paras_.Add(Database.CreateInParameter("@Tag3", tag3, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		#endregion // GetByTag3
		#region GetByTag4
		
		/// <summary>
		/// 按 Tag4（字段） 查询
		/// </summary>
		/// /// <param name = "tag4">标记3</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag4(string tag4)
		{
			return GetByTag4(tag4, 0, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag4Async(string tag4)
		{
			return await GetByTag4Async(tag4, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Tag4（字段） 查询
		/// </summary>
		/// /// <param name = "tag4">标记3</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag4(string tag4, TransactionManager tm_)
		{
			return GetByTag4(tag4, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag4Async(string tag4, TransactionManager tm_)
		{
			return await GetByTag4Async(tag4, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Tag4（字段） 查询
		/// </summary>
		/// /// <param name = "tag4">标记3</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag4(string tag4, int top_)
		{
			return GetByTag4(tag4, top_, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag4Async(string tag4, int top_)
		{
			return await GetByTag4Async(tag4, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Tag4（字段） 查询
		/// </summary>
		/// /// <param name = "tag4">标记3</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag4(string tag4, int top_, TransactionManager tm_)
		{
			return GetByTag4(tag4, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag4Async(string tag4, int top_, TransactionManager tm_)
		{
			return await GetByTag4Async(tag4, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Tag4（字段） 查询
		/// </summary>
		/// /// <param name = "tag4">标记3</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag4(string tag4, string sort_)
		{
			return GetByTag4(tag4, 0, sort_, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag4Async(string tag4, string sort_)
		{
			return await GetByTag4Async(tag4, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Tag4（字段） 查询
		/// </summary>
		/// /// <param name = "tag4">标记3</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag4(string tag4, string sort_, TransactionManager tm_)
		{
			return GetByTag4(tag4, 0, sort_, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag4Async(string tag4, string sort_, TransactionManager tm_)
		{
			return await GetByTag4Async(tag4, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Tag4（字段） 查询
		/// </summary>
		/// /// <param name = "tag4">标记3</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByTag4(string tag4, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(tag4 != null ? "`Tag4` = @Tag4" : "`Tag4` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (tag4 != null)
				paras_.Add(Database.CreateInParameter("@Tag4", tag4, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		public async Task<List<Admin_oper_logEO>> GetByTag4Async(string tag4, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(tag4 != null ? "`Tag4` = @Tag4" : "`Tag4` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (tag4 != null)
				paras_.Add(Database.CreateInParameter("@Tag4", tag4, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		#endregion // GetByTag4
		#region GetByRecDate
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录时间</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByRecDate(DateTime recDate)
		{
			return GetByRecDate(recDate, 0, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByRecDateAsync(DateTime recDate)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByRecDate(DateTime recDate, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByRecDateAsync(DateTime recDate, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录时间</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByRecDate(DateTime recDate, int top_)
		{
			return GetByRecDate(recDate, top_, string.Empty, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByRecDateAsync(DateTime recDate, int top_)
		{
			return await GetByRecDateAsync(recDate, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByRecDate(DateTime recDate, int top_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByRecDateAsync(DateTime recDate, int top_, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByRecDate(DateTime recDate, string sort_)
		{
			return GetByRecDate(recDate, 0, sort_, null);
		}
		public async Task<List<Admin_oper_logEO>> GetByRecDateAsync(DateTime recDate, string sort_)
		{
			return await GetByRecDateAsync(recDate, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByRecDate(DateTime recDate, string sort_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, sort_, tm_);
		}
		public async Task<List<Admin_oper_logEO>> GetByRecDateAsync(DateTime recDate, string sort_, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_oper_logEO> GetByRecDate(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		public async Task<List<Admin_oper_logEO>> GetByRecDateAsync(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_oper_logEO.MapDataReader);
		}
		#endregion // GetByRecDate
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
