using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Orders;

namespace YuShang.ERP.Entities.Finances
{
    /// <summary>
    /// 应付账款（欠着供应商的钱）
    /// </summary>
    public class AccountsPayable
    {
        public int AccountsPayableId
        {
            get;
            set;
        }

        //public virtual ICollection<AccountsRecordRelation> Relations
        //{
        //    get;
        //    set;
        //}


        /// <summary>
        /// 关联的采购合同ID 
        /// </summary>
        public int? OrderContractId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual OrderContract OrderContract
        {
            get;
            set;
        }

        /// <summary>
        /// 关联的采购合同编号
        /// </summary>
        //[MaxLength(100)]
        [Display(Name = "关联的采购合同编号")]
        [NotMapped]
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
        /// 应付账款的支付状态：0，未结清；1，已结清；
        /// </summary>
        [Required]
        [Display(Name = "支付状态")]
        public int PayStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 应付账款金额
        /// </summary>
        [Required]
        [Display(Name = "应付账款金额")]
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

        //public virtual ICollection<AccountsPayRecord> AccountsPayRecords
        //{
        //    get;
        //    set;
        //}
    }
}
