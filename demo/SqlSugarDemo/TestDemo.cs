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
            //2023-10-17 11:02:06
            var items = DbUtil.GetDb().Queryable<Ss_data_move_logEO>()
                .Where(it => it.DataMoveID == 5)
                .Where("DATE_FORMAT(recdate,'%Y-%m-%d')=DATE_FORMAT(UTC_DATE(),'%Y-%m-%d')")
                .ToList();
            Console.WriteLine("");
        }
    }

}
