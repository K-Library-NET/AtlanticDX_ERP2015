using PrivilegeFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YuShang.ERP.Entities.Stocks;

namespace AtlanticDX.Model.Areas.Stocks.Controllers
{
    /// <summary>
    /// 商品出仓记录
    /// </summary>
    [ComplexAuthorize]
    public class StockOutRecordsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDX.Model.AtlanticDXContext m_db = new AtlanticDXContext();

        // GET: Stocks/StockOutRecords
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10, string filterValue = "")
        {
            var tmpQuery = m_db.StockOutRecords.AsNoTracking();
            IEnumerable<StockOutRecord> recs = tmpQuery.Skip((page - 1) * rows)
                .Take(rows).AsParallel().ToArray();

            return Json(new { total = tmpQuery.Count(), rows = recs });

            #region 获取待出仓商品列表
            /*
            ContractListCondition condition = new ContractListCondition()
            {
                Page = 1,
                Rows = 10,
                ListInclude = ContractListInclude.SaleContractOnly
                    | ContractListInclude.WithAggregations | ContractListInclude.WithProductStock,
                OrderField = ContractOrderField.CTIME_ASC,
                IsEnable = true,
                OrderType = 1,
                UserName = HttpContext.User.Identity.Name,
            };

            StockOutViewModel outItems = ContractManager.Instance
                    .GetIndexListStockItemForSold(
                         HttpContext.GetOwinContext(), m_db as ExtendedIdentityDbContext,
                        condition);

            if (outItems != null && outItems.IsEnable.GetValueOrDefault()
                && outItems.StockViewModel != null && outItems.StockViewModel.IsEnable.GetValueOrDefault())
            {
                return Json(new
                {
                    total = outItems.Aggregations.Count,
                    rows = outItems.StockViewModel.StockItems
                });
            }

            if (outItems != null && !string.IsNullOrEmpty(outItems.ErrorMessage))
                return Json(outItems.ErrorMessage);

            return Json(null);
            //FIXED 获取待出仓商品列表 
             */
            #endregion
        }


        [HttpPost]
        public JsonResult Add(StockOutRecord model)
        {
            //FIXED 出仓处理逻辑
            if (ModelState.IsValid)
            {
                string errorMessage = AddStockOutForUpdateStockItem(model);
                if (!string.IsNullOrEmpty(errorMessage))
                    ModelState.AddModelError(string.Empty, errorMessage);
            }
            var allErrors = this.GetModelStateErrors(ModelState);
            return Json(allErrors);
        }

        private string AddStockOutForUpdateStockItem(StockOutRecord model)
        {
            return AppBusinessManager.Instance.AddStockOutForUpdateStockItem(
                m_db, model, HttpContext.User.Identity.Name);
        }
    }
}