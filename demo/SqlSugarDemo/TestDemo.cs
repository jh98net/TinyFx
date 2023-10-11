using SqlSugarDemo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Data.SqlSugarEx;

namespace SqlSugarDemo
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            var mo = DbUtil.CreateRepository<Sdemo_userEO>();
            var item = mo.GetSingle(x => x.UserID==2);
            //var mo = DbUtil.CreateRepository<Sdemo_classEO>();
            //var item = mo.Update(it => new()
            //{
            //    Name = ""
            //}, it => it.Name != null);
            //mo.Update(it => new() 
            //{

            //}, it=>it.Name != null);

            Console.WriteLine(item.ClassID);
        }
    }

}
