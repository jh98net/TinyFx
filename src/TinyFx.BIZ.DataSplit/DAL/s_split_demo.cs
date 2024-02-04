﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace TinyFx.BIZ.DataSplit.DAL
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("s_split_demo")]
    public partial class Ss_split_demoEO
    {
           public Ss_split_demoEO(){

            this.DateNum =0;
            this.OrderNum =0;
            this.RecDate =DateTime.Now;

           }
           /// <summary>
           /// Desc:ObjectID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string ObjectID {get;set;}

           /// <summary>
           /// Desc:日期值
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int DateNum {get;set;}

           /// <summary>
           /// Desc:天
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime DayId {get;set;}

           /// <summary>
           /// Desc:顺序号
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int OrderNum {get;set;}

           /// <summary>
           /// Desc:记录时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime RecDate {get;set;}

    }
}
