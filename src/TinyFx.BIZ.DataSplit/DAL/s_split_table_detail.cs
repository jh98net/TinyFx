using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace TinyFx.BIZ.DataSplit.DAL
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
            this.RowCount =0;
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
           /// Desc:日志编码(GUID)
           /// Default:
           /// Nullable:False
           /// </summary>           
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
			///                                          目标表名: MoveTableMode + MoveTableValue
			///              3-分表-按最大行数 ==> SplitMaxRowCount=最大行数
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int HandleMode {get;set;}

           /// <summary>
           /// Desc:分表后的表名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SplitTableName {get;set;}

           /// <summary>
           /// Desc:分表字段最小值(包含)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? ColumnMin {get;set;}

           /// <summary>
           /// Desc:分表字段最大值(不包含)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? ColumnMax {get;set;}

           /// <summary>
           /// Desc:记录数
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int RowCount {get;set;}

           /// <summary>
           /// Desc:备份待删除执行数据
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? BackupDeleteData {get;set;}

           /// <summary>
           /// Desc:状态(0-无效1-有效2-备份待删除)
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
