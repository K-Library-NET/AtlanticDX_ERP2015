using Microsoft.Owin;
using PrivilegeFramework;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using YuShang.ERP.Entities.Stocks;

namespace AtlanticDX.Model.Areas.Stocks.Controllers
{
    /// <summary>
    /// 商品入仓
    /// </summary>
    public class StockInsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        AtlanticDXContext db = new AtlanticDXContext();

        // GET: Stocks/StockIns
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10, string filterValue = "")
        {
            IOwinContext owinContext = HttpContext.GetOwinContext();
            ContractListCondition condition = new ContractListCondition()
            {
                IsEnable = true,
                OrderField = ContractOrderField.CONTRACT_KEY_ASC,
                OrderType = 1,
                Page = 1,
                Rows = 10,
                ListInclude = //条件：只返回采购+不包含未审核通过或者未经审核
                    //+带有港口代理+内地物流+香港物流+采购索赔信息
                    //需要返回Count
                    ContractListInclude.OrderContractOnly
                    | ContractListInclude.WithoutContractStatusAuditNoPass
                    | ContractListInclude.WithoutContractStatusNotAudited
                    | ContractListInclude.WithHarborAgent
                    | ContractListInclude.WithHongkongLogistics
                    | ContractListInclude.WithMainlandLogistics
                    | ContractListInclude.WithOrderCompensation
                    | ContractListInclude.WithAggregations,
                UserName = HttpContext.User.Identity.Name
            };
            //ContractManager.Instance.GetIndexListOrderContract(owinContext, db, condition);
            ProductItemViewModel viewModels = ContractManager.Instance
                .GetIndexListProductItemsNonStock(
                owinContext, db, condition, null);//不管是否“已经收货”都可以入仓

            if (viewModels == null || viewModels.IsEnable == false)
            {
                return Json(
                    new
                    {
                        total = 0,
                        rows = new List<ProductItemInfo>(),
                        errormessage = viewModels.ErrorMessage,
                    });
            }

            int count = 0;

            if (viewModels.Aggregations.IsEnable.GetValueOrDefault() && viewModels.Aggregations.Count.HasValue)
            {
                count = viewModels.Aggregations.Count.Value;
            }

            return Json(new { total = count, rows = viewModels.ContractItems });

            ////FIXED 获取待入库商品列表
            //return null;
        }

        [HttpPost]
        public JsonResult Add(StockItem model)
        {
            //FIXED 入库处理逻辑
            if (ModelState.IsValid)
            {
                string errorMessage = AppBusinessManager.Instance
                     .AddOrUpdateStockItem(db, model, HttpContext.User.Identity.Name);

                if (!string.IsNullOrEmpty(errorMessage))
                    ModelState.AddModelError(string.Empty, errorMessage);
            }
            var allErrors = this.GetModelStateErrors(ModelState);
            return Json(allErrors);
        }


    }
}