using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace TinyFx.BIZ.DataSplit.DAL
{
    ///<summary>
    ///表分区定义
    ///</summary>
    [SugarTable("s_split_partition")]
    public partial class Ss_split_partitionEO
    {
           public Ss_split_partitionEO(){

            this.Method =0;
            this.DbTimeout =0;
            this.Status =0;
            this.RecDate =DateTime.Now;

           }
           /// <summary>
           /// Desc:数据库标识
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string DatabaseId {get;set;}

           /// <summary>
           /// Desc:分表表名
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string TableName {get;set;}

           /// <summary>
           /// Desc:分区方式0-未知1-range2-list3-columns4-hash5-key6-custom
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
           /// Desc:状态(0-无效1-待执行2-执行完毕)
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
