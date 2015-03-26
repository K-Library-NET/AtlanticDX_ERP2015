using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Orders
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
        [Display(Name = "货品编号")]
        [NotMapped]
        public string ProductKey
        {
            get
            {
                if (this.Product != null)
                    return this.Product.ProductKey;

                return string.Empty;
            }
        }

        /// <summary>
        /// 货品名称
        /// </summary> 
        [Display(Name = "货品名称")]
        [NotMapped]
        public string ProductName
        {
            get
            {
                if (this.Product != null)
                    return this.Product.ProductFullName;

                return string.Empty;
            }
        }

        [Display(Name = "货品")]
        [Required]
        [Index("IX_ProductItem_ProductId", IsClustered = false)]
        public int ProductId
        {
            get;
            set;
        }

        /// <summary>
        /// 商品（资源管理）
        /// Navigation Property
        /// </summary>
        public virtual YuShang.ERP.Entities.ResMgr.Product Product
        {
            get;
            set;
        }

        /// <summary>
        /// 采购合同编号
        /// </summary>
        //[MaxLength(100)]
        [NotMapped]
        [Display(Name = "采购合同编号")]
        public string OrderContractKey
        {
            get
            {
                if (this.OrderContract != null)
                    return this.OrderContract.OrderContractKey;
                return string.Empty;
            }
        }

        /// <summary>
        /// 关联的采购合同ID
        /// </summary>
        [Display(Name = "采购合同")]
        [Required]
        [Index("IX_ProductItem_OrderId", IsClustered = false)]
        public int OrderContractId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property
        /// </summary>
        [JsonIgnore]
        public virtual OrderContract OrderContract
        {
            get;
            set;
        }

        /// <summary>
        /// 货品量（手工填写）
        /// </summary>
        [Display(Name = "货品量")]
        //[Required]
        public double? Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// 净重（手工填写）
        /// </summary>
        [Display(Name = "净重")]
        //[Required]
        public double? NetWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 单价（手工填写）
        /// </summary>
        [Display(Name = "单价")]
        //[Required]
        public double? UnitPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 单位：重量单位或计量单位？
        /// </summary>
        [Display(Name = "单位")]
        //[Required]
        [MaxLength(100)]
        public string Units
        {
            get;
            set;
        }

        /// <summary>
        /// 货币。这里如果不是CNY，则后面可能需要把所有的都转变成CNY计算
        /// </summary>
        [Display(Name = "货币")]
        [MaxLength(100)]
        public string Currency
        {
            get;
            set;
        }

        //[Required]
        [Display(Name = "指导销售价")]
        public double? SalesGuidePrice { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public ProductItemStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 收货日期
        /// </summary>
        [Display(Name = "收货日期")]
        public DateTime? ReceiveTime
        {
            get;
            set;
        }


        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Comments
        {
            get;
            set;
        }

        /// <summary>
        /// 货款小计
        /// </summary>
        [NotMapped]
        public double SubTotal
        {
            get
            {
                return Math.Round(
                    (this.NetWeight.HasValue ? this.NetWeight.Value : 0) *
                    (this.UnitPrice.HasValue ? this.UnitPrice.Value : 0), 2);
            }
        }

        ///// <summary>
        ///// 自增ID
        ///// </summary>
        //public int? OrderClaimCompensationItemId
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 采购索赔项
        /// Navigation property
        /// </summary> 
        public virtual OrderClaimCompensationItem OrderClaimCompensationItem
        {
            get;
            set;
            //get
            //{
            //    if (this.OrderContract != null && this.OrderContract.ClaimCompensation != null
            //        && this.OrderContract.ClaimCompensation.OrderClaimCompensationItems != null
            //        && this.OrderContract.ClaimCompensation.OrderClaimCompensationItems.Count() > 0)
            //    {
            //        return this.OrderContract.ClaimCompensation
            //            .OrderClaimCompensationItems.FirstOrDefault(
            //            p => p.ProductItemId == this.ProductItemId);
            //    }
            //    return null;
            //}
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

    public enum ProductItemStatus
    {
        NotReceived,
        Received
    }
}
