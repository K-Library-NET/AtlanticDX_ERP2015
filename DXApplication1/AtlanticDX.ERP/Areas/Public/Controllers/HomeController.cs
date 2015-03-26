using PrivilegeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuShang.ERP.Entities.Privileges;

namespace AtlanticDX.ERP.Areas.Public.Controllers
{
    [ComplexAuthorize]
    public class HomeController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: Public/Home
        public ActionResult Index()
        {
            ViewBag.Title = "大西洋贸易ERP系统";
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                //用户信息
                ViewBag.CurrentUser = context.Users.Where(m => m.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
                //左侧菜单
                ViewBag.TopMenus = context.SysMenus.Where(m => m.ParentId == 0).ToList();
                if (ViewBag.TopMenus != null)
                {
                    foreach (SysMenu topMenu in ViewBag.TopMenus)
                    {
                        IList<SysMenu> subMenus = context.SysMenus.Where(m => m.ParentId == topMenu.SysMenuId
                            && m.IsShowInNavTree > 0).OrderBy(m => m.IsShowInNavTree).ToList();
                        ViewData["SubMenu" + topMenu.SysMenuId] = subMenus.Count() > 0 ? subMenus : null;
                    }
                }
            }
            return View();
        }

        public ActionResult Main()
        {
            //用户信息
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                ViewBag.CurrentUser = context.Users.Where(m => m.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            }
            return View();
        }

    }
}