using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YuShang.ERP.Entities.Orders;

namespace AtlanticDX.ERP.Areas.Orders.Models
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