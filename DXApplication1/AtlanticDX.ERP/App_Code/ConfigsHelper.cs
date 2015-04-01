using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuShang.ERP.Entities.Privileges;

namespace AtlanticDX.ERP.Helper
{
    public class ConfigsHelper
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetSysRoles()
        {
            PrivilegeFramework.ApplicationRoleManager roleMgr =
                new PrivilegeFramework.ApplicationRoleManager(
                    new PrivilegeFramework.ExtendedIdentityDbContext(
                    PrivilegeFramework.ExtendedIdentityDbContext.GetNameOrConnectionByConfig()));

            var dbroles = roleMgr.Roles.ToList();

            if (dbroles != null && dbroles.Count > 0)
            {
                var results = from one in dbroles
                              orderby one.PrivilegeLevel ascending
                              select new SelectListItem()
                              {
                                  Text = one.Name,
                                  Value = one.Name,
                                  Selected = one.PrivilegeLevel <= 1
                              };

                return results;
            }

            ////FIXED 获取角色列表
            return new SelectListItem[] {
                new SelectListItem{Value="游客",Text="游客"}
            };
        }
    }
}