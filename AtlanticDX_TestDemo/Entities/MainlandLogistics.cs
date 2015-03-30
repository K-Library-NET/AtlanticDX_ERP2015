using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 内地物流（资源管理对象）
    /// </summary> 
    public class MainlandLogistics
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public int MainlandLogisticsId { get; set; }

        /// <summary>
        /// 内地物流编号
        /// </summary>
        public string MainlandLogisticsKey { get; set; }

        /// <summary>
        /// 内地物流名称
        /// </summary>
        public string MainlandLogisticsName { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 支付费用，可分批次支付
        /// </summary>
        public List<PayItem> CostPayItems
        { get; set; }

        public bool CommitToPayCost { get; set; }
    }
}
