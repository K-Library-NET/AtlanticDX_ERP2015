using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Orders
{
    public abstract class LogisticsProductItem
    {
        //public int LogisticsProductItemId
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 采购商品项ID
        /// used by Navigation Property
        /// </summary>
        public int ProductItemId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigaion Property
        /// </summary>
        public virtual ProductItem ProductItem
        {
            get;
            set;
        }

        /// <summary>
        /// 货品编号
        /// </summary>
        [Display(Name = "货品出厂编号")]
        [NotMapped]
        public string ProductKey
        {
            get
            {
                if (ProductItem != null)
                {
                    return ProductItem.ProductKey;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 收单件数
        /// </summary>
        [Display(Name = "收单件数")]
        public double? ContractQuantity { get; set; }

        /// <summary>
        /// 收单吨重
        /// </summary>
        [Display(Name = "收单吨重")]
        public double? ContractWeight { get; set; }

        /// <summary>
        /// 运费/吨
        /// </summary>
        [Display(Name = "运费/吨")]
        public double? FreightCharges { get; set; }

        /// <summary>
        /// 保险
        /// </summary>
        [Display(Name = "保险")]
        public double? Insurance { get; set; }

        /// <summary>
        /// 收货日期
        /// </summary>
        [Display(Name = "收货日期")]
        public DateTime? ReceivingTime { get; set; }

        /// <summary>
        /// 收货件数
        /// </summary>
        [Display(Name = "收货件数")]
        public double? ReceivingQuantity { get; set; }

        /// <summary>
        /// 收货吨重
        /// </summary>
        [Display(Name = "收货吨重")]
        public double? ReceivingWeight { get; set; }

        /// <summary>
        /// 运费小计
        /// </summary>
        [NotMapped]
        [Display(Name = "运费小计")]
        public virtual double SubTotal
        {
            get
            {
                var temp = (this.FreightCharges.HasValue ? this.FreightCharges.Value : 0)
                    * (this.ContractWeight.HasValue ? this.ContractWeight.Value : 0);// -this.Compensation;
                return temp;
            }
        }

        //[NotMapped]
        //[Display(Name = "索赔费用")]
        //public double Compensation
        //{
        //    get
        //    {
        //        if (this.ProductItem != null &&
        //            this.ProductItem.OrderClaimCompensationItem != null)
        //        {
        //            return this.ProductItem.OrderClaimCompensationItem.Compensation;
        //        }

        //        return 0;
        //    }
        //}
    }

}
