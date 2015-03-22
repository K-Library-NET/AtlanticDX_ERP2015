using AtlanticDX.ERP.Areas.Finances.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuShang.ERP.Entities.Finances;

namespace AtlanticDX.ERP.Areas.Finances.Controllers
{
    /// <summary>
    /// 应付账款
    /// </summary>
    public class AccountsPayablesController : Controller
    {
        // GET: Finances/AccountsPayables
        public ActionResult Index()
        {
            return View();
        }

        private AtlanticDXContext db = new AtlanticDXContext();

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        { 
            List<AccountingEventType> eventTypes = new List<AccountingEventType>();

            //var list = db.AccountsReceivables//.Include("OrderContract")
            //    .Where(m => ((eventTypes.Count < 1 ? true : false) ||
            //    eventTypes.Contains(m.EventType))).GroupJoin(db.FinancialRecordRelations,
            //    rec => rec.AccountsReceivableId, relation => relation.RelatedObjectType
            //        == FinancialRelatedObjectType.AccountsReceiveRecord_To_AccountsReceivable ? relation.RelatedObjectId : -1,
            //    (Receivable, Relation) => new { Receivable, Relation, Receivable.CTIME })
            //    .OrderByDescending(m => m.CTIME)
            //    .Skip((page - 1) * rows).Take(rows).ToList();

            var list1 = db.AccountsPayables
                .Where(m => ((eventTypes.Count < 1 ? true : false) ||
                eventTypes.Contains(m.EventType)));
            var list2 = list1.OrderByDescending(m => m.CTIME)
               .Skip((page - 1) * rows).Take(rows).ToList();

            var list3 = from one in list2 select new AccountsPayableViewModel(one, db);

            return Json(new { total = list1.Count(), rows = list3.ToList() });
        }
    }
}