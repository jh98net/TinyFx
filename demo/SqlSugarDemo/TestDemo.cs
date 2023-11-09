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
            var endDate = DateTime.Now;
            //2023-10-17 11:02:06
            var value = await DbUtil.GetDb().Ado.GetScalarAsync($"SELECT MIN(`RecDate`)"
                + $" FROM `s_provider_order` WHERE `recdate` < @EndDate", new SugarParameter("@EndDate", endDate));
            Console.WriteLine("");
        }
    }

}
