using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.DbCaching;

namespace TinyFx.BIZ.DataSplit.Caching
{
    internal class DbCacheUtil
    {
        public static Stfx_split_tableEO GetSplitTable(string databaseId, string tableName)
        {
            return DbCachingUtil.GetSingle(() => new Stfx_split_tableEO
            {
                DatabaseId = databaseId,
                TableName = tableName
            });
        }
        public static List<Stfx_split_table_detailEO> GetSplitDetails(string databaseId, string tableName)
        {
            return DbCachingUtil.GetList(() => new Stfx_split_table_detailEO
            {
                DatabaseId = databaseId,
                TableName = tableName
            }).Where(it => it.Status == 1 || it.Status == 2).ToList();
        }
    }
}
