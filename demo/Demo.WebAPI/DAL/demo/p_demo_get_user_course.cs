/******************************************************
 * 此代码由代码生成器工具自动生成，请不要修改
 * TinyFx代码生成器核心库版本号：1.0.0.0
 * git: https://github.com/jh98net/TinyFx
 * 文档生成时间：2022-05-15 11: 17:56
 ******************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TinyFx;
using TinyFx.Data;
using TinyFx.Data.MySql;

namespace Demo.WebAPI
{
	#region MO
	/// <summary>
	/// 存储过程描述
	/// 【存储过程 p_demo_get_user_course 的操作类】
	/// </summary>
	public class P_demo_get_user_courseMO : MySqlProcMO
	{
	    public override string ProcName => "`p_demo_get_user_course`";
		#region Constructors
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "database">数据来源</param>
		public P_demo_get_user_courseMO(MySqlDatabase database) : base(database) { }
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name = "connectionStringName">配置文件.config中定义的连接字符串名称</param>
		public P_demo_get_user_courseMO(string connectionStringName = null) : base(connectionStringName) { }
	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="connectionString">数据库连接字符串，如server=192.168.1.1;database=testdb;uid=root;pwd=root</param>
	    /// <param name="commandTimeout">CommandTimeout时间</param>
	    public P_demo_get_user_courseMO(string connectionString, int commandTimeout) : base(connectionString, commandTimeout) { }
		#endregion // Constructors
		// 获取ProcDao
		private MySqlProcDao GetProcDao(long? pUserID, TransactionManager tm = null)
		{
			var ret = new MySqlProcDao(ProcName, Database);
			ret.AddInParameter("@pUserID", pUserID.HasValue ? pUserID.Value : (object)DBNull.Value, MySqlDbType.Int64);
			ret.AddOutParameter("@pPageCount", MySqlDbType.Int32);
	        return ret;
		}
		/// <summary>
		/// 获取单行数据
		/// </summary>
		/// /// <param name = "pUserID">输入参数 pUserID bigint</param>
		/// <param name="tm">事务对象</param>
		/// <returns></returns>
		public DataRow ExecSingle(long? pUserID, TransactionManager tm = null)
		{
			using (var dao = GetProcDao(pUserID))
			{
				return dao.ExecSingle(tm);
			}
		}
		/// <summary>
		/// 执行存储过程返回DataReader对象
		/// </summary>
		/// /// <param name = "pUserID">输入参数 pUserID bigint</param>
		/// <param name="tm">事务对象</param>
		/// <returns></returns>
		public IDataReader ExecReader(long? pUserID, TransactionManager tm = null)
		{
			var dao = GetProcDao(pUserID);
			return dao.ExecReader(tm);
		}
		/// <summary>
		/// 获取DataTable数据
		/// </summary>
		/// /// <param name = "pUserID">输入参数 pUserID bigint</param>
		/// <param name="tm">事务对象</param>
		/// <returns></returns>
		public DataTable ExecTable(long? pUserID, TransactionManager tm = null)
		{
			using (var dao = GetProcDao(pUserID))
			{
				return dao.ExecTable(tm);
			}
		}
		/// <summary>
		/// 获取首行首列值
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// /// <param name = "pUserID">输入参数 pUserID bigint</param>
		/// <param name="tm">事务对象</param>
		/// <returns></returns>
		public T ExecScalar<T>(long? pUserID, TransactionManager tm = null)
		{
		    return ExecSingle(pUserID, tm)[0].ConvertTo<T>();
		}
		/// <summary>
		/// 获取执行存储过程返回的所有数据
		/// </summary>
		/// /// <param name = "pUserID">输入参数 pUserID bigint</param>
		/// <param name="tm">事务对象</param>
		/// <returns></returns>
		public ResultData Execute(long? pUserID, TransactionManager tm = null)
		{
		    ResultData ret = null;
		    using (var dao = GetProcDao(pUserID))
		    {
		        var reader = dao.ExecReader(tm);
		        if (reader.FieldCount == 0) return null;
		        ret = new ResultData();
		        do
		        {
		            var table = reader.ToTable(false);
		            ret.Results.Add(table);
		        }
		        while (reader.NextResult());
		        ret.PPageCount = dao.GetParameterValue<int>("@pPageCount");
		    }
		    return ret;
		}
		/// <summary>
		/// 存储过程返回的数据对象
		/// </summary>
		public class ResultData
		{
			#region OutputParameters
		
			/// <summary>
			/// 输出参数
			/// 【pPageCount int】
			/// </summary>
			public int PPageCount { get; set; }
		
			#endregion // OutputParameters
		    /// <summary>
		    /// 存储过程返回的结果集集合
		    /// </summary>
		    public List<DataTable> Results = new List<DataTable>();
			
			/// <summary>
			/// 是否有结果集
			/// </summary>
		    public bool HasResult => Results.Count > 0;
		    /// <summary>
		    /// 获取存储过程返回的第一个DataTable结果集
		    /// </summary>
		    public DataTable FirstResult => HasResult ? Results[0] : null;
		}
	}
	#endregion // MO
}
