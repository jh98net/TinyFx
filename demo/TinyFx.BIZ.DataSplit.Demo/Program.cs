using TinyFx;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Text;

TinyFxHost.Start();

//await new DemoHelper().InitData();

//
var list = new List<Ss_split_tableEO>
{
    new Ss_split_tableEO()
    {
        DatabaseId = "default",
        TableName = "s_split_demo",
        HandleMode = (int)HandleMode.Backup,
        ColumnName = "ObjectID",
        ColumnType = (int)ColumnType.ObjectId,
        MoveMode = (int)MoveMode.Month,
        MoveKeepValue = 1,
        MoveWhere = null,
        SplitMaxRowCount = 10,
        SplitMaxRowHours = 2
    },
};
await new DataSplitJob().Execute(list);
