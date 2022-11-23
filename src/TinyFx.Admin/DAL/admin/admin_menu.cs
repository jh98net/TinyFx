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
	/// 管理菜单
	/// 【表 admin_menu 的实体类】
	/// </summary>
	[DataContract]
	public class Admin_menuEO : IRowMapper<Admin_menuEO>
	{
		/// <summary>
		/// 构造函数 
		/// </summary>
		public Admin_menuEO()
		{
			this.ParentId = "0";
			this.OrderNum = 0;
			this.LinkMode = 0;
			this.UrlTarget = 0;
			this.Status = 0;
		}
		#region 主键原始值 & 主键集合
	    
		/// <summary>
		/// 当前对象是否保存原始主键值，当修改了主键值时为 true
		/// </summary>
		public bool HasOriginal { get; protected set; }
		
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
	        return new Dictionary<string, object>() { { "MenuID", MenuID }, };
	    }
	    /// <summary>
	    /// 获取主键集合JSON格式
	    /// </summary>
	    /// <returns></returns>
	    public string GetPrimaryKeysJson() => SerializerUtil.SerializeJson(GetPrimaryKeys());
		#endregion // 主键原始值
		#region 所有字段
		/// <summary>
		/// 菜单编码GUID
		/// 【主键 varchar(40)】
		/// </summary>
		[DataMember(Order = 1)]
		public string MenuID { get; set; }
		/// <summary>
		/// 站点编码
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 2)]
		public string SiteID { get; set; }
		/// <summary>
		/// 父菜单编码 0-根菜单
		/// 【字段 varchar(40)】
		/// </summary>
		[DataMember(Order = 3)]
		public string ParentId { get; set; }
		/// <summary>
		/// 菜单显示标题
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 4)]
		public string Title { get; set; }
		/// <summary>
		/// 排序，从小到大
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 5)]
		public int OrderNum { get; set; }
		/// <summary>
		/// 图标
		/// 【字段 varchar(250)】
		/// </summary>
		[DataMember(Order = 6)]
		public string Icon { get; set; }
		/// <summary>
		/// 链接类型0-目录1-rul 2-siteid+url 3-GenID
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 7)]
		public int LinkMode { get; set; }
		/// <summary>
		/// 菜单URL，尽量使用相对路径
		/// 【字段 varchar(250)】
		/// </summary>
		[DataMember(Order = 8)]
		public string Url { get; set; }
		/// <summary>
		/// GenID编码
		/// 【字段 varchar(50)】
		/// </summary>
		[DataMember(Order = 9)]
		public string GenID { get; set; }
		/// <summary>
		/// 链接模式 0-TAB打开 1-新窗口打开
		/// 【字段 tinyint】
		/// </summary>
		[DataMember(Order = 10)]
		public int UrlTarget { get; set; }
		/// <summary>
		/// 功能和数据权限参数。格式：类型-参数| 类型-参数
		///              用于在定义页面内权限时可设置的权限选项列表
		///              如：ControlID-btnOk|ControlID-btnCancle
		/// 【字段 varchar(250)】
		/// </summary>
		[DataMember(Order = 11)]
		public string PrivParams { get; set; }
		/// <summary>
		/// 拼音
		/// 【字段 varchar(20)】
		/// </summary>
		[DataMember(Order = 12)]
		public string Pinyin { get; set; }
		/// <summary>
		/// 描述
		/// 【字段 varchar(500)】
		/// </summary>
		[DataMember(Order = 13)]
		public string Desc { get; set; }
		/// <summary>
		/// 状态 0-无效 1-有效
		/// 【字段 tinyint】
		/// </summary>
		[DataMember(Order = 14)]
		public int Status { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public Admin_menuEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static Admin_menuEO MapDataReader(IDataReader reader)
		{
		    Admin_menuEO ret = new Admin_menuEO();
			ret.MenuID = reader.ToString("MenuID");
			ret.OriginalMenuID = ret.MenuID;
			ret.SiteID = reader.ToString("SiteID");
			ret.ParentId = reader.ToString("ParentId");
			ret.Title = reader.ToString("Title");
			ret.OrderNum = reader.ToInt32("OrderNum");
			ret.Icon = reader.ToString("Icon");
			ret.LinkMode = reader.ToInt32("LinkMode");
			ret.Url = reader.ToString("Url");
			ret.GenID = reader.ToString("GenID");
			ret.UrlTarget = reader.ToInt32("UrlTarget");
			ret.PrivParams = reader.ToString("PrivParams");
			ret.Pinyin = reader.ToString("Pinyin");
			ret.Desc = reader.ToString("Desc");
			ret.Status = reader.ToInt32("Status");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 管理菜单
	/// 【表 admin_menu 的操作类】
	/// </summary>
	public class Admin_menuMO : MySqlTableMO<Admin_menuEO>
	{
		/// <summary>
		/// 表名
		/// </summary>
	    public override string TableName => "`admin_menu`";
	    
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public Admin_menuMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public Admin_menuMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public Admin_menuMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
	    
	    #region  Add
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name = "item">要插入的实体对象</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public override int Add(Admin_menuEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			Database.ExecSqlNonQuery(sql_, paras_, tm_); 
	        return 1;
		}
		public override async Task<int> AddAsync(Admin_menuEO item, TransactionManager tm_ = null)
		{
			RepairAddData(item, out string sql_, out List<MySqlParameter> paras_);
			await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_); 
	        return 1;
		}
	    private void RepairAddData(Admin_menuEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"INSERT INTO `admin_menu` (`MenuID`, `SiteID`, `ParentId`, `Title`, `OrderNum`, `Icon`, `LinkMode`, `Url`, `GenID`, `UrlTarget`, `PrivParams`, `Pinyin`, `Desc`, `Status`) VALUE (@MenuID, @SiteID, @ParentId, @Title, @OrderNum, @Icon, @LinkMode, @Url, @GenID, @UrlTarget, @PrivParams, @Pinyin, @Desc, @Status);";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", item.MenuID, MySqlDbType.VarChar),
				Database.CreateInParameter("@SiteID", item.SiteID != null ? item.SiteID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@ParentId", item.ParentId != null ? item.ParentId : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Title", item.Title != null ? item.Title : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@OrderNum", item.OrderNum, MySqlDbType.Int32),
				Database.CreateInParameter("@Icon", item.Icon != null ? item.Icon : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LinkMode", item.LinkMode, MySqlDbType.Int32),
				Database.CreateInParameter("@Url", item.Url != null ? item.Url : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GenID", item.GenID != null ? item.GenID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UrlTarget", item.UrlTarget, MySqlDbType.Byte),
				Database.CreateInParameter("@PrivParams", item.PrivParams != null ? item.PrivParams : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Pinyin", item.Pinyin != null ? item.Pinyin : (object)DBNull.Value, MySqlDbType.VarChar),
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
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPK(string menuID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(menuID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			RepiarRemoveByPKData(menuID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepiarRemoveByPKData(string menuID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_menu` WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
		/// <summary>
		/// 删除指定实体对应的记录
		/// </summary>
		/// <param name = "item">要删除的实体</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Remove(Admin_menuEO item, TransactionManager tm_ = null)
		{
			return RemoveByPK(item.MenuID, tm_);
		}
		public async Task<int> RemoveAsync(Admin_menuEO item, TransactionManager tm_ = null)
		{
			return await RemoveByPKAsync(item.MenuID, tm_);
		}
		#endregion // RemoveByPK
		
		#region RemoveByXXX
		#region RemoveBySiteID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveBySiteID(string siteID, TransactionManager tm_ = null)
		{
			RepairRemoveBySiteIDData(siteID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveBySiteIDAsync(string siteID, TransactionManager tm_ = null)
		{
			RepairRemoveBySiteIDData(siteID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveBySiteIDData(string siteID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_menu` WHERE " + (siteID != null ? "`SiteID` = @SiteID" : "`SiteID` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (siteID != null)
				paras_.Add(Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar));
		}
		#endregion // RemoveBySiteID
		#region RemoveByParentId
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByParentId(string parentId, TransactionManager tm_ = null)
		{
			RepairRemoveByParentIdData(parentId, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByParentIdAsync(string parentId, TransactionManager tm_ = null)
		{
			RepairRemoveByParentIdData(parentId, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByParentIdData(string parentId, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_menu` WHERE " + (parentId != null ? "`ParentId` = @ParentId" : "`ParentId` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (parentId != null)
				paras_.Add(Database.CreateInParameter("@ParentId", parentId, MySqlDbType.VarChar));
		}
		#endregion // RemoveByParentId
		#region RemoveByTitle
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByTitle(string title, TransactionManager tm_ = null)
		{
			RepairRemoveByTitleData(title, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByTitleAsync(string title, TransactionManager tm_ = null)
		{
			RepairRemoveByTitleData(title, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByTitleData(string title, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_menu` WHERE " + (title != null ? "`Title` = @Title" : "`Title` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (title != null)
				paras_.Add(Database.CreateInParameter("@Title", title, MySqlDbType.VarChar));
		}
		#endregion // RemoveByTitle
		#region RemoveByOrderNum
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
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
			sql_ = @"DELETE FROM `admin_menu` WHERE `OrderNum` = @OrderNum";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32));
		}
		#endregion // RemoveByOrderNum
		#region RemoveByIcon
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByIcon(string icon, TransactionManager tm_ = null)
		{
			RepairRemoveByIconData(icon, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByIconAsync(string icon, TransactionManager tm_ = null)
		{
			RepairRemoveByIconData(icon, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByIconData(string icon, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_menu` WHERE " + (icon != null ? "`Icon` = @Icon" : "`Icon` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (icon != null)
				paras_.Add(Database.CreateInParameter("@Icon", icon, MySqlDbType.VarChar));
		}
		#endregion // RemoveByIcon
		#region RemoveByLinkMode
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByLinkMode(int linkMode, TransactionManager tm_ = null)
		{
			RepairRemoveByLinkModeData(linkMode, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByLinkModeAsync(int linkMode, TransactionManager tm_ = null)
		{
			RepairRemoveByLinkModeData(linkMode, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByLinkModeData(int linkMode, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_menu` WHERE `LinkMode` = @LinkMode";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@LinkMode", linkMode, MySqlDbType.Int32));
		}
		#endregion // RemoveByLinkMode
		#region RemoveByUrl
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByUrl(string url, TransactionManager tm_ = null)
		{
			RepairRemoveByUrlData(url, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByUrlAsync(string url, TransactionManager tm_ = null)
		{
			RepairRemoveByUrlData(url, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByUrlData(string url, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_menu` WHERE " + (url != null ? "`Url` = @Url" : "`Url` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (url != null)
				paras_.Add(Database.CreateInParameter("@Url", url, MySqlDbType.VarChar));
		}
		#endregion // RemoveByUrl
		#region RemoveByGenID
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByGenID(string genID, TransactionManager tm_ = null)
		{
			RepairRemoveByGenIDData(genID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByGenIDAsync(string genID, TransactionManager tm_ = null)
		{
			RepairRemoveByGenIDData(genID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByGenIDData(string genID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_menu` WHERE " + (genID != null ? "`GenID` = @GenID" : "`GenID` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (genID != null)
				paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.VarChar));
		}
		#endregion // RemoveByGenID
		#region RemoveByUrlTarget
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByUrlTarget(int urlTarget, TransactionManager tm_ = null)
		{
			RepairRemoveByUrlTargetData(urlTarget, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByUrlTargetAsync(int urlTarget, TransactionManager tm_ = null)
		{
			RepairRemoveByUrlTargetData(urlTarget, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByUrlTargetData(int urlTarget, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_menu` WHERE `UrlTarget` = @UrlTarget";
			paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@UrlTarget", urlTarget, MySqlDbType.Byte));
		}
		#endregion // RemoveByUrlTarget
		#region RemoveByPrivParams
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPrivParams(string privParams, TransactionManager tm_ = null)
		{
			RepairRemoveByPrivParamsData(privParams, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPrivParamsAsync(string privParams, TransactionManager tm_ = null)
		{
			RepairRemoveByPrivParamsData(privParams, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByPrivParamsData(string privParams, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_menu` WHERE " + (privParams != null ? "`PrivParams` = @PrivParams" : "`PrivParams` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (privParams != null)
				paras_.Add(Database.CreateInParameter("@PrivParams", privParams, MySqlDbType.VarChar));
		}
		#endregion // RemoveByPrivParams
		#region RemoveByPinyin
		/// <summary>
		/// 按字段删除
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name="tm_">事务管理对象</param>
		public int RemoveByPinyin(string pinyin, TransactionManager tm_ = null)
		{
			RepairRemoveByPinyinData(pinyin, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> RemoveByPinyinAsync(string pinyin, TransactionManager tm_ = null)
		{
			RepairRemoveByPinyinData(pinyin, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairRemoveByPinyinData(string pinyin, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"DELETE FROM `admin_menu` WHERE " + (pinyin != null ? "`Pinyin` = @Pinyin" : "`Pinyin` IS NULL");
			paras_ = new List<MySqlParameter>();
			if (pinyin != null)
				paras_.Add(Database.CreateInParameter("@Pinyin", pinyin, MySqlDbType.VarChar));
		}
		#endregion // RemoveByPinyin
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
			sql_ = @"DELETE FROM `admin_menu` WHERE " + (desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL");
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
			sql_ = @"DELETE FROM `admin_menu` WHERE `Status` = @Status";
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
		public int Put(Admin_menuEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutAsync(Admin_menuEO item, TransactionManager tm_ = null)
		{
			RepairPutData(item, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutData(Admin_menuEO item, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `MenuID` = @MenuID, `SiteID` = @SiteID, `ParentId` = @ParentId, `Title` = @Title, `OrderNum` = @OrderNum, `Icon` = @Icon, `LinkMode` = @LinkMode, `Url` = @Url, `GenID` = @GenID, `UrlTarget` = @UrlTarget, `PrivParams` = @PrivParams, `Pinyin` = @Pinyin, `Desc` = @Desc, `Status` = @Status WHERE `MenuID` = @MenuID_Original";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", item.MenuID, MySqlDbType.VarChar),
				Database.CreateInParameter("@SiteID", item.SiteID != null ? item.SiteID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@ParentId", item.ParentId != null ? item.ParentId : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Title", item.Title != null ? item.Title : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@OrderNum", item.OrderNum, MySqlDbType.Int32),
				Database.CreateInParameter("@Icon", item.Icon != null ? item.Icon : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@LinkMode", item.LinkMode, MySqlDbType.Int32),
				Database.CreateInParameter("@Url", item.Url != null ? item.Url : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@GenID", item.GenID != null ? item.GenID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@UrlTarget", item.UrlTarget, MySqlDbType.Byte),
				Database.CreateInParameter("@PrivParams", item.PrivParams != null ? item.PrivParams : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Pinyin", item.Pinyin != null ? item.Pinyin : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Desc", item.Desc != null ? item.Desc : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@Status", item.Status, MySqlDbType.Byte),
				Database.CreateInParameter("@MenuID_Original", item.HasOriginal ? item.OriginalMenuID : item.MenuID, MySqlDbType.VarChar),
			};
		}
		
		/// <summary>
		/// 更新实体集合到数据库
		/// </summary>
		/// <param name = "items">要更新的实体对象集合</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int Put(IEnumerable<Admin_menuEO> items, TransactionManager tm_ = null)
		{
			int ret = 0;
			foreach (var item in items)
			{
		        ret += Put(item, tm_);
			}
			return ret;
		}
		public async Task<int> PutAsync(IEnumerable<Admin_menuEO> items, TransactionManager tm_ = null)
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
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string menuID, string set_, params object[] values_)
		{
			return Put(set_, "`MenuID` = @MenuID", ConcatValues(values_, menuID));
		}
		public async Task<int> PutByPKAsync(string menuID, string set_, params object[] values_)
		{
			return await PutAsync(set_, "`MenuID` = @MenuID", ConcatValues(values_, menuID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="tm_">事务管理对象</param>
		/// <param name="values_">参数值</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string menuID, string set_, TransactionManager tm_, params object[] values_)
		{
			return Put(set_, "`MenuID` = @MenuID", tm_, ConcatValues(values_, menuID));
		}
		public async Task<int> PutByPKAsync(string menuID, string set_, TransactionManager tm_, params object[] values_)
		{
			return await PutAsync(set_, "`MenuID` = @MenuID", tm_, ConcatValues(values_, menuID));
		}
		/// <summary>
		/// 按主键更新指定列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "set_">更新的列数据</param>
		/// <param name="paras_">参数值</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutByPK(string menuID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
	        };
			return Put(set_, "`MenuID` = @MenuID", ConcatParameters(paras_, newParas_), tm_);
		}
		public async Task<int> PutByPKAsync(string menuID, string set_, IEnumerable<MySqlParameter> paras_, TransactionManager tm_ = null)
		{
			var newParas_ = new List<MySqlParameter>() {
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
	        };
			return await PutAsync(set_, "`MenuID` = @MenuID", ConcatParameters(paras_, newParas_), tm_);
		}
		#endregion // PutByPK
		
		#region PutXXX
		#region PutSiteID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutSiteIDByPK(string menuID, string siteID, TransactionManager tm_ = null)
		{
			RepairPutSiteIDByPKData(menuID, siteID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutSiteIDByPKAsync(string menuID, string siteID, TransactionManager tm_ = null)
		{
			RepairPutSiteIDByPKData(menuID, siteID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutSiteIDByPKData(string menuID, string siteID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `SiteID` = @SiteID  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@SiteID", siteID != null ? siteID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutSiteID(string siteID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `SiteID` = @SiteID";
			var parameter_ = Database.CreateInParameter("@SiteID", siteID != null ? siteID : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutSiteIDAsync(string siteID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `SiteID` = @SiteID";
			var parameter_ = Database.CreateInParameter("@SiteID", siteID != null ? siteID : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutSiteID
		#region PutParentId
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutParentIdByPK(string menuID, string parentId, TransactionManager tm_ = null)
		{
			RepairPutParentIdByPKData(menuID, parentId, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutParentIdByPKAsync(string menuID, string parentId, TransactionManager tm_ = null)
		{
			RepairPutParentIdByPKData(menuID, parentId, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutParentIdByPKData(string menuID, string parentId, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `ParentId` = @ParentId  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@ParentId", parentId != null ? parentId : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutParentId(string parentId, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `ParentId` = @ParentId";
			var parameter_ = Database.CreateInParameter("@ParentId", parentId != null ? parentId : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutParentIdAsync(string parentId, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `ParentId` = @ParentId";
			var parameter_ = Database.CreateInParameter("@ParentId", parentId != null ? parentId : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutParentId
		#region PutTitle
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTitleByPK(string menuID, string title, TransactionManager tm_ = null)
		{
			RepairPutTitleByPKData(menuID, title, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutTitleByPKAsync(string menuID, string title, TransactionManager tm_ = null)
		{
			RepairPutTitleByPKData(menuID, title, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutTitleByPKData(string menuID, string title, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `Title` = @Title  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Title", title != null ? title : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutTitle(string title, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `Title` = @Title";
			var parameter_ = Database.CreateInParameter("@Title", title != null ? title : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutTitleAsync(string title, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `Title` = @Title";
			var parameter_ = Database.CreateInParameter("@Title", title != null ? title : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutTitle
		#region PutOrderNum
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutOrderNumByPK(string menuID, int orderNum, TransactionManager tm_ = null)
		{
			RepairPutOrderNumByPKData(menuID, orderNum, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutOrderNumByPKAsync(string menuID, int orderNum, TransactionManager tm_ = null)
		{
			RepairPutOrderNumByPKData(menuID, orderNum, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutOrderNumByPKData(string menuID, int orderNum, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `OrderNum` = @OrderNum  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutOrderNum(int orderNum, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `OrderNum` = @OrderNum";
			var parameter_ = Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutOrderNumAsync(int orderNum, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `OrderNum` = @OrderNum";
			var parameter_ = Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutOrderNum
		#region PutIcon
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "icon">图标</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIconByPK(string menuID, string icon, TransactionManager tm_ = null)
		{
			RepairPutIconByPKData(menuID, icon, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutIconByPKAsync(string menuID, string icon, TransactionManager tm_ = null)
		{
			RepairPutIconByPKData(menuID, icon, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutIconByPKData(string menuID, string icon, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `Icon` = @Icon  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Icon", icon != null ? icon : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutIcon(string icon, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `Icon` = @Icon";
			var parameter_ = Database.CreateInParameter("@Icon", icon != null ? icon : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutIconAsync(string icon, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `Icon` = @Icon";
			var parameter_ = Database.CreateInParameter("@Icon", icon != null ? icon : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutIcon
		#region PutLinkMode
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLinkModeByPK(string menuID, int linkMode, TransactionManager tm_ = null)
		{
			RepairPutLinkModeByPKData(menuID, linkMode, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutLinkModeByPKAsync(string menuID, int linkMode, TransactionManager tm_ = null)
		{
			RepairPutLinkModeByPKData(menuID, linkMode, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutLinkModeByPKData(string menuID, int linkMode, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `LinkMode` = @LinkMode  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@LinkMode", linkMode, MySqlDbType.Int32),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutLinkMode(int linkMode, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `LinkMode` = @LinkMode";
			var parameter_ = Database.CreateInParameter("@LinkMode", linkMode, MySqlDbType.Int32);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutLinkModeAsync(int linkMode, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `LinkMode` = @LinkMode";
			var parameter_ = Database.CreateInParameter("@LinkMode", linkMode, MySqlDbType.Int32);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutLinkMode
		#region PutUrl
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUrlByPK(string menuID, string url, TransactionManager tm_ = null)
		{
			RepairPutUrlByPKData(menuID, url, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutUrlByPKAsync(string menuID, string url, TransactionManager tm_ = null)
		{
			RepairPutUrlByPKData(menuID, url, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutUrlByPKData(string menuID, string url, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `Url` = @Url  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Url", url != null ? url : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUrl(string url, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `Url` = @Url";
			var parameter_ = Database.CreateInParameter("@Url", url != null ? url : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutUrlAsync(string url, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `Url` = @Url";
			var parameter_ = Database.CreateInParameter("@Url", url != null ? url : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutUrl
		#region PutGenID
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGenIDByPK(string menuID, string genID, TransactionManager tm_ = null)
		{
			RepairPutGenIDByPKData(menuID, genID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutGenIDByPKAsync(string menuID, string genID, TransactionManager tm_ = null)
		{
			RepairPutGenIDByPKData(menuID, genID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutGenIDByPKData(string menuID, string genID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `GenID` = @GenID  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@GenID", genID != null ? genID : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutGenID(string genID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `GenID` = @GenID";
			var parameter_ = Database.CreateInParameter("@GenID", genID != null ? genID : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutGenIDAsync(string genID, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `GenID` = @GenID";
			var parameter_ = Database.CreateInParameter("@GenID", genID != null ? genID : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutGenID
		#region PutUrlTarget
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUrlTargetByPK(string menuID, int urlTarget, TransactionManager tm_ = null)
		{
			RepairPutUrlTargetByPKData(menuID, urlTarget, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutUrlTargetByPKAsync(string menuID, int urlTarget, TransactionManager tm_ = null)
		{
			RepairPutUrlTargetByPKData(menuID, urlTarget, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutUrlTargetByPKData(string menuID, int urlTarget, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `UrlTarget` = @UrlTarget  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@UrlTarget", urlTarget, MySqlDbType.Byte),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutUrlTarget(int urlTarget, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `UrlTarget` = @UrlTarget";
			var parameter_ = Database.CreateInParameter("@UrlTarget", urlTarget, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutUrlTargetAsync(int urlTarget, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `UrlTarget` = @UrlTarget";
			var parameter_ = Database.CreateInParameter("@UrlTarget", urlTarget, MySqlDbType.Byte);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutUrlTarget
		#region PutPrivParams
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPrivParamsByPK(string menuID, string privParams, TransactionManager tm_ = null)
		{
			RepairPutPrivParamsByPKData(menuID, privParams, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutPrivParamsByPKAsync(string menuID, string privParams, TransactionManager tm_ = null)
		{
			RepairPutPrivParamsByPKData(menuID, privParams, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutPrivParamsByPKData(string menuID, string privParams, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `PrivParams` = @PrivParams  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@PrivParams", privParams != null ? privParams : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPrivParams(string privParams, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `PrivParams` = @PrivParams";
			var parameter_ = Database.CreateInParameter("@PrivParams", privParams != null ? privParams : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutPrivParamsAsync(string privParams, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `PrivParams` = @PrivParams";
			var parameter_ = Database.CreateInParameter("@PrivParams", privParams != null ? privParams : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutPrivParams
		#region PutPinyin
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPinyinByPK(string menuID, string pinyin, TransactionManager tm_ = null)
		{
			RepairPutPinyinByPKData(menuID, pinyin, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutPinyinByPKAsync(string menuID, string pinyin, TransactionManager tm_ = null)
		{
			RepairPutPinyinByPKData(menuID, pinyin, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutPinyinByPKData(string menuID, string pinyin, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `Pinyin` = @Pinyin  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Pinyin", pinyin != null ? pinyin : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
	 
		/// <summary>
		/// 更新一列数据
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutPinyin(string pinyin, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `Pinyin` = @Pinyin";
			var parameter_ = Database.CreateInParameter("@Pinyin", pinyin != null ? pinyin : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutPinyinAsync(string pinyin, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `Pinyin` = @Pinyin";
			var parameter_ = Database.CreateInParameter("@Pinyin", pinyin != null ? pinyin : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutPinyin
		#region PutDesc
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutDescByPK(string menuID, string desc, TransactionManager tm_ = null)
		{
			RepairPutDescByPKData(menuID, desc, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutDescByPKAsync(string menuID, string desc, TransactionManager tm_ = null)
		{
			RepairPutDescByPKData(menuID, desc, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutDescByPKData(string menuID, string desc, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `Desc` = @Desc  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.VarChar),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
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
			const string sql_ = @"UPDATE `admin_menu` SET `Desc` = @Desc";
			var parameter_ = Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutDescAsync(string desc, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `Desc` = @Desc";
			var parameter_ = Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.VarChar);
			return await Database.ExecSqlNonQueryAsync(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		#endregion // PutDesc
		#region PutStatus
		/// <summary>
		/// 按主键更新列数据
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>受影响的行数</return>
		public int PutStatusByPK(string menuID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(menuID, status, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlNonQuery(sql_, paras_, tm_);
		}
		public async Task<int> PutStatusByPKAsync(string menuID, int status, TransactionManager tm_ = null)
		{
			RepairPutStatusByPKData(menuID, status, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlNonQueryAsync(sql_, paras_, tm_);
		}
		private void RepairPutStatusByPKData(string menuID, int status, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = @"UPDATE `admin_menu` SET `Status` = @Status  WHERE `MenuID` = @MenuID";
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@Status", status, MySqlDbType.Byte),
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
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
			const string sql_ = @"UPDATE `admin_menu` SET `Status` = @Status";
			var parameter_ = Database.CreateInParameter("@Status", status, MySqlDbType.Byte);
			return Database.ExecSqlNonQuery(sql_, new MySqlParameter[] { parameter_ }, tm_);
		}
		public async Task<int> PutStatusAsync(int status, TransactionManager tm_ = null)
		{
			const string sql_ = @"UPDATE `admin_menu` SET `Status` = @Status";
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
		public bool Set(Admin_menuEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.MenuID) == null)
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
		public async Task<bool> SetAsync(Admin_menuEO item, TransactionManager tm = null)
		{
			bool ret = true;
			if(GetByPK(item.MenuID) == null)
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
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public Admin_menuEO GetByPK(string menuID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(menuID, out string sql_, out List<MySqlParameter> paras_);
			return Database.ExecSqlSingle(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<Admin_menuEO> GetByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			RepairGetByPKData(menuID, out string sql_, out List<MySqlParameter> paras_);
			return await Database.ExecSqlSingleAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		private void RepairGetByPKData(string menuID, out string sql_, out List<MySqlParameter> paras_)
		{
			sql_ = BuildSelectSQL("`MenuID` = @MenuID", 0, null);
			paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
		}
		#endregion // GetByPK
		
		#region GetXXXByPK
		
		/// <summary>
		/// 按主键查询 SiteID（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetSiteIDByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`SiteID`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<string> GetSiteIDByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`SiteID`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 ParentId（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetParentIdByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`ParentId`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<string> GetParentIdByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`ParentId`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Title（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetTitleByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Title`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<string> GetTitleByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Title`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 OrderNum（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetOrderNumByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`OrderNum`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<int> GetOrderNumByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`OrderNum`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Icon（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetIconByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Icon`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<string> GetIconByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Icon`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 LinkMode（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetLinkModeByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`LinkMode`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<int> GetLinkModeByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`LinkMode`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Url（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetUrlByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Url`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<string> GetUrlByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Url`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 GenID（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetGenIDByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`GenID`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<string> GetGenIDByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`GenID`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 UrlTarget（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetUrlTargetByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`UrlTarget`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<int> GetUrlTargetByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`UrlTarget`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 PrivParams（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetPrivParamsByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`PrivParams`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<string> GetPrivParamsByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`PrivParams`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Pinyin（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetPinyinByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Pinyin`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<string> GetPinyinByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Pinyin`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Desc（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public string GetDescByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)GetScalar("`Desc`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<string> GetDescByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (string)await GetScalarAsync("`Desc`", "`MenuID` = @MenuID", paras_, tm_);
		}
		
		/// <summary>
		/// 按主键查询 Status（字段）
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return></return>
		public int GetStatusByPK(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (int)GetScalar("`Status`", "`MenuID` = @MenuID", paras_, tm_);
		}
		public async Task<int> GetStatusByPKAsync(string menuID, TransactionManager tm_ = null)
		{
			var paras_ = new List<MySqlParameter>() 
			{
				Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar),
			};
			return (int)await GetScalarAsync("`Status`", "`MenuID` = @MenuID", paras_, tm_);
		}
		#endregion // GetXXXByPK
		#region GetByXXX
		#region GetBySiteID
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetBySiteID(string siteID)
		{
			return GetBySiteID(siteID, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetBySiteIDAsync(string siteID)
		{
			return await GetBySiteIDAsync(siteID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetBySiteID(string siteID, TransactionManager tm_)
		{
			return GetBySiteID(siteID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetBySiteIDAsync(string siteID, TransactionManager tm_)
		{
			return await GetBySiteIDAsync(siteID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetBySiteID(string siteID, int top_)
		{
			return GetBySiteID(siteID, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetBySiteIDAsync(string siteID, int top_)
		{
			return await GetBySiteIDAsync(siteID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetBySiteID(string siteID, int top_, TransactionManager tm_)
		{
			return GetBySiteID(siteID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetBySiteIDAsync(string siteID, int top_, TransactionManager tm_)
		{
			return await GetBySiteIDAsync(siteID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetBySiteID(string siteID, string sort_)
		{
			return GetBySiteID(siteID, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetBySiteIDAsync(string siteID, string sort_)
		{
			return await GetBySiteIDAsync(siteID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetBySiteID(string siteID, string sort_, TransactionManager tm_)
		{
			return GetBySiteID(siteID, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetBySiteIDAsync(string siteID, string sort_, TransactionManager tm_)
		{
			return await GetBySiteIDAsync(siteID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetBySiteID(string siteID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(siteID != null ? "`SiteID` = @SiteID" : "`SiteID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (siteID != null)
				paras_.Add(Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetBySiteIDAsync(string siteID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(siteID != null ? "`SiteID` = @SiteID" : "`SiteID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (siteID != null)
				paras_.Add(Database.CreateInParameter("@SiteID", siteID, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetBySiteID
		#region GetByParentId
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByParentId(string parentId)
		{
			return GetByParentId(parentId, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByParentIdAsync(string parentId)
		{
			return await GetByParentIdAsync(parentId, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByParentId(string parentId, TransactionManager tm_)
		{
			return GetByParentId(parentId, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByParentIdAsync(string parentId, TransactionManager tm_)
		{
			return await GetByParentIdAsync(parentId, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByParentId(string parentId, int top_)
		{
			return GetByParentId(parentId, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByParentIdAsync(string parentId, int top_)
		{
			return await GetByParentIdAsync(parentId, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByParentId(string parentId, int top_, TransactionManager tm_)
		{
			return GetByParentId(parentId, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByParentIdAsync(string parentId, int top_, TransactionManager tm_)
		{
			return await GetByParentIdAsync(parentId, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByParentId(string parentId, string sort_)
		{
			return GetByParentId(parentId, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByParentIdAsync(string parentId, string sort_)
		{
			return await GetByParentIdAsync(parentId, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByParentId(string parentId, string sort_, TransactionManager tm_)
		{
			return GetByParentId(parentId, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByParentIdAsync(string parentId, string sort_, TransactionManager tm_)
		{
			return await GetByParentIdAsync(parentId, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByParentId(string parentId, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(parentId != null ? "`ParentId` = @ParentId" : "`ParentId` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (parentId != null)
				paras_.Add(Database.CreateInParameter("@ParentId", parentId, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByParentIdAsync(string parentId, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(parentId != null ? "`ParentId` = @ParentId" : "`ParentId` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (parentId != null)
				paras_.Add(Database.CreateInParameter("@ParentId", parentId, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByParentId
		#region GetByTitle
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByTitle(string title)
		{
			return GetByTitle(title, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByTitleAsync(string title)
		{
			return await GetByTitleAsync(title, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByTitle(string title, TransactionManager tm_)
		{
			return GetByTitle(title, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByTitleAsync(string title, TransactionManager tm_)
		{
			return await GetByTitleAsync(title, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByTitle(string title, int top_)
		{
			return GetByTitle(title, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByTitleAsync(string title, int top_)
		{
			return await GetByTitleAsync(title, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByTitle(string title, int top_, TransactionManager tm_)
		{
			return GetByTitle(title, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByTitleAsync(string title, int top_, TransactionManager tm_)
		{
			return await GetByTitleAsync(title, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByTitle(string title, string sort_)
		{
			return GetByTitle(title, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByTitleAsync(string title, string sort_)
		{
			return await GetByTitleAsync(title, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByTitle(string title, string sort_, TransactionManager tm_)
		{
			return GetByTitle(title, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByTitleAsync(string title, string sort_, TransactionManager tm_)
		{
			return await GetByTitleAsync(title, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByTitle(string title, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(title != null ? "`Title` = @Title" : "`Title` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (title != null)
				paras_.Add(Database.CreateInParameter("@Title", title, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByTitleAsync(string title, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(title != null ? "`Title` = @Title" : "`Title` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (title != null)
				paras_.Add(Database.CreateInParameter("@Title", title, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByTitle
		#region GetByOrderNum
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByOrderNum(int orderNum)
		{
			return GetByOrderNum(orderNum, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByOrderNumAsync(int orderNum)
		{
			return await GetByOrderNumAsync(orderNum, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByOrderNum(int orderNum, TransactionManager tm_)
		{
			return GetByOrderNum(orderNum, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByOrderNumAsync(int orderNum, TransactionManager tm_)
		{
			return await GetByOrderNumAsync(orderNum, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByOrderNum(int orderNum, int top_)
		{
			return GetByOrderNum(orderNum, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByOrderNumAsync(int orderNum, int top_)
		{
			return await GetByOrderNumAsync(orderNum, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByOrderNum(int orderNum, int top_, TransactionManager tm_)
		{
			return GetByOrderNum(orderNum, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByOrderNumAsync(int orderNum, int top_, TransactionManager tm_)
		{
			return await GetByOrderNumAsync(orderNum, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByOrderNum(int orderNum, string sort_)
		{
			return GetByOrderNum(orderNum, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByOrderNumAsync(int orderNum, string sort_)
		{
			return await GetByOrderNumAsync(orderNum, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByOrderNum(int orderNum, string sort_, TransactionManager tm_)
		{
			return GetByOrderNum(orderNum, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByOrderNumAsync(int orderNum, string sort_, TransactionManager tm_)
		{
			return await GetByOrderNumAsync(orderNum, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByOrderNum(int orderNum, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`OrderNum` = @OrderNum", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByOrderNumAsync(int orderNum, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`OrderNum` = @OrderNum", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByOrderNum
		#region GetByIcon
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByIcon(string icon)
		{
			return GetByIcon(icon, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByIconAsync(string icon)
		{
			return await GetByIconAsync(icon, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByIcon(string icon, TransactionManager tm_)
		{
			return GetByIcon(icon, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByIconAsync(string icon, TransactionManager tm_)
		{
			return await GetByIconAsync(icon, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByIcon(string icon, int top_)
		{
			return GetByIcon(icon, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByIconAsync(string icon, int top_)
		{
			return await GetByIconAsync(icon, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByIcon(string icon, int top_, TransactionManager tm_)
		{
			return GetByIcon(icon, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByIconAsync(string icon, int top_, TransactionManager tm_)
		{
			return await GetByIconAsync(icon, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByIcon(string icon, string sort_)
		{
			return GetByIcon(icon, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByIconAsync(string icon, string sort_)
		{
			return await GetByIconAsync(icon, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByIcon(string icon, string sort_, TransactionManager tm_)
		{
			return GetByIcon(icon, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByIconAsync(string icon, string sort_, TransactionManager tm_)
		{
			return await GetByIconAsync(icon, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByIcon(string icon, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(icon != null ? "`Icon` = @Icon" : "`Icon` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (icon != null)
				paras_.Add(Database.CreateInParameter("@Icon", icon, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByIconAsync(string icon, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(icon != null ? "`Icon` = @Icon" : "`Icon` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (icon != null)
				paras_.Add(Database.CreateInParameter("@Icon", icon, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByIcon
		#region GetByLinkMode
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByLinkMode(int linkMode)
		{
			return GetByLinkMode(linkMode, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByLinkModeAsync(int linkMode)
		{
			return await GetByLinkModeAsync(linkMode, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByLinkMode(int linkMode, TransactionManager tm_)
		{
			return GetByLinkMode(linkMode, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByLinkModeAsync(int linkMode, TransactionManager tm_)
		{
			return await GetByLinkModeAsync(linkMode, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByLinkMode(int linkMode, int top_)
		{
			return GetByLinkMode(linkMode, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByLinkModeAsync(int linkMode, int top_)
		{
			return await GetByLinkModeAsync(linkMode, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByLinkMode(int linkMode, int top_, TransactionManager tm_)
		{
			return GetByLinkMode(linkMode, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByLinkModeAsync(int linkMode, int top_, TransactionManager tm_)
		{
			return await GetByLinkModeAsync(linkMode, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByLinkMode(int linkMode, string sort_)
		{
			return GetByLinkMode(linkMode, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByLinkModeAsync(int linkMode, string sort_)
		{
			return await GetByLinkModeAsync(linkMode, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByLinkMode(int linkMode, string sort_, TransactionManager tm_)
		{
			return GetByLinkMode(linkMode, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByLinkModeAsync(int linkMode, string sort_, TransactionManager tm_)
		{
			return await GetByLinkModeAsync(linkMode, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByLinkMode(int linkMode, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`LinkMode` = @LinkMode", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@LinkMode", linkMode, MySqlDbType.Int32));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByLinkModeAsync(int linkMode, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`LinkMode` = @LinkMode", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@LinkMode", linkMode, MySqlDbType.Int32));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByLinkMode
		#region GetByUrl
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrl(string url)
		{
			return GetByUrl(url, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByUrlAsync(string url)
		{
			return await GetByUrlAsync(url, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrl(string url, TransactionManager tm_)
		{
			return GetByUrl(url, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByUrlAsync(string url, TransactionManager tm_)
		{
			return await GetByUrlAsync(url, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrl(string url, int top_)
		{
			return GetByUrl(url, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByUrlAsync(string url, int top_)
		{
			return await GetByUrlAsync(url, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrl(string url, int top_, TransactionManager tm_)
		{
			return GetByUrl(url, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByUrlAsync(string url, int top_, TransactionManager tm_)
		{
			return await GetByUrlAsync(url, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrl(string url, string sort_)
		{
			return GetByUrl(url, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByUrlAsync(string url, string sort_)
		{
			return await GetByUrlAsync(url, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrl(string url, string sort_, TransactionManager tm_)
		{
			return GetByUrl(url, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByUrlAsync(string url, string sort_, TransactionManager tm_)
		{
			return await GetByUrlAsync(url, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrl(string url, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(url != null ? "`Url` = @Url" : "`Url` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (url != null)
				paras_.Add(Database.CreateInParameter("@Url", url, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByUrlAsync(string url, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(url != null ? "`Url` = @Url" : "`Url` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (url != null)
				paras_.Add(Database.CreateInParameter("@Url", url, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByUrl
		#region GetByGenID
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByGenID(string genID)
		{
			return GetByGenID(genID, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByGenIDAsync(string genID)
		{
			return await GetByGenIDAsync(genID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByGenID(string genID, TransactionManager tm_)
		{
			return GetByGenID(genID, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByGenIDAsync(string genID, TransactionManager tm_)
		{
			return await GetByGenIDAsync(genID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByGenID(string genID, int top_)
		{
			return GetByGenID(genID, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByGenIDAsync(string genID, int top_)
		{
			return await GetByGenIDAsync(genID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByGenID(string genID, int top_, TransactionManager tm_)
		{
			return GetByGenID(genID, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByGenIDAsync(string genID, int top_, TransactionManager tm_)
		{
			return await GetByGenIDAsync(genID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByGenID(string genID, string sort_)
		{
			return GetByGenID(genID, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByGenIDAsync(string genID, string sort_)
		{
			return await GetByGenIDAsync(genID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByGenID(string genID, string sort_, TransactionManager tm_)
		{
			return GetByGenID(genID, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByGenIDAsync(string genID, string sort_, TransactionManager tm_)
		{
			return await GetByGenIDAsync(genID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByGenID(string genID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(genID != null ? "`GenID` = @GenID" : "`GenID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (genID != null)
				paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByGenIDAsync(string genID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(genID != null ? "`GenID` = @GenID" : "`GenID` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (genID != null)
				paras_.Add(Database.CreateInParameter("@GenID", genID, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByGenID
		#region GetByUrlTarget
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrlTarget(int urlTarget)
		{
			return GetByUrlTarget(urlTarget, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByUrlTargetAsync(int urlTarget)
		{
			return await GetByUrlTargetAsync(urlTarget, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrlTarget(int urlTarget, TransactionManager tm_)
		{
			return GetByUrlTarget(urlTarget, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByUrlTargetAsync(int urlTarget, TransactionManager tm_)
		{
			return await GetByUrlTargetAsync(urlTarget, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrlTarget(int urlTarget, int top_)
		{
			return GetByUrlTarget(urlTarget, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByUrlTargetAsync(int urlTarget, int top_)
		{
			return await GetByUrlTargetAsync(urlTarget, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrlTarget(int urlTarget, int top_, TransactionManager tm_)
		{
			return GetByUrlTarget(urlTarget, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByUrlTargetAsync(int urlTarget, int top_, TransactionManager tm_)
		{
			return await GetByUrlTargetAsync(urlTarget, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrlTarget(int urlTarget, string sort_)
		{
			return GetByUrlTarget(urlTarget, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByUrlTargetAsync(int urlTarget, string sort_)
		{
			return await GetByUrlTargetAsync(urlTarget, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrlTarget(int urlTarget, string sort_, TransactionManager tm_)
		{
			return GetByUrlTarget(urlTarget, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByUrlTargetAsync(int urlTarget, string sort_, TransactionManager tm_)
		{
			return await GetByUrlTargetAsync(urlTarget, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByUrlTarget(int urlTarget, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`UrlTarget` = @UrlTarget", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@UrlTarget", urlTarget, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByUrlTargetAsync(int urlTarget, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`UrlTarget` = @UrlTarget", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@UrlTarget", urlTarget, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByUrlTarget
		#region GetByPrivParams
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPrivParams(string privParams)
		{
			return GetByPrivParams(privParams, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByPrivParamsAsync(string privParams)
		{
			return await GetByPrivParamsAsync(privParams, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPrivParams(string privParams, TransactionManager tm_)
		{
			return GetByPrivParams(privParams, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByPrivParamsAsync(string privParams, TransactionManager tm_)
		{
			return await GetByPrivParamsAsync(privParams, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPrivParams(string privParams, int top_)
		{
			return GetByPrivParams(privParams, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByPrivParamsAsync(string privParams, int top_)
		{
			return await GetByPrivParamsAsync(privParams, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPrivParams(string privParams, int top_, TransactionManager tm_)
		{
			return GetByPrivParams(privParams, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByPrivParamsAsync(string privParams, int top_, TransactionManager tm_)
		{
			return await GetByPrivParamsAsync(privParams, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPrivParams(string privParams, string sort_)
		{
			return GetByPrivParams(privParams, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByPrivParamsAsync(string privParams, string sort_)
		{
			return await GetByPrivParamsAsync(privParams, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPrivParams(string privParams, string sort_, TransactionManager tm_)
		{
			return GetByPrivParams(privParams, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByPrivParamsAsync(string privParams, string sort_, TransactionManager tm_)
		{
			return await GetByPrivParamsAsync(privParams, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPrivParams(string privParams, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(privParams != null ? "`PrivParams` = @PrivParams" : "`PrivParams` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (privParams != null)
				paras_.Add(Database.CreateInParameter("@PrivParams", privParams, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByPrivParamsAsync(string privParams, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(privParams != null ? "`PrivParams` = @PrivParams" : "`PrivParams` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (privParams != null)
				paras_.Add(Database.CreateInParameter("@PrivParams", privParams, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByPrivParams
		#region GetByPinyin
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPinyin(string pinyin)
		{
			return GetByPinyin(pinyin, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByPinyinAsync(string pinyin)
		{
			return await GetByPinyinAsync(pinyin, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPinyin(string pinyin, TransactionManager tm_)
		{
			return GetByPinyin(pinyin, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByPinyinAsync(string pinyin, TransactionManager tm_)
		{
			return await GetByPinyinAsync(pinyin, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPinyin(string pinyin, int top_)
		{
			return GetByPinyin(pinyin, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByPinyinAsync(string pinyin, int top_)
		{
			return await GetByPinyinAsync(pinyin, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPinyin(string pinyin, int top_, TransactionManager tm_)
		{
			return GetByPinyin(pinyin, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByPinyinAsync(string pinyin, int top_, TransactionManager tm_)
		{
			return await GetByPinyinAsync(pinyin, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPinyin(string pinyin, string sort_)
		{
			return GetByPinyin(pinyin, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByPinyinAsync(string pinyin, string sort_)
		{
			return await GetByPinyinAsync(pinyin, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPinyin(string pinyin, string sort_, TransactionManager tm_)
		{
			return GetByPinyin(pinyin, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByPinyinAsync(string pinyin, string sort_, TransactionManager tm_)
		{
			return await GetByPinyinAsync(pinyin, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByPinyin(string pinyin, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(pinyin != null ? "`Pinyin` = @Pinyin" : "`Pinyin` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (pinyin != null)
				paras_.Add(Database.CreateInParameter("@Pinyin", pinyin, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByPinyinAsync(string pinyin, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(pinyin != null ? "`Pinyin` = @Pinyin" : "`Pinyin` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (pinyin != null)
				paras_.Add(Database.CreateInParameter("@Pinyin", pinyin, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByPinyin
		#region GetByDesc
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByDesc(string desc)
		{
			return GetByDesc(desc, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByDescAsync(string desc)
		{
			return await GetByDescAsync(desc, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByDesc(string desc, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByDescAsync(string desc, TransactionManager tm_)
		{
			return await GetByDescAsync(desc, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByDesc(string desc, int top_)
		{
			return GetByDesc(desc, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByDescAsync(string desc, int top_)
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
		public List<Admin_menuEO> GetByDesc(string desc, int top_, TransactionManager tm_)
		{
			return GetByDesc(desc, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByDescAsync(string desc, int top_, TransactionManager tm_)
		{
			return await GetByDescAsync(desc, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByDesc(string desc, string sort_)
		{
			return GetByDesc(desc, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByDescAsync(string desc, string sort_)
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
		public List<Admin_menuEO> GetByDesc(string desc, string sort_, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByDescAsync(string desc, string sort_, TransactionManager tm_)
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
		public List<Admin_menuEO> GetByDesc(string desc, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.VarChar));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByDescAsync(string desc, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL(desc != null ? "`Desc` = @Desc" : "`Desc` IS NULL", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			if (desc != null)
				paras_.Add(Database.CreateInParameter("@Desc", desc, MySqlDbType.VarChar));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByDesc
		#region GetByStatus
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByStatus(int status)
		{
			return GetByStatus(status, 0, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByStatusAsync(int status)
		{
			return await GetByStatusAsync(status, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByStatus(int status, TransactionManager tm_)
		{
			return GetByStatus(status, 0, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByStatusAsync(int status, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByStatus(int status, int top_)
		{
			return GetByStatus(status, top_, string.Empty, null);
		}
		public async Task<List<Admin_menuEO>> GetByStatusAsync(int status, int top_)
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
		public List<Admin_menuEO> GetByStatus(int status, int top_, TransactionManager tm_)
		{
			return GetByStatus(status, top_, string.Empty, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByStatusAsync(int status, int top_, TransactionManager tm_)
		{
			return await GetByStatusAsync(status, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<Admin_menuEO> GetByStatus(int status, string sort_)
		{
			return GetByStatus(status, 0, sort_, null);
		}
		public async Task<List<Admin_menuEO>> GetByStatusAsync(int status, string sort_)
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
		public List<Admin_menuEO> GetByStatus(int status, string sort_, TransactionManager tm_)
		{
			return GetByStatus(status, 0, sort_, tm_);
		}
		public async Task<List<Admin_menuEO>> GetByStatusAsync(int status, string sort_, TransactionManager tm_)
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
		public List<Admin_menuEO> GetByStatus(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
			return Database.ExecSqlList(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		public async Task<List<Admin_menuEO>> GetByStatusAsync(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var paras_ = new List<MySqlParameter>();
			paras_.Add(Database.CreateInParameter("@Status", status, MySqlDbType.Byte));
			return await Database.ExecSqlListAsync(sql_, paras_, tm_, Admin_menuEO.MapDataReader);
		}
		#endregion // GetByStatus
		#endregion // GetByXXX
		#endregion // Get
	}
	#endregion // MO
}
