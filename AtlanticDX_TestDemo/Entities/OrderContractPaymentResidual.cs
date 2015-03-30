using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 余款	、设置坏账、			
    /// 余款支配订单号	、余款状态	未结清/已结清	
    /// 等等……
    /// </summary>
    public class OrderContractPaymentResidual
    {
        public int OrderContractPaymentResidualId
        {
            get;
            set;
        }

        public int OrderContractPaymentId
        {
            get;
            set;
        }
    }
}
