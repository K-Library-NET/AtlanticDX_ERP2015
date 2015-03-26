using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 货品项
    /// </summary>
    public class ProductItem
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ProductItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 货品编号
        /// </summary>
        public string ProductKey
        {
            get;
            set;
        }

        /// <summary>
        /// 采购合同编号
        /// </summary>
        public string OrderContractKey
        {
            get;
            set;
        }

        /// <summary>
        /// 货品量（手工填写）
        /// </summary>
        public double Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// 净重（手工填写）
        /// </summary>
        public double NetWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 单价（手工填写）
        /// </summary>
        public double UnitPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 货币。这里如果不是CNY，则后面可能需要把所有的都转变成CNY计算
        /// </summary>
        public string Currency
        {
            get;
            set;
        }

        /// <summary>
        /// 货款小计
        /// </summary>
        public double SubTotal
        {
            get
            {
                return
                    Math.Round(
                    this.NetWeight * this.UnitPrice, 2);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("ProductKey:{0}\t", this.ProductKey);
            builder.AppendFormat("件数:{0}\t", this.Quantity);
            builder.AppendFormat("净重:{0}\t", this.NetWeight);
            builder.AppendFormat("单价:{0}\t", this.UnitPrice);

            return builder.ToString();
        }
    }
}
