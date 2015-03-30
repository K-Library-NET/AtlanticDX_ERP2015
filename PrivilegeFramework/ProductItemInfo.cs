using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.ResMgr;

namespace PrivilegeFramework
{
    public class ProductItemInfo
    {
        private ProductItem item;

        public ProductItemInfo()
        {

        }

        public ProductItemInfo(ProductItem item)
        {
            this.item = item;
            if (item != null)
            {
                this.IsEnable = true;

                AssignValues(item, this);
            }
        }

        public static void AssignValues(ProductItem source, ProductItemInfo target)
        {
            target.Currency = source.Currency;
            target.Comments = source.Comments;
            target.ImportStatus = source.Status;
            target.NetWeight = source.NetWeight.GetValueOrDefault();
            target.OrderContractId = source.OrderContractId;
            target.OrderContractKey = source.OrderContractKey;
            target.ProductId = source.ProductId;
            target.ProductItemId = source.ProductItemId;
            target.ProductName = source.ProductName;
            target.ProductKey = source.ProductKey;
            target.Quantity = source.Quantity;
            target.ReceiveTime = source.ReceiveTime;
            target.SalesGuidePrice = source.SalesGuidePrice.GetValueOrDefault();
            target.UnitPrice = source.UnitPrice;
            target.Units = source.Units;

            target.Product = new ProductViewModel(source.Product);
        }

        /// <summary>
        /// 是否处于有效状态：
        /// 如果IsEnable==false，那么就是没有这一附加项
        /// </summary>
        [Display(Name = "是否处于有效状态")]
        ////[Required]
        public bool? IsEnable
        {
            get;
            set;
        }

        [Display(Name = "货品ID")]
        //[Required]
        public int? ProductId { get; set; }

        public int? ProductItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 货品出厂编号
        /// </summary> 
        [Display(Name = "货品出厂编号")]
        public string ProductKey
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
        /// 单价（手工填写）
        /// </summary>
        [Display(Name = "单价")]
        public double? UnitPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 净重（手工填写）
        /// </summary>
        [Display(Name = "净重")]
        public double? NetWeight
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

        /// <summary>
        /// 指导销售价
        /// </summary>
        [Display(Name = "指导销售价")]
        public double? SalesGuidePrice { get; set; }

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
        /// 关联的采购合同ID
        /// </summary>
        public int? OrderContractId
        {
            get;
            set;
        }

        /// <summary>
        /// 采购状态：未收货/已收货
        /// </summary>
        [Display(Name = "采购状态")]
        public ProductItemStatus? ImportStatus
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

        public static void AssignValues(ProductItemInfo source, ProductItem target)
        {
            target.ProductId = source.ProductId.GetValueOrDefault();
            target.Currency = source.Currency;
            target.NetWeight = source.NetWeight;
            target.Comments = source.Comments;
            target.ReceiveTime = source.ReceiveTime;
            target.SalesGuidePrice = source.SalesGuidePrice;
            target.Status = source.ImportStatus.GetValueOrDefault();//.Status;
            target.Quantity = source.Quantity;
            target.UnitPrice = source.UnitPrice;
            target.Units = source.Units;
        }

        [Display(Name = "货款小计")]
        public double? SubTotal
        {
            get
            {
                if (this.NetWeight.HasValue && this.UnitPrice.HasValue)
                {
                    return Math.Round(
                        (this.NetWeight.HasValue ? this.NetWeight.Value : 0) *
                        (this.UnitPrice.HasValue ? this.UnitPrice.Value : 0), 2);
                }
                return null;
            }
        }

        /// <summary>
        /// 商品
        /// </summary>
        [Display(Name = "商品")]
        public ProductViewModel Product
        {
            get;
            set;
        }

        public string ProductFullName
        {
            get
            {
                if (Product != null)
                    return this.Product.ProductFullName;
                return ProductName;
            }
        }
    }

    public class ProductViewModel
    {
        private Product item;

        /// <summary>
        /// 是否处于有效状态：
        /// 如果IsEnable==false，那么就是没有这一附加项
        /// </summary>
        [Display(Name = "是否处于有效状态")]
        ////[Required]
        public bool? IsEnable
        {
            get;
            set;
        }

        public ProductViewModel(Product item)
        {
            this.item = item;
            if (item != null)
            {
                this.IsEnable = true;

                AssignValues(item, this);
            }
        }

        internal static void AssignValues(Product source, ProductViewModel target)
        {
            target.Brand = source.Brand;
            target.Comments = source.Comments;
            target.Grade = source.Grade;
            target.MadeInCountry = source.MadeInCountry;
            target.MadeInFactory = source.MadeInFactory;
            target.Packing = source.Packing;
            //target.ProductFullName = source.ProductFullName;
            target.ProductId = source.ProductId;
            target.ProductKey = source.ProductKey;
            target.ProductName = source.ProductName;
            target.ProductNameENG = source.ProductNameENG;
            target.ProductType = source.ProductType;
            target.Specification = source.Specification;
            target.Units = source.Units;
            target.UnitsPerMonth = source.UnitsPerMonth;
        }


        [Display(Name = "货品")]
        public int? ProductId
        {
            get;
            set;
        }

        /// <summary>
        /// 货品编号
        /// </summary> 
        [MaxLength(100)]
        [Display(Name = "货品出厂编号")]
        public string ProductKey
        {
            get;
            set;
        }

        /// <summary>
        /// 货品名
        /// </summary>
        //[Required]
        [MaxLength(100)]
        [Display(Name = "货品名（中）")]
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 货品名（英）
        /// </summary>
        //[Required]
        [MaxLength(100)]
        [Display(Name = "货品名（英）")]
        public string ProductNameENG
        {
            get;
            set;
        }

        /// <summary>
        /// 商品类别
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "商品类别")]
        public string ProductType
        {
            get;
            set;
        }

        /// <summary>
        /// 单位：重量单位或计量单位？
        /// </summary>
        [Display(Name = "单位")]
        [MaxLength(100)]
        public string Units
        {
            get;
            set;
        }

        /// <summary>
        /// 国家/厂号/货品名/品牌
        /// </summary>
        [NotMapped]
        [Display(Name = "国家/厂号/货品名/品牌")]
        public string ProductFullName
        {
            get
            {
                return string.Format("{0} {1} {2} {3}",
                    this.MadeInCountry, this.MadeInFactory,
                    this.ProductName, this.Brand);
            }
        }

        /// <summary>
        /// 国家
        /// </summary>
        [Display(Name = "国家")]
        [MaxLength(100)]
        public string MadeInCountry { get; set; }

        /// <summary>
        /// 厂号
        /// </summary>
        [Display(Name = "厂号")]
        [MaxLength(100)]
        public string MadeInFactory { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [Display(Name = "品牌")]
        [MaxLength(100)]
        public string Brand { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [Display(Name = "级别")]
        [MaxLength(100)]
        public string Grade { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [Display(Name = "规格")]
        [MaxLength(100)]
        public string Specification
        {
            get;
            set;
        }

        /// <summary>
        /// 包装
        /// </summary>
        [Display(Name = "包装")]
        [MaxLength(100)]
        public string Packing { get; set; }

        /// <summary>
        /// 月生产量（吨）
        /// </summary>
        [Display(Name = "月生产量（吨）")]
        [MaxLength(100)]
        public string UnitsPerMonth { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(200)]
        public string Comments { get; set; }
    }
}
