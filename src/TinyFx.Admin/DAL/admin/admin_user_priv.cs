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
	/// 
	/// 【表 admin_user_priv 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_user_privEO : IRowMapper<Admin_user_privEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_user_privEO()
		{
			this.PrivType = 0;
			this.IsEnabled = true;
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
		
		private int _originalPrivType;
		/// <summary>
		/// 【数据库中的原始主键 PrivType 值的副本，用于主键值更新】
		/// </summary>
		public int OriginalPrivType
		{
			get { return _originalPrivType; }
			set { HasOriginal = true; _originalPrivType = value; }
		}
		
		private string _originalPrivID;
		/// <summary>
		/// 【数据库中的原始主键 PrivID 值的副本，用于主键值更新】
		/// </summary>
		public string OriginalPrivID
		{
			get { return _originalPrivID; }
			set { HasOriginal = true; _originalPrivID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "UserID", UserID },  { "PrivType", PrivType },  { "PrivID", PrivID }, };
	    }
	    /// <summary>
	    /// 获取主键集合JSON格式
	    /// </summary>
	    /// <returns></returns>
	    public string GetPrimaryKeysJson() => SerializerUtil.SerializeJson(GetPrimaryKeys());
		#endregion // 主键原始值
		#region 所有字段
		/// <summary>
		/// 管理用户ID
		/// 【主键 varchar(40)】
		/// </summary>
		[DataMember(Order = 1)]
		public string UserID { get; set; }
		/// <summary>
		/// 权限类型 0-site 1-role 2-menu
		/// 【主键 int】
		/// </summary>
		[DataMember(Order = 2)]
		public int PrivType { get; set; }
		/// <summary>
		/// 角色编码或菜单编码
		/// 【主键 varchar(100)】
		/// </summary>
		[DataMember(Order = 3)]
		public string PrivID { get; set; }
		/// <summary>
		/// 是否允许
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 4)]
		public bool IsEnabled { get; set; }
		/// <summary>
		/// 功能和数据权限参数。格式：类型-参数| 类型-参数
		///              用于在定义页面内权限时可设置的权限选项列表
		///              如：ControlID-btnOk|ControlID-btnCancle
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 5)]
		public string PrivParamsValue { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_user_privEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_user_privEO MapDataReader(IDataReader reader)
		{
		    Admin_user_privEO ret = new Admin_user_privEO();
			ret.UserID = reader.ToString("UserID");
			ret.OriginalUserID = ret.UserID;
			ret.PrivType = reader.ToInt32("PrivType");
			ret.OriginalPrivType = ret.PrivType;
			ret.PrivID = reader.ToString("PrivID");
			ret.OriginalPrivID = ret.PrivID;
			ret.IsEnabled = reader.ToBoolean("IsEnabled");
			ret.PrivParamsValue = reader.ToString("PrivParamsValue");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 
	/// 【表 admin_user_priv 的操作类】
	/// </summary>
	public class Admin_user_privMO : MySqlTableMO<Admin_user_privEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_user_priv`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_user_privMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_user_privMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_user_privMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_user_privEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_user_privEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_user_privEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_user_priv` (`UserID`, `PrivType`, `PrivID`, `IsEnabled`, `PrivParamsValue`) VALUE (@UserID, @PrivType, @PrivID, @IsEnabled, @PrivParamsValue);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", item.UserID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", item.PrivType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", item.PrivID, MySqlDbType.VarChar),
				Database.CreateInParameter("@IsEnabled", item.IsEnabled, MySqlDbType.Byte),
				Database.CreateInParameter("@PrivParamsValue", item.PrivParamsValue != null ? item.PrivParamsValue : (object)DBNull.Value, MySqlDbType.Text),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(userID, privType, privID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(userID, privType, privID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(string userID, int privType, string privID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user_priv` WHERE `UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_user_privEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.UserID, item.PrivType, item.PrivID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_user_privEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.UserID, item.PrivType, item.PrivID, tm_);
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
			sql_ = @"DELETE FROM `admin_user_priv` WHERE `UserID` = @UserID";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar));
		}
		#endregion // RemoveByUserID
		#region RemoveByPrivType
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPrivType(int privType, TransactionManager tm_ = null)
		{
			RepairRemoveByPrivTypeData(privType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPrivTypeAsync(int privType, TransactionManager tm_ = null)
		{
			RepairRemoveByPrivTypeData(privType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByPrivTypeData(int privType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user_priv` WHERE `PrivType` = @PrivType";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32));
		}
		#endregion // RemoveByPrivType
		#region RemoveByPrivID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPrivID(string privID, TransactionManager tm_ = null)
		{
			RepairRemoveByPrivIDData(privID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPrivIDAsync(string privID, TransactionManager tm_ = null)
		{
			RepairRemoveByPrivIDData(privID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByPrivIDData(string privID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user_priv` WHERE `PrivID` = @PrivID";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar));
		}
		#endregion // RemoveByPrivID
		#region RemoveByIsEnabled
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByIsEnabled(bool isEnabled, TransactionManager tm_ = null)
		{
			RepairRemoveByIsEnabledData(isEnabled, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByIsEnabledAsync(bool isEnabled, TransactionManager tm_ = null)
		{
			RepairRemoveByIsEnabledData(isEnabled, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByIsEnabledData(bool isEnabled, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user_priv` WHERE `IsEnabled` = @IsEnabled";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsEnabled", isEnabled, MySqlDbType.Byte));
		}
		#endregion // RemoveByIsEnabled
		#region RemoveByPrivParamsValue
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPrivParamsValue(string privParamsValue, TransactionManager tm_ = null)
		{
			RepairRemoveByPrivParamsValueData(privParamsValue, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPrivParamsValueAsync(string privParamsValue, TransactionManager tm_ = null)
		{
			RepairRemoveByPrivParamsValueData(privParamsValue, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByPrivParamsValueData(string privParamsValue, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_user_priv` WHERE " + (privParamsValue != null ? "`PrivParamsValue` = @PrivParamsValue" : "`PrivParamsValue` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (privParamsValue != null)
				paras_.Add(Database.CreateInParameter("@PrivParamsValue", privParamsValue, MySqlDbType.Text));
		}
		#endregion // RemoveByPrivParamsValue
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
		public int Put(Admin_user_privEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_user_privEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_user_privEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user_priv` SET `UserID` = @UserID, `PrivType` = @PrivType, `PrivID` = @PrivID, `IsEnabled` = @IsEnabled, `PrivParamsValue` = @PrivParamsValue WHERE `UserID` = @UserID_Original AND `PrivType` = @PrivType_Original AND `PrivID` = @PrivID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", item.UserID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", item.PrivType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", item.PrivID, MySqlDbType.VarChar),
				Database.CreateInParameter("@IsEnabled", item.IsEnabled, MySqlDbType.Byte),
				Database.CreateInParameter("@PrivParamsValue", item.PrivParamsValue != null ? item.PrivParamsValue : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@UserID_Original", item.HasOriginal ? item.OriginalUserID : item.UserID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType_Original", item.HasOriginal ? item.OriginalPrivType : item.PrivType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID_Original", item.HasOriginal ? item.OriginalPrivID : item.PrivID, MySqlDbType.VarChar),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_user_privEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_user_privEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string userID, int privType, string privID, string set_, params object[] values_)
		{
			return Put(set_, "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", ConcatValues(values_, userID, privType, privID));
		}
		public async Task<int> PutByPKAsync(string userID, int privType, string privID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", ConcatValues(values_, userID, privType, privID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string userID, int privType, string privID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", tm_, ConcatValues(values_, userID, privType, privID));
		}
		public async Task<int> PutByPKAsync(string userID, int privType, string privID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", tm_, ConcatValues(values_, userID, privType, privID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string userID, int privType, string privID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
	        };
			return Put(set_, "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(string userID, int privType, string privID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
	        };
			return await PutAsync(set_, "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutUserID
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUserID(string userID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user_priv` SET `UserID` = @UserID";
			var parameter_ = Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutUserIDAsync(string userID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user_priv` SET `UserID` = @UserID";
			var parameter_ = Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutUserID
		#region PutPrivType
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPrivType(int privType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user_priv` SET `PrivType` = @PrivType";
			var parameter_ = Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutPrivTypeAsync(int privType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user_priv` SET `PrivType` = @PrivType";
			var parameter_ = Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutPrivType
		#region PutPrivID
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPrivID(string privID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user_priv` SET `PrivID` = @PrivID";
			var parameter_ = Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutPrivIDAsync(string privID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user_priv` SET `PrivID` = @PrivID";
			var parameter_ = Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutPrivID
		#region PutIsEnabled
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIsEnabledByPK(string userID, int privType, string privID, bool isEnabled, TransactionManager tm_ = null)
		{
			RepairPutIsEnabledByPKData(userID, privType, privID, isEnabled, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutIsEnabledByPKAsync(string userID, int privType, string privID, bool isEnabled, TransactionManager tm_ = null)
		{
			RepairPutIsEnabledByPKData(userID, privType, privID, isEnabled, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutIsEnabledByPKData(string userID, int privType, string privID, bool isEnabled, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user_priv` SET `IsEnabled` = @IsEnabled  WHERE `UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@IsEnabled", isEnabled, MySqlDbType.Byte),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIsEnabled(bool isEnabled, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user_priv` SET `IsEnabled` = @IsEnabled";
			var parameter_ = Database.CreateInParameter("@IsEnabled", isEnabled, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutIsEnabledAsync(bool isEnabled, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user_priv` SET `IsEnabled` = @IsEnabled";
			var parameter_ = Database.CreateInParameter("@IsEnabled", isEnabled, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutIsEnabled
		#region PutPrivParamsValue
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPrivParamsValueByPK(string userID, int privType, string privID, string privParamsValue, TransactionManager tm_ = null)
		{
			RepairPutPrivParamsValueByPKData(userID, privType, privID, privParamsValue, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutPrivParamsValueByPKAsync(string userID, int privType, string privID, string privParamsValue, TransactionManager tm_ = null)
		{
			RepairPutPrivParamsValueByPKData(userID, privType, privID, privParamsValue, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutPrivParamsValueByPKData(string userID, int privType, string privID, string privParamsValue, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_user_priv` SET `PrivParamsValue` = @PrivParamsValue  WHERE `UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@PrivParamsValue", privParamsValue != null ? privParamsValue : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPrivParamsValue(string privParamsValue, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user_priv` SET `PrivParamsValue` = @PrivParamsValue";
			var parameter_ = Database.CreateInParameter("@PrivParamsValue", privParamsValue != null ? privParamsValue : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutPrivParamsValueAsync(string privParamsValue, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_user_priv` SET `PrivParamsValue` = @PrivParamsValue";
			var parameter_ = Database.CreateInParameter("@PrivParamsValue", privParamsValue != null ? privParamsValue : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutPrivParamsValue
		#endregion // PutXXX
		#endregion // Put
	   
		#region Set
		
		/// <summary>
		/// 插入或者更新数据
		/// </summary>
		/// <param name = "item">要更新的实体对象</param>
		/// <param name="tm">事务管理对象</param>
		/// <return>true:插入操作；false:更新操作</return>
		public bool Set(Admin_user_privEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.UserID, item.PrivType, item.PrivID) == null)
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
		public async Task<bool> SetAsync(Admin_user_privEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.UserID, item.PrivType, item.PrivID) == null)
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
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_user_privEO GetByPK(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(userID, privType, privID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		public async Task<Admin_user_privEO> GetByPKAsync(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(userID, privType, privID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		private void RepairGetByPKData(string userID, int privType, string privID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 UserID（字段）
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetUserIDByPK(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`UserID`", "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", paras_, tm_);
		}
		public async Task<string> GetUserIDByPKAsync(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`UserID`", "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 PrivType（字段）
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetPrivTypeByPK(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`PrivType`", "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", paras_, tm_);
		}
		public async Task<int> GetPrivTypeByPKAsync(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`PrivType`", "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 PrivID（字段）
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetPrivIDByPK(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`PrivID`", "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", paras_, tm_);
		}
		public async Task<string> GetPrivIDByPKAsync(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`PrivID`", "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 IsEnabled（字段）
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public bool GetIsEnabledByPK(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
			return (bool)GetScalar("`IsEnabled`", "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", paras_, tm_);
		}
		public async Task<bool> GetIsEnabledByPKAsync(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
			return (bool)await GetScalarAsync("`IsEnabled`", "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 PrivParamsValue（字段）
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetPrivParamsValueByPK(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`PrivParamsValue`", "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", paras_, tm_);
		}
		public async Task<string> GetPrivParamsValueByPKAsync(string userID, int privType, string privID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32),
				Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`PrivParamsValue`", "`UserID` = @UserID AND `PrivType` = @PrivType AND `PrivID` = @PrivID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByUserID
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByUserID(string userID)
		{
			return GetByUserID(userID, 0, string.Empty, null);
		}
		public async Task<List<Admin_user_privEO>> GetByUserIDAsync(string userID)
		{
			return await GetByUserIDAsync(userID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByUserID(string userID, TransactionManager tm_)
		{
			return GetByUserID(userID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByUserIDAsync(string userID, TransactionManager tm_)
		{
			return await GetByUserIDAsync(userID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByUserID(string userID, int top_)
		{
			return GetByUserID(userID, top_, string.Empty, null);
		}
		public async Task<List<Admin_user_privEO>> GetByUserIDAsync(string userID, int top_)
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
		public List<Admin_user_privEO> GetByUserID(string userID, int top_, TransactionManager tm_)
		{
			return GetByUserID(userID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByUserIDAsync(string userID, int top_, TransactionManager tm_)
		{
			return await GetByUserIDAsync(userID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByUserID(string userID, string sort_)
		{
			return GetByUserID(userID, 0, sort_, null);
		}
		public async Task<List<Admin_user_privEO>> GetByUserIDAsync(string userID, string sort_)
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
		public List<Admin_user_privEO> GetByUserID(string userID, string sort_, TransactionManager tm_)
		{
			return GetByUserID(userID, 0, sort_, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByUserIDAsync(string userID, string sort_, TransactionManager tm_)
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
		public List<Admin_user_privEO> GetByUserID(string userID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`UserID` = @UserID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		public async Task<List<Admin_user_privEO>> GetByUserIDAsync(string userID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`UserID` = @UserID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		#endregion // GetByUserID
		#region GetByPrivType
		
		/// <summary>
		/// 按 PrivType（字段） 查询
		/// </summary>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivType(int privType)
		{
			return GetByPrivType(privType, 0, string.Empty, null);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivTypeAsync(int privType)
		{
			return await GetByPrivTypeAsync(privType, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivType（字段） 查询
		/// </summary>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivType(int privType, TransactionManager tm_)
		{
			return GetByPrivType(privType, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivTypeAsync(int privType, TransactionManager tm_)
		{
			return await GetByPrivTypeAsync(privType, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivType（字段） 查询
		/// </summary>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivType(int privType, int top_)
		{
			return GetByPrivType(privType, top_, string.Empty, null);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivTypeAsync(int privType, int top_)
		{
			return await GetByPrivTypeAsync(privType, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivType（字段） 查询
		/// </summary>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivType(int privType, int top_, TransactionManager tm_)
		{
			return GetByPrivType(privType, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivTypeAsync(int privType, int top_, TransactionManager tm_)
		{
			return await GetByPrivTypeAsync(privType, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivType（字段） 查询
		/// </summary>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivType(int privType, string sort_)
		{
			return GetByPrivType(privType, 0, sort_, null);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivTypeAsync(int privType, string sort_)
		{
			return await GetByPrivTypeAsync(privType, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 PrivType（字段） 查询
		/// </summary>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivType(int privType, string sort_, TransactionManager tm_)
		{
			return GetByPrivType(privType, 0, sort_, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivTypeAsync(int privType, string sort_, TransactionManager tm_)
		{
			return await GetByPrivTypeAsync(privType, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 PrivType（字段） 查询
		/// </summary>
		/// /// <param name = "privType">权限类型 0-site 1-role 2-menu</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivType(int privType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`PrivType` = @PrivType", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivTypeAsync(int privType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`PrivType` = @PrivType", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@PrivType", privType, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		#endregion // GetByPrivType
		#region GetByPrivID
		
		/// <summary>
		/// 按 PrivID（字段） 查询
		/// </summary>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivID(string privID)
		{
			return GetByPrivID(privID, 0, string.Empty, null);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivIDAsync(string privID)
		{
			return await GetByPrivIDAsync(privID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivID（字段） 查询
		/// </summary>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivID(string privID, TransactionManager tm_)
		{
			return GetByPrivID(privID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivIDAsync(string privID, TransactionManager tm_)
		{
			return await GetByPrivIDAsync(privID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivID（字段） 查询
		/// </summary>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivID(string privID, int top_)
		{
			return GetByPrivID(privID, top_, string.Empty, null);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivIDAsync(string privID, int top_)
		{
			return await GetByPrivIDAsync(privID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivID（字段） 查询
		/// </summary>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivID(string privID, int top_, TransactionManager tm_)
		{
			return GetByPrivID(privID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivIDAsync(string privID, int top_, TransactionManager tm_)
		{
			return await GetByPrivIDAsync(privID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivID（字段） 查询
		/// </summary>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivID(string privID, string sort_)
		{
			return GetByPrivID(privID, 0, sort_, null);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivIDAsync(string privID, string sort_)
		{
			return await GetByPrivIDAsync(privID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 PrivID（字段） 查询
		/// </summary>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivID(string privID, string sort_, TransactionManager tm_)
		{
			return GetByPrivID(privID, 0, sort_, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivIDAsync(string privID, string sort_, TransactionManager tm_)
		{
			return await GetByPrivIDAsync(privID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 PrivID（字段） 查询
		/// </summary>
		/// /// <param name = "privID">角色编码或菜单编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivID(string privID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`PrivID` = @PrivID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivIDAsync(string privID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`PrivID` = @PrivID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@PrivID", privID, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		#endregion // GetByPrivID
		#region GetByIsEnabled
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByIsEnabled(bool isEnabled)
		{
			return GetByIsEnabled(isEnabled, 0, string.Empty, null);
		}
		public async Task<List<Admin_user_privEO>> GetByIsEnabledAsync(bool isEnabled)
		{
			return await GetByIsEnabledAsync(isEnabled, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByIsEnabled(bool isEnabled, TransactionManager tm_)
		{
			return GetByIsEnabled(isEnabled, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByIsEnabledAsync(bool isEnabled, TransactionManager tm_)
		{
			return await GetByIsEnabledAsync(isEnabled, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByIsEnabled(bool isEnabled, int top_)
		{
			return GetByIsEnabled(isEnabled, top_, string.Empty, null);
		}
		public async Task<List<Admin_user_privEO>> GetByIsEnabledAsync(bool isEnabled, int top_)
		{
			return await GetByIsEnabledAsync(isEnabled, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByIsEnabled(bool isEnabled, int top_, TransactionManager tm_)
		{
			return GetByIsEnabled(isEnabled, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByIsEnabledAsync(bool isEnabled, int top_, TransactionManager tm_)
		{
			return await GetByIsEnabledAsync(isEnabled, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByIsEnabled(bool isEnabled, string sort_)
		{
			return GetByIsEnabled(isEnabled, 0, sort_, null);
		}
		public async Task<List<Admin_user_privEO>> GetByIsEnabledAsync(bool isEnabled, string sort_)
		{
			return await GetByIsEnabledAsync(isEnabled, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByIsEnabled(bool isEnabled, string sort_, TransactionManager tm_)
		{
			return GetByIsEnabled(isEnabled, 0, sort_, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByIsEnabledAsync(bool isEnabled, string sort_, TransactionManager tm_)
		{
			return await GetByIsEnabledAsync(isEnabled, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByIsEnabled(bool isEnabled, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`IsEnabled` = @IsEnabled", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsEnabled", isEnabled, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		public async Task<List<Admin_user_privEO>> GetByIsEnabledAsync(bool isEnabled, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`IsEnabled` = @IsEnabled", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsEnabled", isEnabled, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		#endregion // GetByIsEnabled
		#region GetByPrivParamsValue
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivParamsValue(string privParamsValue)
		{
			return GetByPrivParamsValue(privParamsValue, 0, string.Empty, null);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivParamsValueAsync(string privParamsValue)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivParamsValue(string privParamsValue, TransactionManager tm_)
		{
			return GetByPrivParamsValue(privParamsValue, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivParamsValueAsync(string privParamsValue, TransactionManager tm_)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivParamsValue(string privParamsValue, int top_)
		{
			return GetByPrivParamsValue(privParamsValue, top_, string.Empty, null);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivParamsValueAsync(string privParamsValue, int top_)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivParamsValue(string privParamsValue, int top_, TransactionManager tm_)
		{
			return GetByPrivParamsValue(privParamsValue, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivParamsValueAsync(string privParamsValue, int top_, TransactionManager tm_)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivParamsValue(string privParamsValue, string sort_)
		{
			return GetByPrivParamsValue(privParamsValue, 0, sort_, null);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivParamsValueAsync(string privParamsValue, string sort_)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivParamsValue(string privParamsValue, string sort_, TransactionManager tm_)
		{
			return GetByPrivParamsValue(privParamsValue, 0, sort_, tm_);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivParamsValueAsync(string privParamsValue, string sort_, TransactionManager tm_)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_user_privEO> GetByPrivParamsValue(string privParamsValue, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(privParamsValue != null ? "`PrivParamsValue` = @PrivParamsValue" : "`PrivParamsValue` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (privParamsValue != null)
				paras_.Add(Database.CreateInParameter("@PrivParamsValue", privParamsValue, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		public async Task<List<Admin_user_privEO>> GetByPrivParamsValueAsync(string privParamsValue, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(privParamsValue != null ? "`PrivParamsValue` = @PrivParamsValue" : "`PrivParamsValue` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (privParamsValue != null)
				paras_.Add(Database.CreateInParameter("@PrivParamsValue", privParamsValue, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_user_privEO.MapDataReader);
		}
		#endregion // GetByPrivParamsValue
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
