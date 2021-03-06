﻿using System.Linq;
using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.Finances.Controllers
{
    /// <summary>
    /// 采购订金
    /// </summary>
    [PrivilegeFramework.ComplexAuthorize]
    public class ImportDepositeController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: ImportDeposite
        public ActionResult Index()
        {
            return View();
        }

        private AtlanticDXContext db = new AtlanticDXContext();

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            var list1 = db.AccountsPayables.Where(m => m.EventType ==
                YuShang.ERP.Entities.Finances.AccountingEventType.ImportDeposite);

            var list = list1.OrderByDescending(m => m.CTIME).Skip((page - 1) * rows).Take(rows);

            return Json(new { total = list1.Count(), rows = list.ToList() });
        }
    }
}