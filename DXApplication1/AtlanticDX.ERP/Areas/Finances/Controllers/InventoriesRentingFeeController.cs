﻿using System.Linq;
using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.Finances.Controllers
{
    /// <summary>
    /// 仓租
    /// </summary>
    [PrivilegeFramework.ComplexAuthorize]
    public class InventoriesRentingFeeController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: InventoriesRentingFee
        public ActionResult Index()
        {
            return View();
        }

        private AtlanticDXContext db = new AtlanticDXContext();

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            var list1 = db.AccountsPayables.Where(m => m.EventType ==
                YuShang.ERP.Entities.Finances.AccountingEventType.InventoriesRentingFee);

            var list = list1.OrderByDescending(m => m.CTIME).Skip((page - 1) * rows).Take(rows);

            return Json(new { total = list1.Count(), rows = list.ToList() });
        }
    }
}