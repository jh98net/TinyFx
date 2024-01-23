using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace TinyFx.BIZ.DataSplit.DAL
{
    ///<summary>
    ///分库日志
    ///</summary>
    [SugarTable("s_split_db_log")]
    public partial class Ss_split_db_logEO
    {
           public Ss_split_db_logEO(){

            this.HandlerTime =0;
            this.Status =0;
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
           /// Desc:分库表名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TableName {get;set;}

           /// <summary>
           /// Desc:分库字段名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ColumnName {get;set;}

           /// <summary>
           /// Desc:分库字段的值
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ColumnValue {get;set;}

           /// <summary>
           /// Desc:数据库标识
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? DatabaseId {get;set;}

           /// <summary>
           /// Desc:执行时长（秒）
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int HandlerTime {get;set;}

           /// <summary>
           /// Desc:执行日志
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? HandlerLog {get;set;}

           /// <summary>
           /// Desc:状态(0-未同步1-已同步)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int Status {get;set;}

           /// <summary>
           /// Desc:记录时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime RecDate {get;set;}

    }
}
