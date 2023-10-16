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
            try
            {
                DbUtil.BeginTran();
                var mo1 = DbUtil.CreateRepository<Sdemo_classEO>();
                var mo2 = DbUtil.CreateRepository<Sv_demo_user_courseEO>();

                DbUtil.CommitTran();
            }
            catch (Exception ex)
            {
                DbUtil.RollbackTran();
            }
        }
    }

}
