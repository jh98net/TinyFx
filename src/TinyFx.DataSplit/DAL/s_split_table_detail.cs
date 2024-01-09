using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace TinyFx.DataSplit.DAL
{
    ///<summary>
    ///分表明细
    ///</summary>
    [SugarTable("s_split_table_detail")]
    public partial class Ss_split_table_detailEO
    {
           public Ss_split_table_detailEO(){

            this.ColumnType =0;
            this.HandleMode =0;
            this.Status =0;
            this.RecDate =DateTime.Now;

           }
           /// <summary>
           /// Desc:明细ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string DetailID {get;set;}

           /// <summary>
           /// Desc:数据库标识
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? DatabaseId {get;set;}

           /// <summary>
           /// Desc:分表表名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? TableName {get;set;}

           /// <summary>
           /// Desc:分表字段名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? ColumnName {get;set;}

           /// <summary>
           /// Desc:分表字段类型0-未知1-DateTime(UTC)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int ColumnType {get;set;}

           /// <summary>
           /// Desc:处理模式（取不同字段的值）
			///              0-未知
			///              1-删除
			///              2-迁移
			///              3-分表-按表最大行数 
			///                  ==> SplitMaxRowCount=最大行数 表名=xxx_yyyyMMddHHmmss_3_
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int HandleMode {get;set;}

           /// <summary>
           /// Desc:分表后的表名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? SplitTableName {get;set;}

           /// <summary>
           /// Desc:状态(0-无效1-有效)
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
