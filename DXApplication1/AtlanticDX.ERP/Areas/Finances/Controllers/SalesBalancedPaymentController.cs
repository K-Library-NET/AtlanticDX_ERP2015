using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Finances.Controllers
{
    /// <summary>
    /// 销售尾款
    /// </summary>
    [PrivilegeFramework.ComplexAuthorize]
    public class SalesBalancedPaymentController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: SalesBalancedPayment
        public ActionResult Index()
        {
            return View();
        }

        private AtlanticDXContext db = new AtlanticDXContext();

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            var list1 = db.AccountsReceivables.Where(m => m.EventType ==
                YuShang.ERP.Entities.Finances.AccountingEventType.SalesBalancedPayment);

            var list = list1.OrderByDescending(m => m.CTIME).Skip((page - 1) * rows).Take(rows);

            return Json(new { total = list1.Count(), rows = list.ToList() });
        }
    }
}