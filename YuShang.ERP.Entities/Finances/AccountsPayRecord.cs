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
    ///// （对外）付款记录
    ///// </summary>
    //public class AccountsPayRecord : YuShang.ERP.Entities.Finances.IAccountsRecord
    //{
    //    public int AccountsPayRecordId
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// 应付账款记录ID
    //    /// </summary>
    //    [NotMapped] //DEBUG：只是保证编译通过
    //    public string OperatorSysUserId
    //    {
    //        get;
    //        set;
    //    }

    //    ///// <summary>
    //    ///// 应付账款记录
    //    ///// </summary>
    //    //public virtual AccountsPayable AccountsPayable
    //    //{
    //    //    get;
    //    //    set;
    //    //}

    //    /// <summary>
    //    /// 记录类型，如果说应收账款那就是1；应付账款那就是0；
    //    /// </summary>
    //    // [Required]
    //    [NotMapped] //DEBUG：只是保证编译通过
    //    public FinancialRecordType RecordType { get; set; }

    //    public virtual ICollection<AccountsRecordRelation> RecordRelations
    //    {
    //        get;
    //        set;
    //    }

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
    //    /// 记录更新日期
    //    /// </summary>
    //    [Display(Name = "记录更新日期")]
    //    public DateTime? UTIME
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
    //    /// （付款时间）年，用于Group By
    //    /// </summary>
    //    [Display(Name = "年")]
    //    [Required]
    //    public int Year
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// （付款时间）月，用于Group By
    //    /// </summary>
    //    [Display(Name = "月")]
    //    [Required]
    //    public int Month
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// （付款时间）日，用于Group By
    //    /// </summary>
    //    [Display(Name = "日")]
    //    [Required]
    //    public int Day
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// 操作用户名
    //    /// </summary>
    //    [Display(Name = "操作用户名")]
    //    [MaxLength(128)]
    //    [Required]
    //    public string OperatorSysUserName
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

    //    /// <summary>
    //    /// 因为是Record型数据，并非Entity型数据，所以设置这个字段，
    //    /// 用于标记记录删除，但是不做物理删除
    //    /// </summary>
    //    public bool IsDeleted
    //    {
    //        get;
    //        set;
    //    }
    //}
}
