using TinyFx;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Text;

TinyFxHost.Start();

//await new DemoHelper().InitData(); return;
//var o = ObjectId.Parse("61cf998046740cf7058e539c");
//Console.WriteLine(o.CreationTime.ToFormatString());

//
var list = new List<Ss_split_tableEO>
{
    new Ss_split_tableEO()
    {
        DatabaseId = "default",
        TableName = "s_split_demo",
        HandleMode = (int)HandleMode.Backup,
        ColumnName = "DateNum",
        ColumnType = (int)ColumnType.NumMonth,
        MoveKeepMode = (int)MoveKeepMode.Month,
        MoveKeepValue = 2,
        MoveTableMode = (int)MoveTableMode.Month,
        MoveTableValue = null,
        MoveWhere = null,
        SplitMaxRowCount = 10,
        SplitMaxRowHours = 2
    },
};
await new DataSplitJob().Execute(list);
