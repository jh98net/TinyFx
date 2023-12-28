using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.DbCaching
{
    public interface IDbCachePreloadProvider
    {
        List<DbCachePreloadItem> GetPreloadList();
    }
    public class DbCachePreloadItem
    {
        public Type EntityType { get; set; }
        public object[] SplitDbKeys { get; set; }
        public DbCachePreloadItem(Type entityType, object[] splitDbKeys = null)
        {
            EntityType = entityType;
            SplitDbKeys = splitDbKeys;
        }
    }
}
