/******************************************************
 * 此代码由代码生成器工具自动生成，请不要修改
 * TinyFx代码生成器核心库版本号：1.0.0.0
 * git: https://github.com/jh98net/TinyFx
 * 文档生成时间：2022-11-13 22: 11:28
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
	/// 部门或组
	/// 【表 admin_group 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_groupEO : IRowMapper<Admin_groupEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_groupEO()
		{
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
		private int _originalGroupID;
		/// <summary>
		/// 【数据库中的原始主键 GroupID 值的副本，用于主键值更新】
		/// </summary>
		public int OriginalGroupID
		{
			get { return _originalGroupID; }
			set { HasOriginal = true; _originalGroupID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "GroupID", GroupID }, };
	    }
	    /// <summary>
	    /// 获取主键集合JSON格式
	    /// </summary>
	    /// <returns></returns>
	    public string GetPrimaryKeysJson() => SerializerUtil.SerializeJson(GetPrimaryKeys());
		#endregion // 主键原始值
		#region 所有字段
		/// <summary>
		/// 编码
		/// 【主键 int】
		/// </summary>
		[DataMember(Order = 1)]
		public int GroupID { get; set; }
		/// <summary>
		/// 名称
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 2)]
		public string GroupName { get; set; }
		/// <summary>
		/// 描述
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 3)]
		public string Desc { get; set; }
		/// <summary>
		/// 父编码
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 4)]
		public int? ParentID { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_groupEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_groupEO MapDataReader(IDataReader reader)
		{
		    Admin_groupEO ret = new Admin_groupEO();
			ret.GroupID = reader.ToInt32("GroupID");
			ret.OriginalGroupID = ret.GroupID;
			ret.GroupName = reader.ToString("GroupName");
			ret.Desc = reader.ToString("Desc");
			ret.ParentID = reader.ToInt32N("ParentID");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 部门或组
	/// 【表 admin_group 的操作类】
	/// </summary>
	public class Admin_groupMO : MySqlTableMO<Admin_groupEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_group`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_groupMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_groupMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_groupMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_groupEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_groupEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_groupEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_group` (`GroupID`, `GroupName`, `Desc`, `ParentID`) VALUE (@GroupID, @GroupName, @Desc, @ParentID);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupID", item.GroupID, MySqlDbType.Int32),
				Database.CreateInParameter("@GroupName", item.GroupName != null ? item.GroupName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Desc", item.Desc != null ? item.Desc : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@ParentID", item.ParentID.HasValue ? item.ParentID.Value : (object)DBNull.Value, MySqlDbType.Int32),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "groupID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(int groupID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(groupID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(int groupID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(groupID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(int groupID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_group` WHERE `GroupID` = @GroupID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_groupEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.GroupID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_groupEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.GroupID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByGroupName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "groupName">名称</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByGroupName(string groupName, TransactionManager tm_ = null)
		{
			RepairRemoveByGroupNameData(groupName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByGroupNameAsync(string groupName, TransactionManager tm_ = null)
		{
			RepairRemoveByGroupNameData(groupName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByGroupNameData(string groupName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_group` WHERE " + (groupName != null ? "`GroupName` = @GroupName" : "`GroupName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (groupName != null)
				paras_.Add(Database.CreateInParameter("@GroupName", groupName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByGroupName
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
			sql_ = @"DELETE FROM `admin_group` WHERE " + (desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.Text));
		}
		#endregion // RemoveByDesc
		#region RemoveByParentID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "parentID">父编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByParentID(int? parentID, TransactionManager tm_ = null)
		{
			RepairRemoveByParentIDData(parentID.Value, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByParentIDAsync(int? parentID, TransactionManager tm_ = null)
		{
			RepairRemoveByParentIDData(parentID.Value, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByParentIDData(int? parentID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_group` WHERE " + (parentID.HasValue ? "`ParentID` = @ParentID" : "`ParentID` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (parentID.HasValue)
				paras_.Add(Database.CreateInParameter("@ParentID", parentID.Value, MySqlDbType.Int32));
		}
		#endregion // RemoveByParentID
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
		public int Put(Admin_groupEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_groupEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_groupEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_group` SET `GroupID` = @GroupID, `GroupName` = @GroupName, `Desc` = @Desc, `ParentID` = @ParentID WHERE `GroupID` = @GroupID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupID", item.GroupID, MySqlDbType.Int32),
				Database.CreateInParameter("@GroupName", item.GroupName != null ? item.GroupName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Desc", item.Desc != null ? item.Desc : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@ParentID", item.ParentID.HasValue ? item.ParentID.Value : (object)DBNull.Value, MySqlDbType.Int32),
				Database.CreateInParameter("@GroupID_Original", item.HasOriginal ? item.OriginalGroupID : item.GroupID, MySqlDbType.Int32),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_groupEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_groupEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "groupID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(int groupID, string set_, params object[] values_)
		{
			return Put(set_, "`GroupID` = @GroupID", ConcatValues(values_, groupID));
		}
		public async Task<int> PutByPKAsync(int groupID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`GroupID` = @GroupID", ConcatValues(values_, groupID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "groupID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(int groupID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`GroupID` = @GroupID", tm_, ConcatValues(values_, groupID));
		}
		public async Task<int> PutByPKAsync(int groupID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`GroupID` = @GroupID", tm_, ConcatValues(values_, groupID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "groupID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(int groupID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
	        };
			return Put(set_, "`GroupID` = @GroupID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(int groupID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
	        };
			return await PutAsync(set_, "`GroupID` = @GroupID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutGroupName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "groupID">编码</param>
		/// /// <param name = "groupName">名称</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGroupNameByPK(int groupID, string groupName, TransactionManager tm_ = null)
		{
			RepairPutGroupNameByPKData(groupID, groupName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutGroupNameByPKAsync(int groupID, string groupName, TransactionManager tm_ = null)
		{
			RepairPutGroupNameByPKData(groupID, groupName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutGroupNameByPKData(int groupID, string groupName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_group` SET `GroupName` = @GroupName  WHERE `GroupID` = @GroupID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupName", groupName != null ? groupName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "groupName">名称</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGroupName(string groupName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_group` SET `GroupName` = @GroupName";
			var parameter_ = Database.CreateInParameter("@GroupName", groupName != null ? groupName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutGroupNameAsync(string groupName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_group` SET `GroupName` = @GroupName";
			var parameter_ = Database.CreateInParameter("@GroupName", groupName != null ? groupName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutGroupName
		#region PutDesc
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "groupID">编码</param>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDescByPK(int groupID, string desc, TransactionManager tm_ = null)
		{
			RepairPutDescByPKData(groupID, desc, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDescByPKAsync(int groupID, string desc, TransactionManager tm_ = null)
		{
			RepairPutDescByPKData(groupID, desc, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDescByPKData(int groupID, string desc, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_group` SET `Desc` = @Desc  WHERE `GroupID` = @GroupID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
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
			const string sql_ = @"UPDATE `admin_group` SET `Desc` = @Desc";
			var parameter_ = Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDescAsync(string desc, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_group` SET `Desc` = @Desc";
			var parameter_ = Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDesc
		#region PutParentID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "groupID">编码</param>
		/// /// <param name = "parentID">父编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutParentIDByPK(int groupID, int? parentID, TransactionManager tm_ = null)
		{
			RepairPutParentIDByPKData(groupID, parentID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutParentIDByPKAsync(int groupID, int? parentID, TransactionManager tm_ = null)
		{
			RepairPutParentIDByPKData(groupID, parentID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutParentIDByPKData(int groupID, int? parentID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_group` SET `ParentID` = @ParentID  WHERE `GroupID` = @GroupID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ParentID", parentID.HasValue ? parentID.Value : (object)DBNull.Value, MySqlDbType.Int32),
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "parentID">父编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutParentID(int? parentID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_group` SET `ParentID` = @ParentID";
			var parameter_ = Database.CreateInParameter("@ParentID", parentID.HasValue ? parentID.Value : (object)DBNull.Value, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutParentIDAsync(int? parentID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_group` SET `ParentID` = @ParentID";
			var parameter_ = Database.CreateInParameter("@ParentID", parentID.HasValue ? parentID.Value : (object)DBNull.Value, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutParentID
		#endregion // PutXXX
		#endregion // Put
	   
		#region Set
		
		/// <summary>
		/// 插入或者更新数据
		/// </summary>
		/// <param name = "item">要更新的实体对象</param>
		/// <param name="tm">事务管理对象</param>
		/// <return>true:插入操作；false:更新操作</return>
		public bool Set(Admin_groupEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.GroupID) == null)
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
		public async Task<bool> SetAsync(Admin_groupEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.GroupID) == null)
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
		/// /// <param name = "groupID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_groupEO GetByPK(int groupID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(groupID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_groupEO.MapDataReader);
		}
		public async Task<Admin_groupEO> GetByPKAsync(int groupID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(groupID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_groupEO.MapDataReader);
		}
		private void RepairGetByPKData(int groupID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`GroupID` = @GroupID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 GroupName（字段）
		/// </summary>
		/// /// <param name = "groupID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetGroupNameByPK(int groupID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
			};
			return (string)GetScalar("`GroupName`", "`GroupID` = @GroupID", paras_, tm_);
		}
		public async Task<string> GetGroupNameByPKAsync(int groupID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
			};
			return (string)await GetScalarAsync("`GroupName`", "`GroupID` = @GroupID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Desc（字段）
		/// </summary>
		/// /// <param name = "groupID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetDescByPK(int groupID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
			};
			return (string)GetScalar("`Desc`", "`GroupID` = @GroupID", paras_, tm_);
		}
		public async Task<string> GetDescByPKAsync(int groupID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
			};
			return (string)await GetScalarAsync("`Desc`", "`GroupID` = @GroupID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ParentID（字段）
		/// </summary>
		/// /// <param name = "groupID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int? GetParentIDByPK(int groupID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
			};
			return (int?)GetScalar("`ParentID`", "`GroupID` = @GroupID", paras_, tm_);
		}
		public async Task<int?> GetParentIDByPKAsync(int groupID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GroupID", groupID, MySqlDbType.Int32),
			};
			return (int?)await GetScalarAsync("`ParentID`", "`GroupID` = @GroupID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByGroupName
		
		/// <summary>
		/// 按 GroupName（字段） 查询
		/// </summary>
		/// /// <param name = "groupName">名称</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByGroupName(string groupName)
		{
			return GetByGroupName(groupName, 0, string.Empty, null);
		}
		public async Task<List<Admin_groupEO>> GetByGroupNameAsync(string groupName)
		{
			return await GetByGroupNameAsync(groupName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GroupName（字段） 查询
		/// </summary>
		/// /// <param name = "groupName">名称</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByGroupName(string groupName, TransactionManager tm_)
		{
			return GetByGroupName(groupName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_groupEO>> GetByGroupNameAsync(string groupName, TransactionManager tm_)
		{
			return await GetByGroupNameAsync(groupName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GroupName（字段） 查询
		/// </summary>
		/// /// <param name = "groupName">名称</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByGroupName(string groupName, int top_)
		{
			return GetByGroupName(groupName, top_, string.Empty, null);
		}
		public async Task<List<Admin_groupEO>> GetByGroupNameAsync(string groupName, int top_)
		{
			return await GetByGroupNameAsync(groupName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GroupName（字段） 查询
		/// </summary>
		/// /// <param name = "groupName">名称</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByGroupName(string groupName, int top_, TransactionManager tm_)
		{
			return GetByGroupName(groupName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_groupEO>> GetByGroupNameAsync(string groupName, int top_, TransactionManager tm_)
		{
			return await GetByGroupNameAsync(groupName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GroupName（字段） 查询
		/// </summary>
		/// /// <param name = "groupName">名称</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByGroupName(string groupName, string sort_)
		{
			return GetByGroupName(groupName, 0, sort_, null);
		}
		public async Task<List<Admin_groupEO>> GetByGroupNameAsync(string groupName, string sort_)
		{
			return await GetByGroupNameAsync(groupName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 GroupName（字段） 查询
		/// </summary>
		/// /// <param name = "groupName">名称</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByGroupName(string groupName, string sort_, TransactionManager tm_)
		{
			return GetByGroupName(groupName, 0, sort_, tm_);
		}
		public async Task<List<Admin_groupEO>> GetByGroupNameAsync(string groupName, string sort_, TransactionManager tm_)
		{
			return await GetByGroupNameAsync(groupName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 GroupName（字段） 查询
		/// </summary>
		/// /// <param name = "groupName">名称</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByGroupName(string groupName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(groupName != null ? "`GroupName` = @GroupName" : "`GroupName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (groupName != null)
				paras_.Add(Database.CreateInParameter("@GroupName", groupName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_groupEO.MapDataReader);
		}
		public async Task<List<Admin_groupEO>> GetByGroupNameAsync(string groupName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(groupName != null ? "`GroupName` = @GroupName" : "`GroupName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (groupName != null)
				paras_.Add(Database.CreateInParameter("@GroupName", groupName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_groupEO.MapDataReader);
		}
		#endregion // GetByGroupName
		#region GetByDesc
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByDesc(string desc)
		{
			return GetByDesc(desc, 0, string.Empty, null);
		}
		public async Task<List<Admin_groupEO>> GetByDescAsync(string desc)
		{
			return await GetByDescAsync(desc, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByDesc(string desc, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_groupEO>> GetByDescAsync(string desc, TransactionManager tm_)
		{
			return await GetByDescAsync(desc, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByDesc(string desc, int top_)
		{
			return GetByDesc(desc, top_, string.Empty, null);
		}
		public async Task<List<Admin_groupEO>> GetByDescAsync(string desc, int top_)
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
		public List<Admin_groupEO> GetByDesc(string desc, int top_, TransactionManager tm_)
		{
			return GetByDesc(desc, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_groupEO>> GetByDescAsync(string desc, int top_, TransactionManager tm_)
		{
			return await GetByDescAsync(desc, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByDesc(string desc, string sort_)
		{
			return GetByDesc(desc, 0, sort_, null);
		}
		public async Task<List<Admin_groupEO>> GetByDescAsync(string desc, string sort_)
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
		public List<Admin_groupEO> GetByDesc(string desc, string sort_, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, sort_, tm_);
		}
		public async Task<List<Admin_groupEO>> GetByDescAsync(string desc, string sort_, TransactionManager tm_)
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
		public List<Admin_groupEO> GetByDesc(string desc, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_groupEO.MapDataReader);
		}
		public async Task<List<Admin_groupEO>> GetByDescAsync(string desc, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_groupEO.MapDataReader);
		}
		#endregion // GetByDesc
		#region GetByParentID
		
		/// <summary>
		/// 按 ParentID（字段） 查询
		/// </summary>
		/// /// <param name = "parentID">父编码</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByParentID(int? parentID)
		{
			return GetByParentID(parentID, 0, string.Empty, null);
		}
		public async Task<List<Admin_groupEO>> GetByParentIDAsync(int? parentID)
		{
			return await GetByParentIDAsync(parentID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ParentID（字段） 查询
		/// </summary>
		/// /// <param name = "parentID">父编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByParentID(int? parentID, TransactionManager tm_)
		{
			return GetByParentID(parentID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_groupEO>> GetByParentIDAsync(int? parentID, TransactionManager tm_)
		{
			return await GetByParentIDAsync(parentID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ParentID（字段） 查询
		/// </summary>
		/// /// <param name = "parentID">父编码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByParentID(int? parentID, int top_)
		{
			return GetByParentID(parentID, top_, string.Empty, null);
		}
		public async Task<List<Admin_groupEO>> GetByParentIDAsync(int? parentID, int top_)
		{
			return await GetByParentIDAsync(parentID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ParentID（字段） 查询
		/// </summary>
		/// /// <param name = "parentID">父编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByParentID(int? parentID, int top_, TransactionManager tm_)
		{
			return GetByParentID(parentID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_groupEO>> GetByParentIDAsync(int? parentID, int top_, TransactionManager tm_)
		{
			return await GetByParentIDAsync(parentID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ParentID（字段） 查询
		/// </summary>
		/// /// <param name = "parentID">父编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByParentID(int? parentID, string sort_)
		{
			return GetByParentID(parentID, 0, sort_, null);
		}
		public async Task<List<Admin_groupEO>> GetByParentIDAsync(int? parentID, string sort_)
		{
			return await GetByParentIDAsync(parentID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ParentID（字段） 查询
		/// </summary>
		/// /// <param name = "parentID">父编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByParentID(int? parentID, string sort_, TransactionManager tm_)
		{
			return GetByParentID(parentID, 0, sort_, tm_);
		}
		public async Task<List<Admin_groupEO>> GetByParentIDAsync(int? parentID, string sort_, TransactionManager tm_)
		{
			return await GetByParentIDAsync(parentID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ParentID（字段） 查询
		/// </summary>
		/// /// <param name = "parentID">父编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_groupEO> GetByParentID(int? parentID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(parentID.HasValue ? "`ParentID` = @ParentID" : "`ParentID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (parentID.HasValue)
				paras_.Add(Database.CreateInParameter("@ParentID", parentID.Value, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_groupEO.MapDataReader);
		}
		public async Task<List<Admin_groupEO>> GetByParentIDAsync(int? parentID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(parentID.HasValue ? "`ParentID` = @ParentID" : "`ParentID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (parentID.HasValue)
				paras_.Add(Database.CreateInParameter("@ParentID", parentID.Value, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_groupEO.MapDataReader);
		}
		#endregion // GetByParentID
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
