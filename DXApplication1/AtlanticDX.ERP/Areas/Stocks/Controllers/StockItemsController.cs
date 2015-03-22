using AtlanticDX.ERP.Areas.Stocks.Models;
using PrivilegeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Stocks.Controllers
{
    /// <summary>
    /// 现货库存
    /// </summary>
    [ComplexAuthorize]
    public class StockItemsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        // GET: Stocks/StockItems
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10, string filterValue = "")
        {
            DateTime start = DateTime.Now;
            List<StockHouseViewModel> viewModels = new List<StockHouseViewModel>();

            var query = db.StoreHouses.Join(db.StockItems.Include("ProductItem"),
                 st => st.StoreHouseId, si => si.StoreHouseId,
                 (StoreHouse, StockItem) => new { StoreHouse, StockItem })
                 .Where(m => m.StockItem.IsAllSold == false && m.StoreHouse.IsDeleted == false).GroupBy(
                    m => m.StoreHouse).OrderBy(m => m.Key.StoreHouseName);

            DateTime step1 = DateTime.Now;

            System.Diagnostics.Debug.WriteLine("stockItems Index step1: " + step1.Subtract(start).TotalMilliseconds);

            var array = query.ToArray();

            foreach (var gp in array)
            {
                StockHouseViewModel vm = new StockHouseViewModel(gp.Key);
                var stockItemGroup = gp.ToArray();
                var itemList = from one in stockItemGroup
                               select new StockItemViewModel(one.StockItem);
                vm.StockItems = itemList;
                viewModels.Add(vm);
            }

            System.Diagnostics.Debug.WriteLine("stockItems Index step2: " + DateTime.Now.Subtract(start).TotalMilliseconds);

            return Json(new { total = viewModels.Count, rows = viewModels });


            System.Diagnostics.Debug.WriteLine("stockItems Index step2: " + DateTime.Now.Subtract(start).TotalMilliseconds);

            ////按照仓库管理中的仓库分组，分组查看库存记录数据
            ////目前暂定没有库存的仓库不展示
            //var stockItems = db.StockItems.Where(m => m.IsSold == false);
            //var stockItemGroups = stockItems.GroupBy(
            //    m => m.StoreHouseId).ToDictionary(m => m.Key);
            //var storeHouseItems = db.StoreHouses.Where(m => m.IsDeleted == false
            //     && stockItemGroups.ContainsKey(m.StoreHouseId));

            //foreach (var storeHouseItem in storeHouseItems)
            //{
            //    StockHouseViewModel vm = new StockHouseViewModel(storeHouseItem);
            //    var stockItemGroup = stockItemGroups[storeHouseItem.StoreHouseId];
            //    var items = stockItemGroup.ToArray();
            //    var itemList = from one in items select new StockItemViewModel(one);
            //    vm.StockItems = itemList;
            //    viewModels.Add(vm);
            //}
            // return Json(viewModels);
        }


    }
}