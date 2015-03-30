using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 香港物流（资源管理对象）
    /// </summary>
    public class HongkongLogistics
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public int HongkongLogisticsId { get; set; }

        /// <summary>
        /// 香港物流编号
        /// </summary>
        public string HongkongLogisticsKey { get; set; }

        /// <summary>
        /// 香港物流名称
        /// </summary>
        public string HongkongLogisticsName { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 支付费用，可分批次支付
        /// </summary>
        public List<PayItem> CostPayItems
        { get; set; }

        /// <summary>
        /// 确认/同意支付货款
        /// </summary>
        public bool CommitToPayCost { get; set; }
    }
}
