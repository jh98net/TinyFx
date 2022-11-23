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
	/// 消息
	/// 【表 admin_msg 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_msgEO : IRowMapper<Admin_msgEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_msgEO()
		{
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
		private string _originalMsgID;
		/// <summary>
		/// 【数据库中的原始主键 MsgID 值的副本，用于主键值更新】
		/// </summary>
		public string OriginalMsgID
		{
			get { return _originalMsgID; }
			set { HasOriginal = true; _originalMsgID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "MsgID", MsgID }, };
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
		public string MsgID { get; set; }
		/// <summary>
		/// 用户
		/// 【字段 varchar(40)】
		/// </summary>
		[DataMember(Order = 2)]
		public string UserID { get; set; }
		/// <summary>
		/// 标识
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 3)]
		public int Flag { get; set; }
		/// <summary>
		/// 来源
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 4)]
		public string From { get; set; }
		/// <summary>
		/// 标题
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 5)]
		public string Title { get; set; }
		/// <summary>
		/// 内容
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 6)]
		public string Content { get; set; }
		/// <summary>
		/// 
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 7)]
		public string Label { get; set; }
		/// <summary>
		/// 状态0-未知1-有效2-删除
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 8)]
		public int Status { get; set; }
		/// <summary>
		/// 发送时间
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 9)]
		public DateTime SendDate { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_msgEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_msgEO MapDataReader(IDataReader reader)
		{
		    Admin_msgEO ret = new Admin_msgEO();
			ret.MsgID = reader.ToString("MsgID");
			ret.OriginalMsgID = ret.MsgID;
			ret.UserID = reader.ToString("UserID");
			ret.Flag = reader.ToInt32("Flag");
			ret.From = reader.ToString("From");
			ret.Title = reader.ToString("Title");
			ret.Content = reader.ToString("Content");
			ret.Label = reader.ToString("Label");
			ret.Status = reader.ToInt32("Status");
			ret.SendDate = reader.ToDateTime("SendDate");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 消息
	/// 【表 admin_msg 的操作类】
	/// </summary>
	public class Admin_msgMO : MySqlTableMO<Admin_msgEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_msg`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_msgMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_msgMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_msgMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_msgEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_msgEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_msgEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_msg` (`MsgID`, `UserID`, `Flag`, `From`, `Title`, `Content`, `Label`, `Status`, `SendDate`) VALUE (@MsgID, @UserID, @Flag, @From, @Title, @Content, @Label, @Status, @SendDate);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", item.MsgID, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", item.UserID != null ? item.UserID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Flag", item.Flag, MySqlDbType.Int32),
				Database.CreateInParameter("@From", item.From != null ? item.From : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Title", item.Title != null ? item.Title : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Content", item.Content != null ? item.Content : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Label", item.Label != null ? item.Label : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Status", item.Status, MySqlDbType.Int32),
				Database.CreateInParameter("@SendDate", item.SendDate, MySqlDbType.DateTime),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(string msgID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(msgID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(string msgID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(msgID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(string msgID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_msg` WHERE `MsgID` = @MsgID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_msgEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.MsgID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_msgEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.MsgID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByUserID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "userID">用户</param>
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
			sql_ = @"DELETE FROM `admin_msg` WHERE " + (userID != null ? "`UserID` = @UserID" : "`UserID` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (userID != null)
				paras_.Add(Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar));
		}
		#endregion // RemoveByUserID
		#region RemoveByFlag
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "flag">标识</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByFlag(int flag, TransactionManager tm_ = null)
		{
			RepairRemoveByFlagData(flag, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByFlagAsync(int flag, TransactionManager tm_ = null)
		{
			RepairRemoveByFlagData(flag, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByFlagData(int flag, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_msg` WHERE `Flag` = @Flag";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Flag", flag, MySqlDbType.Int32));
		}
		#endregion // RemoveByFlag
		#region RemoveByFrom
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "from">来源</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByFrom(string from, TransactionManager tm_ = null)
		{
			RepairRemoveByFromData(from, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByFromAsync(string from, TransactionManager tm_ = null)
		{
			RepairRemoveByFromData(from, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByFromData(string from, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_msg` WHERE " + (from != null ? "`From` = @From" : "`From` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (from != null)
				paras_.Add(Database.CreateInParameter("@From", from, MySqlDbType.VarChar));
		}
		#endregion // RemoveByFrom
		#region RemoveByTitle
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "title">标题</param>
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
			sql_ = @"DELETE FROM `admin_msg` WHERE " + (title != null ? "`Title` = @Title" : "`Title` IS NULL");
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
			sql_ = @"DELETE FROM `admin_msg` WHERE " + (content != null ? "`Content` = @Content" : "`Content` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (content != null)
				paras_.Add(Database.CreateInParameter("@Content", content, MySqlDbType.Text));
		}
		#endregion // RemoveByContent
		#region RemoveByLabel
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "label"></param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByLabel(string label, TransactionManager tm_ = null)
		{
			RepairRemoveByLabelData(label, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByLabelAsync(string label, TransactionManager tm_ = null)
		{
			RepairRemoveByLabelData(label, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByLabelData(string label, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_msg` WHERE " + (label != null ? "`Label` = @Label" : "`Label` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (label != null)
				paras_.Add(Database.CreateInParameter("@Label", label, MySqlDbType.VarChar));
		}
		#endregion // RemoveByLabel
		#region RemoveByStatus
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "status">状态0-未知1-有效2-删除</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByStatus(int status, TransactionManager tm_ = null)
		{
			RepairRemoveByStatusData(status, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByStatusAsync(int status, TransactionManager tm_ = null)
		{
			RepairRemoveByStatusData(status, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByStatusData(int status, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_msg` WHERE `Status` = @Status";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Int32));
		}
		#endregion // RemoveByStatus
		#region RemoveBySendDate
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "sendDate">发送时间</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveBySendDate(DateTime sendDate, TransactionManager tm_ = null)
		{
			RepairRemoveBySendDateData(sendDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveBySendDateAsync(DateTime sendDate, TransactionManager tm_ = null)
		{
			RepairRemoveBySendDateData(sendDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveBySendDateData(DateTime sendDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_msg` WHERE `SendDate` = @SendDate";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@SendDate", sendDate, MySqlDbType.DateTime));
		}
		#endregion // RemoveBySendDate
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
		public int Put(Admin_msgEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_msgEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_msgEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_msg` SET `MsgID` = @MsgID, `UserID` = @UserID, `Flag` = @Flag, `From` = @From, `Title` = @Title, `Content` = @Content, `Label` = @Label, `Status` = @Status, `SendDate` = @SendDate WHERE `MsgID` = @MsgID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", item.MsgID, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", item.UserID != null ? item.UserID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Flag", item.Flag, MySqlDbType.Int32),
				Database.CreateInParameter("@From", item.From != null ? item.From : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Title", item.Title != null ? item.Title : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Content", item.Content != null ? item.Content : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Label", item.Label != null ? item.Label : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Status", item.Status, MySqlDbType.Int32),
				Database.CreateInParameter("@SendDate", item.SendDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@MsgID_Original", item.HasOriginal ? item.OriginalMsgID : item.MsgID, MySqlDbType.VarChar),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_msgEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_msgEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string msgID, string set_, params object[] values_)
		{
			return Put(set_, "`MsgID` = @MsgID", ConcatValues(values_, msgID));
		}
		public async Task<int> PutByPKAsync(string msgID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`MsgID` = @MsgID", ConcatValues(values_, msgID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string msgID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`MsgID` = @MsgID", tm_, ConcatValues(values_, msgID));
		}
		public async Task<int> PutByPKAsync(string msgID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`MsgID` = @MsgID", tm_, ConcatValues(values_, msgID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string msgID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
	        };
			return Put(set_, "`MsgID` = @MsgID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(string msgID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
	        };
			return await PutAsync(set_, "`MsgID` = @MsgID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutUserID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// /// <param name = "userID">用户</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUserIDByPK(string msgID, string userID, TransactionManager tm_ = null)
		{
			RepairPutUserIDByPKData(msgID, userID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutUserIDByPKAsync(string msgID, string userID, TransactionManager tm_ = null)
		{
			RepairPutUserIDByPKData(msgID, userID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutUserIDByPKData(string msgID, string userID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_msg` SET `UserID` = @UserID  WHERE `MsgID` = @MsgID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID != null ? userID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "userID">用户</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUserID(string userID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `UserID` = @UserID";
			var parameter_ = Database.CreateInParameter("@UserID", userID != null ? userID : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutUserIDAsync(string userID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `UserID` = @UserID";
			var parameter_ = Database.CreateInParameter("@UserID", userID != null ? userID : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutUserID
		#region PutFlag
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// /// <param name = "flag">标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFlagByPK(string msgID, int flag, TransactionManager tm_ = null)
		{
			RepairPutFlagByPKData(msgID, flag, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutFlagByPKAsync(string msgID, int flag, TransactionManager tm_ = null)
		{
			RepairPutFlagByPKData(msgID, flag, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutFlagByPKData(string msgID, int flag, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_msg` SET `Flag` = @Flag  WHERE `MsgID` = @MsgID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Flag", flag, MySqlDbType.Int32),
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "flag">标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFlag(int flag, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `Flag` = @Flag";
			var parameter_ = Database.CreateInParameter("@Flag", flag, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutFlagAsync(int flag, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `Flag` = @Flag";
			var parameter_ = Database.CreateInParameter("@Flag", flag, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutFlag
		#region PutFrom
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// /// <param name = "from">来源</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFromByPK(string msgID, string from, TransactionManager tm_ = null)
		{
			RepairPutFromByPKData(msgID, from, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutFromByPKAsync(string msgID, string from, TransactionManager tm_ = null)
		{
			RepairPutFromByPKData(msgID, from, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutFromByPKData(string msgID, string from, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_msg` SET `From` = @From  WHERE `MsgID` = @MsgID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@From", from != null ? from : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "from">来源</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFrom(string from, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `From` = @From";
			var parameter_ = Database.CreateInParameter("@From", from != null ? from : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutFromAsync(string from, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `From` = @From";
			var parameter_ = Database.CreateInParameter("@From", from != null ? from : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutFrom
		#region PutTitle
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// /// <param name = "title">标题</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTitleByPK(string msgID, string title, TransactionManager tm_ = null)
		{
			RepairPutTitleByPKData(msgID, title, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTitleByPKAsync(string msgID, string title, TransactionManager tm_ = null)
		{
			RepairPutTitleByPKData(msgID, title, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTitleByPKData(string msgID, string title, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_msg` SET `Title` = @Title  WHERE `MsgID` = @MsgID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Title", title != null ? title : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "title">标题</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTitle(string title, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `Title` = @Title";
			var parameter_ = Database.CreateInParameter("@Title", title != null ? title : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTitleAsync(string title, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `Title` = @Title";
			var parameter_ = Database.CreateInParameter("@Title", title != null ? title : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTitle
		#region PutContent
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// /// <param name = "content">内容</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutContentByPK(string msgID, string content, TransactionManager tm_ = null)
		{
			RepairPutContentByPKData(msgID, content, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutContentByPKAsync(string msgID, string content, TransactionManager tm_ = null)
		{
			RepairPutContentByPKData(msgID, content, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutContentByPKData(string msgID, string content, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_msg` SET `Content` = @Content  WHERE `MsgID` = @MsgID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Content", content != null ? content : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
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
			const string sql_ = @"UPDATE `admin_msg` SET `Content` = @Content";
			var parameter_ = Database.CreateInParameter("@Content", content != null ? content : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutContentAsync(string content, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `Content` = @Content";
			var parameter_ = Database.CreateInParameter("@Content", content != null ? content : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutContent
		#region PutLabel
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// /// <param name = "label"></param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLabelByPK(string msgID, string label, TransactionManager tm_ = null)
		{
			RepairPutLabelByPKData(msgID, label, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutLabelByPKAsync(string msgID, string label, TransactionManager tm_ = null)
		{
			RepairPutLabelByPKData(msgID, label, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutLabelByPKData(string msgID, string label, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_msg` SET `Label` = @Label  WHERE `MsgID` = @MsgID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Label", label != null ? label : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "label"></param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLabel(string label, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `Label` = @Label";
			var parameter_ = Database.CreateInParameter("@Label", label != null ? label : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutLabelAsync(string label, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `Label` = @Label";
			var parameter_ = Database.CreateInParameter("@Label", label != null ? label : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutLabel
		#region PutStatus
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// /// <param name = "status">状态0-未知1-有效2-删除</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutStatusByPK(string msgID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(msgID, status, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutStatusByPKAsync(string msgID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(msgID, status, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutStatusByPKData(string msgID, int status, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_msg` SET `Status` = @Status  WHERE `MsgID` = @MsgID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Status", status, MySqlDbType.Int32),
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "status">状态0-未知1-有效2-删除</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutStatus(int status, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `Status` = @Status";
			var parameter_ = Database.CreateInParameter("@Status", status, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutStatusAsync(int status, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `Status` = @Status";
			var parameter_ = Database.CreateInParameter("@Status", status, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutStatus
		#region PutSendDate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// /// <param name = "sendDate">发送时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutSendDateByPK(string msgID, DateTime sendDate, TransactionManager tm_ = null)
		{
			RepairPutSendDateByPKData(msgID, sendDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutSendDateByPKAsync(string msgID, DateTime sendDate, TransactionManager tm_ = null)
		{
			RepairPutSendDateByPKData(msgID, sendDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutSendDateByPKData(string msgID, DateTime sendDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_msg` SET `SendDate` = @SendDate  WHERE `MsgID` = @MsgID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SendDate", sendDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "sendDate">发送时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutSendDate(DateTime sendDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `SendDate` = @SendDate";
			var parameter_ = Database.CreateInParameter("@SendDate", sendDate, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutSendDateAsync(DateTime sendDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_msg` SET `SendDate` = @SendDate";
			var parameter_ = Database.CreateInParameter("@SendDate", sendDate, MySqlDbType.DateTime);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutSendDate
		#endregion // PutXXX
		#endregion // Put
	   
		#region Set
		
		/// <summary>
		/// 插入或者更新数据
		/// </summary>
		/// <param name = "item">要更新的实体对象</param>
		/// <param name="tm">事务管理对象</param>
		/// <return>true:插入操作；false:更新操作</return>
		public bool Set(Admin_msgEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.MsgID) == null)
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
		public async Task<bool> SetAsync(Admin_msgEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.MsgID) == null)
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
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_msgEO GetByPK(string msgID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(msgID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		public async Task<Admin_msgEO> GetByPKAsync(string msgID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(msgID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		private void RepairGetByPKData(string msgID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`MsgID` = @MsgID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 UserID（字段）
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetUserIDByPK(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`UserID`", "`MsgID` = @MsgID", paras_, tm_);
		}
		public async Task<string> GetUserIDByPKAsync(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`UserID`", "`MsgID` = @MsgID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Flag（字段）
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetFlagByPK(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`Flag`", "`MsgID` = @MsgID", paras_, tm_);
		}
		public async Task<int> GetFlagByPKAsync(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`Flag`", "`MsgID` = @MsgID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 From（字段）
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetFromByPK(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`From`", "`MsgID` = @MsgID", paras_, tm_);
		}
		public async Task<string> GetFromByPKAsync(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`From`", "`MsgID` = @MsgID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Title（字段）
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetTitleByPK(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Title`", "`MsgID` = @MsgID", paras_, tm_);
		}
		public async Task<string> GetTitleByPKAsync(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Title`", "`MsgID` = @MsgID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Content（字段）
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetContentByPK(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Content`", "`MsgID` = @MsgID", paras_, tm_);
		}
		public async Task<string> GetContentByPKAsync(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Content`", "`MsgID` = @MsgID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Label（字段）
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetLabelByPK(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Label`", "`MsgID` = @MsgID", paras_, tm_);
		}
		public async Task<string> GetLabelByPKAsync(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Label`", "`MsgID` = @MsgID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Status（字段）
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetStatusByPK(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`Status`", "`MsgID` = @MsgID", paras_, tm_);
		}
		public async Task<int> GetStatusByPKAsync(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`Status`", "`MsgID` = @MsgID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 SendDate（字段）
		/// </summary>
		/// /// <param name = "msgID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public DateTime GetSendDateByPK(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (DateTime)GetScalar("`SendDate`", "`MsgID` = @MsgID", paras_, tm_);
		}
		public async Task<DateTime> GetSendDateByPKAsync(string msgID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MsgID", msgID, MySqlDbType.VarChar),
			};
			return (DateTime)await GetScalarAsync("`SendDate`", "`MsgID` = @MsgID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByUserID
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">用户</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByUserID(string userID)
		{
			return GetByUserID(userID, 0, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByUserIDAsync(string userID)
		{
			return await GetByUserIDAsync(userID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">用户</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByUserID(string userID, TransactionManager tm_)
		{
			return GetByUserID(userID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByUserIDAsync(string userID, TransactionManager tm_)
		{
			return await GetByUserIDAsync(userID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">用户</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByUserID(string userID, int top_)
		{
			return GetByUserID(userID, top_, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByUserIDAsync(string userID, int top_)
		{
			return await GetByUserIDAsync(userID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">用户</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByUserID(string userID, int top_, TransactionManager tm_)
		{
			return GetByUserID(userID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByUserIDAsync(string userID, int top_, TransactionManager tm_)
		{
			return await GetByUserIDAsync(userID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">用户</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByUserID(string userID, string sort_)
		{
			return GetByUserID(userID, 0, sort_, null);
		}
		public async Task<List<Admin_msgEO>> GetByUserIDAsync(string userID, string sort_)
		{
			return await GetByUserIDAsync(userID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">用户</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByUserID(string userID, string sort_, TransactionManager tm_)
		{
			return GetByUserID(userID, 0, sort_, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByUserIDAsync(string userID, string sort_, TransactionManager tm_)
		{
			return await GetByUserIDAsync(userID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">用户</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByUserID(string userID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(userID != null ? "`UserID` = @UserID" : "`UserID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (userID != null)
				paras_.Add(Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		public async Task<List<Admin_msgEO>> GetByUserIDAsync(string userID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(userID != null ? "`UserID` = @UserID" : "`UserID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (userID != null)
				paras_.Add(Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		#endregion // GetByUserID
		#region GetByFlag
		
		/// <summary>
		/// 按 Flag（字段） 查询
		/// </summary>
		/// /// <param name = "flag">标识</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFlag(int flag)
		{
			return GetByFlag(flag, 0, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByFlagAsync(int flag)
		{
			return await GetByFlagAsync(flag, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Flag（字段） 查询
		/// </summary>
		/// /// <param name = "flag">标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFlag(int flag, TransactionManager tm_)
		{
			return GetByFlag(flag, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByFlagAsync(int flag, TransactionManager tm_)
		{
			return await GetByFlagAsync(flag, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Flag（字段） 查询
		/// </summary>
		/// /// <param name = "flag">标识</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFlag(int flag, int top_)
		{
			return GetByFlag(flag, top_, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByFlagAsync(int flag, int top_)
		{
			return await GetByFlagAsync(flag, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Flag（字段） 查询
		/// </summary>
		/// /// <param name = "flag">标识</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFlag(int flag, int top_, TransactionManager tm_)
		{
			return GetByFlag(flag, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByFlagAsync(int flag, int top_, TransactionManager tm_)
		{
			return await GetByFlagAsync(flag, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Flag（字段） 查询
		/// </summary>
		/// /// <param name = "flag">标识</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFlag(int flag, string sort_)
		{
			return GetByFlag(flag, 0, sort_, null);
		}
		public async Task<List<Admin_msgEO>> GetByFlagAsync(int flag, string sort_)
		{
			return await GetByFlagAsync(flag, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Flag（字段） 查询
		/// </summary>
		/// /// <param name = "flag">标识</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFlag(int flag, string sort_, TransactionManager tm_)
		{
			return GetByFlag(flag, 0, sort_, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByFlagAsync(int flag, string sort_, TransactionManager tm_)
		{
			return await GetByFlagAsync(flag, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Flag（字段） 查询
		/// </summary>
		/// /// <param name = "flag">标识</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFlag(int flag, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Flag` = @Flag", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Flag", flag, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		public async Task<List<Admin_msgEO>> GetByFlagAsync(int flag, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Flag` = @Flag", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Flag", flag, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		#endregion // GetByFlag
		#region GetByFrom
		
		/// <summary>
		/// 按 From（字段） 查询
		/// </summary>
		/// /// <param name = "from">来源</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFrom(string from)
		{
			return GetByFrom(from, 0, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByFromAsync(string from)
		{
			return await GetByFromAsync(from, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 From（字段） 查询
		/// </summary>
		/// /// <param name = "from">来源</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFrom(string from, TransactionManager tm_)
		{
			return GetByFrom(from, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByFromAsync(string from, TransactionManager tm_)
		{
			return await GetByFromAsync(from, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 From（字段） 查询
		/// </summary>
		/// /// <param name = "from">来源</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFrom(string from, int top_)
		{
			return GetByFrom(from, top_, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByFromAsync(string from, int top_)
		{
			return await GetByFromAsync(from, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 From（字段） 查询
		/// </summary>
		/// /// <param name = "from">来源</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFrom(string from, int top_, TransactionManager tm_)
		{
			return GetByFrom(from, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByFromAsync(string from, int top_, TransactionManager tm_)
		{
			return await GetByFromAsync(from, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 From（字段） 查询
		/// </summary>
		/// /// <param name = "from">来源</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFrom(string from, string sort_)
		{
			return GetByFrom(from, 0, sort_, null);
		}
		public async Task<List<Admin_msgEO>> GetByFromAsync(string from, string sort_)
		{
			return await GetByFromAsync(from, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 From（字段） 查询
		/// </summary>
		/// /// <param name = "from">来源</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFrom(string from, string sort_, TransactionManager tm_)
		{
			return GetByFrom(from, 0, sort_, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByFromAsync(string from, string sort_, TransactionManager tm_)
		{
			return await GetByFromAsync(from, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 From（字段） 查询
		/// </summary>
		/// /// <param name = "from">来源</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByFrom(string from, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(from != null ? "`From` = @From" : "`From` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (from != null)
				paras_.Add(Database.CreateInParameter("@From", from, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		public async Task<List<Admin_msgEO>> GetByFromAsync(string from, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(from != null ? "`From` = @From" : "`From` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (from != null)
				paras_.Add(Database.CreateInParameter("@From", from, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		#endregion // GetByFrom
		#region GetByTitle
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">标题</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByTitle(string title)
		{
			return GetByTitle(title, 0, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByTitleAsync(string title)
		{
			return await GetByTitleAsync(title, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">标题</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByTitle(string title, TransactionManager tm_)
		{
			return GetByTitle(title, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByTitleAsync(string title, TransactionManager tm_)
		{
			return await GetByTitleAsync(title, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">标题</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByTitle(string title, int top_)
		{
			return GetByTitle(title, top_, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByTitleAsync(string title, int top_)
		{
			return await GetByTitleAsync(title, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">标题</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByTitle(string title, int top_, TransactionManager tm_)
		{
			return GetByTitle(title, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByTitleAsync(string title, int top_, TransactionManager tm_)
		{
			return await GetByTitleAsync(title, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">标题</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByTitle(string title, string sort_)
		{
			return GetByTitle(title, 0, sort_, null);
		}
		public async Task<List<Admin_msgEO>> GetByTitleAsync(string title, string sort_)
		{
			return await GetByTitleAsync(title, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">标题</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByTitle(string title, string sort_, TransactionManager tm_)
		{
			return GetByTitle(title, 0, sort_, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByTitleAsync(string title, string sort_, TransactionManager tm_)
		{
			return await GetByTitleAsync(title, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">标题</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByTitle(string title, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(title != null ? "`Title` = @Title" : "`Title` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (title != null)
				paras_.Add(Database.CreateInParameter("@Title", title, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		public async Task<List<Admin_msgEO>> GetByTitleAsync(string title, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(title != null ? "`Title` = @Title" : "`Title` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (title != null)
				paras_.Add(Database.CreateInParameter("@Title", title, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		#endregion // GetByTitle
		#region GetByContent
		
		/// <summary>
		/// 按 Content（字段） 查询
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByContent(string content)
		{
			return GetByContent(content, 0, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByContentAsync(string content)
		{
			return await GetByContentAsync(content, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Content（字段） 查询
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByContent(string content, TransactionManager tm_)
		{
			return GetByContent(content, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByContentAsync(string content, TransactionManager tm_)
		{
			return await GetByContentAsync(content, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Content（字段） 查询
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByContent(string content, int top_)
		{
			return GetByContent(content, top_, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByContentAsync(string content, int top_)
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
		public List<Admin_msgEO> GetByContent(string content, int top_, TransactionManager tm_)
		{
			return GetByContent(content, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByContentAsync(string content, int top_, TransactionManager tm_)
		{
			return await GetByContentAsync(content, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Content（字段） 查询
		/// </summary>
		/// /// <param name = "content">内容</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByContent(string content, string sort_)
		{
			return GetByContent(content, 0, sort_, null);
		}
		public async Task<List<Admin_msgEO>> GetByContentAsync(string content, string sort_)
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
		public List<Admin_msgEO> GetByContent(string content, string sort_, TransactionManager tm_)
		{
			return GetByContent(content, 0, sort_, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByContentAsync(string content, string sort_, TransactionManager tm_)
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
		public List<Admin_msgEO> GetByContent(string content, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(content != null ? "`Content` = @Content" : "`Content` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (content != null)
				paras_.Add(Database.CreateInParameter("@Content", content, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		public async Task<List<Admin_msgEO>> GetByContentAsync(string content, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(content != null ? "`Content` = @Content" : "`Content` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (content != null)
				paras_.Add(Database.CreateInParameter("@Content", content, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		#endregion // GetByContent
		#region GetByLabel
		
		/// <summary>
		/// 按 Label（字段） 查询
		/// </summary>
		/// /// <param name = "label"></param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByLabel(string label)
		{
			return GetByLabel(label, 0, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByLabelAsync(string label)
		{
			return await GetByLabelAsync(label, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Label（字段） 查询
		/// </summary>
		/// /// <param name = "label"></param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByLabel(string label, TransactionManager tm_)
		{
			return GetByLabel(label, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByLabelAsync(string label, TransactionManager tm_)
		{
			return await GetByLabelAsync(label, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Label（字段） 查询
		/// </summary>
		/// /// <param name = "label"></param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByLabel(string label, int top_)
		{
			return GetByLabel(label, top_, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByLabelAsync(string label, int top_)
		{
			return await GetByLabelAsync(label, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Label（字段） 查询
		/// </summary>
		/// /// <param name = "label"></param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByLabel(string label, int top_, TransactionManager tm_)
		{
			return GetByLabel(label, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByLabelAsync(string label, int top_, TransactionManager tm_)
		{
			return await GetByLabelAsync(label, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Label（字段） 查询
		/// </summary>
		/// /// <param name = "label"></param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByLabel(string label, string sort_)
		{
			return GetByLabel(label, 0, sort_, null);
		}
		public async Task<List<Admin_msgEO>> GetByLabelAsync(string label, string sort_)
		{
			return await GetByLabelAsync(label, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Label（字段） 查询
		/// </summary>
		/// /// <param name = "label"></param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByLabel(string label, string sort_, TransactionManager tm_)
		{
			return GetByLabel(label, 0, sort_, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByLabelAsync(string label, string sort_, TransactionManager tm_)
		{
			return await GetByLabelAsync(label, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Label（字段） 查询
		/// </summary>
		/// /// <param name = "label"></param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByLabel(string label, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(label != null ? "`Label` = @Label" : "`Label` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (label != null)
				paras_.Add(Database.CreateInParameter("@Label", label, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		public async Task<List<Admin_msgEO>> GetByLabelAsync(string label, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(label != null ? "`Label` = @Label" : "`Label` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (label != null)
				paras_.Add(Database.CreateInParameter("@Label", label, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		#endregion // GetByLabel
		#region GetByStatus
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态0-未知1-有效2-删除</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByStatus(int status)
		{
			return GetByStatus(status, 0, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByStatusAsync(int status)
		{
			return await GetByStatusAsync(status, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态0-未知1-有效2-删除</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByStatus(int status, TransactionManager tm_)
		{
			return GetByStatus(status, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByStatusAsync(int status, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态0-未知1-有效2-删除</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByStatus(int status, int top_)
		{
			return GetByStatus(status, top_, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetByStatusAsync(int status, int top_)
		{
			return await GetByStatusAsync(status, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态0-未知1-有效2-删除</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByStatus(int status, int top_, TransactionManager tm_)
		{
			return GetByStatus(status, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByStatusAsync(int status, int top_, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态0-未知1-有效2-删除</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByStatus(int status, string sort_)
		{
			return GetByStatus(status, 0, sort_, null);
		}
		public async Task<List<Admin_msgEO>> GetByStatusAsync(int status, string sort_)
		{
			return await GetByStatusAsync(status, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态0-未知1-有效2-删除</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByStatus(int status, string sort_, TransactionManager tm_)
		{
			return GetByStatus(status, 0, sort_, tm_);
		}
		public async Task<List<Admin_msgEO>> GetByStatusAsync(int status, string sort_, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态0-未知1-有效2-删除</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetByStatus(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		public async Task<List<Admin_msgEO>> GetByStatusAsync(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		#endregion // GetByStatus
		#region GetBySendDate
		
		/// <summary>
		/// 按 SendDate（字段） 查询
		/// </summary>
		/// /// <param name = "sendDate">发送时间</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetBySendDate(DateTime sendDate)
		{
			return GetBySendDate(sendDate, 0, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetBySendDateAsync(DateTime sendDate)
		{
			return await GetBySendDateAsync(sendDate, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 SendDate（字段） 查询
		/// </summary>
		/// /// <param name = "sendDate">发送时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetBySendDate(DateTime sendDate, TransactionManager tm_)
		{
			return GetBySendDate(sendDate, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetBySendDateAsync(DateTime sendDate, TransactionManager tm_)
		{
			return await GetBySendDateAsync(sendDate, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 SendDate（字段） 查询
		/// </summary>
		/// /// <param name = "sendDate">发送时间</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetBySendDate(DateTime sendDate, int top_)
		{
			return GetBySendDate(sendDate, top_, string.Empty, null);
		}
		public async Task<List<Admin_msgEO>> GetBySendDateAsync(DateTime sendDate, int top_)
		{
			return await GetBySendDateAsync(sendDate, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 SendDate（字段） 查询
		/// </summary>
		/// /// <param name = "sendDate">发送时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetBySendDate(DateTime sendDate, int top_, TransactionManager tm_)
		{
			return GetBySendDate(sendDate, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_msgEO>> GetBySendDateAsync(DateTime sendDate, int top_, TransactionManager tm_)
		{
			return await GetBySendDateAsync(sendDate, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 SendDate（字段） 查询
		/// </summary>
		/// /// <param name = "sendDate">发送时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetBySendDate(DateTime sendDate, string sort_)
		{
			return GetBySendDate(sendDate, 0, sort_, null);
		}
		public async Task<List<Admin_msgEO>> GetBySendDateAsync(DateTime sendDate, string sort_)
		{
			return await GetBySendDateAsync(sendDate, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 SendDate（字段） 查询
		/// </summary>
		/// /// <param name = "sendDate">发送时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetBySendDate(DateTime sendDate, string sort_, TransactionManager tm_)
		{
			return GetBySendDate(sendDate, 0, sort_, tm_);
		}
		public async Task<List<Admin_msgEO>> GetBySendDateAsync(DateTime sendDate, string sort_, TransactionManager tm_)
		{
			return await GetBySendDateAsync(sendDate, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 SendDate（字段） 查询
		/// </summary>
		/// /// <param name = "sendDate">发送时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_msgEO> GetBySendDate(DateTime sendDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`SendDate` = @SendDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@SendDate", sendDate, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		public async Task<List<Admin_msgEO>> GetBySendDateAsync(DateTime sendDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`SendDate` = @SendDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@SendDate", sendDate, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_msgEO.MapDataReader);
		}
		#endregion // GetBySendDate
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
