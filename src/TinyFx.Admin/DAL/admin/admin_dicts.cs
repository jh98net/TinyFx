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
	/// 后台字典
	/// 【表 admin_dicts 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_dictsEO : IRowMapper<Admin_dictsEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_dictsEO()
		{
			this.Type = 0;
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
		private string _originalDictID;
		/// <summary>
		/// 【数据库中的原始主键 DictID 值的副本，用于主键值更新】
		/// </summary>
		public string OriginalDictID
		{
			get { return _originalDictID; }
			set { HasOriginal = true; _originalDictID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "DictID", DictID }, };
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
		public string DictID { get; set; }
		/// <summary>
		/// 类别
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 2)]
		public string Category { get; set; }
		/// <summary>
		/// 描述
		/// 【字段 varchar(200)】
		/// </summary>
		[DataMember(Order = 3)]
		public string Name { get; set; }
		/// <summary>
		/// 值
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 4)]
		public string Value { get; set; }
		/// <summary>
		/// 类型0-系统1-用户定义
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 5)]
		public int Type { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_dictsEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_dictsEO MapDataReader(IDataReader reader)
		{
		    Admin_dictsEO ret = new Admin_dictsEO();
			ret.DictID = reader.ToString("DictID");
			ret.OriginalDictID = ret.DictID;
			ret.Category = reader.ToString("Category");
			ret.Name = reader.ToString("Name");
			ret.Value = reader.ToString("Value");
			ret.Type = reader.ToInt32("Type");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 后台字典
	/// 【表 admin_dicts 的操作类】
	/// </summary>
	public class Admin_dictsMO : MySqlTableMO<Admin_dictsEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_dicts`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_dictsMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_dictsMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_dictsMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_dictsEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_dictsEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_dictsEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_dicts` (`DictID`, `Category`, `Name`, `Value`, `Type`) VALUE (@DictID, @Category, @Name, @Value, @Type);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", item.DictID, MySqlDbType.VarChar),
				Database.CreateInParameter("@Category", item.Category != null ? item.Category : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Name", item.Name != null ? item.Name : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Value", item.Value != null ? item.Value : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Type", item.Type, MySqlDbType.Int32),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "dictID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(string dictID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(dictID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(string dictID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(dictID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(string dictID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_dicts` WHERE `DictID` = @DictID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_dictsEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.DictID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_dictsEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.DictID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByCategory
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "category">类别</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByCategory(string category, TransactionManager tm_ = null)
		{
			RepairRemoveByCategoryData(category, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByCategoryAsync(string category, TransactionManager tm_ = null)
		{
			RepairRemoveByCategoryData(category, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByCategoryData(string category, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_dicts` WHERE " + (category != null ? "`Category` = @Category" : "`Category` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (category != null)
				paras_.Add(Database.CreateInParameter("@Category", category, MySqlDbType.VarChar));
		}
		#endregion // RemoveByCategory
		#region RemoveByName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "name">描述</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByName(string name, TransactionManager tm_ = null)
		{
			RepairRemoveByNameData(name, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByNameAsync(string name, TransactionManager tm_ = null)
		{
			RepairRemoveByNameData(name, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByNameData(string name, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_dicts` WHERE " + (name != null ? "`Name` = @Name" : "`Name` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (name != null)
				paras_.Add(Database.CreateInParameter("@Name", name, MySqlDbType.VarChar));
		}
		#endregion // RemoveByName
		#region RemoveByValue
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "valueValue">值</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByValue(string valueValue, TransactionManager tm_ = null)
		{
			RepairRemoveByValueData(valueValue, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByValueAsync(string valueValue, TransactionManager tm_ = null)
		{
			RepairRemoveByValueData(valueValue, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByValueData(string valueValue, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_dicts` WHERE " + (valueValue != null ? "`Value` = @Value" : "`Value` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (valueValue != null)
				paras_.Add(Database.CreateInParameter("@Value", valueValue, MySqlDbType.Text));
		}
		#endregion // RemoveByValue
		#region RemoveByType
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "type">类型0-系统1-用户定义</param>
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
			sql_ = @"DELETE FROM `admin_dicts` WHERE `Type` = @Type";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Type", type, MySqlDbType.Int32));
		}
		#endregion // RemoveByType
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
		public int Put(Admin_dictsEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_dictsEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_dictsEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_dicts` SET `DictID` = @DictID, `Category` = @Category, `Name` = @Name, `Value` = @Value, `Type` = @Type WHERE `DictID` = @DictID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", item.DictID, MySqlDbType.VarChar),
				Database.CreateInParameter("@Category", item.Category != null ? item.Category : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Name", item.Name != null ? item.Name : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Value", item.Value != null ? item.Value : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Type", item.Type, MySqlDbType.Int32),
				Database.CreateInParameter("@DictID_Original", item.HasOriginal ? item.OriginalDictID : item.DictID, MySqlDbType.VarChar),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_dictsEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_dictsEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "dictID">编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string dictID, string set_, params object[] values_)
		{
			return Put(set_, "`DictID` = @DictID", ConcatValues(values_, dictID));
		}
		public async Task<int> PutByPKAsync(string dictID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`DictID` = @DictID", ConcatValues(values_, dictID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "dictID">编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string dictID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`DictID` = @DictID", tm_, ConcatValues(values_, dictID));
		}
		public async Task<int> PutByPKAsync(string dictID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`DictID` = @DictID", tm_, ConcatValues(values_, dictID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "dictID">编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string dictID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
	        };
			return Put(set_, "`DictID` = @DictID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(string dictID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
	        };
			return await PutAsync(set_, "`DictID` = @DictID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutCategory
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "dictID">编码GUID</param>
		/// /// <param name = "category">类别</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutCategoryByPK(string dictID, string category, TransactionManager tm_ = null)
		{
			RepairPutCategoryByPKData(dictID, category, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutCategoryByPKAsync(string dictID, string category, TransactionManager tm_ = null)
		{
			RepairPutCategoryByPKData(dictID, category, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutCategoryByPKData(string dictID, string category, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_dicts` SET `Category` = @Category  WHERE `DictID` = @DictID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Category", category != null ? category : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "category">类别</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutCategory(string category, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_dicts` SET `Category` = @Category";
			var parameter_ = Database.CreateInParameter("@Category", category != null ? category : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutCategoryAsync(string category, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_dicts` SET `Category` = @Category";
			var parameter_ = Database.CreateInParameter("@Category", category != null ? category : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutCategory
		#region PutName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "dictID">编码GUID</param>
		/// /// <param name = "name">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutNameByPK(string dictID, string name, TransactionManager tm_ = null)
		{
			RepairPutNameByPKData(dictID, name, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutNameByPKAsync(string dictID, string name, TransactionManager tm_ = null)
		{
			RepairPutNameByPKData(dictID, name, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutNameByPKData(string dictID, string name, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_dicts` SET `Name` = @Name  WHERE `DictID` = @DictID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Name", name != null ? name : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "name">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutName(string name, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_dicts` SET `Name` = @Name";
			var parameter_ = Database.CreateInParameter("@Name", name != null ? name : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutNameAsync(string name, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_dicts` SET `Name` = @Name";
			var parameter_ = Database.CreateInParameter("@Name", name != null ? name : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutName
		#region PutValue
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "dictID">编码GUID</param>
		/// /// <param name = "valueValue">值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutValueByPK(string dictID, string valueValue, TransactionManager tm_ = null)
		{
			RepairPutValueByPKData(dictID, valueValue, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutValueByPKAsync(string dictID, string valueValue, TransactionManager tm_ = null)
		{
			RepairPutValueByPKData(dictID, valueValue, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutValueByPKData(string dictID, string valueValue, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_dicts` SET `Value` = @Value  WHERE `DictID` = @DictID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Value", valueValue != null ? valueValue : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "valueValue">值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutValue(string valueValue, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_dicts` SET `Value` = @Value";
			var parameter_ = Database.CreateInParameter("@Value", valueValue != null ? valueValue : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutValueAsync(string valueValue, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_dicts` SET `Value` = @Value";
			var parameter_ = Database.CreateInParameter("@Value", valueValue != null ? valueValue : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutValue
		#region PutType
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "dictID">编码GUID</param>
		/// /// <param name = "type">类型0-系统1-用户定义</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTypeByPK(string dictID, int type, TransactionManager tm_ = null)
		{
			RepairPutTypeByPKData(dictID, type, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTypeByPKAsync(string dictID, int type, TransactionManager tm_ = null)
		{
			RepairPutTypeByPKData(dictID, type, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTypeByPKData(string dictID, int type, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_dicts` SET `Type` = @Type  WHERE `DictID` = @DictID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Type", type, MySqlDbType.Int32),
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "type">类型0-系统1-用户定义</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutType(int type, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_dicts` SET `Type` = @Type";
			var parameter_ = Database.CreateInParameter("@Type", type, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTypeAsync(int type, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_dicts` SET `Type` = @Type";
			var parameter_ = Database.CreateInParameter("@Type", type, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutType
		#endregion // PutXXX
		#endregion // Put
	   
		#region Set
		
		/// <summary>
		/// 插入或者更新数据
		/// </summary>
		/// <param name = "item">要更新的实体对象</param>
		/// <param name="tm">事务管理对象</param>
		/// <return>true:插入操作；false:更新操作</return>
		public bool Set(Admin_dictsEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.DictID) == null)
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
		public async Task<bool> SetAsync(Admin_dictsEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.DictID) == null)
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
		/// /// <param name = "dictID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_dictsEO GetByPK(string dictID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(dictID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_dictsEO.MapDataReader);
		}
		public async Task<Admin_dictsEO> GetByPKAsync(string dictID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(dictID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_dictsEO.MapDataReader);
		}
		private void RepairGetByPKData(string dictID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`DictID` = @DictID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 Category（字段）
		/// </summary>
		/// /// <param name = "dictID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetCategoryByPK(string dictID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Category`", "`DictID` = @DictID", paras_, tm_);
		}
		public async Task<string> GetCategoryByPKAsync(string dictID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Category`", "`DictID` = @DictID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Name（字段）
		/// </summary>
		/// /// <param name = "dictID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetNameByPK(string dictID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Name`", "`DictID` = @DictID", paras_, tm_);
		}
		public async Task<string> GetNameByPKAsync(string dictID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Name`", "`DictID` = @DictID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Value（字段）
		/// </summary>
		/// /// <param name = "dictID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetValueByPK(string dictID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Value`", "`DictID` = @DictID", paras_, tm_);
		}
		public async Task<string> GetValueByPKAsync(string dictID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Value`", "`DictID` = @DictID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Type（字段）
		/// </summary>
		/// /// <param name = "dictID">编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetTypeByPK(string dictID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`Type`", "`DictID` = @DictID", paras_, tm_);
		}
		public async Task<int> GetTypeByPKAsync(string dictID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DictID", dictID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`Type`", "`DictID` = @DictID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByCategory
		
		/// <summary>
		/// 按 Category（字段） 查询
		/// </summary>
		/// /// <param name = "category">类别</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByCategory(string category)
		{
			return GetByCategory(category, 0, string.Empty, null);
		}
		public async Task<List<Admin_dictsEO>> GetByCategoryAsync(string category)
		{
			return await GetByCategoryAsync(category, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Category（字段） 查询
		/// </summary>
		/// /// <param name = "category">类别</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByCategory(string category, TransactionManager tm_)
		{
			return GetByCategory(category, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByCategoryAsync(string category, TransactionManager tm_)
		{
			return await GetByCategoryAsync(category, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Category（字段） 查询
		/// </summary>
		/// /// <param name = "category">类别</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByCategory(string category, int top_)
		{
			return GetByCategory(category, top_, string.Empty, null);
		}
		public async Task<List<Admin_dictsEO>> GetByCategoryAsync(string category, int top_)
		{
			return await GetByCategoryAsync(category, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Category（字段） 查询
		/// </summary>
		/// /// <param name = "category">类别</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByCategory(string category, int top_, TransactionManager tm_)
		{
			return GetByCategory(category, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByCategoryAsync(string category, int top_, TransactionManager tm_)
		{
			return await GetByCategoryAsync(category, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Category（字段） 查询
		/// </summary>
		/// /// <param name = "category">类别</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByCategory(string category, string sort_)
		{
			return GetByCategory(category, 0, sort_, null);
		}
		public async Task<List<Admin_dictsEO>> GetByCategoryAsync(string category, string sort_)
		{
			return await GetByCategoryAsync(category, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Category（字段） 查询
		/// </summary>
		/// /// <param name = "category">类别</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByCategory(string category, string sort_, TransactionManager tm_)
		{
			return GetByCategory(category, 0, sort_, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByCategoryAsync(string category, string sort_, TransactionManager tm_)
		{
			return await GetByCategoryAsync(category, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Category（字段） 查询
		/// </summary>
		/// /// <param name = "category">类别</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByCategory(string category, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(category != null ? "`Category` = @Category" : "`Category` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (category != null)
				paras_.Add(Database.CreateInParameter("@Category", category, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_dictsEO.MapDataReader);
		}
		public async Task<List<Admin_dictsEO>> GetByCategoryAsync(string category, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(category != null ? "`Category` = @Category" : "`Category` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (category != null)
				paras_.Add(Database.CreateInParameter("@Category", category, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_dictsEO.MapDataReader);
		}
		#endregion // GetByCategory
		#region GetByName
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">描述</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByName(string name)
		{
			return GetByName(name, 0, string.Empty, null);
		}
		public async Task<List<Admin_dictsEO>> GetByNameAsync(string name)
		{
			return await GetByNameAsync(name, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByName(string name, TransactionManager tm_)
		{
			return GetByName(name, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByNameAsync(string name, TransactionManager tm_)
		{
			return await GetByNameAsync(name, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByName(string name, int top_)
		{
			return GetByName(name, top_, string.Empty, null);
		}
		public async Task<List<Admin_dictsEO>> GetByNameAsync(string name, int top_)
		{
			return await GetByNameAsync(name, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByName(string name, int top_, TransactionManager tm_)
		{
			return GetByName(name, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByNameAsync(string name, int top_, TransactionManager tm_)
		{
			return await GetByNameAsync(name, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByName(string name, string sort_)
		{
			return GetByName(name, 0, sort_, null);
		}
		public async Task<List<Admin_dictsEO>> GetByNameAsync(string name, string sort_)
		{
			return await GetByNameAsync(name, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByName(string name, string sort_, TransactionManager tm_)
		{
			return GetByName(name, 0, sort_, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByNameAsync(string name, string sort_, TransactionManager tm_)
		{
			return await GetByNameAsync(name, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByName(string name, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(name != null ? "`Name` = @Name" : "`Name` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (name != null)
				paras_.Add(Database.CreateInParameter("@Name", name, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_dictsEO.MapDataReader);
		}
		public async Task<List<Admin_dictsEO>> GetByNameAsync(string name, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(name != null ? "`Name` = @Name" : "`Name` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (name != null)
				paras_.Add(Database.CreateInParameter("@Name", name, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_dictsEO.MapDataReader);
		}
		#endregion // GetByName
		#region GetByValue
		
		/// <summary>
		/// 按 Value（字段） 查询
		/// </summary>
		/// /// <param name = "valueValue">值</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByValue(string valueValue)
		{
			return GetByValue(valueValue, 0, string.Empty, null);
		}
		public async Task<List<Admin_dictsEO>> GetByValueAsync(string valueValue)
		{
			return await GetByValueAsync(valueValue, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Value（字段） 查询
		/// </summary>
		/// /// <param name = "valueValue">值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByValue(string valueValue, TransactionManager tm_)
		{
			return GetByValue(valueValue, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByValueAsync(string valueValue, TransactionManager tm_)
		{
			return await GetByValueAsync(valueValue, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Value（字段） 查询
		/// </summary>
		/// /// <param name = "valueValue">值</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByValue(string valueValue, int top_)
		{
			return GetByValue(valueValue, top_, string.Empty, null);
		}
		public async Task<List<Admin_dictsEO>> GetByValueAsync(string valueValue, int top_)
		{
			return await GetByValueAsync(valueValue, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Value（字段） 查询
		/// </summary>
		/// /// <param name = "valueValue">值</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByValue(string valueValue, int top_, TransactionManager tm_)
		{
			return GetByValue(valueValue, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByValueAsync(string valueValue, int top_, TransactionManager tm_)
		{
			return await GetByValueAsync(valueValue, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Value（字段） 查询
		/// </summary>
		/// /// <param name = "valueValue">值</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByValue(string valueValue, string sort_)
		{
			return GetByValue(valueValue, 0, sort_, null);
		}
		public async Task<List<Admin_dictsEO>> GetByValueAsync(string valueValue, string sort_)
		{
			return await GetByValueAsync(valueValue, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Value（字段） 查询
		/// </summary>
		/// /// <param name = "valueValue">值</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByValue(string valueValue, string sort_, TransactionManager tm_)
		{
			return GetByValue(valueValue, 0, sort_, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByValueAsync(string valueValue, string sort_, TransactionManager tm_)
		{
			return await GetByValueAsync(valueValue, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Value（字段） 查询
		/// </summary>
		/// /// <param name = "valueValue">值</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByValue(string valueValue, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(valueValue != null ? "`Value` = @Value" : "`Value` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (valueValue != null)
				paras_.Add(Database.CreateInParameter("@Value", valueValue, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_dictsEO.MapDataReader);
		}
		public async Task<List<Admin_dictsEO>> GetByValueAsync(string valueValue, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(valueValue != null ? "`Value` = @Value" : "`Value` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (valueValue != null)
				paras_.Add(Database.CreateInParameter("@Value", valueValue, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_dictsEO.MapDataReader);
		}
		#endregion // GetByValue
		#region GetByType
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-系统1-用户定义</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByType(int type)
		{
			return GetByType(type, 0, string.Empty, null);
		}
		public async Task<List<Admin_dictsEO>> GetByTypeAsync(int type)
		{
			return await GetByTypeAsync(type, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-系统1-用户定义</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByType(int type, TransactionManager tm_)
		{
			return GetByType(type, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByTypeAsync(int type, TransactionManager tm_)
		{
			return await GetByTypeAsync(type, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-系统1-用户定义</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByType(int type, int top_)
		{
			return GetByType(type, top_, string.Empty, null);
		}
		public async Task<List<Admin_dictsEO>> GetByTypeAsync(int type, int top_)
		{
			return await GetByTypeAsync(type, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-系统1-用户定义</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByType(int type, int top_, TransactionManager tm_)
		{
			return GetByType(type, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByTypeAsync(int type, int top_, TransactionManager tm_)
		{
			return await GetByTypeAsync(type, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-系统1-用户定义</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByType(int type, string sort_)
		{
			return GetByType(type, 0, sort_, null);
		}
		public async Task<List<Admin_dictsEO>> GetByTypeAsync(int type, string sort_)
		{
			return await GetByTypeAsync(type, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-系统1-用户定义</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByType(int type, string sort_, TransactionManager tm_)
		{
			return GetByType(type, 0, sort_, tm_);
		}
		public async Task<List<Admin_dictsEO>> GetByTypeAsync(int type, string sort_, TransactionManager tm_)
		{
			return await GetByTypeAsync(type, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Type（字段） 查询
		/// </summary>
		/// /// <param name = "type">类型0-系统1-用户定义</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_dictsEO> GetByType(int type, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Type` = @Type", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Type", type, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_dictsEO.MapDataReader);
		}
		public async Task<List<Admin_dictsEO>> GetByTypeAsync(int type, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Type` = @Type", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Type", type, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_dictsEO.MapDataReader);
		}
		#endregion // GetByType
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
