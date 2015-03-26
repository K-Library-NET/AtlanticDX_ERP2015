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

        [Display(Name = "记录创建时间")]
        [Required]
        public DateTime CTIME
        {
            get;
            set;
        }

        //public virtual ICollection<AccountsReceiveRecord> AccountsReceiveRecords
        //{
        //    get;
        //    set;
        //}
    }
}
