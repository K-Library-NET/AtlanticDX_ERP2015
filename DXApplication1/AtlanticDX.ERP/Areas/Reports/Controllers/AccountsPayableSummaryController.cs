using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Reports.Controllers
{
    /// <summary>
    /// 应付账款汇总表
    /// </summary>
    [PrivilegeFramework.ComplexAuthorize]
    public class AccountsPayableSummaryController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: Reports/AccountsPayableSummary
        public ActionResult Index()
        {
            return View();
        }
    }
}