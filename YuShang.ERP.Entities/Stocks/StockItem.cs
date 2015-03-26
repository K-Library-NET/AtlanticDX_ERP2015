using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Orders;

namespace YuShang.ERP.Entities.Stocks
{
    /// <summary>
    /// 入仓库存项，跟货品项还不完全相同
    /// </summary> 
    public class StockItem
    {
        public int StockItemId
        {
            get;
            set;
        }

        public int ProductItemId
        {
            get;
            set;
        }

        public virtual ProductItem ProductItem
        {
            get;
            set;
        }

        /// <summary>
        /// 货品出厂编号，与Product对象关联
        /// </summary>
        [Display(Name = "货品出厂编号")]
        [NotMapped]
        public string ProductKey
        {
            get
            {
                if (this.ProductItem != null)
                    return this.ProductItem.ProductKey;

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
                if (this.ProductItem != null)
                    return this.ProductItem.ProductName;

                return string.Empty;
            }
        }

        [Required]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = false)]
        [Display(Name = "仓库Id")]
        public int StoreHouseId
        {
            get;
            set;
        }

        /// <summary>
        /// 仓库
        /// Navigation Property
        /// </summary>
        public virtual ResMgr.StoreHouse StoreHouse { get; set; }

        /// <summary>
        /// 仓卡号
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "仓卡号")]
        public string StoreHouseMountNumber
        {
            get;
            set;
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
                if (this.ProductItem != null)
                    return this.ProductItem.OrderContractKey;
                return string.Empty;
            }
        }

        /// <summary>
        /// 件数（手工填写）
        /// </summary>
        [Display(Name = "件数")]
        //[Required]
        public double? Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// 入仓吨数（手工填写，在入仓过程产生）
        /// </summary>
        [Display(Name = "入仓吨数")]
        //[Required]
        public double? StockWeight
        {
            get;
            set;
        }

        ///// <summary>
        ///// 库存状态：0,未到港；1,已到港未进内地（香港物流）；
        ///// 2,已经进入内地（内地物流）；3,已经入仓（入仓后）；
        ///// </summary>
        //[Display(Name = "库存状态")]
        //public int InventoryStatus
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 入仓日期
        /// </summary>
        [Display(Name = "入仓日期")]
        //[Required]
        public DateTime? StockInDate
        {
            get;
            set;
        }

        ///// <summary>
        ///// 单价（手工填写，从采购合同里面带过来）
        ///// </summary>
        //[Display(Name = "单价")]
        //public double UnitPrice
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 货币。这里如果不是CNY，则后面可能需要把所有的都转变成CNY计算
        ///// </summary>
        //[Display(Name = "货币")]
        //public string Currency
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 货款小计
        ///// </summary>
        //[NotMapped]
        //public double SubTotal
        //{
        //    get
        //    {
        //        return
        //            Math.Round(
        //            this.NetWeight * this.UnitPrice, 2);
        //    }
        //}

        /// <summary>
        /// 是否已被销售完毕
        /// </summary>
        [Display(Name = "是否已被销售完毕")]
        [Index]
        [Required]
        public bool IsAllSold
        {
            get;
            set;
        }

        [Display(Name = "库存状态")]
        //[NotMapped]
        public StockStatus StockStatus
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual ICollection<StockOutRecord> StockOutRecords
        {
            get;
            set;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("ProductKey:{0}\t", this.ProductKey);
            builder.AppendFormat("商品名称:{0}\t", this.ProductName);
            builder.AppendFormat("件数:{0}\t", this.Quantity);
            builder.AppendFormat("仓卡号:{0}\t", this.StoreHouseMountNumber);
            builder.AppendFormat("入仓吨数:{0}\t", this.StockWeight);
            builder.AppendFormat("OrderContractKey:{0}\t", this.OrderContractKey);

            return builder.ToString();
        }
    }
}
