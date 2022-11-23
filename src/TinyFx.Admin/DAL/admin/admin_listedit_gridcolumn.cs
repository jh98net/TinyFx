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
	/// 查询grid列定义
	/// 【表 admin_listedit_gridcolumn 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_listedit_gridcolumnEO : IRowMapper<Admin_listedit_gridcolumnEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_listedit_gridcolumnEO()
		{
			this.OrderNum = 0;
			this.IsPrimaryKey = false;
			this.Locked = false;
			this.Visible = true;
			this.RecDate = DateTime.Now;
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
		private long _originalGridColumnID;
		/// <summary>
		/// 【数据库中的原始主键 GridColumnID 值的副本，用于主键值更新】
		/// </summary>
		public long OriginalGridColumnID
		{
			get { return _originalGridColumnID; }
			set { HasOriginal = true; _originalGridColumnID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "GridColumnID", GridColumnID }, };
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
		public long GridColumnID { get; set; }
		/// <summary>
		/// 编码
		/// 【字段 bigint】
		/// </summary>
		[DataMember(Order = 2)]
		public long GenID { get; set; }
		/// <summary>
		/// 排序
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 3)]
		public int OrderNum { get; set; }
		/// <summary>
		/// 列类型
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 4)]
		public string ColumnType { get; set; }
		/// <summary>
		/// 是否主键
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 5)]
		public bool IsPrimaryKey { get; set; }
		/// <summary>
		/// 数据字段名
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 6)]
		public string FieldName { get; set; }
		/// <summary>
		/// 数据表列名
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 7)]
		public string ColumnName { get; set; }
		/// <summary>
		/// 显示列名
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 8)]
		public string Text { get; set; }
		/// <summary>
		/// 对齐方式
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 9)]
		public string Align { get; set; }
		/// <summary>
		/// 列宽
		/// 【字段 varchar(10)】
		/// </summary>
		[DataMember(Order = 10)]
		public string Width { get; set; }
		/// <summary>
		/// 列宽flex
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 11)]
		public int? Flex { get; set; }
		/// <summary>
		/// 是否锁定
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 12)]
		public bool Locked { get; set; }
		/// <summary>
		/// 是否可见
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 13)]
		public bool Visible { get; set; }
		/// <summary>
		/// 显示格式
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 14)]
		public string Format { get; set; }
		/// <summary>
		/// BooleanColumn true文本
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 15)]
		public string TrueText { get; set; }
		/// <summary>
		/// BooleanColumn false文本
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 16)]
		public string FalseText { get; set; }
		/// <summary>
		/// 过滤类型
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 17)]
		public string FilterType { get; set; }
		/// <summary>
		/// 输出显示函数名
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 18)]
		public string RenderFn { get; set; }
		/// <summary>
		/// 输出显示函数内容
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 19)]
		public string RenderFnContent { get; set; }
		/// <summary>
		/// 输出显示handler
		/// 【字段 varchar(1000)】
		/// </summary>
		[DataMember(Order = 20)]
		public string RenderHandler { get; set; }
		/// <summary>
		/// 记录日期
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 21)]
		public DateTime RecDate { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_listedit_gridcolumnEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_listedit_gridcolumnEO MapDataReader(IDataReader reader)
		{
		    Admin_listedit_gridcolumnEO ret = new Admin_listedit_gridcolumnEO();
			ret.GridColumnID = reader.ToInt64("GridColumnID");
			ret.OriginalGridColumnID = ret.GridColumnID;
			ret.GenID = reader.ToInt64("GenID");
			ret.OrderNum = reader.ToInt32("OrderNum");
			ret.ColumnType = reader.ToString("ColumnType");
			ret.IsPrimaryKey = reader.ToBoolean("IsPrimaryKey");
			ret.FieldName = reader.ToString("FieldName");
			ret.ColumnName = reader.ToString("ColumnName");
			ret.Text = reader.ToString("Text");
			ret.Align = reader.ToString("Align");
			ret.Width = reader.ToString("Width");
			ret.Flex = reader.ToInt32N("Flex");
			ret.Locked = reader.ToBoolean("Locked");
			ret.Visible = reader.ToBoolean("Visible");
			ret.Format = reader.ToString("Format");
			ret.TrueText = reader.ToString("TrueText");
			ret.FalseText = reader.ToString("FalseText");
			ret.FilterType = reader.ToString("FilterType");
			ret.RenderFn = reader.ToString("RenderFn");
			ret.RenderFnContent = reader.ToString("RenderFnContent");
			ret.RenderHandler = reader.ToString("RenderHandler");
			ret.RecDate = reader.ToDateTime("RecDate");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 查询grid列定义
	/// 【表 admin_listedit_gridcolumn 的操作类】
	/// </summary>
	public class Admin_listedit_gridcolumnMO : MySqlTableMO<Admin_listedit_gridcolumnEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_listedit_gridcolumn`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_listedit_gridcolumnMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_listedit_gridcolumnMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_listedit_gridcolumnMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_listedit_gridcolumnEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			item.GridColumnID = Database.ExecSqlScalar<long>(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_listedit_gridcolumnEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			item.GridColumnID = await Database.ExecSqlScalarAsync<long>(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_listedit_gridcolumnEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_listedit_gridcolumn` (`GenID`, `OrderNum`, `ColumnType`, `IsPrimaryKey`, `FieldName`, `ColumnName`, `Text`, `Align`, `Width`, `Flex`, `Locked`, `Visible`, `Format`, `TrueText`, `FalseText`, `FilterType`, `RenderFn`, `RenderFnContent`, `RenderHandler`, `RecDate`) VALUE (@GenID, @OrderNum, @ColumnType, @IsPrimaryKey, @FieldName, @ColumnName, @Text, @Align, @Width, @Flex, @Locked, @Visible, @Format, @TrueText, @FalseText, @FilterType, @RenderFn, @RenderFnContent, @RenderHandler, @RecDate);SELECT LAST_INSERT_ID();";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", item.GenID, MySqlDbType.Int64),
				Database.CreateInParameter("@OrderNum", item.OrderNum, MySqlDbType.Int32),
				Database.CreateInParameter("@ColumnType", item.ColumnType != null ? item.ColumnType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@IsPrimaryKey", item.IsPrimaryKey, MySqlDbType.Byte),
				Database.CreateInParameter("@FieldName", item.FieldName != null ? item.FieldName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@ColumnName", item.ColumnName != null ? item.ColumnName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Text", item.Text != null ? item.Text : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Align", item.Align != null ? item.Align : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Width", item.Width != null ? item.Width : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Flex", item.Flex.HasValue ? item.Flex.Value : (object)DBNull.Value, MySqlDbType.Int32),
				Database.CreateInParameter("@Locked", item.Locked, MySqlDbType.Byte),
				Database.CreateInParameter("@Visible", item.Visible, MySqlDbType.Byte),
				Database.CreateInParameter("@Format", item.Format != null ? item.Format : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@TrueText", item.TrueText != null ? item.TrueText : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@FalseText", item.FalseText != null ? item.FalseText : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@FilterType", item.FilterType != null ? item.FilterType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RenderFn", item.RenderFn != null ? item.RenderFn : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RenderFnContent", item.RenderFnContent != null ? item.RenderFnContent : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@RenderHandler", item.RenderHandler != null ? item.RenderHandler : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RecDate", item.RecDate, MySqlDbType.DateTime),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(gridColumnID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(gridColumnID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(long gridColumnID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_listedit_gridcolumnEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.GridColumnID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_listedit_gridcolumnEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.GridColumnID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByGenID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "genID">编码</param>
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
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64));
		}
		#endregion // RemoveByGenID
		#region RemoveByOrderNum
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "orderNum">排序</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByOrderNum(int orderNum, TransactionManager tm_ = null)
		{
			RepairRemoveByOrderNumData(orderNum, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByOrderNumAsync(int orderNum, TransactionManager tm_ = null)
		{
			RepairRemoveByOrderNumData(orderNum, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByOrderNumData(int orderNum, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE `OrderNum` = @OrderNum";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32));
		}
		#endregion // RemoveByOrderNum
		#region RemoveByColumnType
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "columnType">列类型</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByColumnType(string columnType, TransactionManager tm_ = null)
		{
			RepairRemoveByColumnTypeData(columnType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByColumnTypeAsync(string columnType, TransactionManager tm_ = null)
		{
			RepairRemoveByColumnTypeData(columnType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByColumnTypeData(string columnType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (columnType != null ? "`ColumnType` = @ColumnType" : "`ColumnType` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (columnType != null)
				paras_.Add(Database.CreateInParameter("@ColumnType", columnType, MySqlDbType.VarChar));
		}
		#endregion // RemoveByColumnType
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
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE `IsPrimaryKey` = @IsPrimaryKey";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte));
		}
		#endregion // RemoveByIsPrimaryKey
		#region RemoveByFieldName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "fieldName">数据字段名</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByFieldName(string fieldName, TransactionManager tm_ = null)
		{
			RepairRemoveByFieldNameData(fieldName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByFieldNameAsync(string fieldName, TransactionManager tm_ = null)
		{
			RepairRemoveByFieldNameData(fieldName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByFieldNameData(string fieldName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (fieldName != null ? "`FieldName` = @FieldName" : "`FieldName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (fieldName != null)
				paras_.Add(Database.CreateInParameter("@FieldName", fieldName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByFieldName
		#region RemoveByColumnName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "columnName">数据表列名</param>
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
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (columnName != null ? "`ColumnName` = @ColumnName" : "`ColumnName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (columnName != null)
				paras_.Add(Database.CreateInParameter("@ColumnName", columnName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByColumnName
		#region RemoveByText
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "text">显示列名</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByText(string text, TransactionManager tm_ = null)
		{
			RepairRemoveByTextData(text, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTextAsync(string text, TransactionManager tm_ = null)
		{
			RepairRemoveByTextData(text, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTextData(string text, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (text != null ? "`Text` = @Text" : "`Text` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (text != null)
				paras_.Add(Database.CreateInParameter("@Text", text, MySqlDbType.VarChar));
		}
		#endregion // RemoveByText
		#region RemoveByAlign
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "align">对齐方式</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByAlign(string align, TransactionManager tm_ = null)
		{
			RepairRemoveByAlignData(align, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByAlignAsync(string align, TransactionManager tm_ = null)
		{
			RepairRemoveByAlignData(align, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByAlignData(string align, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (align != null ? "`Align` = @Align" : "`Align` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (align != null)
				paras_.Add(Database.CreateInParameter("@Align", align, MySqlDbType.VarChar));
		}
		#endregion // RemoveByAlign
		#region RemoveByWidth
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "width">列宽</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByWidth(string width, TransactionManager tm_ = null)
		{
			RepairRemoveByWidthData(width, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByWidthAsync(string width, TransactionManager tm_ = null)
		{
			RepairRemoveByWidthData(width, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByWidthData(string width, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (width != null ? "`Width` = @Width" : "`Width` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (width != null)
				paras_.Add(Database.CreateInParameter("@Width", width, MySqlDbType.VarChar));
		}
		#endregion // RemoveByWidth
		#region RemoveByFlex
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "flex">列宽flex</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByFlex(int? flex, TransactionManager tm_ = null)
		{
			RepairRemoveByFlexData(flex.Value, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByFlexAsync(int? flex, TransactionManager tm_ = null)
		{
			RepairRemoveByFlexData(flex.Value, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByFlexData(int? flex, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (flex.HasValue ? "`Flex` = @Flex" : "`Flex` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (flex.HasValue)
				paras_.Add(Database.CreateInParameter("@Flex", flex.Value, MySqlDbType.Int32));
		}
		#endregion // RemoveByFlex
		#region RemoveByLocked
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "locked">是否锁定</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByLocked(bool locked, TransactionManager tm_ = null)
		{
			RepairRemoveByLockedData(locked, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByLockedAsync(bool locked, TransactionManager tm_ = null)
		{
			RepairRemoveByLockedData(locked, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByLockedData(bool locked, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE `Locked` = @Locked";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Locked", locked, MySqlDbType.Byte));
		}
		#endregion // RemoveByLocked
		#region RemoveByVisible
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "visible">是否可见</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByVisible(bool visible, TransactionManager tm_ = null)
		{
			RepairRemoveByVisibleData(visible, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByVisibleAsync(bool visible, TransactionManager tm_ = null)
		{
			RepairRemoveByVisibleData(visible, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByVisibleData(bool visible, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE `Visible` = @Visible";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Visible", visible, MySqlDbType.Byte));
		}
		#endregion // RemoveByVisible
		#region RemoveByFormat
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "format">显示格式</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByFormat(string format, TransactionManager tm_ = null)
		{
			RepairRemoveByFormatData(format, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByFormatAsync(string format, TransactionManager tm_ = null)
		{
			RepairRemoveByFormatData(format, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByFormatData(string format, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (format != null ? "`Format` = @Format" : "`Format` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (format != null)
				paras_.Add(Database.CreateInParameter("@Format", format, MySqlDbType.VarChar));
		}
		#endregion // RemoveByFormat
		#region RemoveByTrueText
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "trueText">BooleanColumn true文本</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByTrueText(string trueText, TransactionManager tm_ = null)
		{
			RepairRemoveByTrueTextData(trueText, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTrueTextAsync(string trueText, TransactionManager tm_ = null)
		{
			RepairRemoveByTrueTextData(trueText, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTrueTextData(string trueText, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (trueText != null ? "`TrueText` = @TrueText" : "`TrueText` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (trueText != null)
				paras_.Add(Database.CreateInParameter("@TrueText", trueText, MySqlDbType.VarChar));
		}
		#endregion // RemoveByTrueText
		#region RemoveByFalseText
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "falseText">BooleanColumn false文本</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByFalseText(string falseText, TransactionManager tm_ = null)
		{
			RepairRemoveByFalseTextData(falseText, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByFalseTextAsync(string falseText, TransactionManager tm_ = null)
		{
			RepairRemoveByFalseTextData(falseText, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByFalseTextData(string falseText, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (falseText != null ? "`FalseText` = @FalseText" : "`FalseText` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (falseText != null)
				paras_.Add(Database.CreateInParameter("@FalseText", falseText, MySqlDbType.VarChar));
		}
		#endregion // RemoveByFalseText
		#region RemoveByFilterType
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "filterType">过滤类型</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByFilterType(string filterType, TransactionManager tm_ = null)
		{
			RepairRemoveByFilterTypeData(filterType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByFilterTypeAsync(string filterType, TransactionManager tm_ = null)
		{
			RepairRemoveByFilterTypeData(filterType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByFilterTypeData(string filterType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (filterType != null ? "`FilterType` = @FilterType" : "`FilterType` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (filterType != null)
				paras_.Add(Database.CreateInParameter("@FilterType", filterType, MySqlDbType.VarChar));
		}
		#endregion // RemoveByFilterType
		#region RemoveByRenderFn
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "renderFn">输出显示函数名</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByRenderFn(string renderFn, TransactionManager tm_ = null)
		{
			RepairRemoveByRenderFnData(renderFn, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByRenderFnAsync(string renderFn, TransactionManager tm_ = null)
		{
			RepairRemoveByRenderFnData(renderFn, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByRenderFnData(string renderFn, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (renderFn != null ? "`RenderFn` = @RenderFn" : "`RenderFn` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (renderFn != null)
				paras_.Add(Database.CreateInParameter("@RenderFn", renderFn, MySqlDbType.VarChar));
		}
		#endregion // RemoveByRenderFn
		#region RemoveByRenderFnContent
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "renderFnContent">输出显示函数内容</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByRenderFnContent(string renderFnContent, TransactionManager tm_ = null)
		{
			RepairRemoveByRenderFnContentData(renderFnContent, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByRenderFnContentAsync(string renderFnContent, TransactionManager tm_ = null)
		{
			RepairRemoveByRenderFnContentData(renderFnContent, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByRenderFnContentData(string renderFnContent, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (renderFnContent != null ? "`RenderFnContent` = @RenderFnContent" : "`RenderFnContent` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (renderFnContent != null)
				paras_.Add(Database.CreateInParameter("@RenderFnContent", renderFnContent, MySqlDbType.Text));
		}
		#endregion // RemoveByRenderFnContent
		#region RemoveByRenderHandler
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "renderHandler">输出显示handler</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByRenderHandler(string renderHandler, TransactionManager tm_ = null)
		{
			RepairRemoveByRenderHandlerData(renderHandler, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByRenderHandlerAsync(string renderHandler, TransactionManager tm_ = null)
		{
			RepairRemoveByRenderHandlerData(renderHandler, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByRenderHandlerData(string renderHandler, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE " + (renderHandler != null ? "`RenderHandler` = @RenderHandler" : "`RenderHandler` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (renderHandler != null)
				paras_.Add(Database.CreateInParameter("@RenderHandler", renderHandler, MySqlDbType.VarChar));
		}
		#endregion // RemoveByRenderHandler
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
			sql_ = @"DELETE FROM `admin_listedit_gridcolumn` WHERE `RecDate` = @RecDate";
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
		public int Put(Admin_listedit_gridcolumnEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_listedit_gridcolumnEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_listedit_gridcolumnEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `GenID` = @GenID, `OrderNum` = @OrderNum, `ColumnType` = @ColumnType, `IsPrimaryKey` = @IsPrimaryKey, `FieldName` = @FieldName, `ColumnName` = @ColumnName, `Text` = @Text, `Align` = @Align, `Width` = @Width, `Flex` = @Flex, `Locked` = @Locked, `Visible` = @Visible, `Format` = @Format, `TrueText` = @TrueText, `FalseText` = @FalseText, `FilterType` = @FilterType, `RenderFn` = @RenderFn, `RenderFnContent` = @RenderFnContent, `RenderHandler` = @RenderHandler WHERE `GridColumnID` = @GridColumnID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", item.GenID, MySqlDbType.Int64),
				Database.CreateInParameter("@OrderNum", item.OrderNum, MySqlDbType.Int32),
				Database.CreateInParameter("@ColumnType", item.ColumnType != null ? item.ColumnType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@IsPrimaryKey", item.IsPrimaryKey, MySqlDbType.Byte),
				Database.CreateInParameter("@FieldName", item.FieldName != null ? item.FieldName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@ColumnName", item.ColumnName != null ? item.ColumnName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Text", item.Text != null ? item.Text : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Align", item.Align != null ? item.Align : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Width", item.Width != null ? item.Width : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Flex", item.Flex.HasValue ? item.Flex.Value : (object)DBNull.Value, MySqlDbType.Int32),
				Database.CreateInParameter("@Locked", item.Locked, MySqlDbType.Byte),
				Database.CreateInParameter("@Visible", item.Visible, MySqlDbType.Byte),
				Database.CreateInParameter("@Format", item.Format != null ? item.Format : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@TrueText", item.TrueText != null ? item.TrueText : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@FalseText", item.FalseText != null ? item.FalseText : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@FilterType", item.FilterType != null ? item.FilterType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RenderFn", item.RenderFn != null ? item.RenderFn : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@RenderFnContent", item.RenderFnContent != null ? item.RenderFnContent : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@RenderHandler", item.RenderHandler != null ? item.RenderHandler : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID_Original", item.HasOriginal ? item.OriginalGridColumnID : item.GridColumnID, MySqlDbType.Int64),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_listedit_gridcolumnEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_listedit_gridcolumnEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long gridColumnID, string set_, params object[] values_)
		{
			return Put(set_, "`GridColumnID` = @GridColumnID", ConcatValues(values_, gridColumnID));
		}
		public async Task<int> PutByPKAsync(long gridColumnID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`GridColumnID` = @GridColumnID", ConcatValues(values_, gridColumnID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long gridColumnID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`GridColumnID` = @GridColumnID", tm_, ConcatValues(values_, gridColumnID));
		}
		public async Task<int> PutByPKAsync(long gridColumnID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`GridColumnID` = @GridColumnID", tm_, ConcatValues(values_, gridColumnID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long gridColumnID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
	        };
			return Put(set_, "`GridColumnID` = @GridColumnID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(long gridColumnID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
	        };
			return await PutAsync(set_, "`GridColumnID` = @GridColumnID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutGenID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGenIDByPK(long gridColumnID, long genID, TransactionManager tm_ = null)
		{
			RepairPutGenIDByPKData(gridColumnID, genID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutGenIDByPKAsync(long gridColumnID, long genID, TransactionManager tm_ = null)
		{
			RepairPutGenIDByPKData(gridColumnID, genID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutGenIDByPKData(long gridColumnID, long genID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `GenID` = @GenID  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGenID(long genID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `GenID` = @GenID";
			var parameter_ = Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutGenIDAsync(long genID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `GenID` = @GenID";
			var parameter_ = Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutGenID
		#region PutOrderNum
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "orderNum">排序</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutOrderNumByPK(long gridColumnID, int orderNum, TransactionManager tm_ = null)
		{
			RepairPutOrderNumByPKData(gridColumnID, orderNum, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutOrderNumByPKAsync(long gridColumnID, int orderNum, TransactionManager tm_ = null)
		{
			RepairPutOrderNumByPKData(gridColumnID, orderNum, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutOrderNumByPKData(long gridColumnID, int orderNum, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `OrderNum` = @OrderNum  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "orderNum">排序</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutOrderNum(int orderNum, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `OrderNum` = @OrderNum";
			var parameter_ = Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutOrderNumAsync(int orderNum, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `OrderNum` = @OrderNum";
			var parameter_ = Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutOrderNum
		#region PutColumnType
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "columnType">列类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutColumnTypeByPK(long gridColumnID, string columnType, TransactionManager tm_ = null)
		{
			RepairPutColumnTypeByPKData(gridColumnID, columnType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutColumnTypeByPKAsync(long gridColumnID, string columnType, TransactionManager tm_ = null)
		{
			RepairPutColumnTypeByPKData(gridColumnID, columnType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutColumnTypeByPKData(long gridColumnID, string columnType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `ColumnType` = @ColumnType  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ColumnType", columnType != null ? columnType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "columnType">列类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutColumnType(string columnType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `ColumnType` = @ColumnType";
			var parameter_ = Database.CreateInParameter("@ColumnType", columnType != null ? columnType : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutColumnTypeAsync(string columnType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `ColumnType` = @ColumnType";
			var parameter_ = Database.CreateInParameter("@ColumnType", columnType != null ? columnType : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutColumnType
		#region PutIsPrimaryKey
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIsPrimaryKeyByPK(long gridColumnID, bool isPrimaryKey, TransactionManager tm_ = null)
		{
			RepairPutIsPrimaryKeyByPKData(gridColumnID, isPrimaryKey, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutIsPrimaryKeyByPKAsync(long gridColumnID, bool isPrimaryKey, TransactionManager tm_ = null)
		{
			RepairPutIsPrimaryKeyByPKData(gridColumnID, isPrimaryKey, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutIsPrimaryKeyByPKData(long gridColumnID, bool isPrimaryKey, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `IsPrimaryKey` = @IsPrimaryKey  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `IsPrimaryKey` = @IsPrimaryKey";
			var parameter_ = Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutIsPrimaryKeyAsync(bool isPrimaryKey, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `IsPrimaryKey` = @IsPrimaryKey";
			var parameter_ = Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutIsPrimaryKey
		#region PutFieldName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "fieldName">数据字段名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFieldNameByPK(long gridColumnID, string fieldName, TransactionManager tm_ = null)
		{
			RepairPutFieldNameByPKData(gridColumnID, fieldName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutFieldNameByPKAsync(long gridColumnID, string fieldName, TransactionManager tm_ = null)
		{
			RepairPutFieldNameByPKData(gridColumnID, fieldName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutFieldNameByPKData(long gridColumnID, string fieldName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `FieldName` = @FieldName  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@FieldName", fieldName != null ? fieldName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "fieldName">数据字段名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFieldName(string fieldName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `FieldName` = @FieldName";
			var parameter_ = Database.CreateInParameter("@FieldName", fieldName != null ? fieldName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutFieldNameAsync(string fieldName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `FieldName` = @FieldName";
			var parameter_ = Database.CreateInParameter("@FieldName", fieldName != null ? fieldName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutFieldName
		#region PutColumnName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "columnName">数据表列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutColumnNameByPK(long gridColumnID, string columnName, TransactionManager tm_ = null)
		{
			RepairPutColumnNameByPKData(gridColumnID, columnName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutColumnNameByPKAsync(long gridColumnID, string columnName, TransactionManager tm_ = null)
		{
			RepairPutColumnNameByPKData(gridColumnID, columnName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutColumnNameByPKData(long gridColumnID, string columnName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `ColumnName` = @ColumnName  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ColumnName", columnName != null ? columnName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "columnName">数据表列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutColumnName(string columnName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `ColumnName` = @ColumnName";
			var parameter_ = Database.CreateInParameter("@ColumnName", columnName != null ? columnName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutColumnNameAsync(string columnName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `ColumnName` = @ColumnName";
			var parameter_ = Database.CreateInParameter("@ColumnName", columnName != null ? columnName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutColumnName
		#region PutText
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "text">显示列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTextByPK(long gridColumnID, string text, TransactionManager tm_ = null)
		{
			RepairPutTextByPKData(gridColumnID, text, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTextByPKAsync(long gridColumnID, string text, TransactionManager tm_ = null)
		{
			RepairPutTextByPKData(gridColumnID, text, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTextByPKData(long gridColumnID, string text, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Text` = @Text  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Text", text != null ? text : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "text">显示列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutText(string text, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Text` = @Text";
			var parameter_ = Database.CreateInParameter("@Text", text != null ? text : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTextAsync(string text, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Text` = @Text";
			var parameter_ = Database.CreateInParameter("@Text", text != null ? text : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutText
		#region PutAlign
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "align">对齐方式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutAlignByPK(long gridColumnID, string align, TransactionManager tm_ = null)
		{
			RepairPutAlignByPKData(gridColumnID, align, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAlignByPKAsync(long gridColumnID, string align, TransactionManager tm_ = null)
		{
			RepairPutAlignByPKData(gridColumnID, align, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutAlignByPKData(long gridColumnID, string align, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Align` = @Align  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Align", align != null ? align : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "align">对齐方式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutAlign(string align, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Align` = @Align";
			var parameter_ = Database.CreateInParameter("@Align", align != null ? align : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutAlignAsync(string align, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Align` = @Align";
			var parameter_ = Database.CreateInParameter("@Align", align != null ? align : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutAlign
		#region PutWidth
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "width">列宽</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutWidthByPK(long gridColumnID, string width, TransactionManager tm_ = null)
		{
			RepairPutWidthByPKData(gridColumnID, width, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutWidthByPKAsync(long gridColumnID, string width, TransactionManager tm_ = null)
		{
			RepairPutWidthByPKData(gridColumnID, width, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutWidthByPKData(long gridColumnID, string width, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Width` = @Width  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Width", width != null ? width : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "width">列宽</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutWidth(string width, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Width` = @Width";
			var parameter_ = Database.CreateInParameter("@Width", width != null ? width : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutWidthAsync(string width, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Width` = @Width";
			var parameter_ = Database.CreateInParameter("@Width", width != null ? width : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutWidth
		#region PutFlex
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "flex">列宽flex</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFlexByPK(long gridColumnID, int? flex, TransactionManager tm_ = null)
		{
			RepairPutFlexByPKData(gridColumnID, flex, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutFlexByPKAsync(long gridColumnID, int? flex, TransactionManager tm_ = null)
		{
			RepairPutFlexByPKData(gridColumnID, flex, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutFlexByPKData(long gridColumnID, int? flex, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Flex` = @Flex  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Flex", flex.HasValue ? flex.Value : (object)DBNull.Value, MySqlDbType.Int32),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "flex">列宽flex</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFlex(int? flex, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Flex` = @Flex";
			var parameter_ = Database.CreateInParameter("@Flex", flex.HasValue ? flex.Value : (object)DBNull.Value, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutFlexAsync(int? flex, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Flex` = @Flex";
			var parameter_ = Database.CreateInParameter("@Flex", flex.HasValue ? flex.Value : (object)DBNull.Value, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutFlex
		#region PutLocked
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "locked">是否锁定</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLockedByPK(long gridColumnID, bool locked, TransactionManager tm_ = null)
		{
			RepairPutLockedByPKData(gridColumnID, locked, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutLockedByPKAsync(long gridColumnID, bool locked, TransactionManager tm_ = null)
		{
			RepairPutLockedByPKData(gridColumnID, locked, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutLockedByPKData(long gridColumnID, bool locked, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Locked` = @Locked  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Locked", locked, MySqlDbType.Byte),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "locked">是否锁定</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLocked(bool locked, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Locked` = @Locked";
			var parameter_ = Database.CreateInParameter("@Locked", locked, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutLockedAsync(bool locked, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Locked` = @Locked";
			var parameter_ = Database.CreateInParameter("@Locked", locked, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutLocked
		#region PutVisible
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "visible">是否可见</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutVisibleByPK(long gridColumnID, bool visible, TransactionManager tm_ = null)
		{
			RepairPutVisibleByPKData(gridColumnID, visible, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutVisibleByPKAsync(long gridColumnID, bool visible, TransactionManager tm_ = null)
		{
			RepairPutVisibleByPKData(gridColumnID, visible, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutVisibleByPKData(long gridColumnID, bool visible, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Visible` = @Visible  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Visible", visible, MySqlDbType.Byte),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "visible">是否可见</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutVisible(bool visible, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Visible` = @Visible";
			var parameter_ = Database.CreateInParameter("@Visible", visible, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutVisibleAsync(bool visible, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Visible` = @Visible";
			var parameter_ = Database.CreateInParameter("@Visible", visible, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutVisible
		#region PutFormat
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "format">显示格式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFormatByPK(long gridColumnID, string format, TransactionManager tm_ = null)
		{
			RepairPutFormatByPKData(gridColumnID, format, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutFormatByPKAsync(long gridColumnID, string format, TransactionManager tm_ = null)
		{
			RepairPutFormatByPKData(gridColumnID, format, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutFormatByPKData(long gridColumnID, string format, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Format` = @Format  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Format", format != null ? format : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "format">显示格式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFormat(string format, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Format` = @Format";
			var parameter_ = Database.CreateInParameter("@Format", format != null ? format : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutFormatAsync(string format, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `Format` = @Format";
			var parameter_ = Database.CreateInParameter("@Format", format != null ? format : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutFormat
		#region PutTrueText
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "trueText">BooleanColumn true文本</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTrueTextByPK(long gridColumnID, string trueText, TransactionManager tm_ = null)
		{
			RepairPutTrueTextByPKData(gridColumnID, trueText, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTrueTextByPKAsync(long gridColumnID, string trueText, TransactionManager tm_ = null)
		{
			RepairPutTrueTextByPKData(gridColumnID, trueText, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTrueTextByPKData(long gridColumnID, string trueText, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `TrueText` = @TrueText  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@TrueText", trueText != null ? trueText : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "trueText">BooleanColumn true文本</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTrueText(string trueText, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `TrueText` = @TrueText";
			var parameter_ = Database.CreateInParameter("@TrueText", trueText != null ? trueText : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTrueTextAsync(string trueText, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `TrueText` = @TrueText";
			var parameter_ = Database.CreateInParameter("@TrueText", trueText != null ? trueText : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTrueText
		#region PutFalseText
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "falseText">BooleanColumn false文本</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFalseTextByPK(long gridColumnID, string falseText, TransactionManager tm_ = null)
		{
			RepairPutFalseTextByPKData(gridColumnID, falseText, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutFalseTextByPKAsync(long gridColumnID, string falseText, TransactionManager tm_ = null)
		{
			RepairPutFalseTextByPKData(gridColumnID, falseText, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutFalseTextByPKData(long gridColumnID, string falseText, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `FalseText` = @FalseText  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@FalseText", falseText != null ? falseText : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "falseText">BooleanColumn false文本</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFalseText(string falseText, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `FalseText` = @FalseText";
			var parameter_ = Database.CreateInParameter("@FalseText", falseText != null ? falseText : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutFalseTextAsync(string falseText, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `FalseText` = @FalseText";
			var parameter_ = Database.CreateInParameter("@FalseText", falseText != null ? falseText : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutFalseText
		#region PutFilterType
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "filterType">过滤类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFilterTypeByPK(long gridColumnID, string filterType, TransactionManager tm_ = null)
		{
			RepairPutFilterTypeByPKData(gridColumnID, filterType, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutFilterTypeByPKAsync(long gridColumnID, string filterType, TransactionManager tm_ = null)
		{
			RepairPutFilterTypeByPKData(gridColumnID, filterType, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutFilterTypeByPKData(long gridColumnID, string filterType, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `FilterType` = @FilterType  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@FilterType", filterType != null ? filterType : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "filterType">过滤类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutFilterType(string filterType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `FilterType` = @FilterType";
			var parameter_ = Database.CreateInParameter("@FilterType", filterType != null ? filterType : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutFilterTypeAsync(string filterType, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `FilterType` = @FilterType";
			var parameter_ = Database.CreateInParameter("@FilterType", filterType != null ? filterType : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutFilterType
		#region PutRenderFn
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "renderFn">输出显示函数名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRenderFnByPK(long gridColumnID, string renderFn, TransactionManager tm_ = null)
		{
			RepairPutRenderFnByPKData(gridColumnID, renderFn, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRenderFnByPKAsync(long gridColumnID, string renderFn, TransactionManager tm_ = null)
		{
			RepairPutRenderFnByPKData(gridColumnID, renderFn, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRenderFnByPKData(long gridColumnID, string renderFn, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RenderFn` = @RenderFn  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RenderFn", renderFn != null ? renderFn : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "renderFn">输出显示函数名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRenderFn(string renderFn, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RenderFn` = @RenderFn";
			var parameter_ = Database.CreateInParameter("@RenderFn", renderFn != null ? renderFn : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRenderFnAsync(string renderFn, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RenderFn` = @RenderFn";
			var parameter_ = Database.CreateInParameter("@RenderFn", renderFn != null ? renderFn : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRenderFn
		#region PutRenderFnContent
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "renderFnContent">输出显示函数内容</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRenderFnContentByPK(long gridColumnID, string renderFnContent, TransactionManager tm_ = null)
		{
			RepairPutRenderFnContentByPKData(gridColumnID, renderFnContent, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRenderFnContentByPKAsync(long gridColumnID, string renderFnContent, TransactionManager tm_ = null)
		{
			RepairPutRenderFnContentByPKData(gridColumnID, renderFnContent, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRenderFnContentByPKData(long gridColumnID, string renderFnContent, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RenderFnContent` = @RenderFnContent  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RenderFnContent", renderFnContent != null ? renderFnContent : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "renderFnContent">输出显示函数内容</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRenderFnContent(string renderFnContent, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RenderFnContent` = @RenderFnContent";
			var parameter_ = Database.CreateInParameter("@RenderFnContent", renderFnContent != null ? renderFnContent : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRenderFnContentAsync(string renderFnContent, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RenderFnContent` = @RenderFnContent";
			var parameter_ = Database.CreateInParameter("@RenderFnContent", renderFnContent != null ? renderFnContent : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRenderFnContent
		#region PutRenderHandler
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "renderHandler">输出显示handler</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRenderHandlerByPK(long gridColumnID, string renderHandler, TransactionManager tm_ = null)
		{
			RepairPutRenderHandlerByPKData(gridColumnID, renderHandler, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRenderHandlerByPKAsync(long gridColumnID, string renderHandler, TransactionManager tm_ = null)
		{
			RepairPutRenderHandlerByPKData(gridColumnID, renderHandler, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRenderHandlerByPKData(long gridColumnID, string renderHandler, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RenderHandler` = @RenderHandler  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RenderHandler", renderHandler != null ? renderHandler : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "renderHandler">输出显示handler</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRenderHandler(string renderHandler, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RenderHandler` = @RenderHandler";
			var parameter_ = Database.CreateInParameter("@RenderHandler", renderHandler != null ? renderHandler : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRenderHandlerAsync(string renderHandler, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RenderHandler` = @RenderHandler";
			var parameter_ = Database.CreateInParameter("@RenderHandler", renderHandler != null ? renderHandler : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutRenderHandler
		#region PutRecDate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRecDateByPK(long gridColumnID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(gridColumnID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRecDateByPKAsync(long gridColumnID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(gridColumnID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRecDateByPKData(long gridColumnID, DateTime recDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RecDate` = @RecDate  WHERE `GridColumnID` = @GridColumnID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RecDate` = @RecDate";
			var parameter_ = Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRecDateAsync(DateTime recDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit_gridcolumn` SET `RecDate` = @RecDate";
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
		public bool Set(Admin_listedit_gridcolumnEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.GridColumnID) == null)
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
		public async Task<bool> SetAsync(Admin_listedit_gridcolumnEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.GridColumnID) == null)
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
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_listedit_gridcolumnEO GetByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(gridColumnID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<Admin_listedit_gridcolumnEO> GetByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(gridColumnID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		private void RepairGetByPKData(long gridColumnID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`GridColumnID` = @GridColumnID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 GenID（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public long GetGenIDByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (long)GetScalar("`GenID`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<long> GetGenIDByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (long)await GetScalarAsync("`GenID`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 OrderNum（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetOrderNumByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`OrderNum`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<int> GetOrderNumByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`OrderNum`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ColumnType（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetColumnTypeByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`ColumnType`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetColumnTypeByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`ColumnType`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 IsPrimaryKey（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public bool GetIsPrimaryKeyByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (bool)GetScalar("`IsPrimaryKey`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<bool> GetIsPrimaryKeyByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (bool)await GetScalarAsync("`IsPrimaryKey`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 FieldName（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetFieldNameByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`FieldName`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetFieldNameByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`FieldName`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ColumnName（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetColumnNameByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`ColumnName`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetColumnNameByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`ColumnName`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Text（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetTextByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`Text`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetTextByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`Text`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Align（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetAlignByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`Align`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetAlignByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`Align`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Width（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetWidthByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`Width`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetWidthByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`Width`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Flex（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int? GetFlexByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (int?)GetScalar("`Flex`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<int?> GetFlexByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (int?)await GetScalarAsync("`Flex`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Locked（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public bool GetLockedByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (bool)GetScalar("`Locked`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<bool> GetLockedByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (bool)await GetScalarAsync("`Locked`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Visible（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public bool GetVisibleByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (bool)GetScalar("`Visible`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<bool> GetVisibleByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (bool)await GetScalarAsync("`Visible`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Format（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetFormatByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`Format`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetFormatByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`Format`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 TrueText（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetTrueTextByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`TrueText`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetTrueTextByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`TrueText`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 FalseText（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetFalseTextByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`FalseText`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetFalseTextByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`FalseText`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 FilterType（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetFilterTypeByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`FilterType`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetFilterTypeByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`FilterType`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RenderFn（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetRenderFnByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`RenderFn`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetRenderFnByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`RenderFn`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RenderFnContent（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetRenderFnContentByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`RenderFnContent`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetRenderFnContentByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`RenderFnContent`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RenderHandler（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetRenderHandlerByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`RenderHandler`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<string> GetRenderHandlerByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`RenderHandler`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RecDate（字段）
		/// </summary>
		/// /// <param name = "gridColumnID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public DateTime GetRecDateByPK(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (DateTime)GetScalar("`RecDate`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		public async Task<DateTime> GetRecDateByPKAsync(long gridColumnID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridColumnID", gridColumnID, MySqlDbType.Int64),
			};
			return (DateTime)await GetScalarAsync("`RecDate`", "`GridColumnID` = @GridColumnID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByGenID
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByGenID(long genID)
		{
			return GetByGenID(genID, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByGenIDAsync(long genID)
		{
			return await GetByGenIDAsync(genID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByGenID(long genID, TransactionManager tm_)
		{
			return GetByGenID(genID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByGenIDAsync(long genID, TransactionManager tm_)
		{
			return await GetByGenIDAsync(genID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByGenID(long genID, int top_)
		{
			return GetByGenID(genID, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByGenIDAsync(long genID, int top_)
		{
			return await GetByGenIDAsync(genID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByGenID(long genID, int top_, TransactionManager tm_)
		{
			return GetByGenID(genID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByGenIDAsync(long genID, int top_, TransactionManager tm_)
		{
			return await GetByGenIDAsync(genID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByGenID(long genID, string sort_)
		{
			return GetByGenID(genID, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByGenIDAsync(long genID, string sort_)
		{
			return await GetByGenIDAsync(genID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByGenID(long genID, string sort_, TransactionManager tm_)
		{
			return GetByGenID(genID, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByGenIDAsync(long genID, string sort_, TransactionManager tm_)
		{
			return await GetByGenIDAsync(genID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByGenID(long genID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`GenID` = @GenID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByGenIDAsync(long genID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`GenID` = @GenID", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByGenID
		#region GetByOrderNum
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByOrderNum(int orderNum)
		{
			return GetByOrderNum(orderNum, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByOrderNumAsync(int orderNum)
		{
			return await GetByOrderNumAsync(orderNum, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByOrderNum(int orderNum, TransactionManager tm_)
		{
			return GetByOrderNum(orderNum, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByOrderNumAsync(int orderNum, TransactionManager tm_)
		{
			return await GetByOrderNumAsync(orderNum, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByOrderNum(int orderNum, int top_)
		{
			return GetByOrderNum(orderNum, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByOrderNumAsync(int orderNum, int top_)
		{
			return await GetByOrderNumAsync(orderNum, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByOrderNum(int orderNum, int top_, TransactionManager tm_)
		{
			return GetByOrderNum(orderNum, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByOrderNumAsync(int orderNum, int top_, TransactionManager tm_)
		{
			return await GetByOrderNumAsync(orderNum, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByOrderNum(int orderNum, string sort_)
		{
			return GetByOrderNum(orderNum, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByOrderNumAsync(int orderNum, string sort_)
		{
			return await GetByOrderNumAsync(orderNum, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByOrderNum(int orderNum, string sort_, TransactionManager tm_)
		{
			return GetByOrderNum(orderNum, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByOrderNumAsync(int orderNum, string sort_, TransactionManager tm_)
		{
			return await GetByOrderNumAsync(orderNum, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByOrderNum(int orderNum, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`OrderNum` = @OrderNum", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByOrderNumAsync(int orderNum, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`OrderNum` = @OrderNum", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByOrderNum
		#region GetByColumnType
		
		/// <summary>
		/// 按 ColumnType（字段） 查询
		/// </summary>
		/// /// <param name = "columnType">列类型</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnType(string columnType)
		{
			return GetByColumnType(columnType, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnTypeAsync(string columnType)
		{
			return await GetByColumnTypeAsync(columnType, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ColumnType（字段） 查询
		/// </summary>
		/// /// <param name = "columnType">列类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnType(string columnType, TransactionManager tm_)
		{
			return GetByColumnType(columnType, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnTypeAsync(string columnType, TransactionManager tm_)
		{
			return await GetByColumnTypeAsync(columnType, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ColumnType（字段） 查询
		/// </summary>
		/// /// <param name = "columnType">列类型</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnType(string columnType, int top_)
		{
			return GetByColumnType(columnType, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnTypeAsync(string columnType, int top_)
		{
			return await GetByColumnTypeAsync(columnType, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ColumnType（字段） 查询
		/// </summary>
		/// /// <param name = "columnType">列类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnType(string columnType, int top_, TransactionManager tm_)
		{
			return GetByColumnType(columnType, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnTypeAsync(string columnType, int top_, TransactionManager tm_)
		{
			return await GetByColumnTypeAsync(columnType, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ColumnType（字段） 查询
		/// </summary>
		/// /// <param name = "columnType">列类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnType(string columnType, string sort_)
		{
			return GetByColumnType(columnType, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnTypeAsync(string columnType, string sort_)
		{
			return await GetByColumnTypeAsync(columnType, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ColumnType（字段） 查询
		/// </summary>
		/// /// <param name = "columnType">列类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnType(string columnType, string sort_, TransactionManager tm_)
		{
			return GetByColumnType(columnType, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnTypeAsync(string columnType, string sort_, TransactionManager tm_)
		{
			return await GetByColumnTypeAsync(columnType, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ColumnType（字段） 查询
		/// </summary>
		/// /// <param name = "columnType">列类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnType(string columnType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(columnType != null ? "`ColumnType` = @ColumnType" : "`ColumnType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (columnType != null)
				paras_.Add(Database.CreateInParameter("@ColumnType", columnType, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnTypeAsync(string columnType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(columnType != null ? "`ColumnType` = @ColumnType" : "`ColumnType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (columnType != null)
				paras_.Add(Database.CreateInParameter("@ColumnType", columnType, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByColumnType
		#region GetByIsPrimaryKey
		
		/// <summary>
		/// 按 IsPrimaryKey（字段） 查询
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByIsPrimaryKey(bool isPrimaryKey)
		{
			return GetByIsPrimaryKey(isPrimaryKey, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey)
		{
			return await GetByIsPrimaryKeyAsync(isPrimaryKey, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 IsPrimaryKey（字段） 查询
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByIsPrimaryKey(bool isPrimaryKey, TransactionManager tm_)
		{
			return GetByIsPrimaryKey(isPrimaryKey, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, TransactionManager tm_)
		{
			return await GetByIsPrimaryKeyAsync(isPrimaryKey, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IsPrimaryKey（字段） 查询
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByIsPrimaryKey(bool isPrimaryKey, int top_)
		{
			return GetByIsPrimaryKey(isPrimaryKey, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, int top_)
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
		public List<Admin_listedit_gridcolumnEO> GetByIsPrimaryKey(bool isPrimaryKey, int top_, TransactionManager tm_)
		{
			return GetByIsPrimaryKey(isPrimaryKey, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, int top_, TransactionManager tm_)
		{
			return await GetByIsPrimaryKeyAsync(isPrimaryKey, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IsPrimaryKey（字段） 查询
		/// </summary>
		/// /// <param name = "isPrimaryKey">是否主键</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByIsPrimaryKey(bool isPrimaryKey, string sort_)
		{
			return GetByIsPrimaryKey(isPrimaryKey, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, string sort_)
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
		public List<Admin_listedit_gridcolumnEO> GetByIsPrimaryKey(bool isPrimaryKey, string sort_, TransactionManager tm_)
		{
			return GetByIsPrimaryKey(isPrimaryKey, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, string sort_, TransactionManager tm_)
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
		public List<Admin_listedit_gridcolumnEO> GetByIsPrimaryKey(bool isPrimaryKey, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`IsPrimaryKey` = @IsPrimaryKey", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByIsPrimaryKeyAsync(bool isPrimaryKey, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`IsPrimaryKey` = @IsPrimaryKey", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@IsPrimaryKey", isPrimaryKey, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByIsPrimaryKey
		#region GetByFieldName
		
		/// <summary>
		/// 按 FieldName（字段） 查询
		/// </summary>
		/// /// <param name = "fieldName">数据字段名</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFieldName(string fieldName)
		{
			return GetByFieldName(fieldName, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFieldNameAsync(string fieldName)
		{
			return await GetByFieldNameAsync(fieldName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 FieldName（字段） 查询
		/// </summary>
		/// /// <param name = "fieldName">数据字段名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFieldName(string fieldName, TransactionManager tm_)
		{
			return GetByFieldName(fieldName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFieldNameAsync(string fieldName, TransactionManager tm_)
		{
			return await GetByFieldNameAsync(fieldName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 FieldName（字段） 查询
		/// </summary>
		/// /// <param name = "fieldName">数据字段名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFieldName(string fieldName, int top_)
		{
			return GetByFieldName(fieldName, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFieldNameAsync(string fieldName, int top_)
		{
			return await GetByFieldNameAsync(fieldName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 FieldName（字段） 查询
		/// </summary>
		/// /// <param name = "fieldName">数据字段名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFieldName(string fieldName, int top_, TransactionManager tm_)
		{
			return GetByFieldName(fieldName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFieldNameAsync(string fieldName, int top_, TransactionManager tm_)
		{
			return await GetByFieldNameAsync(fieldName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 FieldName（字段） 查询
		/// </summary>
		/// /// <param name = "fieldName">数据字段名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFieldName(string fieldName, string sort_)
		{
			return GetByFieldName(fieldName, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFieldNameAsync(string fieldName, string sort_)
		{
			return await GetByFieldNameAsync(fieldName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 FieldName（字段） 查询
		/// </summary>
		/// /// <param name = "fieldName">数据字段名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFieldName(string fieldName, string sort_, TransactionManager tm_)
		{
			return GetByFieldName(fieldName, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFieldNameAsync(string fieldName, string sort_, TransactionManager tm_)
		{
			return await GetByFieldNameAsync(fieldName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 FieldName（字段） 查询
		/// </summary>
		/// /// <param name = "fieldName">数据字段名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFieldName(string fieldName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(fieldName != null ? "`FieldName` = @FieldName" : "`FieldName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (fieldName != null)
				paras_.Add(Database.CreateInParameter("@FieldName", fieldName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFieldNameAsync(string fieldName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(fieldName != null ? "`FieldName` = @FieldName" : "`FieldName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (fieldName != null)
				paras_.Add(Database.CreateInParameter("@FieldName", fieldName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByFieldName
		#region GetByColumnName
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">数据表列名</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnName(string columnName)
		{
			return GetByColumnName(columnName, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnNameAsync(string columnName)
		{
			return await GetByColumnNameAsync(columnName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">数据表列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnName(string columnName, TransactionManager tm_)
		{
			return GetByColumnName(columnName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnNameAsync(string columnName, TransactionManager tm_)
		{
			return await GetByColumnNameAsync(columnName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">数据表列名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnName(string columnName, int top_)
		{
			return GetByColumnName(columnName, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnNameAsync(string columnName, int top_)
		{
			return await GetByColumnNameAsync(columnName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">数据表列名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnName(string columnName, int top_, TransactionManager tm_)
		{
			return GetByColumnName(columnName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnNameAsync(string columnName, int top_, TransactionManager tm_)
		{
			return await GetByColumnNameAsync(columnName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">数据表列名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnName(string columnName, string sort_)
		{
			return GetByColumnName(columnName, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnNameAsync(string columnName, string sort_)
		{
			return await GetByColumnNameAsync(columnName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">数据表列名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnName(string columnName, string sort_, TransactionManager tm_)
		{
			return GetByColumnName(columnName, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnNameAsync(string columnName, string sort_, TransactionManager tm_)
		{
			return await GetByColumnNameAsync(columnName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ColumnName（字段） 查询
		/// </summary>
		/// /// <param name = "columnName">数据表列名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByColumnName(string columnName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(columnName != null ? "`ColumnName` = @ColumnName" : "`ColumnName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (columnName != null)
				paras_.Add(Database.CreateInParameter("@ColumnName", columnName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByColumnNameAsync(string columnName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(columnName != null ? "`ColumnName` = @ColumnName" : "`ColumnName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (columnName != null)
				paras_.Add(Database.CreateInParameter("@ColumnName", columnName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByColumnName
		#region GetByText
		
		/// <summary>
		/// 按 Text（字段） 查询
		/// </summary>
		/// /// <param name = "text">显示列名</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByText(string text)
		{
			return GetByText(text, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTextAsync(string text)
		{
			return await GetByTextAsync(text, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Text（字段） 查询
		/// </summary>
		/// /// <param name = "text">显示列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByText(string text, TransactionManager tm_)
		{
			return GetByText(text, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTextAsync(string text, TransactionManager tm_)
		{
			return await GetByTextAsync(text, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Text（字段） 查询
		/// </summary>
		/// /// <param name = "text">显示列名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByText(string text, int top_)
		{
			return GetByText(text, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTextAsync(string text, int top_)
		{
			return await GetByTextAsync(text, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Text（字段） 查询
		/// </summary>
		/// /// <param name = "text">显示列名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByText(string text, int top_, TransactionManager tm_)
		{
			return GetByText(text, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTextAsync(string text, int top_, TransactionManager tm_)
		{
			return await GetByTextAsync(text, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Text（字段） 查询
		/// </summary>
		/// /// <param name = "text">显示列名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByText(string text, string sort_)
		{
			return GetByText(text, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTextAsync(string text, string sort_)
		{
			return await GetByTextAsync(text, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Text（字段） 查询
		/// </summary>
		/// /// <param name = "text">显示列名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByText(string text, string sort_, TransactionManager tm_)
		{
			return GetByText(text, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTextAsync(string text, string sort_, TransactionManager tm_)
		{
			return await GetByTextAsync(text, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Text（字段） 查询
		/// </summary>
		/// /// <param name = "text">显示列名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByText(string text, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(text != null ? "`Text` = @Text" : "`Text` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (text != null)
				paras_.Add(Database.CreateInParameter("@Text", text, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTextAsync(string text, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(text != null ? "`Text` = @Text" : "`Text` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (text != null)
				paras_.Add(Database.CreateInParameter("@Text", text, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByText
		#region GetByAlign
		
		/// <summary>
		/// 按 Align（字段） 查询
		/// </summary>
		/// /// <param name = "align">对齐方式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByAlign(string align)
		{
			return GetByAlign(align, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByAlignAsync(string align)
		{
			return await GetByAlignAsync(align, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Align（字段） 查询
		/// </summary>
		/// /// <param name = "align">对齐方式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByAlign(string align, TransactionManager tm_)
		{
			return GetByAlign(align, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByAlignAsync(string align, TransactionManager tm_)
		{
			return await GetByAlignAsync(align, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Align（字段） 查询
		/// </summary>
		/// /// <param name = "align">对齐方式</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByAlign(string align, int top_)
		{
			return GetByAlign(align, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByAlignAsync(string align, int top_)
		{
			return await GetByAlignAsync(align, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Align（字段） 查询
		/// </summary>
		/// /// <param name = "align">对齐方式</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByAlign(string align, int top_, TransactionManager tm_)
		{
			return GetByAlign(align, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByAlignAsync(string align, int top_, TransactionManager tm_)
		{
			return await GetByAlignAsync(align, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Align（字段） 查询
		/// </summary>
		/// /// <param name = "align">对齐方式</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByAlign(string align, string sort_)
		{
			return GetByAlign(align, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByAlignAsync(string align, string sort_)
		{
			return await GetByAlignAsync(align, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Align（字段） 查询
		/// </summary>
		/// /// <param name = "align">对齐方式</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByAlign(string align, string sort_, TransactionManager tm_)
		{
			return GetByAlign(align, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByAlignAsync(string align, string sort_, TransactionManager tm_)
		{
			return await GetByAlignAsync(align, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Align（字段） 查询
		/// </summary>
		/// /// <param name = "align">对齐方式</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByAlign(string align, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(align != null ? "`Align` = @Align" : "`Align` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (align != null)
				paras_.Add(Database.CreateInParameter("@Align", align, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByAlignAsync(string align, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(align != null ? "`Align` = @Align" : "`Align` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (align != null)
				paras_.Add(Database.CreateInParameter("@Align", align, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByAlign
		#region GetByWidth
		
		/// <summary>
		/// 按 Width（字段） 查询
		/// </summary>
		/// /// <param name = "width">列宽</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByWidth(string width)
		{
			return GetByWidth(width, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByWidthAsync(string width)
		{
			return await GetByWidthAsync(width, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Width（字段） 查询
		/// </summary>
		/// /// <param name = "width">列宽</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByWidth(string width, TransactionManager tm_)
		{
			return GetByWidth(width, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByWidthAsync(string width, TransactionManager tm_)
		{
			return await GetByWidthAsync(width, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Width（字段） 查询
		/// </summary>
		/// /// <param name = "width">列宽</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByWidth(string width, int top_)
		{
			return GetByWidth(width, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByWidthAsync(string width, int top_)
		{
			return await GetByWidthAsync(width, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Width（字段） 查询
		/// </summary>
		/// /// <param name = "width">列宽</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByWidth(string width, int top_, TransactionManager tm_)
		{
			return GetByWidth(width, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByWidthAsync(string width, int top_, TransactionManager tm_)
		{
			return await GetByWidthAsync(width, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Width（字段） 查询
		/// </summary>
		/// /// <param name = "width">列宽</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByWidth(string width, string sort_)
		{
			return GetByWidth(width, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByWidthAsync(string width, string sort_)
		{
			return await GetByWidthAsync(width, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Width（字段） 查询
		/// </summary>
		/// /// <param name = "width">列宽</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByWidth(string width, string sort_, TransactionManager tm_)
		{
			return GetByWidth(width, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByWidthAsync(string width, string sort_, TransactionManager tm_)
		{
			return await GetByWidthAsync(width, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Width（字段） 查询
		/// </summary>
		/// /// <param name = "width">列宽</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByWidth(string width, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(width != null ? "`Width` = @Width" : "`Width` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (width != null)
				paras_.Add(Database.CreateInParameter("@Width", width, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByWidthAsync(string width, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(width != null ? "`Width` = @Width" : "`Width` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (width != null)
				paras_.Add(Database.CreateInParameter("@Width", width, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByWidth
		#region GetByFlex
		
		/// <summary>
		/// 按 Flex（字段） 查询
		/// </summary>
		/// /// <param name = "flex">列宽flex</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFlex(int? flex)
		{
			return GetByFlex(flex, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFlexAsync(int? flex)
		{
			return await GetByFlexAsync(flex, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Flex（字段） 查询
		/// </summary>
		/// /// <param name = "flex">列宽flex</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFlex(int? flex, TransactionManager tm_)
		{
			return GetByFlex(flex, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFlexAsync(int? flex, TransactionManager tm_)
		{
			return await GetByFlexAsync(flex, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Flex（字段） 查询
		/// </summary>
		/// /// <param name = "flex">列宽flex</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFlex(int? flex, int top_)
		{
			return GetByFlex(flex, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFlexAsync(int? flex, int top_)
		{
			return await GetByFlexAsync(flex, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Flex（字段） 查询
		/// </summary>
		/// /// <param name = "flex">列宽flex</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFlex(int? flex, int top_, TransactionManager tm_)
		{
			return GetByFlex(flex, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFlexAsync(int? flex, int top_, TransactionManager tm_)
		{
			return await GetByFlexAsync(flex, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Flex（字段） 查询
		/// </summary>
		/// /// <param name = "flex">列宽flex</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFlex(int? flex, string sort_)
		{
			return GetByFlex(flex, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFlexAsync(int? flex, string sort_)
		{
			return await GetByFlexAsync(flex, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Flex（字段） 查询
		/// </summary>
		/// /// <param name = "flex">列宽flex</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFlex(int? flex, string sort_, TransactionManager tm_)
		{
			return GetByFlex(flex, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFlexAsync(int? flex, string sort_, TransactionManager tm_)
		{
			return await GetByFlexAsync(flex, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Flex（字段） 查询
		/// </summary>
		/// /// <param name = "flex">列宽flex</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFlex(int? flex, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(flex.HasValue ? "`Flex` = @Flex" : "`Flex` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (flex.HasValue)
				paras_.Add(Database.CreateInParameter("@Flex", flex.Value, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFlexAsync(int? flex, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(flex.HasValue ? "`Flex` = @Flex" : "`Flex` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (flex.HasValue)
				paras_.Add(Database.CreateInParameter("@Flex", flex.Value, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByFlex
		#region GetByLocked
		
		/// <summary>
		/// 按 Locked（字段） 查询
		/// </summary>
		/// /// <param name = "locked">是否锁定</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByLocked(bool locked)
		{
			return GetByLocked(locked, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByLockedAsync(bool locked)
		{
			return await GetByLockedAsync(locked, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Locked（字段） 查询
		/// </summary>
		/// /// <param name = "locked">是否锁定</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByLocked(bool locked, TransactionManager tm_)
		{
			return GetByLocked(locked, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByLockedAsync(bool locked, TransactionManager tm_)
		{
			return await GetByLockedAsync(locked, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Locked（字段） 查询
		/// </summary>
		/// /// <param name = "locked">是否锁定</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByLocked(bool locked, int top_)
		{
			return GetByLocked(locked, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByLockedAsync(bool locked, int top_)
		{
			return await GetByLockedAsync(locked, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Locked（字段） 查询
		/// </summary>
		/// /// <param name = "locked">是否锁定</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByLocked(bool locked, int top_, TransactionManager tm_)
		{
			return GetByLocked(locked, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByLockedAsync(bool locked, int top_, TransactionManager tm_)
		{
			return await GetByLockedAsync(locked, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Locked（字段） 查询
		/// </summary>
		/// /// <param name = "locked">是否锁定</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByLocked(bool locked, string sort_)
		{
			return GetByLocked(locked, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByLockedAsync(bool locked, string sort_)
		{
			return await GetByLockedAsync(locked, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Locked（字段） 查询
		/// </summary>
		/// /// <param name = "locked">是否锁定</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByLocked(bool locked, string sort_, TransactionManager tm_)
		{
			return GetByLocked(locked, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByLockedAsync(bool locked, string sort_, TransactionManager tm_)
		{
			return await GetByLockedAsync(locked, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Locked（字段） 查询
		/// </summary>
		/// /// <param name = "locked">是否锁定</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByLocked(bool locked, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Locked` = @Locked", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Locked", locked, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByLockedAsync(bool locked, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Locked` = @Locked", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Locked", locked, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByLocked
		#region GetByVisible
		
		/// <summary>
		/// 按 Visible（字段） 查询
		/// </summary>
		/// /// <param name = "visible">是否可见</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByVisible(bool visible)
		{
			return GetByVisible(visible, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByVisibleAsync(bool visible)
		{
			return await GetByVisibleAsync(visible, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Visible（字段） 查询
		/// </summary>
		/// /// <param name = "visible">是否可见</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByVisible(bool visible, TransactionManager tm_)
		{
			return GetByVisible(visible, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByVisibleAsync(bool visible, TransactionManager tm_)
		{
			return await GetByVisibleAsync(visible, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Visible（字段） 查询
		/// </summary>
		/// /// <param name = "visible">是否可见</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByVisible(bool visible, int top_)
		{
			return GetByVisible(visible, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByVisibleAsync(bool visible, int top_)
		{
			return await GetByVisibleAsync(visible, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Visible（字段） 查询
		/// </summary>
		/// /// <param name = "visible">是否可见</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByVisible(bool visible, int top_, TransactionManager tm_)
		{
			return GetByVisible(visible, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByVisibleAsync(bool visible, int top_, TransactionManager tm_)
		{
			return await GetByVisibleAsync(visible, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Visible（字段） 查询
		/// </summary>
		/// /// <param name = "visible">是否可见</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByVisible(bool visible, string sort_)
		{
			return GetByVisible(visible, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByVisibleAsync(bool visible, string sort_)
		{
			return await GetByVisibleAsync(visible, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Visible（字段） 查询
		/// </summary>
		/// /// <param name = "visible">是否可见</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByVisible(bool visible, string sort_, TransactionManager tm_)
		{
			return GetByVisible(visible, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByVisibleAsync(bool visible, string sort_, TransactionManager tm_)
		{
			return await GetByVisibleAsync(visible, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Visible（字段） 查询
		/// </summary>
		/// /// <param name = "visible">是否可见</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByVisible(bool visible, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Visible` = @Visible", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Visible", visible, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByVisibleAsync(bool visible, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Visible` = @Visible", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Visible", visible, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByVisible
		#region GetByFormat
		
		/// <summary>
		/// 按 Format（字段） 查询
		/// </summary>
		/// /// <param name = "format">显示格式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFormat(string format)
		{
			return GetByFormat(format, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFormatAsync(string format)
		{
			return await GetByFormatAsync(format, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Format（字段） 查询
		/// </summary>
		/// /// <param name = "format">显示格式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFormat(string format, TransactionManager tm_)
		{
			return GetByFormat(format, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFormatAsync(string format, TransactionManager tm_)
		{
			return await GetByFormatAsync(format, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Format（字段） 查询
		/// </summary>
		/// /// <param name = "format">显示格式</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFormat(string format, int top_)
		{
			return GetByFormat(format, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFormatAsync(string format, int top_)
		{
			return await GetByFormatAsync(format, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Format（字段） 查询
		/// </summary>
		/// /// <param name = "format">显示格式</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFormat(string format, int top_, TransactionManager tm_)
		{
			return GetByFormat(format, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFormatAsync(string format, int top_, TransactionManager tm_)
		{
			return await GetByFormatAsync(format, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Format（字段） 查询
		/// </summary>
		/// /// <param name = "format">显示格式</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFormat(string format, string sort_)
		{
			return GetByFormat(format, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFormatAsync(string format, string sort_)
		{
			return await GetByFormatAsync(format, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Format（字段） 查询
		/// </summary>
		/// /// <param name = "format">显示格式</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFormat(string format, string sort_, TransactionManager tm_)
		{
			return GetByFormat(format, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFormatAsync(string format, string sort_, TransactionManager tm_)
		{
			return await GetByFormatAsync(format, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Format（字段） 查询
		/// </summary>
		/// /// <param name = "format">显示格式</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFormat(string format, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(format != null ? "`Format` = @Format" : "`Format` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (format != null)
				paras_.Add(Database.CreateInParameter("@Format", format, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFormatAsync(string format, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(format != null ? "`Format` = @Format" : "`Format` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (format != null)
				paras_.Add(Database.CreateInParameter("@Format", format, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByFormat
		#region GetByTrueText
		
		/// <summary>
		/// 按 TrueText（字段） 查询
		/// </summary>
		/// /// <param name = "trueText">BooleanColumn true文本</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByTrueText(string trueText)
		{
			return GetByTrueText(trueText, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTrueTextAsync(string trueText)
		{
			return await GetByTrueTextAsync(trueText, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 TrueText（字段） 查询
		/// </summary>
		/// /// <param name = "trueText">BooleanColumn true文本</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByTrueText(string trueText, TransactionManager tm_)
		{
			return GetByTrueText(trueText, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTrueTextAsync(string trueText, TransactionManager tm_)
		{
			return await GetByTrueTextAsync(trueText, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 TrueText（字段） 查询
		/// </summary>
		/// /// <param name = "trueText">BooleanColumn true文本</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByTrueText(string trueText, int top_)
		{
			return GetByTrueText(trueText, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTrueTextAsync(string trueText, int top_)
		{
			return await GetByTrueTextAsync(trueText, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 TrueText（字段） 查询
		/// </summary>
		/// /// <param name = "trueText">BooleanColumn true文本</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByTrueText(string trueText, int top_, TransactionManager tm_)
		{
			return GetByTrueText(trueText, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTrueTextAsync(string trueText, int top_, TransactionManager tm_)
		{
			return await GetByTrueTextAsync(trueText, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 TrueText（字段） 查询
		/// </summary>
		/// /// <param name = "trueText">BooleanColumn true文本</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByTrueText(string trueText, string sort_)
		{
			return GetByTrueText(trueText, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTrueTextAsync(string trueText, string sort_)
		{
			return await GetByTrueTextAsync(trueText, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 TrueText（字段） 查询
		/// </summary>
		/// /// <param name = "trueText">BooleanColumn true文本</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByTrueText(string trueText, string sort_, TransactionManager tm_)
		{
			return GetByTrueText(trueText, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTrueTextAsync(string trueText, string sort_, TransactionManager tm_)
		{
			return await GetByTrueTextAsync(trueText, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 TrueText（字段） 查询
		/// </summary>
		/// /// <param name = "trueText">BooleanColumn true文本</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByTrueText(string trueText, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(trueText != null ? "`TrueText` = @TrueText" : "`TrueText` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (trueText != null)
				paras_.Add(Database.CreateInParameter("@TrueText", trueText, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByTrueTextAsync(string trueText, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(trueText != null ? "`TrueText` = @TrueText" : "`TrueText` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (trueText != null)
				paras_.Add(Database.CreateInParameter("@TrueText", trueText, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByTrueText
		#region GetByFalseText
		
		/// <summary>
		/// 按 FalseText（字段） 查询
		/// </summary>
		/// /// <param name = "falseText">BooleanColumn false文本</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFalseText(string falseText)
		{
			return GetByFalseText(falseText, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFalseTextAsync(string falseText)
		{
			return await GetByFalseTextAsync(falseText, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 FalseText（字段） 查询
		/// </summary>
		/// /// <param name = "falseText">BooleanColumn false文本</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFalseText(string falseText, TransactionManager tm_)
		{
			return GetByFalseText(falseText, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFalseTextAsync(string falseText, TransactionManager tm_)
		{
			return await GetByFalseTextAsync(falseText, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 FalseText（字段） 查询
		/// </summary>
		/// /// <param name = "falseText">BooleanColumn false文本</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFalseText(string falseText, int top_)
		{
			return GetByFalseText(falseText, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFalseTextAsync(string falseText, int top_)
		{
			return await GetByFalseTextAsync(falseText, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 FalseText（字段） 查询
		/// </summary>
		/// /// <param name = "falseText">BooleanColumn false文本</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFalseText(string falseText, int top_, TransactionManager tm_)
		{
			return GetByFalseText(falseText, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFalseTextAsync(string falseText, int top_, TransactionManager tm_)
		{
			return await GetByFalseTextAsync(falseText, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 FalseText（字段） 查询
		/// </summary>
		/// /// <param name = "falseText">BooleanColumn false文本</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFalseText(string falseText, string sort_)
		{
			return GetByFalseText(falseText, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFalseTextAsync(string falseText, string sort_)
		{
			return await GetByFalseTextAsync(falseText, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 FalseText（字段） 查询
		/// </summary>
		/// /// <param name = "falseText">BooleanColumn false文本</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFalseText(string falseText, string sort_, TransactionManager tm_)
		{
			return GetByFalseText(falseText, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFalseTextAsync(string falseText, string sort_, TransactionManager tm_)
		{
			return await GetByFalseTextAsync(falseText, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 FalseText（字段） 查询
		/// </summary>
		/// /// <param name = "falseText">BooleanColumn false文本</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFalseText(string falseText, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(falseText != null ? "`FalseText` = @FalseText" : "`FalseText` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (falseText != null)
				paras_.Add(Database.CreateInParameter("@FalseText", falseText, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFalseTextAsync(string falseText, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(falseText != null ? "`FalseText` = @FalseText" : "`FalseText` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (falseText != null)
				paras_.Add(Database.CreateInParameter("@FalseText", falseText, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByFalseText
		#region GetByFilterType
		
		/// <summary>
		/// 按 FilterType（字段） 查询
		/// </summary>
		/// /// <param name = "filterType">过滤类型</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFilterType(string filterType)
		{
			return GetByFilterType(filterType, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFilterTypeAsync(string filterType)
		{
			return await GetByFilterTypeAsync(filterType, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 FilterType（字段） 查询
		/// </summary>
		/// /// <param name = "filterType">过滤类型</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFilterType(string filterType, TransactionManager tm_)
		{
			return GetByFilterType(filterType, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFilterTypeAsync(string filterType, TransactionManager tm_)
		{
			return await GetByFilterTypeAsync(filterType, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 FilterType（字段） 查询
		/// </summary>
		/// /// <param name = "filterType">过滤类型</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFilterType(string filterType, int top_)
		{
			return GetByFilterType(filterType, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFilterTypeAsync(string filterType, int top_)
		{
			return await GetByFilterTypeAsync(filterType, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 FilterType（字段） 查询
		/// </summary>
		/// /// <param name = "filterType">过滤类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFilterType(string filterType, int top_, TransactionManager tm_)
		{
			return GetByFilterType(filterType, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFilterTypeAsync(string filterType, int top_, TransactionManager tm_)
		{
			return await GetByFilterTypeAsync(filterType, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 FilterType（字段） 查询
		/// </summary>
		/// /// <param name = "filterType">过滤类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFilterType(string filterType, string sort_)
		{
			return GetByFilterType(filterType, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFilterTypeAsync(string filterType, string sort_)
		{
			return await GetByFilterTypeAsync(filterType, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 FilterType（字段） 查询
		/// </summary>
		/// /// <param name = "filterType">过滤类型</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFilterType(string filterType, string sort_, TransactionManager tm_)
		{
			return GetByFilterType(filterType, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFilterTypeAsync(string filterType, string sort_, TransactionManager tm_)
		{
			return await GetByFilterTypeAsync(filterType, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 FilterType（字段） 查询
		/// </summary>
		/// /// <param name = "filterType">过滤类型</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByFilterType(string filterType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(filterType != null ? "`FilterType` = @FilterType" : "`FilterType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (filterType != null)
				paras_.Add(Database.CreateInParameter("@FilterType", filterType, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByFilterTypeAsync(string filterType, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(filterType != null ? "`FilterType` = @FilterType" : "`FilterType` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (filterType != null)
				paras_.Add(Database.CreateInParameter("@FilterType", filterType, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByFilterType
		#region GetByRenderFn
		
		/// <summary>
		/// 按 RenderFn（字段） 查询
		/// </summary>
		/// /// <param name = "renderFn">输出显示函数名</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFn(string renderFn)
		{
			return GetByRenderFn(renderFn, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnAsync(string renderFn)
		{
			return await GetByRenderFnAsync(renderFn, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RenderFn（字段） 查询
		/// </summary>
		/// /// <param name = "renderFn">输出显示函数名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFn(string renderFn, TransactionManager tm_)
		{
			return GetByRenderFn(renderFn, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnAsync(string renderFn, TransactionManager tm_)
		{
			return await GetByRenderFnAsync(renderFn, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RenderFn（字段） 查询
		/// </summary>
		/// /// <param name = "renderFn">输出显示函数名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFn(string renderFn, int top_)
		{
			return GetByRenderFn(renderFn, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnAsync(string renderFn, int top_)
		{
			return await GetByRenderFnAsync(renderFn, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RenderFn（字段） 查询
		/// </summary>
		/// /// <param name = "renderFn">输出显示函数名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFn(string renderFn, int top_, TransactionManager tm_)
		{
			return GetByRenderFn(renderFn, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnAsync(string renderFn, int top_, TransactionManager tm_)
		{
			return await GetByRenderFnAsync(renderFn, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RenderFn（字段） 查询
		/// </summary>
		/// /// <param name = "renderFn">输出显示函数名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFn(string renderFn, string sort_)
		{
			return GetByRenderFn(renderFn, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnAsync(string renderFn, string sort_)
		{
			return await GetByRenderFnAsync(renderFn, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RenderFn（字段） 查询
		/// </summary>
		/// /// <param name = "renderFn">输出显示函数名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFn(string renderFn, string sort_, TransactionManager tm_)
		{
			return GetByRenderFn(renderFn, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnAsync(string renderFn, string sort_, TransactionManager tm_)
		{
			return await GetByRenderFnAsync(renderFn, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RenderFn（字段） 查询
		/// </summary>
		/// /// <param name = "renderFn">输出显示函数名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFn(string renderFn, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(renderFn != null ? "`RenderFn` = @RenderFn" : "`RenderFn` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (renderFn != null)
				paras_.Add(Database.CreateInParameter("@RenderFn", renderFn, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnAsync(string renderFn, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(renderFn != null ? "`RenderFn` = @RenderFn" : "`RenderFn` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (renderFn != null)
				paras_.Add(Database.CreateInParameter("@RenderFn", renderFn, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByRenderFn
		#region GetByRenderFnContent
		
		/// <summary>
		/// 按 RenderFnContent（字段） 查询
		/// </summary>
		/// /// <param name = "renderFnContent">输出显示函数内容</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFnContent(string renderFnContent)
		{
			return GetByRenderFnContent(renderFnContent, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnContentAsync(string renderFnContent)
		{
			return await GetByRenderFnContentAsync(renderFnContent, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RenderFnContent（字段） 查询
		/// </summary>
		/// /// <param name = "renderFnContent">输出显示函数内容</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFnContent(string renderFnContent, TransactionManager tm_)
		{
			return GetByRenderFnContent(renderFnContent, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnContentAsync(string renderFnContent, TransactionManager tm_)
		{
			return await GetByRenderFnContentAsync(renderFnContent, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RenderFnContent（字段） 查询
		/// </summary>
		/// /// <param name = "renderFnContent">输出显示函数内容</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFnContent(string renderFnContent, int top_)
		{
			return GetByRenderFnContent(renderFnContent, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnContentAsync(string renderFnContent, int top_)
		{
			return await GetByRenderFnContentAsync(renderFnContent, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RenderFnContent（字段） 查询
		/// </summary>
		/// /// <param name = "renderFnContent">输出显示函数内容</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFnContent(string renderFnContent, int top_, TransactionManager tm_)
		{
			return GetByRenderFnContent(renderFnContent, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnContentAsync(string renderFnContent, int top_, TransactionManager tm_)
		{
			return await GetByRenderFnContentAsync(renderFnContent, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RenderFnContent（字段） 查询
		/// </summary>
		/// /// <param name = "renderFnContent">输出显示函数内容</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFnContent(string renderFnContent, string sort_)
		{
			return GetByRenderFnContent(renderFnContent, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnContentAsync(string renderFnContent, string sort_)
		{
			return await GetByRenderFnContentAsync(renderFnContent, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RenderFnContent（字段） 查询
		/// </summary>
		/// /// <param name = "renderFnContent">输出显示函数内容</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFnContent(string renderFnContent, string sort_, TransactionManager tm_)
		{
			return GetByRenderFnContent(renderFnContent, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnContentAsync(string renderFnContent, string sort_, TransactionManager tm_)
		{
			return await GetByRenderFnContentAsync(renderFnContent, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RenderFnContent（字段） 查询
		/// </summary>
		/// /// <param name = "renderFnContent">输出显示函数内容</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderFnContent(string renderFnContent, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(renderFnContent != null ? "`RenderFnContent` = @RenderFnContent" : "`RenderFnContent` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (renderFnContent != null)
				paras_.Add(Database.CreateInParameter("@RenderFnContent", renderFnContent, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderFnContentAsync(string renderFnContent, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(renderFnContent != null ? "`RenderFnContent` = @RenderFnContent" : "`RenderFnContent` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (renderFnContent != null)
				paras_.Add(Database.CreateInParameter("@RenderFnContent", renderFnContent, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByRenderFnContent
		#region GetByRenderHandler
		
		/// <summary>
		/// 按 RenderHandler（字段） 查询
		/// </summary>
		/// /// <param name = "renderHandler">输出显示handler</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderHandler(string renderHandler)
		{
			return GetByRenderHandler(renderHandler, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderHandlerAsync(string renderHandler)
		{
			return await GetByRenderHandlerAsync(renderHandler, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RenderHandler（字段） 查询
		/// </summary>
		/// /// <param name = "renderHandler">输出显示handler</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderHandler(string renderHandler, TransactionManager tm_)
		{
			return GetByRenderHandler(renderHandler, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderHandlerAsync(string renderHandler, TransactionManager tm_)
		{
			return await GetByRenderHandlerAsync(renderHandler, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RenderHandler（字段） 查询
		/// </summary>
		/// /// <param name = "renderHandler">输出显示handler</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderHandler(string renderHandler, int top_)
		{
			return GetByRenderHandler(renderHandler, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderHandlerAsync(string renderHandler, int top_)
		{
			return await GetByRenderHandlerAsync(renderHandler, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RenderHandler（字段） 查询
		/// </summary>
		/// /// <param name = "renderHandler">输出显示handler</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderHandler(string renderHandler, int top_, TransactionManager tm_)
		{
			return GetByRenderHandler(renderHandler, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderHandlerAsync(string renderHandler, int top_, TransactionManager tm_)
		{
			return await GetByRenderHandlerAsync(renderHandler, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RenderHandler（字段） 查询
		/// </summary>
		/// /// <param name = "renderHandler">输出显示handler</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderHandler(string renderHandler, string sort_)
		{
			return GetByRenderHandler(renderHandler, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderHandlerAsync(string renderHandler, string sort_)
		{
			return await GetByRenderHandlerAsync(renderHandler, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RenderHandler（字段） 查询
		/// </summary>
		/// /// <param name = "renderHandler">输出显示handler</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderHandler(string renderHandler, string sort_, TransactionManager tm_)
		{
			return GetByRenderHandler(renderHandler, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderHandlerAsync(string renderHandler, string sort_, TransactionManager tm_)
		{
			return await GetByRenderHandlerAsync(renderHandler, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RenderHandler（字段） 查询
		/// </summary>
		/// /// <param name = "renderHandler">输出显示handler</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRenderHandler(string renderHandler, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(renderHandler != null ? "`RenderHandler` = @RenderHandler" : "`RenderHandler` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (renderHandler != null)
				paras_.Add(Database.CreateInParameter("@RenderHandler", renderHandler, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRenderHandlerAsync(string renderHandler, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(renderHandler != null ? "`RenderHandler` = @RenderHandler" : "`RenderHandler` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (renderHandler != null)
				paras_.Add(Database.CreateInParameter("@RenderHandler", renderHandler, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByRenderHandler
		#region GetByRecDate
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRecDate(DateTime recDate)
		{
			return GetByRecDate(recDate, 0, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRecDateAsync(DateTime recDate)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRecDate(DateTime recDate, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRecDateAsync(DateTime recDate, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRecDate(DateTime recDate, int top_)
		{
			return GetByRecDate(recDate, top_, string.Empty, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRecDateAsync(DateTime recDate, int top_)
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
		public List<Admin_listedit_gridcolumnEO> GetByRecDate(DateTime recDate, int top_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRecDateAsync(DateTime recDate, int top_, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listedit_gridcolumnEO> GetByRecDate(DateTime recDate, string sort_)
		{
			return GetByRecDate(recDate, 0, sort_, null);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRecDateAsync(DateTime recDate, string sort_)
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
		public List<Admin_listedit_gridcolumnEO> GetByRecDate(DateTime recDate, string sort_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, sort_, tm_);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRecDateAsync(DateTime recDate, string sort_, TransactionManager tm_)
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
		public List<Admin_listedit_gridcolumnEO> GetByRecDate(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		public async Task<List<Admin_listedit_gridcolumnEO>> GetByRecDateAsync(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listedit_gridcolumnEO.MapDataReader);
		}
		#endregion // GetByRecDate
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
