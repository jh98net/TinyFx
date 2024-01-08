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
            this.HandlerMode =0;
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
           /// Desc:分表字段类型0-未知1-DateTime2-ObjectId
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int ColumnType {get;set;}

           /// <summary>
           /// Desc:处理模式
			///              0-未知
			///              1-删除 ==> HandlerData=保留天数
			///              2-按天迁移 ==> HandlerData=保留天数 表名=xxx_yyyyMMdd_2_
			///              3-按周迁移 ==> HandlerData=保留周数 表名=xxx_yyyyWeek_3_
			///              4-按月迁移 ==> HandlerData=保留月数 表名=xxx_yyyyMM_4_
			///              5-按季迁移 ==> HandlerData=保留季数 表名=xxx_yyyy1,2,3,4_5_
			///              6-按年迁移 ==> HandlerData=保留年数 表名=xxx_yyyy_6_
			///              9-按最大行数分表 ==> HandlerData=最大行数 表名=xxx_yyyyMMddHHmmss_9_
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int HandlerMode {get;set;}

           /// <summary>
           /// Desc:分表处理数据（根据SplitMode不同而不同）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? HandlerData {get;set;}

           /// <summary>
           /// Desc:迁移数据的条件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? MoveWhere {get;set;}

           /// <summary>
           /// Desc:执行日志
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? HandlerLog {get;set;}

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
