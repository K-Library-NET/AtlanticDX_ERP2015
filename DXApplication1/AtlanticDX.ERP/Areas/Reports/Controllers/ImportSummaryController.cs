using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Reports.Controllers
{
    /// <summary>
    /// 采购汇总表
    /// </summary>
    [PrivilegeFramework.ComplexAuthorize]
    public class ImportSummaryController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: Reports/ImportSummary
        public ActionResult Index()
        {
            return View();
        }
    }
}