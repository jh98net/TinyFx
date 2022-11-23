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
	/// 角色表
	/// 【表 admin_role 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_roleEO : IRowMapper<Admin_roleEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_roleEO()
		{
			this.Status = 0;
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
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "RoleID", RoleID }, };
	    }
	    /// <summary>
	    /// 获取主键集合JSON格式
	    /// </summary>
	    /// <returns></returns>
	    public string GetPrimaryKeysJson() => SerializerUtil.SerializeJson(GetPrimaryKeys());
		#endregion // 主键原始值
		#region 所有字段
		/// <summary>
		/// 角色标识
		/// 【主键 int】
		/// </summary>
		[DataMember(Order = 1)]
		public int RoleID { get; set; }
		/// <summary>
		/// 角色名称
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 2)]
		public string RoleName { get; set; }
		/// <summary>
		/// 描述
		/// 【字段 varchar(255)】
		/// </summary>
		[DataMember(Order = 3)]
		public string Desc { get; set; }
		/// <summary>
		/// 状态 0-无效 1-有效
		/// 【字段 tinyint】
		/// </summary>
		[DataMember(Order = 4)]
		public int Status { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_roleEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_roleEO MapDataReader(IDataReader reader)
		{
		    Admin_roleEO ret = new Admin_roleEO();
			ret.RoleID = reader.ToInt32("RoleID");
			ret.OriginalRoleID = ret.RoleID;
			ret.RoleName = reader.ToString("RoleName");
			ret.Desc = reader.ToString("Desc");
			ret.Status = reader.ToInt32("Status");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 角色表
	/// 【表 admin_role 的操作类】
	/// </summary>
	public class Admin_roleMO : MySqlTableMO<Admin_roleEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_role`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_roleMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_roleMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_roleMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_roleEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_roleEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_roleEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_role` (`RoleID`, `RoleName`, `Desc`, `Status`) VALUE (@RoleID, @RoleName, @Desc, @Status);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", item.RoleID, MySqlDbType.Int32),
				Database.CreateInParameter("@RoleName", item.RoleName != null ? item.RoleName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Desc", item.Desc != null ? item.Desc : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Status", item.Status, MySqlDbType.Byte),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "roleID">角色标识</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(int roleID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(roleID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(int roleID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(roleID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(int roleID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_role` WHERE `RoleID` = @RoleID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_roleEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.RoleID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_roleEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.RoleID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByRoleName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "roleName">角色名称</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByRoleName(string roleName, TransactionManager tm_ = null)
		{
			RepairRemoveByRoleNameData(roleName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByRoleNameAsync(string roleName, TransactionManager tm_ = null)
		{
			RepairRemoveByRoleNameData(roleName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByRoleNameData(string roleName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_role` WHERE " + (roleName != null ? "`RoleName` = @RoleName" : "`RoleName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (roleName != null)
				paras_.Add(Database.CreateInParameter("@RoleName", roleName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByRoleName
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
			sql_ = @"DELETE FROM `admin_role` WHERE " + (desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.VarChar));
		}
		#endregion // RemoveByDesc
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
			sql_ = @"DELETE FROM `admin_role` WHERE `Status` = @Status";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
		}
		#endregion // RemoveByStatus
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
		public int Put(Admin_roleEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_roleEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_roleEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_role` SET `RoleID` = @RoleID, `RoleName` = @RoleName, `Desc` = @Desc, `Status` = @Status WHERE `RoleID` = @RoleID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", item.RoleID, MySqlDbType.Int32),
				Database.CreateInParameter("@RoleName", item.RoleName != null ? item.RoleName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Desc", item.Desc != null ? item.Desc : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Status", item.Status, MySqlDbType.Byte),
				Database.CreateInParameter("@RoleID_Original", item.HasOriginal ? item.OriginalRoleID : item.RoleID, MySqlDbType.Int32),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_roleEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_roleEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "roleID">角色标识</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(int roleID, string set_, params object[] values_)
		{
			return Put(set_, "`RoleID` = @RoleID", ConcatValues(values_, roleID));
		}
		public async Task<int> PutByPKAsync(int roleID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`RoleID` = @RoleID", ConcatValues(values_, roleID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "roleID">角色标识</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(int roleID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`RoleID` = @RoleID", tm_, ConcatValues(values_, roleID));
		}
		public async Task<int> PutByPKAsync(int roleID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`RoleID` = @RoleID", tm_, ConcatValues(values_, roleID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "roleID">角色标识</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(int roleID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
	        };
			return Put(set_, "`RoleID` = @RoleID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(int roleID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
	        };
			return await PutAsync(set_, "`RoleID` = @RoleID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutRoleName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "roleID">角色标识</param>
		/// /// <param name = "roleName">角色名称</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRoleNameByPK(int roleID, string roleName, TransactionManager tm_ = null)
		{
			RepairPutRoleNameByPKData(roleID, roleName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRoleNameByPKAsync(int roleID, string roleName, TransactionManager tm_ = null)
		{
			RepairPutRoleNameByPKData(roleID, roleName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRoleNameByPKData(int roleID, string roleName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_role` SET `RoleName` = @RoleName  WHERE `RoleID` = @RoleID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleName", roleName != null ? roleName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "roleName">角色名称</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRoleName(string roleName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_role` SET `RoleName` = @RoleName";
			var parameter_ = Database.CreateInParameter("@RoleName", roleName != null ? roleName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRoleNameAsync(string roleName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_role` SET `RoleName` = @RoleName";
			var parameter_ = Database.CreateInParameter("@RoleName", roleName != null ? roleName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRoleName
		#region PutDesc
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "roleID">角色标识</param>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDescByPK(int roleID, string desc, TransactionManager tm_ = null)
		{
			RepairPutDescByPKData(roleID, desc, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDescByPKAsync(int roleID, string desc, TransactionManager tm_ = null)
		{
			RepairPutDescByPKData(roleID, desc, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDescByPKData(int roleID, string desc, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_role` SET `Desc` = @Desc  WHERE `RoleID` = @RoleID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
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
			const string sql_ = @"UPDATE `admin_role` SET `Desc` = @Desc";
			var parameter_ = Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDescAsync(string desc, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_role` SET `Desc` = @Desc";
			var parameter_ = Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDesc
		#region PutStatus
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "roleID">角色标识</param>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutStatusByPK(int roleID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(roleID, status, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutStatusByPKAsync(int roleID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(roleID, status, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutStatusByPKData(int roleID, int status, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_role` SET `Status` = @Status  WHERE `RoleID` = @RoleID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Status", status, MySqlDbType.Byte),
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
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
			const string sql_ = @"UPDATE `admin_role` SET `Status` = @Status";
			var parameter_ = Database.CreateInParameter("@Status", status, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutStatusAsync(int status, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_role` SET `Status` = @Status";
			var parameter_ = Database.CreateInParameter("@Status", status, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutStatus
		#endregion // PutXXX
		#endregion // Put
	   
		#region Set
		
		/// <summary>
		/// 插入或者更新数据
		/// </summary>
		/// <param name = "item">要更新的实体对象</param>
		/// <param name="tm">事务管理对象</param>
		/// <return>true:插入操作；false:更新操作</return>
		public bool Set(Admin_roleEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.RoleID) == null)
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
		public async Task<bool> SetAsync(Admin_roleEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.RoleID) == null)
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
		/// /// <param name = "roleID">角色标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_roleEO GetByPK(int roleID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(roleID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_roleEO.MapDataReader);
		}
		public async Task<Admin_roleEO> GetByPKAsync(int roleID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(roleID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_roleEO.MapDataReader);
		}
		private void RepairGetByPKData(int roleID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`RoleID` = @RoleID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 RoleName（字段）
		/// </summary>
		/// /// <param name = "roleID">角色标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetRoleNameByPK(int roleID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
			};
			return (string)GetScalar("`RoleName`", "`RoleID` = @RoleID", paras_, tm_);
		}
		public async Task<string> GetRoleNameByPKAsync(int roleID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
			};
			return (string)await GetScalarAsync("`RoleName`", "`RoleID` = @RoleID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Desc（字段）
		/// </summary>
		/// /// <param name = "roleID">角色标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetDescByPK(int roleID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
			};
			return (string)GetScalar("`Desc`", "`RoleID` = @RoleID", paras_, tm_);
		}
		public async Task<string> GetDescByPKAsync(int roleID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
			};
			return (string)await GetScalarAsync("`Desc`", "`RoleID` = @RoleID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Status（字段）
		/// </summary>
		/// /// <param name = "roleID">角色标识</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetStatusByPK(int roleID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
			};
			return (int)GetScalar("`Status`", "`RoleID` = @RoleID", paras_, tm_);
		}
		public async Task<int> GetStatusByPKAsync(int roleID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32),
			};
			return (int)await GetScalarAsync("`Status`", "`RoleID` = @RoleID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByRoleName
		
		/// <summary>
		/// 按 RoleName（字段） 查询
		/// </summary>
		/// /// <param name = "roleName">角色名称</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByRoleName(string roleName)
		{
			return GetByRoleName(roleName, 0, string.Empty, null);
		}
		public async Task<List<Admin_roleEO>> GetByRoleNameAsync(string roleName)
		{
			return await GetByRoleNameAsync(roleName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RoleName（字段） 查询
		/// </summary>
		/// /// <param name = "roleName">角色名称</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByRoleName(string roleName, TransactionManager tm_)
		{
			return GetByRoleName(roleName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_roleEO>> GetByRoleNameAsync(string roleName, TransactionManager tm_)
		{
			return await GetByRoleNameAsync(roleName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RoleName（字段） 查询
		/// </summary>
		/// /// <param name = "roleName">角色名称</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByRoleName(string roleName, int top_)
		{
			return GetByRoleName(roleName, top_, string.Empty, null);
		}
		public async Task<List<Admin_roleEO>> GetByRoleNameAsync(string roleName, int top_)
		{
			return await GetByRoleNameAsync(roleName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RoleName（字段） 查询
		/// </summary>
		/// /// <param name = "roleName">角色名称</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByRoleName(string roleName, int top_, TransactionManager tm_)
		{
			return GetByRoleName(roleName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_roleEO>> GetByRoleNameAsync(string roleName, int top_, TransactionManager tm_)
		{
			return await GetByRoleNameAsync(roleName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RoleName（字段） 查询
		/// </summary>
		/// /// <param name = "roleName">角色名称</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByRoleName(string roleName, string sort_)
		{
			return GetByRoleName(roleName, 0, sort_, null);
		}
		public async Task<List<Admin_roleEO>> GetByRoleNameAsync(string roleName, string sort_)
		{
			return await GetByRoleNameAsync(roleName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RoleName（字段） 查询
		/// </summary>
		/// /// <param name = "roleName">角色名称</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByRoleName(string roleName, string sort_, TransactionManager tm_)
		{
			return GetByRoleName(roleName, 0, sort_, tm_);
		}
		public async Task<List<Admin_roleEO>> GetByRoleNameAsync(string roleName, string sort_, TransactionManager tm_)
		{
			return await GetByRoleNameAsync(roleName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RoleName（字段） 查询
		/// </summary>
		/// /// <param name = "roleName">角色名称</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByRoleName(string roleName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(roleName != null ? "`RoleName` = @RoleName" : "`RoleName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (roleName != null)
				paras_.Add(Database.CreateInParameter("@RoleName", roleName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_roleEO.MapDataReader);
		}
		public async Task<List<Admin_roleEO>> GetByRoleNameAsync(string roleName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(roleName != null ? "`RoleName` = @RoleName" : "`RoleName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (roleName != null)
				paras_.Add(Database.CreateInParameter("@RoleName", roleName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_roleEO.MapDataReader);
		}
		#endregion // GetByRoleName
		#region GetByDesc
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByDesc(string desc)
		{
			return GetByDesc(desc, 0, string.Empty, null);
		}
		public async Task<List<Admin_roleEO>> GetByDescAsync(string desc)
		{
			return await GetByDescAsync(desc, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByDesc(string desc, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_roleEO>> GetByDescAsync(string desc, TransactionManager tm_)
		{
			return await GetByDescAsync(desc, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByDesc(string desc, int top_)
		{
			return GetByDesc(desc, top_, string.Empty, null);
		}
		public async Task<List<Admin_roleEO>> GetByDescAsync(string desc, int top_)
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
		public List<Admin_roleEO> GetByDesc(string desc, int top_, TransactionManager tm_)
		{
			return GetByDesc(desc, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_roleEO>> GetByDescAsync(string desc, int top_, TransactionManager tm_)
		{
			return await GetByDescAsync(desc, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByDesc(string desc, string sort_)
		{
			return GetByDesc(desc, 0, sort_, null);
		}
		public async Task<List<Admin_roleEO>> GetByDescAsync(string desc, string sort_)
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
		public List<Admin_roleEO> GetByDesc(string desc, string sort_, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, sort_, tm_);
		}
		public async Task<List<Admin_roleEO>> GetByDescAsync(string desc, string sort_, TransactionManager tm_)
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
		public List<Admin_roleEO> GetByDesc(string desc, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_roleEO.MapDataReader);
		}
		public async Task<List<Admin_roleEO>> GetByDescAsync(string desc, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_roleEO.MapDataReader);
		}
		#endregion // GetByDesc
		#region GetByStatus
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByStatus(int status)
		{
			return GetByStatus(status, 0, string.Empty, null);
		}
		public async Task<List<Admin_roleEO>> GetByStatusAsync(int status)
		{
			return await GetByStatusAsync(status, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByStatus(int status, TransactionManager tm_)
		{
			return GetByStatus(status, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_roleEO>> GetByStatusAsync(int status, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByStatus(int status, int top_)
		{
			return GetByStatus(status, top_, string.Empty, null);
		}
		public async Task<List<Admin_roleEO>> GetByStatusAsync(int status, int top_)
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
		public List<Admin_roleEO> GetByStatus(int status, int top_, TransactionManager tm_)
		{
			return GetByStatus(status, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_roleEO>> GetByStatusAsync(int status, int top_, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_roleEO> GetByStatus(int status, string sort_)
		{
			return GetByStatus(status, 0, sort_, null);
		}
		public async Task<List<Admin_roleEO>> GetByStatusAsync(int status, string sort_)
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
		public List<Admin_roleEO> GetByStatus(int status, string sort_, TransactionManager tm_)
		{
			return GetByStatus(status, 0, sort_, tm_);
		}
		public async Task<List<Admin_roleEO>> GetByStatusAsync(int status, string sort_, TransactionManager tm_)
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
		public List<Admin_roleEO> GetByStatus(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_roleEO.MapDataReader);
		}
		public async Task<List<Admin_roleEO>> GetByStatusAsync(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_roleEO.MapDataReader);
		}
		#endregion // GetByStatus
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
