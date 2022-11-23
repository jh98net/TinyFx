/******************************************************
 * 此代码由代码生成器工具自动生成，请不要修改
 * TinyFx代码生成器核心库版本号：1.0.0.0
 * git: https://github.com/jh98net/TinyFx
 * 文档生成时间：2022-11-09 18: 12:12
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

namespace TinyFx.Demos.demo
{
	#region EO
	/// <summary>
	/// 
	/// 【视图 v_demo_user_course 的实体类】
	/// </summary>
	[Serializable]
	public class V_demo_user_courseEO : IRowMapper<V_demo_user_courseEO>
	{
		#region 所有字段
		/// <summary>
		/// 
		/// 【字段 bigint】
		/// </summary>
		[DataMember(Order = 1)]
		public long? UserID { get; set; }
		/// <summary>
		/// 类别编码
		/// 【字段 varchar(10)】
		/// </summary>
		[DataMember(Order = 2)]
		public string ClassID { get; set; }
		/// <summary>
		/// 课程编码（GUID）
		/// 【字段 char(36)】
		/// </summary>
		[DataMember(Order = 3)]
		public string CourseID { get; set; }
		/// <summary>
		/// 名称
		/// 【字段 varchar(10)】
		/// </summary>
		[DataMember(Order = 4)]
		public string Name { get; set; }
		/// <summary>
		/// 说明
		/// 【字段 text】
		/// </summary>
		[DataMember(Order = 5)]
		public string Note { get; set; }
		/// <summary>
		/// 
		/// 【字段 varchar(3)】
		/// </summary>
		[DataMember(Order = 6)]
		public string TestColumn { get; set; }
		#endregion // 所有列
		#region 实体映射
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public V_demo_user_courseEO MapRow(IDataReader reader)
		{
			return MapDataReader(reader);
		}
		
		/// <summary>
		/// 将IDataReader映射成实体对象
		/// </summary>
		/// <param name = "reader">只进结果集流</param>
		/// <return>实体对象</return>
		public static V_demo_user_courseEO MapDataReader(IDataReader reader)
		{
		    V_demo_user_courseEO ret = new V_demo_user_courseEO();
			ret.UserID = reader.ToInt64N("UserID");
			ret.ClassID = reader.ToString("ClassID");
			ret.CourseID = reader.ToString("CourseID");
			ret.Name = reader.ToString("Name");
			ret.Note = reader.ToString("Note");
			ret.TestColumn = reader.ToString("TestColumn");
		    return ret;
		}
		
		#endregion
	}
	#endregion // EO

	#region MO
	/// <summary>
	/// 
	/// 【视图 v_demo_user_course 的操作类】
	/// </summary>
	public class V_demo_user_courseMO : MySqlViewMO<V_demo_user_courseEO>
	{
		/// <summary>
		/// 视图名
		/// </summary>
	    public override string ViewName => "`v_demo_user_course`"; 
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public V_demo_user_courseMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public V_demo_user_courseMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public V_demo_user_courseMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
		#region Get
		#region GetByUserID
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID"></param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByUserID(long? userID)
		{
			return GetByUserID(userID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID"></param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByUserID(long? userID, TransactionManager tm_)
		{
			return GetByUserID(userID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID"></param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByUserID(long? userID, int top_)
		{
			return GetByUserID(userID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID"></param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByUserID(long? userID, int top_, TransactionManager tm_)
		{
			return GetByUserID(userID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID"></param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByUserID(long? userID, string sort_)
		{
			return GetByUserID(userID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID"></param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByUserID(long? userID, string sort_, TransactionManager tm_)
		{
			return GetByUserID(userID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 UserID（字段） 查询
		/// </summary>
		/// /// <param name = "userID"></param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByUserID(long? userID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`UserID` = @UserID", top_, sort_);
			var parameter_ = Database.CreateInParameter("@UserID", userID.HasValue ? userID.Value : (object)DBNull.Value, MySqlDbType.Int64);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_demo_user_courseEO.MapDataReader);
		}
		#endregion // GetByUserID
		#region GetByClassID
		
		/// <summary>
		/// 按 ClassID（字段） 查询
		/// </summary>
		/// /// <param name = "classID">类别编码</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByClassID(string classID)
		{
			return GetByClassID(classID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ClassID（字段） 查询
		/// </summary>
		/// /// <param name = "classID">类别编码</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByClassID(string classID, TransactionManager tm_)
		{
			return GetByClassID(classID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ClassID（字段） 查询
		/// </summary>
		/// /// <param name = "classID">类别编码</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByClassID(string classID, int top_)
		{
			return GetByClassID(classID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 ClassID（字段） 查询
		/// </summary>
		/// /// <param name = "classID">类别编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByClassID(string classID, int top_, TransactionManager tm_)
		{
			return GetByClassID(classID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 ClassID（字段） 查询
		/// </summary>
		/// /// <param name = "classID">类别编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByClassID(string classID, string sort_)
		{
			return GetByClassID(classID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 ClassID（字段） 查询
		/// </summary>
		/// /// <param name = "classID">类别编码</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByClassID(string classID, string sort_, TransactionManager tm_)
		{
			return GetByClassID(classID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 ClassID（字段） 查询
		/// </summary>
		/// /// <param name = "classID">类别编码</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByClassID(string classID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`ClassID` = @ClassID", top_, sort_);
			var parameter_ = Database.CreateInParameter("@ClassID", classID != null ? classID : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_demo_user_courseEO.MapDataReader);
		}
		#endregion // GetByClassID
		#region GetByCourseID
		
		/// <summary>
		/// 按 CourseID（字段） 查询
		/// </summary>
		/// /// <param name = "courseID">课程编码（GUID）</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByCourseID(string courseID)
		{
			return GetByCourseID(courseID, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 CourseID（字段） 查询
		/// </summary>
		/// /// <param name = "courseID">课程编码（GUID）</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByCourseID(string courseID, TransactionManager tm_)
		{
			return GetByCourseID(courseID, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 CourseID（字段） 查询
		/// </summary>
		/// /// <param name = "courseID">课程编码（GUID）</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByCourseID(string courseID, int top_)
		{
			return GetByCourseID(courseID, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 CourseID（字段） 查询
		/// </summary>
		/// /// <param name = "courseID">课程编码（GUID）</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByCourseID(string courseID, int top_, TransactionManager tm_)
		{
			return GetByCourseID(courseID, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 CourseID（字段） 查询
		/// </summary>
		/// /// <param name = "courseID">课程编码（GUID）</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByCourseID(string courseID, string sort_)
		{
			return GetByCourseID(courseID, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 CourseID（字段） 查询
		/// </summary>
		/// /// <param name = "courseID">课程编码（GUID）</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByCourseID(string courseID, string sort_, TransactionManager tm_)
		{
			return GetByCourseID(courseID, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 CourseID（字段） 查询
		/// </summary>
		/// /// <param name = "courseID">课程编码（GUID）</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByCourseID(string courseID, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`CourseID` = @CourseID", top_, sort_);
			var parameter_ = Database.CreateInParameter("@CourseID", courseID != null ? courseID : (object)DBNull.Value, MySqlDbType.String);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_demo_user_courseEO.MapDataReader);
		}
		#endregion // GetByCourseID
		#region GetByName
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">名称</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByName(string name)
		{
			return GetByName(name, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">名称</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByName(string name, TransactionManager tm_)
		{
			return GetByName(name, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">名称</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByName(string name, int top_)
		{
			return GetByName(name, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">名称</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByName(string name, int top_, TransactionManager tm_)
		{
			return GetByName(name, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">名称</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByName(string name, string sort_)
		{
			return GetByName(name, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">名称</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByName(string name, string sort_, TransactionManager tm_)
		{
			return GetByName(name, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Name（字段） 查询
		/// </summary>
		/// /// <param name = "name">名称</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByName(string name, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Name` = @Name", top_, sort_);
			var parameter_ = Database.CreateInParameter("@Name", name != null ? name : (object)DBNull.Value, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_demo_user_courseEO.MapDataReader);
		}
		#endregion // GetByName
		#region GetByNote
		
		/// <summary>
		/// 按 Note（字段） 查询
		/// </summary>
		/// /// <param name = "note">说明</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByNote(string note)
		{
			return GetByNote(note, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Note（字段） 查询
		/// </summary>
		/// /// <param name = "note">说明</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByNote(string note, TransactionManager tm_)
		{
			return GetByNote(note, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Note（字段） 查询
		/// </summary>
		/// /// <param name = "note">说明</param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByNote(string note, int top_)
		{
			return GetByNote(note, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 Note（字段） 查询
		/// </summary>
		/// /// <param name = "note">说明</param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByNote(string note, int top_, TransactionManager tm_)
		{
			return GetByNote(note, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 Note（字段） 查询
		/// </summary>
		/// /// <param name = "note">说明</param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByNote(string note, string sort_)
		{
			return GetByNote(note, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 Note（字段） 查询
		/// </summary>
		/// /// <param name = "note">说明</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByNote(string note, string sort_, TransactionManager tm_)
		{
			return GetByNote(note, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 Note（字段） 查询
		/// </summary>
		/// /// <param name = "note">说明</param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByNote(string note, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`Note` = @Note", top_, sort_);
			var parameter_ = Database.CreateInParameter("@Note", note != null ? note : (object)DBNull.Value, MySqlDbType.Text);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_demo_user_courseEO.MapDataReader);
		}
		#endregion // GetByNote
		#region GetByTestColumn
		
		/// <summary>
		/// 按 TestColumn（字段） 查询
		/// </summary>
		/// /// <param name = "testColumn"></param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByTestColumn(string testColumn)
		{
			return GetByTestColumn(testColumn, 0, string.Empty, null);
		}
		
		/// <summary>
		/// 按 TestColumn（字段） 查询
		/// </summary>
		/// /// <param name = "testColumn"></param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByTestColumn(string testColumn, TransactionManager tm_)
		{
			return GetByTestColumn(testColumn, 0, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 TestColumn（字段） 查询
		/// </summary>
		/// /// <param name = "testColumn"></param>
		/// <param name = "top_">获取行数</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByTestColumn(string testColumn, int top_)
		{
			return GetByTestColumn(testColumn, top_, string.Empty, null);
		}
		
		/// <summary>
		/// 按 TestColumn（字段） 查询
		/// </summary>
		/// /// <param name = "testColumn"></param>
		/// <param name = "top_">获取行数</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByTestColumn(string testColumn, int top_, TransactionManager tm_)
		{
			return GetByTestColumn(testColumn, top_, string.Empty, tm_);
		}
		
		/// <summary>
		/// 按 TestColumn（字段） 查询
		/// </summary>
		/// /// <param name = "testColumn"></param>
		/// <param name = "sort_">排序表达式</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByTestColumn(string testColumn, string sort_)
		{
			return GetByTestColumn(testColumn, 0, sort_, null);
		}
		
		/// <summary>
		/// 按 TestColumn（字段） 查询
		/// </summary>
		/// /// <param name = "testColumn"></param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByTestColumn(string testColumn, string sort_, TransactionManager tm_)
		{
			return GetByTestColumn(testColumn, 0, sort_, tm_);
		}
		
		/// <summary>
		/// 按 TestColumn（字段） 查询
		/// </summary>
		/// /// <param name = "testColumn"></param>
		/// <param name = "top_">获取行数</param>
		/// <param name = "sort_">排序表达式</param>
		/// <param name="tm_">事务管理对象</param>
		/// <return>实体对象集合</return>
		public List<V_demo_user_courseEO> GetByTestColumn(string testColumn, int top_, string sort_, TransactionManager tm_)
		{
			var sql_ = BuildSelectSQL("`TestColumn` = @TestColumn", top_, sort_);
			var parameter_ = Database.CreateInParameter("@TestColumn", testColumn, MySqlDbType.VarChar);
			return Database.ExecSqlList(sql_, new MySqlParameter[] { parameter_ }, tm_, V_demo_user_courseEO.MapDataReader);
		}
		#endregion // GetByTestColumn
		#endregion // Get
	}
	#endregion // MO
}
