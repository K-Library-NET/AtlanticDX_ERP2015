using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Sale;

namespace YuShang.ERP.Entities.Stocks
{
    /// <summary>
    /// 出仓记录
    /// </summary>
    public class StockOutRecord
    {
        public int StockOutRecordId
        {
            get;
            set;
        }

        /// <summary>
        /// 入仓库存项的ID
        /// </summary>
        [Display(Name = "库存项ID")]
        [Required]
        public int StockItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 入库的库存量
        /// Navigation Property
        /// </summary>
        public virtual StockItem StockItem
        {
            get;
            set;
        }

        /// <summary>
        /// 销售合同ID
        /// Used by navigation Property
        /// </summary>
        public int? SaleContractId
        {
            get;
            set;
        }

        /// <summary>
        /// NavigationProperty
        /// </summary>
        public virtual SaleContract SaleContract
        {
            get;
            set;
        }

        /// <summary>
        /// 出仓销售合同编号
        /// </summary>
        //[MaxLength(100)]
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
        /// 采购合同编号
        /// </summary>
        //[Required]
        //[MaxLength(100)]
        //[Index(IsUnique = false)]
        [NotMapped]
        [Display(Name = "采购合同编号")]
        public string OrderContractKey
        {
            get
            {
                if (this.StockItem != null)
                    return this.StockItem.OrderContractKey;
                return string.Empty;
            }
        }

        ///// <summary>
        ///// 货品编号，与Product对象关联
        ///// </summary>
        //[Required]
        //[MaxLength(100)]
        //[Index(IsUnique = false)]
        [NotMapped]
        [Display(Name = "货品编号")]
        public string ProductKey
        {
            get
            {
                if (this.StockItem != null)
                    return this.StockItem.ProductKey;

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
        /// 出仓吨数（手工填写，在出仓过程产生）
        /// </summary>
        [Display(Name = "出仓吨数")]
        //[Required]
        public double? StockWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 仓租费用小计
        /// </summary>
        [Display(Name = "仓费小计")]
        public double? InventoriesFeeSubTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余件数（手工填写）
        /// </summary>
        [Display(Name = "剩余件数")]
        public double? RemainderQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余吨数（手工填写，在出仓过程产生）
        /// </summary>
        [Display(Name = "剩余吨数")]
        public double? RemainderStockWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 出货日期
        /// </summary>
        [Display(Name = "出货日期")]
        //[Required]
        public DateTime? StockOutDate
        {
            get;
            set;
        }

        /// <summary>
        /// 操作用户ID
        /// </summary>
        [Display(Name = "操作用户ID")]
        [MaxLength(128)]
        //[Required]
        public string OperatorSysUserName
        {
            get;
            set;
        }

        [Display(Name = "备注")]
        [MaxLength(256)]
        public string Comments
        {
            get;
            set;
        }
    }
}
