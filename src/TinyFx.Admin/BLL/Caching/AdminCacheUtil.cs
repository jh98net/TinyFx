using Microsoft.AspNetCore.Http.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Admin.DAL;

namespace TinyFx.Admin.BLL.Caching
{
    public static class AdminCacheUtil
    {
        private static object _sync = new object();

        #region Admin_dictsEO
        private static Dictionary<string, Admin_dictsEO> _dicts;
        public static Dictionary<string, Admin_dictsEO> Dicts
        {
            get
            {
                if (_dicts == null || _dicts.Count ==0)
                {
                    lock (_sync)
                    {
                        if (_dicts == null || _dicts.Count == 0)
                        {
                            _dicts = new Dictionary<string, Admin_dictsEO>();
                            foreach (var dict in new Admin_dictsMO().GetAll())
                                _dicts.Add(dict.DictID, dict);
                        }
                    }
                }
                return _dicts;
            }
        }
        #endregion

        #region Admin_siteEO
        private static Dictionary<string, Admin_siteEO> _sites;
        public static Dictionary<string, Admin_siteEO> Sites
        {
            get 
            { 
                if(_sites==null || _sites.Count ==0)
                {
                    lock (_sync)
                    {
                        if (_sites == null || _sites.Count == 0)
                        {
                            _sites = new Dictionary<string, Admin_siteEO>();
                            foreach (var site in new Admin_siteMO().GetAll())
                                _sites.Add(site.SiteID, site);
                        }
                    }
                }
                return _sites;
            }
        }
        #endregion

        #region Admin_menuEO
        private static Dictionary<string, Admin_menuEO> _menus;
        public static Dictionary<string, Admin_menuEO> Menus
        {
            get
            {
                if (_menus == null || _menus.Count == 0)
                {
                    lock(_sync)
                    {
                        if (_menus == null || _menus.Count == 0)
                        {
                            _menus = new Dictionary<string, Admin_menuEO>();
                            foreach (var menu in new Admin_menuMO().GetAll())
                                _menus.Add(menu.MenuID, menu);
                        }
                    }
                }
                return _menus;
            }
        }
        #endregion

        #region Admin_roleEO
        private static Dictionary<int, Admin_roleEO> _roles;
        public static Dictionary<int, Admin_roleEO> Roles
        {
            get
            {
                if (_roles == null || _roles.Count == 0) 
                {
                    lock (_sync)
                    {
                        _roles = new Dictionary<int, Admin_roleEO>();
                        if (_roles == null || _roles.Count == 0)
                        {
                            foreach (var role in new Admin_roleMO().GetAll())
                                _roles.Add(role.RoleID, role);
                        }
                    }
                }
                return _roles;
            }
        }
        #endregion

        #region RoleMenus
        private static Dictionary<int, List<Admin_menuEO>> _roleMenus;
        public static Dictionary<int, List<Admin_menuEO>> RoleMenus
        {
            get
            {
                if (_roleMenus == null || _roleMenus.Count == 0)
                {
                    lock (_sync) 
                    {
                        if (_roleMenus == null || _roleMenus.Count == 0)
                        {
                            _roleMenus = new Dictionary<int, List<Admin_menuEO>>();
                            foreach (var item in new Admin_role_menuMO().GetAll())
                            {
                                if (!_roleMenus.ContainsKey(item.RoleID))
                                    _roleMenus.Add(item.RoleID, new List<Admin_menuEO>());
                                var menu = Menus[item.MenuID];
                                _roleMenus[item.RoleID].Add(menu);
                            }
                        }
                    }
                }
                return _roleMenus;
            }
        }
        #endregion
    }
}
