using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace TinyFx.DataSplit.DAL
{
    ///<summary>
    ///分库
    ///</summary>
    [SugarTable("s_split_db")]
    public partial class Ss_split_dbEO
    {
           public Ss_split_dbEO(){

            this.Status =0;
            this.RecDate =DateTime.Now;

           }
           /// <summary>
           /// Desc:分库表名
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string TableName {get;set;}

           /// <summary>
           /// Desc:分库字段名
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string ColumnName {get;set;}

           /// <summary>
           /// Desc:分库字段的值
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string ColumnValue {get;set;}

           /// <summary>
           /// Desc:数据库标识
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string DatabaseId {get;set;}

           /// <summary>
           /// Desc:状态(0-无效1-有效2-已改变未同步)
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
