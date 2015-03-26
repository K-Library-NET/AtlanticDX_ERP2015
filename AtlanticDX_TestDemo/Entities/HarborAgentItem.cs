using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 港口代理Item
    /// </summary>
    public class HarborAgentItem
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int HarborAgentItemId { get; set; }

        /// <summary>
        /// 采购合同
        /// </summary>
        public string OrderContractKey { get; set; }

        /*
         *码头费	代理费	关税	    反倾销税	增值税	其他费用	备注	综合费用
          1200	1000	32111	39847	84759	1		    158918 
         */

        /// <summary>
        /// 码头费
        /// </summary>
        public double HarborCost { get; set; }

        /// <summary>
        /// 代理费
        /// </summary>
        public double AgentCost { get; set; }

        /// <summary>
        /// 关税
        /// </summary>
        public double Tariff { get; set; }

        /// <summary>
        /// 反倾销税
        /// </summary>
        public double AntiDumpingTax { get; set; }

        /// <summary>
        /// 增值税
        /// </summary>
        public double ValueAddedTax { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>
        public double OthersCost { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 综合费用
        /// 自动计算公式：码头费、代理费、关税、反倾销税、增值税、其他费用总和. 或自定义填入数值.
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// 支付费用，可分批次支付
        /// </summary>
        public IEnumerable<PayItem> CostPayItems
        { get; set; }
    }
}
