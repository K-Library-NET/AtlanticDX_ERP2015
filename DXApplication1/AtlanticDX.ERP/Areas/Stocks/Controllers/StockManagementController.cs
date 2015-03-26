using AtlanticDX.ERP.Areas.Stocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuShang.ERP.Entities.Stocks;

namespace AtlanticDX.ERP.Areas.Stocks.Controllers
{
    /// <summary>
    /// 库存管理
    /// </summary>
    public class StockManagementController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        // GET: Stocks/StockManagement
        public ActionResult Index()
        {
            List<StockHouseViewModel> viewModels = new List<StockHouseViewModel>();

            //按照仓库管理中的仓库分组，分组查看库存记录数据
            //目前暂定没有库存的仓库不展示
            var stockItems = db.StockItems.Where(m => m.IsAllSold == false);
            var stockItemGroups = stockItems.GroupBy(
                m => m.StoreHouseId).ToDictionary(m => m.Key);
            var storeHouseItems = db.StoreHouses.Where(m => m.IsDeleted == false
                 && stockItemGroups.ContainsKey(m.StoreHouseId));

            foreach (var storeHouseItem in storeHouseItems)
            {
                StockHouseViewModel vm = new StockHouseViewModel(storeHouseItem);
                var stockItemGroup = stockItemGroups[storeHouseItem.StoreHouseId];
                var items = stockItemGroup.ToArray();
                var itemList = from one in items select new StockItemViewModel(one);
                vm.StockItems = itemList;
                viewModels.Add(vm);
            }

            return View(viewModels);
        }
    }
}