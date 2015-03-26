using AtlanticDX.ERP.Areas.Orders.Models;
using PrivilegeFramework;
using PrivilegeFramework.PrivilegeFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityFramework;
using YuShang.ERP.Entities.Orders;

namespace AtlanticDX.ERP.Areas.Orders.Controllers
{
    [ComplexAuthorize]
    /// <summary>
    /// 采购管理——采购交单
    /// </summary>
    public class OrderSubmitController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext dxContext = new AtlanticDXContext();

        // GET: /Orders/OrderSubmit
        public ActionResult Index()
        {
            double temp1 = AppBusinessManager.Instance.GetProductsTotal(
                HttpContext.User.Identity.Name);
            double temp2 = AppBusinessManager.Instance.GetOrderPaymentTotal(
                HttpContext.User.Identity.Name);

            //FIXED  商品数量合计
            ViewBag.ProductsTotal = temp1;
            //FIXED  合计采购额
            ViewBag.PaymentTotal = temp2;
            return View();
        }

        [HttpPost]
        public JsonResult Index(OrderSubmitFilterViewModel filterModel,
            int page = 1, int rows = 10)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("ThreadID: {0} , dbcontext:{1}",
                System.Threading.Thread.CurrentThread.ManagedThreadId,
               dxContext.GetHashCode()));

            filterModel.UserName = HttpContext.User.Identity.Name;

            double ProductsTotal = 0;
            double PaymentTotal = 0;
            int total = 0;
            IEnumerable<OrderContract> list = null;

            IQueryable<OrderContract> contractQuery =
                AppBusinessManager.Instance.GetIndexListOrderContract(
               HttpContext.GetOwinContext(), dxContext,
                filterModel as IOrderContractPrivilegeFilter, out ProductsTotal, out PaymentTotal);

            total = contractQuery.AsParallel().Count();

            list = contractQuery.AsParallel().OrderByDescending(
                m => m.OrderCreateTime).Skip((page - 1) * rows).Take(rows).ToList();

            //FIXED  商品数量小计
            //var ProductsTotal = 1111;
            //FIXED  采购额小计
            //var PaymentTotal = 2222;
            return Json(new { total = total, rows = list, ProductsTotal = ProductsTotal, PaymentTotal = PaymentTotal });
        }

        public JsonResult Audit(AuditOrderContractViewModel model)
        {
            if (ModelState.IsValid)
            {
                OrderContract orderContract = dxContext.OrderContracts.Find(model.OrderContractId);
                if (orderContract == null)
                {
                    ModelState.AddModelError("", "数据不存在");
                }
                else if (orderContract.ContractStatus != ContractStatus.NotAudited)
                {
                    ModelState.AddModelError("", "已经审核过，不要重复提交");
                }
                else
                {
                    orderContract.Comments = model.Comments;
                    orderContract.ContractStatus = model.ContractStatus;
                    try
                    {
                        dxContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        LogHelper.Error("采购交单", e);
                        ModelState.AddModelError("", "系统忙，请稍后再试");
                    }

                }
            }
            return Json(ModelState.GetModelStateErrors());
        }

        [HttpPost]
        public JsonResult AddProductItemSalesGuidePrice(
            IEnumerable<AddSalesGuidePriceViewModel> list)
        {
            //FIXED 批量添加指导销售价
            if (ModelState.IsValid)
            {
                OrderContract orderContract = null;
                try
                {
                    ProductItem item = dxContext.ProductItems.Find(
                        list.First().ProductItemId);
                    if (item != null && item.OrderContract != null)
                    {
                        orderContract = dxContext.OrderContracts.Find(
                           item.OrderContract.OrderContractId);
                    }
                    if (orderContract != null)
                    {
                        foreach (var i in orderContract.OrderProducts)
                        {
                            var j = list.FirstOrDefault(
                                m => m.ProductItemId == i.ProductItemId);
                            if (j != null)
                                i.SalesGuidePrice = j.SalesGuidePrice;
                        }

                        dxContext.SaveChanges();
                    }
                }
                catch (Exception ee)
                {
                    ModelState.AddModelError(string.Empty, ee);
                }
            }
            return Json(ModelState.GetModelStateErrors());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            dxContext.Dispose();
        }
    }
}