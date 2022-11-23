/******************************************************
 * 此代码由代码生成器工具自动生成，请不要修改
 * TinyFx代码生成器核心库版本号：1.0.0.0
 * git: https://github.com/jh98net/TinyFx
 * 文档生成时间：2022-11-13 22: 11:30
 ******************************************************/
using System;
using System.Data;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyFx;
using TinyFx.Data;
using MySql.Data.MySqlClient;
using TinyFx.Data.MySql;

namespace TinyFx.Admin.DAL
{
	#region EO
	/// <summary>
	/// 
	/// 【视图 v_admin_user_owner_priv 的实体类】
	/// </summary>
	[Serializable]
	public class V_admin_user_owner_privEO : IRowMapper<V_admin_user_owner_privEO>
	{
		#region 所有字段
		/// <summary>
		/// 菜单编码GUID
		/// 【字段 varchar(40)】
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
		/// <summary>
		/// 
		/// 【字段 int】
		/// </summary>
		[DataMember(Order = 15)]
		public int RoleID { get; set; }
		/// <summary>
		/// 功能和数据权限参数。格式：类型-参数| 类型-参数
		///              用于在定义页面内权限时可设置的权限选项列表
		///              如：ControlID-btnOk|ControlID-btnCancle
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 16)]
		public string PrivParamsValue { get; set; }
		/// <summary>
		/// 是否允许
		/// 【字段 tinyint(1)】
		/// </summary>
		[DataMember(Order = 17)]
		public bool IsEnabled { get; set; }
		/// <summary>
		/// 管理用户ID
		/// 【字段 varchar(40)】
		/// </summary>
		[DataMember(Order = 18)]
		public string UserID { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public V_admin_user_owner_privEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static V_admin_user_owner_privEO MapDataReader(IDataReader reader)
		{
		    V_admin_user_owner_privEO ret = new V_admin_user_owner_privEO();
			ret.MenuID = reader.ToString("MenuID");
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
			ret.RoleID = reader.ToInt32("RoleID");
			ret.PrivParamsValue = reader.ToString("PrivParamsValue");
			ret.IsEnabled = reader.ToBoolean("IsEnabled");
			ret.UserID = reader.ToString("UserID");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 
	/// 【视图 v_admin_user_owner_priv 的操作类】
	/// </summary>
	public class V_admin_user_owner_privMO : MySqlViewMO<V_admin_user_owner_privEO>
	{
		/// <summary>
		/// 视图名
		/// </summary>
	    public override string ViewName => "`v_admin_user_owner_priv`"; 
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public V_admin_user_owner_privMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public V_admin_user_owner_privMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public V_admin_user_owner_privMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
		#region Get
		#region GetByMenuID
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByMenuID(string menuID)
		{
			return GetByMenuID(menuID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByMenuID(string menuID, TransactionManager tm_)
		{
			return GetByMenuID(menuID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByMenuID(string menuID, int top_)
		{
			return GetByMenuID(menuID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByMenuID(string menuID, int top_, TransactionManager tm_)
		{
			return GetByMenuID(menuID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByMenuID(string menuID, string sort_)
		{
			return GetByMenuID(menuID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByMenuID(string menuID, string sort_, TransactionManager tm_)
		{
			return GetByMenuID(menuID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 MenuID（字段） 查询
		/// </summary>
		/// /// <param name = "menuID">菜单编码GUID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByMenuID(string menuID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`MenuID` = @MenuID", top_, sort_);
			var parameter_ = Database.CreateInParameter("@MenuID", menuID, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByMenuID
		#region GetBySiteID
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetBySiteID(string siteID)
		{
			return GetBySiteID(siteID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetBySiteID(string siteID, TransactionManager tm_)
		{
			return GetBySiteID(siteID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetBySiteID(string siteID, int top_)
		{
			return GetBySiteID(siteID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetBySiteID(string siteID, int top_, TransactionManager tm_)
		{
			return GetBySiteID(siteID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetBySiteID(string siteID, string sort_)
		{
			return GetBySiteID(siteID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetBySiteID(string siteID, string sort_, TransactionManager tm_)
		{
			return GetBySiteID(siteID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 SiteID（字段） 查询
		/// </summary>
		/// /// <param name = "siteID">站点编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetBySiteID(string siteID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`SiteID` = @SiteID", top_, sort_);
			var parameter_ = Database.CreateInParameter("@SiteID", siteID != null ? siteID : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetBySiteID
		#region GetByParentId
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByParentId(string parentId)
		{
			return GetByParentId(parentId, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByParentId(string parentId, TransactionManager tm_)
		{
			return GetByParentId(parentId, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByParentId(string parentId, int top_)
		{
			return GetByParentId(parentId, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByParentId(string parentId, int top_, TransactionManager tm_)
		{
			return GetByParentId(parentId, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByParentId(string parentId, string sort_)
		{
			return GetByParentId(parentId, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByParentId(string parentId, string sort_, TransactionManager tm_)
		{
			return GetByParentId(parentId, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ParentId（字段） 查询
		/// </summary>
		/// /// <param name = "parentId">父菜单编码 0-根菜单</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByParentId(string parentId, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`ParentId` = @ParentId", top_, sort_);
			var parameter_ = Database.CreateInParameter("@ParentId", parentId != null ? parentId : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByParentId
		#region GetByTitle
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByTitle(string title)
		{
			return GetByTitle(title, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByTitle(string title, TransactionManager tm_)
		{
			return GetByTitle(title, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByTitle(string title, int top_)
		{
			return GetByTitle(title, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByTitle(string title, int top_, TransactionManager tm_)
		{
			return GetByTitle(title, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByTitle(string title, string sort_)
		{
			return GetByTitle(title, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByTitle(string title, string sort_, TransactionManager tm_)
		{
			return GetByTitle(title, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Title（字段） 查询
		/// </summary>
		/// /// <param name = "title">菜单显示标题</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByTitle(string title, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Title` = @Title", top_, sort_);
			var parameter_ = Database.CreateInParameter("@Title", title != null ? title : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByTitle
		#region GetByOrderNum
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByOrderNum(int orderNum)
		{
			return GetByOrderNum(orderNum, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByOrderNum(int orderNum, TransactionManager tm_)
		{
			return GetByOrderNum(orderNum, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByOrderNum(int orderNum, int top_)
		{
			return GetByOrderNum(orderNum, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByOrderNum(int orderNum, int top_, TransactionManager tm_)
		{
			return GetByOrderNum(orderNum, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByOrderNum(int orderNum, string sort_)
		{
			return GetByOrderNum(orderNum, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByOrderNum(int orderNum, string sort_, TransactionManager tm_)
		{
			return GetByOrderNum(orderNum, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 OrderNum（字段） 查询
		/// </summary>
		/// /// <param name = "orderNum">排序，从小到大</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByOrderNum(int orderNum, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`OrderNum` = @OrderNum", top_, sort_);
			var parameter_ = Database.CreateInParameter("@OrderNum", orderNum, MySqlDbType.Int32);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByOrderNum
		#region GetByIcon
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIcon(string icon)
		{
			return GetByIcon(icon, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIcon(string icon, TransactionManager tm_)
		{
			return GetByIcon(icon, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIcon(string icon, int top_)
		{
			return GetByIcon(icon, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIcon(string icon, int top_, TransactionManager tm_)
		{
			return GetByIcon(icon, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIcon(string icon, string sort_)
		{
			return GetByIcon(icon, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIcon(string icon, string sort_, TransactionManager tm_)
		{
			return GetByIcon(icon, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Icon（字段） 查询
		/// </summary>
		/// /// <param name = "icon">图标</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIcon(string icon, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Icon` = @Icon", top_, sort_);
			var parameter_ = Database.CreateInParameter("@Icon", icon != null ? icon : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByIcon
		#region GetByLinkMode
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByLinkMode(int linkMode)
		{
			return GetByLinkMode(linkMode, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByLinkMode(int linkMode, TransactionManager tm_)
		{
			return GetByLinkMode(linkMode, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByLinkMode(int linkMode, int top_)
		{
			return GetByLinkMode(linkMode, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByLinkMode(int linkMode, int top_, TransactionManager tm_)
		{
			return GetByLinkMode(linkMode, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByLinkMode(int linkMode, string sort_)
		{
			return GetByLinkMode(linkMode, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByLinkMode(int linkMode, string sort_, TransactionManager tm_)
		{
			return GetByLinkMode(linkMode, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 LinkMode（字段） 查询
		/// </summary>
		/// /// <param name = "linkMode">链接类型0-目录1-rul 2-siteid+url 3-GenID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByLinkMode(int linkMode, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`LinkMode` = @LinkMode", top_, sort_);
			var parameter_ = Database.CreateInParameter("@LinkMode", linkMode, MySqlDbType.Int32);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByLinkMode
		#region GetByUrl
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrl(string url)
		{
			return GetByUrl(url, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrl(string url, TransactionManager tm_)
		{
			return GetByUrl(url, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrl(string url, int top_)
		{
			return GetByUrl(url, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrl(string url, int top_, TransactionManager tm_)
		{
			return GetByUrl(url, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrl(string url, string sort_)
		{
			return GetByUrl(url, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrl(string url, string sort_, TransactionManager tm_)
		{
			return GetByUrl(url, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Url（字段） 查询
		/// </summary>
		/// /// <param name = "url">菜单URL，尽量使用相对路径</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrl(string url, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Url` = @Url", top_, sort_);
			var parameter_ = Database.CreateInParameter("@Url", url != null ? url : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByUrl
		#region GetByGenID
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByGenID(string genID)
		{
			return GetByGenID(genID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByGenID(string genID, TransactionManager tm_)
		{
			return GetByGenID(genID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByGenID(string genID, int top_)
		{
			return GetByGenID(genID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByGenID(string genID, int top_, TransactionManager tm_)
		{
			return GetByGenID(genID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByGenID(string genID, string sort_)
		{
			return GetByGenID(genID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByGenID(string genID, string sort_, TransactionManager tm_)
		{
			return GetByGenID(genID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 GenID（字段） 查询
		/// </summary>
		/// /// <param name = "genID">GenID编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByGenID(string genID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`GenID` = @GenID", top_, sort_);
			var parameter_ = Database.CreateInParameter("@GenID", genID != null ? genID : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByGenID
		#region GetByUrlTarget
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrlTarget(int urlTarget)
		{
			return GetByUrlTarget(urlTarget, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrlTarget(int urlTarget, TransactionManager tm_)
		{
			return GetByUrlTarget(urlTarget, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrlTarget(int urlTarget, int top_)
		{
			return GetByUrlTarget(urlTarget, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrlTarget(int urlTarget, int top_, TransactionManager tm_)
		{
			return GetByUrlTarget(urlTarget, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrlTarget(int urlTarget, string sort_)
		{
			return GetByUrlTarget(urlTarget, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrlTarget(int urlTarget, string sort_, TransactionManager tm_)
		{
			return GetByUrlTarget(urlTarget, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 UrlTarget（字段） 查询
		/// </summary>
		/// /// <param name = "urlTarget">链接模式 0-TAB打开 1-新窗口打开</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUrlTarget(int urlTarget, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`UrlTarget` = @UrlTarget", top_, sort_);
			var parameter_ = Database.CreateInParameter("@UrlTarget", urlTarget, MySqlDbType.Byte);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByUrlTarget
		#region GetByPrivParams
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParams(string privParams)
		{
			return GetByPrivParams(privParams, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParams(string privParams, TransactionManager tm_)
		{
			return GetByPrivParams(privParams, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParams(string privParams, int top_)
		{
			return GetByPrivParams(privParams, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParams(string privParams, int top_, TransactionManager tm_)
		{
			return GetByPrivParams(privParams, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParams(string privParams, string sort_)
		{
			return GetByPrivParams(privParams, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParams(string privParams, string sort_, TransactionManager tm_)
		{
			return GetByPrivParams(privParams, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 PrivParams（字段） 查询
		/// </summary>
		/// /// <param name = "privParams">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParams(string privParams, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`PrivParams` = @PrivParams", top_, sort_);
			var parameter_ = Database.CreateInParameter("@PrivParams", privParams != null ? privParams : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByPrivParams
		#region GetByPinyin
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPinyin(string pinyin)
		{
			return GetByPinyin(pinyin, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPinyin(string pinyin, TransactionManager tm_)
		{
			return GetByPinyin(pinyin, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPinyin(string pinyin, int top_)
		{
			return GetByPinyin(pinyin, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPinyin(string pinyin, int top_, TransactionManager tm_)
		{
			return GetByPinyin(pinyin, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPinyin(string pinyin, string sort_)
		{
			return GetByPinyin(pinyin, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPinyin(string pinyin, string sort_, TransactionManager tm_)
		{
			return GetByPinyin(pinyin, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Pinyin（字段） 查询
		/// </summary>
		/// /// <param name = "pinyin">拼音</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPinyin(string pinyin, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Pinyin` = @Pinyin", top_, sort_);
			var parameter_ = Database.CreateInParameter("@Pinyin", pinyin != null ? pinyin : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByPinyin
		#region GetByDesc
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByDesc(string desc)
		{
			return GetByDesc(desc, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByDesc(string desc, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByDesc(string desc, int top_)
		{
			return GetByDesc(desc, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByDesc(string desc, int top_, TransactionManager tm_)
		{
			return GetByDesc(desc, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByDesc(string desc, string sort_)
		{
			return GetByDesc(desc, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByDesc(string desc, string sort_, TransactionManager tm_)
		{
			return GetByDesc(desc, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Desc（字段） 查询
		/// </summary>
		/// /// <param name = "desc">描述</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByDesc(string desc, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Desc` = @Desc", top_, sort_);
			var parameter_ = Database.CreateInParameter("@Desc", desc != null ? desc : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByDesc
		#region GetByStatus
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByStatus(int status)
		{
			return GetByStatus(status, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByStatus(int status, TransactionManager tm_)
		{
			return GetByStatus(status, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByStatus(int status, int top_)
		{
			return GetByStatus(status, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByStatus(int status, int top_, TransactionManager tm_)
		{
			return GetByStatus(status, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByStatus(int status, string sort_)
		{
			return GetByStatus(status, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByStatus(int status, string sort_, TransactionManager tm_)
		{
			return GetByStatus(status, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Status（字段） 查询
		/// </summary>
		/// /// <param name = "status">状态 0-无效 1-有效</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByStatus(int status, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Status` = @Status", top_, sort_);
			var parameter_ = Database.CreateInParameter("@Status", status, MySqlDbType.Byte);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByStatus
		#region GetByRoleID
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID"></param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByRoleID(int roleID)
		{
			return GetByRoleID(roleID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID"></param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByRoleID(int roleID, TransactionManager tm_)
		{
			return GetByRoleID(roleID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID"></param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByRoleID(int roleID, int top_)
		{
			return GetByRoleID(roleID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID"></param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByRoleID(int roleID, int top_, TransactionManager tm_)
		{
			return GetByRoleID(roleID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID"></param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByRoleID(int roleID, string sort_)
		{
			return GetByRoleID(roleID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID"></param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByRoleID(int roleID, string sort_, TransactionManager tm_)
		{
			return GetByRoleID(roleID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 RoleID（字段） 查询
		/// </summary>
		/// /// <param name = "roleID"></param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByRoleID(int roleID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`RoleID` = @RoleID", top_, sort_);
			var parameter_ = Database.CreateInParameter("@RoleID", roleID, MySqlDbType.Int32);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByRoleID
		#region GetByPrivParamsValue
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParamsValue(string privParamsValue)
		{
			return GetByPrivParamsValue(privParamsValue, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParamsValue(string privParamsValue, TransactionManager tm_)
		{
			return GetByPrivParamsValue(privParamsValue, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParamsValue(string privParamsValue, int top_)
		{
			return GetByPrivParamsValue(privParamsValue, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParamsValue(string privParamsValue, int top_, TransactionManager tm_)
		{
			return GetByPrivParamsValue(privParamsValue, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParamsValue(string privParamsValue, string sort_)
		{
			return GetByPrivParamsValue(privParamsValue, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParamsValue(string privParamsValue, string sort_, TransactionManager tm_)
		{
			return GetByPrivParamsValue(privParamsValue, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 PrivParamsValue（字段） 查询
		/// </summary>
		/// /// <param name = "privParamsValue">功能和数据权限参数。格式：类型-参数| 类型-参数</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByPrivParamsValue(string privParamsValue, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`PrivParamsValue` = @PrivParamsValue", top_, sort_);
			var parameter_ = Database.CreateInParameter("@PrivParamsValue", privParamsValue != null ? privParamsValue : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByPrivParamsValue
		#region GetByIsEnabled
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIsEnabled(bool isEnabled)
		{
			return GetByIsEnabled(isEnabled, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIsEnabled(bool isEnabled, TransactionManager tm_)
		{
			return GetByIsEnabled(isEnabled, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIsEnabled(bool isEnabled, int top_)
		{
			return GetByIsEnabled(isEnabled, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIsEnabled(bool isEnabled, int top_, TransactionManager tm_)
		{
			return GetByIsEnabled(isEnabled, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIsEnabled(bool isEnabled, string sort_)
		{
			return GetByIsEnabled(isEnabled, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIsEnabled(bool isEnabled, string sort_, TransactionManager tm_)
		{
			return GetByIsEnabled(isEnabled, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 IsEnabled（字段） 查询
		/// </summary>
		/// /// <param name = "isEnabled">是否允许</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByIsEnabled(bool isEnabled, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`IsEnabled` = @IsEnabled", top_, sort_);
			var parameter_ = Database.CreateInParameter("@IsEnabled", isEnabled, MySqlDbType.Byte);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByIsEnabled
		#region GetByUserID
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUserID(string userID)
		{
			return GetByUserID(userID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUserID(string userID, TransactionManager tm_)
		{
			return GetByUserID(userID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUserID(string userID, int top_)
		{
			return GetByUserID(userID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUserID(string userID, int top_, TransactionManager tm_)
		{
			return GetByUserID(userID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUserID(string userID, string sort_)
		{
			return GetByUserID(userID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUserID(string userID, string sort_, TransactionManager tm_)
		{
			return GetByUserID(userID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID">管理用户ID</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_admin_user_owner_privEO> GetByUserID(string userID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`UserID` = @UserID", top_, sort_);
			var parameter_ = Database.CreateInParameter("@UserID", userID, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_admin_user_owner_privEO.MapDataReader);
		}
		#endregion // GetByUserID
		#endregion // Get
	}
	#endregion // MO
}
