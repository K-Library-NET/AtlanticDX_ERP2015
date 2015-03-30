using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Stocks
{
    public enum StockStatus
    {
        [System.ComponentModel.DataAnnotations.Display(Name = "库存中")]
        InStock = 0,
        [System.ComponentModel.DataAnnotations.Display(Name = "待销售出仓")]
        InStockSelling = 1,
        [System.ComponentModel.DataAnnotations.Display(Name = "已售")]
        IsSold = -1,
    }
}
