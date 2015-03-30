using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Finances
{
    ///// <summary>
    ///// 收款记录
    ///// </summary>
    //public class AccountsReceiveRecord : YuShang.ERP.Entities.Finances.IAccountsRecord
    //{
    //    /// <summary>
    //    /// 自增ID
    //    /// </summary>
    //    public int AccountsReceiveRecordId
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// 应收账款记录ID
    //    /// </summary>
    //    public int AccountsReceivableId
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// 应收账款记录
    //    /// </summary>
    //    public virtual AccountsReceivable AccountsReceivable
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// 记录类型，如果说应收账款那就是1；应付账款那就是0；
    //    /// </summary>
    //    [Required]
    //    public FinancialRecordType RecordType { get; set; }

    //    /// <summary>
    //    /// 记录产生日期
    //    /// </summary>
    //    [Display(Name = "记录产生日期")]
    //    [Required]
    //    public DateTime CTIME
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// 金额
    //    /// </summary>
    //    [Display(Name = "金额")]
    //    [Required]
    //    public double Amount
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// （收款时间）年，用于Group By
    //    /// </summary>
    //    [Display(Name = "年")]
    //    [Required]
    //    public int Year
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// （收款时间）月，用于Group By
    //    /// </summary>
    //    [Display(Name = "月")]
    //    [Required]
    //    public int Month
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// （收款时间）日，用于Group By
    //    /// </summary>
    //    [Display(Name = "日")]
    //    [Required]
    //    public int Day
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// 操作用户ID
    //    /// </summary>
    //    [Display(Name = "操作用户ID")]
    //    [MaxLength(128)]
    //    [Required]
    //    public string OperatorSysUserId
    //    {
    //        get;
    //        set;
    //    }

    //    [Display(Name = "备注")]
    //    [MaxLength(256)]
    //    public string Comments
    //    {
    //        get;
    //        set;
    //    }
    //}
}
