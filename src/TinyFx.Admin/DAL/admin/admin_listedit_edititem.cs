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
	/// 添加编辑控件定义
	/// 【表 admin_listedit_edititem 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_listedit_edititemEO : IRowMapper<Admin_listedit_edititemEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_listedit_edititemEO()
		{
			this.IsPrimaryKey = false;
			this.RowIndex = 0;
			this.ColumnIndex = 0;
			this.WidthNum = 0;
			this.RecDate = DateTime.Now;
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
		private long _originalEditItemID;
		/// <summary>
		/// 【数据库中的原始主键 EditItemID 值的副本，用于主键值更新】
		/// </summary>
		public long OriginalEditItemID
		{
			get { return _originalEditItemID; }
			set { HasOriginal = true; _originalEditItemID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "EditItemID", EditItemID }, };
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
		/// 【主键 bigint】
		/// </summary>
		[DataMember(Order = 1)]
		public long EditItemID { get; set; }
		/// <summary>
		/// Gen编码
		/// 【字段 bigint】
		/// </summary>
		[DataMember(Order = 2)]
		public long GenID { get; set; }
		/// <summary>
		/// 列名
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 3)]
		public string ColumnName { get; set; }
		/// <summary>
		/// 是否主键
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 4)]
		public bool IsPrimaryKey { get; set; }
		/// <summary>
		/// 控件类型
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 5)]
		public string ControlType { get; set; }
		/// <summary>
		/// 控件ID
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 6)]
		public string ControlID { get; set; }
		/// <summary>
		/// Label
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 7)]
		public string FieldLabel { get; set; }
		/// <summary>
		/// 所在行
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 8)]
		public int RowIndex { get; set; }
		/// <summary>
		/// 所在列
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 9)]
		public int ColumnIndex { get; set; }
		/// <summary>
		/// 宽度
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 10)]
		public int WidthNum { get; set; }
		/// <summary>
		/// 参数名
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 11)]
		public string EditParameterName { get; set; }
		/// <summary>
		/// 参数类型
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 12)]
		public string EditDbType { get; set; }
		/// <summary>
		/// 参数DotNet类型
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 13)]
		public string EditDotNetType { get; set; }
		/// <summary>
		/// 默认值类型
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 14)]
		public string DefaultValueSet { get; set; }
		/// <summary>
		/// 默认值字符串
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 15)]
		public string DefaultValueString { get; set; }
		/// <summary>
		/// Json系列化
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 16)]
		public string JsonData { get; set; }
		/// <summary>
		/// 记录日期
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 17)]
		public DateTime RecDate { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_listedit_edititemEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_listedit_edititemEO MapDataReader(IDataReader reader)
		{
		    Admin_listedit_edititemEO ret = new Admin_listedit_edititemEO();
			ret.EditItemID = reader.ToInt64("EditItemID");
			ret.OriginalEditItemID = ret.EditItemID;
			ret.GenID = reader.ToInt64("GenID");
			ret.ColumnName = reader.ToString("ColumnName");
			ret.IsPrimaryKey = reader.ToBoolean("IsPrimaryKey");
			ret.ControlType = reader.ToString("ControlType");
			ret.ControlID = reader.ToString("ControlID");
			ret.FieldLabel = reader.ToString("FieldLabel");
			ret.RowIndex = reader.ToInt32("RowIndex");
			ret.ColumnIndex = reader.ToInt32("ColumnIndex");
			ret.WidthNum = reader.ToInt32("WidthNum");
			ret.EditParameterName = reader.ToString("EditParameterName");
			ret.EditDbType = reader.ToString("EditDbType");
			ret.EditDotNetType = reader.ToString("EditDotNetType");
			ret.DefaultValueSet = reader.ToString("DefaultValueSet");
			ret.DefaultValueString = reader.ToString("DefaultValueString");
			ret.JsonData = reader.ToString("JsonData");
			ret.RecDate = reader.ToDateTime("RecDate");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 添加编辑控件定义
	/// 【表 admin_listedit_edititem 的操作类】
	/// </summary>
	public class Admin_listedit_edititemMO : MySqlTableMO<Admin_listedit_edititemEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_listedit_edititem`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_listedit_edititemMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_listedit_edititemMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_listedit_edititemMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_listedit_edititemEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			item.EditItemID = Database.ExecSqlScalar<long>(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_listedit_edititemEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			item.EditItemID = await Database.ExecSqlScalarAsync<long>(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_listedit_edititemEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_listedit_edititem` (`GenID`, `ColumnName`, `IsPrimaryKey`, `ControlType`, `ControlID`, `FieldLabel`, `RowIndex`, `ColumnIndex`, `WidthNum`, `EditParameterName`, `EditDbType`, `EditDotNetType`, `DefaultValueSet`, `DefaultValueString`, `JsonData`, `RecDate`) VALUE (@GenID, @ColumnName, @IsPrimaryKey, @ControlType, @ControlID, @FieldLabel, @RowIndex, @ColumnIndex, @WidthNum, @EditParameterName, @EditDbType, @EditDotNetType, @DefaultValueSet, @DefaultValueString, @JsonData, @RecDate);SELECT LAST_INSERT_ID();";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", item.GenID, MySqlDbType.Int64),
				Database.CreateInParameter("@ColumnName", item.ColumnName != null ? item.ColumnName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@IsPrimaryKey", item.IsPrimaryKey, MySqlDbType.Byte),
				Database.CreateInParameter("@ControlType", item.ControlType != null ? item.ControlType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@ControlID", item.ControlID != null ? item.ControlID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@FieldLabel", item.FieldLabel != null ? item.FieldLabel : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RowIndex", item.RowIndex, MySqlDbType.Int32),
				Database.CreateInParameter("@ColumnIndex", item.ColumnIndex, MySqlDbType.Int32),
				Database.CreateInParameter("@WidthNum", item.WidthNum, MySqlDbType.Int32),
				Database.CreateInParameter("@EditParameterName", item.EditParameterName != null ? item.EditParameterName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditDbType", item.EditDbType != null ? item.EditDbType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditDotNetType", item.EditDotNetType != null ? item.EditDotNetType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DefaultValueSet", item.DefaultValueSet != null ? item.DefaultValueSet : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DefaultValueString", item.DefaultValueString != null ? item.DefaultValueString : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@JsonData", item.JsonData != null ? item.JsonData : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@RecDate", item.RecDate, MySqlDbType.DateTime),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(long editItemID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(editItemID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(editItemID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(long editItemID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_listedit_edititemEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.EditItemID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_listedit_edititemEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.EditItemID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByGenID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByGenID(long genID, TransactionManager tm_ = null)
		{
			RepairRemoveByGenIDData(genID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByGenIDAsync(long genID, TransactionManager tm_ = null)
		{
			RepairRemoveByGenIDData(genID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByGenIDData(long genID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64));
		}
		#endregion // RemoveByGenID
		#region RemoveByColumnName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "columnName">列名</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByColumnName(string columnName, TransactionManager tm_ = null)
		{
			RepairRemoveByColumnNameData(columnName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByColumnNameAsync(string columnName, TransactionManager tm_ = null)
		{
			RepairRemoveByColumnNameData(columnName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByColumnNameData(string columnName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE " + (columnName != null ? "`ColumnName` = @ColumnName" : "`ColumnName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (columnName != null)
				paras_.Add(Database.CreateInParameter("@ColumnName", columnName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByColumnName
		#region RemoveByIsPrimaryKey
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByIsPrimaryKey(bool isPrimaryKey, TransactionManager tm_ = null)
		{
			RepairRemoveByIsPrimaryKeyData(isPrimaryKey, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByIsPrimaryKeyAsync(bool isPrimaryKey, TransactionManager tm_ = null)
		{
			RepairRemoveByIsPrimaryKeyData(isPrimaryKey, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByIsPrimaryKeyData(bool isPrimaryKey, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE `IsPrimaryKey` = @IsPrimaryKey";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte));
		}
		#endregion // RemoveByIsPrimaryKey
		#region RemoveByControlType
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByControlType(string controlType, TransactionManager tm_ = null)
		{
			RepairRemoveByControlTypeData(controlType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByControlTypeAsync(string controlType, TransactionManager tm_ = null)
		{
			RepairRemoveByControlTypeData(controlType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByControlTypeData(string controlType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE " + (controlType != null ? "`ControlType` = @ControlType" : "`ControlType` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (controlType != null)
				paras_.Add(Database.CreateInParameter("@ControlType", controlType, MySqlDbType.VarChar));
		}
		#endregion // RemoveByControlType
		#region RemoveByControlID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByControlID(string controlID, TransactionManager tm_ = null)
		{
			RepairRemoveByControlIDData(controlID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByControlIDAsync(string controlID, TransactionManager tm_ = null)
		{
			RepairRemoveByControlIDData(controlID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByControlIDData(string controlID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE " + (controlID != null ? "`ControlID` = @ControlID" : "`ControlID` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (controlID != null)
				paras_.Add(Database.CreateInParameter("@ControlID", controlID, MySqlDbType.VarChar));
		}
		#endregion // RemoveByControlID
		#region RemoveByFieldLabel
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByFieldLabel(string fieldLabel, TransactionManager tm_ = null)
		{
			RepairRemoveByFieldLabelData(fieldLabel, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByFieldLabelAsync(string fieldLabel, TransactionManager tm_ = null)
		{
			RepairRemoveByFieldLabelData(fieldLabel, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByFieldLabelData(string fieldLabel, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE " + (fieldLabel != null ? "`FieldLabel` = @FieldLabel" : "`FieldLabel` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (fieldLabel != null)
				paras_.Add(Database.CreateInParameter("@FieldLabel", fieldLabel, MySqlDbType.VarChar));
		}
		#endregion // RemoveByFieldLabel
		#region RemoveByRowIndex
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByRowIndex(int rowIndex, TransactionManager tm_ = null)
		{
			RepairRemoveByRowIndexData(rowIndex, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByRowIndexAsync(int rowIndex, TransactionManager tm_ = null)
		{
			RepairRemoveByRowIndexData(rowIndex, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByRowIndexData(int rowIndex, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE `RowIndex` = @RowIndex";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RowIndex", rowIndex, MySqlDbType.Int32));
		}
		#endregion // RemoveByRowIndex
		#region RemoveByColumnIndex
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByColumnIndex(int columnIndex, TransactionManager tm_ = null)
		{
			RepairRemoveByColumnIndexData(columnIndex, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByColumnIndexAsync(int columnIndex, TransactionManager tm_ = null)
		{
			RepairRemoveByColumnIndexData(columnIndex, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByColumnIndexData(int columnIndex, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE `ColumnIndex` = @ColumnIndex";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32));
		}
		#endregion // RemoveByColumnIndex
		#region RemoveByWidthNum
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "widthNum">宽度</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByWidthNum(int widthNum, TransactionManager tm_ = null)
		{
			RepairRemoveByWidthNumData(widthNum, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByWidthNumAsync(int widthNum, TransactionManager tm_ = null)
		{
			RepairRemoveByWidthNumData(widthNum, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByWidthNumData(int widthNum, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE `WidthNum` = @WidthNum";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@WidthNum", widthNum, MySqlDbType.Int32));
		}
		#endregion // RemoveByWidthNum
		#region RemoveByEditParameterName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "editParameterName">参数名</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByEditParameterName(string editParameterName, TransactionManager tm_ = null)
		{
			RepairRemoveByEditParameterNameData(editParameterName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByEditParameterNameAsync(string editParameterName, TransactionManager tm_ = null)
		{
			RepairRemoveByEditParameterNameData(editParameterName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByEditParameterNameData(string editParameterName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE " + (editParameterName != null ? "`EditParameterName` = @EditParameterName" : "`EditParameterName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (editParameterName != null)
				paras_.Add(Database.CreateInParameter("@EditParameterName", editParameterName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByEditParameterName
		#region RemoveByEditDbType
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "editDbType">参数类型</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByEditDbType(string editDbType, TransactionManager tm_ = null)
		{
			RepairRemoveByEditDbTypeData(editDbType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByEditDbTypeAsync(string editDbType, TransactionManager tm_ = null)
		{
			RepairRemoveByEditDbTypeData(editDbType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByEditDbTypeData(string editDbType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE " + (editDbType != null ? "`EditDbType` = @EditDbType" : "`EditDbType` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (editDbType != null)
				paras_.Add(Database.CreateInParameter("@EditDbType", editDbType, MySqlDbType.VarChar));
		}
		#endregion // RemoveByEditDbType
		#region RemoveByEditDotNetType
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "editDotNetType">参数DotNet类型</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByEditDotNetType(string editDotNetType, TransactionManager tm_ = null)
		{
			RepairRemoveByEditDotNetTypeData(editDotNetType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByEditDotNetTypeAsync(string editDotNetType, TransactionManager tm_ = null)
		{
			RepairRemoveByEditDotNetTypeData(editDotNetType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByEditDotNetTypeData(string editDotNetType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE " + (editDotNetType != null ? "`EditDotNetType` = @EditDotNetType" : "`EditDotNetType` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (editDotNetType != null)
				paras_.Add(Database.CreateInParameter("@EditDotNetType", editDotNetType, MySqlDbType.VarChar));
		}
		#endregion // RemoveByEditDotNetType
		#region RemoveByDefaultValueSet
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "defaultValueSet">默认值类型</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByDefaultValueSet(string defaultValueSet, TransactionManager tm_ = null)
		{
			RepairRemoveByDefaultValueSetData(defaultValueSet, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByDefaultValueSetAsync(string defaultValueSet, TransactionManager tm_ = null)
		{
			RepairRemoveByDefaultValueSetData(defaultValueSet, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByDefaultValueSetData(string defaultValueSet, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE " + (defaultValueSet != null ? "`DefaultValueSet` = @DefaultValueSet" : "`DefaultValueSet` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (defaultValueSet != null)
				paras_.Add(Database.CreateInParameter("@DefaultValueSet", defaultValueSet, MySqlDbType.VarChar));
		}
		#endregion // RemoveByDefaultValueSet
		#region RemoveByDefaultValueString
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "defaultValueString">默认值字符串</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByDefaultValueString(string defaultValueString, TransactionManager tm_ = null)
		{
			RepairRemoveByDefaultValueStringData(defaultValueString, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByDefaultValueStringAsync(string defaultValueString, TransactionManager tm_ = null)
		{
			RepairRemoveByDefaultValueStringData(defaultValueString, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByDefaultValueStringData(string defaultValueString, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE " + (defaultValueString != null ? "`DefaultValueString` = @DefaultValueString" : "`DefaultValueString` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (defaultValueString != null)
				paras_.Add(Database.CreateInParameter("@DefaultValueString", defaultValueString, MySqlDbType.VarChar));
		}
		#endregion // RemoveByDefaultValueString
		#region RemoveByJsonData
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByJsonData(string jsonData, TransactionManager tm_ = null)
		{
			RepairRemoveByJsonDataData(jsonData, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByJsonDataAsync(string jsonData, TransactionManager tm_ = null)
		{
			RepairRemoveByJsonDataData(jsonData, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByJsonDataData(string jsonData, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE " + (jsonData != null ? "`JsonData` = @JsonData" : "`JsonData` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (jsonData != null)
				paras_.Add(Database.CreateInParameter("@JsonData", jsonData, MySqlDbType.Text));
		}
		#endregion // RemoveByJsonData
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
			sql_ = @"DELETE FROM `admin_listedit_edititem` WHERE `RecDate` = @RecDate";
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
		public int Put(Admin_listedit_edititemEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_listedit_edititemEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_listedit_edititemEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `GenID` = @GenID, `ColumnName` = @ColumnName, `IsPrimaryKey` = @IsPrimaryKey, `ControlType` = @ControlType, `ControlID` = @ControlID, `FieldLabel` = @FieldLabel, `RowIndex` = @RowIndex, `ColumnIndex` = @ColumnIndex, `WidthNum` = @WidthNum, `EditParameterName` = @EditParameterName, `EditDbType` = @EditDbType, `EditDotNetType` = @EditDotNetType, `DefaultValueSet` = @DefaultValueSet, `DefaultValueString` = @DefaultValueString, `JsonData` = @JsonData WHERE `EditItemID` = @EditItemID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", item.GenID, MySqlDbType.Int64),
				Database.CreateInParameter("@ColumnName", item.ColumnName != null ? item.ColumnName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@IsPrimaryKey", item.IsPrimaryKey, MySqlDbType.Byte),
				Database.CreateInParameter("@ControlType", item.ControlType != null ? item.ControlType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@ControlID", item.ControlID != null ? item.ControlID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@FieldLabel", item.FieldLabel != null ? item.FieldLabel : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RowIndex", item.RowIndex, MySqlDbType.Int32),
				Database.CreateInParameter("@ColumnIndex", item.ColumnIndex, MySqlDbType.Int32),
				Database.CreateInParameter("@WidthNum", item.WidthNum, MySqlDbType.Int32),
				Database.CreateInParameter("@EditParameterName", item.EditParameterName != null ? item.EditParameterName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditDbType", item.EditDbType != null ? item.EditDbType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditDotNetType", item.EditDotNetType != null ? item.EditDotNetType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DefaultValueSet", item.DefaultValueSet != null ? item.DefaultValueSet : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DefaultValueString", item.DefaultValueString != null ? item.DefaultValueString : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@JsonData", item.JsonData != null ? item.JsonData : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@EditItemID_Original", item.HasOriginal ? item.OriginalEditItemID : item.EditItemID, MySqlDbType.Int64),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_listedit_edititemEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_listedit_edititemEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "editItemID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long editItemID, string set_, params object[] values_)
		{
			return Put(set_, "`EditItemID` = @EditItemID", ConcatValues(values_, editItemID));
		}
		public async Task<int> PutByPKAsync(long editItemID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`EditItemID` = @EditItemID", ConcatValues(values_, editItemID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long editItemID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`EditItemID` = @EditItemID", tm_, ConcatValues(values_, editItemID));
		}
		public async Task<int> PutByPKAsync(long editItemID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`EditItemID` = @EditItemID", tm_, ConcatValues(values_, editItemID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long editItemID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
	        };
			return Put(set_, "`EditItemID` = @EditItemID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(long editItemID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
	        };
			return await PutAsync(set_, "`EditItemID` = @EditItemID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutGenID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGenIDByPK(long editItemID, long genID, TransactionManager tm_ = null)
		{
			RepairPutGenIDByPKData(editItemID, genID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutGenIDByPKAsync(long editItemID, long genID, TransactionManager tm_ = null)
		{
			RepairPutGenIDByPKData(editItemID, genID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutGenIDByPKData(long editItemID, long genID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `GenID` = @GenID  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGenID(long genID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `GenID` = @GenID";
			var parameter_ = Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutGenIDAsync(long genID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `GenID` = @GenID";
			var parameter_ = Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutGenID
		#region PutColumnName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "columnName">列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutColumnNameByPK(long editItemID, string columnName, TransactionManager tm_ = null)
		{
			RepairPutColumnNameByPKData(editItemID, columnName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutColumnNameByPKAsync(long editItemID, string columnName, TransactionManager tm_ = null)
		{
			RepairPutColumnNameByPKData(editItemID, columnName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutColumnNameByPKData(long editItemID, string columnName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `ColumnName` = @ColumnName  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ColumnName", columnName != null ? columnName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "columnName">列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutColumnName(string columnName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `ColumnName` = @ColumnName";
			var parameter_ = Database.CreateInParameter("@ColumnName", columnName != null ? columnName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutColumnNameAsync(string columnName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `ColumnName` = @ColumnName";
			var parameter_ = Database.CreateInParameter("@ColumnName", columnName != null ? columnName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutColumnName
		#region PutIsPrimaryKey
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIsPrimaryKeyByPK(long editItemID, bool isPrimaryKey, TransactionManager tm_ = null)
		{
			RepairPutIsPrimaryKeyByPKData(editItemID, isPrimaryKey, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutIsPrimaryKeyByPKAsync(long editItemID, bool isPrimaryKey, TransactionManager tm_ = null)
		{
			RepairPutIsPrimaryKeyByPKData(editItemID, isPrimaryKey, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutIsPrimaryKeyByPKData(long editItemID, bool isPrimaryKey, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `IsPrimaryKey` = @IsPrimaryKey  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIsPrimaryKey(bool isPrimaryKey, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `IsPrimaryKey` = @IsPrimaryKey";
			var parameter_ = Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutIsPrimaryKeyAsync(bool isPrimaryKey, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `IsPrimaryKey` = @IsPrimaryKey";
			var parameter_ = Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutIsPrimaryKey
		#region PutControlType
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutControlTypeByPK(long editItemID, string controlType, TransactionManager tm_ = null)
		{
			RepairPutControlTypeByPKData(editItemID, controlType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutControlTypeByPKAsync(long editItemID, string controlType, TransactionManager tm_ = null)
		{
			RepairPutControlTypeByPKData(editItemID, controlType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutControlTypeByPKData(long editItemID, string controlType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `ControlType` = @ControlType  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ControlType", controlType != null ? controlType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutControlType(string controlType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `ControlType` = @ControlType";
			var parameter_ = Database.CreateInParameter("@ControlType", controlType != null ? controlType : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutControlTypeAsync(string controlType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `ControlType` = @ControlType";
			var parameter_ = Database.CreateInParameter("@ControlType", controlType != null ? controlType : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutControlType
		#region PutControlID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutControlIDByPK(long editItemID, string controlID, TransactionManager tm_ = null)
		{
			RepairPutControlIDByPKData(editItemID, controlID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutControlIDByPKAsync(long editItemID, string controlID, TransactionManager tm_ = null)
		{
			RepairPutControlIDByPKData(editItemID, controlID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutControlIDByPKData(long editItemID, string controlID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `ControlID` = @ControlID  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ControlID", controlID != null ? controlID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutControlID(string controlID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `ControlID` = @ControlID";
			var parameter_ = Database.CreateInParameter("@ControlID", controlID != null ? controlID : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutControlIDAsync(string controlID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `ControlID` = @ControlID";
			var parameter_ = Database.CreateInParameter("@ControlID", controlID != null ? controlID : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutControlID
		#region PutFieldLabel
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFieldLabelByPK(long editItemID, string fieldLabel, TransactionManager tm_ = null)
		{
			RepairPutFieldLabelByPKData(editItemID, fieldLabel, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutFieldLabelByPKAsync(long editItemID, string fieldLabel, TransactionManager tm_ = null)
		{
			RepairPutFieldLabelByPKData(editItemID, fieldLabel, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutFieldLabelByPKData(long editItemID, string fieldLabel, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `FieldLabel` = @FieldLabel  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@FieldLabel", fieldLabel != null ? fieldLabel : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFieldLabel(string fieldLabel, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `FieldLabel` = @FieldLabel";
			var parameter_ = Database.CreateInParameter("@FieldLabel", fieldLabel != null ? fieldLabel : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutFieldLabelAsync(string fieldLabel, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `FieldLabel` = @FieldLabel";
			var parameter_ = Database.CreateInParameter("@FieldLabel", fieldLabel != null ? fieldLabel : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutFieldLabel
		#region PutRowIndex
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRowIndexByPK(long editItemID, int rowIndex, TransactionManager tm_ = null)
		{
			RepairPutRowIndexByPKData(editItemID, rowIndex, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRowIndexByPKAsync(long editItemID, int rowIndex, TransactionManager tm_ = null)
		{
			RepairPutRowIndexByPKData(editItemID, rowIndex, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRowIndexByPKData(long editItemID, int rowIndex, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `RowIndex` = @RowIndex  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RowIndex", rowIndex, MySqlDbType.Int32),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRowIndex(int rowIndex, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `RowIndex` = @RowIndex";
			var parameter_ = Database.CreateInParameter("@RowIndex", rowIndex, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRowIndexAsync(int rowIndex, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `RowIndex` = @RowIndex";
			var parameter_ = Database.CreateInParameter("@RowIndex", rowIndex, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRowIndex
		#region PutColumnIndex
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutColumnIndexByPK(long editItemID, int columnIndex, TransactionManager tm_ = null)
		{
			RepairPutColumnIndexByPKData(editItemID, columnIndex, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutColumnIndexByPKAsync(long editItemID, int columnIndex, TransactionManager tm_ = null)
		{
			RepairPutColumnIndexByPKData(editItemID, columnIndex, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutColumnIndexByPKData(long editItemID, int columnIndex, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `ColumnIndex` = @ColumnIndex  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutColumnIndex(int columnIndex, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `ColumnIndex` = @ColumnIndex";
			var parameter_ = Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutColumnIndexAsync(int columnIndex, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `ColumnIndex` = @ColumnIndex";
			var parameter_ = Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutColumnIndex
		#region PutWidthNum
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "widthNum">宽度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutWidthNumByPK(long editItemID, int widthNum, TransactionManager tm_ = null)
		{
			RepairPutWidthNumByPKData(editItemID, widthNum, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutWidthNumByPKAsync(long editItemID, int widthNum, TransactionManager tm_ = null)
		{
			RepairPutWidthNumByPKData(editItemID, widthNum, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutWidthNumByPKData(long editItemID, int widthNum, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `WidthNum` = @WidthNum  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@WidthNum", widthNum, MySqlDbType.Int32),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "widthNum">宽度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutWidthNum(int widthNum, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `WidthNum` = @WidthNum";
			var parameter_ = Database.CreateInParameter("@WidthNum", widthNum, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutWidthNumAsync(int widthNum, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `WidthNum` = @WidthNum";
			var parameter_ = Database.CreateInParameter("@WidthNum", widthNum, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutWidthNum
		#region PutEditParameterName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "editParameterName">参数名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditParameterNameByPK(long editItemID, string editParameterName, TransactionManager tm_ = null)
		{
			RepairPutEditParameterNameByPKData(editItemID, editParameterName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutEditParameterNameByPKAsync(long editItemID, string editParameterName, TransactionManager tm_ = null)
		{
			RepairPutEditParameterNameByPKData(editItemID, editParameterName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutEditParameterNameByPKData(long editItemID, string editParameterName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `EditParameterName` = @EditParameterName  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditParameterName", editParameterName != null ? editParameterName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "editParameterName">参数名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditParameterName(string editParameterName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `EditParameterName` = @EditParameterName";
			var parameter_ = Database.CreateInParameter("@EditParameterName", editParameterName != null ? editParameterName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutEditParameterNameAsync(string editParameterName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `EditParameterName` = @EditParameterName";
			var parameter_ = Database.CreateInParameter("@EditParameterName", editParameterName != null ? editParameterName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutEditParameterName
		#region PutEditDbType
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "editDbType">参数类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditDbTypeByPK(long editItemID, string editDbType, TransactionManager tm_ = null)
		{
			RepairPutEditDbTypeByPKData(editItemID, editDbType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutEditDbTypeByPKAsync(long editItemID, string editDbType, TransactionManager tm_ = null)
		{
			RepairPutEditDbTypeByPKData(editItemID, editDbType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutEditDbTypeByPKData(long editItemID, string editDbType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `EditDbType` = @EditDbType  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditDbType", editDbType != null ? editDbType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "editDbType">参数类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditDbType(string editDbType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `EditDbType` = @EditDbType";
			var parameter_ = Database.CreateInParameter("@EditDbType", editDbType != null ? editDbType : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutEditDbTypeAsync(string editDbType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `EditDbType` = @EditDbType";
			var parameter_ = Database.CreateInParameter("@EditDbType", editDbType != null ? editDbType : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutEditDbType
		#region PutEditDotNetType
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "editDotNetType">参数DotNet类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditDotNetTypeByPK(long editItemID, string editDotNetType, TransactionManager tm_ = null)
		{
			RepairPutEditDotNetTypeByPKData(editItemID, editDotNetType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutEditDotNetTypeByPKAsync(long editItemID, string editDotNetType, TransactionManager tm_ = null)
		{
			RepairPutEditDotNetTypeByPKData(editItemID, editDotNetType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutEditDotNetTypeByPKData(long editItemID, string editDotNetType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `EditDotNetType` = @EditDotNetType  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditDotNetType", editDotNetType != null ? editDotNetType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "editDotNetType">参数DotNet类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditDotNetType(string editDotNetType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `EditDotNetType` = @EditDotNetType";
			var parameter_ = Database.CreateInParameter("@EditDotNetType", editDotNetType != null ? editDotNetType : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutEditDotNetTypeAsync(string editDotNetType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `EditDotNetType` = @EditDotNetType";
			var parameter_ = Database.CreateInParameter("@EditDotNetType", editDotNetType != null ? editDotNetType : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutEditDotNetType
		#region PutDefaultValueSet
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "defaultValueSet">默认值类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDefaultValueSetByPK(long editItemID, string defaultValueSet, TransactionManager tm_ = null)
		{
			RepairPutDefaultValueSetByPKData(editItemID, defaultValueSet, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDefaultValueSetByPKAsync(long editItemID, string defaultValueSet, TransactionManager tm_ = null)
		{
			RepairPutDefaultValueSetByPKData(editItemID, defaultValueSet, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDefaultValueSetByPKData(long editItemID, string defaultValueSet, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `DefaultValueSet` = @DefaultValueSet  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DefaultValueSet", defaultValueSet != null ? defaultValueSet : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "defaultValueSet">默认值类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDefaultValueSet(string defaultValueSet, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `DefaultValueSet` = @DefaultValueSet";
			var parameter_ = Database.CreateInParameter("@DefaultValueSet", defaultValueSet != null ? defaultValueSet : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDefaultValueSetAsync(string defaultValueSet, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `DefaultValueSet` = @DefaultValueSet";
			var parameter_ = Database.CreateInParameter("@DefaultValueSet", defaultValueSet != null ? defaultValueSet : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDefaultValueSet
		#region PutDefaultValueString
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "defaultValueString">默认值字符串</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDefaultValueStringByPK(long editItemID, string defaultValueString, TransactionManager tm_ = null)
		{
			RepairPutDefaultValueStringByPKData(editItemID, defaultValueString, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDefaultValueStringByPKAsync(long editItemID, string defaultValueString, TransactionManager tm_ = null)
		{
			RepairPutDefaultValueStringByPKData(editItemID, defaultValueString, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDefaultValueStringByPKData(long editItemID, string defaultValueString, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `DefaultValueString` = @DefaultValueString  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DefaultValueString", defaultValueString != null ? defaultValueString : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "defaultValueString">默认值字符串</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDefaultValueString(string defaultValueString, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `DefaultValueString` = @DefaultValueString";
			var parameter_ = Database.CreateInParameter("@DefaultValueString", defaultValueString != null ? defaultValueString : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDefaultValueStringAsync(string defaultValueString, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `DefaultValueString` = @DefaultValueString";
			var parameter_ = Database.CreateInParameter("@DefaultValueString", defaultValueString != null ? defaultValueString : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDefaultValueString
		#region PutJsonData
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutJsonDataByPK(long editItemID, string jsonData, TransactionManager tm_ = null)
		{
			RepairPutJsonDataByPKData(editItemID, jsonData, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutJsonDataByPKAsync(long editItemID, string jsonData, TransactionManager tm_ = null)
		{
			RepairPutJsonDataByPKData(editItemID, jsonData, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutJsonDataByPKData(long editItemID, string jsonData, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `JsonData` = @JsonData  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@JsonData", jsonData != null ? jsonData : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutJsonData(string jsonData, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `JsonData` = @JsonData";
			var parameter_ = Database.CreateInParameter("@JsonData", jsonData != null ? jsonData : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutJsonDataAsync(string jsonData, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `JsonData` = @JsonData";
			var parameter_ = Database.CreateInParameter("@JsonData", jsonData != null ? jsonData : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutJsonData
		#region PutRecDate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRecDateByPK(long editItemID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(editItemID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRecDateByPKAsync(long editItemID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(editItemID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRecDateByPKData(long editItemID, DateTime recDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_edititem` SET `RecDate` = @RecDate  WHERE `EditItemID` = @EditItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `RecDate` = @RecDate";
			var parameter_ = Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRecDateAsync(DateTime recDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_edititem` SET `RecDate` = @RecDate";
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
		public bool Set(Admin_listedit_edititemEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.EditItemID) == null)
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
		public async Task<bool> SetAsync(Admin_listedit_edititemEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.EditItemID) == null)
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
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_listedit_edititemEO GetByPK(long editItemID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(editItemID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<Admin_listedit_edititemEO> GetByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(editItemID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		private void RepairGetByPKData(long editItemID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`EditItemID` = @EditItemID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 GenID（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public long GetGenIDByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (long)GetScalar("`GenID`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<long> GetGenIDByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (long)await GetScalarAsync("`GenID`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ColumnName（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetColumnNameByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`ColumnName`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<string> GetColumnNameByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`ColumnName`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 IsPrimaryKey（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public bool GetIsPrimaryKeyByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (bool)GetScalar("`IsPrimaryKey`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<bool> GetIsPrimaryKeyByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (bool)await GetScalarAsync("`IsPrimaryKey`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ControlType（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetControlTypeByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`ControlType`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<string> GetControlTypeByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`ControlType`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ControlID（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetControlIDByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`ControlID`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<string> GetControlIDByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`ControlID`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 FieldLabel（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetFieldLabelByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`FieldLabel`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<string> GetFieldLabelByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`FieldLabel`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RowIndex（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetRowIndexByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`RowIndex`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<int> GetRowIndexByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`RowIndex`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ColumnIndex（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetColumnIndexByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`ColumnIndex`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<int> GetColumnIndexByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`ColumnIndex`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 WidthNum（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetWidthNumByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`WidthNum`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<int> GetWidthNumByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`WidthNum`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 EditParameterName（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetEditParameterNameByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`EditParameterName`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<string> GetEditParameterNameByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`EditParameterName`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 EditDbType（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetEditDbTypeByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`EditDbType`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<string> GetEditDbTypeByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`EditDbType`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 EditDotNetType（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetEditDotNetTypeByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`EditDotNetType`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<string> GetEditDotNetTypeByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`EditDotNetType`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 DefaultValueSet（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetDefaultValueSetByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`DefaultValueSet`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<string> GetDefaultValueSetByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`DefaultValueSet`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 DefaultValueString（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetDefaultValueStringByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`DefaultValueString`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<string> GetDefaultValueStringByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`DefaultValueString`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 JsonData（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetJsonDataByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`JsonData`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<string> GetJsonDataByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`JsonData`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RecDate（字段）
		/// </summary>
		/// /// <param name = "editItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public DateTime GetRecDateByPK(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (DateTime)GetScalar("`RecDate`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		public async Task<DateTime> GetRecDateByPKAsync(long editItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditItemID", editItemID, MySqlDbType.Int64),
			};
			return (DateTime)await GetScalarAsync("`RecDate`", "`EditItemID` = @EditItemID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByGenID
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByGenID(long genID)
		{
			return GetByGenID(genID, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByGenIDAsync(long genID)
		{
			return await GetByGenIDAsync(genID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByGenID(long genID, TransactionManager tm_)
		{
			return GetByGenID(genID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByGenIDAsync(long genID, TransactionManager tm_)
		{
			return await GetByGenIDAsync(genID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByGenID(long genID, int top_)
		{
			return GetByGenID(genID, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByGenIDAsync(long genID, int top_)
		{
			return await GetByGenIDAsync(genID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByGenID(long genID, int top_, TransactionManager tm_)
		{
			return GetByGenID(genID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByGenIDAsync(long genID, int top_, TransactionManager tm_)
		{
			return await GetByGenIDAsync(genID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByGenID(long genID, string sort_)
		{
			return GetByGenID(genID, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByGenIDAsync(long genID, string sort_)
		{
			return await GetByGenIDAsync(genID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByGenID(long genID, string sort_, TransactionManager tm_)
		{
			return GetByGenID(genID, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByGenIDAsync(long genID, string sort_, TransactionManager tm_)
		{
			return await GetByGenIDAsync(genID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByGenID(long genID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`GenID` = @GenID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByGenIDAsync(long genID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`GenID` = @GenID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByGenID
		#region GetByColumnName
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">列名</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnName(string columnName)
		{
			return GetByColumnName(columnName, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnNameAsync(string columnName)
		{
			return await GetByColumnNameAsync(columnName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnName(string columnName, TransactionManager tm_)
		{
			return GetByColumnName(columnName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnNameAsync(string columnName, TransactionManager tm_)
		{
			return await GetByColumnNameAsync(columnName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">列名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnName(string columnName, int top_)
		{
			return GetByColumnName(columnName, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnNameAsync(string columnName, int top_)
		{
			return await GetByColumnNameAsync(columnName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">列名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnName(string columnName, int top_, TransactionManager tm_)
		{
			return GetByColumnName(columnName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnNameAsync(string columnName, int top_, TransactionManager tm_)
		{
			return await GetByColumnNameAsync(columnName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">列名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnName(string columnName, string sort_)
		{
			return GetByColumnName(columnName, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnNameAsync(string columnName, string sort_)
		{
			return await GetByColumnNameAsync(columnName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">列名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnName(string columnName, string sort_, TransactionManager tm_)
		{
			return GetByColumnName(columnName, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnNameAsync(string columnName, string sort_, TransactionManager tm_)
		{
			return await GetByColumnNameAsync(columnName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">列名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnName(string columnName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(columnName != null ? "`ColumnName` = @ColumnName" : "`ColumnName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (columnName != null)
				paras_.Add(Database.CreateInParameter("@ColumnName", columnName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnNameAsync(string columnName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(columnName != null ? "`ColumnName` = @ColumnName" : "`ColumnName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (columnName != null)
				paras_.Add(Database.CreateInParameter("@ColumnName", columnName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByColumnName
		#region GetByIsPrimaryKey
		
		/// <summary>
		/// 按 IsPrimaryKey（字段） 查询
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByIsPrimaryKey(bool isPrimaryKey)
		{
			return GetByIsPrimaryKey(isPrimaryKey, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey)
		{
			return await GetByIsPrimaryKeyAsync(isPrimaryKey, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 IsPrimaryKey（字段） 查询
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByIsPrimaryKey(bool isPrimaryKey, TransactionManager tm_)
		{
			return GetByIsPrimaryKey(isPrimaryKey, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, TransactionManager tm_)
		{
			return await GetByIsPrimaryKeyAsync(isPrimaryKey, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IsPrimaryKey（字段） 查询
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByIsPrimaryKey(bool isPrimaryKey, int top_)
		{
			return GetByIsPrimaryKey(isPrimaryKey, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, int top_)
		{
			return await GetByIsPrimaryKeyAsync(isPrimaryKey, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 IsPrimaryKey（字段） 查询
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByIsPrimaryKey(bool isPrimaryKey, int top_, TransactionManager tm_)
		{
			return GetByIsPrimaryKey(isPrimaryKey, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, int top_, TransactionManager tm_)
		{
			return await GetByIsPrimaryKeyAsync(isPrimaryKey, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IsPrimaryKey（字段） 查询
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByIsPrimaryKey(bool isPrimaryKey, string sort_)
		{
			return GetByIsPrimaryKey(isPrimaryKey, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, string sort_)
		{
			return await GetByIsPrimaryKeyAsync(isPrimaryKey, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 IsPrimaryKey（字段） 查询
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByIsPrimaryKey(bool isPrimaryKey, string sort_, TransactionManager tm_)
		{
			return GetByIsPrimaryKey(isPrimaryKey, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, string sort_, TransactionManager tm_)
		{
			return await GetByIsPrimaryKeyAsync(isPrimaryKey, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 IsPrimaryKey（字段） 查询
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByIsPrimaryKey(bool isPrimaryKey, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`IsPrimaryKey` = @IsPrimaryKey", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`IsPrimaryKey` = @IsPrimaryKey", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByIsPrimaryKey
		#region GetByControlType
		
		/// <summary>
		/// 按 ControlType（字段） 查询
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlType(string controlType)
		{
			return GetByControlType(controlType, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlTypeAsync(string controlType)
		{
			return await GetByControlTypeAsync(controlType, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ControlType（字段） 查询
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlType(string controlType, TransactionManager tm_)
		{
			return GetByControlType(controlType, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlTypeAsync(string controlType, TransactionManager tm_)
		{
			return await GetByControlTypeAsync(controlType, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ControlType（字段） 查询
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlType(string controlType, int top_)
		{
			return GetByControlType(controlType, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlTypeAsync(string controlType, int top_)
		{
			return await GetByControlTypeAsync(controlType, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ControlType（字段） 查询
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlType(string controlType, int top_, TransactionManager tm_)
		{
			return GetByControlType(controlType, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlTypeAsync(string controlType, int top_, TransactionManager tm_)
		{
			return await GetByControlTypeAsync(controlType, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ControlType（字段） 查询
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlType(string controlType, string sort_)
		{
			return GetByControlType(controlType, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlTypeAsync(string controlType, string sort_)
		{
			return await GetByControlTypeAsync(controlType, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ControlType（字段） 查询
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlType(string controlType, string sort_, TransactionManager tm_)
		{
			return GetByControlType(controlType, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlTypeAsync(string controlType, string sort_, TransactionManager tm_)
		{
			return await GetByControlTypeAsync(controlType, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ControlType（字段） 查询
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlType(string controlType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(controlType != null ? "`ControlType` = @ControlType" : "`ControlType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (controlType != null)
				paras_.Add(Database.CreateInParameter("@ControlType", controlType, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlTypeAsync(string controlType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(controlType != null ? "`ControlType` = @ControlType" : "`ControlType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (controlType != null)
				paras_.Add(Database.CreateInParameter("@ControlType", controlType, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByControlType
		#region GetByControlID
		
		/// <summary>
		/// 按 ControlID（字段） 查询
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlID(string controlID)
		{
			return GetByControlID(controlID, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlIDAsync(string controlID)
		{
			return await GetByControlIDAsync(controlID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ControlID（字段） 查询
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlID(string controlID, TransactionManager tm_)
		{
			return GetByControlID(controlID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlIDAsync(string controlID, TransactionManager tm_)
		{
			return await GetByControlIDAsync(controlID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ControlID（字段） 查询
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlID(string controlID, int top_)
		{
			return GetByControlID(controlID, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlIDAsync(string controlID, int top_)
		{
			return await GetByControlIDAsync(controlID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ControlID（字段） 查询
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlID(string controlID, int top_, TransactionManager tm_)
		{
			return GetByControlID(controlID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlIDAsync(string controlID, int top_, TransactionManager tm_)
		{
			return await GetByControlIDAsync(controlID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ControlID（字段） 查询
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlID(string controlID, string sort_)
		{
			return GetByControlID(controlID, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlIDAsync(string controlID, string sort_)
		{
			return await GetByControlIDAsync(controlID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ControlID（字段） 查询
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlID(string controlID, string sort_, TransactionManager tm_)
		{
			return GetByControlID(controlID, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlIDAsync(string controlID, string sort_, TransactionManager tm_)
		{
			return await GetByControlIDAsync(controlID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ControlID（字段） 查询
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByControlID(string controlID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(controlID != null ? "`ControlID` = @ControlID" : "`ControlID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (controlID != null)
				paras_.Add(Database.CreateInParameter("@ControlID", controlID, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByControlIDAsync(string controlID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(controlID != null ? "`ControlID` = @ControlID" : "`ControlID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (controlID != null)
				paras_.Add(Database.CreateInParameter("@ControlID", controlID, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByControlID
		#region GetByFieldLabel
		
		/// <summary>
		/// 按 FieldLabel（字段） 查询
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByFieldLabel(string fieldLabel)
		{
			return GetByFieldLabel(fieldLabel, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByFieldLabelAsync(string fieldLabel)
		{
			return await GetByFieldLabelAsync(fieldLabel, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 FieldLabel（字段） 查询
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByFieldLabel(string fieldLabel, TransactionManager tm_)
		{
			return GetByFieldLabel(fieldLabel, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByFieldLabelAsync(string fieldLabel, TransactionManager tm_)
		{
			return await GetByFieldLabelAsync(fieldLabel, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 FieldLabel（字段） 查询
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByFieldLabel(string fieldLabel, int top_)
		{
			return GetByFieldLabel(fieldLabel, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByFieldLabelAsync(string fieldLabel, int top_)
		{
			return await GetByFieldLabelAsync(fieldLabel, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 FieldLabel（字段） 查询
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByFieldLabel(string fieldLabel, int top_, TransactionManager tm_)
		{
			return GetByFieldLabel(fieldLabel, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByFieldLabelAsync(string fieldLabel, int top_, TransactionManager tm_)
		{
			return await GetByFieldLabelAsync(fieldLabel, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 FieldLabel（字段） 查询
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByFieldLabel(string fieldLabel, string sort_)
		{
			return GetByFieldLabel(fieldLabel, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByFieldLabelAsync(string fieldLabel, string sort_)
		{
			return await GetByFieldLabelAsync(fieldLabel, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 FieldLabel（字段） 查询
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByFieldLabel(string fieldLabel, string sort_, TransactionManager tm_)
		{
			return GetByFieldLabel(fieldLabel, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByFieldLabelAsync(string fieldLabel, string sort_, TransactionManager tm_)
		{
			return await GetByFieldLabelAsync(fieldLabel, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 FieldLabel（字段） 查询
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByFieldLabel(string fieldLabel, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(fieldLabel != null ? "`FieldLabel` = @FieldLabel" : "`FieldLabel` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (fieldLabel != null)
				paras_.Add(Database.CreateInParameter("@FieldLabel", fieldLabel, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByFieldLabelAsync(string fieldLabel, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(fieldLabel != null ? "`FieldLabel` = @FieldLabel" : "`FieldLabel` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (fieldLabel != null)
				paras_.Add(Database.CreateInParameter("@FieldLabel", fieldLabel, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByFieldLabel
		#region GetByRowIndex
		
		/// <summary>
		/// 按 RowIndex（字段） 查询
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByRowIndex(int rowIndex)
		{
			return GetByRowIndex(rowIndex, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRowIndexAsync(int rowIndex)
		{
			return await GetByRowIndexAsync(rowIndex, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RowIndex（字段） 查询
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByRowIndex(int rowIndex, TransactionManager tm_)
		{
			return GetByRowIndex(rowIndex, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRowIndexAsync(int rowIndex, TransactionManager tm_)
		{
			return await GetByRowIndexAsync(rowIndex, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RowIndex（字段） 查询
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByRowIndex(int rowIndex, int top_)
		{
			return GetByRowIndex(rowIndex, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRowIndexAsync(int rowIndex, int top_)
		{
			return await GetByRowIndexAsync(rowIndex, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RowIndex（字段） 查询
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByRowIndex(int rowIndex, int top_, TransactionManager tm_)
		{
			return GetByRowIndex(rowIndex, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRowIndexAsync(int rowIndex, int top_, TransactionManager tm_)
		{
			return await GetByRowIndexAsync(rowIndex, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RowIndex（字段） 查询
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByRowIndex(int rowIndex, string sort_)
		{
			return GetByRowIndex(rowIndex, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRowIndexAsync(int rowIndex, string sort_)
		{
			return await GetByRowIndexAsync(rowIndex, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RowIndex（字段） 查询
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByRowIndex(int rowIndex, string sort_, TransactionManager tm_)
		{
			return GetByRowIndex(rowIndex, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRowIndexAsync(int rowIndex, string sort_, TransactionManager tm_)
		{
			return await GetByRowIndexAsync(rowIndex, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RowIndex（字段） 查询
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByRowIndex(int rowIndex, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RowIndex` = @RowIndex", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RowIndex", rowIndex, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRowIndexAsync(int rowIndex, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RowIndex` = @RowIndex", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RowIndex", rowIndex, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByRowIndex
		#region GetByColumnIndex
		
		/// <summary>
		/// 按 ColumnIndex（字段） 查询
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnIndex(int columnIndex)
		{
			return GetByColumnIndex(columnIndex, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnIndexAsync(int columnIndex)
		{
			return await GetByColumnIndexAsync(columnIndex, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ColumnIndex（字段） 查询
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnIndex(int columnIndex, TransactionManager tm_)
		{
			return GetByColumnIndex(columnIndex, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnIndexAsync(int columnIndex, TransactionManager tm_)
		{
			return await GetByColumnIndexAsync(columnIndex, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ColumnIndex（字段） 查询
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnIndex(int columnIndex, int top_)
		{
			return GetByColumnIndex(columnIndex, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnIndexAsync(int columnIndex, int top_)
		{
			return await GetByColumnIndexAsync(columnIndex, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ColumnIndex（字段） 查询
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnIndex(int columnIndex, int top_, TransactionManager tm_)
		{
			return GetByColumnIndex(columnIndex, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnIndexAsync(int columnIndex, int top_, TransactionManager tm_)
		{
			return await GetByColumnIndexAsync(columnIndex, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ColumnIndex（字段） 查询
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnIndex(int columnIndex, string sort_)
		{
			return GetByColumnIndex(columnIndex, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnIndexAsync(int columnIndex, string sort_)
		{
			return await GetByColumnIndexAsync(columnIndex, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ColumnIndex（字段） 查询
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnIndex(int columnIndex, string sort_, TransactionManager tm_)
		{
			return GetByColumnIndex(columnIndex, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnIndexAsync(int columnIndex, string sort_, TransactionManager tm_)
		{
			return await GetByColumnIndexAsync(columnIndex, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ColumnIndex（字段） 查询
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByColumnIndex(int columnIndex, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`ColumnIndex` = @ColumnIndex", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByColumnIndexAsync(int columnIndex, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`ColumnIndex` = @ColumnIndex", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByColumnIndex
		#region GetByWidthNum
		
		/// <summary>
		/// 按 WidthNum（字段） 查询
		/// </summary>
		/// /// <param name = "widthNum">宽度</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByWidthNum(int widthNum)
		{
			return GetByWidthNum(widthNum, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByWidthNumAsync(int widthNum)
		{
			return await GetByWidthNumAsync(widthNum, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 WidthNum（字段） 查询
		/// </summary>
		/// /// <param name = "widthNum">宽度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByWidthNum(int widthNum, TransactionManager tm_)
		{
			return GetByWidthNum(widthNum, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByWidthNumAsync(int widthNum, TransactionManager tm_)
		{
			return await GetByWidthNumAsync(widthNum, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 WidthNum（字段） 查询
		/// </summary>
		/// /// <param name = "widthNum">宽度</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByWidthNum(int widthNum, int top_)
		{
			return GetByWidthNum(widthNum, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByWidthNumAsync(int widthNum, int top_)
		{
			return await GetByWidthNumAsync(widthNum, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 WidthNum（字段） 查询
		/// </summary>
		/// /// <param name = "widthNum">宽度</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByWidthNum(int widthNum, int top_, TransactionManager tm_)
		{
			return GetByWidthNum(widthNum, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByWidthNumAsync(int widthNum, int top_, TransactionManager tm_)
		{
			return await GetByWidthNumAsync(widthNum, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 WidthNum（字段） 查询
		/// </summary>
		/// /// <param name = "widthNum">宽度</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByWidthNum(int widthNum, string sort_)
		{
			return GetByWidthNum(widthNum, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByWidthNumAsync(int widthNum, string sort_)
		{
			return await GetByWidthNumAsync(widthNum, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 WidthNum（字段） 查询
		/// </summary>
		/// /// <param name = "widthNum">宽度</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByWidthNum(int widthNum, string sort_, TransactionManager tm_)
		{
			return GetByWidthNum(widthNum, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByWidthNumAsync(int widthNum, string sort_, TransactionManager tm_)
		{
			return await GetByWidthNumAsync(widthNum, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 WidthNum（字段） 查询
		/// </summary>
		/// /// <param name = "widthNum">宽度</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByWidthNum(int widthNum, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`WidthNum` = @WidthNum", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@WidthNum", widthNum, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByWidthNumAsync(int widthNum, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`WidthNum` = @WidthNum", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@WidthNum", widthNum, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByWidthNum
		#region GetByEditParameterName
		
		/// <summary>
		/// 按 EditParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "editParameterName">参数名</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditParameterName(string editParameterName)
		{
			return GetByEditParameterName(editParameterName, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditParameterNameAsync(string editParameterName)
		{
			return await GetByEditParameterNameAsync(editParameterName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "editParameterName">参数名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditParameterName(string editParameterName, TransactionManager tm_)
		{
			return GetByEditParameterName(editParameterName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditParameterNameAsync(string editParameterName, TransactionManager tm_)
		{
			return await GetByEditParameterNameAsync(editParameterName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "editParameterName">参数名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditParameterName(string editParameterName, int top_)
		{
			return GetByEditParameterName(editParameterName, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditParameterNameAsync(string editParameterName, int top_)
		{
			return await GetByEditParameterNameAsync(editParameterName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "editParameterName">参数名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditParameterName(string editParameterName, int top_, TransactionManager tm_)
		{
			return GetByEditParameterName(editParameterName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditParameterNameAsync(string editParameterName, int top_, TransactionManager tm_)
		{
			return await GetByEditParameterNameAsync(editParameterName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "editParameterName">参数名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditParameterName(string editParameterName, string sort_)
		{
			return GetByEditParameterName(editParameterName, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditParameterNameAsync(string editParameterName, string sort_)
		{
			return await GetByEditParameterNameAsync(editParameterName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 EditParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "editParameterName">参数名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditParameterName(string editParameterName, string sort_, TransactionManager tm_)
		{
			return GetByEditParameterName(editParameterName, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditParameterNameAsync(string editParameterName, string sort_, TransactionManager tm_)
		{
			return await GetByEditParameterNameAsync(editParameterName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 EditParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "editParameterName">参数名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditParameterName(string editParameterName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(editParameterName != null ? "`EditParameterName` = @EditParameterName" : "`EditParameterName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (editParameterName != null)
				paras_.Add(Database.CreateInParameter("@EditParameterName", editParameterName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditParameterNameAsync(string editParameterName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(editParameterName != null ? "`EditParameterName` = @EditParameterName" : "`EditParameterName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (editParameterName != null)
				paras_.Add(Database.CreateInParameter("@EditParameterName", editParameterName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByEditParameterName
		#region GetByEditDbType
		
		/// <summary>
		/// 按 EditDbType（字段） 查询
		/// </summary>
		/// /// <param name = "editDbType">参数类型</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDbType(string editDbType)
		{
			return GetByEditDbType(editDbType, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDbTypeAsync(string editDbType)
		{
			return await GetByEditDbTypeAsync(editDbType, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditDbType（字段） 查询
		/// </summary>
		/// /// <param name = "editDbType">参数类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDbType(string editDbType, TransactionManager tm_)
		{
			return GetByEditDbType(editDbType, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDbTypeAsync(string editDbType, TransactionManager tm_)
		{
			return await GetByEditDbTypeAsync(editDbType, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditDbType（字段） 查询
		/// </summary>
		/// /// <param name = "editDbType">参数类型</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDbType(string editDbType, int top_)
		{
			return GetByEditDbType(editDbType, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDbTypeAsync(string editDbType, int top_)
		{
			return await GetByEditDbTypeAsync(editDbType, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditDbType（字段） 查询
		/// </summary>
		/// /// <param name = "editDbType">参数类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDbType(string editDbType, int top_, TransactionManager tm_)
		{
			return GetByEditDbType(editDbType, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDbTypeAsync(string editDbType, int top_, TransactionManager tm_)
		{
			return await GetByEditDbTypeAsync(editDbType, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditDbType（字段） 查询
		/// </summary>
		/// /// <param name = "editDbType">参数类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDbType(string editDbType, string sort_)
		{
			return GetByEditDbType(editDbType, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDbTypeAsync(string editDbType, string sort_)
		{
			return await GetByEditDbTypeAsync(editDbType, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 EditDbType（字段） 查询
		/// </summary>
		/// /// <param name = "editDbType">参数类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDbType(string editDbType, string sort_, TransactionManager tm_)
		{
			return GetByEditDbType(editDbType, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDbTypeAsync(string editDbType, string sort_, TransactionManager tm_)
		{
			return await GetByEditDbTypeAsync(editDbType, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 EditDbType（字段） 查询
		/// </summary>
		/// /// <param name = "editDbType">参数类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDbType(string editDbType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(editDbType != null ? "`EditDbType` = @EditDbType" : "`EditDbType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (editDbType != null)
				paras_.Add(Database.CreateInParameter("@EditDbType", editDbType, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDbTypeAsync(string editDbType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(editDbType != null ? "`EditDbType` = @EditDbType" : "`EditDbType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (editDbType != null)
				paras_.Add(Database.CreateInParameter("@EditDbType", editDbType, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByEditDbType
		#region GetByEditDotNetType
		
		/// <summary>
		/// 按 EditDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "editDotNetType">参数DotNet类型</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDotNetType(string editDotNetType)
		{
			return GetByEditDotNetType(editDotNetType, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDotNetTypeAsync(string editDotNetType)
		{
			return await GetByEditDotNetTypeAsync(editDotNetType, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "editDotNetType">参数DotNet类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDotNetType(string editDotNetType, TransactionManager tm_)
		{
			return GetByEditDotNetType(editDotNetType, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDotNetTypeAsync(string editDotNetType, TransactionManager tm_)
		{
			return await GetByEditDotNetTypeAsync(editDotNetType, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "editDotNetType">参数DotNet类型</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDotNetType(string editDotNetType, int top_)
		{
			return GetByEditDotNetType(editDotNetType, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDotNetTypeAsync(string editDotNetType, int top_)
		{
			return await GetByEditDotNetTypeAsync(editDotNetType, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "editDotNetType">参数DotNet类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDotNetType(string editDotNetType, int top_, TransactionManager tm_)
		{
			return GetByEditDotNetType(editDotNetType, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDotNetTypeAsync(string editDotNetType, int top_, TransactionManager tm_)
		{
			return await GetByEditDotNetTypeAsync(editDotNetType, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "editDotNetType">参数DotNet类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDotNetType(string editDotNetType, string sort_)
		{
			return GetByEditDotNetType(editDotNetType, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDotNetTypeAsync(string editDotNetType, string sort_)
		{
			return await GetByEditDotNetTypeAsync(editDotNetType, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 EditDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "editDotNetType">参数DotNet类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDotNetType(string editDotNetType, string sort_, TransactionManager tm_)
		{
			return GetByEditDotNetType(editDotNetType, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDotNetTypeAsync(string editDotNetType, string sort_, TransactionManager tm_)
		{
			return await GetByEditDotNetTypeAsync(editDotNetType, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 EditDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "editDotNetType">参数DotNet类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByEditDotNetType(string editDotNetType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(editDotNetType != null ? "`EditDotNetType` = @EditDotNetType" : "`EditDotNetType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (editDotNetType != null)
				paras_.Add(Database.CreateInParameter("@EditDotNetType", editDotNetType, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByEditDotNetTypeAsync(string editDotNetType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(editDotNetType != null ? "`EditDotNetType` = @EditDotNetType" : "`EditDotNetType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (editDotNetType != null)
				paras_.Add(Database.CreateInParameter("@EditDotNetType", editDotNetType, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByEditDotNetType
		#region GetByDefaultValueSet
		
		/// <summary>
		/// 按 DefaultValueSet（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueSet">默认值类型</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueSet(string defaultValueSet)
		{
			return GetByDefaultValueSet(defaultValueSet, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueSetAsync(string defaultValueSet)
		{
			return await GetByDefaultValueSetAsync(defaultValueSet, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DefaultValueSet（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueSet">默认值类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueSet(string defaultValueSet, TransactionManager tm_)
		{
			return GetByDefaultValueSet(defaultValueSet, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueSetAsync(string defaultValueSet, TransactionManager tm_)
		{
			return await GetByDefaultValueSetAsync(defaultValueSet, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DefaultValueSet（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueSet">默认值类型</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueSet(string defaultValueSet, int top_)
		{
			return GetByDefaultValueSet(defaultValueSet, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueSetAsync(string defaultValueSet, int top_)
		{
			return await GetByDefaultValueSetAsync(defaultValueSet, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DefaultValueSet（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueSet">默认值类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueSet(string defaultValueSet, int top_, TransactionManager tm_)
		{
			return GetByDefaultValueSet(defaultValueSet, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueSetAsync(string defaultValueSet, int top_, TransactionManager tm_)
		{
			return await GetByDefaultValueSetAsync(defaultValueSet, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DefaultValueSet（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueSet">默认值类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueSet(string defaultValueSet, string sort_)
		{
			return GetByDefaultValueSet(defaultValueSet, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueSetAsync(string defaultValueSet, string sort_)
		{
			return await GetByDefaultValueSetAsync(defaultValueSet, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 DefaultValueSet（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueSet">默认值类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueSet(string defaultValueSet, string sort_, TransactionManager tm_)
		{
			return GetByDefaultValueSet(defaultValueSet, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueSetAsync(string defaultValueSet, string sort_, TransactionManager tm_)
		{
			return await GetByDefaultValueSetAsync(defaultValueSet, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 DefaultValueSet（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueSet">默认值类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueSet(string defaultValueSet, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(defaultValueSet != null ? "`DefaultValueSet` = @DefaultValueSet" : "`DefaultValueSet` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (defaultValueSet != null)
				paras_.Add(Database.CreateInParameter("@DefaultValueSet", defaultValueSet, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueSetAsync(string defaultValueSet, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(defaultValueSet != null ? "`DefaultValueSet` = @DefaultValueSet" : "`DefaultValueSet` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (defaultValueSet != null)
				paras_.Add(Database.CreateInParameter("@DefaultValueSet", defaultValueSet, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByDefaultValueSet
		#region GetByDefaultValueString
		
		/// <summary>
		/// 按 DefaultValueString（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueString">默认值字符串</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueString(string defaultValueString)
		{
			return GetByDefaultValueString(defaultValueString, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueStringAsync(string defaultValueString)
		{
			return await GetByDefaultValueStringAsync(defaultValueString, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DefaultValueString（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueString">默认值字符串</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueString(string defaultValueString, TransactionManager tm_)
		{
			return GetByDefaultValueString(defaultValueString, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueStringAsync(string defaultValueString, TransactionManager tm_)
		{
			return await GetByDefaultValueStringAsync(defaultValueString, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DefaultValueString（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueString">默认值字符串</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueString(string defaultValueString, int top_)
		{
			return GetByDefaultValueString(defaultValueString, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueStringAsync(string defaultValueString, int top_)
		{
			return await GetByDefaultValueStringAsync(defaultValueString, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DefaultValueString（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueString">默认值字符串</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueString(string defaultValueString, int top_, TransactionManager tm_)
		{
			return GetByDefaultValueString(defaultValueString, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueStringAsync(string defaultValueString, int top_, TransactionManager tm_)
		{
			return await GetByDefaultValueStringAsync(defaultValueString, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DefaultValueString（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueString">默认值字符串</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueString(string defaultValueString, string sort_)
		{
			return GetByDefaultValueString(defaultValueString, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueStringAsync(string defaultValueString, string sort_)
		{
			return await GetByDefaultValueStringAsync(defaultValueString, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 DefaultValueString（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueString">默认值字符串</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueString(string defaultValueString, string sort_, TransactionManager tm_)
		{
			return GetByDefaultValueString(defaultValueString, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueStringAsync(string defaultValueString, string sort_, TransactionManager tm_)
		{
			return await GetByDefaultValueStringAsync(defaultValueString, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 DefaultValueString（字段） 查询
		/// </summary>
		/// /// <param name = "defaultValueString">默认值字符串</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByDefaultValueString(string defaultValueString, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(defaultValueString != null ? "`DefaultValueString` = @DefaultValueString" : "`DefaultValueString` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (defaultValueString != null)
				paras_.Add(Database.CreateInParameter("@DefaultValueString", defaultValueString, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByDefaultValueStringAsync(string defaultValueString, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(defaultValueString != null ? "`DefaultValueString` = @DefaultValueString" : "`DefaultValueString` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (defaultValueString != null)
				paras_.Add(Database.CreateInParameter("@DefaultValueString", defaultValueString, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByDefaultValueString
		#region GetByJsonData
		
		/// <summary>
		/// 按 JsonData（字段） 查询
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByJsonData(string jsonData)
		{
			return GetByJsonData(jsonData, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByJsonDataAsync(string jsonData)
		{
			return await GetByJsonDataAsync(jsonData, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 JsonData（字段） 查询
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByJsonData(string jsonData, TransactionManager tm_)
		{
			return GetByJsonData(jsonData, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByJsonDataAsync(string jsonData, TransactionManager tm_)
		{
			return await GetByJsonDataAsync(jsonData, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 JsonData（字段） 查询
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByJsonData(string jsonData, int top_)
		{
			return GetByJsonData(jsonData, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByJsonDataAsync(string jsonData, int top_)
		{
			return await GetByJsonDataAsync(jsonData, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 JsonData（字段） 查询
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByJsonData(string jsonData, int top_, TransactionManager tm_)
		{
			return GetByJsonData(jsonData, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByJsonDataAsync(string jsonData, int top_, TransactionManager tm_)
		{
			return await GetByJsonDataAsync(jsonData, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 JsonData（字段） 查询
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByJsonData(string jsonData, string sort_)
		{
			return GetByJsonData(jsonData, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByJsonDataAsync(string jsonData, string sort_)
		{
			return await GetByJsonDataAsync(jsonData, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 JsonData（字段） 查询
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByJsonData(string jsonData, string sort_, TransactionManager tm_)
		{
			return GetByJsonData(jsonData, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByJsonDataAsync(string jsonData, string sort_, TransactionManager tm_)
		{
			return await GetByJsonDataAsync(jsonData, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 JsonData（字段） 查询
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByJsonData(string jsonData, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(jsonData != null ? "`JsonData` = @JsonData" : "`JsonData` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (jsonData != null)
				paras_.Add(Database.CreateInParameter("@JsonData", jsonData, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByJsonDataAsync(string jsonData, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(jsonData != null ? "`JsonData` = @JsonData" : "`JsonData` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (jsonData != null)
				paras_.Add(Database.CreateInParameter("@JsonData", jsonData, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByJsonData
		#region GetByRecDate
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByRecDate(DateTime recDate)
		{
			return GetByRecDate(recDate, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRecDateAsync(DateTime recDate)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByRecDate(DateTime recDate, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRecDateAsync(DateTime recDate, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByRecDate(DateTime recDate, int top_)
		{
			return GetByRecDate(recDate, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRecDateAsync(DateTime recDate, int top_)
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
		public List<Admin_listedit_edititemEO> GetByRecDate(DateTime recDate, int top_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRecDateAsync(DateTime recDate, int top_, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_edititemEO> GetByRecDate(DateTime recDate, string sort_)
		{
			return GetByRecDate(recDate, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRecDateAsync(DateTime recDate, string sort_)
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
		public List<Admin_listedit_edititemEO> GetByRecDate(DateTime recDate, string sort_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRecDateAsync(DateTime recDate, string sort_, TransactionManager tm_)
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
		public List<Admin_listedit_edititemEO> GetByRecDate(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_edititemEO>> GetByRecDateAsync(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_edititemEO.MapDataReader);
		}
		#endregion // GetByRecDate
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
