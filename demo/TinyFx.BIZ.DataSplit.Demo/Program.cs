using Microsoft.AspNetCore.Http;
using TinyFx;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Security;
using TinyFx.Text;

TinyFxHost.Start();

//await new DemoHelper().InitData();

//
var list = new List<Stfx_split_tableEO>
{
    new Stfx_split_tableEO()
    {
        DatabaseId = "default",
        TableName = "demo_tfx_split",
        HandleMode = (int)HandleMode.SplitMaxRows,
        ColumnName = "ObjectID",
        ColumnType = (int)ColumnType.ObjectId,
        MoveKeepMode = (int)MoveKeepMode.None,
        MoveKeepValue = 0,
        MoveTableMode = (int)MoveTableMode.None,
        MoveWhere = null,
        SplitMaxRowCount = 2,
        SplitMaxRowHours = 2
    },
};
await new DataSplitJob().Execute(list);
