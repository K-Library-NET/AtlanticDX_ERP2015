using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Sale
{
    /// <summary>
    /// 销售还价
    /// </summary>
    public class SaleBargain
    {
        public int SaleBargainId
        {
            get;
            set;
        }

        /// <summary>
        /// 还价的销售员的ID
        /// </summary>
        //[Required]
        [MaxLength(100)]
        [Index]
        public string BargainSysUserKey { get; set; }

        /// <summary>
        /// 销售订单号（人工编写，默认全部大写字母加数字）
        /// </summary>
        //[Required]
        //[MaxLength(100)]
        //[Index(IsUnique = false)]
        [Display(Name = "销售订单号")]
        [NotMapped]
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
        /// 操作状态（成交/取消还盘）
        /// </summary>
        [Display(Name = "操作状态（成交/取消还盘）")]
        public int OperationState
        {
            get;
            set;
        }

        public int SaleContractId
        {
            get;
            set;
        }

        public virtual SaleContract SaleContract
        {
            get;
            set;
        }

        /// <summary>
        /// 还价商品项
        /// </summary>
        public virtual ICollection<SaleBargainItem> BargainItems
        {
            get;
            set;
        }

        /// <summary>
        /// 还价总价: DEBUG，还没确认
        /// </summary>
        [NotMapped]
        public double Total
        {
            get
            {//DEBUG
                return this.BargainItems.Sum(m => m.BargainUnitPrice);
            }
        }
    }
}
