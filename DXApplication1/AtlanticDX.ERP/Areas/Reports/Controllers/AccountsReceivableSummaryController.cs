using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.Reports.Controllers
{
    /// <summary>
    /// 应收账款汇总表
    /// </summary>
    [PrivilegeFramework.ComplexAuthorize]
    public class AccountsReceivableSummaryController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: Reports/AccountsReceivableSummary
        public ActionResult Index()
        {
            return View();
        }
    }
}