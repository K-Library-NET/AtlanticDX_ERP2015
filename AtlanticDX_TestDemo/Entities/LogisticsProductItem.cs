using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    public class LogisticsProductItem
    {
        /// <summary>
        /// 货品编号
        /// </summary>
        public string ProductKey
        {
            get;
            set;
        }

        /// <summary>
        /// 收单件数
        /// </summary>
        public double ContractQuantity { get; set; }

        /// <summary>
        /// 收单吨重
        /// </summary>
        public double ContractWeight { get; set; }

        /// <summary>
        /// 运费/吨
        /// </summary>
        public double FreightCharges { get; set; }

        /// <summary>
        /// 保险
        /// </summary>
        public double Insurance { get; set; }

        /// <summary>
        /// 货运索赔
        /// </summary>
        public double Compensation { get; set; }

        /// <summary>
        /// 索赔原因
        /// </summary>
        public string CompensationReason { get; set; }

        /// <summary>
        /// 收货日期
        /// </summary>
        public DateTime ReceivingTime { get; set; }

        /// <summary>
        /// 收货件数
        /// </summary>
        public double ReceivingQuantity { get; set; }

        /// <summary>
        /// 收货吨重
        /// </summary>
        public double ReceivingWeight { get; set; }

        /// <summary>
        /// 运费小计
        /// </summary>
        public virtual double SubTotal
        {
            get
            {
                var temp = (this.FreightCharges * this.ContractWeight);// -this.Compensation;
                return temp;
            }
        }
    }
}
