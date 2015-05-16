using System.Linq;
using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.Public.Controllers
{
    public class DataController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext dxContext = new AtlanticDXContext();
        /// <summary>
        /// 获取商品数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Products(int page = 1, int rows = 10, string filterValue = "")
        {
            var query = dxContext.Products.Where(m => m.IsDeleted == false);
            if (!string.IsNullOrEmpty(filterValue))
            {
                query = query.Where(m => m.ProductKey.Contains(filterValue) || m.ProductName.Contains(filterValue) || m.Specification.Contains(filterValue));
            }
            int total = query.Count();
            query = query.OrderBy(m => m.ProductId)
                .Skip((page - 1) * rows)
                .Take(rows);
            return Json(new { total = total, rows = query.ToList() });
        }

        /// <summary>
        /// 期货销售选择商品
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="filterValue">请输入商品编号或名称或型号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ProductItemProducts(int page = 1, int rows = 10, string filterValue = "")
        {
            //fixed? 期货销售选择商品
            var query = dxContext.ProductItems.AsEnumerable();

            int total = query.Count();
            query = query.OrderBy(m => m.ProductName).ThenBy(m => m.ReceiveTime).Skip((page - 1) * rows).Take(rows);
            return Json(new { total = total, rows = query.ToList() });
        }

        /// <summary>
        /// 现货销售选择商品
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="filterValue">请输入商品编号或名称或型号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult StockItemProducts(int page = 1, int rows = 10, string filterValue = "")
        {
            //fixed? 现货销售选择商品
            var query = dxContext.StockItems.AsEnumerable();

            int total = query.Count();
            query = query.OrderBy(m => m.ProductName).ThenBy(m => m.StockInDate).Skip((page - 1) * rows).Take(rows);
            return Json(new { total = total, rows = query.ToList() });
        }

        /// <summary>
        /// 销售客户
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="filterValue">名称/公司名/联系人/电话</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaleClients(int page = 1, int rows = 10, string filterValue = "")
        {
            var query = dxContext.SaleClients.Where(m => m.IsDeleted == false);
            if (!string.IsNullOrEmpty(filterValue))
            {
                query = query.Where(m => m.Name.Contains(filterValue) || m.CompanyName.Contains(filterValue) || m.MobilePhone.Contains(filterValue));
            }
            int total = query.Count();
            query = query.OrderBy(m => m.SaleClientId)
                .Skip((page - 1) * rows)
                .Take(rows);
            return Json(new { total = total, rows = query.ToList() });
        }

        public JsonResult ProductDetail(int productId)
        {
            var data = dxContext.Products.Where(m => m.ProductId == productId).SingleOrDefault();
            return Json(data);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            dxContext.Dispose();
        }
    }
}