using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 香港物流Item
    /// </summary>
    public class HongkongLogisticsItem : LogisticsProductItem
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int HongkongLogisticsItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 香港物流编号
        /// </summary>
        public string HongkongLogisticsKey { get; set; }

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
        /// 运费小计
        /// </summary>
        public override double SubTotal
        {
            get
            {
                var temp = (this.FreightCharges * this.ContractWeight) - this.Compensation;
                return temp;
            }
        }
    }
}
