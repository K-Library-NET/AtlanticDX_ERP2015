using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Sale;

namespace YuShang.ERP.Entities.Finances
{
    /// <summary>
    /// 应收账款
    /// </summary>
    public class AccountsReceivable
    {
        public int AccountsReceivableId
        {
            get;
            set;
        }

        /// <summary>
        /// 应收账款的实际财务记录
        /// </summary>
        [Display(Name = "应收账款的实际财务记录")]
        public virtual ICollection<AccountsRecord> Records
        {
            get;
            set;
        }

        /// <summary>
        /// 关联的销售合同ID
        /// </summary>
        public int? SaleContractId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual SaleContract SaleContract
        {
            get;
            set;
        }

        /// <summary>
        /// 关联的销售合同编号
        /// </summary>
        [Display(Name = "关联的销售合同编号")]
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
        /// 费用类别
        /// </summary>
        [Display(Name = "费用类别")]
        [Required]
        public AccountingEventType EventType
        {
            get;
            set;
        }

        /// <summary>
        /// 应收账款的支付状态：0，未结清；1，已结清；
        /// </summary>
        [Display(Name = "支付状态")]
        [Required]
        public int PayStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 应收账款金额
        /// </summary>
        [Display(Name = "应收账款金额")]
        [Required]
        public double Amount
        {
            get;
            set;
        }

        /// <summary>
        /// 实际收款金额，每次变动的时候更新
        /// </summary>
        [Required]
        [Display(Name = "实收账款金额")]
        public double ReceiveAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 应收金额的币种
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Display(Name = "应收金额的币种")]
        public string Currency
        {
            get;
            set;
        }

        [Display(Name = "记录创建时间")]
        [Required]
        public DateTime CTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 记录更新时间
        /// </summary>
        [Display(Name = "记录更新时间")]
        public DateTime UTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Memo
        {
            get;
            set;
        }
    }
}
