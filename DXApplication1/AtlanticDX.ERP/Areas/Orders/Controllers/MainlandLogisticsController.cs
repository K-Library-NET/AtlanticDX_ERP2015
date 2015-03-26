using PrivilegeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuShang.ERP.Entities.Orders;

namespace AtlanticDX.ERP.Areas.Orders.Controllers
{
    [ComplexAuthorize]
    public class MainlandLogisticsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        // GET: MainlandLogistics
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(DateTime? ReceiveTimeFrom, DateTime? ReceiveTimeTo,
            string LogisCompanyName = "", int page = 1, int rows = 10)
        {
            IQueryable<ProductItem> sourceQuery = db.ProductItems
                .Include("OrderContract.MainlandLogistics.MLLogisCompany").AsNoTracking()
                .Where(m => m.OrderContract != null && m.OrderContract.MainlandLogistics
                    != null && m.OrderContract.MainlandLogistics.MLLogisCompany != null); ;

            IQueryable<ProductItem> itemList = AppBusinessManager.Instance
                .GetIndexListProductItemByMainlandLogistics(db, sourceQuery,
                LogisCompanyName, ReceiveTimeFrom, ReceiveTimeTo,
                HttpContext.User.Identity.Name);

            //Fixed 20150108 liangdawen : => to ProductItem 
            IEnumerable<ProductItem> list = itemList.AsParallel()
               .OrderByDescending(m => m.OrderContract.MainlandLogistics.MLLogisCompany.CompanyName)
               .Skip((page - 1) * rows).Take(rows).ToList();

            return Json(new { total = itemList.Count(), rows = list });
        }
    }
}