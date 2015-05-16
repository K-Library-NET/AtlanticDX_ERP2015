using System.Collections.Generic;
using YuShang.ERP.Entities.Sale;

namespace AtlanticDX.Model.Areas.Orders.Models
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