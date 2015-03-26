using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.Stocks;

namespace YuShang.ERP.Entities.Sale
{
    ///<summary>
    ///销售合同中的货品
    ///</summary>
    public class SaleProductItem
    {
        public int SaleProductItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 货品编号，与Product对象关联
        /// </summary>
        //[Required]
        //[MaxLength(100)]
        [Display(Name = "货品编号")]
        [NotMapped]
        public string ProductKey
        {
            get
            {
                if (this.SaleContract != null)
                {
                    if (this.SaleContract.OrderType == 1
                        && this.StockItem != null)
                    {//现货销售
                        return this.StockItem.ProductKey;
                    }
                    else if (this.SaleContract.OrderType == 0
                        && this.OrderProductItem != null)
                    {//期货销售
                        return this.OrderProductItem.ProductKey;
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// 采购合同编号
        /// </summary>
        [NotMapped]
        [Display(Name = "采购合同编号")]
        public string OrderContractKey
        {
            get
            {
                if (this.SaleContract != null)
                {
                    if (this.SaleContract.OrderType == 1
                        && this.StockItem != null)
                    {//现货销售
                        return this.StockItem.OrderContractKey;
                    }
                    else if (this.SaleContract.OrderType == 0
                        && this.OrderProductItem != null)
                    {//期货销售
                        return this.OrderProductItem.OrderContractKey;
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// 销售合同ID 
        /// </summary>
        public int SaleContractId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property
        /// </summary>
        [JsonIgnore] //断开json looping
        public virtual SaleContract SaleContract
        {
            get;
            set;
        }

        [NotMapped]
        [Display(Name = "销售合同编号")]
        public string SaleContractKey
        {
            get
            {
                if (this.SaleContract != null)
                    return this.SaleContract.SaleContractKey;

                return string.Empty;
            }
        }

        /// <summary>
        /// 对应的库存项ID（现货销售才有，期货销售没有！）
        /// Used by Navigation Property
        /// </summary>
        [Display(Name = "对应的库存项ID")]
        public int? StockItemId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual StockItem StockItem
        {
            get;
            set;
        }

        /// <summary>
        /// 对应的采购货品项ID（期货销售才有，现货销售没有！）
        /// Used by Navigation Property
        /// </summary>
        [Display(Name = "对应的采购货品项ID")]
        public int? ProductItemId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual ProductItem OrderProductItem
        {
            get;
            set;
        }

        /// <summary>
        /// 货品量（手工填写）
        /// </summary>
        [Display(Name = "货品量")]
        public double? Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// 销售吨重（手工填写）
        /// </summary>
        [Display(Name = "销售吨重")]
        public double? Weight
        {
            get;
            set;
        }

        /// <summary>
        /// 销售单价（手工填写）
        /// </summary>
        [Display(Name = "销售单价")]
        public double? UnitPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 货币，空值则是CNY人民币。
        /// 这里如果不是CNY，则后面可能需要把所有的都转变成CNY计算
        /// </summary>
        [Display(Name = "货币")]
        public string Currency
        {
            get;
            set;
        }

        /// <summary>
        /// 发货状态
        /// </summary>
        [Display(Name = "发货状态")]
        public SaleShipmentStatus ShipmentStatus
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
                return Math.Round(this.Weight.GetValueOrDefault()
                    * this.UnitPrice.GetValueOrDefault(), 2);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("ProductKey:{0}\t", this.ProductKey);
            builder.AppendFormat("销售件数:{0}\t", this.Quantity);
            builder.AppendFormat("销售吨重:{0}\t", this.Weight);
            builder.AppendFormat("销售单价:{0}\t", this.UnitPrice);

            return builder.ToString();
        }

        public int? SaleClaimCompensationItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 销售索赔项
        /// </summary>
        public virtual SaleClaimCompensationItem SaleClaimCompensationItem
        {
            get;
            set;
        } 

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Comments { get; set; }
    }

    public enum SaleShipmentStatus
    {
        /// <summary>
        /// 未发货
        /// </summary>
        [Display(Name = "未发货")]
        NonShipment = 0,
        /// <summary>
        /// 已发货（在途）
        /// </summary>
        [Display(Name = "已发货（在途）")]
        OnShipping,
        /// <summary>
        /// 客户确认收货
        /// </summary>
        [Display(Name = "客户确认收货")]
        ClientReceived,
        /// <summary>
        /// 客户确认收货并且拒收（要求退货）
        /// </summary>
        [Display(Name = "客户确认收货并且拒收（要求退货）")]
        ClientReceivedAndRejected,
        /// <summary>
        /// 客户退货过程中（返程在途）
        /// </summary>
        [Display(Name = "客户退货过程中（返程在途）")]
        ClientReceivedAndRejectedBacking,
        /// <summary>
        /// 货品被退回并且入仓
        /// </summary>
        [Display(Name = "货品被退回并且入仓")]
        BackedAndInStock,
    }
}
