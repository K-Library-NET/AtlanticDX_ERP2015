using PrivilegeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuShang.ERP.Entities.Orders;

namespace AtlanticDX.ERP.Areas.Orders.Controllers
{
    /// <summary>
    /// 采购管理——采购合同
    /// </summary>
    [ComplexAuthorize]
    public class OrderContractController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {

        private AtlanticDXContext dxContext = new AtlanticDXContext();

        // GET: /Oreders/OrderContract
        public ActionResult Index()
        {
            double temp1 = AppBusinessManager.Instance.GetOrdersTotal(
                HttpContext.User.Identity.Name); // 1234;
            double temp2 = AppBusinessManager.Instance.GetOrderDepositeTotal(
                HttpContext.User.Identity.Name); //5678;

            //TODO  订单总数
            ViewBag.OrdersPaymentTotal = temp1;

            //TODO  已付总定金
            ViewBag.PaidDepositTotal = temp2;
            return View();
        }

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10, DateTime? ETA = null,
            string filterValue = "")
        {
            DateTime dt1 = DateTime.Now;

            ContractListCondition condition = new ContractListCondition()
            {
                IsEnable = true,
                Page = page,
                Rows = rows,
                ListInclude = ContractListInclude.BothOrderAndSaleContract
                    | ContractListInclude.WithAggregations
                    | ContractListInclude.WithHarborAgent
                    | ContractListInclude.WithHongkongLogistics
                    | ContractListInclude.WithMainlandLogistics
                    | ContractListInclude.WithOrderCompensation
                    | ContractListInclude.WithProductStock
                    | ContractListInclude.WithSaleCompensation,
                UserName = HttpContext.User.Identity.Name,
                OrderField = ContractOrderField.CTIME_DESC,
                ProductFullNameFilterValue = filterValue,
            };
            if (ETA.HasValue)
            {
                condition.ETAFrom = ETA.GetValueOrDefault().Date;
                condition.ETATo = ETA.GetValueOrDefault().Date.AddDays(1);
            }

            ContractViewModel contractViewModel = ContractManager.Instance.GetIndexListContracts(
                HttpContext.GetOwinContext(), dxContext, condition);

            TimeSpan span1 = DateTime.Now.Subtract(dt1);
            System.Diagnostics.Debug.WriteLine(string.Format(
                "OrderContractController Index part1 :{0},{1},{2},{3}; TimeSpan:{4}", page, rows, ETA, filterValue, span1.TotalMilliseconds));

            IEnumerable<ContractInfo> list = contractViewModel.ContractItems;

            TimeSpan span2 = DateTime.Now.Subtract(dt1);
            System.Diagnostics.Debug.WriteLine(string.Format(
                "OrderContractController Index:{0},{1},{2},{3}; TimeSpan:{4}", page, rows, ETA, filterValue, span2.TotalMilliseconds));
            return Json(new
            {
                total = contractViewModel.Aggregations.Count.GetValueOrDefault(),
                rows = list
            });
        }

        public ActionResult Add()
        {
            //当前采购员
            ViewBag.CurrentUser = dxContext.Users.Where(m => m.UserName == HttpContext.User.Identity.Name).SingleOrDefault();
            return View();
        }


        /// <summary>
        /// 报关和物流信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddBgwl(string OrderContractKey)
        {
            ContractListCondition condition = new ContractListCondition()
            {
                IsEnable = true,
                UserName = HttpContext.User.Identity.Name,
                ListInclude = ContractListInclude.WithMainlandLogistics | ContractListInclude.WithHongkongLogistics
               | ContractListInclude.WithHarborAgent | ContractListInclude.WithOrderCompensation
            };
            var temp = ContractManager.Instance.FindBy(dxContext, HttpContext.GetOwinContext(),
                OrderContractKey, ContractViewModelType.OrderContract, condition);
          
            return View(temp);
        }

        [HttpPost]
        public JsonResult AddBgwl(ContractInfo model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = AppBusinessManager.Instance.AddOrderContractRelatedObjs(
                    dxContext as ExtendedIdentityDbContext, model,
                    HttpContext.User.Identity.Name);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError(string.Empty, "添加失败");
                }
            }
            var temp = ModelState.GetModelStateErrors();
            //System.Diagnostics.Debug.WriteLine(ModelState.GetModelStateErrors());
            return Json(temp);  //ModelState.GetModelStateErrors());
        }

        [HttpPost]
        public JsonResult Add(ContractInfo model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = AppBusinessManager.Instance.AddOrderContractCore(
                    dxContext as ExtendedIdentityDbContext, model,
                    HttpContext.User.Identity.Name);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError(string.Empty, "添加失败");
                }
            }
            var temp = ModelState.GetModelStateErrors();
            //System.Diagnostics.Debug.WriteLine(ModelState.GetModelStateErrors());
            return Json(temp);  //ModelState.GetModelStateErrors());
        }

        public ActionResult Edit(string OrderContractKey)
        {
            ContractListCondition condition = new ContractListCondition()
            {
                IsEnable = true,
                UserName = HttpContext.User.Identity.Name,
                ListInclude = ContractListInclude.WithMainlandLogistics | ContractListInclude.WithHongkongLogistics
               | ContractListInclude.WithHarborAgent | ContractListInclude.WithOrderCompensation
            };

            var temp = ContractManager.Instance.FindBy(dxContext, HttpContext.GetOwinContext(),
                OrderContractKey, ContractViewModelType.OrderContract, condition);

            //var temp = dxContext.OrderContracts.Include("HarborAgent").Include("HongKongLogistics").Include("MainlandLogistics").First(m => m.OrderContractKey == OrderContractKey);
            return View(temp);
        }

        [HttpPost]
        public JsonResult Edit(ContractInfo model)
        {
            return this.UpdateCore(model);
            //return Json(null);
        }

        private JsonResult UpdateCore(ContractInfo model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = AppBusinessManager.Instance.UpdateOrderContractCore(
                    dxContext, model, HttpContext.User.Identity.Name);

                if (!string.IsNullOrEmpty(errorMessage))
                    ModelState.AddModelError(string.Empty, errorMessage);
                return Json(ModelState.GetModelStateErrors());
            }

            return Json(null);
        }

        private JsonResult RemoveCore(OrderContract model)
        {
            if (ModelState.IsValid)
            {
                var temp = dxContext.OrderContracts.FirstOrDefault(
                     m => model.OrderContractKey == model.OrderContractKey);
                if (temp != null)
                {
                    dxContext.OrderContracts.Remove(temp);
                    dxContext.SaveChanges();
                }
            }

            return Json(null);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            dxContext.Dispose();
        }
    }
}