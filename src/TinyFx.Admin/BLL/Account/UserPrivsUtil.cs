using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Admin.DAL;
using TinyFx.Caching;
using TinyFx.Configuration;

namespace TinyFx.Admin.BLL.Account
{
    public static class UserPrivsUtil
    {
        public static List<V_admin_user_role_privEO> GetUserMenus(string userId)
        {
            var key = GetCacheKey(userId);
            if (CachingUtil.TryGet(key, out List<V_admin_user_role_privEO> ret))
                return ret;

            var privs = new Dictionary<string, V_admin_user_role_privEO>();
            new V_admin_user_role_privMO().GetByUserID(userId).ForEach(item => {
                privs.TryAdd(item.MenuID, item);
            });
            new V_admin_user_owner_privMO().GetByUserID(userId).ForEach(item => {
                if (item.IsEnabled) 
                {
                    if (privs.ContainsKey(item.MenuID))
                    {
                        privs[item.MenuID].PrivParamsValue = item.PrivParamsValue;
                    }
                    else
                    {
                        privs.Add(item.MenuID, new V_admin_user_role_privEO { 
                            MenuID = item.MenuID,
                            SiteID = item.SiteID,
                            ParentId= item.ParentId,
                            Title= item.Title,
                            OrderNum= item.OrderNum,
                            Icon= item.Icon,
                            LinkMode= item.LinkMode,
                            Url= item.Url,
                            GenID= item.GenID,
                            UrlTarget= item.UrlTarget,
                            PrivParams= item.PrivParams,
                            Pinyin= item.Pinyin,
                            Desc= item.Desc,
                            Status= item.Status,
                            RoleID=item.RoleID,
                            IsEnabled=item.IsEnabled,
                            PrivParamsValue=item.PrivParamsValue,
                            UserID=item.UserID,
                        });
                    }
                }
                else {
                    if (privs.ContainsKey(item.MenuID))
                        privs.Remove(item.MenuID);
                }
            });
            ret = privs.Values.ToList();
            CachingUtil.Set(key, ret);
            return ret;
        }
        private static string GetCacheKey(string userId)
            => $"{ConfigUtil.Project.ProjectId}:UserMenus:{userId}";
    }
}
