using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    public class MainlandLogisticsItem : LogisticsProductItem
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int MainlandLogisticsItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 香港物流编号
        /// </summary>
        public string MainlandLogisticsKey { get; set; }

        /// <summary>
        /// 采购合同
        /// </summary>
        public string OrderContractKey { get; set; }

        /// <summary>
        /// 运费总计
        /// </summary>
        public double CostTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 确认支付运费
        /// </summary>
        public int AcceptToPayCostStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 支付费用，可分批次支付
        /// </summary>
        public IEnumerable<PayItem> CostPayItems
        { get; set; }
    }
}
