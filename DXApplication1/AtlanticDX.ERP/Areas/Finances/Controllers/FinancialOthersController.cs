using AtlanticDX.Model.Areas.Finances.Models;
using System.Linq;
using System.Web.Mvc;
using YuShang.ERP.Entities.Finances;

namespace AtlanticDX.Model.Areas.Finances.Controllers
{
    /// <summary>
    /// 其他分类
    /// </summary>
    [PrivilegeFramework.ComplexAuthorize]
    public class FinancialOthersController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: FinancialOthers
        public ActionResult Index()
        {
            return View();
        }

        private AtlanticDXContext db = new AtlanticDXContext(); 

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            var list1 = db.AccountsPayables.Select<AccountsPayable, FinancialViewModel>(m =>
                new FinancialViewModel(m));
            var list2 = db.AccountsReceivables.Select<AccountsReceivable, FinancialViewModel>(
                m => new FinancialViewModel(m));

            var unionAll = list1.Union(list2);

            var list = unionAll.OrderByDescending(m => m.CTIME).Skip((page - 1) * rows).Take(rows);

            return Json(new { total = unionAll.Count(), rows = list.ToList() });
        }
    }
}