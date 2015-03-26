using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 销售合同发货
    /// </summary>
    public class SaleContractShipment
    {
        public int SaleContractShipmentId
        {
            get;
            set;
        }

        /// <summary>
        /// 销售合同发货单号
        /// </summary>
        public string SaleContractSN
        {
            get;
            set;
        }
    }
}
