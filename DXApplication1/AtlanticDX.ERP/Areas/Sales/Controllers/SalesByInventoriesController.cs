using AtlanticDX.ERP.Areas.Orders.Models;
using AtlanticDX.ERP.Areas.Sales.Models;
using PrivilegeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.Sale;

namespace AtlanticDX.ERP.Areas.Sales.Controllers
{
    [ComplexAuthorize]
    /// <summary>
    /// 现货销售
    /// </summary>
    public class SalesByInventoriesController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {

        private AtlanticDXContext dxContext = new AtlanticDXContext();
        // GET: SalesByInventories
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="filterValue">请输入单据号或供应商</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10, DateTime? DateFrom = null, DateTime? DateTo = null,
            string filterValue = "")
        {
            ContractListCondition condition = new ContractListCondition()
            {
                IsEnable = true,
                ListInclude = ContractListInclude.SaleContractOnly
                | ContractListInclude.WithAggregations
               | ContractListInclude.WithProductStock,
                OrderType = 1,
                OrderField = ContractOrderField.CTIME_ASC,
                Page = page,
                Rows = rows,
                ProductFullNameFilterValue = filterValue,
                UserName = HttpContext.User.Identity.Name
            };

            var viewModels = ContractManager.Instance.GetIndexListContracts(HttpContext.GetOwinContext(),
                dxContext, condition);

            int total = 0;
            if (viewModels != null && viewModels.IsEnable.GetValueOrDefault())
            {
                if (viewModels.Aggregations != null &&
                    viewModels.Aggregations.IsEnable.GetValueOrDefault())
                    total = viewModels.Aggregations.Count.GetValueOrDefault();
                return Json(new { total = total, rows = viewModels.ContractItems });
            }
            else if (viewModels != null && !string.IsNullOrEmpty(viewModels.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, viewModels.ErrorMessage);
            }

            //IQueryable<SaleContract> saleContractQuery =
            //    AppBusinessManager.Instance.GetIndexListSaleContract(dxContext, 1, //现货销售
            //    DateFrom, DateTo, filterValue, HttpContext.User.Identity.Name, out total);

            //IEnumerable<SaleContract> list = saleContractQuery.AsParallel()
            //   .OrderByDescending(m => m.SaleCreateTime).Skip((page - 1) * rows).Take(rows).ToList();

            //FIXED  获取现货销售列表
            return Json(new { total = total, rows = new ContractInfo[] { } });
        }

        public ActionResult Add()
        {
            ViewBag.CurrentUser = dxContext.Users.Where(m => m.UserName == HttpContext.User.Identity.Name).SingleOrDefault();
            return View();
        }

        [HttpPost]
        public JsonResult Add(ContractInfo model)
        {
            if (ModelState.IsValid)
            {

                //FIXED  添加现货销售
                string errorMsg = AppBusinessManager.Instance.AddSaleContractCore(
                    dxContext as ExtendedIdentityDbContext, model,
                    HttpContext.User.Identity.Name);

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    this.ModelState.AddModelError(string.Empty, errorMsg);
                }
            }
            return Json(ModelState.GetModelStateErrors());
        }

        public ActionResult Edit(string SaleContractKey)
        {
            var sale = dxContext.SaleContracts.Include(
                "SaleProducts.OrderProductItem.Product")
                .Include("SaleProducts.StockItem.StoreHouse")
                .Include("SaleClient").Include("SaleBargins")
                .First(m => m.SaleContractKey == SaleContractKey);
            if (sale == null)
                return View(sale);

            ContractInfo model = new ContractInfo(sale);

            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(ContractInfo model)
        {
            if (ModelState.IsValid)
            {
                string errorMsg = AppBusinessManager.Instance.UpdateSaleContractCore(
                    dxContext as ExtendedIdentityDbContext, model, HttpContext.User.Identity.Name);
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    this.ModelState.AddModelError(string.Empty, errorMsg);
                }
                //FIXED  编辑现货销售
            }
            return Json(ModelState.GetModelStateErrors());
        }

        public ActionResult Audit(string SaleContractKey)
        {
            var temp = dxContext.SaleContracts.FirstOrDefault(m => m.SaleContractKey == SaleContractKey);

            if (temp != null)
            {
                return View(
                    //return Json(
                    new SaleContractWithBargainsViewModel()
                {
                    SaleContract = temp,
                    SaleBargainsCount = temp.SaleBargins.Count,
                    SaleBargains = temp.SaleBargins.OrderByDescending(p => p.Total).Take(10).ToArray(),//返回头10个
                    SelectedSaleBargainId = temp.SelectedSaleBargainId
                }
                    //, JsonRequestBehavior.AllowGet);
                );
            }

            return View();
        }

        [HttpPost]
        public JsonResult Audit(AuditSaleContractViewModel model)
        {
            if (ModelState.IsValid)
            {
                SaleContract contract = dxContext.SaleContracts.Find(model.SaleContractId);
                if (contract != null && (contract.ContractStatus == ContractStatus.AuditNoPass
                    || contract.ContractStatus == ContractStatus.NotAudited))
                {
                    string errorMessage = AppBusinessManager.Instance.UpdateSaleContractStatusCore(
                        dxContext, contract, model.ContractStatus, HttpContext.User.Identity.Name);

                    if (!string.IsNullOrEmpty(errorMessage))
                        ModelState.AddModelError(string.Empty, errorMessage);
                }
                //FIXED 审核销售订单
            }
            return Json(ModelState.GetModelStateErrors());
        }

        public ActionResult AddSaleBargin(string SaleContractKey)
        {
            var sale = dxContext.SaleContracts.FirstOrDefault(m => m.SaleContractKey == SaleContractKey);
            return View(sale);
        }

        [HttpPost]
        public JsonResult AddSaleBargin(AddSaleBarginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userName = HttpContext.User.Identity.Name;
                SaleBargain bargain = new SaleBargain()
                {
                    BargainItems = model.BargainItems,
                    BargainSysUserKey = userName,
                    SaleContractId = model.SaleContractId
                };

                string errorMsg = AppBusinessManager.Instance.AddOrUpdateSaleBargain(
                   this.dxContext as ExtendedIdentityDbContext, bargain);

                if (!string.IsNullOrEmpty(errorMsg))
                    ModelState.AddModelError(string.Empty, errorMsg);
                //FIXED 添加销售还价逻辑
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