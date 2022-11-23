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
	/// 自定义SQL查询添加编辑模板数据
	/// 【表 admin_listedit 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_listeditEO : IRowMapper<Admin_listeditEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_listeditEO()
		{
			this.PageSize = 20;
			this.GridHeight = 600;
			this.HasDelete = false;
			this.HasAdd = false;
			this.HasEdit = false;
			this.HasView = false;
			this.EditWidth = 0;
			this.HasDialog = false;
			this.DialogWidth = 0;
			this.DialogHeight = 0;
			this.Status = 1;
			this.RecDate = DateTime.Now;
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
		private long _originalGenID;
		/// <summary>
		/// 【数据库中的原始主键 GenID 值的副本，用于主键值更新】
		/// </summary>
		public long OriginalGenID
		{
			get { return _originalGenID; }
			set { HasOriginal = true; _originalGenID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "GenID", GenID }, };
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
		public long GenID { get; set; }
		/// <summary>
		/// 查询连接字符串名
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 2)]
		public string ConnectionStringName { get; set; }
		/// <summary>
		/// 原始SQL
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 3)]
		public string QuerySQLSource { get; set; }
		/// <summary>
		/// SQL解析后SelectStatement的json
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 4)]
		public string QuerySQL { get; set; }
		/// <summary>
		/// 查询页面Title
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 5)]
		public string QueryTitle { get; set; }
		/// <summary>
		/// Grid页大小
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 6)]
		public int PageSize { get; set; }
		/// <summary>
		/// Grid高度
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 7)]
		public int GridHeight { get; set; }
		/// <summary>
		/// 删除表名
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 8)]
		public string TableName { get; set; }
		/// <summary>
		/// 主键集合json序列化
		/// 【字段 varchar(2000)】
		/// </summary>
		[DataMember(Order = 9)]
		public string PrimaryKeys { get; set; }
		/// <summary>
		/// 是否有删除
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 10)]
		public bool HasDelete { get; set; }
		/// <summary>
		/// 删除SQL
		/// 【字段 varchar(1000)】
		/// </summary>
		[DataMember(Order = 11)]
		public string DeleteSQL { get; set; }
		/// <summary>
		/// 是否有添加
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 12)]
		public bool HasAdd { get; set; }
		/// <summary>
		/// 添加SQL
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 13)]
		public string AddSQL { get; set; }
		/// <summary>
		/// 是否有编辑
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 14)]
		public bool HasEdit { get; set; }
		/// <summary>
		/// 是否有查看
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 15)]
		public bool HasView { get; set; }
		/// <summary>
		/// 编辑获取数据SQL
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 16)]
		public string SelectSQL { get; set; }
		/// <summary>
		/// 编辑SQL
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 17)]
		public string EditSQL { get; set; }
		/// <summary>
		/// 添加编辑页面Title
		/// 【字段 varchar(100)】
		/// </summary>
		[DataMember(Order = 18)]
		public string EditTitle { get; set; }
		/// <summary>
		/// 添加编辑页面宽度
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 19)]
		public int EditWidth { get; set; }
		/// <summary>
		/// 是否有Dialog功能（只支持单一主键）
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 20)]
		public bool HasDialog { get; set; }
		/// <summary>
		/// 对话框SQL
		/// 【字段 varchar(1000)】
		/// </summary>
		[DataMember(Order = 21)]
		public string DialogSQL { get; set; }
		/// <summary>
		/// 对话框列名
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 22)]
		public string DialogFieldName { get; set; }
		/// <summary>
		/// 对话框宽度
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 23)]
		public int DialogWidth { get; set; }
		/// <summary>
		/// 对话框高度
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 24)]
		public int DialogHeight { get; set; }
		/// <summary>
		/// 状态 0-初始 1-有效 2-无效
		/// 【字段 tinyint】
		/// </summary>
		[DataMember(Order = 25)]
		public int Status { get; set; }
		/// <summary>
		/// 记录日期
		/// 【字段 datetime】
		/// </summary>
		[DataMember(Order = 26)]
		public DateTime RecDate { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_listeditEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_listeditEO MapDataReader(IDataReader reader)
		{
		    Admin_listeditEO ret = new Admin_listeditEO();
			ret.GenID = reader.ToInt64("GenID");
			ret.OriginalGenID = ret.GenID;
			ret.ConnectionStringName = reader.ToString("ConnectionStringName");
			ret.QuerySQLSource = reader.ToString("QuerySQLSource");
			ret.QuerySQL = reader.ToString("QuerySQL");
			ret.QueryTitle = reader.ToString("QueryTitle");
			ret.PageSize = reader.ToInt32("PageSize");
			ret.GridHeight = reader.ToInt32("GridHeight");
			ret.TableName = reader.ToString("TableName");
			ret.PrimaryKeys = reader.ToString("PrimaryKeys");
			ret.HasDelete = reader.ToBoolean("HasDelete");
			ret.DeleteSQL = reader.ToString("DeleteSQL");
			ret.HasAdd = reader.ToBoolean("HasAdd");
			ret.AddSQL = reader.ToString("AddSQL");
			ret.HasEdit = reader.ToBoolean("HasEdit");
			ret.HasView = reader.ToBoolean("HasView");
			ret.SelectSQL = reader.ToString("SelectSQL");
			ret.EditSQL = reader.ToString("EditSQL");
			ret.EditTitle = reader.ToString("EditTitle");
			ret.EditWidth = reader.ToInt32("EditWidth");
			ret.HasDialog = reader.ToBoolean("HasDialog");
			ret.DialogSQL = reader.ToString("DialogSQL");
			ret.DialogFieldName = reader.ToString("DialogFieldName");
			ret.DialogWidth = reader.ToInt32("DialogWidth");
			ret.DialogHeight = reader.ToInt32("DialogHeight");
			ret.Status = reader.ToInt32("Status");
			ret.RecDate = reader.ToDateTime("RecDate");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 自定义SQL查询添加编辑模板数据
	/// 【表 admin_listedit 的操作类】
	/// </summary>
	public class Admin_listeditMO : MySqlTableMO<Admin_listeditEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_listedit`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_listeditMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_listeditMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_listeditMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_listeditEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			item.GenID = Database.ExecSqlScalar<long>(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_listeditEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			item.GenID = await Database.ExecSqlScalarAsync<long>(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_listeditEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_listedit` (`ConnectionStringName`, `QuerySQLSource`, `QuerySQL`, `QueryTitle`, `PageSize`, `GridHeight`, `TableName`, `PrimaryKeys`, `HasDelete`, `DeleteSQL`, `HasAdd`, `AddSQL`, `HasEdit`, `HasView`, `SelectSQL`, `EditSQL`, `EditTitle`, `EditWidth`, `HasDialog`, `DialogSQL`, `DialogFieldName`, `DialogWidth`, `DialogHeight`, `Status`, `RecDate`) VALUE (@ConnectionStringName, @QuerySQLSource, @QuerySQL, @QueryTitle, @PageSize, @GridHeight, @TableName, @PrimaryKeys, @HasDelete, @DeleteSQL, @HasAdd, @AddSQL, @HasEdit, @HasView, @SelectSQL, @EditSQL, @EditTitle, @EditWidth, @HasDialog, @DialogSQL, @DialogFieldName, @DialogWidth, @DialogHeight, @Status, @RecDate);SELECT LAST_INSERT_ID();";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ConnectionStringName", item.ConnectionStringName != null ? item.ConnectionStringName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QuerySQLSource", item.QuerySQLSource != null ? item.QuerySQLSource : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@QuerySQL", item.QuerySQL != null ? item.QuerySQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@QueryTitle", item.QueryTitle != null ? item.QueryTitle : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@PageSize", item.PageSize, MySqlDbType.Int32),
				Database.CreateInParameter("@GridHeight", item.GridHeight, MySqlDbType.Int32),
				Database.CreateInParameter("@TableName", item.TableName != null ? item.TableName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrimaryKeys", item.PrimaryKeys != null ? item.PrimaryKeys : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@HasDelete", item.HasDelete, MySqlDbType.Byte),
				Database.CreateInParameter("@DeleteSQL", item.DeleteSQL != null ? item.DeleteSQL : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@HasAdd", item.HasAdd, MySqlDbType.Byte),
				Database.CreateInParameter("@AddSQL", item.AddSQL != null ? item.AddSQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@HasEdit", item.HasEdit, MySqlDbType.Byte),
				Database.CreateInParameter("@HasView", item.HasView, MySqlDbType.Byte),
				Database.CreateInParameter("@SelectSQL", item.SelectSQL != null ? item.SelectSQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@EditSQL", item.EditSQL != null ? item.EditSQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@EditTitle", item.EditTitle != null ? item.EditTitle : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditWidth", item.EditWidth, MySqlDbType.Int32),
				Database.CreateInParameter("@HasDialog", item.HasDialog, MySqlDbType.Byte),
				Database.CreateInParameter("@DialogSQL", item.DialogSQL != null ? item.DialogSQL : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DialogFieldName", item.DialogFieldName != null ? item.DialogFieldName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DialogWidth", item.DialogWidth, MySqlDbType.Int32),
				Database.CreateInParameter("@DialogHeight", item.DialogHeight, MySqlDbType.Int32),
				Database.CreateInParameter("@Status", item.Status, MySqlDbType.Byte),
				Database.CreateInParameter("@RecDate", item.RecDate, MySqlDbType.DateTime),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(long genID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(genID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(long genID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(genID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(long genID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_listeditEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.GenID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_listeditEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.GenID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveByConnectionStringName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "connectionStringName">查询连接字符串名</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByConnectionStringName(string connectionStringName, TransactionManager tm_ = null)
		{
			RepairRemoveByConnectionStringNameData(connectionStringName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByConnectionStringNameAsync(string connectionStringName, TransactionManager tm_ = null)
		{
			RepairRemoveByConnectionStringNameData(connectionStringName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByConnectionStringNameData(string connectionStringName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (connectionStringName != null ? "`ConnectionStringName` = @ConnectionStringName" : "`ConnectionStringName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (connectionStringName != null)
				paras_.Add(Database.CreateInParameter("@ConnectionStringName", connectionStringName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByConnectionStringName
		#region RemoveByQuerySQLSource
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "querySQLSource">原始SQL</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByQuerySQLSource(string querySQLSource, TransactionManager tm_ = null)
		{
			RepairRemoveByQuerySQLSourceData(querySQLSource, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByQuerySQLSourceAsync(string querySQLSource, TransactionManager tm_ = null)
		{
			RepairRemoveByQuerySQLSourceData(querySQLSource, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByQuerySQLSourceData(string querySQLSource, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (querySQLSource != null ? "`QuerySQLSource` = @QuerySQLSource" : "`QuerySQLSource` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (querySQLSource != null)
				paras_.Add(Database.CreateInParameter("@QuerySQLSource", querySQLSource, MySqlDbType.Text));
		}
		#endregion // RemoveByQuerySQLSource
		#region RemoveByQuerySQL
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "querySQL">SQL解析后SelectStatement的json</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByQuerySQL(string querySQL, TransactionManager tm_ = null)
		{
			RepairRemoveByQuerySQLData(querySQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByQuerySQLAsync(string querySQL, TransactionManager tm_ = null)
		{
			RepairRemoveByQuerySQLData(querySQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByQuerySQLData(string querySQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (querySQL != null ? "`QuerySQL` = @QuerySQL" : "`QuerySQL` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (querySQL != null)
				paras_.Add(Database.CreateInParameter("@QuerySQL", querySQL, MySqlDbType.Text));
		}
		#endregion // RemoveByQuerySQL
		#region RemoveByQueryTitle
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "queryTitle">查询页面Title</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByQueryTitle(string queryTitle, TransactionManager tm_ = null)
		{
			RepairRemoveByQueryTitleData(queryTitle, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByQueryTitleAsync(string queryTitle, TransactionManager tm_ = null)
		{
			RepairRemoveByQueryTitleData(queryTitle, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByQueryTitleData(string queryTitle, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (queryTitle != null ? "`QueryTitle` = @QueryTitle" : "`QueryTitle` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (queryTitle != null)
				paras_.Add(Database.CreateInParameter("@QueryTitle", queryTitle, MySqlDbType.VarChar));
		}
		#endregion // RemoveByQueryTitle
		#region RemoveByPageSize
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "pageSize">Grid页大小</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPageSize(int pageSize, TransactionManager tm_ = null)
		{
			RepairRemoveByPageSizeData(pageSize, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPageSizeAsync(int pageSize, TransactionManager tm_ = null)
		{
			RepairRemoveByPageSizeData(pageSize, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByPageSizeData(int pageSize, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE `PageSize` = @PageSize";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@PageSize", pageSize, MySqlDbType.Int32));
		}
		#endregion // RemoveByPageSize
		#region RemoveByGridHeight
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "gridHeight">Grid高度</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByGridHeight(int gridHeight, TransactionManager tm_ = null)
		{
			RepairRemoveByGridHeightData(gridHeight, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByGridHeightAsync(int gridHeight, TransactionManager tm_ = null)
		{
			RepairRemoveByGridHeightData(gridHeight, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByGridHeightData(int gridHeight, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE `GridHeight` = @GridHeight";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GridHeight", gridHeight, MySqlDbType.Int32));
		}
		#endregion // RemoveByGridHeight
		#region RemoveByTableName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "tableName">删除表名</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByTableName(string tableName, TransactionManager tm_ = null)
		{
			RepairRemoveByTableNameData(tableName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTableNameAsync(string tableName, TransactionManager tm_ = null)
		{
			RepairRemoveByTableNameData(tableName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTableNameData(string tableName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (tableName != null ? "`TableName` = @TableName" : "`TableName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (tableName != null)
				paras_.Add(Database.CreateInParameter("@TableName", tableName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByTableName
		#region RemoveByPrimaryKeys
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "primaryKeys">主键集合json序列化</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPrimaryKeys(string primaryKeys, TransactionManager tm_ = null)
		{
			RepairRemoveByPrimaryKeysData(primaryKeys, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPrimaryKeysAsync(string primaryKeys, TransactionManager tm_ = null)
		{
			RepairRemoveByPrimaryKeysData(primaryKeys, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByPrimaryKeysData(string primaryKeys, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (primaryKeys != null ? "`PrimaryKeys` = @PrimaryKeys" : "`PrimaryKeys` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (primaryKeys != null)
				paras_.Add(Database.CreateInParameter("@PrimaryKeys", primaryKeys, MySqlDbType.VarChar));
		}
		#endregion // RemoveByPrimaryKeys
		#region RemoveByHasDelete
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "hasDelete">是否有删除</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByHasDelete(bool hasDelete, TransactionManager tm_ = null)
		{
			RepairRemoveByHasDeleteData(hasDelete, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByHasDeleteAsync(bool hasDelete, TransactionManager tm_ = null)
		{
			RepairRemoveByHasDeleteData(hasDelete, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByHasDeleteData(bool hasDelete, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE `HasDelete` = @HasDelete";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasDelete", hasDelete, MySqlDbType.Byte));
		}
		#endregion // RemoveByHasDelete
		#region RemoveByDeleteSQL
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "deleteSQL">删除SQL</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByDeleteSQL(string deleteSQL, TransactionManager tm_ = null)
		{
			RepairRemoveByDeleteSQLData(deleteSQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByDeleteSQLAsync(string deleteSQL, TransactionManager tm_ = null)
		{
			RepairRemoveByDeleteSQLData(deleteSQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByDeleteSQLData(string deleteSQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (deleteSQL != null ? "`DeleteSQL` = @DeleteSQL" : "`DeleteSQL` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (deleteSQL != null)
				paras_.Add(Database.CreateInParameter("@DeleteSQL", deleteSQL, MySqlDbType.VarChar));
		}
		#endregion // RemoveByDeleteSQL
		#region RemoveByHasAdd
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "hasAdd">是否有添加</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByHasAdd(bool hasAdd, TransactionManager tm_ = null)
		{
			RepairRemoveByHasAddData(hasAdd, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByHasAddAsync(bool hasAdd, TransactionManager tm_ = null)
		{
			RepairRemoveByHasAddData(hasAdd, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByHasAddData(bool hasAdd, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE `HasAdd` = @HasAdd";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasAdd", hasAdd, MySqlDbType.Byte));
		}
		#endregion // RemoveByHasAdd
		#region RemoveByAddSQL
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "addSQL">添加SQL</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByAddSQL(string addSQL, TransactionManager tm_ = null)
		{
			RepairRemoveByAddSQLData(addSQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByAddSQLAsync(string addSQL, TransactionManager tm_ = null)
		{
			RepairRemoveByAddSQLData(addSQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByAddSQLData(string addSQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (addSQL != null ? "`AddSQL` = @AddSQL" : "`AddSQL` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (addSQL != null)
				paras_.Add(Database.CreateInParameter("@AddSQL", addSQL, MySqlDbType.Text));
		}
		#endregion // RemoveByAddSQL
		#region RemoveByHasEdit
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "hasEdit">是否有编辑</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByHasEdit(bool hasEdit, TransactionManager tm_ = null)
		{
			RepairRemoveByHasEditData(hasEdit, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByHasEditAsync(bool hasEdit, TransactionManager tm_ = null)
		{
			RepairRemoveByHasEditData(hasEdit, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByHasEditData(bool hasEdit, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE `HasEdit` = @HasEdit";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasEdit", hasEdit, MySqlDbType.Byte));
		}
		#endregion // RemoveByHasEdit
		#region RemoveByHasView
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "hasView">是否有查看</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByHasView(bool hasView, TransactionManager tm_ = null)
		{
			RepairRemoveByHasViewData(hasView, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByHasViewAsync(bool hasView, TransactionManager tm_ = null)
		{
			RepairRemoveByHasViewData(hasView, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByHasViewData(bool hasView, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE `HasView` = @HasView";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasView", hasView, MySqlDbType.Byte));
		}
		#endregion // RemoveByHasView
		#region RemoveBySelectSQL
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "selectSQL">编辑获取数据SQL</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveBySelectSQL(string selectSQL, TransactionManager tm_ = null)
		{
			RepairRemoveBySelectSQLData(selectSQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveBySelectSQLAsync(string selectSQL, TransactionManager tm_ = null)
		{
			RepairRemoveBySelectSQLData(selectSQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveBySelectSQLData(string selectSQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (selectSQL != null ? "`SelectSQL` = @SelectSQL" : "`SelectSQL` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (selectSQL != null)
				paras_.Add(Database.CreateInParameter("@SelectSQL", selectSQL, MySqlDbType.Text));
		}
		#endregion // RemoveBySelectSQL
		#region RemoveByEditSQL
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "editSQL">编辑SQL</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByEditSQL(string editSQL, TransactionManager tm_ = null)
		{
			RepairRemoveByEditSQLData(editSQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByEditSQLAsync(string editSQL, TransactionManager tm_ = null)
		{
			RepairRemoveByEditSQLData(editSQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByEditSQLData(string editSQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (editSQL != null ? "`EditSQL` = @EditSQL" : "`EditSQL` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (editSQL != null)
				paras_.Add(Database.CreateInParameter("@EditSQL", editSQL, MySqlDbType.Text));
		}
		#endregion // RemoveByEditSQL
		#region RemoveByEditTitle
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "editTitle">添加编辑页面Title</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByEditTitle(string editTitle, TransactionManager tm_ = null)
		{
			RepairRemoveByEditTitleData(editTitle, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByEditTitleAsync(string editTitle, TransactionManager tm_ = null)
		{
			RepairRemoveByEditTitleData(editTitle, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByEditTitleData(string editTitle, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (editTitle != null ? "`EditTitle` = @EditTitle" : "`EditTitle` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (editTitle != null)
				paras_.Add(Database.CreateInParameter("@EditTitle", editTitle, MySqlDbType.VarChar));
		}
		#endregion // RemoveByEditTitle
		#region RemoveByEditWidth
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "editWidth">添加编辑页面宽度</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByEditWidth(int editWidth, TransactionManager tm_ = null)
		{
			RepairRemoveByEditWidthData(editWidth, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByEditWidthAsync(int editWidth, TransactionManager tm_ = null)
		{
			RepairRemoveByEditWidthData(editWidth, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByEditWidthData(int editWidth, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE `EditWidth` = @EditWidth";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@EditWidth", editWidth, MySqlDbType.Int32));
		}
		#endregion // RemoveByEditWidth
		#region RemoveByHasDialog
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "hasDialog">是否有Dialog功能（只支持单一主键）</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByHasDialog(bool hasDialog, TransactionManager tm_ = null)
		{
			RepairRemoveByHasDialogData(hasDialog, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByHasDialogAsync(bool hasDialog, TransactionManager tm_ = null)
		{
			RepairRemoveByHasDialogData(hasDialog, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByHasDialogData(bool hasDialog, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE `HasDialog` = @HasDialog";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasDialog", hasDialog, MySqlDbType.Byte));
		}
		#endregion // RemoveByHasDialog
		#region RemoveByDialogSQL
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "dialogSQL">对话框SQL</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByDialogSQL(string dialogSQL, TransactionManager tm_ = null)
		{
			RepairRemoveByDialogSQLData(dialogSQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByDialogSQLAsync(string dialogSQL, TransactionManager tm_ = null)
		{
			RepairRemoveByDialogSQLData(dialogSQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByDialogSQLData(string dialogSQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (dialogSQL != null ? "`DialogSQL` = @DialogSQL" : "`DialogSQL` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (dialogSQL != null)
				paras_.Add(Database.CreateInParameter("@DialogSQL", dialogSQL, MySqlDbType.VarChar));
		}
		#endregion // RemoveByDialogSQL
		#region RemoveByDialogFieldName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "dialogFieldName">对话框列名</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByDialogFieldName(string dialogFieldName, TransactionManager tm_ = null)
		{
			RepairRemoveByDialogFieldNameData(dialogFieldName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByDialogFieldNameAsync(string dialogFieldName, TransactionManager tm_ = null)
		{
			RepairRemoveByDialogFieldNameData(dialogFieldName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByDialogFieldNameData(string dialogFieldName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE " + (dialogFieldName != null ? "`DialogFieldName` = @DialogFieldName" : "`DialogFieldName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (dialogFieldName != null)
				paras_.Add(Database.CreateInParameter("@DialogFieldName", dialogFieldName, MySqlDbType.VarChar));
		}
		#endregion // RemoveByDialogFieldName
		#region RemoveByDialogWidth
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "dialogWidth">对话框宽度</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByDialogWidth(int dialogWidth, TransactionManager tm_ = null)
		{
			RepairRemoveByDialogWidthData(dialogWidth, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByDialogWidthAsync(int dialogWidth, TransactionManager tm_ = null)
		{
			RepairRemoveByDialogWidthData(dialogWidth, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByDialogWidthData(int dialogWidth, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE `DialogWidth` = @DialogWidth";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@DialogWidth", dialogWidth, MySqlDbType.Int32));
		}
		#endregion // RemoveByDialogWidth
		#region RemoveByDialogHeight
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "dialogHeight">对话框高度</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByDialogHeight(int dialogHeight, TransactionManager tm_ = null)
		{
			RepairRemoveByDialogHeightData(dialogHeight, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByDialogHeightAsync(int dialogHeight, TransactionManager tm_ = null)
		{
			RepairRemoveByDialogHeightData(dialogHeight, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByDialogHeightData(int dialogHeight, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_listedit` WHERE `DialogHeight` = @DialogHeight";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@DialogHeight", dialogHeight, MySqlDbType.Int32));
		}
		#endregion // RemoveByDialogHeight
		#region RemoveByStatus
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "status">状态 0-初始 1-有效 2-无效</param>
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
			sql_ = @"DELETE FROM `admin_listedit` WHERE `Status` = @Status";
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
			sql_ = @"DELETE FROM `admin_listedit` WHERE `RecDate` = @RecDate";
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
		public int Put(Admin_listeditEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_listeditEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_listeditEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `ConnectionStringName` = @ConnectionStringName, `QuerySQLSource` = @QuerySQLSource, `QuerySQL` = @QuerySQL, `QueryTitle` = @QueryTitle, `PageSize` = @PageSize, `GridHeight` = @GridHeight, `TableName` = @TableName, `PrimaryKeys` = @PrimaryKeys, `HasDelete` = @HasDelete, `DeleteSQL` = @DeleteSQL, `HasAdd` = @HasAdd, `AddSQL` = @AddSQL, `HasEdit` = @HasEdit, `HasView` = @HasView, `SelectSQL` = @SelectSQL, `EditSQL` = @EditSQL, `EditTitle` = @EditTitle, `EditWidth` = @EditWidth, `HasDialog` = @HasDialog, `DialogSQL` = @DialogSQL, `DialogFieldName` = @DialogFieldName, `DialogWidth` = @DialogWidth, `DialogHeight` = @DialogHeight, `Status` = @Status WHERE `GenID` = @GenID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ConnectionStringName", item.ConnectionStringName != null ? item.ConnectionStringName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@QuerySQLSource", item.QuerySQLSource != null ? item.QuerySQLSource : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@QuerySQL", item.QuerySQL != null ? item.QuerySQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@QueryTitle", item.QueryTitle != null ? item.QueryTitle : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@PageSize", item.PageSize, MySqlDbType.Int32),
				Database.CreateInParameter("@GridHeight", item.GridHeight, MySqlDbType.Int32),
				Database.CreateInParameter("@TableName", item.TableName != null ? item.TableName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@PrimaryKeys", item.PrimaryKeys != null ? item.PrimaryKeys : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@HasDelete", item.HasDelete, MySqlDbType.Byte),
				Database.CreateInParameter("@DeleteSQL", item.DeleteSQL != null ? item.DeleteSQL : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@HasAdd", item.HasAdd, MySqlDbType.Byte),
				Database.CreateInParameter("@AddSQL", item.AddSQL != null ? item.AddSQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@HasEdit", item.HasEdit, MySqlDbType.Byte),
				Database.CreateInParameter("@HasView", item.HasView, MySqlDbType.Byte),
				Database.CreateInParameter("@SelectSQL", item.SelectSQL != null ? item.SelectSQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@EditSQL", item.EditSQL != null ? item.EditSQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@EditTitle", item.EditTitle != null ? item.EditTitle : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@EditWidth", item.EditWidth, MySqlDbType.Int32),
				Database.CreateInParameter("@HasDialog", item.HasDialog, MySqlDbType.Byte),
				Database.CreateInParameter("@DialogSQL", item.DialogSQL != null ? item.DialogSQL : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DialogFieldName", item.DialogFieldName != null ? item.DialogFieldName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@DialogWidth", item.DialogWidth, MySqlDbType.Int32),
				Database.CreateInParameter("@DialogHeight", item.DialogHeight, MySqlDbType.Int32),
				Database.CreateInParameter("@Status", item.Status, MySqlDbType.Byte),
				Database.CreateInParameter("@GenID_Original", item.HasOriginal ? item.OriginalGenID : item.GenID, MySqlDbType.Int64),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_listeditEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_listeditEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "genID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long genID, string set_, params object[] values_)
		{
			return Put(set_, "`GenID` = @GenID", ConcatValues(values_, genID));
		}
		public async Task<int> PutByPKAsync(long genID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`GenID` = @GenID", ConcatValues(values_, genID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long genID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`GenID` = @GenID", tm_, ConcatValues(values_, genID));
		}
		public async Task<int> PutByPKAsync(long genID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`GenID` = @GenID", tm_, ConcatValues(values_, genID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(long genID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
	        };
			return Put(set_, "`GenID` = @GenID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(long genID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
	        };
			return await PutAsync(set_, "`GenID` = @GenID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutConnectionStringName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "connectionStringName">查询连接字符串名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutConnectionStringNameByPK(long genID, string connectionStringName, TransactionManager tm_ = null)
		{
			RepairPutConnectionStringNameByPKData(genID, connectionStringName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutConnectionStringNameByPKAsync(long genID, string connectionStringName, TransactionManager tm_ = null)
		{
			RepairPutConnectionStringNameByPKData(genID, connectionStringName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutConnectionStringNameByPKData(long genID, string connectionStringName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `ConnectionStringName` = @ConnectionStringName  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ConnectionStringName", connectionStringName != null ? connectionStringName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "connectionStringName">查询连接字符串名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutConnectionStringName(string connectionStringName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `ConnectionStringName` = @ConnectionStringName";
			var parameter_ = Database.CreateInParameter("@ConnectionStringName", connectionStringName != null ? connectionStringName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutConnectionStringNameAsync(string connectionStringName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `ConnectionStringName` = @ConnectionStringName";
			var parameter_ = Database.CreateInParameter("@ConnectionStringName", connectionStringName != null ? connectionStringName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutConnectionStringName
		#region PutQuerySQLSource
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "querySQLSource">原始SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQuerySQLSourceByPK(long genID, string querySQLSource, TransactionManager tm_ = null)
		{
			RepairPutQuerySQLSourceByPKData(genID, querySQLSource, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutQuerySQLSourceByPKAsync(long genID, string querySQLSource, TransactionManager tm_ = null)
		{
			RepairPutQuerySQLSourceByPKData(genID, querySQLSource, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutQuerySQLSourceByPKData(long genID, string querySQLSource, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `QuerySQLSource` = @QuerySQLSource  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QuerySQLSource", querySQLSource != null ? querySQLSource : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "querySQLSource">原始SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQuerySQLSource(string querySQLSource, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `QuerySQLSource` = @QuerySQLSource";
			var parameter_ = Database.CreateInParameter("@QuerySQLSource", querySQLSource != null ? querySQLSource : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutQuerySQLSourceAsync(string querySQLSource, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `QuerySQLSource` = @QuerySQLSource";
			var parameter_ = Database.CreateInParameter("@QuerySQLSource", querySQLSource != null ? querySQLSource : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutQuerySQLSource
		#region PutQuerySQL
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "querySQL">SQL解析后SelectStatement的json</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQuerySQLByPK(long genID, string querySQL, TransactionManager tm_ = null)
		{
			RepairPutQuerySQLByPKData(genID, querySQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutQuerySQLByPKAsync(long genID, string querySQL, TransactionManager tm_ = null)
		{
			RepairPutQuerySQLByPKData(genID, querySQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutQuerySQLByPKData(long genID, string querySQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `QuerySQL` = @QuerySQL  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QuerySQL", querySQL != null ? querySQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "querySQL">SQL解析后SelectStatement的json</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQuerySQL(string querySQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `QuerySQL` = @QuerySQL";
			var parameter_ = Database.CreateInParameter("@QuerySQL", querySQL != null ? querySQL : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutQuerySQLAsync(string querySQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `QuerySQL` = @QuerySQL";
			var parameter_ = Database.CreateInParameter("@QuerySQL", querySQL != null ? querySQL : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutQuerySQL
		#region PutQueryTitle
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "queryTitle">查询页面Title</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQueryTitleByPK(long genID, string queryTitle, TransactionManager tm_ = null)
		{
			RepairPutQueryTitleByPKData(genID, queryTitle, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutQueryTitleByPKAsync(long genID, string queryTitle, TransactionManager tm_ = null)
		{
			RepairPutQueryTitleByPKData(genID, queryTitle, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutQueryTitleByPKData(long genID, string queryTitle, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `QueryTitle` = @QueryTitle  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@QueryTitle", queryTitle != null ? queryTitle : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "queryTitle">查询页面Title</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutQueryTitle(string queryTitle, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `QueryTitle` = @QueryTitle";
			var parameter_ = Database.CreateInParameter("@QueryTitle", queryTitle != null ? queryTitle : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutQueryTitleAsync(string queryTitle, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `QueryTitle` = @QueryTitle";
			var parameter_ = Database.CreateInParameter("@QueryTitle", queryTitle != null ? queryTitle : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutQueryTitle
		#region PutPageSize
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "pageSize">Grid页大小</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPageSizeByPK(long genID, int pageSize, TransactionManager tm_ = null)
		{
			RepairPutPageSizeByPKData(genID, pageSize, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutPageSizeByPKAsync(long genID, int pageSize, TransactionManager tm_ = null)
		{
			RepairPutPageSizeByPKData(genID, pageSize, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutPageSizeByPKData(long genID, int pageSize, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `PageSize` = @PageSize  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@PageSize", pageSize, MySqlDbType.Int32),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "pageSize">Grid页大小</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPageSize(int pageSize, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `PageSize` = @PageSize";
			var parameter_ = Database.CreateInParameter("@PageSize", pageSize, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutPageSizeAsync(int pageSize, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `PageSize` = @PageSize";
			var parameter_ = Database.CreateInParameter("@PageSize", pageSize, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutPageSize
		#region PutGridHeight
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "gridHeight">Grid高度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGridHeightByPK(long genID, int gridHeight, TransactionManager tm_ = null)
		{
			RepairPutGridHeightByPKData(genID, gridHeight, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutGridHeightByPKAsync(long genID, int gridHeight, TransactionManager tm_ = null)
		{
			RepairPutGridHeightByPKData(genID, gridHeight, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutGridHeightByPKData(long genID, int gridHeight, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `GridHeight` = @GridHeight  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GridHeight", gridHeight, MySqlDbType.Int32),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "gridHeight">Grid高度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGridHeight(int gridHeight, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `GridHeight` = @GridHeight";
			var parameter_ = Database.CreateInParameter("@GridHeight", gridHeight, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutGridHeightAsync(int gridHeight, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `GridHeight` = @GridHeight";
			var parameter_ = Database.CreateInParameter("@GridHeight", gridHeight, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutGridHeight
		#region PutTableName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "tableName">删除表名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTableNameByPK(long genID, string tableName, TransactionManager tm_ = null)
		{
			RepairPutTableNameByPKData(genID, tableName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTableNameByPKAsync(long genID, string tableName, TransactionManager tm_ = null)
		{
			RepairPutTableNameByPKData(genID, tableName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTableNameByPKData(long genID, string tableName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `TableName` = @TableName  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@TableName", tableName != null ? tableName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "tableName">删除表名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTableName(string tableName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `TableName` = @TableName";
			var parameter_ = Database.CreateInParameter("@TableName", tableName != null ? tableName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTableNameAsync(string tableName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `TableName` = @TableName";
			var parameter_ = Database.CreateInParameter("@TableName", tableName != null ? tableName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTableName
		#region PutPrimaryKeys
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "primaryKeys">主键集合json序列化</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPrimaryKeysByPK(long genID, string primaryKeys, TransactionManager tm_ = null)
		{
			RepairPutPrimaryKeysByPKData(genID, primaryKeys, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutPrimaryKeysByPKAsync(long genID, string primaryKeys, TransactionManager tm_ = null)
		{
			RepairPutPrimaryKeysByPKData(genID, primaryKeys, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutPrimaryKeysByPKData(long genID, string primaryKeys, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `PrimaryKeys` = @PrimaryKeys  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@PrimaryKeys", primaryKeys != null ? primaryKeys : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "primaryKeys">主键集合json序列化</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPrimaryKeys(string primaryKeys, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `PrimaryKeys` = @PrimaryKeys";
			var parameter_ = Database.CreateInParameter("@PrimaryKeys", primaryKeys != null ? primaryKeys : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutPrimaryKeysAsync(string primaryKeys, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `PrimaryKeys` = @PrimaryKeys";
			var parameter_ = Database.CreateInParameter("@PrimaryKeys", primaryKeys != null ? primaryKeys : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutPrimaryKeys
		#region PutHasDelete
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "hasDelete">是否有删除</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutHasDeleteByPK(long genID, bool hasDelete, TransactionManager tm_ = null)
		{
			RepairPutHasDeleteByPKData(genID, hasDelete, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutHasDeleteByPKAsync(long genID, bool hasDelete, TransactionManager tm_ = null)
		{
			RepairPutHasDeleteByPKData(genID, hasDelete, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutHasDeleteByPKData(long genID, bool hasDelete, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `HasDelete` = @HasDelete  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@HasDelete", hasDelete, MySqlDbType.Byte),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "hasDelete">是否有删除</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutHasDelete(bool hasDelete, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `HasDelete` = @HasDelete";
			var parameter_ = Database.CreateInParameter("@HasDelete", hasDelete, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutHasDeleteAsync(bool hasDelete, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `HasDelete` = @HasDelete";
			var parameter_ = Database.CreateInParameter("@HasDelete", hasDelete, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutHasDelete
		#region PutDeleteSQL
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "deleteSQL">删除SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDeleteSQLByPK(long genID, string deleteSQL, TransactionManager tm_ = null)
		{
			RepairPutDeleteSQLByPKData(genID, deleteSQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDeleteSQLByPKAsync(long genID, string deleteSQL, TransactionManager tm_ = null)
		{
			RepairPutDeleteSQLByPKData(genID, deleteSQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDeleteSQLByPKData(long genID, string deleteSQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `DeleteSQL` = @DeleteSQL  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DeleteSQL", deleteSQL != null ? deleteSQL : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "deleteSQL">删除SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDeleteSQL(string deleteSQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `DeleteSQL` = @DeleteSQL";
			var parameter_ = Database.CreateInParameter("@DeleteSQL", deleteSQL != null ? deleteSQL : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDeleteSQLAsync(string deleteSQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `DeleteSQL` = @DeleteSQL";
			var parameter_ = Database.CreateInParameter("@DeleteSQL", deleteSQL != null ? deleteSQL : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDeleteSQL
		#region PutHasAdd
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "hasAdd">是否有添加</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutHasAddByPK(long genID, bool hasAdd, TransactionManager tm_ = null)
		{
			RepairPutHasAddByPKData(genID, hasAdd, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutHasAddByPKAsync(long genID, bool hasAdd, TransactionManager tm_ = null)
		{
			RepairPutHasAddByPKData(genID, hasAdd, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutHasAddByPKData(long genID, bool hasAdd, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `HasAdd` = @HasAdd  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@HasAdd", hasAdd, MySqlDbType.Byte),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "hasAdd">是否有添加</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutHasAdd(bool hasAdd, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `HasAdd` = @HasAdd";
			var parameter_ = Database.CreateInParameter("@HasAdd", hasAdd, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutHasAddAsync(bool hasAdd, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `HasAdd` = @HasAdd";
			var parameter_ = Database.CreateInParameter("@HasAdd", hasAdd, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutHasAdd
		#region PutAddSQL
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "addSQL">添加SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutAddSQLByPK(long genID, string addSQL, TransactionManager tm_ = null)
		{
			RepairPutAddSQLByPKData(genID, addSQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAddSQLByPKAsync(long genID, string addSQL, TransactionManager tm_ = null)
		{
			RepairPutAddSQLByPKData(genID, addSQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutAddSQLByPKData(long genID, string addSQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `AddSQL` = @AddSQL  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@AddSQL", addSQL != null ? addSQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "addSQL">添加SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutAddSQL(string addSQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `AddSQL` = @AddSQL";
			var parameter_ = Database.CreateInParameter("@AddSQL", addSQL != null ? addSQL : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutAddSQLAsync(string addSQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `AddSQL` = @AddSQL";
			var parameter_ = Database.CreateInParameter("@AddSQL", addSQL != null ? addSQL : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutAddSQL
		#region PutHasEdit
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "hasEdit">是否有编辑</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutHasEditByPK(long genID, bool hasEdit, TransactionManager tm_ = null)
		{
			RepairPutHasEditByPKData(genID, hasEdit, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutHasEditByPKAsync(long genID, bool hasEdit, TransactionManager tm_ = null)
		{
			RepairPutHasEditByPKData(genID, hasEdit, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutHasEditByPKData(long genID, bool hasEdit, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `HasEdit` = @HasEdit  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@HasEdit", hasEdit, MySqlDbType.Byte),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "hasEdit">是否有编辑</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutHasEdit(bool hasEdit, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `HasEdit` = @HasEdit";
			var parameter_ = Database.CreateInParameter("@HasEdit", hasEdit, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutHasEditAsync(bool hasEdit, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `HasEdit` = @HasEdit";
			var parameter_ = Database.CreateInParameter("@HasEdit", hasEdit, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutHasEdit
		#region PutHasView
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "hasView">是否有查看</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutHasViewByPK(long genID, bool hasView, TransactionManager tm_ = null)
		{
			RepairPutHasViewByPKData(genID, hasView, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutHasViewByPKAsync(long genID, bool hasView, TransactionManager tm_ = null)
		{
			RepairPutHasViewByPKData(genID, hasView, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutHasViewByPKData(long genID, bool hasView, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `HasView` = @HasView  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@HasView", hasView, MySqlDbType.Byte),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "hasView">是否有查看</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutHasView(bool hasView, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `HasView` = @HasView";
			var parameter_ = Database.CreateInParameter("@HasView", hasView, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutHasViewAsync(bool hasView, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `HasView` = @HasView";
			var parameter_ = Database.CreateInParameter("@HasView", hasView, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutHasView
		#region PutSelectSQL
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "selectSQL">编辑获取数据SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutSelectSQLByPK(long genID, string selectSQL, TransactionManager tm_ = null)
		{
			RepairPutSelectSQLByPKData(genID, selectSQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutSelectSQLByPKAsync(long genID, string selectSQL, TransactionManager tm_ = null)
		{
			RepairPutSelectSQLByPKData(genID, selectSQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutSelectSQLByPKData(long genID, string selectSQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `SelectSQL` = @SelectSQL  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SelectSQL", selectSQL != null ? selectSQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "selectSQL">编辑获取数据SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutSelectSQL(string selectSQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `SelectSQL` = @SelectSQL";
			var parameter_ = Database.CreateInParameter("@SelectSQL", selectSQL != null ? selectSQL : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutSelectSQLAsync(string selectSQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `SelectSQL` = @SelectSQL";
			var parameter_ = Database.CreateInParameter("@SelectSQL", selectSQL != null ? selectSQL : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutSelectSQL
		#region PutEditSQL
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "editSQL">编辑SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditSQLByPK(long genID, string editSQL, TransactionManager tm_ = null)
		{
			RepairPutEditSQLByPKData(genID, editSQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutEditSQLByPKAsync(long genID, string editSQL, TransactionManager tm_ = null)
		{
			RepairPutEditSQLByPKData(genID, editSQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutEditSQLByPKData(long genID, string editSQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `EditSQL` = @EditSQL  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditSQL", editSQL != null ? editSQL : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "editSQL">编辑SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditSQL(string editSQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `EditSQL` = @EditSQL";
			var parameter_ = Database.CreateInParameter("@EditSQL", editSQL != null ? editSQL : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutEditSQLAsync(string editSQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `EditSQL` = @EditSQL";
			var parameter_ = Database.CreateInParameter("@EditSQL", editSQL != null ? editSQL : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutEditSQL
		#region PutEditTitle
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "editTitle">添加编辑页面Title</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditTitleByPK(long genID, string editTitle, TransactionManager tm_ = null)
		{
			RepairPutEditTitleByPKData(genID, editTitle, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutEditTitleByPKAsync(long genID, string editTitle, TransactionManager tm_ = null)
		{
			RepairPutEditTitleByPKData(genID, editTitle, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutEditTitleByPKData(long genID, string editTitle, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `EditTitle` = @EditTitle  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditTitle", editTitle != null ? editTitle : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "editTitle">添加编辑页面Title</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditTitle(string editTitle, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `EditTitle` = @EditTitle";
			var parameter_ = Database.CreateInParameter("@EditTitle", editTitle != null ? editTitle : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutEditTitleAsync(string editTitle, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `EditTitle` = @EditTitle";
			var parameter_ = Database.CreateInParameter("@EditTitle", editTitle != null ? editTitle : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutEditTitle
		#region PutEditWidth
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "editWidth">添加编辑页面宽度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditWidthByPK(long genID, int editWidth, TransactionManager tm_ = null)
		{
			RepairPutEditWidthByPKData(genID, editWidth, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutEditWidthByPKAsync(long genID, int editWidth, TransactionManager tm_ = null)
		{
			RepairPutEditWidthByPKData(genID, editWidth, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutEditWidthByPKData(long genID, int editWidth, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `EditWidth` = @EditWidth  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@EditWidth", editWidth, MySqlDbType.Int32),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "editWidth">添加编辑页面宽度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutEditWidth(int editWidth, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `EditWidth` = @EditWidth";
			var parameter_ = Database.CreateInParameter("@EditWidth", editWidth, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutEditWidthAsync(int editWidth, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `EditWidth` = @EditWidth";
			var parameter_ = Database.CreateInParameter("@EditWidth", editWidth, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutEditWidth
		#region PutHasDialog
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "hasDialog">是否有Dialog功能（只支持单一主键）</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutHasDialogByPK(long genID, bool hasDialog, TransactionManager tm_ = null)
		{
			RepairPutHasDialogByPKData(genID, hasDialog, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutHasDialogByPKAsync(long genID, bool hasDialog, TransactionManager tm_ = null)
		{
			RepairPutHasDialogByPKData(genID, hasDialog, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutHasDialogByPKData(long genID, bool hasDialog, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `HasDialog` = @HasDialog  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@HasDialog", hasDialog, MySqlDbType.Byte),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "hasDialog">是否有Dialog功能（只支持单一主键）</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutHasDialog(bool hasDialog, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `HasDialog` = @HasDialog";
			var parameter_ = Database.CreateInParameter("@HasDialog", hasDialog, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutHasDialogAsync(bool hasDialog, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `HasDialog` = @HasDialog";
			var parameter_ = Database.CreateInParameter("@HasDialog", hasDialog, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutHasDialog
		#region PutDialogSQL
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "dialogSQL">对话框SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDialogSQLByPK(long genID, string dialogSQL, TransactionManager tm_ = null)
		{
			RepairPutDialogSQLByPKData(genID, dialogSQL, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDialogSQLByPKAsync(long genID, string dialogSQL, TransactionManager tm_ = null)
		{
			RepairPutDialogSQLByPKData(genID, dialogSQL, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDialogSQLByPKData(long genID, string dialogSQL, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `DialogSQL` = @DialogSQL  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DialogSQL", dialogSQL != null ? dialogSQL : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "dialogSQL">对话框SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDialogSQL(string dialogSQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `DialogSQL` = @DialogSQL";
			var parameter_ = Database.CreateInParameter("@DialogSQL", dialogSQL != null ? dialogSQL : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDialogSQLAsync(string dialogSQL, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `DialogSQL` = @DialogSQL";
			var parameter_ = Database.CreateInParameter("@DialogSQL", dialogSQL != null ? dialogSQL : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDialogSQL
		#region PutDialogFieldName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "dialogFieldName">对话框列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDialogFieldNameByPK(long genID, string dialogFieldName, TransactionManager tm_ = null)
		{
			RepairPutDialogFieldNameByPKData(genID, dialogFieldName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDialogFieldNameByPKAsync(long genID, string dialogFieldName, TransactionManager tm_ = null)
		{
			RepairPutDialogFieldNameByPKData(genID, dialogFieldName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDialogFieldNameByPKData(long genID, string dialogFieldName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `DialogFieldName` = @DialogFieldName  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DialogFieldName", dialogFieldName != null ? dialogFieldName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "dialogFieldName">对话框列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDialogFieldName(string dialogFieldName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `DialogFieldName` = @DialogFieldName";
			var parameter_ = Database.CreateInParameter("@DialogFieldName", dialogFieldName != null ? dialogFieldName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDialogFieldNameAsync(string dialogFieldName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `DialogFieldName` = @DialogFieldName";
			var parameter_ = Database.CreateInParameter("@DialogFieldName", dialogFieldName != null ? dialogFieldName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDialogFieldName
		#region PutDialogWidth
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "dialogWidth">对话框宽度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDialogWidthByPK(long genID, int dialogWidth, TransactionManager tm_ = null)
		{
			RepairPutDialogWidthByPKData(genID, dialogWidth, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDialogWidthByPKAsync(long genID, int dialogWidth, TransactionManager tm_ = null)
		{
			RepairPutDialogWidthByPKData(genID, dialogWidth, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDialogWidthByPKData(long genID, int dialogWidth, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `DialogWidth` = @DialogWidth  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DialogWidth", dialogWidth, MySqlDbType.Int32),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "dialogWidth">对话框宽度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDialogWidth(int dialogWidth, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `DialogWidth` = @DialogWidth";
			var parameter_ = Database.CreateInParameter("@DialogWidth", dialogWidth, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDialogWidthAsync(int dialogWidth, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `DialogWidth` = @DialogWidth";
			var parameter_ = Database.CreateInParameter("@DialogWidth", dialogWidth, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDialogWidth
		#region PutDialogHeight
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "dialogHeight">对话框高度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDialogHeightByPK(long genID, int dialogHeight, TransactionManager tm_ = null)
		{
			RepairPutDialogHeightByPKData(genID, dialogHeight, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDialogHeightByPKAsync(long genID, int dialogHeight, TransactionManager tm_ = null)
		{
			RepairPutDialogHeightByPKData(genID, dialogHeight, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDialogHeightByPKData(long genID, int dialogHeight, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `DialogHeight` = @DialogHeight  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@DialogHeight", dialogHeight, MySqlDbType.Int32),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "dialogHeight">对话框高度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDialogHeight(int dialogHeight, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `DialogHeight` = @DialogHeight";
			var parameter_ = Database.CreateInParameter("@DialogHeight", dialogHeight, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDialogHeightAsync(int dialogHeight, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `DialogHeight` = @DialogHeight";
			var parameter_ = Database.CreateInParameter("@DialogHeight", dialogHeight, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDialogHeight
		#region PutStatus
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "status">状态 0-初始 1-有效 2-无效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutStatusByPK(long genID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(genID, status, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutStatusByPKAsync(long genID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(genID, status, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutStatusByPKData(long genID, int status, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `Status` = @Status  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Status", status, MySqlDbType.Byte),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "status">状态 0-初始 1-有效 2-无效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutStatus(int status, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `Status` = @Status";
			var parameter_ = Database.CreateInParameter("@Status", status, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutStatusAsync(int status, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `Status` = @Status";
			var parameter_ = Database.CreateInParameter("@Status", status, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutStatus
		#region PutRecDate
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutRecDateByPK(long genID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(genID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutRecDateByPKAsync(long genID, DateTime recDate, TransactionManager tm_ = null)
		{
			RepairPutRecDateByPKData(genID, recDate, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutRecDateByPKData(long genID, DateTime recDate, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_listedit` SET `RecDate` = @RecDate  WHERE `GenID` = @GenID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime),
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
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
			const string sql_ = @"UPDATE `admin_listedit` SET `RecDate` = @RecDate";
			var parameter_ = Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutRecDateAsync(DateTime recDate, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_listedit` SET `RecDate` = @RecDate";
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
		public bool Set(Admin_listeditEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.GenID) == null)
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
		public async Task<bool> SetAsync(Admin_listeditEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.GenID) == null)
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
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_listeditEO GetByPK(long genID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(genID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<Admin_listeditEO> GetByPKAsync(long genID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(genID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		private void RepairGetByPKData(long genID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`GenID` = @GenID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 ConnectionStringName（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetConnectionStringNameByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`ConnectionStringName`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetConnectionStringNameByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`ConnectionStringName`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 QuerySQLSource（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetQuerySQLSourceByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`QuerySQLSource`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetQuerySQLSourceByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`QuerySQLSource`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 QuerySQL（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetQuerySQLByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`QuerySQL`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetQuerySQLByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`QuerySQL`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 QueryTitle（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetQueryTitleByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`QueryTitle`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetQueryTitleByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`QueryTitle`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 PageSize（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetPageSizeByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`PageSize`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<int> GetPageSizeByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`PageSize`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 GridHeight（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetGridHeightByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`GridHeight`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<int> GetGridHeightByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`GridHeight`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 TableName（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetTableNameByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`TableName`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetTableNameByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`TableName`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 PrimaryKeys（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetPrimaryKeysByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`PrimaryKeys`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetPrimaryKeysByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`PrimaryKeys`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 HasDelete（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public bool GetHasDeleteByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (bool)GetScalar("`HasDelete`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<bool> GetHasDeleteByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (bool)await GetScalarAsync("`HasDelete`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 DeleteSQL（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetDeleteSQLByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`DeleteSQL`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetDeleteSQLByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`DeleteSQL`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 HasAdd（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public bool GetHasAddByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (bool)GetScalar("`HasAdd`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<bool> GetHasAddByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (bool)await GetScalarAsync("`HasAdd`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 AddSQL（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetAddSQLByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`AddSQL`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetAddSQLByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`AddSQL`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 HasEdit（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public bool GetHasEditByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (bool)GetScalar("`HasEdit`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<bool> GetHasEditByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (bool)await GetScalarAsync("`HasEdit`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 HasView（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public bool GetHasViewByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (bool)GetScalar("`HasView`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<bool> GetHasViewByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (bool)await GetScalarAsync("`HasView`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 SelectSQL（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetSelectSQLByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`SelectSQL`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetSelectSQLByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`SelectSQL`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 EditSQL（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetEditSQLByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`EditSQL`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetEditSQLByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`EditSQL`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 EditTitle（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetEditTitleByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`EditTitle`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetEditTitleByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`EditTitle`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 EditWidth（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetEditWidthByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`EditWidth`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<int> GetEditWidthByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`EditWidth`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 HasDialog（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public bool GetHasDialogByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (bool)GetScalar("`HasDialog`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<bool> GetHasDialogByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (bool)await GetScalarAsync("`HasDialog`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 DialogSQL（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetDialogSQLByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`DialogSQL`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetDialogSQLByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`DialogSQL`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 DialogFieldName（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetDialogFieldNameByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)GetScalar("`DialogFieldName`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<string> GetDialogFieldNameByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (string)await GetScalarAsync("`DialogFieldName`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 DialogWidth（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetDialogWidthByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`DialogWidth`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<int> GetDialogWidthByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`DialogWidth`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 DialogHeight（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetDialogHeightByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`DialogHeight`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<int> GetDialogHeightByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`DialogHeight`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Status（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetStatusByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)GetScalar("`Status`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<int> GetStatusByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (int)await GetScalarAsync("`Status`", "`GenID` = @GenID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 RecDate（字段）
		/// </summary>
		/// /// <param name = "genID">编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public DateTime GetRecDateByPK(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (DateTime)GetScalar("`RecDate`", "`GenID` = @GenID", paras_, tm_);
		}
		public async Task<DateTime> GetRecDateByPKAsync(long genID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID, MySqlDbType.Int64),
			};
			return (DateTime)await GetScalarAsync("`RecDate`", "`GenID` = @GenID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetByConnectionStringName
		
		/// <summary>
		/// 按 ConnectionStringName（字段） 查询
		/// </summary>
		/// /// <param name = "connectionStringName">查询连接字符串名</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByConnectionStringName(string connectionStringName)
		{
			return GetByConnectionStringName(connectionStringName, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByConnectionStringNameAsync(string connectionStringName)
		{
			return await GetByConnectionStringNameAsync(connectionStringName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ConnectionStringName（字段） 查询
		/// </summary>
		/// /// <param name = "connectionStringName">查询连接字符串名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByConnectionStringName(string connectionStringName, TransactionManager tm_)
		{
			return GetByConnectionStringName(connectionStringName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByConnectionStringNameAsync(string connectionStringName, TransactionManager tm_)
		{
			return await GetByConnectionStringNameAsync(connectionStringName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ConnectionStringName（字段） 查询
		/// </summary>
		/// /// <param name = "connectionStringName">查询连接字符串名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByConnectionStringName(string connectionStringName, int top_)
		{
			return GetByConnectionStringName(connectionStringName, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByConnectionStringNameAsync(string connectionStringName, int top_)
		{
			return await GetByConnectionStringNameAsync(connectionStringName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ConnectionStringName（字段） 查询
		/// </summary>
		/// /// <param name = "connectionStringName">查询连接字符串名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByConnectionStringName(string connectionStringName, int top_, TransactionManager tm_)
		{
			return GetByConnectionStringName(connectionStringName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByConnectionStringNameAsync(string connectionStringName, int top_, TransactionManager tm_)
		{
			return await GetByConnectionStringNameAsync(connectionStringName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ConnectionStringName（字段） 查询
		/// </summary>
		/// /// <param name = "connectionStringName">查询连接字符串名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByConnectionStringName(string connectionStringName, string sort_)
		{
			return GetByConnectionStringName(connectionStringName, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByConnectionStringNameAsync(string connectionStringName, string sort_)
		{
			return await GetByConnectionStringNameAsync(connectionStringName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ConnectionStringName（字段） 查询
		/// </summary>
		/// /// <param name = "connectionStringName">查询连接字符串名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByConnectionStringName(string connectionStringName, string sort_, TransactionManager tm_)
		{
			return GetByConnectionStringName(connectionStringName, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByConnectionStringNameAsync(string connectionStringName, string sort_, TransactionManager tm_)
		{
			return await GetByConnectionStringNameAsync(connectionStringName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ConnectionStringName（字段） 查询
		/// </summary>
		/// /// <param name = "connectionStringName">查询连接字符串名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByConnectionStringName(string connectionStringName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(connectionStringName != null ? "`ConnectionStringName` = @ConnectionStringName" : "`ConnectionStringName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (connectionStringName != null)
				paras_.Add(Database.CreateInParameter("@ConnectionStringName", connectionStringName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByConnectionStringNameAsync(string connectionStringName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(connectionStringName != null ? "`ConnectionStringName` = @ConnectionStringName" : "`ConnectionStringName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (connectionStringName != null)
				paras_.Add(Database.CreateInParameter("@ConnectionStringName", connectionStringName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByConnectionStringName
		#region GetByQuerySQLSource
		
		/// <summary>
		/// 按 QuerySQLSource（字段） 查询
		/// </summary>
		/// /// <param name = "querySQLSource">原始SQL</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQLSource(string querySQLSource)
		{
			return GetByQuerySQLSource(querySQLSource, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLSourceAsync(string querySQLSource)
		{
			return await GetByQuerySQLSourceAsync(querySQLSource, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QuerySQLSource（字段） 查询
		/// </summary>
		/// /// <param name = "querySQLSource">原始SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQLSource(string querySQLSource, TransactionManager tm_)
		{
			return GetByQuerySQLSource(querySQLSource, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLSourceAsync(string querySQLSource, TransactionManager tm_)
		{
			return await GetByQuerySQLSourceAsync(querySQLSource, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QuerySQLSource（字段） 查询
		/// </summary>
		/// /// <param name = "querySQLSource">原始SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQLSource(string querySQLSource, int top_)
		{
			return GetByQuerySQLSource(querySQLSource, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLSourceAsync(string querySQLSource, int top_)
		{
			return await GetByQuerySQLSourceAsync(querySQLSource, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QuerySQLSource（字段） 查询
		/// </summary>
		/// /// <param name = "querySQLSource">原始SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQLSource(string querySQLSource, int top_, TransactionManager tm_)
		{
			return GetByQuerySQLSource(querySQLSource, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLSourceAsync(string querySQLSource, int top_, TransactionManager tm_)
		{
			return await GetByQuerySQLSourceAsync(querySQLSource, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QuerySQLSource（字段） 查询
		/// </summary>
		/// /// <param name = "querySQLSource">原始SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQLSource(string querySQLSource, string sort_)
		{
			return GetByQuerySQLSource(querySQLSource, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLSourceAsync(string querySQLSource, string sort_)
		{
			return await GetByQuerySQLSourceAsync(querySQLSource, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 QuerySQLSource（字段） 查询
		/// </summary>
		/// /// <param name = "querySQLSource">原始SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQLSource(string querySQLSource, string sort_, TransactionManager tm_)
		{
			return GetByQuerySQLSource(querySQLSource, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLSourceAsync(string querySQLSource, string sort_, TransactionManager tm_)
		{
			return await GetByQuerySQLSourceAsync(querySQLSource, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 QuerySQLSource（字段） 查询
		/// </summary>
		/// /// <param name = "querySQLSource">原始SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQLSource(string querySQLSource, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(querySQLSource != null ? "`QuerySQLSource` = @QuerySQLSource" : "`QuerySQLSource` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (querySQLSource != null)
				paras_.Add(Database.CreateInParameter("@QuerySQLSource", querySQLSource, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLSourceAsync(string querySQLSource, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(querySQLSource != null ? "`QuerySQLSource` = @QuerySQLSource" : "`QuerySQLSource` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (querySQLSource != null)
				paras_.Add(Database.CreateInParameter("@QuerySQLSource", querySQLSource, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByQuerySQLSource
		#region GetByQuerySQL
		
		/// <summary>
		/// 按 QuerySQL（字段） 查询
		/// </summary>
		/// /// <param name = "querySQL">SQL解析后SelectStatement的json</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQL(string querySQL)
		{
			return GetByQuerySQL(querySQL, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLAsync(string querySQL)
		{
			return await GetByQuerySQLAsync(querySQL, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QuerySQL（字段） 查询
		/// </summary>
		/// /// <param name = "querySQL">SQL解析后SelectStatement的json</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQL(string querySQL, TransactionManager tm_)
		{
			return GetByQuerySQL(querySQL, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLAsync(string querySQL, TransactionManager tm_)
		{
			return await GetByQuerySQLAsync(querySQL, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QuerySQL（字段） 查询
		/// </summary>
		/// /// <param name = "querySQL">SQL解析后SelectStatement的json</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQL(string querySQL, int top_)
		{
			return GetByQuerySQL(querySQL, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLAsync(string querySQL, int top_)
		{
			return await GetByQuerySQLAsync(querySQL, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QuerySQL（字段） 查询
		/// </summary>
		/// /// <param name = "querySQL">SQL解析后SelectStatement的json</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQL(string querySQL, int top_, TransactionManager tm_)
		{
			return GetByQuerySQL(querySQL, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLAsync(string querySQL, int top_, TransactionManager tm_)
		{
			return await GetByQuerySQLAsync(querySQL, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QuerySQL（字段） 查询
		/// </summary>
		/// /// <param name = "querySQL">SQL解析后SelectStatement的json</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQL(string querySQL, string sort_)
		{
			return GetByQuerySQL(querySQL, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLAsync(string querySQL, string sort_)
		{
			return await GetByQuerySQLAsync(querySQL, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 QuerySQL（字段） 查询
		/// </summary>
		/// /// <param name = "querySQL">SQL解析后SelectStatement的json</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQL(string querySQL, string sort_, TransactionManager tm_)
		{
			return GetByQuerySQL(querySQL, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLAsync(string querySQL, string sort_, TransactionManager tm_)
		{
			return await GetByQuerySQLAsync(querySQL, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 QuerySQL（字段） 查询
		/// </summary>
		/// /// <param name = "querySQL">SQL解析后SelectStatement的json</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQuerySQL(string querySQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(querySQL != null ? "`QuerySQL` = @QuerySQL" : "`QuerySQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (querySQL != null)
				paras_.Add(Database.CreateInParameter("@QuerySQL", querySQL, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByQuerySQLAsync(string querySQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(querySQL != null ? "`QuerySQL` = @QuerySQL" : "`QuerySQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (querySQL != null)
				paras_.Add(Database.CreateInParameter("@QuerySQL", querySQL, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByQuerySQL
		#region GetByQueryTitle
		
		/// <summary>
		/// 按 QueryTitle（字段） 查询
		/// </summary>
		/// /// <param name = "queryTitle">查询页面Title</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQueryTitle(string queryTitle)
		{
			return GetByQueryTitle(queryTitle, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByQueryTitleAsync(string queryTitle)
		{
			return await GetByQueryTitleAsync(queryTitle, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QueryTitle（字段） 查询
		/// </summary>
		/// /// <param name = "queryTitle">查询页面Title</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQueryTitle(string queryTitle, TransactionManager tm_)
		{
			return GetByQueryTitle(queryTitle, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByQueryTitleAsync(string queryTitle, TransactionManager tm_)
		{
			return await GetByQueryTitleAsync(queryTitle, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QueryTitle（字段） 查询
		/// </summary>
		/// /// <param name = "queryTitle">查询页面Title</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQueryTitle(string queryTitle, int top_)
		{
			return GetByQueryTitle(queryTitle, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByQueryTitleAsync(string queryTitle, int top_)
		{
			return await GetByQueryTitleAsync(queryTitle, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 QueryTitle（字段） 查询
		/// </summary>
		/// /// <param name = "queryTitle">查询页面Title</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQueryTitle(string queryTitle, int top_, TransactionManager tm_)
		{
			return GetByQueryTitle(queryTitle, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByQueryTitleAsync(string queryTitle, int top_, TransactionManager tm_)
		{
			return await GetByQueryTitleAsync(queryTitle, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 QueryTitle（字段） 查询
		/// </summary>
		/// /// <param name = "queryTitle">查询页面Title</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQueryTitle(string queryTitle, string sort_)
		{
			return GetByQueryTitle(queryTitle, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByQueryTitleAsync(string queryTitle, string sort_)
		{
			return await GetByQueryTitleAsync(queryTitle, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 QueryTitle（字段） 查询
		/// </summary>
		/// /// <param name = "queryTitle">查询页面Title</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQueryTitle(string queryTitle, string sort_, TransactionManager tm_)
		{
			return GetByQueryTitle(queryTitle, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByQueryTitleAsync(string queryTitle, string sort_, TransactionManager tm_)
		{
			return await GetByQueryTitleAsync(queryTitle, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 QueryTitle（字段） 查询
		/// </summary>
		/// /// <param name = "queryTitle">查询页面Title</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByQueryTitle(string queryTitle, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(queryTitle != null ? "`QueryTitle` = @QueryTitle" : "`QueryTitle` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (queryTitle != null)
				paras_.Add(Database.CreateInParameter("@QueryTitle", queryTitle, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByQueryTitleAsync(string queryTitle, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(queryTitle != null ? "`QueryTitle` = @QueryTitle" : "`QueryTitle` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (queryTitle != null)
				paras_.Add(Database.CreateInParameter("@QueryTitle", queryTitle, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByQueryTitle
		#region GetByPageSize
		
		/// <summary>
		/// 按 PageSize（字段） 查询
		/// </summary>
		/// /// <param name = "pageSize">Grid页大小</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPageSize(int pageSize)
		{
			return GetByPageSize(pageSize, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByPageSizeAsync(int pageSize)
		{
			return await GetByPageSizeAsync(pageSize, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PageSize（字段） 查询
		/// </summary>
		/// /// <param name = "pageSize">Grid页大小</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPageSize(int pageSize, TransactionManager tm_)
		{
			return GetByPageSize(pageSize, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByPageSizeAsync(int pageSize, TransactionManager tm_)
		{
			return await GetByPageSizeAsync(pageSize, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PageSize（字段） 查询
		/// </summary>
		/// /// <param name = "pageSize">Grid页大小</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPageSize(int pageSize, int top_)
		{
			return GetByPageSize(pageSize, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByPageSizeAsync(int pageSize, int top_)
		{
			return await GetByPageSizeAsync(pageSize, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PageSize（字段） 查询
		/// </summary>
		/// /// <param name = "pageSize">Grid页大小</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPageSize(int pageSize, int top_, TransactionManager tm_)
		{
			return GetByPageSize(pageSize, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByPageSizeAsync(int pageSize, int top_, TransactionManager tm_)
		{
			return await GetByPageSizeAsync(pageSize, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PageSize（字段） 查询
		/// </summary>
		/// /// <param name = "pageSize">Grid页大小</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPageSize(int pageSize, string sort_)
		{
			return GetByPageSize(pageSize, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByPageSizeAsync(int pageSize, string sort_)
		{
			return await GetByPageSizeAsync(pageSize, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 PageSize（字段） 查询
		/// </summary>
		/// /// <param name = "pageSize">Grid页大小</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPageSize(int pageSize, string sort_, TransactionManager tm_)
		{
			return GetByPageSize(pageSize, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByPageSizeAsync(int pageSize, string sort_, TransactionManager tm_)
		{
			return await GetByPageSizeAsync(pageSize, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 PageSize（字段） 查询
		/// </summary>
		/// /// <param name = "pageSize">Grid页大小</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPageSize(int pageSize, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`PageSize` = @PageSize", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@PageSize", pageSize, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByPageSizeAsync(int pageSize, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`PageSize` = @PageSize", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@PageSize", pageSize, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByPageSize
		#region GetByGridHeight
		
		/// <summary>
		/// 按 GridHeight（字段） 查询
		/// </summary>
		/// /// <param name = "gridHeight">Grid高度</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByGridHeight(int gridHeight)
		{
			return GetByGridHeight(gridHeight, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByGridHeightAsync(int gridHeight)
		{
			return await GetByGridHeightAsync(gridHeight, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GridHeight（字段） 查询
		/// </summary>
		/// /// <param name = "gridHeight">Grid高度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByGridHeight(int gridHeight, TransactionManager tm_)
		{
			return GetByGridHeight(gridHeight, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByGridHeightAsync(int gridHeight, TransactionManager tm_)
		{
			return await GetByGridHeightAsync(gridHeight, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GridHeight（字段） 查询
		/// </summary>
		/// /// <param name = "gridHeight">Grid高度</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByGridHeight(int gridHeight, int top_)
		{
			return GetByGridHeight(gridHeight, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByGridHeightAsync(int gridHeight, int top_)
		{
			return await GetByGridHeightAsync(gridHeight, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GridHeight（字段） 查询
		/// </summary>
		/// /// <param name = "gridHeight">Grid高度</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByGridHeight(int gridHeight, int top_, TransactionManager tm_)
		{
			return GetByGridHeight(gridHeight, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByGridHeightAsync(int gridHeight, int top_, TransactionManager tm_)
		{
			return await GetByGridHeightAsync(gridHeight, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GridHeight（字段） 查询
		/// </summary>
		/// /// <param name = "gridHeight">Grid高度</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByGridHeight(int gridHeight, string sort_)
		{
			return GetByGridHeight(gridHeight, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByGridHeightAsync(int gridHeight, string sort_)
		{
			return await GetByGridHeightAsync(gridHeight, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 GridHeight（字段） 查询
		/// </summary>
		/// /// <param name = "gridHeight">Grid高度</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByGridHeight(int gridHeight, string sort_, TransactionManager tm_)
		{
			return GetByGridHeight(gridHeight, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByGridHeightAsync(int gridHeight, string sort_, TransactionManager tm_)
		{
			return await GetByGridHeightAsync(gridHeight, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 GridHeight（字段） 查询
		/// </summary>
		/// /// <param name = "gridHeight">Grid高度</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByGridHeight(int gridHeight, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`GridHeight` = @GridHeight", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GridHeight", gridHeight, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByGridHeightAsync(int gridHeight, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`GridHeight` = @GridHeight", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@GridHeight", gridHeight, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByGridHeight
		#region GetByTableName
		
		/// <summary>
		/// 按 TableName（字段） 查询
		/// </summary>
		/// /// <param name = "tableName">删除表名</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByTableName(string tableName)
		{
			return GetByTableName(tableName, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByTableNameAsync(string tableName)
		{
			return await GetByTableNameAsync(tableName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 TableName（字段） 查询
		/// </summary>
		/// /// <param name = "tableName">删除表名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByTableName(string tableName, TransactionManager tm_)
		{
			return GetByTableName(tableName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByTableNameAsync(string tableName, TransactionManager tm_)
		{
			return await GetByTableNameAsync(tableName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 TableName（字段） 查询
		/// </summary>
		/// /// <param name = "tableName">删除表名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByTableName(string tableName, int top_)
		{
			return GetByTableName(tableName, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByTableNameAsync(string tableName, int top_)
		{
			return await GetByTableNameAsync(tableName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 TableName（字段） 查询
		/// </summary>
		/// /// <param name = "tableName">删除表名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByTableName(string tableName, int top_, TransactionManager tm_)
		{
			return GetByTableName(tableName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByTableNameAsync(string tableName, int top_, TransactionManager tm_)
		{
			return await GetByTableNameAsync(tableName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 TableName（字段） 查询
		/// </summary>
		/// /// <param name = "tableName">删除表名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByTableName(string tableName, string sort_)
		{
			return GetByTableName(tableName, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByTableNameAsync(string tableName, string sort_)
		{
			return await GetByTableNameAsync(tableName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 TableName（字段） 查询
		/// </summary>
		/// /// <param name = "tableName">删除表名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByTableName(string tableName, string sort_, TransactionManager tm_)
		{
			return GetByTableName(tableName, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByTableNameAsync(string tableName, string sort_, TransactionManager tm_)
		{
			return await GetByTableNameAsync(tableName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 TableName（字段） 查询
		/// </summary>
		/// /// <param name = "tableName">删除表名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByTableName(string tableName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(tableName != null ? "`TableName` = @TableName" : "`TableName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (tableName != null)
				paras_.Add(Database.CreateInParameter("@TableName", tableName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByTableNameAsync(string tableName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(tableName != null ? "`TableName` = @TableName" : "`TableName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (tableName != null)
				paras_.Add(Database.CreateInParameter("@TableName", tableName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByTableName
		#region GetByPrimaryKeys
		
		/// <summary>
		/// 按 PrimaryKeys（字段） 查询
		/// </summary>
		/// /// <param name = "primaryKeys">主键集合json序列化</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPrimaryKeys(string primaryKeys)
		{
			return GetByPrimaryKeys(primaryKeys, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByPrimaryKeysAsync(string primaryKeys)
		{
			return await GetByPrimaryKeysAsync(primaryKeys, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrimaryKeys（字段） 查询
		/// </summary>
		/// /// <param name = "primaryKeys">主键集合json序列化</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPrimaryKeys(string primaryKeys, TransactionManager tm_)
		{
			return GetByPrimaryKeys(primaryKeys, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByPrimaryKeysAsync(string primaryKeys, TransactionManager tm_)
		{
			return await GetByPrimaryKeysAsync(primaryKeys, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrimaryKeys（字段） 查询
		/// </summary>
		/// /// <param name = "primaryKeys">主键集合json序列化</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPrimaryKeys(string primaryKeys, int top_)
		{
			return GetByPrimaryKeys(primaryKeys, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByPrimaryKeysAsync(string primaryKeys, int top_)
		{
			return await GetByPrimaryKeysAsync(primaryKeys, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrimaryKeys（字段） 查询
		/// </summary>
		/// /// <param name = "primaryKeys">主键集合json序列化</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPrimaryKeys(string primaryKeys, int top_, TransactionManager tm_)
		{
			return GetByPrimaryKeys(primaryKeys, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByPrimaryKeysAsync(string primaryKeys, int top_, TransactionManager tm_)
		{
			return await GetByPrimaryKeysAsync(primaryKeys, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrimaryKeys（字段） 查询
		/// </summary>
		/// /// <param name = "primaryKeys">主键集合json序列化</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPrimaryKeys(string primaryKeys, string sort_)
		{
			return GetByPrimaryKeys(primaryKeys, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByPrimaryKeysAsync(string primaryKeys, string sort_)
		{
			return await GetByPrimaryKeysAsync(primaryKeys, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 PrimaryKeys（字段） 查询
		/// </summary>
		/// /// <param name = "primaryKeys">主键集合json序列化</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPrimaryKeys(string primaryKeys, string sort_, TransactionManager tm_)
		{
			return GetByPrimaryKeys(primaryKeys, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByPrimaryKeysAsync(string primaryKeys, string sort_, TransactionManager tm_)
		{
			return await GetByPrimaryKeysAsync(primaryKeys, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 PrimaryKeys（字段） 查询
		/// </summary>
		/// /// <param name = "primaryKeys">主键集合json序列化</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByPrimaryKeys(string primaryKeys, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(primaryKeys != null ? "`PrimaryKeys` = @PrimaryKeys" : "`PrimaryKeys` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (primaryKeys != null)
				paras_.Add(Database.CreateInParameter("@PrimaryKeys", primaryKeys, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByPrimaryKeysAsync(string primaryKeys, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(primaryKeys != null ? "`PrimaryKeys` = @PrimaryKeys" : "`PrimaryKeys` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (primaryKeys != null)
				paras_.Add(Database.CreateInParameter("@PrimaryKeys", primaryKeys, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByPrimaryKeys
		#region GetByHasDelete
		
		/// <summary>
		/// 按 HasDelete（字段） 查询
		/// </summary>
		/// /// <param name = "hasDelete">是否有删除</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDelete(bool hasDelete)
		{
			return GetByHasDelete(hasDelete, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDeleteAsync(bool hasDelete)
		{
			return await GetByHasDeleteAsync(hasDelete, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 HasDelete（字段） 查询
		/// </summary>
		/// /// <param name = "hasDelete">是否有删除</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDelete(bool hasDelete, TransactionManager tm_)
		{
			return GetByHasDelete(hasDelete, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDeleteAsync(bool hasDelete, TransactionManager tm_)
		{
			return await GetByHasDeleteAsync(hasDelete, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 HasDelete（字段） 查询
		/// </summary>
		/// /// <param name = "hasDelete">是否有删除</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDelete(bool hasDelete, int top_)
		{
			return GetByHasDelete(hasDelete, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDeleteAsync(bool hasDelete, int top_)
		{
			return await GetByHasDeleteAsync(hasDelete, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 HasDelete（字段） 查询
		/// </summary>
		/// /// <param name = "hasDelete">是否有删除</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDelete(bool hasDelete, int top_, TransactionManager tm_)
		{
			return GetByHasDelete(hasDelete, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDeleteAsync(bool hasDelete, int top_, TransactionManager tm_)
		{
			return await GetByHasDeleteAsync(hasDelete, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 HasDelete（字段） 查询
		/// </summary>
		/// /// <param name = "hasDelete">是否有删除</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDelete(bool hasDelete, string sort_)
		{
			return GetByHasDelete(hasDelete, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDeleteAsync(bool hasDelete, string sort_)
		{
			return await GetByHasDeleteAsync(hasDelete, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 HasDelete（字段） 查询
		/// </summary>
		/// /// <param name = "hasDelete">是否有删除</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDelete(bool hasDelete, string sort_, TransactionManager tm_)
		{
			return GetByHasDelete(hasDelete, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDeleteAsync(bool hasDelete, string sort_, TransactionManager tm_)
		{
			return await GetByHasDeleteAsync(hasDelete, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 HasDelete（字段） 查询
		/// </summary>
		/// /// <param name = "hasDelete">是否有删除</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDelete(bool hasDelete, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`HasDelete` = @HasDelete", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasDelete", hasDelete, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDeleteAsync(bool hasDelete, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`HasDelete` = @HasDelete", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasDelete", hasDelete, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByHasDelete
		#region GetByDeleteSQL
		
		/// <summary>
		/// 按 DeleteSQL（字段） 查询
		/// </summary>
		/// /// <param name = "deleteSQL">删除SQL</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDeleteSQL(string deleteSQL)
		{
			return GetByDeleteSQL(deleteSQL, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDeleteSQLAsync(string deleteSQL)
		{
			return await GetByDeleteSQLAsync(deleteSQL, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DeleteSQL（字段） 查询
		/// </summary>
		/// /// <param name = "deleteSQL">删除SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDeleteSQL(string deleteSQL, TransactionManager tm_)
		{
			return GetByDeleteSQL(deleteSQL, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDeleteSQLAsync(string deleteSQL, TransactionManager tm_)
		{
			return await GetByDeleteSQLAsync(deleteSQL, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DeleteSQL（字段） 查询
		/// </summary>
		/// /// <param name = "deleteSQL">删除SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDeleteSQL(string deleteSQL, int top_)
		{
			return GetByDeleteSQL(deleteSQL, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDeleteSQLAsync(string deleteSQL, int top_)
		{
			return await GetByDeleteSQLAsync(deleteSQL, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DeleteSQL（字段） 查询
		/// </summary>
		/// /// <param name = "deleteSQL">删除SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDeleteSQL(string deleteSQL, int top_, TransactionManager tm_)
		{
			return GetByDeleteSQL(deleteSQL, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDeleteSQLAsync(string deleteSQL, int top_, TransactionManager tm_)
		{
			return await GetByDeleteSQLAsync(deleteSQL, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DeleteSQL（字段） 查询
		/// </summary>
		/// /// <param name = "deleteSQL">删除SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDeleteSQL(string deleteSQL, string sort_)
		{
			return GetByDeleteSQL(deleteSQL, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDeleteSQLAsync(string deleteSQL, string sort_)
		{
			return await GetByDeleteSQLAsync(deleteSQL, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 DeleteSQL（字段） 查询
		/// </summary>
		/// /// <param name = "deleteSQL">删除SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDeleteSQL(string deleteSQL, string sort_, TransactionManager tm_)
		{
			return GetByDeleteSQL(deleteSQL, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDeleteSQLAsync(string deleteSQL, string sort_, TransactionManager tm_)
		{
			return await GetByDeleteSQLAsync(deleteSQL, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 DeleteSQL（字段） 查询
		/// </summary>
		/// /// <param name = "deleteSQL">删除SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDeleteSQL(string deleteSQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(deleteSQL != null ? "`DeleteSQL` = @DeleteSQL" : "`DeleteSQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (deleteSQL != null)
				paras_.Add(Database.CreateInParameter("@DeleteSQL", deleteSQL, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByDeleteSQLAsync(string deleteSQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(deleteSQL != null ? "`DeleteSQL` = @DeleteSQL" : "`DeleteSQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (deleteSQL != null)
				paras_.Add(Database.CreateInParameter("@DeleteSQL", deleteSQL, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByDeleteSQL
		#region GetByHasAdd
		
		/// <summary>
		/// 按 HasAdd（字段） 查询
		/// </summary>
		/// /// <param name = "hasAdd">是否有添加</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasAdd(bool hasAdd)
		{
			return GetByHasAdd(hasAdd, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasAddAsync(bool hasAdd)
		{
			return await GetByHasAddAsync(hasAdd, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 HasAdd（字段） 查询
		/// </summary>
		/// /// <param name = "hasAdd">是否有添加</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasAdd(bool hasAdd, TransactionManager tm_)
		{
			return GetByHasAdd(hasAdd, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasAddAsync(bool hasAdd, TransactionManager tm_)
		{
			return await GetByHasAddAsync(hasAdd, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 HasAdd（字段） 查询
		/// </summary>
		/// /// <param name = "hasAdd">是否有添加</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasAdd(bool hasAdd, int top_)
		{
			return GetByHasAdd(hasAdd, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasAddAsync(bool hasAdd, int top_)
		{
			return await GetByHasAddAsync(hasAdd, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 HasAdd（字段） 查询
		/// </summary>
		/// /// <param name = "hasAdd">是否有添加</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasAdd(bool hasAdd, int top_, TransactionManager tm_)
		{
			return GetByHasAdd(hasAdd, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasAddAsync(bool hasAdd, int top_, TransactionManager tm_)
		{
			return await GetByHasAddAsync(hasAdd, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 HasAdd（字段） 查询
		/// </summary>
		/// /// <param name = "hasAdd">是否有添加</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasAdd(bool hasAdd, string sort_)
		{
			return GetByHasAdd(hasAdd, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasAddAsync(bool hasAdd, string sort_)
		{
			return await GetByHasAddAsync(hasAdd, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 HasAdd（字段） 查询
		/// </summary>
		/// /// <param name = "hasAdd">是否有添加</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasAdd(bool hasAdd, string sort_, TransactionManager tm_)
		{
			return GetByHasAdd(hasAdd, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasAddAsync(bool hasAdd, string sort_, TransactionManager tm_)
		{
			return await GetByHasAddAsync(hasAdd, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 HasAdd（字段） 查询
		/// </summary>
		/// /// <param name = "hasAdd">是否有添加</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasAdd(bool hasAdd, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`HasAdd` = @HasAdd", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasAdd", hasAdd, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByHasAddAsync(bool hasAdd, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`HasAdd` = @HasAdd", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasAdd", hasAdd, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByHasAdd
		#region GetByAddSQL
		
		/// <summary>
		/// 按 AddSQL（字段） 查询
		/// </summary>
		/// /// <param name = "addSQL">添加SQL</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByAddSQL(string addSQL)
		{
			return GetByAddSQL(addSQL, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByAddSQLAsync(string addSQL)
		{
			return await GetByAddSQLAsync(addSQL, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 AddSQL（字段） 查询
		/// </summary>
		/// /// <param name = "addSQL">添加SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByAddSQL(string addSQL, TransactionManager tm_)
		{
			return GetByAddSQL(addSQL, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByAddSQLAsync(string addSQL, TransactionManager tm_)
		{
			return await GetByAddSQLAsync(addSQL, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 AddSQL（字段） 查询
		/// </summary>
		/// /// <param name = "addSQL">添加SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByAddSQL(string addSQL, int top_)
		{
			return GetByAddSQL(addSQL, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByAddSQLAsync(string addSQL, int top_)
		{
			return await GetByAddSQLAsync(addSQL, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 AddSQL（字段） 查询
		/// </summary>
		/// /// <param name = "addSQL">添加SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByAddSQL(string addSQL, int top_, TransactionManager tm_)
		{
			return GetByAddSQL(addSQL, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByAddSQLAsync(string addSQL, int top_, TransactionManager tm_)
		{
			return await GetByAddSQLAsync(addSQL, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 AddSQL（字段） 查询
		/// </summary>
		/// /// <param name = "addSQL">添加SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByAddSQL(string addSQL, string sort_)
		{
			return GetByAddSQL(addSQL, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByAddSQLAsync(string addSQL, string sort_)
		{
			return await GetByAddSQLAsync(addSQL, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 AddSQL（字段） 查询
		/// </summary>
		/// /// <param name = "addSQL">添加SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByAddSQL(string addSQL, string sort_, TransactionManager tm_)
		{
			return GetByAddSQL(addSQL, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByAddSQLAsync(string addSQL, string sort_, TransactionManager tm_)
		{
			return await GetByAddSQLAsync(addSQL, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 AddSQL（字段） 查询
		/// </summary>
		/// /// <param name = "addSQL">添加SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByAddSQL(string addSQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(addSQL != null ? "`AddSQL` = @AddSQL" : "`AddSQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (addSQL != null)
				paras_.Add(Database.CreateInParameter("@AddSQL", addSQL, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByAddSQLAsync(string addSQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(addSQL != null ? "`AddSQL` = @AddSQL" : "`AddSQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (addSQL != null)
				paras_.Add(Database.CreateInParameter("@AddSQL", addSQL, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByAddSQL
		#region GetByHasEdit
		
		/// <summary>
		/// 按 HasEdit（字段） 查询
		/// </summary>
		/// /// <param name = "hasEdit">是否有编辑</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasEdit(bool hasEdit)
		{
			return GetByHasEdit(hasEdit, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasEditAsync(bool hasEdit)
		{
			return await GetByHasEditAsync(hasEdit, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 HasEdit（字段） 查询
		/// </summary>
		/// /// <param name = "hasEdit">是否有编辑</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasEdit(bool hasEdit, TransactionManager tm_)
		{
			return GetByHasEdit(hasEdit, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasEditAsync(bool hasEdit, TransactionManager tm_)
		{
			return await GetByHasEditAsync(hasEdit, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 HasEdit（字段） 查询
		/// </summary>
		/// /// <param name = "hasEdit">是否有编辑</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasEdit(bool hasEdit, int top_)
		{
			return GetByHasEdit(hasEdit, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasEditAsync(bool hasEdit, int top_)
		{
			return await GetByHasEditAsync(hasEdit, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 HasEdit（字段） 查询
		/// </summary>
		/// /// <param name = "hasEdit">是否有编辑</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasEdit(bool hasEdit, int top_, TransactionManager tm_)
		{
			return GetByHasEdit(hasEdit, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasEditAsync(bool hasEdit, int top_, TransactionManager tm_)
		{
			return await GetByHasEditAsync(hasEdit, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 HasEdit（字段） 查询
		/// </summary>
		/// /// <param name = "hasEdit">是否有编辑</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasEdit(bool hasEdit, string sort_)
		{
			return GetByHasEdit(hasEdit, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasEditAsync(bool hasEdit, string sort_)
		{
			return await GetByHasEditAsync(hasEdit, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 HasEdit（字段） 查询
		/// </summary>
		/// /// <param name = "hasEdit">是否有编辑</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasEdit(bool hasEdit, string sort_, TransactionManager tm_)
		{
			return GetByHasEdit(hasEdit, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasEditAsync(bool hasEdit, string sort_, TransactionManager tm_)
		{
			return await GetByHasEditAsync(hasEdit, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 HasEdit（字段） 查询
		/// </summary>
		/// /// <param name = "hasEdit">是否有编辑</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasEdit(bool hasEdit, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`HasEdit` = @HasEdit", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasEdit", hasEdit, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByHasEditAsync(bool hasEdit, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`HasEdit` = @HasEdit", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasEdit", hasEdit, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByHasEdit
		#region GetByHasView
		
		/// <summary>
		/// 按 HasView（字段） 查询
		/// </summary>
		/// /// <param name = "hasView">是否有查看</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasView(bool hasView)
		{
			return GetByHasView(hasView, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasViewAsync(bool hasView)
		{
			return await GetByHasViewAsync(hasView, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 HasView（字段） 查询
		/// </summary>
		/// /// <param name = "hasView">是否有查看</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasView(bool hasView, TransactionManager tm_)
		{
			return GetByHasView(hasView, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasViewAsync(bool hasView, TransactionManager tm_)
		{
			return await GetByHasViewAsync(hasView, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 HasView（字段） 查询
		/// </summary>
		/// /// <param name = "hasView">是否有查看</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasView(bool hasView, int top_)
		{
			return GetByHasView(hasView, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasViewAsync(bool hasView, int top_)
		{
			return await GetByHasViewAsync(hasView, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 HasView（字段） 查询
		/// </summary>
		/// /// <param name = "hasView">是否有查看</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasView(bool hasView, int top_, TransactionManager tm_)
		{
			return GetByHasView(hasView, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasViewAsync(bool hasView, int top_, TransactionManager tm_)
		{
			return await GetByHasViewAsync(hasView, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 HasView（字段） 查询
		/// </summary>
		/// /// <param name = "hasView">是否有查看</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasView(bool hasView, string sort_)
		{
			return GetByHasView(hasView, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasViewAsync(bool hasView, string sort_)
		{
			return await GetByHasViewAsync(hasView, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 HasView（字段） 查询
		/// </summary>
		/// /// <param name = "hasView">是否有查看</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasView(bool hasView, string sort_, TransactionManager tm_)
		{
			return GetByHasView(hasView, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasViewAsync(bool hasView, string sort_, TransactionManager tm_)
		{
			return await GetByHasViewAsync(hasView, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 HasView（字段） 查询
		/// </summary>
		/// /// <param name = "hasView">是否有查看</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasView(bool hasView, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`HasView` = @HasView", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasView", hasView, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByHasViewAsync(bool hasView, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`HasView` = @HasView", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasView", hasView, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByHasView
		#region GetBySelectSQL
		
		/// <summary>
		/// 按 SelectSQL（字段） 查询
		/// </summary>
		/// /// <param name = "selectSQL">编辑获取数据SQL</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetBySelectSQL(string selectSQL)
		{
			return GetBySelectSQL(selectSQL, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetBySelectSQLAsync(string selectSQL)
		{
			return await GetBySelectSQLAsync(selectSQL, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 SelectSQL（字段） 查询
		/// </summary>
		/// /// <param name = "selectSQL">编辑获取数据SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetBySelectSQL(string selectSQL, TransactionManager tm_)
		{
			return GetBySelectSQL(selectSQL, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetBySelectSQLAsync(string selectSQL, TransactionManager tm_)
		{
			return await GetBySelectSQLAsync(selectSQL, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 SelectSQL（字段） 查询
		/// </summary>
		/// /// <param name = "selectSQL">编辑获取数据SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetBySelectSQL(string selectSQL, int top_)
		{
			return GetBySelectSQL(selectSQL, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetBySelectSQLAsync(string selectSQL, int top_)
		{
			return await GetBySelectSQLAsync(selectSQL, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 SelectSQL（字段） 查询
		/// </summary>
		/// /// <param name = "selectSQL">编辑获取数据SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetBySelectSQL(string selectSQL, int top_, TransactionManager tm_)
		{
			return GetBySelectSQL(selectSQL, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetBySelectSQLAsync(string selectSQL, int top_, TransactionManager tm_)
		{
			return await GetBySelectSQLAsync(selectSQL, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 SelectSQL（字段） 查询
		/// </summary>
		/// /// <param name = "selectSQL">编辑获取数据SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetBySelectSQL(string selectSQL, string sort_)
		{
			return GetBySelectSQL(selectSQL, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetBySelectSQLAsync(string selectSQL, string sort_)
		{
			return await GetBySelectSQLAsync(selectSQL, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 SelectSQL（字段） 查询
		/// </summary>
		/// /// <param name = "selectSQL">编辑获取数据SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetBySelectSQL(string selectSQL, string sort_, TransactionManager tm_)
		{
			return GetBySelectSQL(selectSQL, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetBySelectSQLAsync(string selectSQL, string sort_, TransactionManager tm_)
		{
			return await GetBySelectSQLAsync(selectSQL, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 SelectSQL（字段） 查询
		/// </summary>
		/// /// <param name = "selectSQL">编辑获取数据SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetBySelectSQL(string selectSQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(selectSQL != null ? "`SelectSQL` = @SelectSQL" : "`SelectSQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (selectSQL != null)
				paras_.Add(Database.CreateInParameter("@SelectSQL", selectSQL, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetBySelectSQLAsync(string selectSQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(selectSQL != null ? "`SelectSQL` = @SelectSQL" : "`SelectSQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (selectSQL != null)
				paras_.Add(Database.CreateInParameter("@SelectSQL", selectSQL, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetBySelectSQL
		#region GetByEditSQL
		
		/// <summary>
		/// 按 EditSQL（字段） 查询
		/// </summary>
		/// /// <param name = "editSQL">编辑SQL</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditSQL(string editSQL)
		{
			return GetByEditSQL(editSQL, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByEditSQLAsync(string editSQL)
		{
			return await GetByEditSQLAsync(editSQL, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditSQL（字段） 查询
		/// </summary>
		/// /// <param name = "editSQL">编辑SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditSQL(string editSQL, TransactionManager tm_)
		{
			return GetByEditSQL(editSQL, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByEditSQLAsync(string editSQL, TransactionManager tm_)
		{
			return await GetByEditSQLAsync(editSQL, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditSQL（字段） 查询
		/// </summary>
		/// /// <param name = "editSQL">编辑SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditSQL(string editSQL, int top_)
		{
			return GetByEditSQL(editSQL, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByEditSQLAsync(string editSQL, int top_)
		{
			return await GetByEditSQLAsync(editSQL, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditSQL（字段） 查询
		/// </summary>
		/// /// <param name = "editSQL">编辑SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditSQL(string editSQL, int top_, TransactionManager tm_)
		{
			return GetByEditSQL(editSQL, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByEditSQLAsync(string editSQL, int top_, TransactionManager tm_)
		{
			return await GetByEditSQLAsync(editSQL, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditSQL（字段） 查询
		/// </summary>
		/// /// <param name = "editSQL">编辑SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditSQL(string editSQL, string sort_)
		{
			return GetByEditSQL(editSQL, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByEditSQLAsync(string editSQL, string sort_)
		{
			return await GetByEditSQLAsync(editSQL, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 EditSQL（字段） 查询
		/// </summary>
		/// /// <param name = "editSQL">编辑SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditSQL(string editSQL, string sort_, TransactionManager tm_)
		{
			return GetByEditSQL(editSQL, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByEditSQLAsync(string editSQL, string sort_, TransactionManager tm_)
		{
			return await GetByEditSQLAsync(editSQL, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 EditSQL（字段） 查询
		/// </summary>
		/// /// <param name = "editSQL">编辑SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditSQL(string editSQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(editSQL != null ? "`EditSQL` = @EditSQL" : "`EditSQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (editSQL != null)
				paras_.Add(Database.CreateInParameter("@EditSQL", editSQL, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByEditSQLAsync(string editSQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(editSQL != null ? "`EditSQL` = @EditSQL" : "`EditSQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (editSQL != null)
				paras_.Add(Database.CreateInParameter("@EditSQL", editSQL, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByEditSQL
		#region GetByEditTitle
		
		/// <summary>
		/// 按 EditTitle（字段） 查询
		/// </summary>
		/// /// <param name = "editTitle">添加编辑页面Title</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditTitle(string editTitle)
		{
			return GetByEditTitle(editTitle, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByEditTitleAsync(string editTitle)
		{
			return await GetByEditTitleAsync(editTitle, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditTitle（字段） 查询
		/// </summary>
		/// /// <param name = "editTitle">添加编辑页面Title</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditTitle(string editTitle, TransactionManager tm_)
		{
			return GetByEditTitle(editTitle, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByEditTitleAsync(string editTitle, TransactionManager tm_)
		{
			return await GetByEditTitleAsync(editTitle, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditTitle（字段） 查询
		/// </summary>
		/// /// <param name = "editTitle">添加编辑页面Title</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditTitle(string editTitle, int top_)
		{
			return GetByEditTitle(editTitle, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByEditTitleAsync(string editTitle, int top_)
		{
			return await GetByEditTitleAsync(editTitle, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditTitle（字段） 查询
		/// </summary>
		/// /// <param name = "editTitle">添加编辑页面Title</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditTitle(string editTitle, int top_, TransactionManager tm_)
		{
			return GetByEditTitle(editTitle, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByEditTitleAsync(string editTitle, int top_, TransactionManager tm_)
		{
			return await GetByEditTitleAsync(editTitle, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditTitle（字段） 查询
		/// </summary>
		/// /// <param name = "editTitle">添加编辑页面Title</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditTitle(string editTitle, string sort_)
		{
			return GetByEditTitle(editTitle, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByEditTitleAsync(string editTitle, string sort_)
		{
			return await GetByEditTitleAsync(editTitle, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 EditTitle（字段） 查询
		/// </summary>
		/// /// <param name = "editTitle">添加编辑页面Title</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditTitle(string editTitle, string sort_, TransactionManager tm_)
		{
			return GetByEditTitle(editTitle, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByEditTitleAsync(string editTitle, string sort_, TransactionManager tm_)
		{
			return await GetByEditTitleAsync(editTitle, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 EditTitle（字段） 查询
		/// </summary>
		/// /// <param name = "editTitle">添加编辑页面Title</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditTitle(string editTitle, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(editTitle != null ? "`EditTitle` = @EditTitle" : "`EditTitle` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (editTitle != null)
				paras_.Add(Database.CreateInParameter("@EditTitle", editTitle, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByEditTitleAsync(string editTitle, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(editTitle != null ? "`EditTitle` = @EditTitle" : "`EditTitle` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (editTitle != null)
				paras_.Add(Database.CreateInParameter("@EditTitle", editTitle, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByEditTitle
		#region GetByEditWidth
		
		/// <summary>
		/// 按 EditWidth（字段） 查询
		/// </summary>
		/// /// <param name = "editWidth">添加编辑页面宽度</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditWidth(int editWidth)
		{
			return GetByEditWidth(editWidth, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByEditWidthAsync(int editWidth)
		{
			return await GetByEditWidthAsync(editWidth, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditWidth（字段） 查询
		/// </summary>
		/// /// <param name = "editWidth">添加编辑页面宽度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditWidth(int editWidth, TransactionManager tm_)
		{
			return GetByEditWidth(editWidth, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByEditWidthAsync(int editWidth, TransactionManager tm_)
		{
			return await GetByEditWidthAsync(editWidth, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditWidth（字段） 查询
		/// </summary>
		/// /// <param name = "editWidth">添加编辑页面宽度</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditWidth(int editWidth, int top_)
		{
			return GetByEditWidth(editWidth, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByEditWidthAsync(int editWidth, int top_)
		{
			return await GetByEditWidthAsync(editWidth, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 EditWidth（字段） 查询
		/// </summary>
		/// /// <param name = "editWidth">添加编辑页面宽度</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditWidth(int editWidth, int top_, TransactionManager tm_)
		{
			return GetByEditWidth(editWidth, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByEditWidthAsync(int editWidth, int top_, TransactionManager tm_)
		{
			return await GetByEditWidthAsync(editWidth, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 EditWidth（字段） 查询
		/// </summary>
		/// /// <param name = "editWidth">添加编辑页面宽度</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditWidth(int editWidth, string sort_)
		{
			return GetByEditWidth(editWidth, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByEditWidthAsync(int editWidth, string sort_)
		{
			return await GetByEditWidthAsync(editWidth, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 EditWidth（字段） 查询
		/// </summary>
		/// /// <param name = "editWidth">添加编辑页面宽度</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditWidth(int editWidth, string sort_, TransactionManager tm_)
		{
			return GetByEditWidth(editWidth, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByEditWidthAsync(int editWidth, string sort_, TransactionManager tm_)
		{
			return await GetByEditWidthAsync(editWidth, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 EditWidth（字段） 查询
		/// </summary>
		/// /// <param name = "editWidth">添加编辑页面宽度</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByEditWidth(int editWidth, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`EditWidth` = @EditWidth", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@EditWidth", editWidth, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByEditWidthAsync(int editWidth, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`EditWidth` = @EditWidth", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@EditWidth", editWidth, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByEditWidth
		#region GetByHasDialog
		
		/// <summary>
		/// 按 HasDialog（字段） 查询
		/// </summary>
		/// /// <param name = "hasDialog">是否有Dialog功能（只支持单一主键）</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDialog(bool hasDialog)
		{
			return GetByHasDialog(hasDialog, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDialogAsync(bool hasDialog)
		{
			return await GetByHasDialogAsync(hasDialog, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 HasDialog（字段） 查询
		/// </summary>
		/// /// <param name = "hasDialog">是否有Dialog功能（只支持单一主键）</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDialog(bool hasDialog, TransactionManager tm_)
		{
			return GetByHasDialog(hasDialog, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDialogAsync(bool hasDialog, TransactionManager tm_)
		{
			return await GetByHasDialogAsync(hasDialog, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 HasDialog（字段） 查询
		/// </summary>
		/// /// <param name = "hasDialog">是否有Dialog功能（只支持单一主键）</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDialog(bool hasDialog, int top_)
		{
			return GetByHasDialog(hasDialog, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDialogAsync(bool hasDialog, int top_)
		{
			return await GetByHasDialogAsync(hasDialog, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 HasDialog（字段） 查询
		/// </summary>
		/// /// <param name = "hasDialog">是否有Dialog功能（只支持单一主键）</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDialog(bool hasDialog, int top_, TransactionManager tm_)
		{
			return GetByHasDialog(hasDialog, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDialogAsync(bool hasDialog, int top_, TransactionManager tm_)
		{
			return await GetByHasDialogAsync(hasDialog, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 HasDialog（字段） 查询
		/// </summary>
		/// /// <param name = "hasDialog">是否有Dialog功能（只支持单一主键）</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDialog(bool hasDialog, string sort_)
		{
			return GetByHasDialog(hasDialog, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDialogAsync(bool hasDialog, string sort_)
		{
			return await GetByHasDialogAsync(hasDialog, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 HasDialog（字段） 查询
		/// </summary>
		/// /// <param name = "hasDialog">是否有Dialog功能（只支持单一主键）</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDialog(bool hasDialog, string sort_, TransactionManager tm_)
		{
			return GetByHasDialog(hasDialog, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDialogAsync(bool hasDialog, string sort_, TransactionManager tm_)
		{
			return await GetByHasDialogAsync(hasDialog, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 HasDialog（字段） 查询
		/// </summary>
		/// /// <param name = "hasDialog">是否有Dialog功能（只支持单一主键）</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByHasDialog(bool hasDialog, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`HasDialog` = @HasDialog", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasDialog", hasDialog, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByHasDialogAsync(bool hasDialog, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`HasDialog` = @HasDialog", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@HasDialog", hasDialog, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByHasDialog
		#region GetByDialogSQL
		
		/// <summary>
		/// 按 DialogSQL（字段） 查询
		/// </summary>
		/// /// <param name = "dialogSQL">对话框SQL</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogSQL(string dialogSQL)
		{
			return GetByDialogSQL(dialogSQL, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogSQLAsync(string dialogSQL)
		{
			return await GetByDialogSQLAsync(dialogSQL, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DialogSQL（字段） 查询
		/// </summary>
		/// /// <param name = "dialogSQL">对话框SQL</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogSQL(string dialogSQL, TransactionManager tm_)
		{
			return GetByDialogSQL(dialogSQL, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogSQLAsync(string dialogSQL, TransactionManager tm_)
		{
			return await GetByDialogSQLAsync(dialogSQL, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DialogSQL（字段） 查询
		/// </summary>
		/// /// <param name = "dialogSQL">对话框SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogSQL(string dialogSQL, int top_)
		{
			return GetByDialogSQL(dialogSQL, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogSQLAsync(string dialogSQL, int top_)
		{
			return await GetByDialogSQLAsync(dialogSQL, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DialogSQL（字段） 查询
		/// </summary>
		/// /// <param name = "dialogSQL">对话框SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogSQL(string dialogSQL, int top_, TransactionManager tm_)
		{
			return GetByDialogSQL(dialogSQL, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogSQLAsync(string dialogSQL, int top_, TransactionManager tm_)
		{
			return await GetByDialogSQLAsync(dialogSQL, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DialogSQL（字段） 查询
		/// </summary>
		/// /// <param name = "dialogSQL">对话框SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogSQL(string dialogSQL, string sort_)
		{
			return GetByDialogSQL(dialogSQL, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogSQLAsync(string dialogSQL, string sort_)
		{
			return await GetByDialogSQLAsync(dialogSQL, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 DialogSQL（字段） 查询
		/// </summary>
		/// /// <param name = "dialogSQL">对话框SQL</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogSQL(string dialogSQL, string sort_, TransactionManager tm_)
		{
			return GetByDialogSQL(dialogSQL, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogSQLAsync(string dialogSQL, string sort_, TransactionManager tm_)
		{
			return await GetByDialogSQLAsync(dialogSQL, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 DialogSQL（字段） 查询
		/// </summary>
		/// /// <param name = "dialogSQL">对话框SQL</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogSQL(string dialogSQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(dialogSQL != null ? "`DialogSQL` = @DialogSQL" : "`DialogSQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (dialogSQL != null)
				paras_.Add(Database.CreateInParameter("@DialogSQL", dialogSQL, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogSQLAsync(string dialogSQL, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(dialogSQL != null ? "`DialogSQL` = @DialogSQL" : "`DialogSQL` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (dialogSQL != null)
				paras_.Add(Database.CreateInParameter("@DialogSQL", dialogSQL, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByDialogSQL
		#region GetByDialogFieldName
		
		/// <summary>
		/// 按 DialogFieldName（字段） 查询
		/// </summary>
		/// /// <param name = "dialogFieldName">对话框列名</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogFieldName(string dialogFieldName)
		{
			return GetByDialogFieldName(dialogFieldName, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogFieldNameAsync(string dialogFieldName)
		{
			return await GetByDialogFieldNameAsync(dialogFieldName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DialogFieldName（字段） 查询
		/// </summary>
		/// /// <param name = "dialogFieldName">对话框列名</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogFieldName(string dialogFieldName, TransactionManager tm_)
		{
			return GetByDialogFieldName(dialogFieldName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogFieldNameAsync(string dialogFieldName, TransactionManager tm_)
		{
			return await GetByDialogFieldNameAsync(dialogFieldName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DialogFieldName（字段） 查询
		/// </summary>
		/// /// <param name = "dialogFieldName">对话框列名</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogFieldName(string dialogFieldName, int top_)
		{
			return GetByDialogFieldName(dialogFieldName, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogFieldNameAsync(string dialogFieldName, int top_)
		{
			return await GetByDialogFieldNameAsync(dialogFieldName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DialogFieldName（字段） 查询
		/// </summary>
		/// /// <param name = "dialogFieldName">对话框列名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogFieldName(string dialogFieldName, int top_, TransactionManager tm_)
		{
			return GetByDialogFieldName(dialogFieldName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogFieldNameAsync(string dialogFieldName, int top_, TransactionManager tm_)
		{
			return await GetByDialogFieldNameAsync(dialogFieldName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DialogFieldName（字段） 查询
		/// </summary>
		/// /// <param name = "dialogFieldName">对话框列名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogFieldName(string dialogFieldName, string sort_)
		{
			return GetByDialogFieldName(dialogFieldName, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogFieldNameAsync(string dialogFieldName, string sort_)
		{
			return await GetByDialogFieldNameAsync(dialogFieldName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 DialogFieldName（字段） 查询
		/// </summary>
		/// /// <param name = "dialogFieldName">对话框列名</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogFieldName(string dialogFieldName, string sort_, TransactionManager tm_)
		{
			return GetByDialogFieldName(dialogFieldName, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogFieldNameAsync(string dialogFieldName, string sort_, TransactionManager tm_)
		{
			return await GetByDialogFieldNameAsync(dialogFieldName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 DialogFieldName（字段） 查询
		/// </summary>
		/// /// <param name = "dialogFieldName">对话框列名</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogFieldName(string dialogFieldName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(dialogFieldName != null ? "`DialogFieldName` = @DialogFieldName" : "`DialogFieldName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (dialogFieldName != null)
				paras_.Add(Database.CreateInParameter("@DialogFieldName", dialogFieldName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogFieldNameAsync(string dialogFieldName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(dialogFieldName != null ? "`DialogFieldName` = @DialogFieldName" : "`DialogFieldName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (dialogFieldName != null)
				paras_.Add(Database.CreateInParameter("@DialogFieldName", dialogFieldName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByDialogFieldName
		#region GetByDialogWidth
		
		/// <summary>
		/// 按 DialogWidth（字段） 查询
		/// </summary>
		/// /// <param name = "dialogWidth">对话框宽度</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogWidth(int dialogWidth)
		{
			return GetByDialogWidth(dialogWidth, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogWidthAsync(int dialogWidth)
		{
			return await GetByDialogWidthAsync(dialogWidth, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DialogWidth（字段） 查询
		/// </summary>
		/// /// <param name = "dialogWidth">对话框宽度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogWidth(int dialogWidth, TransactionManager tm_)
		{
			return GetByDialogWidth(dialogWidth, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogWidthAsync(int dialogWidth, TransactionManager tm_)
		{
			return await GetByDialogWidthAsync(dialogWidth, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DialogWidth（字段） 查询
		/// </summary>
		/// /// <param name = "dialogWidth">对话框宽度</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogWidth(int dialogWidth, int top_)
		{
			return GetByDialogWidth(dialogWidth, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogWidthAsync(int dialogWidth, int top_)
		{
			return await GetByDialogWidthAsync(dialogWidth, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DialogWidth（字段） 查询
		/// </summary>
		/// /// <param name = "dialogWidth">对话框宽度</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogWidth(int dialogWidth, int top_, TransactionManager tm_)
		{
			return GetByDialogWidth(dialogWidth, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogWidthAsync(int dialogWidth, int top_, TransactionManager tm_)
		{
			return await GetByDialogWidthAsync(dialogWidth, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DialogWidth（字段） 查询
		/// </summary>
		/// /// <param name = "dialogWidth">对话框宽度</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogWidth(int dialogWidth, string sort_)
		{
			return GetByDialogWidth(dialogWidth, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogWidthAsync(int dialogWidth, string sort_)
		{
			return await GetByDialogWidthAsync(dialogWidth, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 DialogWidth（字段） 查询
		/// </summary>
		/// /// <param name = "dialogWidth">对话框宽度</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogWidth(int dialogWidth, string sort_, TransactionManager tm_)
		{
			return GetByDialogWidth(dialogWidth, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogWidthAsync(int dialogWidth, string sort_, TransactionManager tm_)
		{
			return await GetByDialogWidthAsync(dialogWidth, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 DialogWidth（字段） 查询
		/// </summary>
		/// /// <param name = "dialogWidth">对话框宽度</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogWidth(int dialogWidth, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`DialogWidth` = @DialogWidth", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@DialogWidth", dialogWidth, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogWidthAsync(int dialogWidth, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`DialogWidth` = @DialogWidth", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@DialogWidth", dialogWidth, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByDialogWidth
		#region GetByDialogHeight
		
		/// <summary>
		/// 按 DialogHeight（字段） 查询
		/// </summary>
		/// /// <param name = "dialogHeight">对话框高度</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogHeight(int dialogHeight)
		{
			return GetByDialogHeight(dialogHeight, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogHeightAsync(int dialogHeight)
		{
			return await GetByDialogHeightAsync(dialogHeight, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DialogHeight（字段） 查询
		/// </summary>
		/// /// <param name = "dialogHeight">对话框高度</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogHeight(int dialogHeight, TransactionManager tm_)
		{
			return GetByDialogHeight(dialogHeight, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogHeightAsync(int dialogHeight, TransactionManager tm_)
		{
			return await GetByDialogHeightAsync(dialogHeight, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DialogHeight（字段） 查询
		/// </summary>
		/// /// <param name = "dialogHeight">对话框高度</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogHeight(int dialogHeight, int top_)
		{
			return GetByDialogHeight(dialogHeight, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogHeightAsync(int dialogHeight, int top_)
		{
			return await GetByDialogHeightAsync(dialogHeight, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 DialogHeight（字段） 查询
		/// </summary>
		/// /// <param name = "dialogHeight">对话框高度</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogHeight(int dialogHeight, int top_, TransactionManager tm_)
		{
			return GetByDialogHeight(dialogHeight, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogHeightAsync(int dialogHeight, int top_, TransactionManager tm_)
		{
			return await GetByDialogHeightAsync(dialogHeight, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 DialogHeight（字段） 查询
		/// </summary>
		/// /// <param name = "dialogHeight">对话框高度</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogHeight(int dialogHeight, string sort_)
		{
			return GetByDialogHeight(dialogHeight, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogHeightAsync(int dialogHeight, string sort_)
		{
			return await GetByDialogHeightAsync(dialogHeight, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 DialogHeight（字段） 查询
		/// </summary>
		/// /// <param name = "dialogHeight">对话框高度</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogHeight(int dialogHeight, string sort_, TransactionManager tm_)
		{
			return GetByDialogHeight(dialogHeight, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogHeightAsync(int dialogHeight, string sort_, TransactionManager tm_)
		{
			return await GetByDialogHeightAsync(dialogHeight, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 DialogHeight（字段） 查询
		/// </summary>
		/// /// <param name = "dialogHeight">对话框高度</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByDialogHeight(int dialogHeight, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`DialogHeight` = @DialogHeight", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@DialogHeight", dialogHeight, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByDialogHeightAsync(int dialogHeight, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`DialogHeight` = @DialogHeight", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@DialogHeight", dialogHeight, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByDialogHeight
		#region GetByStatus
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-初始 1-有效 2-无效</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByStatus(int status)
		{
			return GetByStatus(status, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByStatusAsync(int status)
		{
			return await GetByStatusAsync(status, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-初始 1-有效 2-无效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByStatus(int status, TransactionManager tm_)
		{
			return GetByStatus(status, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByStatusAsync(int status, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-初始 1-有效 2-无效</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByStatus(int status, int top_)
		{
			return GetByStatus(status, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByStatusAsync(int status, int top_)
		{
			return await GetByStatusAsync(status, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-初始 1-有效 2-无效</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByStatus(int status, int top_, TransactionManager tm_)
		{
			return GetByStatus(status, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByStatusAsync(int status, int top_, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-初始 1-有效 2-无效</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByStatus(int status, string sort_)
		{
			return GetByStatus(status, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByStatusAsync(int status, string sort_)
		{
			return await GetByStatusAsync(status, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-初始 1-有效 2-无效</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByStatus(int status, string sort_, TransactionManager tm_)
		{
			return GetByStatus(status, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByStatusAsync(int status, string sort_, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-初始 1-有效 2-无效</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByStatus(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByStatusAsync(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByStatus
		#region GetByRecDate
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByRecDate(DateTime recDate)
		{
			return GetByRecDate(recDate, 0, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByRecDateAsync(DateTime recDate)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByRecDate(DateTime recDate, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByRecDateAsync(DateTime recDate, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByRecDate(DateTime recDate, int top_)
		{
			return GetByRecDate(recDate, top_, string.Empty, null);
		}
		public async Task<List<Admin_listeditEO>> GetByRecDateAsync(DateTime recDate, int top_)
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
		public List<Admin_listeditEO> GetByRecDate(DateTime recDate, int top_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByRecDateAsync(DateTime recDate, int top_, TransactionManager tm_)
		{
			return await GetByRecDateAsync(recDate, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RecDate（字段） 查询
		/// </summary>
		/// /// <param name = "recDate">记录日期</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_listeditEO> GetByRecDate(DateTime recDate, string sort_)
		{
			return GetByRecDate(recDate, 0, sort_, null);
		}
		public async Task<List<Admin_listeditEO>> GetByRecDateAsync(DateTime recDate, string sort_)
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
		public List<Admin_listeditEO> GetByRecDate(DateTime recDate, string sort_, TransactionManager tm_)
		{
			return GetByRecDate(recDate, 0, sort_, tm_);
		}
		public async Task<List<Admin_listeditEO>> GetByRecDateAsync(DateTime recDate, string sort_, TransactionManager tm_)
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
		public List<Admin_listeditEO> GetByRecDate(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		public async Task<List<Admin_listeditEO>> GetByRecDateAsync(DateTime recDate, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RecDate` = @RecDate", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@RecDate", recDate, MySqlDbType.DateTime));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_listeditEO.MapDataReader);
		}
		#endregion // GetByRecDate
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
