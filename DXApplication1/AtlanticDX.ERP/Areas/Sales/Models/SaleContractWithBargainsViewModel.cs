using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YuShang.ERP.Entities.Sale;

namespace AtlanticDX.ERP.Areas.Sales.Models
{
    public class SaleContractWithBargainsViewModel
    {
        public SaleContract SaleContract
        {
            get;
            set;
        }

        public int? SaleBargainsCount
        {
            get;
            set;
        }

        public SaleBargain[] SaleBargains
        {
            get;
            set;
        }

        public int? SelectedSaleBargainId
        {
            get;
            set;
        }
    }
}