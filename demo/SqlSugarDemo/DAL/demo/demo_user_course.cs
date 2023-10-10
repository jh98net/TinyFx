﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace SqlSugarDemo.DAL
{

    [SugarTable("demo_user_course")]
    public partial class Sdemo_user_courseEO
    {
           public Sdemo_user_courseEO(){


           }
           /// <summary>
           /// Desc:用户编码（自增字段）
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public long UserID {get;set;}

           /// <summary>
           /// Desc:学年
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public int Year {get;set;}

           /// <summary>
           /// Desc:课程编码（GUID）
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string CourseID {get;set;}

           /// <summary>
           /// Desc:说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? Note {get;set;}

    }
}
