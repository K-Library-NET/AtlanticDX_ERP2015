using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YuShang.ERP.Entities.Stocks;

namespace AtlanticDX.ERP.Areas.Stocks.Models
{
    /// <summary>
    /// 入仓记录展示Model
    /// </summary>
    public class StockItemViewModel
    {
        public StockItemViewModel()
        {

        }

        public StockItemViewModel(StockItem stockItem)
        {
            this.StockItem = stockItem;
        }

        public StockItem StockItem { get; set; }
    }
}