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
    public class HongkongLogisticsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        // GET: HongkongLogistics
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(DateTime? ReceiveTimeFrom, DateTime? ReceiveTimeTo,
            string LogisCompanyName = "", int page = 1, int rows = 10)
        {
            IQueryable<ProductItem> sourceQuery = db.ProductItems
                .Include("OrderContract.HongKongLogistics.HKLogisCompany").AsNoTracking()
                .Where(m => m.OrderContract != null && m.OrderContract.HongKongLogistics
                    != null && m.OrderContract.HongKongLogistics.HKLogisCompany != null);

            IQueryable<ProductItem> itemList = AppBusinessManager.Instance
                .GetIndexListProductItemByHKLogistics(db, sourceQuery,
                LogisCompanyName, ReceiveTimeFrom, ReceiveTimeTo,
                HttpContext.User.Identity.Name);

            //var temp1 = sourceQuery.ToArray();

            //Fixed 20150108 liangdawen : => to ProductItem 
            IEnumerable<ProductItem> list = itemList.AsParallel()
               .OrderByDescending(m => m.OrderContract.HongKongLogistics.HKLogisCompany.CompanyName)
               .Skip((page - 1) * rows).Take(rows).ToList();

            return Json(new { total = itemList.Count(), rows = list });
        }
    }
}