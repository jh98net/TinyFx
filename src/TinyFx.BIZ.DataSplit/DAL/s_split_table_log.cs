using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace TinyFx.BIZ.DataSplit.DAL
{
    ///<summary>
    ///分表执行日志
    ///</summary>
    [SugarTable("s_split_table_log")]
    public partial class Ss_split_table_logEO
    {
           public Ss_split_table_logEO(){

            this.ColumnType =0;
            this.HandleMode =0;
            this.MoveKeepMode =0;
            this.MoveKeepValue =0;
            this.MoveTableMode =0;
            this.SplitMaxRowCount =0;
            this.HandleOrder =0;
            this.DbTimeout =0;
            this.BathPageSize =0;
            this.Status =0;
            this.RowNum =0;
            this.HandleSeconds =0;
            this.RecDate =DateTime.Now;

           }
           /// <summary>
           /// Desc:日志编码(GUID)
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string LogID {get;set;}

           /// <summary>
           /// Desc:数据库标识
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string DatabaseId {get;set;}

           /// <summary>
           /// Desc:分表表名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TableName {get;set;}

           /// <summary>
           /// Desc:分表字段名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ColumnName {get;set;}

           /// <summary>
           /// Desc:分表字段类型(0-DateTime(UTC)1-ObjectId)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int ColumnType {get;set;}

           /// <summary>
           /// Desc:处理模式
			///              0-未知
			///              1-迁移-删除 ==> 保留天数: MoveKeepMode + MoveKeepValue
			///              2-迁移-备份 ==> 保留天数: MoveKeepMode + MoveKeepValue
			///                                  目标表名: MoveTableMode + MoveTableValue
			///              3-分表-按最大行数 ==> SplitMaxRowCount=最大行数 表名=xxx_yyyyMMddHHmmss_3_
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int HandleMode {get;set;}

           /// <summary>
           /// Desc:迁移保留模式(0-天1-月)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int MoveKeepMode {get;set;}

           /// <summary>
           /// Desc:迁移保留模式的值
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int MoveKeepValue {get;set;}

           /// <summary>
           /// Desc:迁移目标表名模式(0-天1-月2-指定表名)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int MoveTableMode {get;set;}

           /// <summary>
           /// Desc:迁移目标表名模式的值
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? MoveTableValue {get;set;}

           /// <summary>
           /// Desc:迁移数据的条件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? MoveWhere {get;set;}

           /// <summary>
           /// Desc:分表最大记录数
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int SplitMaxRowCount {get;set;}

           /// <summary>
           /// Desc:处理顺序(小到大)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int HandleOrder {get;set;}

           /// <summary>
           /// Desc:数据库执超时（秒）
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int DbTimeout {get;set;}

           /// <summary>
           /// Desc:批处理分页行数
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int BathPageSize {get;set;}

           /// <summary>
           /// Desc:执行基础UTC时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime ExecTime {get;set;}

           /// <summary>
           /// Desc:状态(0-进行中1-成功2-失败)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int Status {get;set;}

           /// <summary>
           /// Desc:起始时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? BeginDate {get;set;}

           /// <summary>
           /// Desc:结束时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? EndDate {get;set;}

           /// <summary>
           /// Desc:处理记录数
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int RowNum {get;set;}

           /// <summary>
           /// Desc:执行时长（秒）
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int HandleSeconds {get;set;}

           /// <summary>
           /// Desc:执行日志
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? HandleLog {get;set;}

           /// <summary>
           /// Desc:异常消息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? Exception {get;set;}

           /// <summary>
           /// Desc:分表后的表名,|分割
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? HandleTables {get;set;}

           /// <summary>
           /// Desc:记录时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime RecDate {get;set;}

    }
}
