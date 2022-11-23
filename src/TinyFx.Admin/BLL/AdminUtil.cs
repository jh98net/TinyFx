using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Admin.BLL;
using TinyFx.Configuration;
using TinyFx.Data.MySql;

namespace TinyFx.Admin
{
    public static class AdminUtil
    {
        private static MySqlDatabase _adminDb;
        public static MySqlDatabase AdminDb
        {
            get
            {
                if (_adminDb == null)
                    _adminDb = new MySqlDatabase();
                return _adminDb;
            }
        }
        public static AdminSection Options
        {
            get
            {
                var _options = ConfigUtil.GetSection<AdminSection>();
                if (_options == null)
                    throw new Exception("配置文件不存在Admin配置节");
                return _options;
            }
        }
    }
}
