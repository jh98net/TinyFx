using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Data.SqlSugarEx
{
    public class Repository<T> : SimpleClient<T>
        where T : class, new()
    {
        public Repository(params object[] routingData)
        {
            var routingProvider = DIUtil.GetRequiredService<IDbRoutingProvider>();
            var configId = routingProvider.RouteDb<T>(routingData);
            var db = SqlSugarUtil.GetDb(configId).CopyNew();
            //
            db.CurrentConnectionConfig.ConfigureExternalServices.SplitTableService
                = routingProvider.RouteTable<T>();
            base.Context = db;
        }
    }
}
