using YuShang.ERP.Entities.Orders;

namespace AtlanticDX.Model.Areas.Orders.Models
{
    public class AuditSaleContractViewModel
    {
        public int SaleContractId
        {
            get;
            set;
        }

        public int SelectedSaleBargainId
        {
            get;
            set;
        }

        public ContractStatus ContractStatus { get; set; }
    }
}