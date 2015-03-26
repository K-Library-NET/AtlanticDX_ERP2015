using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YuShang.ERP.Entities.Sale;

namespace AtlanticDX.ERP.Areas.Orders.Models
{
    public class AddSaleBarginViewModel
    {
        public int SaleContractId
        {
            get;
            set;
        }

        public virtual ICollection<SaleBargainItem> BargainItems
        {
            get;
            set;
        }
    }
}