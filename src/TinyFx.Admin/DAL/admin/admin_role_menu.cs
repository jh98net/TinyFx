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
	/// 用户菜单权限，对单个菜单授权，不针对目录
	/// 【表 admin_role_menu 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_role_menuEO : IRowMapper<Admin_role_menuEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_role_menuEO()
		{
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
		private int _originalRoleID;
		/// <summary>
		/// 【数据库中的原始主键 RoleID 值的副本，用于主键值更新】
		/// </summary>
		public int OriginalRoleID
		{
			get { return _originalRoleID; }
			set { HasOriginal = true; _originalRoleID = value; }
		}
		
		private string _originalMenuID;
		/// <summary>
		/// 【数据库中的原始主键 MenuID 值的副本，用于主键值更新】
		/// </summary>
		public string OriginalMenuID
		{
			get { return _originalMenuID; }
			set { HasOriginal = true; _originalMenuID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "RoleID", RoleID },  { "MenuID", MenuID }, };
	    }
	    /// <summary>
	    /// 获取主键集合JSON格式
	    /// </summary>
	    /// <returns></returns>
	    public string GetPrimaryKeysJson() => SerializerUtil.SerializeJson(GetPrimaryKeys());
		#endregion // 主键原始值
		#region 所有字段
		/// <summary>
		/// 权限标识
		/// 【主键 int】
		/// </summary>
		[DataMember(Order = 1)]
		public int RoleID { get; set; }
		/// <summary>
		/// 菜单编码GUID
		/// 【主键 varchar(40)】
		/// </summary>
		[DataMember(Order = 2)]
		public string MenuID { get; set; }
		/// <summary>
		/// 功能和数据权限参数。
		///              类型-参数-是否有权限| 类型-参数-是否有权限
		///              如：ControlID-btnOk-true|ControlID-btnCancle-false
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 3)]
		public string PrivParamsValue { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_role_menuEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_role_menuEO MapDataReader(IDataReader reader)
		{
		    Admin_role_menuEO ret = new Admin_role_menuEO();
			ret.RoleID = reader.ToInt32("RoleID");
			ret.OriginalRoleID = ret.RoleID;
			ret.MenuID = reader.ToString("MenuID");
			ret.OriginalMenuID = ret.MenuID;
			ret.PrivParamsValue = reader.ToString("PrivParamsValue");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 用户菜单权限，对单个菜单授权，不针对目录
	/// 【表 admin_role_menu 的操作类】
	/// </summary>
	public class Admin_role_menuMO : MySqlTableMO<Admin_role_menuEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_role_menu`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_role_menuMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_role_menuMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_role_menuMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_role_menuEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_role_menuEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_role_menuEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_role_menu` (`RoleID`, `MenuID`, `PrivParamsValue`) VALUE (@RoleID, @MenuID, @PrivParamsValue);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", item.RoleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", item.MenuID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivParamsValue", item.PrivParamsValue != null ? item.PrivParamsValue : (object)DBNull.Value, MySqlDbType.Text),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(int roleID, string menuID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(roleID, menuID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(int roleID, string menuID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(roleID, menuID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(int roleID, string menuID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_role_menu` WHERE `RoleID` = @RoleID AND `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_role_menuEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.RoleID, item.MenuID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_role_menuEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.RoleID, item.MenuID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByRoleID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByRoleID(int roleID, TransactionManager tm_ = null)
		{
			RepairRemoveByRoleIDData(roleID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByRoleIDAsync(int roleID, TransactionManager tm_ = null)
		{
			RepairRemoveByRoleIDData(roleID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByRoleIDData(int roleID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_role_menu` WHERE `RoleID` = @RoleID";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32));
		}
		#endregion // RemoveByRoleID
		#region RemoveByMenuID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByMenuID(string menuID, TransactionManager tm_ = null)
		{
			RepairRemoveByMenuIDData(menuID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByMenuIDAsync(string menuID, TransactionManager tm_ = null)
		{
			RepairRemoveByMenuIDData(menuID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByMenuIDData(string menuID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_role_menu` WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar));
		}
		#endregion // RemoveByMenuID
		#region RemoveByPrivParamsValue
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。</param>
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
			sql_ = @"DELETE FROM `admin_role_menu` WHERE " + (privParamsValue != null ? "`PrivParamsValue` = @PrivParamsValue" : "`PrivParamsValue` IS NULL");
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
		public int Put(Admin_role_menuEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_role_menuEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_role_menuEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_role_menu` SET `RoleID` = @RoleID, `MenuID` = @MenuID, `PrivParamsValue` = @PrivParamsValue WHERE `RoleID` = @RoleID_Original AND `MenuID` = @MenuID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", item.RoleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", item.MenuID, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrivParamsValue", item.PrivParamsValue != null ? item.PrivParamsValue : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@RoleID_Original", item.HasOriginal ? item.OriginalRoleID : item.RoleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID_Original", item.HasOriginal ? item.OriginalMenuID : item.MenuID, MySqlDbType.VarChar),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_role_menuEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_role_menuEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "roleID">权限标识</param>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(int roleID, string menuID, string set_, params object[] values_)
		{
			return Put(set_, "`RoleID` = @RoleID AND `MenuID` = @MenuID", ConcatValues(values_, roleID, menuID));
		}
		public async Task<int> PutByPKAsync(int roleID, string menuID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`RoleID` = @RoleID AND `MenuID` = @MenuID", ConcatValues(values_, roleID, menuID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(int roleID, string menuID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`RoleID` = @RoleID AND `MenuID` = @MenuID", tm_, ConcatValues(values_, roleID, menuID));
		}
		public async Task<int> PutByPKAsync(int roleID, string menuID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`RoleID` = @RoleID AND `MenuID` = @MenuID", tm_, ConcatValues(values_, roleID, menuID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(int roleID, string menuID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
	        };
			return Put(set_, "`RoleID` = @RoleID AND `MenuID` = @MenuID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(int roleID, string menuID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
	        };
			return await PutAsync(set_, "`RoleID` = @RoleID AND `MenuID` = @MenuID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutRoleID
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRoleID(int roleID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_role_menu` SET `RoleID` = @RoleID";
			var parameter_ = Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRoleIDAsync(int roleID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_role_menu` SET `RoleID` = @RoleID";
			var parameter_ = Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRoleID
		#region PutMenuID
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutMenuID(string menuID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_role_menu` SET `MenuID` = @MenuID";
			var parameter_ = Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutMenuIDAsync(string menuID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_role_menu` SET `MenuID` = @MenuID";
			var parameter_ = Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutMenuID
		#region PutPrivParamsValue
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "privParamsValue">功能和数据权限参数。</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPrivParamsValueByPK(int roleID, string menuID, string privParamsValue, TransactionManager tm_ = null)
		{
			RepairPutPrivParamsValueByPKData(roleID, menuID, privParamsValue, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutPrivParamsValueByPKAsync(int roleID, string menuID, string privParamsValue, TransactionManager tm_ = null)
		{
			RepairPutPrivParamsValueByPKData(roleID, menuID, privParamsValue, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutPrivParamsValueByPKData(int roleID, string menuID, string privParamsValue, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_role_menu` SET `PrivParamsValue` = @PrivParamsValue  WHERE `RoleID` = @RoleID AND `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@PrivParamsValue", privParamsValue != null ? privParamsValue : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPrivParamsValue(string privParamsValue, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_role_menu` SET `PrivParamsValue` = @PrivParamsValue";
			var parameter_ = Database.CreateInParameter("@PrivParamsValue", privParamsValue != null ? privParamsValue : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutPrivParamsValueAsync(string privParamsValue, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_role_menu` SET `PrivParamsValue` = @PrivParamsValue";
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
		public bool Set(Admin_role_menuEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.RoleID, item.MenuID) == null)
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
		public async Task<bool> SetAsync(Admin_role_menuEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.RoleID, item.MenuID) == null)
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
		/// /// <param name = "roleID">权限标识</param>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_role_menuEO GetByPK(int roleID, string menuID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(roleID, menuID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_role_menuEO.MapDataReader);
		}
		public async Task<Admin_role_menuEO> GetByPKAsync(int roleID, string menuID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(roleID, menuID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_role_menuEO.MapDataReader);
		}
		private void RepairGetByPKData(int roleID, string menuID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`RoleID` = @RoleID AND `MenuID` = @MenuID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 RoleID（字段）
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetRoleIDByPK(int roleID, string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`RoleID`", "`RoleID` = @RoleID AND `MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<int> GetRoleIDByPKAsync(int roleID, string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`RoleID`", "`RoleID` = @RoleID AND `MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 MenuID（字段）
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetMenuIDByPK(int roleID, string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`MenuID`", "`RoleID` = @RoleID AND `MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<string> GetMenuIDByPKAsync(int roleID, string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`MenuID`", "`RoleID` = @RoleID AND `MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 PrivParamsValue（字段）
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetPrivParamsValueByPK(int roleID, string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`PrivParamsValue`", "`RoleID` = @RoleID AND `MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<string> GetPrivParamsValueByPKAsync(int roleID, string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`PrivParamsValue`", "`RoleID` = @RoleID AND `MenuID` = @MenuID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByRoleID
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByRoleID(int roleID)
		{
			return GetByRoleID(roleID, 0, string.Empty, null);
		}
		public async Task<List<Admin_role_menuEO>> GetByRoleIDAsync(int roleID)
		{
			return await GetByRoleIDAsync(roleID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByRoleID(int roleID, TransactionManager tm_)
		{
			return GetByRoleID(roleID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_role_menuEO>> GetByRoleIDAsync(int roleID, TransactionManager tm_)
		{
			return await GetByRoleIDAsync(roleID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByRoleID(int roleID, int top_)
		{
			return GetByRoleID(roleID, top_, string.Empty, null);
		}
		public async Task<List<Admin_role_menuEO>> GetByRoleIDAsync(int roleID, int top_)
		{
			return await GetByRoleIDAsync(roleID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByRoleID(int roleID, int top_, TransactionManager tm_)
		{
			return GetByRoleID(roleID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_role_menuEO>> GetByRoleIDAsync(int roleID, int top_, TransactionManager tm_)
		{
			return await GetByRoleIDAsync(roleID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByRoleID(int roleID, string sort_)
		{
			return GetByRoleID(roleID, 0, sort_, null);
		}
		public async Task<List<Admin_role_menuEO>> GetByRoleIDAsync(int roleID, string sort_)
		{
			return await GetByRoleIDAsync(roleID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByRoleID(int roleID, string sort_, TransactionManager tm_)
		{
			return GetByRoleID(roleID, 0, sort_, tm_);
		}
		public async Task<List<Admin_role_menuEO>> GetByRoleIDAsync(int roleID, string sort_, TransactionManager tm_)
		{
			return await GetByRoleIDAsync(roleID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID">权限标识</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByRoleID(int roleID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RoleID` = @RoleID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_role_menuEO.MapDataReader);
		}
		public async Task<List<Admin_role_menuEO>> GetByRoleIDAsync(int roleID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RoleID` = @RoleID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_role_menuEO.MapDataReader);
		}
		#endregion // GetByRoleID
		#region GetByMenuID
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByMenuID(string menuID)
		{
			return GetByMenuID(menuID, 0, string.Empty, null);
		}
		public async Task<List<Admin_role_menuEO>> GetByMenuIDAsync(string menuID)
		{
			return await GetByMenuIDAsync(menuID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByMenuID(string menuID, TransactionManager tm_)
		{
			return GetByMenuID(menuID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_role_menuEO>> GetByMenuIDAsync(string menuID, TransactionManager tm_)
		{
			return await GetByMenuIDAsync(menuID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByMenuID(string menuID, int top_)
		{
			return GetByMenuID(menuID, top_, string.Empty, null);
		}
		public async Task<List<Admin_role_menuEO>> GetByMenuIDAsync(string menuID, int top_)
		{
			return await GetByMenuIDAsync(menuID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByMenuID(string menuID, int top_, TransactionManager tm_)
		{
			return GetByMenuID(menuID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_role_menuEO>> GetByMenuIDAsync(string menuID, int top_, TransactionManager tm_)
		{
			return await GetByMenuIDAsync(menuID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByMenuID(string menuID, string sort_)
		{
			return GetByMenuID(menuID, 0, sort_, null);
		}
		public async Task<List<Admin_role_menuEO>> GetByMenuIDAsync(string menuID, string sort_)
		{
			return await GetByMenuIDAsync(menuID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByMenuID(string menuID, string sort_, TransactionManager tm_)
		{
			return GetByMenuID(menuID, 0, sort_, tm_);
		}
		public async Task<List<Admin_role_menuEO>> GetByMenuIDAsync(string menuID, string sort_, TransactionManager tm_)
		{
			return await GetByMenuIDAsync(menuID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByMenuID(string menuID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`MenuID` = @MenuID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_role_menuEO.MapDataReader);
		}
		public async Task<List<Admin_role_menuEO>> GetByMenuIDAsync(string menuID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`MenuID` = @MenuID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_role_menuEO.MapDataReader);
		}
		#endregion // GetByMenuID
		#region GetByPrivParamsValue
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByPrivParamsValue(string privParamsValue)
		{
			return GetByPrivParamsValue(privParamsValue, 0, string.Empty, null);
		}
		public async Task<List<Admin_role_menuEO>> GetByPrivParamsValueAsync(string privParamsValue)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByPrivParamsValue(string privParamsValue, TransactionManager tm_)
		{
			return GetByPrivParamsValue(privParamsValue, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_role_menuEO>> GetByPrivParamsValueAsync(string privParamsValue, TransactionManager tm_)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByPrivParamsValue(string privParamsValue, int top_)
		{
			return GetByPrivParamsValue(privParamsValue, top_, string.Empty, null);
		}
		public async Task<List<Admin_role_menuEO>> GetByPrivParamsValueAsync(string privParamsValue, int top_)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByPrivParamsValue(string privParamsValue, int top_, TransactionManager tm_)
		{
			return GetByPrivParamsValue(privParamsValue, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_role_menuEO>> GetByPrivParamsValueAsync(string privParamsValue, int top_, TransactionManager tm_)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByPrivParamsValue(string privParamsValue, string sort_)
		{
			return GetByPrivParamsValue(privParamsValue, 0, sort_, null);
		}
		public async Task<List<Admin_role_menuEO>> GetByPrivParamsValueAsync(string privParamsValue, string sort_)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByPrivParamsValue(string privParamsValue, string sort_, TransactionManager tm_)
		{
			return GetByPrivParamsValue(privParamsValue, 0, sort_, tm_);
		}
		public async Task<List<Admin_role_menuEO>> GetByPrivParamsValueAsync(string privParamsValue, string sort_, TransactionManager tm_)
		{
			return await GetByPrivParamsValueAsync(privParamsValue, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_role_menuEO> GetByPrivParamsValue(string privParamsValue, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(privParamsValue != null ? "`PrivParamsValue` = @PrivParamsValue" : "`PrivParamsValue` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (privParamsValue != null)
				paras_.Add(Database.CreateInParameter("@PrivParamsValue", privParamsValue, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_role_menuEO.MapDataReader);
		}
		public async Task<List<Admin_role_menuEO>> GetByPrivParamsValueAsync(string privParamsValue, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(privParamsValue != null ? "`PrivParamsValue` = @PrivParamsValue" : "`PrivParamsValue` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (privParamsValue != null)
				paras_.Add(Database.CreateInParameter("@PrivParamsValue", privParamsValue, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_role_menuEO.MapDataReader);
		}
		#endregion // GetByPrivParamsValue
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
