using System.Collections.Generic;
using System.Linq;

namespace AtlanticDX.Model.Areas.Sales.Models
{
    public class SaleProductItemBargainsViewModel
    {
        public SaleProductItemBargainsViewModel(
            IEnumerable<SaleProductItemBargainItemViewModel> items)
        {
            if (items != null && items.Count() > 0)
            {
                this.Items = new List<SaleProductItemBargainItemViewModel>(
                    items.OrderByDescending(item => item.BargainUnitPrice));
            }
        }

        public int SaleProductItemId
        {
            get;
            set;
        }

        public ICollection<SaleProductItemBargainItemViewModel> Items
        {
            get;
            set;
        }
    }
}