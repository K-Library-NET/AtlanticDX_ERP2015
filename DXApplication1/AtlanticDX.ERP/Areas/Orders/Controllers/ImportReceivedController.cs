using AtlanticDX.Model.Areas.Orders.Models;
using PrivilegeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YuShang.ERP.Entities.Orders;

namespace AtlanticDX.Model.Areas.Orders.Controllers
{
    [ComplexAuthorize]
    public class ImportReceivedController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext dxContext = new AtlanticDXContext();

        // GET: MainlandReceived
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderCreateTimeFrom"></param>
        /// <param name="OrderCreateTimeTo"></param>
        /// <param name="filterValue">单据号或供应商或备注</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Index(DateTime? OrderCreateTimeFrom,
            DateTime? OrderCreateTimeTo, string receiveFilterValue,
            int page = 1, int rows = 10)
        {
            IQueryable<ProductItem> sourceQuery = dxContext.ProductItems
                .Include("OrderContract.Supplier").AsNoTracking()
                .Where(p => p.OrderContract != null && p.OrderContract.Supplier != null);

            IQueryable<ProductItem> itemList = AppBusinessManager.Instance.
                GetIndexListProductItemByReceiveFilter(dxContext, sourceQuery,
                receiveFilterValue, OrderCreateTimeFrom, OrderCreateTimeTo,
                HttpContext.User.Identity.Name);

            //Fixed 20150108 liangdawen : => to ProductItem 
            IEnumerable<ProductItem> list = itemList.AsParallel()
               .OrderBy(m => m.OrderContract.OrderCreateTime)
               .Skip((page - 1) * rows).Take(rows).ToList();

            return Json(new { total = itemList.Count(), rows = list });
        }


        [HttpPost]
        public JsonResult AddCompensate(AddCompensateViewModel model)
        {
            if (ModelState.IsValid)
            {
                OrderClaimCompensationItem item = new OrderClaimCompensationItem()
                {
                    Compensation = model.Compensation,
                    CompensationHappenedType = model.CompensationHappenedType,
                    CompensationReason = model.CompensationReason,
                    //Currency = model.Currency,
                    ProductItemId = model.ProductItemId
                };
                string errorMessage = AppBusinessManager.Instance.AddOrUpdateOrderCompensationItem(
                       dxContext, item);
                if (!string.IsNullOrEmpty(errorMessage))
                    ModelState.AddModelError(string.Empty, errorMessage);

                //FIXED 添加索赔处理业务逻辑
            }
            return Json(this.GetModelStateErrors(ModelState));
        }

        [HttpPost]
        public JsonResult DoReceive(ReceiveViewModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = AppBusinessManager.Instance
                    .UpdateImportProductItemReceive(dxContext, model.ProductItemId,
                     model.ReceiveTime, model.Status, model.Comments);

                if (!string.IsNullOrEmpty(errorMessage))
                    ModelState.AddModelError(string.Empty, errorMessage);
                //FIXED 添加收货处理业务逻辑
            }
            return Json(this.GetModelStateErrors(ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            dxContext.Dispose();
        }
    }
}