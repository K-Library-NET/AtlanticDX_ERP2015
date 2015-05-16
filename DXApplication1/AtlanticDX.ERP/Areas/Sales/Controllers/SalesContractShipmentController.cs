using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrivilegeFramework;
using YuShang.ERP.Entities.Sale;

namespace AtlanticDX.Model.Areas.Sales.Controllers
{
    [ComplexAuthorize]
    public class SalesContractShipmentController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        // GET: SalesContractShipment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10, string filterValue = "")
        {
            ContractListCondition condition = new ContractListCondition()
            {
                IsEnable = true,
                ListInclude = ContractListInclude.SaleContractOnly | ContractListInclude.WithAggregations
                | ContractListInclude.WithoutContractStatusAuditNoPass | ContractListInclude.WithoutContractStatusNotAudited
                | ContractListInclude.WithProductStock | ContractListInclude.WithSaleCompensation,
                Rows = rows,
                Page = page,
                OrderField = ContractOrderField.CTIME_ASC,
                UserName = HttpContext.User.Identity.Name
            };

            //FIXED 销售合同待发货列表
            //不管现货期货，只要SaleProductItem状态是未发货即可
            ProductItemViewModel viewModels = ContractManager.Instance.GetIndexListProductItemsForShipment(
                HttpContext.GetOwinContext(), db, condition);

            if (viewModels != null && viewModels.IsEnable == true)
            {
                int count = 0;
                if (viewModels.Aggregations != null && viewModels.Aggregations.IsEnable.GetValueOrDefault())
                {
                    count = viewModels.Aggregations.Count.GetValueOrDefault();
                }

                return Json(new { total = count, rows = viewModels.SaleProductItems });
            }

            return Json(this.GetModelStateErrors(ModelState));
        }

        [HttpPost]
        public JsonResult Shipment(SaleProductItemInfo model)
        {
            SaleProductItem item = this.GetSaleProductItem(model);
            if (item == null)
            {
                ModelState.AddModelError(string.Empty, "找不到对应的货品项。");
                return Json(this.GetModelStateErrors(ModelState));
                //return View(this.GetModelStateErrors(ModelState));
            }

            if (item != null)
            {
                item.Comments = model.Comments;
                item.ShipmentStatus = model.ShipmentStatus;
            }

            string errorMsg = AppBusinessManager.Instance.AddOrUpdateSaleProductItem(
                HttpContext.GetOwinContext(), db, item);

            if (!string.IsNullOrEmpty(errorMsg))
            {
                ModelState.AddModelError(string.Empty, errorMsg);
            }

            return Json(this.GetModelStateErrors(ModelState));
            //FIXED 销售发货业务逻辑
            //return View(this.GetModelStateErrors(ModelState));
        }

        private SaleProductItem GetSaleProductItem(SaleProductItemInfo model)
        {
            SaleProductItem productItem = null;
            if (model != null && model.SaleContractId.HasValue && model.ProductItemId.HasValue)
            {
                productItem = db.SaleProductItems.FirstOrDefault(m =>
                   m.SaleContractId == model.SaleContractId.GetValueOrDefault()
                   && (m.ProductItemId == model.ProductItemId.Value));
            }
            else if (model != null && model.SaleProductItemId.HasValue)
            {
                productItem = db.SaleProductItems.Find(
                    model.SaleProductItemId.GetValueOrDefault());
            }
            else if (model != null && model.StockItemId.HasValue)
            {
                productItem = db.SaleProductItems.FirstOrDefault(m =>
                    m.StockItemId == model.StockItemId.Value);
            }

            //if (productItem == null)
            //{
            //    productItem = db.SaleProductItems.Create();
            //    productItem.ProductItemId = model.ProductItemId;
            //    productItem.Quantity = model.Quantity;
            //    productItem.SaleContractId = model.SaleContractId.GetValueOrDefault();
            //    productItem.StockItemId = model.StockItemId;
            //    productItem.UnitPrice = model.UnitPrice;
            //    productItem.Weight = model.Weight;
            //    productItem.Currency = model.Currency;
            //    productItem.Comments = model.Comments;
            //    productItem.ShipmentStatus = model.ShipmentStatus;
            //}

            return productItem;
        }

        [HttpPost]
        public JsonResult ClaimCompensation(SaleCompensationInfoItem model)
        {
            if (model == null || model.SaleProductItemId.HasValue == false)
            {
                ModelState.AddModelError(string.Empty, "找不到请求索赔所对应的货品。");
                return Json(this.GetModelStateErrors(ModelState));
            }
            SaleClaimCompensation entity = this.GetSaleProductCompensationItem(model);

            string errorMsg = AppBusinessManager.Instance.AddOrUpdateSaleClaimCompensation(
                HttpContext.GetOwinContext(), db, entity, model);

            if (!string.IsNullOrEmpty(errorMsg))
            {
                ModelState.AddModelError(string.Empty, errorMsg);
            }
            return Json(this.GetModelStateErrors(ModelState));
            //FIXED 销售索赔业务逻辑
            //return View(this.GetModelStateErrors(ModelState));
        }

        private SaleClaimCompensation GetSaleProductCompensationItem(
            SaleCompensationInfoItem model)
        {
            SaleClaimCompensationItem item = null;
            if (model != null && model.SaleProductItemId.HasValue)//.SaleContractId.HasValue && model.ProductItemId.HasValue)
            {
                item = db.SaleClaimCompensationItems.FirstOrDefault(m =>
                    m.SaleProductItemId == model.SaleProductItemId.GetValueOrDefault());
            }

            if (item != null && item.SaleClaimCompensation != null)
                return item.SaleClaimCompensation;

            //if (item != null && item.SaleProductItem != null
            //    && item.SaleProductItem.SaleContract != null)
            //{
            //    SaleClaimCompensation claimCompensation = db.SaleClaimCompensations.Create();
            //    claimCompensation.IsCompensationClear = false;
            //    claimCompensation.IsCompensationAbandoned = false;

            //    return claimCompensation;
            //}

            return null;
            //    && (m.ProductItemId == model.ProductItemId.GetValueOrDefault()));

            //if (item == null)
            //{
            //    item = db.SaleProductItems.Create();
            //    item.ProductItemId = model.ProductItemId;
            //    item.Quantity = model.Quantity;
            //    item.SaleContractId = model.SaleContractId.GetValueOrDefault();
            //    item.StockItemId = model.StockItemId;
            //    item.UnitPrice = model.UnitPrice;
            //    item.Weight = model.Weight;
            //    item.Currency = model.Currency;
            //    item.Comments = model.Comments;
            //}

            //return item;
        }

    }
}