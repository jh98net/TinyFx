using Demo.ConsoleEXE;
using Demo.ConsoleEXE.DAL;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Common;
using TinyFx.Data;
using TinyFx.DbCaching;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.IP2Country;
using TinyFx.Randoms;
using TinyFx.ShortId;
using TinyFx.Text;

namespace TinyFx.Demos
{
    internal class TestDemo : DemoBase
    {
        public override async Task Execute()
        {
            //var helper = new DemoHelper();
            //await helper.InitData("demo");
            var item = new Ss_split_tableEO
            {
                DatabaseId = "default",
                TableName = "s_split_demo",
                ColumnName = "ObjectID",
                ColumnType = 2, // 1-datetime 2-objectId
                HandleMode = (int)HandleMode.Delete,
                MoveKeepMode = 0, // 0-天1-月
                MoveKeepValue = 3,
                MoveTableMode = 0,
                MoveTableValue = null,
                MoveWhere = null,
                SplitMaxRowCount = 0,
            };
            await new DataSplitJob().Execute(item);
            Console.WriteLine("OK");
        }
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
