using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Finances
{
    /// <summary>
    /// 财务记录
    /// </summary>
    public class AccountsRecord : IAccountsRecord
    {
        public int AccountsRecordId
        {
            get;
            set;
        }

        /// <summary>
        /// 记录类型：实收账款、实付账款
        /// </summary>
        [Required]
        [Index(IsClustered = false)]
        [Display(Name = "财务记录类型")]
        public FinancialRecordType RecordType { get; set; }

        ///// <summary>
        ///// 与业务对象的关联关系
        ///// </summary>
        //public virtual ICollection<AccountsRecordRelation> RecordRelations
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 对应的应付账款ID
        /// </summary>
        public int? AccountsPayableId
        {
            get;
            set;
        }

        /// <summary>
        /// 对应的应收账款ID
        /// </summary>
        public int? AccountsReceivableId
        {
            get;
            set;
        }

        /// <summary>
        /// 对应的应付账款
        /// </summary>
        public virtual AccountsPayable AccountsPayable
        {
            get;
            set;
        }

        /// <summary>
        /// 对应的应收账款
        /// </summary>
        public virtual AccountsReceivable AccountsReceivable
        {
            get;
            set;
        }

        /// <summary>
        /// 记录产生日期
        /// </summary>
        [Display(Name = "记录产生日期")]
        [Required]
        public DateTime CTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 记录更新日期
        /// </summary>
        [Display(Name = "记录更新日期")]
        public DateTime? UTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        [Required]
        public double Amount
        {
            get;
            set;
        }

        /// <summary>
        /// 财务记录的币种
        /// </summary>
        [Display(Name = "币种")]
        [Required]
        [MaxLength(100)]
        public string Currency
        {
            get;
            set;
        }

        /// <summary>
        /// 财务记录产生或修改时刻的汇率，目前都是转到人民币
        /// </summary>
        [Display(Name = "汇率")]
        [Required]
        public double CurrencyExchangeRate
        {
            get;
            set;
        }

        /// <summary>
        /// （付款时间）年，用于Group By
        /// </summary>
        [Display(Name = "年")]
        [Required]
        public int Year
        {
            get;
            set;
        }

        /// <summary>
        /// （付款时间）月，用于Group By
        /// </summary>
        [Display(Name = "月")]
        [Required]
        public int Month
        {
            get;
            set;
        }

        /// <summary>
        /// （付款时间）日，用于Group By
        /// </summary>
        [Display(Name = "日")]
        [Required]
        public int Day
        {
            get;
            set;
        }

        /// <summary>
        /// 操作用户名
        /// </summary>
        [Display(Name = "操作用户名")]
        [MaxLength(128)]
        [Required]
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

        /// <summary>
        /// 因为是Record型数据，并非Entity型数据，所以设置这个字段，
        /// 用于标记记录删除，但是不做物理删除
        /// </summary>
        public bool IsDeleted
        {
            get;
            set;
        }
    }
}
