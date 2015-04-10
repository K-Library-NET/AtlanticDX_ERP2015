using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Configs;

namespace PrivilegeFramework
{
    public class SaleProductItemInfo
    {
        private YuShang.ERP.Entities.Sale.SaleProductItem item;

        public SaleProductItemInfo()
        {

        }

        public SaleProductItemInfo(YuShang.ERP.Entities.Sale.SaleProductItem item)
        {
            this.item = item;
            if (item != null)
            {
                this.IsEnable = true;
                this.Comments = item.Comments;
                this.Currency = item.SaleContract != null ? item.SaleContract.Currency
                    : CurrencyConfigs.SALE_DEFAULT_CURRENCY_VALUE;//.Currency; 
                this.OrderContractKey = item.OrderContractKey;
                this.ProductItemId = item.ProductItemId;
                this.SaleProductItemId = item.SaleProductItemId;
                this.ProductKey = item.ProductKey;
                this.Quantity = item.Quantity;
                this.SaleContractId = item.SaleContractId;
                this.SaleContractKey = item.SaleContractKey;
                this.StockItemId = item.StockItemId;
                this.UnitPrice = item.UnitPrice;
                this.Weight = item.Weight;
                this.SubTotal = item.SubTotal;

                YuShang.ERP.Entities.Orders.ProductItem pitem = null;

                if (item.ProductItemId.HasValue && item.OrderProductItem != null)
                {
                    pitem = item.OrderProductItem;
                }
                else if (item.ProductItemId.HasValue == false && item.StockItemId.HasValue
                   && item.StockItem != null && item.StockItem.ProductItem != null)
                {
                    pitem = item.StockItem.ProductItem;
                }

                if (pitem != null)
                {
                    this.ProductItem = new ProductItemInfo(pitem);
                    this.ProductName = pitem.ProductName;
                    this.ProductItemId = pitem.ProductItemId;
                    this.SalesGuidePrice = (pitem.Product != null) ?
                       (new Nullable<double>(pitem.Product.GuidingPrice)) : null;//.SalesGuidePrice;
                    this.Units = pitem.Units;
                    this.Quantity = pitem.Quantity; //暂时这样
                }
                //this.ProductItem = new ProductItemInfo(item.OrderProductItem);
                //this.ProductName = item.OrderProductItem.ProductName;
                //this.SalesGuidePrice = item.OrderProductItem.SalesGuidePrice;
                //this.Units = item.OrderProductItem.Units;

            }
        }

        /// <summary>
        /// 是否处于有效状态：
        /// 如果IsEnable==false，那么就是没有这一附加项
        /// </summary>
        [Display(Name = "是否处于有效状态")]
        //[Required]
        public bool? IsEnable
        {
            get;
            set;
        }

        /// <summary>
        /// 货品编号，与Product对象关联
        /// </summary>
        ////[Required]
        //[MaxLength(100)]
        [Display(Name = "货品编号")]
        public string ProductKey
        {
            get;
            set;
        }

        /// <summary>
        /// 采购合同编号
        /// </summary> 
        [Display(Name = "采购合同编号")]
        public string OrderContractKey
        {
            get;
            set;
        }

        /// <summary>
        /// 销售合同ID 
        /// </summary>
        public int? SaleContractId
        {
            get;
            set;
        }

        [Display(Name = "销售合同编号")]
        public string SaleContractKey
        {
            get;
            set;
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
        /// 货品名称
        /// </summary> 
        [Display(Name = "货品名称")]
        public string ProductName
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
        /// 单位：重量单位或计量单位？
        /// </summary>
        [Display(Name = "单位")]
        public string Units
        {
            get;
            set;
        }

        /// <summary>
        /// 指导销售价
        /// </summary>
        [Display(Name = "指导销售价")]
        public double? SalesGuidePrice { get; set; }

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
        /// 货款小计
        /// </summary>
        [Display(Name = "货款小计")]
        public double? SubTotal
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
        /// 发货状态
        /// </summary>
        [Display(Name = "发货状态")]
        public YuShang.ERP.Entities.Sale.SaleShipmentStatus ShipmentStatus { get; set; }

        public ProductItemInfo ProductItem { get; set; }

        internal static void AssignValues(SaleProductItemInfo source,
            YuShang.ERP.Entities.Sale.SaleProductItem target)
        {
            target.Comments = source.Comments;
            target.ShipmentStatus = source.ShipmentStatus;
            //target.Currency = source.Currency;
            target.ProductItemId = source.ProductItemId;
            target.Quantity = source.Quantity;
            target.SaleContractId = source.SaleContractId.GetValueOrDefault();
            target.StockItemId = source.StockItemId;
            target.UnitPrice = source.UnitPrice;
            target.Weight = source.Weight;
        }

        /// <summary>
        /// 销售商品
        /// </summary>
        [Display(Name = "销售商品")]
        public int? SaleProductItemId { get; set; }
    }
}
