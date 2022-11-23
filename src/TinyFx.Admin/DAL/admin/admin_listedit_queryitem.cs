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
	/// 查询条件控件定义
	/// 【表 admin_listedit_queryitem 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_listedit_queryitemEO : IRowMapper<Admin_listedit_queryitemEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_listedit_queryitemEO()
		{
			this.RowIndex = 0;
			this.ColumnIndex = 0;
			this.RecDate = DateTime.Now;
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
		private long _originalQueryItemID;
		/// <summary>
		/// 【数据库中的原始主键 QueryItemID 值的副本，用于主键值更新】
		/// </summary>
		public long OriginalQueryItemID
		{
			get { return _originalQueryItemID; }
			set { HasOriginal = true; _originalQueryItemID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "QueryItemID", QueryItemID }, };
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
		public long QueryItemID { get; set; }
		/// <summary>
		/// Gen编码
		/// 【字段 bigint】
		/// </summary>
		[DataMember(Order = 2)]
		public long GenID { get; set; }
		/// <summary>
		/// 查询语句块
		/// 【字段 varchar(255)】
		/// </summary>
		[DataMember(Order = 3)]
		public string QueryBlock { get; set; }
		/// <summary>
		/// 查询参数名
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 4)]
		public string QueryParameterName { get; set; }
		/// <summary>
		/// 参数DbType类型
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 5)]
		public string QueryDbType { get; set; }
		/// <summary>
		/// 参数DotNet类型
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 6)]
		public string QueryDotNetType { get; set; }
		/// <summary>
		/// 控件类型
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 7)]
		public string ControlType { get; set; }
		/// <summary>
		/// 控件ID
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 8)]
		public string ControlID { get; set; }
		/// <summary>
		/// Label
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 9)]
		public string FieldLabel { get; set; }
		/// <summary>
		/// 所在行
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 10)]
		public int RowIndex { get; set; }
		/// <summary>
		/// 所在列
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 11)]
		public int ColumnIndex { get; set; }
		/// <summary>
		/// Json系列化
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 12)]
		public string JsonData { get; set; }
		/// <summary>
		/// 记录日期
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 13)]
		public DateTime RecDate { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_listedit_queryitemEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_listedit_queryitemEO MapDataReader(IDataReader reader)
		{
		    Admin_listedit_queryitemEO ret = new Admin_listedit_queryitemEO();
			ret.QueryItemID = reader.ToInt64("QueryItemID");
			ret.OriginalQueryItemID = ret.QueryItemID;
			ret.GenID = reader.ToInt64("GenID");
			ret.QueryBlock = reader.ToString("QueryBlock");
			ret.QueryParameterName = reader.ToString("QueryParameterName");
			ret.QueryDbType = reader.ToString("QueryDbType");
			ret.QueryDotNetType = reader.ToString("QueryDotNetType");
			ret.ControlType = reader.ToString("ControlType");
			ret.ControlID = reader.ToString("ControlID");
			ret.FieldLabel = reader.ToString("FieldLabel");
			ret.RowIndex = reader.ToInt32("RowIndex");
			ret.ColumnIndex = reader.ToInt32("ColumnIndex");
			ret.JsonData = reader.ToString("JsonData");
			ret.RecDate = reader.ToDateTime("RecDate");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 查询条件控件定义
	/// 【表 admin_listedit_queryitem 的操作类】
	/// </summary>
	public class Admin_listedit_queryitemMO : MySqlTableMO<Admin_listedit_queryitemEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_listedit_queryitem`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_listedit_queryitemMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_listedit_queryitemMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_listedit_queryitemMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_listedit_queryitemEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			item.QueryItemID = Database.ExecSqlScalar<long>(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_listedit_queryitemEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			item.QueryItemID = await Database.ExecSqlScalarAsync<long>(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_listedit_queryitemEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_listedit_queryitem` (`GenID`, `QueryBlock`, `QueryParameterName`, `QueryDbType`, `QueryDotNetType`, `ControlType`, `ControlID`, `FieldLabel`, `RowIndex`, `ColumnIndex`, `JsonData`, `RecDate`) VALUE (@GenID, @QueryBlock, @QueryParameterName, @QueryDbType, @QueryDotNetType, @ControlType, @ControlID, @FieldLabel, @RowIndex, @ColumnIndex, @JsonData, @RecDate);SELECT LAST_INSERT_ID();";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", item.GenID, MySqlDbType.Int64),
				Database.CreateInParameter("@QueryBlock", item.QueryBlock != null ? item.QueryBlock : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryParameterName", item.QueryParameterName != null ? item.QueryParameterName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryDbType", item.QueryDbType != null ? item.QueryDbType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryDotNetType", item.QueryDotNetType != null ? item.QueryDotNetType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@ControlType", item.ControlType != null ? item.ControlType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@ControlID", item.ControlID != null ? item.ControlID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@FieldLabel", item.FieldLabel != null ? item.FieldLabel : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RowIndex", item.RowIndex, MySqlDbType.Int32),
				Database.CreateInParameter("@ColumnIndex", item.ColumnIndex, MySqlDbType.Int32),
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
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(long queryItemID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(queryItemID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(queryItemID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(long queryItemID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_listedit_queryitemEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.QueryItemID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_listedit_queryitemEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.QueryItemID, tm_);
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
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64));
		}
		#endregion // RemoveByGenID
		#region RemoveByQueryBlock
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "queryBlock">查询语句块</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByQueryBlock(string queryBlock, TransactionManager tm_ = null)
		{
			RepairRemoveByQueryBlockData(queryBlock, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByQueryBlockAsync(string queryBlock, TransactionManager tm_ = null)
		{
			RepairRemoveByQueryBlockData(queryBlock, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByQueryBlockData(string queryBlock, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE " + (queryBlock != null ? "`QueryBlock` = @QueryBlock" : "`QueryBlock` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (queryBlock != null)
				paras_.Add(Database.CreateInParameter("@QueryBlock", queryBlock, MySqlDbType.VarChar));
		}
		#endregion // RemoveByQueryBlock
		#region RemoveByQueryParameterName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "queryParameterName">查询参数名</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByQueryParameterName(string queryParameterName, TransactionManager tm_ = null)
		{
			RepairRemoveByQueryParameterNameData(queryParameterName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByQueryParameterNameAsync(string queryParameterName, TransactionManager tm_ = null)
		{
			RepairRemoveByQueryParameterNameData(queryParameterName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByQueryParameterNameData(string queryParameterName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE " + (queryParameterName != null ? "`QueryParameterName` = @QueryParameterName" : "`QueryParameterName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (queryParameterName != null)
				paras_.Add(Database.CreateInParameter("@QueryParameterName", queryParameterName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByQueryParameterName
		#region RemoveByQueryDbType
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "queryDbType">参数DbType类型</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByQueryDbType(string queryDbType, TransactionManager tm_ = null)
		{
			RepairRemoveByQueryDbTypeData(queryDbType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByQueryDbTypeAsync(string queryDbType, TransactionManager tm_ = null)
		{
			RepairRemoveByQueryDbTypeData(queryDbType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByQueryDbTypeData(string queryDbType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE " + (queryDbType != null ? "`QueryDbType` = @QueryDbType" : "`QueryDbType` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (queryDbType != null)
				paras_.Add(Database.CreateInParameter("@QueryDbType", queryDbType, MySqlDbType.VarChar));
		}
		#endregion // RemoveByQueryDbType
		#region RemoveByQueryDotNetType
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "queryDotNetType">参数DotNet类型</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByQueryDotNetType(string queryDotNetType, TransactionManager tm_ = null)
		{
			RepairRemoveByQueryDotNetTypeData(queryDotNetType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByQueryDotNetTypeAsync(string queryDotNetType, TransactionManager tm_ = null)
		{
			RepairRemoveByQueryDotNetTypeData(queryDotNetType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByQueryDotNetTypeData(string queryDotNetType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE " + (queryDotNetType != null ? "`QueryDotNetType` = @QueryDotNetType" : "`QueryDotNetType` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (queryDotNetType != null)
				paras_.Add(Database.CreateInParameter("@QueryDotNetType", queryDotNetType, MySqlDbType.VarChar));
		}
		#endregion // RemoveByQueryDotNetType
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
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE " + (controlType != null ? "`ControlType` = @ControlType" : "`ControlType` IS NULL");
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
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE " + (controlID != null ? "`ControlID` = @ControlID" : "`ControlID` IS NULL");
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
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE " + (fieldLabel != null ? "`FieldLabel` = @FieldLabel" : "`FieldLabel` IS NULL");
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
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE `RowIndex` = @RowIndex";
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
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE `ColumnIndex` = @ColumnIndex";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32));
		}
		#endregion // RemoveByColumnIndex
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
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE " + (jsonData != null ? "`JsonData` = @JsonData" : "`JsonData` IS NULL");
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
			sql_ = @"DELETE FROM `admin_listedit_queryitem` WHERE `RecDate` = @RecDate";
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
		public int Put(Admin_listedit_queryitemEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_listedit_queryitemEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_listedit_queryitemEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `GenID` = @GenID, `QueryBlock` = @QueryBlock, `QueryParameterName` = @QueryParameterName, `QueryDbType` = @QueryDbType, `QueryDotNetType` = @QueryDotNetType, `ControlType` = @ControlType, `ControlID` = @ControlID, `FieldLabel` = @FieldLabel, `RowIndex` = @RowIndex, `ColumnIndex` = @ColumnIndex, `JsonData` = @JsonData WHERE `QueryItemID` = @QueryItemID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", item.GenID, MySqlDbType.Int64),
				Database.CreateInParameter("@QueryBlock", item.QueryBlock != null ? item.QueryBlock : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryParameterName", item.QueryParameterName != null ? item.QueryParameterName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryDbType", item.QueryDbType != null ? item.QueryDbType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryDotNetType", item.QueryDotNetType != null ? item.QueryDotNetType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@ControlType", item.ControlType != null ? item.ControlType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@ControlID", item.ControlID != null ? item.ControlID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@FieldLabel", item.FieldLabel != null ? item.FieldLabel : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RowIndex", item.RowIndex, MySqlDbType.Int32),
				Database.CreateInParameter("@ColumnIndex", item.ColumnIndex, MySqlDbType.Int32),
				Database.CreateInParameter("@JsonData", item.JsonData != null ? item.JsonData : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@QueryItemID_Original", item.HasOriginal ? item.OriginalQueryItemID : item.QueryItemID, MySqlDbType.Int64),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_listedit_queryitemEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_listedit_queryitemEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "queryItemID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long queryItemID, string set_, params object[] values_)
		{
			return Put(set_, "`QueryItemID` = @QueryItemID", ConcatValues(values_, queryItemID));
		}
		public async Task<int> PutByPKAsync(long queryItemID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`QueryItemID` = @QueryItemID", ConcatValues(values_, queryItemID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long queryItemID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`QueryItemID` = @QueryItemID", tm_, ConcatValues(values_, queryItemID));
		}
		public async Task<int> PutByPKAsync(long queryItemID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`QueryItemID` = @QueryItemID", tm_, ConcatValues(values_, queryItemID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long queryItemID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
	        };
			return Put(set_, "`QueryItemID` = @QueryItemID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(long queryItemID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
	        };
			return await PutAsync(set_, "`QueryItemID` = @QueryItemID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutGenID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGenIDByPK(long queryItemID, long genID, TransactionManager tm_ = null)
		{
			RepairPutGenIDByPKData(queryItemID, genID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutGenIDByPKAsync(long queryItemID, long genID, TransactionManager tm_ = null)
		{
			RepairPutGenIDByPKData(queryItemID, genID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutGenIDByPKData(long queryItemID, long genID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `GenID` = @GenID  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `GenID` = @GenID";
			var parameter_ = Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutGenIDAsync(long genID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `GenID` = @GenID";
			var parameter_ = Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutGenID
		#region PutQueryBlock
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "queryBlock">查询语句块</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQueryBlockByPK(long queryItemID, string queryBlock, TransactionManager tm_ = null)
		{
			RepairPutQueryBlockByPKData(queryItemID, queryBlock, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutQueryBlockByPKAsync(long queryItemID, string queryBlock, TransactionManager tm_ = null)
		{
			RepairPutQueryBlockByPKData(queryItemID, queryBlock, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutQueryBlockByPKData(long queryItemID, string queryBlock, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryBlock` = @QueryBlock  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryBlock", queryBlock != null ? queryBlock : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "queryBlock">查询语句块</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQueryBlock(string queryBlock, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryBlock` = @QueryBlock";
			var parameter_ = Database.CreateInParameter("@QueryBlock", queryBlock != null ? queryBlock : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutQueryBlockAsync(string queryBlock, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryBlock` = @QueryBlock";
			var parameter_ = Database.CreateInParameter("@QueryBlock", queryBlock != null ? queryBlock : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutQueryBlock
		#region PutQueryParameterName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "queryParameterName">查询参数名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQueryParameterNameByPK(long queryItemID, string queryParameterName, TransactionManager tm_ = null)
		{
			RepairPutQueryParameterNameByPKData(queryItemID, queryParameterName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutQueryParameterNameByPKAsync(long queryItemID, string queryParameterName, TransactionManager tm_ = null)
		{
			RepairPutQueryParameterNameByPKData(queryItemID, queryParameterName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutQueryParameterNameByPKData(long queryItemID, string queryParameterName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryParameterName` = @QueryParameterName  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryParameterName", queryParameterName != null ? queryParameterName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "queryParameterName">查询参数名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQueryParameterName(string queryParameterName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryParameterName` = @QueryParameterName";
			var parameter_ = Database.CreateInParameter("@QueryParameterName", queryParameterName != null ? queryParameterName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutQueryParameterNameAsync(string queryParameterName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryParameterName` = @QueryParameterName";
			var parameter_ = Database.CreateInParameter("@QueryParameterName", queryParameterName != null ? queryParameterName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutQueryParameterName
		#region PutQueryDbType
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "queryDbType">参数DbType类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQueryDbTypeByPK(long queryItemID, string queryDbType, TransactionManager tm_ = null)
		{
			RepairPutQueryDbTypeByPKData(queryItemID, queryDbType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutQueryDbTypeByPKAsync(long queryItemID, string queryDbType, TransactionManager tm_ = null)
		{
			RepairPutQueryDbTypeByPKData(queryItemID, queryDbType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutQueryDbTypeByPKData(long queryItemID, string queryDbType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryDbType` = @QueryDbType  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryDbType", queryDbType != null ? queryDbType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "queryDbType">参数DbType类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQueryDbType(string queryDbType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryDbType` = @QueryDbType";
			var parameter_ = Database.CreateInParameter("@QueryDbType", queryDbType != null ? queryDbType : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutQueryDbTypeAsync(string queryDbType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryDbType` = @QueryDbType";
			var parameter_ = Database.CreateInParameter("@QueryDbType", queryDbType != null ? queryDbType : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutQueryDbType
		#region PutQueryDotNetType
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "queryDotNetType">参数DotNet类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQueryDotNetTypeByPK(long queryItemID, string queryDotNetType, TransactionManager tm_ = null)
		{
			RepairPutQueryDotNetTypeByPKData(queryItemID, queryDotNetType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutQueryDotNetTypeByPKAsync(long queryItemID, string queryDotNetType, TransactionManager tm_ = null)
		{
			RepairPutQueryDotNetTypeByPKData(queryItemID, queryDotNetType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutQueryDotNetTypeByPKData(long queryItemID, string queryDotNetType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryDotNetType` = @QueryDotNetType  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryDotNetType", queryDotNetType != null ? queryDotNetType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "queryDotNetType">参数DotNet类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQueryDotNetType(string queryDotNetType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryDotNetType` = @QueryDotNetType";
			var parameter_ = Database.CreateInParameter("@QueryDotNetType", queryDotNetType != null ? queryDotNetType : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutQueryDotNetTypeAsync(string queryDotNetType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `QueryDotNetType` = @QueryDotNetType";
			var parameter_ = Database.CreateInParameter("@QueryDotNetType", queryDotNetType != null ? queryDotNetType : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutQueryDotNetType
		#region PutControlType
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutControlTypeByPK(long queryItemID, string controlType, TransactionManager tm_ = null)
		{
			RepairPutControlTypeByPKData(queryItemID, controlType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutControlTypeByPKAsync(long queryItemID, string controlType, TransactionManager tm_ = null)
		{
			RepairPutControlTypeByPKData(queryItemID, controlType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutControlTypeByPKData(long queryItemID, string controlType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `ControlType` = @ControlType  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ControlType", controlType != null ? controlType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `ControlType` = @ControlType";
			var parameter_ = Database.CreateInParameter("@ControlType", controlType != null ? controlType : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutControlTypeAsync(string controlType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `ControlType` = @ControlType";
			var parameter_ = Database.CreateInParameter("@ControlType", controlType != null ? controlType : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutControlType
		#region PutControlID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutControlIDByPK(long queryItemID, string controlID, TransactionManager tm_ = null)
		{
			RepairPutControlIDByPKData(queryItemID, controlID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutControlIDByPKAsync(long queryItemID, string controlID, TransactionManager tm_ = null)
		{
			RepairPutControlIDByPKData(queryItemID, controlID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutControlIDByPKData(long queryItemID, string controlID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `ControlID` = @ControlID  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ControlID", controlID != null ? controlID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `ControlID` = @ControlID";
			var parameter_ = Database.CreateInParameter("@ControlID", controlID != null ? controlID : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutControlIDAsync(string controlID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `ControlID` = @ControlID";
			var parameter_ = Database.CreateInParameter("@ControlID", controlID != null ? controlID : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutControlID
		#region PutFieldLabel
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFieldLabelByPK(long queryItemID, string fieldLabel, TransactionManager tm_ = null)
		{
			RepairPutFieldLabelByPKData(queryItemID, fieldLabel, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutFieldLabelByPKAsync(long queryItemID, string fieldLabel, TransactionManager tm_ = null)
		{
			RepairPutFieldLabelByPKData(queryItemID, fieldLabel, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutFieldLabelByPKData(long queryItemID, string fieldLabel, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `FieldLabel` = @FieldLabel  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@FieldLabel", fieldLabel != null ? fieldLabel : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `FieldLabel` = @FieldLabel";
			var parameter_ = Database.CreateInParameter("@FieldLabel", fieldLabel != null ? fieldLabel : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutFieldLabelAsync(string fieldLabel, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `FieldLabel` = @FieldLabel";
			var parameter_ = Database.CreateInParameter("@FieldLabel", fieldLabel != null ? fieldLabel : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutFieldLabel
		#region PutRowIndex
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRowIndexByPK(long queryItemID, int rowIndex, TransactionManager tm_ = null)
		{
			RepairPutRowIndexByPKData(queryItemID, rowIndex, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRowIndexByPKAsync(long queryItemID, int rowIndex, TransactionManager tm_ = null)
		{
			RepairPutRowIndexByPKData(queryItemID, rowIndex, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRowIndexByPKData(long queryItemID, int rowIndex, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `RowIndex` = @RowIndex  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RowIndex", rowIndex, MySqlDbType.Int32),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `RowIndex` = @RowIndex";
			var parameter_ = Database.CreateInParameter("@RowIndex", rowIndex, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRowIndexAsync(int rowIndex, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `RowIndex` = @RowIndex";
			var parameter_ = Database.CreateInParameter("@RowIndex", rowIndex, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRowIndex
		#region PutColumnIndex
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutColumnIndexByPK(long queryItemID, int columnIndex, TransactionManager tm_ = null)
		{
			RepairPutColumnIndexByPKData(queryItemID, columnIndex, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutColumnIndexByPKAsync(long queryItemID, int columnIndex, TransactionManager tm_ = null)
		{
			RepairPutColumnIndexByPKData(queryItemID, columnIndex, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutColumnIndexByPKData(long queryItemID, int columnIndex, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `ColumnIndex` = @ColumnIndex  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `ColumnIndex` = @ColumnIndex";
			var parameter_ = Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutColumnIndexAsync(int columnIndex, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `ColumnIndex` = @ColumnIndex";
			var parameter_ = Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutColumnIndex
		#region PutJsonData
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutJsonDataByPK(long queryItemID, string jsonData, TransactionManager tm_ = null)
		{
			RepairPutJsonDataByPKData(queryItemID, jsonData, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutJsonDataByPKAsync(long queryItemID, string jsonData, TransactionManager tm_ = null)
		{
			RepairPutJsonDataByPKData(queryItemID, jsonData, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutJsonDataByPKData(long queryItemID, string jsonData, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `JsonData` = @JsonData  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@JsonData", jsonData != null ? jsonData : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `JsonData` = @JsonData";
			var parameter_ = Database.CreateInParameter("@JsonData", jsonData != null ? jsonData : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutJsonDataAsync(string jsonData, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `JsonData` = @JsonData";
			var parameter_ = Database.CreateInParameter("@JsonData", jsonData != null ? jsonData : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutJsonData
		#region PutRecDate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRecDateByPK(long queryItemID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(queryItemID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRecDateByPKAsync(long queryItemID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(queryItemID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRecDateByPKData(long queryItemID, DateTime recDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_queryitem` SET `RecDate` = @RecDate  WHERE `QueryItemID` = @QueryItemID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `RecDate` = @RecDate";
			var parameter_ = Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRecDateAsync(DateTime recDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_queryitem` SET `RecDate` = @RecDate";
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
		public bool Set(Admin_listedit_queryitemEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.QueryItemID) == null)
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
		public async Task<bool> SetAsync(Admin_listedit_queryitemEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.QueryItemID) == null)
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
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_listedit_queryitemEO GetByPK(long queryItemID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(queryItemID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<Admin_listedit_queryitemEO> GetByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(queryItemID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		private void RepairGetByPKData(long queryItemID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`QueryItemID` = @QueryItemID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 GenID（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public long GetGenIDByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (long)GetScalar("`GenID`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<long> GetGenIDByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (long)await GetScalarAsync("`GenID`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 QueryBlock（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetQueryBlockByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`QueryBlock`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<string> GetQueryBlockByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`QueryBlock`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 QueryParameterName（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetQueryParameterNameByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`QueryParameterName`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<string> GetQueryParameterNameByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`QueryParameterName`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 QueryDbType（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetQueryDbTypeByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`QueryDbType`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<string> GetQueryDbTypeByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`QueryDbType`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 QueryDotNetType（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetQueryDotNetTypeByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`QueryDotNetType`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<string> GetQueryDotNetTypeByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`QueryDotNetType`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ControlType（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetControlTypeByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`ControlType`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<string> GetControlTypeByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`ControlType`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ControlID（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetControlIDByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`ControlID`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<string> GetControlIDByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`ControlID`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 FieldLabel（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetFieldLabelByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`FieldLabel`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<string> GetFieldLabelByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`FieldLabel`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RowIndex（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetRowIndexByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`RowIndex`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<int> GetRowIndexByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`RowIndex`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ColumnIndex（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetColumnIndexByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`ColumnIndex`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<int> GetColumnIndexByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`ColumnIndex`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 JsonData（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetJsonDataByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`JsonData`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<string> GetJsonDataByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`JsonData`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RecDate（字段）
		/// </summary>
		/// /// <param name = "queryItemID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public DateTime GetRecDateByPK(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (DateTime)GetScalar("`RecDate`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		public async Task<DateTime> GetRecDateByPKAsync(long queryItemID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryItemID", queryItemID, MySqlDbType.Int64),
			};
			return (DateTime)await GetScalarAsync("`RecDate`", "`QueryItemID` = @QueryItemID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByGenID
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByGenID(long genID)
		{
			return GetByGenID(genID, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByGenIDAsync(long genID)
		{
			return await GetByGenIDAsync(genID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByGenID(long genID, TransactionManager tm_)
		{
			return GetByGenID(genID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByGenIDAsync(long genID, TransactionManager tm_)
		{
			return await GetByGenIDAsync(genID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByGenID(long genID, int top_)
		{
			return GetByGenID(genID, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByGenIDAsync(long genID, int top_)
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
		public List<Admin_listedit_queryitemEO> GetByGenID(long genID, int top_, TransactionManager tm_)
		{
			return GetByGenID(genID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByGenIDAsync(long genID, int top_, TransactionManager tm_)
		{
			return await GetByGenIDAsync(genID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">Gen编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByGenID(long genID, string sort_)
		{
			return GetByGenID(genID, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByGenIDAsync(long genID, string sort_)
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
		public List<Admin_listedit_queryitemEO> GetByGenID(long genID, string sort_, TransactionManager tm_)
		{
			return GetByGenID(genID, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByGenIDAsync(long genID, string sort_, TransactionManager tm_)
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
		public List<Admin_listedit_queryitemEO> GetByGenID(long genID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`GenID` = @GenID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByGenIDAsync(long genID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`GenID` = @GenID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByGenID
		#region GetByQueryBlock
		
		/// <summary>
		/// 按 QueryBlock（字段） 查询
		/// </summary>
		/// /// <param name = "queryBlock">查询语句块</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryBlock(string queryBlock)
		{
			return GetByQueryBlock(queryBlock, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryBlockAsync(string queryBlock)
		{
			return await GetByQueryBlockAsync(queryBlock, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QueryBlock（字段） 查询
		/// </summary>
		/// /// <param name = "queryBlock">查询语句块</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryBlock(string queryBlock, TransactionManager tm_)
		{
			return GetByQueryBlock(queryBlock, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryBlockAsync(string queryBlock, TransactionManager tm_)
		{
			return await GetByQueryBlockAsync(queryBlock, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QueryBlock（字段） 查询
		/// </summary>
		/// /// <param name = "queryBlock">查询语句块</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryBlock(string queryBlock, int top_)
		{
			return GetByQueryBlock(queryBlock, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryBlockAsync(string queryBlock, int top_)
		{
			return await GetByQueryBlockAsync(queryBlock, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QueryBlock（字段） 查询
		/// </summary>
		/// /// <param name = "queryBlock">查询语句块</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryBlock(string queryBlock, int top_, TransactionManager tm_)
		{
			return GetByQueryBlock(queryBlock, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryBlockAsync(string queryBlock, int top_, TransactionManager tm_)
		{
			return await GetByQueryBlockAsync(queryBlock, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QueryBlock（字段） 查询
		/// </summary>
		/// /// <param name = "queryBlock">查询语句块</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryBlock(string queryBlock, string sort_)
		{
			return GetByQueryBlock(queryBlock, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryBlockAsync(string queryBlock, string sort_)
		{
			return await GetByQueryBlockAsync(queryBlock, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 QueryBlock（字段） 查询
		/// </summary>
		/// /// <param name = "queryBlock">查询语句块</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryBlock(string queryBlock, string sort_, TransactionManager tm_)
		{
			return GetByQueryBlock(queryBlock, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryBlockAsync(string queryBlock, string sort_, TransactionManager tm_)
		{
			return await GetByQueryBlockAsync(queryBlock, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 QueryBlock（字段） 查询
		/// </summary>
		/// /// <param name = "queryBlock">查询语句块</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryBlock(string queryBlock, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(queryBlock != null ? "`QueryBlock` = @QueryBlock" : "`QueryBlock` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (queryBlock != null)
				paras_.Add(Database.CreateInParameter("@QueryBlock", queryBlock, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryBlockAsync(string queryBlock, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(queryBlock != null ? "`QueryBlock` = @QueryBlock" : "`QueryBlock` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (queryBlock != null)
				paras_.Add(Database.CreateInParameter("@QueryBlock", queryBlock, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByQueryBlock
		#region GetByQueryParameterName
		
		/// <summary>
		/// 按 QueryParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "queryParameterName">查询参数名</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryParameterName(string queryParameterName)
		{
			return GetByQueryParameterName(queryParameterName, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryParameterNameAsync(string queryParameterName)
		{
			return await GetByQueryParameterNameAsync(queryParameterName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QueryParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "queryParameterName">查询参数名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryParameterName(string queryParameterName, TransactionManager tm_)
		{
			return GetByQueryParameterName(queryParameterName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryParameterNameAsync(string queryParameterName, TransactionManager tm_)
		{
			return await GetByQueryParameterNameAsync(queryParameterName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QueryParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "queryParameterName">查询参数名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryParameterName(string queryParameterName, int top_)
		{
			return GetByQueryParameterName(queryParameterName, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryParameterNameAsync(string queryParameterName, int top_)
		{
			return await GetByQueryParameterNameAsync(queryParameterName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QueryParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "queryParameterName">查询参数名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryParameterName(string queryParameterName, int top_, TransactionManager tm_)
		{
			return GetByQueryParameterName(queryParameterName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryParameterNameAsync(string queryParameterName, int top_, TransactionManager tm_)
		{
			return await GetByQueryParameterNameAsync(queryParameterName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QueryParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "queryParameterName">查询参数名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryParameterName(string queryParameterName, string sort_)
		{
			return GetByQueryParameterName(queryParameterName, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryParameterNameAsync(string queryParameterName, string sort_)
		{
			return await GetByQueryParameterNameAsync(queryParameterName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 QueryParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "queryParameterName">查询参数名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryParameterName(string queryParameterName, string sort_, TransactionManager tm_)
		{
			return GetByQueryParameterName(queryParameterName, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryParameterNameAsync(string queryParameterName, string sort_, TransactionManager tm_)
		{
			return await GetByQueryParameterNameAsync(queryParameterName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 QueryParameterName（字段） 查询
		/// </summary>
		/// /// <param name = "queryParameterName">查询参数名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryParameterName(string queryParameterName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(queryParameterName != null ? "`QueryParameterName` = @QueryParameterName" : "`QueryParameterName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (queryParameterName != null)
				paras_.Add(Database.CreateInParameter("@QueryParameterName", queryParameterName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryParameterNameAsync(string queryParameterName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(queryParameterName != null ? "`QueryParameterName` = @QueryParameterName" : "`QueryParameterName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (queryParameterName != null)
				paras_.Add(Database.CreateInParameter("@QueryParameterName", queryParameterName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByQueryParameterName
		#region GetByQueryDbType
		
		/// <summary>
		/// 按 QueryDbType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDbType">参数DbType类型</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDbType(string queryDbType)
		{
			return GetByQueryDbType(queryDbType, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDbTypeAsync(string queryDbType)
		{
			return await GetByQueryDbTypeAsync(queryDbType, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QueryDbType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDbType">参数DbType类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDbType(string queryDbType, TransactionManager tm_)
		{
			return GetByQueryDbType(queryDbType, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDbTypeAsync(string queryDbType, TransactionManager tm_)
		{
			return await GetByQueryDbTypeAsync(queryDbType, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QueryDbType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDbType">参数DbType类型</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDbType(string queryDbType, int top_)
		{
			return GetByQueryDbType(queryDbType, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDbTypeAsync(string queryDbType, int top_)
		{
			return await GetByQueryDbTypeAsync(queryDbType, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QueryDbType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDbType">参数DbType类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDbType(string queryDbType, int top_, TransactionManager tm_)
		{
			return GetByQueryDbType(queryDbType, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDbTypeAsync(string queryDbType, int top_, TransactionManager tm_)
		{
			return await GetByQueryDbTypeAsync(queryDbType, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QueryDbType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDbType">参数DbType类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDbType(string queryDbType, string sort_)
		{
			return GetByQueryDbType(queryDbType, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDbTypeAsync(string queryDbType, string sort_)
		{
			return await GetByQueryDbTypeAsync(queryDbType, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 QueryDbType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDbType">参数DbType类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDbType(string queryDbType, string sort_, TransactionManager tm_)
		{
			return GetByQueryDbType(queryDbType, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDbTypeAsync(string queryDbType, string sort_, TransactionManager tm_)
		{
			return await GetByQueryDbTypeAsync(queryDbType, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 QueryDbType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDbType">参数DbType类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDbType(string queryDbType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(queryDbType != null ? "`QueryDbType` = @QueryDbType" : "`QueryDbType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (queryDbType != null)
				paras_.Add(Database.CreateInParameter("@QueryDbType", queryDbType, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDbTypeAsync(string queryDbType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(queryDbType != null ? "`QueryDbType` = @QueryDbType" : "`QueryDbType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (queryDbType != null)
				paras_.Add(Database.CreateInParameter("@QueryDbType", queryDbType, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByQueryDbType
		#region GetByQueryDotNetType
		
		/// <summary>
		/// 按 QueryDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDotNetType">参数DotNet类型</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDotNetType(string queryDotNetType)
		{
			return GetByQueryDotNetType(queryDotNetType, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDotNetTypeAsync(string queryDotNetType)
		{
			return await GetByQueryDotNetTypeAsync(queryDotNetType, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QueryDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDotNetType">参数DotNet类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDotNetType(string queryDotNetType, TransactionManager tm_)
		{
			return GetByQueryDotNetType(queryDotNetType, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDotNetTypeAsync(string queryDotNetType, TransactionManager tm_)
		{
			return await GetByQueryDotNetTypeAsync(queryDotNetType, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QueryDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDotNetType">参数DotNet类型</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDotNetType(string queryDotNetType, int top_)
		{
			return GetByQueryDotNetType(queryDotNetType, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDotNetTypeAsync(string queryDotNetType, int top_)
		{
			return await GetByQueryDotNetTypeAsync(queryDotNetType, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QueryDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDotNetType">参数DotNet类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDotNetType(string queryDotNetType, int top_, TransactionManager tm_)
		{
			return GetByQueryDotNetType(queryDotNetType, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDotNetTypeAsync(string queryDotNetType, int top_, TransactionManager tm_)
		{
			return await GetByQueryDotNetTypeAsync(queryDotNetType, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QueryDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDotNetType">参数DotNet类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDotNetType(string queryDotNetType, string sort_)
		{
			return GetByQueryDotNetType(queryDotNetType, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDotNetTypeAsync(string queryDotNetType, string sort_)
		{
			return await GetByQueryDotNetTypeAsync(queryDotNetType, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 QueryDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDotNetType">参数DotNet类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDotNetType(string queryDotNetType, string sort_, TransactionManager tm_)
		{
			return GetByQueryDotNetType(queryDotNetType, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDotNetTypeAsync(string queryDotNetType, string sort_, TransactionManager tm_)
		{
			return await GetByQueryDotNetTypeAsync(queryDotNetType, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 QueryDotNetType（字段） 查询
		/// </summary>
		/// /// <param name = "queryDotNetType">参数DotNet类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByQueryDotNetType(string queryDotNetType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(queryDotNetType != null ? "`QueryDotNetType` = @QueryDotNetType" : "`QueryDotNetType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (queryDotNetType != null)
				paras_.Add(Database.CreateInParameter("@QueryDotNetType", queryDotNetType, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByQueryDotNetTypeAsync(string queryDotNetType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(queryDotNetType != null ? "`QueryDotNetType` = @QueryDotNetType" : "`QueryDotNetType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (queryDotNetType != null)
				paras_.Add(Database.CreateInParameter("@QueryDotNetType", queryDotNetType, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByQueryDotNetType
		#region GetByControlType
		
		/// <summary>
		/// 按 ControlType（字段） 查询
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByControlType(string controlType)
		{
			return GetByControlType(controlType, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlTypeAsync(string controlType)
		{
			return await GetByControlTypeAsync(controlType, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ControlType（字段） 查询
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByControlType(string controlType, TransactionManager tm_)
		{
			return GetByControlType(controlType, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlTypeAsync(string controlType, TransactionManager tm_)
		{
			return await GetByControlTypeAsync(controlType, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ControlType（字段） 查询
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByControlType(string controlType, int top_)
		{
			return GetByControlType(controlType, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlTypeAsync(string controlType, int top_)
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
		public List<Admin_listedit_queryitemEO> GetByControlType(string controlType, int top_, TransactionManager tm_)
		{
			return GetByControlType(controlType, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlTypeAsync(string controlType, int top_, TransactionManager tm_)
		{
			return await GetByControlTypeAsync(controlType, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ControlType（字段） 查询
		/// </summary>
		/// /// <param name = "controlType">控件类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByControlType(string controlType, string sort_)
		{
			return GetByControlType(controlType, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlTypeAsync(string controlType, string sort_)
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
		public List<Admin_listedit_queryitemEO> GetByControlType(string controlType, string sort_, TransactionManager tm_)
		{
			return GetByControlType(controlType, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlTypeAsync(string controlType, string sort_, TransactionManager tm_)
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
		public List<Admin_listedit_queryitemEO> GetByControlType(string controlType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(controlType != null ? "`ControlType` = @ControlType" : "`ControlType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (controlType != null)
				paras_.Add(Database.CreateInParameter("@ControlType", controlType, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlTypeAsync(string controlType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(controlType != null ? "`ControlType` = @ControlType" : "`ControlType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (controlType != null)
				paras_.Add(Database.CreateInParameter("@ControlType", controlType, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByControlType
		#region GetByControlID
		
		/// <summary>
		/// 按 ControlID（字段） 查询
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByControlID(string controlID)
		{
			return GetByControlID(controlID, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlIDAsync(string controlID)
		{
			return await GetByControlIDAsync(controlID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ControlID（字段） 查询
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByControlID(string controlID, TransactionManager tm_)
		{
			return GetByControlID(controlID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlIDAsync(string controlID, TransactionManager tm_)
		{
			return await GetByControlIDAsync(controlID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ControlID（字段） 查询
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByControlID(string controlID, int top_)
		{
			return GetByControlID(controlID, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlIDAsync(string controlID, int top_)
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
		public List<Admin_listedit_queryitemEO> GetByControlID(string controlID, int top_, TransactionManager tm_)
		{
			return GetByControlID(controlID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlIDAsync(string controlID, int top_, TransactionManager tm_)
		{
			return await GetByControlIDAsync(controlID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ControlID（字段） 查询
		/// </summary>
		/// /// <param name = "controlID">控件ID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByControlID(string controlID, string sort_)
		{
			return GetByControlID(controlID, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlIDAsync(string controlID, string sort_)
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
		public List<Admin_listedit_queryitemEO> GetByControlID(string controlID, string sort_, TransactionManager tm_)
		{
			return GetByControlID(controlID, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlIDAsync(string controlID, string sort_, TransactionManager tm_)
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
		public List<Admin_listedit_queryitemEO> GetByControlID(string controlID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(controlID != null ? "`ControlID` = @ControlID" : "`ControlID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (controlID != null)
				paras_.Add(Database.CreateInParameter("@ControlID", controlID, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByControlIDAsync(string controlID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(controlID != null ? "`ControlID` = @ControlID" : "`ControlID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (controlID != null)
				paras_.Add(Database.CreateInParameter("@ControlID", controlID, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByControlID
		#region GetByFieldLabel
		
		/// <summary>
		/// 按 FieldLabel（字段） 查询
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByFieldLabel(string fieldLabel)
		{
			return GetByFieldLabel(fieldLabel, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByFieldLabelAsync(string fieldLabel)
		{
			return await GetByFieldLabelAsync(fieldLabel, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 FieldLabel（字段） 查询
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByFieldLabel(string fieldLabel, TransactionManager tm_)
		{
			return GetByFieldLabel(fieldLabel, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByFieldLabelAsync(string fieldLabel, TransactionManager tm_)
		{
			return await GetByFieldLabelAsync(fieldLabel, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 FieldLabel（字段） 查询
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByFieldLabel(string fieldLabel, int top_)
		{
			return GetByFieldLabel(fieldLabel, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByFieldLabelAsync(string fieldLabel, int top_)
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
		public List<Admin_listedit_queryitemEO> GetByFieldLabel(string fieldLabel, int top_, TransactionManager tm_)
		{
			return GetByFieldLabel(fieldLabel, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByFieldLabelAsync(string fieldLabel, int top_, TransactionManager tm_)
		{
			return await GetByFieldLabelAsync(fieldLabel, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 FieldLabel（字段） 查询
		/// </summary>
		/// /// <param name = "fieldLabel">Label</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByFieldLabel(string fieldLabel, string sort_)
		{
			return GetByFieldLabel(fieldLabel, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByFieldLabelAsync(string fieldLabel, string sort_)
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
		public List<Admin_listedit_queryitemEO> GetByFieldLabel(string fieldLabel, string sort_, TransactionManager tm_)
		{
			return GetByFieldLabel(fieldLabel, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByFieldLabelAsync(string fieldLabel, string sort_, TransactionManager tm_)
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
		public List<Admin_listedit_queryitemEO> GetByFieldLabel(string fieldLabel, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(fieldLabel != null ? "`FieldLabel` = @FieldLabel" : "`FieldLabel` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (fieldLabel != null)
				paras_.Add(Database.CreateInParameter("@FieldLabel", fieldLabel, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByFieldLabelAsync(string fieldLabel, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(fieldLabel != null ? "`FieldLabel` = @FieldLabel" : "`FieldLabel` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (fieldLabel != null)
				paras_.Add(Database.CreateInParameter("@FieldLabel", fieldLabel, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByFieldLabel
		#region GetByRowIndex
		
		/// <summary>
		/// 按 RowIndex（字段） 查询
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByRowIndex(int rowIndex)
		{
			return GetByRowIndex(rowIndex, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRowIndexAsync(int rowIndex)
		{
			return await GetByRowIndexAsync(rowIndex, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RowIndex（字段） 查询
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByRowIndex(int rowIndex, TransactionManager tm_)
		{
			return GetByRowIndex(rowIndex, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRowIndexAsync(int rowIndex, TransactionManager tm_)
		{
			return await GetByRowIndexAsync(rowIndex, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RowIndex（字段） 查询
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByRowIndex(int rowIndex, int top_)
		{
			return GetByRowIndex(rowIndex, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRowIndexAsync(int rowIndex, int top_)
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
		public List<Admin_listedit_queryitemEO> GetByRowIndex(int rowIndex, int top_, TransactionManager tm_)
		{
			return GetByRowIndex(rowIndex, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRowIndexAsync(int rowIndex, int top_, TransactionManager tm_)
		{
			return await GetByRowIndexAsync(rowIndex, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RowIndex（字段） 查询
		/// </summary>
		/// /// <param name = "rowIndex">所在行</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByRowIndex(int rowIndex, string sort_)
		{
			return GetByRowIndex(rowIndex, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRowIndexAsync(int rowIndex, string sort_)
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
		public List<Admin_listedit_queryitemEO> GetByRowIndex(int rowIndex, string sort_, TransactionManager tm_)
		{
			return GetByRowIndex(rowIndex, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRowIndexAsync(int rowIndex, string sort_, TransactionManager tm_)
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
		public List<Admin_listedit_queryitemEO> GetByRowIndex(int rowIndex, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RowIndex` = @RowIndex", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RowIndex", rowIndex, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRowIndexAsync(int rowIndex, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RowIndex` = @RowIndex", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RowIndex", rowIndex, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByRowIndex
		#region GetByColumnIndex
		
		/// <summary>
		/// 按 ColumnIndex（字段） 查询
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByColumnIndex(int columnIndex)
		{
			return GetByColumnIndex(columnIndex, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByColumnIndexAsync(int columnIndex)
		{
			return await GetByColumnIndexAsync(columnIndex, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ColumnIndex（字段） 查询
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByColumnIndex(int columnIndex, TransactionManager tm_)
		{
			return GetByColumnIndex(columnIndex, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByColumnIndexAsync(int columnIndex, TransactionManager tm_)
		{
			return await GetByColumnIndexAsync(columnIndex, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ColumnIndex（字段） 查询
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByColumnIndex(int columnIndex, int top_)
		{
			return GetByColumnIndex(columnIndex, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByColumnIndexAsync(int columnIndex, int top_)
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
		public List<Admin_listedit_queryitemEO> GetByColumnIndex(int columnIndex, int top_, TransactionManager tm_)
		{
			return GetByColumnIndex(columnIndex, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByColumnIndexAsync(int columnIndex, int top_, TransactionManager tm_)
		{
			return await GetByColumnIndexAsync(columnIndex, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ColumnIndex（字段） 查询
		/// </summary>
		/// /// <param name = "columnIndex">所在列</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByColumnIndex(int columnIndex, string sort_)
		{
			return GetByColumnIndex(columnIndex, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByColumnIndexAsync(int columnIndex, string sort_)
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
		public List<Admin_listedit_queryitemEO> GetByColumnIndex(int columnIndex, string sort_, TransactionManager tm_)
		{
			return GetByColumnIndex(columnIndex, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByColumnIndexAsync(int columnIndex, string sort_, TransactionManager tm_)
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
		public List<Admin_listedit_queryitemEO> GetByColumnIndex(int columnIndex, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`ColumnIndex` = @ColumnIndex", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByColumnIndexAsync(int columnIndex, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`ColumnIndex` = @ColumnIndex", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@ColumnIndex", columnIndex, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByColumnIndex
		#region GetByJsonData
		
		/// <summary>
		/// 按 JsonData（字段） 查询
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByJsonData(string jsonData)
		{
			return GetByJsonData(jsonData, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByJsonDataAsync(string jsonData)
		{
			return await GetByJsonDataAsync(jsonData, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 JsonData（字段） 查询
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByJsonData(string jsonData, TransactionManager tm_)
		{
			return GetByJsonData(jsonData, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByJsonDataAsync(string jsonData, TransactionManager tm_)
		{
			return await GetByJsonDataAsync(jsonData, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 JsonData（字段） 查询
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByJsonData(string jsonData, int top_)
		{
			return GetByJsonData(jsonData, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByJsonDataAsync(string jsonData, int top_)
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
		public List<Admin_listedit_queryitemEO> GetByJsonData(string jsonData, int top_, TransactionManager tm_)
		{
			return GetByJsonData(jsonData, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByJsonDataAsync(string jsonData, int top_, TransactionManager tm_)
		{
			return await GetByJsonDataAsync(jsonData, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 JsonData（字段） 查询
		/// </summary>
		/// /// <param name = "jsonData">Json系列化</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByJsonData(string jsonData, string sort_)
		{
			return GetByJsonData(jsonData, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByJsonDataAsync(string jsonData, string sort_)
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
		public List<Admin_listedit_queryitemEO> GetByJsonData(string jsonData, string sort_, TransactionManager tm_)
		{
			return GetByJsonData(jsonData, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByJsonDataAsync(string jsonData, string sort_, TransactionManager tm_)
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
		public List<Admin_listedit_queryitemEO> GetByJsonData(string jsonData, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(jsonData != null ? "`JsonData` = @JsonData" : "`JsonData` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (jsonData != null)
				paras_.Add(Database.CreateInParameter("@JsonData", jsonData, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByJsonDataAsync(string jsonData, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(jsonData != null ? "`JsonData` = @JsonData" : "`JsonData` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (jsonData != null)
				paras_.Add(Database.CreateInParameter("@JsonData", jsonData, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByJsonData
		#region GetByRecDate
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByRecDate(DateTime recDate)
		{
			return GetByRecDate(recDate, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRecDateAsync(DateTime recDate)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByRecDate(DateTime recDate, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRecDateAsync(DateTime recDate, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByRecDate(DateTime recDate, int top_)
		{
			return GetByRecDate(recDate, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRecDateAsync(DateTime recDate, int top_)
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
		public List<Admin_listedit_queryitemEO> GetByRecDate(DateTime recDate, int top_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRecDateAsync(DateTime recDate, int top_, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_queryitemEO> GetByRecDate(DateTime recDate, string sort_)
		{
			return GetByRecDate(recDate, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRecDateAsync(DateTime recDate, string sort_)
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
		public List<Admin_listedit_queryitemEO> GetByRecDate(DateTime recDate, string sort_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRecDateAsync(DateTime recDate, string sort_, TransactionManager tm_)
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
		public List<Admin_listedit_queryitemEO> GetByRecDate(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_queryitemEO>> GetByRecDateAsync(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_queryitemEO.MapDataReader);
		}
		#endregion // GetByRecDate
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
