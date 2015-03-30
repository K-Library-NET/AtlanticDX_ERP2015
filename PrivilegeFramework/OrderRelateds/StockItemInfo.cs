using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Stocks;

namespace PrivilegeFramework
{
    public class StockItemInfo
    {
        private YuShang.ERP.Entities.Stocks.StockItem stockItem;

        public StockItemInfo()
        {

        }

        public StockItemInfo(YuShang.ERP.Entities.Stocks.StockItem stockItem)
        {
            this.stockItem = stockItem;
            if (stockItem != null)
            {
                this.IsEnable = true;
                AssignValues(stockItem, this);
            }
        }

        internal static void AssignValues(YuShang.ERP.Entities.Stocks.StockItem source, StockItemInfo target)
        {
            target.IsAllSold = source.IsAllSold;
            target.OrderContractKey = source.OrderContractKey;
            target.ProductItemId = source.ProductItemId;
            target.ProductKey = source.ProductKey;
            target.ProductName = source.ProductName;
            target.Quantity = source.Quantity;
            target.StockInDate = source.StockInDate;
            target.StockItemId = source.StockItemId;
            target.StockStatus = source.StockStatus;
            target.StockWeight = source.StockWeight;
            target.StoreHouseId = source.StoreHouseId;
            target.StoreHouseMountNumber = source.StoreHouseMountNumber;
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

        /// <summary>
        /// 货品出厂编号，与Product对象关联
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

        [Display(Name = "仓库Id")]
        public int StoreHouseId
        {
            get;
            set;
        }

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
        [Display(Name = "采购合同编号")]
        public string OrderContractKey
        {
            get;
            set;
        }

        /// <summary>
        /// 件数（手工填写）
        /// </summary>
        [Display(Name = "件数")]
        public double? Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// 入仓吨数（手工填写，在入仓过程产生）
        /// </summary>
        [Display(Name = "入仓吨数")]
        public double? StockWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 入仓日期
        /// </summary>
        [Display(Name = "入仓日期")]
        public DateTime? StockInDate
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已被销售完毕
        /// </summary>
        [Display(Name = "是否已被销售完毕")]
        public bool IsAllSold
        {
            get;
            set;
        }

        [Display(Name = "库存状态")]
        public StockStatus StockStatus
        {
            get;
            set;
        }

    }
}
