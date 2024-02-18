﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace TinyFx.BIZ.DataSplit.DAL
{
    ///<summary>
    ///分表
    ///</summary>
    [SugarTable("s_split_table")]
    public partial class Ss_split_tableEO
    {
           public Ss_split_tableEO(){

            this.HandleMode =0;
            this.ColumnType =0;
            this.MoveKeepMode =0;
            this.MoveKeepValue =0;
            this.MoveTableMode =0;
            this.PartitionMethod =0;
            this.SplitMaxRowCount =0;
            this.SplitMaxRowHours =1;
            this.HandleOrder =0;
            this.DbTimeout =0;
            this.BathPageSize =0;
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
           /// Desc:处理模式
			///              0-无
			///              1-迁移-删除 ==> MoveMode + MoveKeepValue
			///              2-迁移-备份 ==> MoveMode + MoveKeepValue
			///              3-分区 => PatitionMethod + PatitionExpr + PatitionValue
			///              3-分表-按最大行数 ==> SplitMaxRowCount + SplitMaxRowHours
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int HandleMode {get;set;}

           /// <summary>
           /// Desc:分表字段名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? ColumnName {get;set;}

           /// <summary>
           /// Desc:分表字段类型(0-未知1-DateTime(UTC)2-ObjectId3-数值天4-周5-月6-季7-年)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int ColumnType {get;set;}

           /// <summary>
           /// Desc:迁移模式(0-无1-天2-周3-月4-季5-年)
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
           /// Desc:迁移表名称模式(0-无1-天2-周3-月4-季5-年)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int MoveTableMode {get;set;}

           /// <summary>
           /// Desc:迁移数据的条件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? MoveWhere {get;set;}

           /// <summary>
           /// Desc:分区方式0-未知1-range2-list3-hash4-key5-range columns6-list columns7-custom
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int PartitionMethod {get;set;}

           /// <summary>
           /// Desc:分区表达式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? PartitionExpr {get;set;}

           /// <summary>
           /// Desc:分区值(范围值|分割)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? PartitionValue {get;set;}

           /// <summary>
           /// Desc:分表最大记录数
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int SplitMaxRowCount {get;set;}

           /// <summary>
           /// Desc:分表最大记录数时下一个表的间隔
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public int SplitMaxRowHours {get;set;}

           /// <summary>
           /// Desc:处理顺序(小到大)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int HandleOrder {get;set;}

           /// <summary>
           /// Desc:数据库超时（秒）
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
