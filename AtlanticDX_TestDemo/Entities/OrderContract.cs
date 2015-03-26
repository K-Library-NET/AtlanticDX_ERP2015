using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 采购订单合同
    /// </summary>
    public class OrderContract
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int OrderContractId
        {
            get;
            set;
        }

        /// <summary>
        /// 订单号（人工编写，默认全部大写字母加数字）
        /// </summary>
        public string OrderContractKey
        {
            get;
            set;
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public ContractStatusType ContractStatus { get; set; }

        /// <summary>
        /// 期货销售、现货销售（0是期货销售，1是现货销售）
        /// </summary>
        public int OrderType
        {
            get;
            set;
        }

        public List<ProductItem> OrderProducts
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商（从资源管理里面选）
        /// </summary>
        public string VendorKey { get; set; }

        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime OrderCreateTime { get; set; }

        /// <summary>
        /// 预计到港时间？
        /// </summary>
        public DateTime ETA { get; set; }

        /// <summary>
        /// 预计发货时间？
        /// </summary>
        public DateTime ETD { get; set; }

        /// <summary>
        /// 订单船期（直接手工填写）
        /// </summary>
        public string ShipmentPeriod { get; set; }

        /// <summary>
        /// 柜号（手工填写）
        /// </summary>
        public string ContainerSerial { get; set; }

        /// <summary>
        /// 提货单号（手工填写）
        /// </summary>
        public string DeliveryBillSerial { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        public string DestinationHarborKey { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public string Payment { get; set; }

        /// <summary>
        /// 采购员
        /// </summary>
        public string OrderSysUserKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("合同号：{0}\t", this.OrderContractKey);
            builder.AppendFormat("合同状态：{0}\t", this.ContractStatus);
            builder.AppendFormat("ETA：{0}\t", this.ETA);
            builder.AppendFormat("ETD：{0}\t", this.ETD);
            builder.AppendFormat("期货/现货：{0}\t", this.OrderType == 1?"现货":"期货");
            //builder.AppendFormat("合同号：{0}\t", this.OrderContractKey);
            //builder.AppendFormat("合同号：{0}\t", this.OrderContractKey);

            return builder.ToString();
        }
    }
}
