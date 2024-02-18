using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.Caching;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Reflection;
using TinyFx.Text;

namespace TinyFx.BIZ.DataSplit
{
    public class TinyFxSplitTableService : ISplitTableService
    {
        public List<SplitTableInfo> GetAllTables(ISqlSugarClient db, EntityInfo entityInfo, List<DbTableInfo> tableInfos)
        {
            var ret = new List<SplitTableInfo>();
            var databaseId = GetDatabaseId(db);
            var tableName = GetTableName(entityInfo);
            var list = DbCacheUtil.GetSplitDetails(databaseId, tableName);
            if (list?.Count > 0)
            {
                foreach (var item in list)
                {
                    ret.Add(new SplitTableInfo
                    {
                        TableName = item.SplitTableName,
                        String = item.BeginValue
                    });
                }
            }
            else
            {
                ret.Add(new SplitTableInfo
                {
                    TableName = tableName,
                    String = null
                });
            }
            ret = ret.OrderByDescending(it=>it.String).ToList();
            return ret;
        }

        public string GetTableName(ISqlSugarClient db, EntityInfo EntityInfo)
        {

            throw new NotImplementedException();
        }

        public string GetTableName(ISqlSugarClient db, EntityInfo EntityInfo, SplitType type)
        {
            throw new NotImplementedException();
        }

        public string GetTableName(ISqlSugarClient db, EntityInfo entityInfo, SplitType splitType, object fieldValue)
        {
            throw new NotImplementedException();
        }
        public object GetFieldValue(ISqlSugarClient db, EntityInfo entityInfo, SplitType splitType, object entityValue)
        {
            if (entityValue == null)
                return null;

            var databaseId = GetDatabaseId(db);
            var tableName = GetTableName(entityInfo);
            var splitTable = DbCacheUtil.GetSplitTable(databaseId, tableName);
            if (!ReflectionUtil.TryGetPropertyValue(entityValue, splitTable.ColumnName, out var value))
                throw new Exception("");
            switch ((ColumnType)splitTable.ColumnType)
            {
                case ColumnType.DateTime:
                    return (value == null || Convert.ToDateTime(value) == DateTime.MinValue)
                        ? DateTime.UtcNow : value;
                case ColumnType.ObjectId:
                    var v = Convert.ToString(value);
                    return string.IsNullOrEmpty(v)
                        ? ObjectId.NewId(DateTime.UtcNow) : v;
                default:
                    throw new Exception("");
            }
        }


        private string GetDatabaseId(ISqlSugarClient db)
        {
            var ret = db.Ado.Context.CurrentConnectionConfig.ConfigId;
            return Convert.ToString(ret);
        }
        private string GetTableName(EntityInfo entityInfo)
        {
            return entityInfo.DbTableName;
            //var attr = entityInfo.Type.GetCustomAttribute<SugarTable>();
            //return attr.TableName;
        }
    }
    class SplitTableData
    {
        public string DatabaseId { get; set; }
        public string TableName { get; set; }
        public bool IsSplitTable { get; set; }
        public Ss_split_tableEO TableEo { get; set; }
        public List<Ss_split_table_detailEO> MyProperty { get; set; }
    }
}
