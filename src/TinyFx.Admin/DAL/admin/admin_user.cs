/******************************************************
 * 此代码由代码生成器工具自动生成，请不要修改
 * TinyFx代码生成器核心库版本号：1.0.0.0
 * git: https://github.com/jh98net/TinyFx
 * 文档生成时间：2022-11-13 22: 11:30
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
	/// 后台用户表
	/// where status=1 时 username唯一
	/// 【表 admin_user 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_userEO : IRowMapper<Admin_userEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_userEO()
		{
			this.IsAdmin = false;
			this.Status = 0;
			this.RecDate = DateTime.Now;
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
		private string _originalUserID;
		/// <summary>
		/// 【数据库中的原始主键 UserID 值的副本，用于主键值更新】
		/// </summary>
		public string OriginalUserID
		{
			get { return _originalUserID; }
			set { HasOriginal = true; _originalUserID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "UserID", UserID }, };
	    }
	    /// <summary>
	    /// 获取主键集合JSON格式
	    /// </summary>
	    /// <returns></returns>
	    public string GetPrimaryKeysJson() => SerializerUtil.SerializeJson(GetPrimaryKeys());
		#endregion // 主键原始值
		#region 所有字段
		/// <summary>
		/// 用户ID GUID
		/// 【主键 varchar(40)】
		/// </summary>
		[DataMember(Order = 1)]
		public string UserID { get; set; }
		/// <summary>
		/// 登录用户名
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 2)]
		public string UserName { get; set; }
		/// <summary>
		/// 登录密码
		/// 【字段 varchar(255)】
		/// </summary>
		[DataMember(Order = 3)]
		public string Password { get; set; }
		/// <summary>
		/// 密码哈希Salt
		/// 【字段 varchar(40)】
		/// </summary>
		[DataMember(Order = 4)]
		public string PasswordSalt { get; set; }
		/// <summary>
		/// 手机号
		/// 【字段 varchar(20)】
		/// </summary>
		[DataMember(Order = 5)]
		public string Mobile { get; set; }
		/// <summary>
		/// 显示用户名
		/// 【字段 varchar(20)】
		/// </summary>
		[DataMember(Order = 6)]
		public string DisplayName { get; set; }
		/// <summary>
		/// 描述
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 7)]
		public string Desc { get; set; }
		/// <summary>
		/// 分组编码
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 8)]
		public int? GroupID { get; set; }
		/// <summary>
		/// 是否管理员
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 9)]
		public bool IsAdmin { get; set; }
		/// <summary>
		/// 注册时间
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 10)]
		public DateTime RegisterDate { get; set; }
		/// <summary>
		/// 审批时间
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 11)]
		public DateTime? ApprovedDate { get; set; }
		/// <summary>
		/// 审批者
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 12)]
		public string ApprovedBy { get; set; }
		/// <summary>
		/// 拒绝时间
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 13)]
		public DateTime? RejectDate { get; set; }
		/// <summary>
		/// 拒绝者
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 14)]
		public string RejectBy { get; set; }
		/// <summary>
		/// 状态 0-无效 1-有效
		/// 【字段 tinyint】
		/// </summary>
		[DataMember(Order = 15)]
		public int Status { get; set; }
		/// <summary>
		/// 记录日期
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 16)]
		public DateTime RecDate { get; set; }
		/// <summary>
		/// 头像
		/// 【字段 varchar(255)】
		/// </summary>
		[DataMember(Order = 17)]
		public string Icon { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_userEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_userEO MapDataReader(IDataReader reader)
		{
		    Admin_userEO ret = new Admin_userEO();
			ret.UserID = reader.ToString("UserID");
			ret.OriginalUserID = ret.UserID;
			ret.UserName = reader.ToString("UserName");
			ret.Password = reader.ToString("Password");
			ret.PasswordSalt = reader.ToString("PasswordSalt");
			ret.Mobile = reader.ToString("Mobile");
			ret.DisplayName = reader.ToString("DisplayName");
			ret.Desc = reader.ToString("Desc");
			ret.GroupID = reader.ToInt32N("GroupID");
			ret.IsAdmin = reader.ToBoolean("IsAdmin");
			ret.RegisterDate = reader.ToDateTime("RegisterDate");
			ret.ApprovedDate = reader.ToDateTimeN("ApprovedDate");
			ret.ApprovedBy = reader.ToString("ApprovedBy");
			ret.RejectDate = reader.ToDateTimeN("RejectDate");
			ret.RejectBy = reader.ToString("RejectBy");
			ret.Status = reader.ToInt32("Status");
			ret.RecDate = reader.ToDateTime("RecDate");
			ret.Icon = reader.ToString("Icon");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 后台用户表
	/// where status=1 时 username唯一
	/// 【表 admin_user 的操作类】
	/// </summary>
	public class Admin_userMO : MySqlTableMO<Admin_userEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_user`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_userMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_userMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_userMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_userEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_userEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_userEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_user` (`UserID`, `UserName`, `Password`, `PasswordSalt`, `Mobile`, `DisplayName`, `Desc`, `GroupID`, `IsAdmin`, `RegisterDate`, `ApprovedDate`, `ApprovedBy`, `RejectDate`, `RejectBy`, `Status`, `RecDate`, `Icon`) VALUE (@UserID, @UserName, @Password, @PasswordSalt, @Mobile, @DisplayName, @Desc, @GroupID, @IsAdmin, @RegisterDate, @ApprovedDate, @ApprovedBy, @RejectDate, @RejectBy, @Status, @RecDate, @Icon);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", item.UserID, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserName", item.UserName != null ? item.UserName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Password", item.Password != null ? item.Password : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@PasswordSalt", item.PasswordSalt != null ? item.PasswordSalt : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Mobile", item.Mobile != null ? item.Mobile : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DisplayName", item.DisplayName != null ? item.DisplayName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Desc", item.Desc != null ? item.Desc : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@GroupID", item.GroupID.HasValue ? item.GroupID.Value : (object)DBNull.Value, MySqlDbType.Int32),
				Database.CreateInParameter("@IsAdmin", item.IsAdmin, MySqlDbType.Byte),
				Database.CreateInParameter("@RegisterDate", item.RegisterDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@ApprovedDate", item.ApprovedDate.HasValue ? item.ApprovedDate.Value : (object)DBNull.Value, MySqlDbType.DateTime),
				Database.CreateInParameter("@ApprovedBy", item.ApprovedBy != null ? item.ApprovedBy : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RejectDate", item.RejectDate.HasValue ? item.RejectDate.Value : (object)DBNull.Value, MySqlDbType.DateTime),
				Database.CreateInParameter("@RejectBy", item.RejectBy != null ? item.RejectBy : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Status", item.Status, MySqlDbType.Byte),
				Database.CreateInParameter("@RecDate", item.RecDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@Icon", item.Icon != null ? item.Icon : (object)DBNull.Value, MySqlDbType.VarChar),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(string userID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(userID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(string userID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(userID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(string userID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_userEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.UserID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_userEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.UserID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByUserName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "userName">登录用户名</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByUserName(string userName, TransactionManager tm_ = null)
		{
			RepairRemoveByUserNameData(userName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByUserNameAsync(string userName, TransactionManager tm_ = null)
		{
			RepairRemoveByUserNameData(userName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByUserNameData(string userName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (userName != null ? "`UserName` = @UserName" : "`UserName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (userName != null)
				paras_.Add(Database.CreateInParameter("@UserName", userName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByUserName
		#region RemoveByPassword
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "password">登录密码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPassword(string password, TransactionManager tm_ = null)
		{
			RepairRemoveByPasswordData(password, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPasswordAsync(string password, TransactionManager tm_ = null)
		{
			RepairRemoveByPasswordData(password, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByPasswordData(string password, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (password != null ? "`Password` = @Password" : "`Password` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (password != null)
				paras_.Add(Database.CreateInParameter("@Password", password, MySqlDbType.VarChar));
		}
		#endregion // RemoveByPassword
		#region RemoveByPasswordSalt
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "passwordSalt">密码哈希Salt</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPasswordSalt(string passwordSalt, TransactionManager tm_ = null)
		{
			RepairRemoveByPasswordSaltData(passwordSalt, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPasswordSaltAsync(string passwordSalt, TransactionManager tm_ = null)
		{
			RepairRemoveByPasswordSaltData(passwordSalt, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByPasswordSaltData(string passwordSalt, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (passwordSalt != null ? "`PasswordSalt` = @PasswordSalt" : "`PasswordSalt` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (passwordSalt != null)
				paras_.Add(Database.CreateInParameter("@PasswordSalt", passwordSalt, MySqlDbType.VarChar));
		}
		#endregion // RemoveByPasswordSalt
		#region RemoveByMobile
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "mobile">手机号</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByMobile(string mobile, TransactionManager tm_ = null)
		{
			RepairRemoveByMobileData(mobile, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByMobileAsync(string mobile, TransactionManager tm_ = null)
		{
			RepairRemoveByMobileData(mobile, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByMobileData(string mobile, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (mobile != null ? "`Mobile` = @Mobile" : "`Mobile` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (mobile != null)
				paras_.Add(Database.CreateInParameter("@Mobile", mobile, MySqlDbType.VarChar));
		}
		#endregion // RemoveByMobile
		#region RemoveByDisplayName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "displayName">显示用户名</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByDisplayName(string displayName, TransactionManager tm_ = null)
		{
			RepairRemoveByDisplayNameData(displayName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByDisplayNameAsync(string displayName, TransactionManager tm_ = null)
		{
			RepairRemoveByDisplayNameData(displayName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByDisplayNameData(string displayName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (displayName != null ? "`DisplayName` = @DisplayName" : "`DisplayName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (displayName != null)
				paras_.Add(Database.CreateInParameter("@DisplayName", displayName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByDisplayName
		#region RemoveByDesc
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByDesc(string desc, TransactionManager tm_ = null)
		{
			RepairRemoveByDescData(desc, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByDescAsync(string desc, TransactionManager tm_ = null)
		{
			RepairRemoveByDescData(desc, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByDescData(string desc, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.Text));
		}
		#endregion // RemoveByDesc
		#region RemoveByGroupID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "groupID">分组编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByGroupID(int? groupID, TransactionManager tm_ = null)
		{
			RepairRemoveByGroupIDData(groupID.Value, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByGroupIDAsync(int? groupID, TransactionManager tm_ = null)
		{
			RepairRemoveByGroupIDData(groupID.Value, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByGroupIDData(int? groupID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (groupID.HasValue ? "`GroupID` = @GroupID" : "`GroupID` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (groupID.HasValue)
				paras_.Add(Database.CreateInParameter("@GroupID", groupID.Value, MySqlDbType.Int32));
		}
		#endregion // RemoveByGroupID
		#region RemoveByIsAdmin
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "isAdmin">是否管理员</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByIsAdmin(bool isAdmin, TransactionManager tm_ = null)
		{
			RepairRemoveByIsAdminData(isAdmin, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByIsAdminAsync(bool isAdmin, TransactionManager tm_ = null)
		{
			RepairRemoveByIsAdminData(isAdmin, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByIsAdminData(bool isAdmin, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE `IsAdmin` = @IsAdmin";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsAdmin", isAdmin, MySqlDbType.Byte));
		}
		#endregion // RemoveByIsAdmin
		#region RemoveByRegisterDate
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "registerDate">注册时间</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByRegisterDate(DateTime registerDate, TransactionManager tm_ = null)
		{
			RepairRemoveByRegisterDateData(registerDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByRegisterDateAsync(DateTime registerDate, TransactionManager tm_ = null)
		{
			RepairRemoveByRegisterDateData(registerDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByRegisterDateData(DateTime registerDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE `RegisterDate` = @RegisterDate";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RegisterDate", registerDate, MySqlDbType.DateTime));
		}
		#endregion // RemoveByRegisterDate
		#region RemoveByApprovedDate
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "approvedDate">审批时间</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByApprovedDate(DateTime? approvedDate, TransactionManager tm_ = null)
		{
			RepairRemoveByApprovedDateData(approvedDate.Value, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByApprovedDateAsync(DateTime? approvedDate, TransactionManager tm_ = null)
		{
			RepairRemoveByApprovedDateData(approvedDate.Value, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByApprovedDateData(DateTime? approvedDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (approvedDate.HasValue ? "`ApprovedDate` = @ApprovedDate" : "`ApprovedDate` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (approvedDate.HasValue)
				paras_.Add(Database.CreateInParameter("@ApprovedDate", approvedDate.Value, MySqlDbType.DateTime));
		}
		#endregion // RemoveByApprovedDate
		#region RemoveByApprovedBy
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "approvedBy">审批者</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByApprovedBy(string approvedBy, TransactionManager tm_ = null)
		{
			RepairRemoveByApprovedByData(approvedBy, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByApprovedByAsync(string approvedBy, TransactionManager tm_ = null)
		{
			RepairRemoveByApprovedByData(approvedBy, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByApprovedByData(string approvedBy, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (approvedBy != null ? "`ApprovedBy` = @ApprovedBy" : "`ApprovedBy` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (approvedBy != null)
				paras_.Add(Database.CreateInParameter("@ApprovedBy", approvedBy, MySqlDbType.VarChar));
		}
		#endregion // RemoveByApprovedBy
		#region RemoveByRejectDate
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "rejectDate">拒绝时间</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByRejectDate(DateTime? rejectDate, TransactionManager tm_ = null)
		{
			RepairRemoveByRejectDateData(rejectDate.Value, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByRejectDateAsync(DateTime? rejectDate, TransactionManager tm_ = null)
		{
			RepairRemoveByRejectDateData(rejectDate.Value, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByRejectDateData(DateTime? rejectDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (rejectDate.HasValue ? "`RejectDate` = @RejectDate" : "`RejectDate` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (rejectDate.HasValue)
				paras_.Add(Database.CreateInParameter("@RejectDate", rejectDate.Value, MySqlDbType.DateTime));
		}
		#endregion // RemoveByRejectDate
		#region RemoveByRejectBy
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "rejectBy">拒绝者</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByRejectBy(string rejectBy, TransactionManager tm_ = null)
		{
			RepairRemoveByRejectByData(rejectBy, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByRejectByAsync(string rejectBy, TransactionManager tm_ = null)
		{
			RepairRemoveByRejectByData(rejectBy, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByRejectByData(string rejectBy, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (rejectBy != null ? "`RejectBy` = @RejectBy" : "`RejectBy` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (rejectBy != null)
				paras_.Add(Database.CreateInParameter("@RejectBy", rejectBy, MySqlDbType.VarChar));
		}
		#endregion // RemoveByRejectBy
		#region RemoveByStatus
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
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
			sql_ = @"DELETE FROM `admin_user` WHERE `Status` = @Status";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
		}
		#endregion // RemoveByStatus
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
			sql_ = @"DELETE FROM `admin_user` WHERE `RecDate` = @RecDate";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
		}
		#endregion // RemoveByRecDate
		#region RemoveByIcon
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "icon">头像</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByIcon(string icon, TransactionManager tm_ = null)
		{
			RepairRemoveByIconData(icon, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByIconAsync(string icon, TransactionManager tm_ = null)
		{
			RepairRemoveByIconData(icon, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByIconData(string icon, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user` WHERE " + (icon != null ? "`Icon` = @Icon" : "`Icon` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (icon != null)
				paras_.Add(Database.CreateInParameter("@Icon", icon, MySqlDbType.VarChar));
		}
		#endregion // RemoveByIcon
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
		public int Put(Admin_userEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_userEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_userEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `UserID` = @UserID, `UserName` = @UserName, `Password` = @Password, `PasswordSalt` = @PasswordSalt, `Mobile` = @Mobile, `DisplayName` = @DisplayName, `Desc` = @Desc, `GroupID` = @GroupID, `IsAdmin` = @IsAdmin, `RegisterDate` = @RegisterDate, `ApprovedDate` = @ApprovedDate, `ApprovedBy` = @ApprovedBy, `RejectDate` = @RejectDate, `RejectBy` = @RejectBy, `Status` = @Status, `Icon` = @Icon WHERE `UserID` = @UserID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", item.UserID, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserName", item.UserName != null ? item.UserName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Password", item.Password != null ? item.Password : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@PasswordSalt", item.PasswordSalt != null ? item.PasswordSalt : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Mobile", item.Mobile != null ? item.Mobile : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DisplayName", item.DisplayName != null ? item.DisplayName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Desc", item.Desc != null ? item.Desc : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@GroupID", item.GroupID.HasValue ? item.GroupID.Value : (object)DBNull.Value, MySqlDbType.Int32),
				Database.CreateInParameter("@IsAdmin", item.IsAdmin, MySqlDbType.Byte),
				Database.CreateInParameter("@RegisterDate", item.RegisterDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@ApprovedDate", item.ApprovedDate.HasValue ? item.ApprovedDate.Value : (object)DBNull.Value, MySqlDbType.DateTime),
				Database.CreateInParameter("@ApprovedBy", item.ApprovedBy != null ? item.ApprovedBy : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RejectDate", item.RejectDate.HasValue ? item.RejectDate.Value : (object)DBNull.Value, MySqlDbType.DateTime),
				Database.CreateInParameter("@RejectBy", item.RejectBy != null ? item.RejectBy : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Status", item.Status, MySqlDbType.Byte),
				Database.CreateInParameter("@Icon", item.Icon != null ? item.Icon : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID_Original", item.HasOriginal ? item.OriginalUserID : item.UserID, MySqlDbType.VarChar),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_userEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_userEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string userID, string set_, params object[] values_)
		{
			return Put(set_, "`UserID` = @UserID", ConcatValues(values_, userID));
		}
		public async Task<int> PutByPKAsync(string userID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`UserID` = @UserID", ConcatValues(values_, userID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string userID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`UserID` = @UserID", tm_, ConcatValues(values_, userID));
		}
		public async Task<int> PutByPKAsync(string userID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`UserID` = @UserID", tm_, ConcatValues(values_, userID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string userID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
	        };
			return Put(set_, "`UserID` = @UserID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(string userID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
	        };
			return await PutAsync(set_, "`UserID` = @UserID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutUserName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "userName">登录用户名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUserNameByPK(string userID, string userName, TransactionManager tm_ = null)
		{
			RepairPutUserNameByPKData(userID, userName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutUserNameByPKAsync(string userID, string userName, TransactionManager tm_ = null)
		{
			RepairPutUserNameByPKData(userID, userName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutUserNameByPKData(string userID, string userName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `UserName` = @UserName  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserName", userName != null ? userName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "userName">登录用户名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUserName(string userName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `UserName` = @UserName";
			var parameter_ = Database.CreateInParameter("@UserName", userName != null ? userName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutUserNameAsync(string userName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `UserName` = @UserName";
			var parameter_ = Database.CreateInParameter("@UserName", userName != null ? userName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutUserName
		#region PutPassword
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "password">登录密码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPasswordByPK(string userID, string password, TransactionManager tm_ = null)
		{
			RepairPutPasswordByPKData(userID, password, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutPasswordByPKAsync(string userID, string password, TransactionManager tm_ = null)
		{
			RepairPutPasswordByPKData(userID, password, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutPasswordByPKData(string userID, string password, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `Password` = @Password  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Password", password != null ? password : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "password">登录密码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPassword(string password, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `Password` = @Password";
			var parameter_ = Database.CreateInParameter("@Password", password != null ? password : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutPasswordAsync(string password, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `Password` = @Password";
			var parameter_ = Database.CreateInParameter("@Password", password != null ? password : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutPassword
		#region PutPasswordSalt
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "passwordSalt">密码哈希Salt</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPasswordSaltByPK(string userID, string passwordSalt, TransactionManager tm_ = null)
		{
			RepairPutPasswordSaltByPKData(userID, passwordSalt, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutPasswordSaltByPKAsync(string userID, string passwordSalt, TransactionManager tm_ = null)
		{
			RepairPutPasswordSaltByPKData(userID, passwordSalt, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutPasswordSaltByPKData(string userID, string passwordSalt, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `PasswordSalt` = @PasswordSalt  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@PasswordSalt", passwordSalt != null ? passwordSalt : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "passwordSalt">密码哈希Salt</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPasswordSalt(string passwordSalt, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `PasswordSalt` = @PasswordSalt";
			var parameter_ = Database.CreateInParameter("@PasswordSalt", passwordSalt != null ? passwordSalt : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutPasswordSaltAsync(string passwordSalt, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `PasswordSalt` = @PasswordSalt";
			var parameter_ = Database.CreateInParameter("@PasswordSalt", passwordSalt != null ? passwordSalt : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutPasswordSalt
		#region PutMobile
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "mobile">手机号</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutMobileByPK(string userID, string mobile, TransactionManager tm_ = null)
		{
			RepairPutMobileByPKData(userID, mobile, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutMobileByPKAsync(string userID, string mobile, TransactionManager tm_ = null)
		{
			RepairPutMobileByPKData(userID, mobile, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutMobileByPKData(string userID, string mobile, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `Mobile` = @Mobile  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Mobile", mobile != null ? mobile : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "mobile">手机号</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutMobile(string mobile, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `Mobile` = @Mobile";
			var parameter_ = Database.CreateInParameter("@Mobile", mobile != null ? mobile : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutMobileAsync(string mobile, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `Mobile` = @Mobile";
			var parameter_ = Database.CreateInParameter("@Mobile", mobile != null ? mobile : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutMobile
		#region PutDisplayName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "displayName">显示用户名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDisplayNameByPK(string userID, string displayName, TransactionManager tm_ = null)
		{
			RepairPutDisplayNameByPKData(userID, displayName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDisplayNameByPKAsync(string userID, string displayName, TransactionManager tm_ = null)
		{
			RepairPutDisplayNameByPKData(userID, displayName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDisplayNameByPKData(string userID, string displayName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `DisplayName` = @DisplayName  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DisplayName", displayName != null ? displayName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "displayName">显示用户名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDisplayName(string displayName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `DisplayName` = @DisplayName";
			var parameter_ = Database.CreateInParameter("@DisplayName", displayName != null ? displayName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDisplayNameAsync(string displayName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `DisplayName` = @DisplayName";
			var parameter_ = Database.CreateInParameter("@DisplayName", displayName != null ? displayName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDisplayName
		#region PutDesc
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDescByPK(string userID, string desc, TransactionManager tm_ = null)
		{
			RepairPutDescByPKData(userID, desc, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDescByPKAsync(string userID, string desc, TransactionManager tm_ = null)
		{
			RepairPutDescByPKData(userID, desc, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDescByPKData(string userID, string desc, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `Desc` = @Desc  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDesc(string desc, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `Desc` = @Desc";
			var parameter_ = Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDescAsync(string desc, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `Desc` = @Desc";
			var parameter_ = Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDesc
		#region PutGroupID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "groupID">分组编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGroupIDByPK(string userID, int? groupID, TransactionManager tm_ = null)
		{
			RepairPutGroupIDByPKData(userID, groupID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutGroupIDByPKAsync(string userID, int? groupID, TransactionManager tm_ = null)
		{
			RepairPutGroupIDByPKData(userID, groupID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutGroupIDByPKData(string userID, int? groupID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `GroupID` = @GroupID  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupID", groupID.HasValue ? groupID.Value : (object)DBNull.Value, MySqlDbType.Int32),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "groupID">分组编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGroupID(int? groupID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `GroupID` = @GroupID";
			var parameter_ = Database.CreateInParameter("@GroupID", groupID.HasValue ? groupID.Value : (object)DBNull.Value, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutGroupIDAsync(int? groupID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `GroupID` = @GroupID";
			var parameter_ = Database.CreateInParameter("@GroupID", groupID.HasValue ? groupID.Value : (object)DBNull.Value, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutGroupID
		#region PutIsAdmin
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "isAdmin">是否管理员</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIsAdminByPK(string userID, bool isAdmin, TransactionManager tm_ = null)
		{
			RepairPutIsAdminByPKData(userID, isAdmin, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutIsAdminByPKAsync(string userID, bool isAdmin, TransactionManager tm_ = null)
		{
			RepairPutIsAdminByPKData(userID, isAdmin, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutIsAdminByPKData(string userID, bool isAdmin, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `IsAdmin` = @IsAdmin  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@IsAdmin", isAdmin, MySqlDbType.Byte),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "isAdmin">是否管理员</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIsAdmin(bool isAdmin, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `IsAdmin` = @IsAdmin";
			var parameter_ = Database.CreateInParameter("@IsAdmin", isAdmin, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutIsAdminAsync(bool isAdmin, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `IsAdmin` = @IsAdmin";
			var parameter_ = Database.CreateInParameter("@IsAdmin", isAdmin, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutIsAdmin
		#region PutRegisterDate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "registerDate">注册时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRegisterDateByPK(string userID, DateTime registerDate, TransactionManager tm_ = null)
		{
			RepairPutRegisterDateByPKData(userID, registerDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRegisterDateByPKAsync(string userID, DateTime registerDate, TransactionManager tm_ = null)
		{
			RepairPutRegisterDateByPKData(userID, registerDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRegisterDateByPKData(string userID, DateTime registerDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `RegisterDate` = @RegisterDate  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RegisterDate", registerDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "registerDate">注册时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRegisterDate(DateTime registerDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `RegisterDate` = @RegisterDate";
			var parameter_ = Database.CreateInParameter("@RegisterDate", registerDate, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRegisterDateAsync(DateTime registerDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `RegisterDate` = @RegisterDate";
			var parameter_ = Database.CreateInParameter("@RegisterDate", registerDate, MySqlDbType.DateTime);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRegisterDate
		#region PutApprovedDate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "approvedDate">审批时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutApprovedDateByPK(string userID, DateTime? approvedDate, TransactionManager tm_ = null)
		{
			RepairPutApprovedDateByPKData(userID, approvedDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutApprovedDateByPKAsync(string userID, DateTime? approvedDate, TransactionManager tm_ = null)
		{
			RepairPutApprovedDateByPKData(userID, approvedDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutApprovedDateByPKData(string userID, DateTime? approvedDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `ApprovedDate` = @ApprovedDate  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ApprovedDate", approvedDate.HasValue ? approvedDate.Value : (object)DBNull.Value, MySqlDbType.DateTime),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "approvedDate">审批时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutApprovedDate(DateTime? approvedDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `ApprovedDate` = @ApprovedDate";
			var parameter_ = Database.CreateInParameter("@ApprovedDate", approvedDate.HasValue ? approvedDate.Value : (object)DBNull.Value, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutApprovedDateAsync(DateTime? approvedDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `ApprovedDate` = @ApprovedDate";
			var parameter_ = Database.CreateInParameter("@ApprovedDate", approvedDate.HasValue ? approvedDate.Value : (object)DBNull.Value, MySqlDbType.DateTime);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutApprovedDate
		#region PutApprovedBy
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "approvedBy">审批者</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutApprovedByByPK(string userID, string approvedBy, TransactionManager tm_ = null)
		{
			RepairPutApprovedByByPKData(userID, approvedBy, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutApprovedByByPKAsync(string userID, string approvedBy, TransactionManager tm_ = null)
		{
			RepairPutApprovedByByPKData(userID, approvedBy, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutApprovedByByPKData(string userID, string approvedBy, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `ApprovedBy` = @ApprovedBy  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ApprovedBy", approvedBy != null ? approvedBy : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "approvedBy">审批者</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutApprovedBy(string approvedBy, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `ApprovedBy` = @ApprovedBy";
			var parameter_ = Database.CreateInParameter("@ApprovedBy", approvedBy != null ? approvedBy : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutApprovedByAsync(string approvedBy, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `ApprovedBy` = @ApprovedBy";
			var parameter_ = Database.CreateInParameter("@ApprovedBy", approvedBy != null ? approvedBy : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutApprovedBy
		#region PutRejectDate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "rejectDate">拒绝时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRejectDateByPK(string userID, DateTime? rejectDate, TransactionManager tm_ = null)
		{
			RepairPutRejectDateByPKData(userID, rejectDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRejectDateByPKAsync(string userID, DateTime? rejectDate, TransactionManager tm_ = null)
		{
			RepairPutRejectDateByPKData(userID, rejectDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRejectDateByPKData(string userID, DateTime? rejectDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `RejectDate` = @RejectDate  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RejectDate", rejectDate.HasValue ? rejectDate.Value : (object)DBNull.Value, MySqlDbType.DateTime),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "rejectDate">拒绝时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRejectDate(DateTime? rejectDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `RejectDate` = @RejectDate";
			var parameter_ = Database.CreateInParameter("@RejectDate", rejectDate.HasValue ? rejectDate.Value : (object)DBNull.Value, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRejectDateAsync(DateTime? rejectDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `RejectDate` = @RejectDate";
			var parameter_ = Database.CreateInParameter("@RejectDate", rejectDate.HasValue ? rejectDate.Value : (object)DBNull.Value, MySqlDbType.DateTime);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRejectDate
		#region PutRejectBy
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "rejectBy">拒绝者</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRejectByByPK(string userID, string rejectBy, TransactionManager tm_ = null)
		{
			RepairPutRejectByByPKData(userID, rejectBy, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRejectByByPKAsync(string userID, string rejectBy, TransactionManager tm_ = null)
		{
			RepairPutRejectByByPKData(userID, rejectBy, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRejectByByPKData(string userID, string rejectBy, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `RejectBy` = @RejectBy  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RejectBy", rejectBy != null ? rejectBy : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "rejectBy">拒绝者</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRejectBy(string rejectBy, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `RejectBy` = @RejectBy";
			var parameter_ = Database.CreateInParameter("@RejectBy", rejectBy != null ? rejectBy : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRejectByAsync(string rejectBy, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `RejectBy` = @RejectBy";
			var parameter_ = Database.CreateInParameter("@RejectBy", rejectBy != null ? rejectBy : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRejectBy
		#region PutStatus
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutStatusByPK(string userID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(userID, status, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutStatusByPKAsync(string userID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(userID, status, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutStatusByPKData(string userID, int status, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `Status` = @Status  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Status", status, MySqlDbType.Byte),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutStatus(int status, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `Status` = @Status";
			var parameter_ = Database.CreateInParameter("@Status", status, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutStatusAsync(int status, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `Status` = @Status";
			var parameter_ = Database.CreateInParameter("@Status", status, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutStatus
		#region PutRecDate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRecDateByPK(string userID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(userID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRecDateByPKAsync(string userID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(userID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRecDateByPKData(string userID, DateTime recDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `RecDate` = @RecDate  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
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
			const string sql_ = @"UPDATE `admin_user` SET `RecDate` = @RecDate";
			var parameter_ = Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRecDateAsync(DateTime recDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `RecDate` = @RecDate";
			var parameter_ = Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRecDate
		#region PutIcon
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// /// <param name = "icon">头像</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIconByPK(string userID, string icon, TransactionManager tm_ = null)
		{
			RepairPutIconByPKData(userID, icon, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutIconByPKAsync(string userID, string icon, TransactionManager tm_ = null)
		{
			RepairPutIconByPKData(userID, icon, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutIconByPKData(string userID, string icon, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user` SET `Icon` = @Icon  WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Icon", icon != null ? icon : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "icon">头像</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIcon(string icon, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `Icon` = @Icon";
			var parameter_ = Database.CreateInParameter("@Icon", icon != null ? icon : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutIconAsync(string icon, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user` SET `Icon` = @Icon";
			var parameter_ = Database.CreateInParameter("@Icon", icon != null ? icon : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutIcon
		#endregion // PutXXX
		#endregion // Put
	   
		#region Set
		
		/// <summary>
		/// 插入或者更新数据
		/// </summary>
		/// <param name = "item">要更新的实体对象</param>
		/// <param name="tm">事务管理对象</param>
		/// <return>true:插入操作；false:更新操作</return>
		public bool Set(Admin_userEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.UserID) == null)
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
		public async Task<bool> SetAsync(Admin_userEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.UserID) == null)
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
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_userEO GetByPK(string userID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(userID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<Admin_userEO> GetByPKAsync(string userID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(userID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		private void RepairGetByPKData(string userID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`UserID` = @UserID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 UserName（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetUserNameByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`UserName`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<string> GetUserNameByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`UserName`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Password（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetPasswordByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Password`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<string> GetPasswordByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Password`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 PasswordSalt（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetPasswordSaltByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`PasswordSalt`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<string> GetPasswordSaltByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`PasswordSalt`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Mobile（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetMobileByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Mobile`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<string> GetMobileByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Mobile`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 DisplayName（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetDisplayNameByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`DisplayName`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<string> GetDisplayNameByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`DisplayName`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Desc（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetDescByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Desc`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<string> GetDescByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Desc`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 GroupID（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int? GetGroupIDByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (int?)GetScalar("`GroupID`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<int?> GetGroupIDByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (int?)await GetScalarAsync("`GroupID`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 IsAdmin（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public bool GetIsAdminByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (bool)GetScalar("`IsAdmin`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<bool> GetIsAdminByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (bool)await GetScalarAsync("`IsAdmin`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RegisterDate（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public DateTime GetRegisterDateByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (DateTime)GetScalar("`RegisterDate`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<DateTime> GetRegisterDateByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (DateTime)await GetScalarAsync("`RegisterDate`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ApprovedDate（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public DateTime? GetApprovedDateByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (DateTime?)GetScalar("`ApprovedDate`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<DateTime?> GetApprovedDateByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (DateTime?)await GetScalarAsync("`ApprovedDate`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ApprovedBy（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetApprovedByByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`ApprovedBy`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<string> GetApprovedByByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`ApprovedBy`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RejectDate（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public DateTime? GetRejectDateByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (DateTime?)GetScalar("`RejectDate`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<DateTime?> GetRejectDateByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (DateTime?)await GetScalarAsync("`RejectDate`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RejectBy（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetRejectByByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`RejectBy`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<string> GetRejectByByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`RejectBy`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Status（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetStatusByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`Status`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<int> GetStatusByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`Status`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RecDate（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public DateTime GetRecDateByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (DateTime)GetScalar("`RecDate`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<DateTime> GetRecDateByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (DateTime)await GetScalarAsync("`RecDate`", "`UserID` = @UserID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Icon（字段）
		/// </summary>
		/// /// <param name = "userID">用户ID GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetIconByPK(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Icon`", "`UserID` = @UserID", paras_, tm_);
		}
		public async Task<string> GetIconByPKAsync(string userID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Icon`", "`UserID` = @UserID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByUserName
		
		/// <summary>
		/// 按 UserName（字段） 查询
		/// </summary>
		/// /// <param name = "userName">登录用户名</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByUserName(string userName)
		{
			return GetByUserName(userName, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByUserNameAsync(string userName)
		{
			return await GetByUserNameAsync(userName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserName（字段） 查询
		/// </summary>
		/// /// <param name = "userName">登录用户名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByUserName(string userName, TransactionManager tm_)
		{
			return GetByUserName(userName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByUserNameAsync(string userName, TransactionManager tm_)
		{
			return await GetByUserNameAsync(userName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserName（字段） 查询
		/// </summary>
		/// /// <param name = "userName">登录用户名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByUserName(string userName, int top_)
		{
			return GetByUserName(userName, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByUserNameAsync(string userName, int top_)
		{
			return await GetByUserNameAsync(userName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserName（字段） 查询
		/// </summary>
		/// /// <param name = "userName">登录用户名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByUserName(string userName, int top_, TransactionManager tm_)
		{
			return GetByUserName(userName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByUserNameAsync(string userName, int top_, TransactionManager tm_)
		{
			return await GetByUserNameAsync(userName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserName（字段） 查询
		/// </summary>
		/// /// <param name = "userName">登录用户名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByUserName(string userName, string sort_)
		{
			return GetByUserName(userName, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByUserNameAsync(string userName, string sort_)
		{
			return await GetByUserNameAsync(userName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 UserName（字段） 查询
		/// </summary>
		/// /// <param name = "userName">登录用户名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByUserName(string userName, string sort_, TransactionManager tm_)
		{
			return GetByUserName(userName, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByUserNameAsync(string userName, string sort_, TransactionManager tm_)
		{
			return await GetByUserNameAsync(userName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 UserName（字段） 查询
		/// </summary>
		/// /// <param name = "userName">登录用户名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByUserName(string userName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(userName != null ? "`UserName` = @UserName" : "`UserName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (userName != null)
				paras_.Add(Database.CreateInParameter("@UserName", userName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByUserNameAsync(string userName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(userName != null ? "`UserName` = @UserName" : "`UserName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (userName != null)
				paras_.Add(Database.CreateInParameter("@UserName", userName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByUserName
		#region GetByPassword
		
		/// <summary>
		/// 按 Password（字段） 查询
		/// </summary>
		/// /// <param name = "password">登录密码</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPassword(string password)
		{
			return GetByPassword(password, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByPasswordAsync(string password)
		{
			return await GetByPasswordAsync(password, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Password（字段） 查询
		/// </summary>
		/// /// <param name = "password">登录密码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPassword(string password, TransactionManager tm_)
		{
			return GetByPassword(password, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByPasswordAsync(string password, TransactionManager tm_)
		{
			return await GetByPasswordAsync(password, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Password（字段） 查询
		/// </summary>
		/// /// <param name = "password">登录密码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPassword(string password, int top_)
		{
			return GetByPassword(password, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByPasswordAsync(string password, int top_)
		{
			return await GetByPasswordAsync(password, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Password（字段） 查询
		/// </summary>
		/// /// <param name = "password">登录密码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPassword(string password, int top_, TransactionManager tm_)
		{
			return GetByPassword(password, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByPasswordAsync(string password, int top_, TransactionManager tm_)
		{
			return await GetByPasswordAsync(password, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Password（字段） 查询
		/// </summary>
		/// /// <param name = "password">登录密码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPassword(string password, string sort_)
		{
			return GetByPassword(password, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByPasswordAsync(string password, string sort_)
		{
			return await GetByPasswordAsync(password, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Password（字段） 查询
		/// </summary>
		/// /// <param name = "password">登录密码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPassword(string password, string sort_, TransactionManager tm_)
		{
			return GetByPassword(password, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByPasswordAsync(string password, string sort_, TransactionManager tm_)
		{
			return await GetByPasswordAsync(password, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Password（字段） 查询
		/// </summary>
		/// /// <param name = "password">登录密码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPassword(string password, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(password != null ? "`Password` = @Password" : "`Password` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (password != null)
				paras_.Add(Database.CreateInParameter("@Password", password, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByPasswordAsync(string password, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(password != null ? "`Password` = @Password" : "`Password` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (password != null)
				paras_.Add(Database.CreateInParameter("@Password", password, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByPassword
		#region GetByPasswordSalt
		
		/// <summary>
		/// 按 PasswordSalt（字段） 查询
		/// </summary>
		/// /// <param name = "passwordSalt">密码哈希Salt</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPasswordSalt(string passwordSalt)
		{
			return GetByPasswordSalt(passwordSalt, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByPasswordSaltAsync(string passwordSalt)
		{
			return await GetByPasswordSaltAsync(passwordSalt, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PasswordSalt（字段） 查询
		/// </summary>
		/// /// <param name = "passwordSalt">密码哈希Salt</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPasswordSalt(string passwordSalt, TransactionManager tm_)
		{
			return GetByPasswordSalt(passwordSalt, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByPasswordSaltAsync(string passwordSalt, TransactionManager tm_)
		{
			return await GetByPasswordSaltAsync(passwordSalt, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PasswordSalt（字段） 查询
		/// </summary>
		/// /// <param name = "passwordSalt">密码哈希Salt</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPasswordSalt(string passwordSalt, int top_)
		{
			return GetByPasswordSalt(passwordSalt, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByPasswordSaltAsync(string passwordSalt, int top_)
		{
			return await GetByPasswordSaltAsync(passwordSalt, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PasswordSalt（字段） 查询
		/// </summary>
		/// /// <param name = "passwordSalt">密码哈希Salt</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPasswordSalt(string passwordSalt, int top_, TransactionManager tm_)
		{
			return GetByPasswordSalt(passwordSalt, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByPasswordSaltAsync(string passwordSalt, int top_, TransactionManager tm_)
		{
			return await GetByPasswordSaltAsync(passwordSalt, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PasswordSalt（字段） 查询
		/// </summary>
		/// /// <param name = "passwordSalt">密码哈希Salt</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPasswordSalt(string passwordSalt, string sort_)
		{
			return GetByPasswordSalt(passwordSalt, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByPasswordSaltAsync(string passwordSalt, string sort_)
		{
			return await GetByPasswordSaltAsync(passwordSalt, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 PasswordSalt（字段） 查询
		/// </summary>
		/// /// <param name = "passwordSalt">密码哈希Salt</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPasswordSalt(string passwordSalt, string sort_, TransactionManager tm_)
		{
			return GetByPasswordSalt(passwordSalt, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByPasswordSaltAsync(string passwordSalt, string sort_, TransactionManager tm_)
		{
			return await GetByPasswordSaltAsync(passwordSalt, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 PasswordSalt（字段） 查询
		/// </summary>
		/// /// <param name = "passwordSalt">密码哈希Salt</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByPasswordSalt(string passwordSalt, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(passwordSalt != null ? "`PasswordSalt` = @PasswordSalt" : "`PasswordSalt` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (passwordSalt != null)
				paras_.Add(Database.CreateInParameter("@PasswordSalt", passwordSalt, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByPasswordSaltAsync(string passwordSalt, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(passwordSalt != null ? "`PasswordSalt` = @PasswordSalt" : "`PasswordSalt` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (passwordSalt != null)
				paras_.Add(Database.CreateInParameter("@PasswordSalt", passwordSalt, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByPasswordSalt
		#region GetByMobile
		
		/// <summary>
		/// 按 Mobile（字段） 查询
		/// </summary>
		/// /// <param name = "mobile">手机号</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByMobile(string mobile)
		{
			return GetByMobile(mobile, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByMobileAsync(string mobile)
		{
			return await GetByMobileAsync(mobile, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Mobile（字段） 查询
		/// </summary>
		/// /// <param name = "mobile">手机号</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByMobile(string mobile, TransactionManager tm_)
		{
			return GetByMobile(mobile, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByMobileAsync(string mobile, TransactionManager tm_)
		{
			return await GetByMobileAsync(mobile, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Mobile（字段） 查询
		/// </summary>
		/// /// <param name = "mobile">手机号</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByMobile(string mobile, int top_)
		{
			return GetByMobile(mobile, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByMobileAsync(string mobile, int top_)
		{
			return await GetByMobileAsync(mobile, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Mobile（字段） 查询
		/// </summary>
		/// /// <param name = "mobile">手机号</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByMobile(string mobile, int top_, TransactionManager tm_)
		{
			return GetByMobile(mobile, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByMobileAsync(string mobile, int top_, TransactionManager tm_)
		{
			return await GetByMobileAsync(mobile, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Mobile（字段） 查询
		/// </summary>
		/// /// <param name = "mobile">手机号</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByMobile(string mobile, string sort_)
		{
			return GetByMobile(mobile, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByMobileAsync(string mobile, string sort_)
		{
			return await GetByMobileAsync(mobile, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Mobile（字段） 查询
		/// </summary>
		/// /// <param name = "mobile">手机号</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByMobile(string mobile, string sort_, TransactionManager tm_)
		{
			return GetByMobile(mobile, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByMobileAsync(string mobile, string sort_, TransactionManager tm_)
		{
			return await GetByMobileAsync(mobile, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Mobile（字段） 查询
		/// </summary>
		/// /// <param name = "mobile">手机号</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByMobile(string mobile, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(mobile != null ? "`Mobile` = @Mobile" : "`Mobile` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (mobile != null)
				paras_.Add(Database.CreateInParameter("@Mobile", mobile, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByMobileAsync(string mobile, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(mobile != null ? "`Mobile` = @Mobile" : "`Mobile` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (mobile != null)
				paras_.Add(Database.CreateInParameter("@Mobile", mobile, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByMobile
		#region GetByDisplayName
		
		/// <summary>
		/// 按 DisplayName（字段） 查询
		/// </summary>
		/// /// <param name = "displayName">显示用户名</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDisplayName(string displayName)
		{
			return GetByDisplayName(displayName, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByDisplayNameAsync(string displayName)
		{
			return await GetByDisplayNameAsync(displayName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DisplayName（字段） 查询
		/// </summary>
		/// /// <param name = "displayName">显示用户名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDisplayName(string displayName, TransactionManager tm_)
		{
			return GetByDisplayName(displayName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByDisplayNameAsync(string displayName, TransactionManager tm_)
		{
			return await GetByDisplayNameAsync(displayName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DisplayName（字段） 查询
		/// </summary>
		/// /// <param name = "displayName">显示用户名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDisplayName(string displayName, int top_)
		{
			return GetByDisplayName(displayName, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByDisplayNameAsync(string displayName, int top_)
		{
			return await GetByDisplayNameAsync(displayName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DisplayName（字段） 查询
		/// </summary>
		/// /// <param name = "displayName">显示用户名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDisplayName(string displayName, int top_, TransactionManager tm_)
		{
			return GetByDisplayName(displayName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByDisplayNameAsync(string displayName, int top_, TransactionManager tm_)
		{
			return await GetByDisplayNameAsync(displayName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DisplayName（字段） 查询
		/// </summary>
		/// /// <param name = "displayName">显示用户名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDisplayName(string displayName, string sort_)
		{
			return GetByDisplayName(displayName, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByDisplayNameAsync(string displayName, string sort_)
		{
			return await GetByDisplayNameAsync(displayName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 DisplayName（字段） 查询
		/// </summary>
		/// /// <param name = "displayName">显示用户名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDisplayName(string displayName, string sort_, TransactionManager tm_)
		{
			return GetByDisplayName(displayName, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByDisplayNameAsync(string displayName, string sort_, TransactionManager tm_)
		{
			return await GetByDisplayNameAsync(displayName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 DisplayName（字段） 查询
		/// </summary>
		/// /// <param name = "displayName">显示用户名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDisplayName(string displayName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(displayName != null ? "`DisplayName` = @DisplayName" : "`DisplayName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (displayName != null)
				paras_.Add(Database.CreateInParameter("@DisplayName", displayName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByDisplayNameAsync(string displayName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(displayName != null ? "`DisplayName` = @DisplayName" : "`DisplayName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (displayName != null)
				paras_.Add(Database.CreateInParameter("@DisplayName", displayName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByDisplayName
		#region GetByDesc
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDesc(string desc)
		{
			return GetByDesc(desc, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByDescAsync(string desc)
		{
			return await GetByDescAsync(desc, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDesc(string desc, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByDescAsync(string desc, TransactionManager tm_)
		{
			return await GetByDescAsync(desc, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDesc(string desc, int top_)
		{
			return GetByDesc(desc, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByDescAsync(string desc, int top_)
		{
			return await GetByDescAsync(desc, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDesc(string desc, int top_, TransactionManager tm_)
		{
			return GetByDesc(desc, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByDescAsync(string desc, int top_, TransactionManager tm_)
		{
			return await GetByDescAsync(desc, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDesc(string desc, string sort_)
		{
			return GetByDesc(desc, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByDescAsync(string desc, string sort_)
		{
			return await GetByDescAsync(desc, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDesc(string desc, string sort_, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByDescAsync(string desc, string sort_, TransactionManager tm_)
		{
			return await GetByDescAsync(desc, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByDesc(string desc, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByDescAsync(string desc, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByDesc
		#region GetByGroupID
		
		/// <summary>
		/// 按 GroupID（字段） 查询
		/// </summary>
		/// /// <param name = "groupID">分组编码</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByGroupID(int? groupID)
		{
			return GetByGroupID(groupID, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByGroupIDAsync(int? groupID)
		{
			return await GetByGroupIDAsync(groupID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GroupID（字段） 查询
		/// </summary>
		/// /// <param name = "groupID">分组编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByGroupID(int? groupID, TransactionManager tm_)
		{
			return GetByGroupID(groupID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByGroupIDAsync(int? groupID, TransactionManager tm_)
		{
			return await GetByGroupIDAsync(groupID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GroupID（字段） 查询
		/// </summary>
		/// /// <param name = "groupID">分组编码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByGroupID(int? groupID, int top_)
		{
			return GetByGroupID(groupID, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByGroupIDAsync(int? groupID, int top_)
		{
			return await GetByGroupIDAsync(groupID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GroupID（字段） 查询
		/// </summary>
		/// /// <param name = "groupID">分组编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByGroupID(int? groupID, int top_, TransactionManager tm_)
		{
			return GetByGroupID(groupID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByGroupIDAsync(int? groupID, int top_, TransactionManager tm_)
		{
			return await GetByGroupIDAsync(groupID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GroupID（字段） 查询
		/// </summary>
		/// /// <param name = "groupID">分组编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByGroupID(int? groupID, string sort_)
		{
			return GetByGroupID(groupID, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByGroupIDAsync(int? groupID, string sort_)
		{
			return await GetByGroupIDAsync(groupID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 GroupID（字段） 查询
		/// </summary>
		/// /// <param name = "groupID">分组编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByGroupID(int? groupID, string sort_, TransactionManager tm_)
		{
			return GetByGroupID(groupID, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByGroupIDAsync(int? groupID, string sort_, TransactionManager tm_)
		{
			return await GetByGroupIDAsync(groupID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 GroupID（字段） 查询
		/// </summary>
		/// /// <param name = "groupID">分组编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByGroupID(int? groupID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(groupID.HasValue ? "`GroupID` = @GroupID" : "`GroupID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (groupID.HasValue)
				paras_.Add(Database.CreateInParameter("@GroupID", groupID.Value, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByGroupIDAsync(int? groupID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(groupID.HasValue ? "`GroupID` = @GroupID" : "`GroupID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (groupID.HasValue)
				paras_.Add(Database.CreateInParameter("@GroupID", groupID.Value, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByGroupID
		#region GetByIsAdmin
		
		/// <summary>
		/// 按 IsAdmin（字段） 查询
		/// </summary>
		/// /// <param name = "isAdmin">是否管理员</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIsAdmin(bool isAdmin)
		{
			return GetByIsAdmin(isAdmin, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByIsAdminAsync(bool isAdmin)
		{
			return await GetByIsAdminAsync(isAdmin, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 IsAdmin（字段） 查询
		/// </summary>
		/// /// <param name = "isAdmin">是否管理员</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIsAdmin(bool isAdmin, TransactionManager tm_)
		{
			return GetByIsAdmin(isAdmin, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByIsAdminAsync(bool isAdmin, TransactionManager tm_)
		{
			return await GetByIsAdminAsync(isAdmin, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IsAdmin（字段） 查询
		/// </summary>
		/// /// <param name = "isAdmin">是否管理员</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIsAdmin(bool isAdmin, int top_)
		{
			return GetByIsAdmin(isAdmin, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByIsAdminAsync(bool isAdmin, int top_)
		{
			return await GetByIsAdminAsync(isAdmin, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 IsAdmin（字段） 查询
		/// </summary>
		/// /// <param name = "isAdmin">是否管理员</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIsAdmin(bool isAdmin, int top_, TransactionManager tm_)
		{
			return GetByIsAdmin(isAdmin, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByIsAdminAsync(bool isAdmin, int top_, TransactionManager tm_)
		{
			return await GetByIsAdminAsync(isAdmin, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IsAdmin（字段） 查询
		/// </summary>
		/// /// <param name = "isAdmin">是否管理员</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIsAdmin(bool isAdmin, string sort_)
		{
			return GetByIsAdmin(isAdmin, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByIsAdminAsync(bool isAdmin, string sort_)
		{
			return await GetByIsAdminAsync(isAdmin, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 IsAdmin（字段） 查询
		/// </summary>
		/// /// <param name = "isAdmin">是否管理员</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIsAdmin(bool isAdmin, string sort_, TransactionManager tm_)
		{
			return GetByIsAdmin(isAdmin, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByIsAdminAsync(bool isAdmin, string sort_, TransactionManager tm_)
		{
			return await GetByIsAdminAsync(isAdmin, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 IsAdmin（字段） 查询
		/// </summary>
		/// /// <param name = "isAdmin">是否管理员</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIsAdmin(bool isAdmin, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`IsAdmin` = @IsAdmin", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsAdmin", isAdmin, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByIsAdminAsync(bool isAdmin, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`IsAdmin` = @IsAdmin", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsAdmin", isAdmin, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByIsAdmin
		#region GetByRegisterDate
		
		/// <summary>
		/// 按 RegisterDate（字段） 查询
		/// </summary>
		/// /// <param name = "registerDate">注册时间</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRegisterDate(DateTime registerDate)
		{
			return GetByRegisterDate(registerDate, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByRegisterDateAsync(DateTime registerDate)
		{
			return await GetByRegisterDateAsync(registerDate, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RegisterDate（字段） 查询
		/// </summary>
		/// /// <param name = "registerDate">注册时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRegisterDate(DateTime registerDate, TransactionManager tm_)
		{
			return GetByRegisterDate(registerDate, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRegisterDateAsync(DateTime registerDate, TransactionManager tm_)
		{
			return await GetByRegisterDateAsync(registerDate, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RegisterDate（字段） 查询
		/// </summary>
		/// /// <param name = "registerDate">注册时间</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRegisterDate(DateTime registerDate, int top_)
		{
			return GetByRegisterDate(registerDate, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByRegisterDateAsync(DateTime registerDate, int top_)
		{
			return await GetByRegisterDateAsync(registerDate, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RegisterDate（字段） 查询
		/// </summary>
		/// /// <param name = "registerDate">注册时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRegisterDate(DateTime registerDate, int top_, TransactionManager tm_)
		{
			return GetByRegisterDate(registerDate, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRegisterDateAsync(DateTime registerDate, int top_, TransactionManager tm_)
		{
			return await GetByRegisterDateAsync(registerDate, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RegisterDate（字段） 查询
		/// </summary>
		/// /// <param name = "registerDate">注册时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRegisterDate(DateTime registerDate, string sort_)
		{
			return GetByRegisterDate(registerDate, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByRegisterDateAsync(DateTime registerDate, string sort_)
		{
			return await GetByRegisterDateAsync(registerDate, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RegisterDate（字段） 查询
		/// </summary>
		/// /// <param name = "registerDate">注册时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRegisterDate(DateTime registerDate, string sort_, TransactionManager tm_)
		{
			return GetByRegisterDate(registerDate, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRegisterDateAsync(DateTime registerDate, string sort_, TransactionManager tm_)
		{
			return await GetByRegisterDateAsync(registerDate, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RegisterDate（字段） 查询
		/// </summary>
		/// /// <param name = "registerDate">注册时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRegisterDate(DateTime registerDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RegisterDate` = @RegisterDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RegisterDate", registerDate, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByRegisterDateAsync(DateTime registerDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RegisterDate` = @RegisterDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RegisterDate", registerDate, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByRegisterDate
		#region GetByApprovedDate
		
		/// <summary>
		/// 按 ApprovedDate（字段） 查询
		/// </summary>
		/// /// <param name = "approvedDate">审批时间</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedDate(DateTime? approvedDate)
		{
			return GetByApprovedDate(approvedDate, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByApprovedDateAsync(DateTime? approvedDate)
		{
			return await GetByApprovedDateAsync(approvedDate, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ApprovedDate（字段） 查询
		/// </summary>
		/// /// <param name = "approvedDate">审批时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedDate(DateTime? approvedDate, TransactionManager tm_)
		{
			return GetByApprovedDate(approvedDate, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByApprovedDateAsync(DateTime? approvedDate, TransactionManager tm_)
		{
			return await GetByApprovedDateAsync(approvedDate, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ApprovedDate（字段） 查询
		/// </summary>
		/// /// <param name = "approvedDate">审批时间</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedDate(DateTime? approvedDate, int top_)
		{
			return GetByApprovedDate(approvedDate, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByApprovedDateAsync(DateTime? approvedDate, int top_)
		{
			return await GetByApprovedDateAsync(approvedDate, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ApprovedDate（字段） 查询
		/// </summary>
		/// /// <param name = "approvedDate">审批时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedDate(DateTime? approvedDate, int top_, TransactionManager tm_)
		{
			return GetByApprovedDate(approvedDate, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByApprovedDateAsync(DateTime? approvedDate, int top_, TransactionManager tm_)
		{
			return await GetByApprovedDateAsync(approvedDate, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ApprovedDate（字段） 查询
		/// </summary>
		/// /// <param name = "approvedDate">审批时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedDate(DateTime? approvedDate, string sort_)
		{
			return GetByApprovedDate(approvedDate, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByApprovedDateAsync(DateTime? approvedDate, string sort_)
		{
			return await GetByApprovedDateAsync(approvedDate, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ApprovedDate（字段） 查询
		/// </summary>
		/// /// <param name = "approvedDate">审批时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedDate(DateTime? approvedDate, string sort_, TransactionManager tm_)
		{
			return GetByApprovedDate(approvedDate, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByApprovedDateAsync(DateTime? approvedDate, string sort_, TransactionManager tm_)
		{
			return await GetByApprovedDateAsync(approvedDate, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ApprovedDate（字段） 查询
		/// </summary>
		/// /// <param name = "approvedDate">审批时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedDate(DateTime? approvedDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(approvedDate.HasValue ? "`ApprovedDate` = @ApprovedDate" : "`ApprovedDate` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (approvedDate.HasValue)
				paras_.Add(Database.CreateInParameter("@ApprovedDate", approvedDate.Value, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByApprovedDateAsync(DateTime? approvedDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(approvedDate.HasValue ? "`ApprovedDate` = @ApprovedDate" : "`ApprovedDate` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (approvedDate.HasValue)
				paras_.Add(Database.CreateInParameter("@ApprovedDate", approvedDate.Value, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByApprovedDate
		#region GetByApprovedBy
		
		/// <summary>
		/// 按 ApprovedBy（字段） 查询
		/// </summary>
		/// /// <param name = "approvedBy">审批者</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedBy(string approvedBy)
		{
			return GetByApprovedBy(approvedBy, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByApprovedByAsync(string approvedBy)
		{
			return await GetByApprovedByAsync(approvedBy, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ApprovedBy（字段） 查询
		/// </summary>
		/// /// <param name = "approvedBy">审批者</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedBy(string approvedBy, TransactionManager tm_)
		{
			return GetByApprovedBy(approvedBy, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByApprovedByAsync(string approvedBy, TransactionManager tm_)
		{
			return await GetByApprovedByAsync(approvedBy, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ApprovedBy（字段） 查询
		/// </summary>
		/// /// <param name = "approvedBy">审批者</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedBy(string approvedBy, int top_)
		{
			return GetByApprovedBy(approvedBy, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByApprovedByAsync(string approvedBy, int top_)
		{
			return await GetByApprovedByAsync(approvedBy, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ApprovedBy（字段） 查询
		/// </summary>
		/// /// <param name = "approvedBy">审批者</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedBy(string approvedBy, int top_, TransactionManager tm_)
		{
			return GetByApprovedBy(approvedBy, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByApprovedByAsync(string approvedBy, int top_, TransactionManager tm_)
		{
			return await GetByApprovedByAsync(approvedBy, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ApprovedBy（字段） 查询
		/// </summary>
		/// /// <param name = "approvedBy">审批者</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedBy(string approvedBy, string sort_)
		{
			return GetByApprovedBy(approvedBy, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByApprovedByAsync(string approvedBy, string sort_)
		{
			return await GetByApprovedByAsync(approvedBy, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ApprovedBy（字段） 查询
		/// </summary>
		/// /// <param name = "approvedBy">审批者</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedBy(string approvedBy, string sort_, TransactionManager tm_)
		{
			return GetByApprovedBy(approvedBy, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByApprovedByAsync(string approvedBy, string sort_, TransactionManager tm_)
		{
			return await GetByApprovedByAsync(approvedBy, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ApprovedBy（字段） 查询
		/// </summary>
		/// /// <param name = "approvedBy">审批者</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByApprovedBy(string approvedBy, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(approvedBy != null ? "`ApprovedBy` = @ApprovedBy" : "`ApprovedBy` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (approvedBy != null)
				paras_.Add(Database.CreateInParameter("@ApprovedBy", approvedBy, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByApprovedByAsync(string approvedBy, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(approvedBy != null ? "`ApprovedBy` = @ApprovedBy" : "`ApprovedBy` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (approvedBy != null)
				paras_.Add(Database.CreateInParameter("@ApprovedBy", approvedBy, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByApprovedBy
		#region GetByRejectDate
		
		/// <summary>
		/// 按 RejectDate（字段） 查询
		/// </summary>
		/// /// <param name = "rejectDate">拒绝时间</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectDate(DateTime? rejectDate)
		{
			return GetByRejectDate(rejectDate, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByRejectDateAsync(DateTime? rejectDate)
		{
			return await GetByRejectDateAsync(rejectDate, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RejectDate（字段） 查询
		/// </summary>
		/// /// <param name = "rejectDate">拒绝时间</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectDate(DateTime? rejectDate, TransactionManager tm_)
		{
			return GetByRejectDate(rejectDate, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRejectDateAsync(DateTime? rejectDate, TransactionManager tm_)
		{
			return await GetByRejectDateAsync(rejectDate, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RejectDate（字段） 查询
		/// </summary>
		/// /// <param name = "rejectDate">拒绝时间</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectDate(DateTime? rejectDate, int top_)
		{
			return GetByRejectDate(rejectDate, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByRejectDateAsync(DateTime? rejectDate, int top_)
		{
			return await GetByRejectDateAsync(rejectDate, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RejectDate（字段） 查询
		/// </summary>
		/// /// <param name = "rejectDate">拒绝时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectDate(DateTime? rejectDate, int top_, TransactionManager tm_)
		{
			return GetByRejectDate(rejectDate, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRejectDateAsync(DateTime? rejectDate, int top_, TransactionManager tm_)
		{
			return await GetByRejectDateAsync(rejectDate, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RejectDate（字段） 查询
		/// </summary>
		/// /// <param name = "rejectDate">拒绝时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectDate(DateTime? rejectDate, string sort_)
		{
			return GetByRejectDate(rejectDate, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByRejectDateAsync(DateTime? rejectDate, string sort_)
		{
			return await GetByRejectDateAsync(rejectDate, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RejectDate（字段） 查询
		/// </summary>
		/// /// <param name = "rejectDate">拒绝时间</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectDate(DateTime? rejectDate, string sort_, TransactionManager tm_)
		{
			return GetByRejectDate(rejectDate, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRejectDateAsync(DateTime? rejectDate, string sort_, TransactionManager tm_)
		{
			return await GetByRejectDateAsync(rejectDate, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RejectDate（字段） 查询
		/// </summary>
		/// /// <param name = "rejectDate">拒绝时间</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectDate(DateTime? rejectDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(rejectDate.HasValue ? "`RejectDate` = @RejectDate" : "`RejectDate` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (rejectDate.HasValue)
				paras_.Add(Database.CreateInParameter("@RejectDate", rejectDate.Value, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByRejectDateAsync(DateTime? rejectDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(rejectDate.HasValue ? "`RejectDate` = @RejectDate" : "`RejectDate` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (rejectDate.HasValue)
				paras_.Add(Database.CreateInParameter("@RejectDate", rejectDate.Value, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByRejectDate
		#region GetByRejectBy
		
		/// <summary>
		/// 按 RejectBy（字段） 查询
		/// </summary>
		/// /// <param name = "rejectBy">拒绝者</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectBy(string rejectBy)
		{
			return GetByRejectBy(rejectBy, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByRejectByAsync(string rejectBy)
		{
			return await GetByRejectByAsync(rejectBy, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RejectBy（字段） 查询
		/// </summary>
		/// /// <param name = "rejectBy">拒绝者</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectBy(string rejectBy, TransactionManager tm_)
		{
			return GetByRejectBy(rejectBy, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRejectByAsync(string rejectBy, TransactionManager tm_)
		{
			return await GetByRejectByAsync(rejectBy, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RejectBy（字段） 查询
		/// </summary>
		/// /// <param name = "rejectBy">拒绝者</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectBy(string rejectBy, int top_)
		{
			return GetByRejectBy(rejectBy, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByRejectByAsync(string rejectBy, int top_)
		{
			return await GetByRejectByAsync(rejectBy, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RejectBy（字段） 查询
		/// </summary>
		/// /// <param name = "rejectBy">拒绝者</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectBy(string rejectBy, int top_, TransactionManager tm_)
		{
			return GetByRejectBy(rejectBy, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRejectByAsync(string rejectBy, int top_, TransactionManager tm_)
		{
			return await GetByRejectByAsync(rejectBy, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RejectBy（字段） 查询
		/// </summary>
		/// /// <param name = "rejectBy">拒绝者</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectBy(string rejectBy, string sort_)
		{
			return GetByRejectBy(rejectBy, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByRejectByAsync(string rejectBy, string sort_)
		{
			return await GetByRejectByAsync(rejectBy, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RejectBy（字段） 查询
		/// </summary>
		/// /// <param name = "rejectBy">拒绝者</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectBy(string rejectBy, string sort_, TransactionManager tm_)
		{
			return GetByRejectBy(rejectBy, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRejectByAsync(string rejectBy, string sort_, TransactionManager tm_)
		{
			return await GetByRejectByAsync(rejectBy, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RejectBy（字段） 查询
		/// </summary>
		/// /// <param name = "rejectBy">拒绝者</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRejectBy(string rejectBy, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(rejectBy != null ? "`RejectBy` = @RejectBy" : "`RejectBy` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (rejectBy != null)
				paras_.Add(Database.CreateInParameter("@RejectBy", rejectBy, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByRejectByAsync(string rejectBy, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(rejectBy != null ? "`RejectBy` = @RejectBy" : "`RejectBy` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (rejectBy != null)
				paras_.Add(Database.CreateInParameter("@RejectBy", rejectBy, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByRejectBy
		#region GetByStatus
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByStatus(int status)
		{
			return GetByStatus(status, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByStatusAsync(int status)
		{
			return await GetByStatusAsync(status, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByStatus(int status, TransactionManager tm_)
		{
			return GetByStatus(status, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByStatusAsync(int status, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByStatus(int status, int top_)
		{
			return GetByStatus(status, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByStatusAsync(int status, int top_)
		{
			return await GetByStatusAsync(status, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByStatus(int status, int top_, TransactionManager tm_)
		{
			return GetByStatus(status, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByStatusAsync(int status, int top_, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByStatus(int status, string sort_)
		{
			return GetByStatus(status, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByStatusAsync(int status, string sort_)
		{
			return await GetByStatusAsync(status, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByStatus(int status, string sort_, TransactionManager tm_)
		{
			return GetByStatus(status, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByStatusAsync(int status, string sort_, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByStatus(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByStatusAsync(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByStatus
		#region GetByRecDate
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRecDate(DateTime recDate)
		{
			return GetByRecDate(recDate, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByRecDateAsync(DateTime recDate)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRecDate(DateTime recDate, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRecDateAsync(DateTime recDate, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRecDate(DateTime recDate, int top_)
		{
			return GetByRecDate(recDate, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByRecDateAsync(DateTime recDate, int top_)
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
		public List<Admin_userEO> GetByRecDate(DateTime recDate, int top_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRecDateAsync(DateTime recDate, int top_, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByRecDate(DateTime recDate, string sort_)
		{
			return GetByRecDate(recDate, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByRecDateAsync(DateTime recDate, string sort_)
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
		public List<Admin_userEO> GetByRecDate(DateTime recDate, string sort_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByRecDateAsync(DateTime recDate, string sort_, TransactionManager tm_)
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
		public List<Admin_userEO> GetByRecDate(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByRecDateAsync(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByRecDate
		#region GetByIcon
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">头像</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIcon(string icon)
		{
			return GetByIcon(icon, 0, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByIconAsync(string icon)
		{
			return await GetByIconAsync(icon, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">头像</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIcon(string icon, TransactionManager tm_)
		{
			return GetByIcon(icon, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByIconAsync(string icon, TransactionManager tm_)
		{
			return await GetByIconAsync(icon, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">头像</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIcon(string icon, int top_)
		{
			return GetByIcon(icon, top_, string.Empty, null);
		}
		public async Task<List<Admin_userEO>> GetByIconAsync(string icon, int top_)
		{
			return await GetByIconAsync(icon, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">头像</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIcon(string icon, int top_, TransactionManager tm_)
		{
			return GetByIcon(icon, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_userEO>> GetByIconAsync(string icon, int top_, TransactionManager tm_)
		{
			return await GetByIconAsync(icon, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">头像</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIcon(string icon, string sort_)
		{
			return GetByIcon(icon, 0, sort_, null);
		}
		public async Task<List<Admin_userEO>> GetByIconAsync(string icon, string sort_)
		{
			return await GetByIconAsync(icon, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">头像</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIcon(string icon, string sort_, TransactionManager tm_)
		{
			return GetByIcon(icon, 0, sort_, tm_);
		}
		public async Task<List<Admin_userEO>> GetByIconAsync(string icon, string sort_, TransactionManager tm_)
		{
			return await GetByIconAsync(icon, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">头像</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_userEO> GetByIcon(string icon, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(icon != null ? "`Icon` = @Icon" : "`Icon` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (icon != null)
				paras_.Add(Database.CreateInParameter("@Icon", icon, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		public async Task<List<Admin_userEO>> GetByIconAsync(string icon, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(icon != null ? "`Icon` = @Icon" : "`Icon` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (icon != null)
				paras_.Add(Database.CreateInParameter("@Icon", icon, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_userEO.MapDataReader);
		}
		#endregion // GetByIcon
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
