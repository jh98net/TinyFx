using Microsoft.AspNetCore.Http;
using TinyFx;
using TinyFx.BIZ.DataSplit;
using TinyFx.BIZ.DataSplit.Common;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Security;
using TinyFx.Text;


TinyFxHost.Start();
var st = SecurityUtil.MD5Hash(string.Empty);
Console.WriteLine(st);
/*
await new DemoHelper().InitData();

//
var list = new List<Ss_split_tableEO>
{
    new Ss_split_tableEO()
    {
        DatabaseId = "default",
        TableName = "s_split_demo",
        HandleMode = (int)HandleMode.SplitMaxRows,
        ColumnName = "ObjectID",
        ColumnType = (int)ColumnType.ObjectId,
        MoveMode = (int)MoveMode.None,
        MoveKeepValue = 0,
        MoveWhere = null,
        SplitMaxRowCount = 2,
        SplitMaxRowHours = 2
    },
};
await new DataSplitJob().Execute(list);
*/