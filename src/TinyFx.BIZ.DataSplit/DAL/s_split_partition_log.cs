using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace TinyFx.BIZ.DataSplit.DAL
{
    ///<summary>
    ///表分区日志
    ///</summary>
    [SugarTable("s_split_partition_log")]
    public partial class Ss_split_partition_logEO
    {
           public Ss_split_partition_logEO(){

            this.Method =0;
            this.DbTimeout =0;
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
           /// Desc:分区方式0-未知1-range2-list3-hash4-key5-range columns6-list columns7-custom
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int Method {get;set;}

           /// <summary>
           /// Desc:分区表达式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? Expression {get;set;}

           /// <summary>
           /// Desc:分区值
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? Values {get;set;}

           /// <summary>
           /// Desc:数据库超时（秒）
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int DbTimeout {get;set;}

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
           /// Desc:记录时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime RecDate {get;set;}

    }
}
