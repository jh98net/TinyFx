using SqlSugar;
using SqlSugarDemo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Data.SqlSugar;

namespace SqlSugarDemo
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var a = DbUtil.GetDb("demo").Queryable<Sdemo_classEO>().InSingle("A001");
            var b = DbUtil.GetDb("demo").Queryable<Sdemo_classEO>().InSingle("A002");

            //DbUtil.GetDb().Updateable<object>().AS("test")
            //    .SetColumns("name", "bbb").Where("id=1").ExecuteCommand();
            //DbUtil.GetNewDb("demo").Updateable<object>().AS("demo_class")
            //    .SetColumns("name", "aaa").Where("classId='A002'").ExecuteCommand();
            DbUtil.GetDb("demo").Updateable<object>().AS("demo_class")
                .SetColumns("name", "aaa").Where("classId='A002'").ExecuteCommand();
            //DbUtil.GetDb("demo").Updateable<object>().AS("demo_class")
            //    .SetColumns("name", "bbb").Where("classId='A002'").ExecuteCommand();

            DbUtil.GetNewDb("demo").Updateable<object>().AS("demo_class")
                .SetColumns("name", "bbb").Where("classId='A002'").ExecuteCommand();

            var tm = new DbTransactionManager();
            try
            {
                tm.Begin();
                tm.GetDb().Updateable<object>().AS("test").SetColumns("name", "ddd").Where("id=1").ExecuteCommand();
                tm.GetDb("demo").Updateable<object>().AS("demo_class").SetColumns("name", "ddd").Where("classId='A002'").ExecuteCommand();
                tm.Rollback();
                //tm.Commit();
            }
            catch (Exception ex)
            {
                tm.Rollback();
            }
        }
    }

}
