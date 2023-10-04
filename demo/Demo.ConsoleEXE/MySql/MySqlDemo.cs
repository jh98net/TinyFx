using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Data.MySql;
using TinyFx.Demos.demo;

namespace TinyFx.Demos.MySql
{
    internal class MySqlDemo : DemoBase
    {
        public override async Task Execute()
        {
            var mo = new Demo_userMO();
            var eo = mo.GetByPK(1);
            Console.WriteLine(eo.ClassID);

            var db = new MySqlDatabase();
            var sql = "select classid from demo_user where userid=@0";
            var id = db.ExecSqlScalar<string>(sql,1);
            Console.WriteLine(id);
        }
    }
}
