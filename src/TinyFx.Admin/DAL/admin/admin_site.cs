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
	/// 后台管理站点
	/// 【表 admin_site 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_siteEO : IRowMapper<Admin_siteEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_siteEO()
		{
			this.Status = 0;
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
		private string _originalSiteID;
		/// <summary>
		/// 【数据库中的原始主键 SiteID 值的副本，用于主键值更新】
		/// </summary>
		public string OriginalSiteID
		{
			get { return _originalSiteID; }
			set { HasOriginal = true; _originalSiteID = value; }
		}
	    /// <summary>
	    /// 获取主键集合。key: 数据库字段名称, value: 主键值
	    /// </summary>
	    /// <returns></returns>
	    public Dictionary<string, object> GetPrimaryKeys()
	    {
	        return new Dictionary<string, object>() { { "SiteID", SiteID }, };
	    }
	    /// <summary>
	    /// 获取主键集合JSON格式
	    /// </summary>
	    /// <returns></returns>
	    public string GetPrimaryKeysJson() => SerializerUtil.SerializeJson(GetPrimaryKeys());
		#endregion // 主键原始值
		#region 所有字段
		/// <summary>
		/// 站点编码
		/// 【主键 varchar(50)】
		/// </summary>
		[DataMember(Order = 1)]
		public string SiteID { get; set; }
		/// <summary>
		/// 站点名称
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 2)]
		public string SiteName { get; set; }
		/// <summary>
		/// 基础路径
		/// 【字段 varchar(255)】
		/// </summary>
		[DataMember(Order = 3)]
		public string BaseUrl { get; set; }
		/// <summary>
		/// 描述
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 4)]
		public string Desc { get; set; }
		/// <summary>
		/// 状态 0-无效 1-有效
		/// 【字段 tinyint】
		/// </summary>
		[DataMember(Order = 5)]
		public int Status { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_siteEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_siteEO MapDataReader(IDataReader reader)
		{
		    Admin_siteEO ret = new Admin_siteEO();
			ret.SiteID = reader.ToString("SiteID");
			ret.OriginalSiteID = ret.SiteID;
			ret.SiteName = reader.ToString("SiteName");
			ret.BaseUrl = reader.ToString("BaseUrl");
			ret.Desc = reader.ToString("Desc");
			ret.Status = reader.ToInt32("Status");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 后台管理站点
	/// 【表 admin_site 的操作类】
	/// </summary>
	public class Admin_siteMO : MySqlTableMO<Admin_siteEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_site`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_siteMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_siteMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_siteMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_siteEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_siteEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_siteEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_site` (`SiteID`, `SiteName`, `BaseUrl`, `Desc`, `Status`) VALUE (@SiteID, @SiteName, @BaseUrl, @Desc, @Status);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", item.SiteID, MySqlDbType.VarChar),
				Database.CreateInParameter("@SiteName", item.SiteName != null ? item.SiteName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@BaseUrl", item.BaseUrl != null ? item.BaseUrl : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Desc", item.Desc != null ? item.Desc : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Status", item.Status, MySqlDbType.Byte),
			};
		}
	    #endregion // Add
	    
		#region Remove
		#region RemoveByPK
		/// <summary>
		/// 按主键删除
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(string siteID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(siteID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(string siteID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(siteID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(string siteID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_site` WHERE `SiteID` = @SiteID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_siteEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.SiteID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_siteEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.SiteID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveBySiteName
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "siteName">站点名称</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveBySiteName(string siteName, TransactionManager tm_ = null)
		{
			RepairRemoveBySiteNameData(siteName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveBySiteNameAsync(string siteName, TransactionManager tm_ = null)
		{
			RepairRemoveBySiteNameData(siteName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveBySiteNameData(string siteName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_site` WHERE " + (siteName != null ? "`SiteName` = @SiteName" : "`SiteName` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (siteName != null)
				paras_.Add(Database.CreateInParameter("@SiteName", siteName, MySqlDbType.VarChar));
		}
		#endregion // RemoveBySiteName
		#region RemoveByBaseUrl
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "baseUrl">基础路径</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByBaseUrl(string baseUrl, TransactionManager tm_ = null)
		{
			RepairRemoveByBaseUrlData(baseUrl, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByBaseUrlAsync(string baseUrl, TransactionManager tm_ = null)
		{
			RepairRemoveByBaseUrlData(baseUrl, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByBaseUrlData(string baseUrl, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_site` WHERE " + (baseUrl != null ? "`BaseUrl` = @BaseUrl" : "`BaseUrl` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (baseUrl != null)
				paras_.Add(Database.CreateInParameter("@BaseUrl", baseUrl, MySqlDbType.VarChar));
		}
		#endregion // RemoveByBaseUrl
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
			sql_ = @"DELETE FROM `admin_site` WHERE " + (desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.Text));
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
			sql_ = @"DELETE FROM `admin_site` WHERE `Status` = @Status";
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
		public int Put(Admin_siteEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_siteEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_siteEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_site` SET `SiteID` = @SiteID, `SiteName` = @SiteName, `BaseUrl` = @BaseUrl, `Desc` = @Desc, `Status` = @Status WHERE `SiteID` = @SiteID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", item.SiteID, MySqlDbType.VarChar),
				Database.CreateInParameter("@SiteName", item.SiteName != null ? item.SiteName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@BaseUrl", item.BaseUrl != null ? item.BaseUrl : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Desc", item.Desc != null ? item.Desc : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@Status", item.Status, MySqlDbType.Byte),
				Database.CreateInParameter("@SiteID_Original", item.HasOriginal ? item.OriginalSiteID : item.SiteID, MySqlDbType.VarChar),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_siteEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_siteEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string siteID, string set_, params object[] values_)
		{
			return Put(set_, "`SiteID` = @SiteID", ConcatValues(values_, siteID));
		}
		public async Task<int> PutByPKAsync(string siteID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`SiteID` = @SiteID", ConcatValues(values_, siteID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string siteID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`SiteID` = @SiteID", tm_, ConcatValues(values_, siteID));
		}
		public async Task<int> PutByPKAsync(string siteID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`SiteID` = @SiteID", tm_, ConcatValues(values_, siteID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string siteID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
	        };
			return Put(set_, "`SiteID` = @SiteID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(string siteID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
	        };
			return await PutAsync(set_, "`SiteID` = @SiteID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutSiteName
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// /// <param name = "siteName">站点名称</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutSiteNameByPK(string siteID, string siteName, TransactionManager tm_ = null)
		{
			RepairPutSiteNameByPKData(siteID, siteName, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutSiteNameByPKAsync(string siteID, string siteName, TransactionManager tm_ = null)
		{
			RepairPutSiteNameByPKData(siteID, siteName, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutSiteNameByPKData(string siteID, string siteName, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_site` SET `SiteName` = @SiteName  WHERE `SiteID` = @SiteID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteName", siteName != null ? siteName : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "siteName">站点名称</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutSiteName(string siteName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_site` SET `SiteName` = @SiteName";
			var parameter_ = Database.CreateInParameter("@SiteName", siteName != null ? siteName : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutSiteNameAsync(string siteName, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_site` SET `SiteName` = @SiteName";
			var parameter_ = Database.CreateInParameter("@SiteName", siteName != null ? siteName : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutSiteName
		#region PutBaseUrl
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// /// <param name = "baseUrl">基础路径</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutBaseUrlByPK(string siteID, string baseUrl, TransactionManager tm_ = null)
		{
			RepairPutBaseUrlByPKData(siteID, baseUrl, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutBaseUrlByPKAsync(string siteID, string baseUrl, TransactionManager tm_ = null)
		{
			RepairPutBaseUrlByPKData(siteID, baseUrl, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutBaseUrlByPKData(string siteID, string baseUrl, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_site` SET `BaseUrl` = @BaseUrl  WHERE `SiteID` = @SiteID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@BaseUrl", baseUrl != null ? baseUrl : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "baseUrl">基础路径</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutBaseUrl(string baseUrl, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_site` SET `BaseUrl` = @BaseUrl";
			var parameter_ = Database.CreateInParameter("@BaseUrl", baseUrl != null ? baseUrl : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutBaseUrlAsync(string baseUrl, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_site` SET `BaseUrl` = @BaseUrl";
			var parameter_ = Database.CreateInParameter("@BaseUrl", baseUrl != null ? baseUrl : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutBaseUrl
		#region PutDesc
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDescByPK(string siteID, string desc, TransactionManager tm_ = null)
		{
			RepairPutDescByPKData(siteID, desc, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDescByPKAsync(string siteID, string desc, TransactionManager tm_ = null)
		{
			RepairPutDescByPKData(siteID, desc, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDescByPKData(string siteID, string desc, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_site` SET `Desc` = @Desc  WHERE `SiteID` = @SiteID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.Text),
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
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
			const string sql_ = @"UPDATE `admin_site` SET `Desc` = @Desc";
			var parameter_ = Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDescAsync(string desc, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_site` SET `Desc` = @Desc";
			var parameter_ = Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.Text);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDesc
		#region PutStatus
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutStatusByPK(string siteID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(siteID, status, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutStatusByPKAsync(string siteID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(siteID, status, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutStatusByPKData(string siteID, int status, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_site` SET `Status` = @Status  WHERE `SiteID` = @SiteID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Status", status, MySqlDbType.Byte),
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
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
			const string sql_ = @"UPDATE `admin_site` SET `Status` = @Status";
			var parameter_ = Database.CreateInParameter("@Status", status, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutStatusAsync(int status, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_site` SET `Status` = @Status";
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
		public bool Set(Admin_siteEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.SiteID) == null)
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
		public async Task<bool> SetAsync(Admin_siteEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.SiteID) == null)
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
		/// /// <param name = "siteID">站点编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_siteEO GetByPK(string siteID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(siteID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_siteEO.MapDataReader);
		}
		public async Task<Admin_siteEO> GetByPKAsync(string siteID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(siteID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_siteEO.MapDataReader);
		}
		private void RepairGetByPKData(string siteID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`SiteID` = @SiteID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 SiteName（字段）
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetSiteNameByPK(string siteID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`SiteName`", "`SiteID` = @SiteID", paras_, tm_);
		}
		public async Task<string> GetSiteNameByPKAsync(string siteID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`SiteName`", "`SiteID` = @SiteID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 BaseUrl（字段）
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetBaseUrlByPK(string siteID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`BaseUrl`", "`SiteID` = @SiteID", paras_, tm_);
		}
		public async Task<string> GetBaseUrlByPKAsync(string siteID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`BaseUrl`", "`SiteID` = @SiteID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Desc（字段）
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetDescByPK(string siteID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Desc`", "`SiteID` = @SiteID", paras_, tm_);
		}
		public async Task<string> GetDescByPKAsync(string siteID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Desc`", "`SiteID` = @SiteID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Status（字段）
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetStatusByPK(string siteID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`Status`", "`SiteID` = @SiteID", paras_, tm_);
		}
		public async Task<int> GetStatusByPKAsync(string siteID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`Status`", "`SiteID` = @SiteID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetBySiteName
		
		/// <summary>
		/// 按 SiteName（字段） 查询
		/// </summary>
		/// /// <param name = "siteName">站点名称</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetBySiteName(string siteName)
		{
			return GetBySiteName(siteName, 0, string.Empty, null);
		}
		public async Task<List<Admin_siteEO>> GetBySiteNameAsync(string siteName)
		{
			return await GetBySiteNameAsync(siteName, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 SiteName（字段） 查询
		/// </summary>
		/// /// <param name = "siteName">站点名称</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetBySiteName(string siteName, TransactionManager tm_)
		{
			return GetBySiteName(siteName, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_siteEO>> GetBySiteNameAsync(string siteName, TransactionManager tm_)
		{
			return await GetBySiteNameAsync(siteName, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 SiteName（字段） 查询
		/// </summary>
		/// /// <param name = "siteName">站点名称</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetBySiteName(string siteName, int top_)
		{
			return GetBySiteName(siteName, top_, string.Empty, null);
		}
		public async Task<List<Admin_siteEO>> GetBySiteNameAsync(string siteName, int top_)
		{
			return await GetBySiteNameAsync(siteName, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 SiteName（字段） 查询
		/// </summary>
		/// /// <param name = "siteName">站点名称</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetBySiteName(string siteName, int top_, TransactionManager tm_)
		{
			return GetBySiteName(siteName, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_siteEO>> GetBySiteNameAsync(string siteName, int top_, TransactionManager tm_)
		{
			return await GetBySiteNameAsync(siteName, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 SiteName（字段） 查询
		/// </summary>
		/// /// <param name = "siteName">站点名称</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetBySiteName(string siteName, string sort_)
		{
			return GetBySiteName(siteName, 0, sort_, null);
		}
		public async Task<List<Admin_siteEO>> GetBySiteNameAsync(string siteName, string sort_)
		{
			return await GetBySiteNameAsync(siteName, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 SiteName（字段） 查询
		/// </summary>
		/// /// <param name = "siteName">站点名称</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetBySiteName(string siteName, string sort_, TransactionManager tm_)
		{
			return GetBySiteName(siteName, 0, sort_, tm_);
		}
		public async Task<List<Admin_siteEO>> GetBySiteNameAsync(string siteName, string sort_, TransactionManager tm_)
		{
			return await GetBySiteNameAsync(siteName, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 SiteName（字段） 查询
		/// </summary>
		/// /// <param name = "siteName">站点名称</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetBySiteName(string siteName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(siteName != null ? "`SiteName` = @SiteName" : "`SiteName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (siteName != null)
				paras_.Add(Database.CreateInParameter("@SiteName", siteName, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_siteEO.MapDataReader);
		}
		public async Task<List<Admin_siteEO>> GetBySiteNameAsync(string siteName, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(siteName != null ? "`SiteName` = @SiteName" : "`SiteName` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (siteName != null)
				paras_.Add(Database.CreateInParameter("@SiteName", siteName, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_siteEO.MapDataReader);
		}
		#endregion // GetBySiteName
		#region GetByBaseUrl
		
		/// <summary>
		/// 按 BaseUrl（字段） 查询
		/// </summary>
		/// /// <param name = "baseUrl">基础路径</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByBaseUrl(string baseUrl)
		{
			return GetByBaseUrl(baseUrl, 0, string.Empty, null);
		}
		public async Task<List<Admin_siteEO>> GetByBaseUrlAsync(string baseUrl)
		{
			return await GetByBaseUrlAsync(baseUrl, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 BaseUrl（字段） 查询
		/// </summary>
		/// /// <param name = "baseUrl">基础路径</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByBaseUrl(string baseUrl, TransactionManager tm_)
		{
			return GetByBaseUrl(baseUrl, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_siteEO>> GetByBaseUrlAsync(string baseUrl, TransactionManager tm_)
		{
			return await GetByBaseUrlAsync(baseUrl, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 BaseUrl（字段） 查询
		/// </summary>
		/// /// <param name = "baseUrl">基础路径</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByBaseUrl(string baseUrl, int top_)
		{
			return GetByBaseUrl(baseUrl, top_, string.Empty, null);
		}
		public async Task<List<Admin_siteEO>> GetByBaseUrlAsync(string baseUrl, int top_)
		{
			return await GetByBaseUrlAsync(baseUrl, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 BaseUrl（字段） 查询
		/// </summary>
		/// /// <param name = "baseUrl">基础路径</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByBaseUrl(string baseUrl, int top_, TransactionManager tm_)
		{
			return GetByBaseUrl(baseUrl, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_siteEO>> GetByBaseUrlAsync(string baseUrl, int top_, TransactionManager tm_)
		{
			return await GetByBaseUrlAsync(baseUrl, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 BaseUrl（字段） 查询
		/// </summary>
		/// /// <param name = "baseUrl">基础路径</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByBaseUrl(string baseUrl, string sort_)
		{
			return GetByBaseUrl(baseUrl, 0, sort_, null);
		}
		public async Task<List<Admin_siteEO>> GetByBaseUrlAsync(string baseUrl, string sort_)
		{
			return await GetByBaseUrlAsync(baseUrl, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 BaseUrl（字段） 查询
		/// </summary>
		/// /// <param name = "baseUrl">基础路径</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByBaseUrl(string baseUrl, string sort_, TransactionManager tm_)
		{
			return GetByBaseUrl(baseUrl, 0, sort_, tm_);
		}
		public async Task<List<Admin_siteEO>> GetByBaseUrlAsync(string baseUrl, string sort_, TransactionManager tm_)
		{
			return await GetByBaseUrlAsync(baseUrl, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 BaseUrl（字段） 查询
		/// </summary>
		/// /// <param name = "baseUrl">基础路径</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByBaseUrl(string baseUrl, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(baseUrl != null ? "`BaseUrl` = @BaseUrl" : "`BaseUrl` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (baseUrl != null)
				paras_.Add(Database.CreateInParameter("@BaseUrl", baseUrl, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_siteEO.MapDataReader);
		}
		public async Task<List<Admin_siteEO>> GetByBaseUrlAsync(string baseUrl, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(baseUrl != null ? "`BaseUrl` = @BaseUrl" : "`BaseUrl` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (baseUrl != null)
				paras_.Add(Database.CreateInParameter("@BaseUrl", baseUrl, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_siteEO.MapDataReader);
		}
		#endregion // GetByBaseUrl
		#region GetByDesc
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByDesc(string desc)
		{
			return GetByDesc(desc, 0, string.Empty, null);
		}
		public async Task<List<Admin_siteEO>> GetByDescAsync(string desc)
		{
			return await GetByDescAsync(desc, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByDesc(string desc, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_siteEO>> GetByDescAsync(string desc, TransactionManager tm_)
		{
			return await GetByDescAsync(desc, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByDesc(string desc, int top_)
		{
			return GetByDesc(desc, top_, string.Empty, null);
		}
		public async Task<List<Admin_siteEO>> GetByDescAsync(string desc, int top_)
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
		public List<Admin_siteEO> GetByDesc(string desc, int top_, TransactionManager tm_)
		{
			return GetByDesc(desc, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_siteEO>> GetByDescAsync(string desc, int top_, TransactionManager tm_)
		{
			return await GetByDescAsync(desc, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByDesc(string desc, string sort_)
		{
			return GetByDesc(desc, 0, sort_, null);
		}
		public async Task<List<Admin_siteEO>> GetByDescAsync(string desc, string sort_)
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
		public List<Admin_siteEO> GetByDesc(string desc, string sort_, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, sort_, tm_);
		}
		public async Task<List<Admin_siteEO>> GetByDescAsync(string desc, string sort_, TransactionManager tm_)
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
		public List<Admin_siteEO> GetByDesc(string desc, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.Text));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_siteEO.MapDataReader);
		}
		public async Task<List<Admin_siteEO>> GetByDescAsync(string desc, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.Text));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_siteEO.MapDataReader);
		}
		#endregion // GetByDesc
		#region GetByStatus
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByStatus(int status)
		{
			return GetByStatus(status, 0, string.Empty, null);
		}
		public async Task<List<Admin_siteEO>> GetByStatusAsync(int status)
		{
			return await GetByStatusAsync(status, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByStatus(int status, TransactionManager tm_)
		{
			return GetByStatus(status, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_siteEO>> GetByStatusAsync(int status, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByStatus(int status, int top_)
		{
			return GetByStatus(status, top_, string.Empty, null);
		}
		public async Task<List<Admin_siteEO>> GetByStatusAsync(int status, int top_)
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
		public List<Admin_siteEO> GetByStatus(int status, int top_, TransactionManager tm_)
		{
			return GetByStatus(status, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_siteEO>> GetByStatusAsync(int status, int top_, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_siteEO> GetByStatus(int status, string sort_)
		{
			return GetByStatus(status, 0, sort_, null);
		}
		public async Task<List<Admin_siteEO>> GetByStatusAsync(int status, string sort_)
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
		public List<Admin_siteEO> GetByStatus(int status, string sort_, TransactionManager tm_)
		{
			return GetByStatus(status, 0, sort_, tm_);
		}
		public async Task<List<Admin_siteEO>> GetByStatusAsync(int status, string sort_, TransactionManager tm_)
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
		public List<Admin_siteEO> GetByStatus(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_siteEO.MapDataReader);
		}
		public async Task<List<Admin_siteEO>> GetByStatusAsync(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_siteEO.MapDataReader);
		}
		#endregion // GetByStatus
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
