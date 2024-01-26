using TinyFx;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Text;

TinyFxHost.Start();

//await new DemoHelper().InitData();
//var o = ObjectId.Parse("61cf998046740cf7058e539c");
//Console.WriteLine(o.CreationTime.ToFormatString());

//
var list = new List<Ss_split_tableEO>
{
    new Ss_split_tableEO()
    {
        DatabaseId = "default",
        TableName = "s_split_demo",
        HandleMode = (int)HandleMode.Move,
        ColumnName = "RecDate",
        ColumnType = 0, // 0-datetime 1-objectId
        MoveKeepMode = (int)MoveKeepMode.Day,
        MoveKeepValue = 5,
        MoveTableMode = (int)MoveTableMode.Year,
        MoveTableValue = null,
        MoveWhere = null,
        SplitMaxRowCount = 0,
    },
};
await new DataSplitJob().Execute(list);
