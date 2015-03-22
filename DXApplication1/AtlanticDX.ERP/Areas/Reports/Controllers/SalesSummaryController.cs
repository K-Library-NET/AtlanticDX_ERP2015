using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Reports.Controllers
{
    /// <summary>
    /// 销售汇总表
    /// </summary>
    [PrivilegeFramework.ComplexAuthorize]
    public class SalesSummaryController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: Reports/SalesSummary
        public ActionResult Index()
        {
            return View();
        }
    }
}