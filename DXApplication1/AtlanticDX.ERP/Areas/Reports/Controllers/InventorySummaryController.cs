using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Reports.Controllers
{
    /// <summary>
    /// 商品库存汇总表
    /// </summary>
    [PrivilegeFramework.ComplexAuthorize]
    public class InventorySummaryController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: Reports/InventorySummary
        public ActionResult Index()
        {
            return View();
        }
    }
}